using API.DTOs.Providers;
using API.DTOs.Users;
using API.Extensions;
using Domain.ProviderDetails;
using Domain.Providers;
using Domain.ProviderServices;
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
            if (TempSession.Token == "")
                return RedirectToAction("Index", "Login", null);
            return View();
        }

        public ActionResult Details(int id)
        {
            if (TempSession.Token == "")
                return RedirectToAction("Index", "Login", null);
            TempSession.IDProviderSelect = id;
            return View();
        }

        #region Provider

        /// <summary>
        /// Obtener un proveedor.
        /// </summary>
        /// <param name="search">Busqueda</param>
        /// <param name="page">Numero de pagina</param>
        /// <param name="pagesize">Cantidad de paginas</param>
        /// <returns>ProviderInfoDTO</returns>
        public Provider GetProvider()
        {
            Provider resp = new Provider();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("providers/{0}", TempSession.IDProviderSelect), null, TempSession.Token, Methop.GET).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<Provider>(ResultJson);
                return resp;
            }
            else
            {
                TempSession.Token = "";
            }
            return resp;
        }

        /// <summary>
        /// Obtener la lista de proveedores.
        /// </summary>
        /// <param name="search">Busqueda</param>
        /// <param name="page">Numero de pagina</param>
        /// <param name="pagesize">Cantidad de paginas</param>
        /// <returns>ProviderInfoDTO</returns>
        public ProviderInfoDTO GetProviderList(string search, int page, int pagesize)
        {
            ProviderInfoDTO resp = new ProviderInfoDTO();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("providers?{0}Page={1}&PageSize={2}", (search == string.Empty) ? "" : String.Format("Search={0}&", search), page, pagesize), null, TempSession.Token, Methop.GET).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<ProviderInfoDTO>(ResultJson);
                return resp;
            }
            else
            {
                TempSession.Token = "";
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
            else
            {
                TempSession.Token = "";
            }
            return resp;
        }

        /// <summary>
        /// Eliminar provider.
        /// </summary>
        /// <param name="provider">Objeto a eliminar</param>
        /// <returns>ProviderInfoDTO</returns>
        public UpdateProviderInfoDTO DeleteProvider(Provider provider)
        {
            UpdateProviderInfoDTO resp = new UpdateProviderInfoDTO();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("providers/{0}", provider.Id), null, TempSession.Token, Methop.DELETE).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<UpdateProviderInfoDTO>(ResultJson);
                return resp;
            }
            else
            {
                TempSession.Token = "";
            }
            return resp;
        }
        #endregion

        #region Provider Services
        /// <summary>
        /// Obtener la lista de servicios de cada proveedor.
        /// </summary>
        /// <param name="search">Busqueda</param>
        /// <param name="page">Numero de pagina</param>
        /// <param name="pagesize">Cantidad de paginas</param>
        /// <returns>ProviderInfoDTO</returns>
        public ProviderServiceInfoDTO GetProviderServices(string search, int page, int pagesize)
        {
            ProviderServiceInfoDTO resp = new ProviderServiceInfoDTO();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("providers/{0}/services?{1}Page={2}&PageSize={3}", TempSession.IDProviderSelect, (search == string.Empty) ? "" : String.Format("Search={0}&", search), page, pagesize), null, TempSession.Token, Methop.GET).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<ProviderServiceInfoDTO>(ResultJson);
                return resp;
            }
            else
            {
                TempSession.Token = "";
            }
            return resp;
        }

        /// <summary>
        /// Insertar o Actualizar servicio de proveedor.
        /// </summary>
        /// <param name="providerService">Objeto a actualizar</param>
        /// <returns>UpdateProviderInfoDTO</returns>
        public AddServiceProviderInfoDTO SaveProviderService(ProviderService providerService)
        {
            AddServiceProviderInfoDTO resp = new AddServiceProviderInfoDTO();
            Dictionary<string, object> Update = new Dictionary<string, object>();
            if (providerService.Id == 0)
                Update.Add("IDService", providerService.IDService);
            Update.Add("PriceHour", providerService.PriceHour);
            Update.Add("CountryISO", providerService.CountryISO);
            //Iniciar Session.
            string ResultJson = "";
            if (providerService.Id == 0)
                ResultJson = _Data.GetDataServiceJson(String.Format("providers/{0}/services", TempSession.IDProviderSelect), Update, TempSession.Token, Methop.POST).Result;
            else
                ResultJson = _Data.GetDataServiceJson(String.Format("providers/services/{0}", providerService.Id), Update, TempSession.Token, Methop.PUT).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<AddServiceProviderInfoDTO>(ResultJson);
                return resp;
            }
            else
            {
                TempSession.Token = "";
            }
            return resp;
        }

        #endregion

        #region Provider Details

        /// <summary>
        /// Obtener la lista de detalles de cada proveedor.
        /// </summary>
        /// <param name="search">Busqueda</param>
        /// <param name="page">Numero de pagina</param>
        /// <param name="pagesize">Cantidad de paginas</param>
        /// <returns>ProviderInfoDTO</returns>
        public ProviderDetailInfoDTO GetProviderDetails(string search, int page, int pagesize)
        {
            ProviderDetailInfoDTO resp = new ProviderDetailInfoDTO();
            //Iniciar Session.
            var ResultJson = _Data.GetDataServiceJson(String.Format("providers/{0}/details?{1}Page={2}&PageSize={3}", TempSession.IDProviderSelect, (search == string.Empty) ? "" : String.Format("Search={0}&", search), page, pagesize), null, TempSession.Token, Methop.GET).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<ProviderDetailInfoDTO>(ResultJson);
                return resp;
            }
            else
            {
                TempSession.Token = "";
            }
            return resp;
        }

        /// <summary>
        /// Insertar o Actualizar detalles de proveedor.
        /// </summary>
        /// <param name="providerDetail">Objeto a actualizar</param>
        /// <returns>UpdateProviderInfoDTO</returns>
        public AddDetailProviderInfoDTO SaveProviderDatails(ProviderDetail providerDetail)
        {
            AddDetailProviderInfoDTO resp = new AddDetailProviderInfoDTO();
            Dictionary<string, object> Update = new Dictionary<string, object>();
            if (providerDetail.Id == 0)
                Update.Add("RowName", providerDetail.RowName);
            Update.Add("RowValue", providerDetail.RowValue);
            //Iniciar Session.
            string ResultJson = "";
            if (providerDetail.Id == 0)
                ResultJson = _Data.GetDataServiceJson(String.Format("providers/{0}/details", TempSession.IDProviderSelect), Update, TempSession.Token, Methop.POST).Result;
            else
                ResultJson = _Data.GetDataServiceJson(String.Format("providers/details/{0}", providerDetail.Id), Update, TempSession.Token, Methop.PUT).Result;
            if (ResultJson != "null")
            {
                resp = JsonConvert.DeserializeObject<AddDetailProviderInfoDTO>(ResultJson);
                return resp;
            }
            else
            {
                TempSession.Token = "";
            }
            return resp;
        }

        #endregion
    }
}
