using API.DTOs.Users;
using API.Helpers;
using API.Services.Dashboard;
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
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _service;
        private readonly ILogger<UserController> _logger;

        public DashboardController(ILogger<UserController> logger, DashboardService service)
        {
            _service = service;
            _logger = logger;
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        { 
            var dashboard = await _service.DashboardAsync();
            return Ok(dashboard);
        }
    }
}