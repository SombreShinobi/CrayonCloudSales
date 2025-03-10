using Microsoft.AspNetCore.Mvc;
using CrayonCloudSales.Core.Interfaces;
using CrayonCloudSales.API.Models;

namespace CrayonCloudSales.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareServicesController : ControllerBase
    {
        private readonly ICCPService _ccpService;
        
        public SoftwareServicesController(ICCPService ccpService)
        {
            _ccpService = ccpService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAvailableServices()
        {
            var services = await _ccpService.GetAvailableServicesAsync();
            
            var serviceDtos = services.Select(s => new SoftwareServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                PricePerLicense = s.PricePerLicense
            });
            
            return Ok(serviceDtos);
        }
    }
}
