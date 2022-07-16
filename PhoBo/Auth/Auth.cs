using Newtonsoft.Json;
using PhoBo.Models;

namespace PhoBo.Auth
{
    public class Auth
    {
        public User GetUser(string userJsonString)
        {
            return JsonConvert.DeserializeObject<User>(userJsonString);
        }
    }
}
