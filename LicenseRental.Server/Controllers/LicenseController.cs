using LicenseRental.Server.Services.Interfaces;
using LicenseRental.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicenseRental.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _licenseService;

        public LicenseController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_licenseService.GetLicenses());
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] string name)
        {
            try
            {
                return Ok(await _licenseService.AddLicenseAsync(name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("{clientId}")]
        [HttpGet]
        public async Task<ActionResult<License>> Get(Guid? clientId)
        {
            var license = await _licenseService.GetLicenseByClientId(clientId.Value);
            return license == null ? new NotFoundResult() : new OkObjectResult(license);
        }

        [HttpPut]
        public async Task<ActionResult<License>> Update([FromBody] Guid renterId)
        {
            try
            {
                var license = await _licenseService.UpdateLicenseAsync(renterId);
                return Ok(license);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
