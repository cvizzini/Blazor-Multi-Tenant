using ExampleApp.Shared.Models;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace ExampleApp.Tester
{
    public static class TokenHelper
    {
        private const string Username = "Admin";
        private const string Password = "P@ssword123";
        public const string BaseAddress = "/api/authenticate/login";

        internal static async Task<AuthUser> GetAccessTokenAsync(HttpClient client)
        {           
            var login = new LoginModel() { Username = Username, Password = Password };
            var tokenResponse = await client.PostAsJsonAsync(BaseAddress, login);

            return await tokenResponse.Content.ReadAsAsync<AuthUser>(new[] { new JsonMediaTypeFormatter() });
        }
    }
}
