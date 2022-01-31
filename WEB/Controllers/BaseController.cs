using Microsoft.AspNetCore.Mvc;
using System.Web;
using WEB.Helper;

namespace WEB.Controllers
{
	public class BaseController : Controller
	{
#if DEBUG
		public RestFullClientUtils _Data = new RestFullClientUtils("http://localhost", "54399");
#else
		public RestFullClientUtils _Data = new RestFullClientUtils("http://localhost", "54399");
#endif

	}
}