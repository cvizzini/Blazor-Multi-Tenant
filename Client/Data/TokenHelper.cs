using ExampleApp.Shared;
using ExampleApp.Shared.Models;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ExampleApp.Client.Data
{
    public class TokenHelper : ITokenHelper
    {
        public TokenHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private const string Username = "Cl0uds1";
        private const string Password = "D3monw0lf!";
        public const string BaseAddress = "/api/authenticate/login";
        private readonly HttpClient _httpClient;

        public async Task<AuthUser> GetAccessTokenAsync()
        {
            var login = new LoginModel() { Username = Username, Password = Password };
            var tokenResponse = await _httpClient.PostAsJsonAsync(BaseAddress, login);

            return await tokenResponse.Content.ReadAsAsync<AuthUser>(new[] { new JsonMediaTypeFormatter() });
        }
    }



}
