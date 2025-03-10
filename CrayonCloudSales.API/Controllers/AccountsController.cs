using Microsoft.AspNetCore.Mvc;
using CrayonCloudSales.Core.Interfaces;
using CrayonCloudSales.API.Models;

namespace CrayonCloudSales.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        
        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCustomerAccounts([FromQuery] int customerId)
        {
            if (customerId <= 0)
                return BadRequest("Invalid customer ID");
                
            var accounts = await _accountRepository.GetAccountsByCustomerIdAsync(customerId);
            
            var accountDtos = accounts.Select(a => new AccountDto
            {
                Id = a.Id,
                Name = a.Name,
                CreatedDate = a.CreatedDate
            });
            
            return Ok(accountDtos);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id, [FromQuery] int customerId)
        {
            if (id <= 0 || customerId <= 0)
                return BadRequest("Invalid IDs provided");
                
            // Verify that the account belongs to the customer
            bool accountBelongsToCustomer = await _accountRepository.AccountBelongsToCustomerAsync(id, customerId);
            if (!accountBelongsToCustomer)
                return StatusCode(403);
                
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
                return NotFound();
                
            var accountDto = new AccountDto
            {
                Id = account.Id,
                Name = account.Name,
                CreatedDate = account.CreatedDate
            };
            
            return Ok(accountDto);
        }
    }
}

