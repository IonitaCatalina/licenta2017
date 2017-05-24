using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestsCollector.Models;

namespace TestsCollector
{
    public static class Data<T>
    {
        static HttpClient client = new HttpClient();     
        static string ServiceUrl = "http://192.168.0.102:90/";

        public static IEnumerable<T> ProcessRequest(string url, string verb, T obj)
        {
            if (verb == "GET")
            {
                Task<IEnumerable<T>> x = Task.Run(async () =>
                {
                    IEnumerable<T> result = await Get(url);
                    return result;
                });

                return x.Result;
            }

            if (verb == "POST")
            {
                Task.Run(() =>
                {
                    Post(url, obj);
                });
            }

            return null;
        }

        internal static async Task<IEnumerable<T>> Get(string url)
        {           
            var response = await client.GetAsync(new Uri(string.Format(ServiceUrl +"{0}", url)));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<T>>(content);
                return items;
            }

            return null;
        }

        internal static void Post(string url, T obj)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            client.PostAsync(new Uri(string.Format(ServiceUrl + "{0}", url)), content);
        }
    }
}