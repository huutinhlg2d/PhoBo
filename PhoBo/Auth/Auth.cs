using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PhoBo.Models;
using System.Diagnostics;

namespace PhoBo.Auth
{
    public static class Auth
    {
        public static User GetUser(HttpContext context)
        {
            string userJsonString = context.Session.GetString("user");
            if (string.IsNullOrEmpty(userJsonString)) { return null; }
            else { return JsonConvert.DeserializeObject<User>(userJsonString); }
        }

        public static bool IsLogged(HttpContext context)
        {
            return GetUser(context) != null;
        }

        public static void Logout(HttpContext context) {
            context.Session.Remove("user");
        }
    }
}
