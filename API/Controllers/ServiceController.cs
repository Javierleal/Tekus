using API.DTOs.Services;
using API.DTOs.Users;
using API.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("services")]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceService _service;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ILogger<ServiceController> logger, ServiceService service)
        {
            _service = service;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetServiceRequest request)
        {
            var users = await _service.SearchAsync(request);
            return Ok(users);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(AddServiceRequest request)
        {
            var Service = await _service.AddServiceAsync(request);
            return Ok(Service);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, UpdateServiceRequest request)
        {
            var Service = await _service.UpdateServiceAsync(id, request);
            return Ok(Service);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Service = await _service.DeleteServiceAsync(id);
            return Ok(Service);
        }
    }
}