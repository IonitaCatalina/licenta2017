using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestsCollector.Models;

namespace TestsCollector.Helpers
{
    public static class LoginHelper
    {
        public static string CheckCredentials(this User user)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");

            var x = Task.Run(async () =>
            {
                var result = await client.PostAsync(
                    new Uri(string.Format("http://192.168.0.105:90/" + "{0}", "api/users/GetUserByEmail")), content);
                return result;
            });

            if (x.Result.StatusCode == System.Net.HttpStatusCode.OK) return "Logged in";
            if (x.Result.StatusCode == System.Net.HttpStatusCode.BadRequest) return "BadRequest";
            return string.Empty;
        }

    }
}