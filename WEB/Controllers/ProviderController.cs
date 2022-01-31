using API.DTOs.Providers;
using API.DTOs.Users;
using API.Extensions;
using Domain.Providers;
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

        /// <summary>
        /// Insertar o Actualizar provider.
        /// </summary>
        /// <param name="provider">Objeto a actualizar</param>
        /// <returns>UpdateProviderInfoDTO</returns>
        public UpdateProviderInfoDTO SaveProvider(Provider provider)
        {
            UpdateProviderInfoDTO resp = new UpdateProviderInfoDTO();
            Dictionary<string, object> Update = new Dictionary<string, object>();
            Update.Add("Name", provider.Name);
            Update.Add("Nit", provider.NIT);
            Update.Add("Email", provider.Email);
            //Iniciar Session.
            string ResultJson = "";
            if (provider.Id == 0)
                ResultJson = _Data.GetDataServiceJson(String.Format("providers"), Update, TempSession.Token, Methop.POST).Result;
            else
                ResultJson = _Data.GetDataServiceJson(String.Format("providers/{0}", provider.Id), Update, TempSession.Token, Methop.PUT).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<UpdateProviderInfoDTO>(ResultJson);
                return resp;
            }
            return resp;
        }

        /// <summary>
        /// Eliminar provider.
        /// </summary>
        /// <param name="provider">Objeto a eliminar</param>
        /// <returns>ProviderInfoDTO</returns>
        public UpdateProviderInfoDTO DeleteService(Provider provider)
        {
            UpdateProviderInfoDTO resp = new UpdateProviderInfoDTO();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("providers/{0}", provider.Id), null, TempSession.Token, Methop.DELETE).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<UpdateProviderInfoDTO>(ResultJson);
                return resp;
            }
            return resp;
        }
    }
}
