using Microsoft.AspNetCore.Mvc;
using CrayonCloudSales.Core.Interfaces;
using CrayonCloudSales.Core.Models;
using CrayonCloudSales.API.Models;

namespace CrayonCloudSales.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareLicensesController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISoftwareLicenseRepository _softwareLicenseRepository;
        private readonly ICCPService _ccpService;

        public SoftwareLicensesController(
            IAccountRepository accountRepository,
            ISoftwareLicenseRepository softwareLicenseRepository,
            ICCPService ccpService)
        {
            _accountRepository = accountRepository;
            _softwareLicenseRepository = softwareLicenseRepository;
            _ccpService = ccpService;
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetLicensesByAccount(int accountId, [FromQuery] int customerId)
        {
            if (accountId <= 0 || customerId <= 0)
                return BadRequest("Invalid IDs provided");

            bool accountBelongsToCustomer = await _accountRepository.AccountBelongsToCustomerAsync(accountId, customerId);
            if (!accountBelongsToCustomer)
                return StatusCode(403);

            var licenses = await _softwareLicenseRepository.GetLicensesByAccountIdAsync(accountId);

            var licenseDtos = licenses.Select(async l =>
            {
                var softwareService = await _ccpService.GetServiceByIdAsync(l.SoftwareServiceId);
                return new SoftwareLicenseDto
                {
                    Id = l.Id,
                    SoftwareName = softwareService.Name ?? "Unknown",
                    Quantity = l.Quantity,
                    State = l.State.ToString(),
                    PurchaseDate = l.PurchaseDate,
                    ValidToDate = l.ValidToDate
                };
            }).Select(l => l.Result);

            return Ok(licenseDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLicenseById(int id, [FromQuery] int customerId)
        {
            if (id <= 0 || customerId <= 0)
                return BadRequest("Invalid IDs provided");

            var license = await _softwareLicenseRepository.GetLicenseByIdAsync(id);
            if (license == null)
                return NotFound();

            bool accountBelongsToCustomer = await _accountRepository.AccountBelongsToCustomerAsync(license.AccountId, customerId);
            if (!accountBelongsToCustomer)
                return StatusCode(403);

            var softwareService = await _ccpService.GetServiceByIdAsync(license.SoftwareServiceId);
            if (softwareService == null)
            {
                return NotFound("Software service not found");
            }

            var licenseDto = new SoftwareLicenseDto
            {
                Id = license.Id,
                SoftwareName = softwareService.Name ?? "Unknown",
                Quantity = license.Quantity,
                State = license.State.ToString(),
                PurchaseDate = license.PurchaseDate,
                ValidToDate = license.ValidToDate
            };

            return Ok(licenseDto);
        }

        [HttpPost("order")]
        public async Task<IActionResult> OrderSoftware([FromBody] OrderSoftwareRequest request)
        {
            if (request.AccountId <= 0 || request.CustomerId <= 0 || request.SoftwareServiceId <= 0 || request.Quantity <= 0)
                return BadRequest("Invalid request parameters");

            bool accountBelongsToCustomer = await _accountRepository.AccountBelongsToCustomerAsync(request.AccountId, request.CustomerId);
            if (!accountBelongsToCustomer)
                return StatusCode(403);

            var softwareService = await _ccpService.GetServiceByIdAsync(request.SoftwareServiceId);
            if (softwareService == null)
            {
                return NotFound("Software service not found");
            }

            var licenseExists = await _softwareLicenseRepository.LicenseExists(request.AccountId, request.SoftwareServiceId);
            if (licenseExists)
            {
                return BadRequest("License already exists");
            }

            var (success, subscriptionId) = await _ccpService.OrderSoftwareAsync(
                softwareService.Id,
                request.Quantity,
                request.ValidToDate);

            if (!success)
                return StatusCode(500, "Failed to order software from CCP");

            var license = new SoftwareLicense
            {
                AccountId = request.AccountId,
                SoftwareServiceId = request.SoftwareServiceId,
                Quantity = request.Quantity,
                State = LicenseState.Active,
                PurchaseDate = DateTime.UtcNow,
                ValidToDate = request.ValidToDate,
                CcpSubscriptionId = subscriptionId
            };

            var addedLicense = await _softwareLicenseRepository.AddLicenseAsync(license);

            var licenseDto = new SoftwareLicenseDto
            {
                Id = addedLicense.Id,
                SoftwareName = softwareService.Name,
                Quantity = addedLicense.Quantity,
                State = addedLicense.State.ToString(),
                PurchaseDate = addedLicense.PurchaseDate,
                ValidToDate = addedLicense.ValidToDate
            };

            return CreatedAtAction(nameof(GetLicenseById), new { id = addedLicense.Id }, licenseDto);
        }

        [HttpPut("{id}/quantity")]
        public async Task<IActionResult> UpdateLicenseQuantity(int id, [FromBody] UpdateQuantityRequest request)
        {
            if (id <= 0 || request.CustomerId <= 0 || request.NewQuantity <= 0)
                return BadRequest("Invalid request parameters");

            var license = await _softwareLicenseRepository.GetLicenseByIdAsync(id);
            if (license == null)
                return NotFound();

            bool accountBelongsToCustomer = await _accountRepository.AccountBelongsToCustomerAsync(license.AccountId, request.CustomerId);
            if (!accountBelongsToCustomer)
                return StatusCode(403);

            bool updateSuccess = await _ccpService.UpdateSoftwareQuantityAsync(license.CcpSubscriptionId, request.NewQuantity);
            if (!updateSuccess)
                return StatusCode(500, "Failed to update license quantity with CCP");

            license.Quantity = request.NewQuantity;
            await _softwareLicenseRepository.UpdateLicenseAsync(license);

            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelLicense(int id, [FromQuery] int customerId)
        {
            if (id <= 0 || customerId <= 0)
                return BadRequest("Invalid IDs provided");

            var license = await _softwareLicenseRepository.GetLicenseByIdAsync(id);
            if (license == null)
                return NotFound();

            if (license.State == LicenseState.Canceled)
                return BadRequest("License is already canceled");

            bool accountBelongsToCustomer = await _accountRepository.AccountBelongsToCustomerAsync(license.AccountId, customerId);
            if (!accountBelongsToCustomer)
                return StatusCode(403);

            bool cancelSuccess = await _ccpService.CancelSoftwareAsync(license.CcpSubscriptionId);
            if (!cancelSuccess)
                return StatusCode(500, "Failed to cancel license with CCP");

            license.State = LicenseState.Canceled;
            await _softwareLicenseRepository.UpdateLicenseAsync(license);

            return NoContent();
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateLicense(int id, [FromQuery] int customerId)
        {
            if (id <= 0 || customerId <= 0)
                return BadRequest("Invalid IDs provided");

            var license = await _softwareLicenseRepository.GetLicenseByIdAsync(id);
            if (license == null)
                return NotFound();

            if (license.State == LicenseState.Active)
                return BadRequest("License is already active");

            bool accountBelongsToCustomer = await _accountRepository.AccountBelongsToCustomerAsync(license.AccountId, customerId);
            if (!accountBelongsToCustomer)
                return StatusCode(403);

            bool cancelSuccess = await _ccpService.ActivateSoftwareAsync(license.CcpSubscriptionId);
            if (!cancelSuccess)
                return StatusCode(500, "Failed to cancel license with CCP");

            license.State = LicenseState.Active;
            await _softwareLicenseRepository.UpdateLicenseAsync(license);

            return NoContent();
        }

        [HttpPut("{id}/extend")]
        public async Task<IActionResult> ExtendLicense(int id, [FromBody] ExtendLicenseRequest request)
        {
            if (id <= 0 || request.CustomerId <= 0 || request.NewValidToDate <= DateTime.UtcNow)
                return BadRequest("Invalid request parameters");

            var license = await _softwareLicenseRepository.GetLicenseByIdAsync(id);
            if (license == null)
                return NotFound();

            bool accountBelongsToCustomer = await _accountRepository.AccountBelongsToCustomerAsync(license.AccountId, request.CustomerId);
            if (!accountBelongsToCustomer)
                return StatusCode(403);

            bool extendSuccess = await _ccpService.ExtendSoftwareLicenseAsync(license.CcpSubscriptionId, request.NewValidToDate);
            if (!extendSuccess)
                return StatusCode(500, "Failed to extend license with CCP");

            license.ValidToDate = request.NewValidToDate;
            await _softwareLicenseRepository.UpdateLicenseAsync(license);

            return NoContent();
        }
    }
}
