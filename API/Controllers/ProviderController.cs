using API.DTOs.Users;
using API.Helpers;
using API.Services.Providers;
using API.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("providers")]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService _service;
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(ILogger<ProviderController> logger
            , ProviderService service)
        {
            _service = service;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProviderRequest request)
        {
            var users = await _service.SearchAsync(request);
            return Ok(users);
        }
    }
}