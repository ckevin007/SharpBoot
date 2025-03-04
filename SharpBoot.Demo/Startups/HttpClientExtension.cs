using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Encodings.Web;
using System.Web;
using Newtonsoft.Json;

namespace SharpBoot.Demo.Startups
{
    public static class HttpClientExtension
    {

        public static async Task<string> GetStringAsync(this HttpClient client, string url, object obj)
        {
            var body = await client.GetStringAsync(url + MapUrl(obj));
            return body;
        }


        public static async Task<T> GetAsync<T>(this HttpClient client, string url, object obj)
        {
            var body = await GetStringAsync(client, url, obj);
            return JsonConvert.DeserializeObject<T>(body);
        }

        private static Dictionary<string, object> Map(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            if (properties == null || properties.Length == 0) return null;
            Dictionary<string, object> dik = new Dictionary<string, object>();
            foreach (var p in properties)
            {
                dik[p.Name] = p.GetValue(obj);
            }
            return dik;
        }

        private static string MapUrl(object obj)
        {
            Dictionary<string, object> map = Map(obj);
            if (map == null) return "";
            List<string> sb = map.AsEnumerable()
                .Select(a => $"{HttpUtility.UrlEncode(a.Key)}={HttpUtility.UrlEncode(a.Value.ToString())}")
                .ToList();
            return string.Join("&", sb);
        }
    }
}
