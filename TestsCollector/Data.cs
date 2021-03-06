﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        static string ServiceUrl = "http://192.168.0.103:90/";

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

        internal async static void Post(string url, T obj)
        {
            var x = JsonConvert.SerializeObject(obj);
            Debug.WriteLine(x);
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            await client.PostAsync(new Uri(string.Format(ServiceUrl + "{0}", url)), content);
        }
    }
}