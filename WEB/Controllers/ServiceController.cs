using API.DTOs.Providers;
using API.DTOs.Services;
using API.DTOs.Users;
using API.Extensions;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static WEB.Helper.RestFullClientUtils;

namespace WEB.Controllers
{
    public class ServiceController : BaseController
    {
        // GET: ProviderController
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Obtener la lista de servicios.
        /// </summary>
        /// <param name="search">Busqueda</param>
        /// <param name="page">Numero de pagina</param>
        /// <param name="pagesize">Cantidad de paginas</param>
        /// <returns>ProviderInfoDTO</returns>
        public ServiceInfoDTO GetService(string search, int page, int pagesize)
        {
            ServiceInfoDTO resp = new ServiceInfoDTO();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("services?{0}Page={1}&PageSize={2}", (search == string.Empty) ? "" : String.Format("Search={0}&", search), page, pagesize), null, TempSession.Token, Methop.GET).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<ServiceInfoDTO>(ResultJson);
                return resp;
            }
            return resp;
        }

        /// <summary>
        /// Insertar o Actualizar servicio.
        /// </summary>
        /// <param name="service">Objeto a actualizar</param>
        /// <returns>ProviderInfoDTO</returns>
        public UpdateServiceInfoDTO SaveService(Service service)
        {
            UpdateServiceInfoDTO resp = new UpdateServiceInfoDTO();
            Dictionary<string, object> Update = new Dictionary<string, object>();
            Update.Add("Name", service.Name);
            Update.Add("Description", service.Description);
            //Iniciar Session.
            string ResultJson = "";
            if (service.Id == 0)
                ResultJson = _Data.GetDataServiceJson(String.Format("services"), Update, TempSession.Token, Methop.POST).Result;
            else
                ResultJson = _Data.GetDataServiceJson(String.Format("services/{0}", service.Id), Update, TempSession.Token, Methop.PUT).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<UpdateServiceInfoDTO>(ResultJson);
                return resp;
            }
            return resp;
        }

        /// <summary>
        /// Eliminar servicio.
        /// </summary>
        /// <param name="service">Objeto a eliminar</param>
        /// <returns>ProviderInfoDTO</returns>
        public UpdateServiceInfoDTO DeleteService(Service service)
        {
            UpdateServiceInfoDTO resp = new UpdateServiceInfoDTO();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("services/{0}", service.Id), null, TempSession.Token, Methop.DELETE).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<UpdateServiceInfoDTO>(ResultJson);
                return resp;
            }
            return resp;
        }

    }
}
