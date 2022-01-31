using API.DTOs.Users;
using API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static WEB.Helper.RestFullClientUtils;

namespace WEB.Controllers
{
    public class LoginController : BaseController
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Logearse en el sistema.
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <param name="pass">Password</param>
        /// <returns>retorna json con datos de session o inicio de session invalido</returns>
        public AuthenticateResponse Login(string user, string pass)
        {
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson("users/authenticate", new Dictionary<string, object>() { { "username", user }, { "password", pass } }, "", Methop.POST).Result;
            if (ResultJson != "null")
            {
                var ResultData = JsonConvert.DeserializeObject<AuthenticateResponse>(ResultJson);
                if (ResultData.Success)
                {
                    TempSession.Token = ResultData.Token;
                    TempSession.Username = ResultData.User.UserName;
                    //HttpContext.Session.SetString("_token", ResultData.Token);
                    //HttpContext.Session.SetString("_username", ResultData.User.UserName);
                }
                return ResultData;
            }
            AuthenticateResponse resp = new AuthenticateResponse();
            resp.Success = false;
            resp.Message = @"Incorrect user or password";
            return resp;
        }


    }
}
