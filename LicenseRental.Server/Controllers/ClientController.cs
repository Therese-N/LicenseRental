using LicenseRental.Server.Services.Interfaces;
using LicenseRental.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicenseRental.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Client>> Get(Guid? id)
        {
            return Ok(await _clientService.GetClient(id.Value));
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Add([FromBody] Guid id)
        {
            return Ok(await _clientService.AddClientAsync(id));
        }
    }
}
