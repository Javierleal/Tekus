using API.DTOs.Dashboard;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WEB.Models;
using static WEB.Helper.RestFullClientUtils;

namespace WEB.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (TempSession.Token == "")
                return RedirectToAction("Index", "Login", null);
            return View();
        }

        /// <summary>
        /// Obtener datos del dashboard.
        /// </summary>
        /// <returns>ProviderInfoDTO</returns>
        public DashBoardResult GetDashboard()
        {
            DashBoardResult resp = new DashBoardResult();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("dashboard"), null, TempSession.Token, Methop.GET).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<DashBoardResult>(ResultJson);
                return resp;
            }
            else
            {
                TempSession.Token = "";
            }
            return resp;
        }

    }
}