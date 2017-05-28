using TestsCollector.Models;

namespace TestsCollector.Helpers
{
    public static class LoginHelper
    {
        public static bool CheckCredentials(this User user)
        {
            var httpResponse = Data<User>.ProcessRequest($"api/users/GetUserByEmail", "POST", user);
            if (httpResponse != null)
                return true;
            return false;
        }

    }
}