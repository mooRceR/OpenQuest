using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenRec.Tools
{
    internal static class GithubTool
    {
        static HttpClient H;
        public static void Setup()
        {
            H = new HttpClient();
        }

        static string BasePath = "https://raw.githubusercontent.com/mooRceR/OpenQuest/master/";

        public static async Task<string> GetString(string Path)
        {
            return await H.GetStringAsync(BasePath + Path);
        }

        public static async Task<float> GetFloat(string Path)
        {
            return float.Parse(await H.GetStringAsync(BasePath + Path));
        }

        public static async Task<T> Get<T>(string Path)
        {
            return JsonConvert.DeserializeObject<T>(await H.GetStringAsync(BasePath + Path));
        }
    }
}
