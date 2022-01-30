using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public async static Task<string> CountryNameByISO(this string ISO)
        {
            try
            {
                string ResultService = "";
                using (HttpClient client = new HttpClient())
                {
                    var Getresponse = client.GetAsync("https://restcountries.com/v3.1/alpha/" + ISO.ToUpper()).Result;
                    if (Getresponse.IsSuccessStatusCode)
                    {
                        string htmlData = await Getresponse.Content.ReadAsStringAsync();
                        ResultService = htmlData.Replace("\\\\\\\"", "\"").Replace("\\", "").Replace("\"{\"", "{\"").Replace("}\"", "}").Replace("\"[", "[").Replace("]\"", "]");
                    }
                }
                var Result = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ResultService);
                var ResultName = JsonConvert.DeserializeObject<Dictionary<string, object>>(Result.FirstOrDefault()["name"].ToString());
                return ResultName["common"].ToString();
            }
            catch (System.Exception)
            {
                return "";
            }
        }
    }
}