using API.DTOs.Providers;
using API.DTOs.Users;
using API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static WEB.Helper.RestFullClientUtils;

namespace WEB.Controllers
{
    public class ProviderController : BaseController
    {
        // GET: ProviderController
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Obtener la lista de proveedores.
        /// </summary>
        /// <param name="search">Busqueda</param>
        /// <param name="page">Numero de pagina</param>
        /// <param name="pagesize">Cantidad de paginas</param>
        /// <returns>ProviderInfoDTO</returns>
        public ProviderInfoDTO GetProvider(string search, int page, int pagesize)
        {
            ProviderInfoDTO resp = new ProviderInfoDTO();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("providers?{0}Page={1}&PageSize={2}", (search == string.Empty) ? "" : String.Format("Search={0}&", search), page, pagesize), null, TempSession.Token, Methop.GET).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<ProviderInfoDTO>(ResultJson);
                return resp;
            }
            return resp;
        }


    }
}
