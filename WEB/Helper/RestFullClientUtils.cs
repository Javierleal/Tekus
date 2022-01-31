using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WEB.Helper
{
	public class RestFullClientUtils
	{
		public RestFullClientUtils()
		{

		}

		public RestFullClientUtils(string _server, string _port = "")
		{
			Server = _server;
			Port = _port;
		}

		public enum User
		{
			id_user,
			user_dni,
			user_email,
			user_firstname,
			user_lastname,
			id_company,
			user_sex,
			user_password,
			user_color
		}

		public enum Methop
		{
			POST,
			GET,
			DELETE,
			PUT
		}

		public enum ResponseValues
		{
			Success,
			AditionalData,
			Message,
			Value,
			id,
			Token
		}

		public string Server { get; set; }
		public string Port { get; set; }

		/// <summary>
		/// Metodo para relizar consulta a servicios de vanguarsoft
		/// </summary>
		/// <param name="Server">Url del server</param>
		/// <param name="MethodName">Nombre del metodo</param>
		/// <param name="Token"></param>
		/// <param name="Parameters"></param>
		/// <param name="MethodType"></param>
		/// <param name="Port"></param>
		/// <returns></returns>
		public async Task<string> GetDataServiceJson(string MethodName, Dictionary<string, object> Parameters = null, string Token = "123", Methop MethodType = Methop.GET, bool ReturnText = false, string _Server = "", string _Port = "")
		{
			if (_Server == "")
				_Server = Server;
			if (_Port == "")
				_Port = Port;
			return JsonConvert.SerializeObject(await GetDataService(_Server, _Port, "", MethodName, Parameters, Token, MethodType, ReturnText));
		}

		/// <summary>
		/// Metodo para relizar consulta a servicios de vanguarsoft
		/// </summary>
		/// <param name="Server">Url del server</param>
		/// <param name="MethodName">Nombre del metodo</param>
		/// <param name="Token"></param>
		/// <param name="Parameters"></param>
		/// <param name="MethodType"></param>
		/// <param name="Port"></param>
		/// <returns></returns>
		public async Task<Dictionary<string, object>> GetDataServiceObject(string Server, string Port, string NameWCF, string MethodName, Dictionary<string, object> Parameters = null, string Token = "123", Methop MethodType = Methop.GET)
		{
			return await GetDataService(Server, Port, NameWCF, MethodName, Parameters, Token, MethodType);
		}


		/// <summary>
		/// Metodo para relizar consulta a servicios de vanguarsoft
		/// </summary>
		/// <param name="Server">Url del server</param>
		/// <param name="MethodName">Nombre del metodo</param>
		/// <param name="Token"></param>
		/// <param name="Parameters"></param>
		/// <param name="MethodType"></param>
		/// <param name="Port"></param>
		/// <returns></returns>
		public async Task<Dictionary<string, object>> GetDataService(string Server, string Port, string NameWCF, string MethodName, Dictionary<string, object> Parameters = null, string Token = "123", Methop MethodType = Methop.GET, bool returnText = false)
		{
			Dictionary<string, object> Result = null;
			try
			{
				StringBuilder parametros = new StringBuilder();
				if (Port != "")
				{
					Port = ":" + Port;
				}
				if (NameWCF != "")
				{
					NameWCF = "/" + NameWCF;
				}
				string URI = string.Format("{0}{1}{2}/{3}", Server, Port, NameWCF, MethodName);
				if (Parameters != null)
				{
					foreach (var item in Parameters)
					{
						parametros.AppendFormat("&{0}={1}", item.Key, item.Value);
					}
				}
				string ResultService = "";
				using (HttpClient client = new HttpClient())
				{
					if (Token != "")
					{
						client.DefaultRequestHeaders.Authorization =
							new AuthenticationHeaderValue("Bearer", Token);
					}
					var uri = new Uri(URI);
					switch (MethodType)
					{
						case Methop.PUT:
						case Methop.POST:
							Dictionary<string, object> ReorganizeParameter = new Dictionary<string, object>();
							//ReorganizeParameter.Add("token", Token);
							foreach (var item in Parameters)
							{
								ReorganizeParameter.Add(item.Key, item.Value);
							}
							var myContent = JsonConvert.SerializeObject(ReorganizeParameter);
							var buffer = Encoding.UTF8.GetBytes(myContent);
							var byteContent = new ByteArrayContent(buffer);
							byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
							var Postresponse = client.PostAsync(uri, byteContent).Result;
							if (Postresponse.IsSuccessStatusCode)
							{
								string htmlData = await Postresponse.Content.ReadAsStringAsync();
								ResultService = (returnText) ? htmlData : htmlData.Replace("\\\\\\\"", "\"").Replace("\\", "").Replace("\"{\"", "{\"").Replace("}\"", "}").Replace("\"[", "[").Replace("]\"", "]");
							}
							break;
						case Methop.GET:
							var Getresponse = client.GetAsync(uri + parametros.ToString()).Result;
							if (Getresponse.IsSuccessStatusCode)
							{
								string htmlData = await Getresponse.Content.ReadAsStringAsync();
								ResultService = (returnText) ? htmlData : htmlData.Replace("\\\\\\\"", "\"").Replace("\\", "").Replace("\"{\"", "{\"").Replace("}\"", "}").Replace("\"[", "[").Replace("]\"", "]");
							}
							break;
						case Methop.DELETE:
							Getresponse = client.DeleteAsync(uri + parametros.ToString()).Result;
							if (Getresponse.IsSuccessStatusCode)
							{
								string htmlData = await Getresponse.Content.ReadAsStringAsync();
								ResultService = (returnText) ? htmlData : htmlData.Replace("\\\\\\\"", "\"").Replace("\\", "").Replace("\"{\"", "{\"").Replace("}\"", "}").Replace("\"[", "[").Replace("]\"", "]");
							}
							break;
						default:
							break;
					}
				}
				var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "" };
				if (returnText)
					Result = new Dictionary<string, object>() { { "Value", ResultService.Replace("\"", "") }, { "Success", true } };
				else
					Result = JsonConvert.DeserializeObject<Dictionary<string, object>>(ResultService, dateTimeConverter);
			}
			catch (Exception ex)
			{
				Dictionary<string, object> ErrorData = new Dictionary<string, object>();
				ErrorData.Add("Success", false);
				ErrorData.Add("Message", "VangData: " + JsonConvert.SerializeObject(ex));
				return ErrorData;
			}
			return Result;
		}
	}
}
