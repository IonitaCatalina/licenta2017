using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestsCollector
{
    public static class Data<T>
    {
        public static IEnumerable<T> GetResult(string url)
        {
            Task<IEnumerable<T>> x = Task.Run(async () =>
            {
                IEnumerable<T> result = await Request(url);
                return result;
            });

            return x.Result;
        }

        internal static async Task<IEnumerable<T>> Request(string url)
        {
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync(new Uri(string.Format("http://192.168.0.103:90/"+"{0}", url)));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<T>>(content);
                return items;
            }

            return null;
        }
    }
}