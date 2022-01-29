using API.DTOs.Users;
using API.Helpers;
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
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ILogger<UserController> _logger;
        private readonly IJwtAuthManager _jwtAuthManager;

        public UserController(ILogger<UserController> logger, UserService service, IJwtAuthManager jwtAuthManager)
        {
            _service = service;
            _logger = logger;
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _service.Authenticate(model, _jwtAuthManager);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserRequest request)
        { 
            var users = await _service.SearchAsync(request);
            return Ok(users);
        }
    }
}