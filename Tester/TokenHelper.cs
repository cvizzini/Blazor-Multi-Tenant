using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ExampleApp.Shared.Models;
using Newtonsoft.Json;

namespace ExampleApp.Tester
{
    public static class TokenHelper
    {
        private const string Username = "Cl0uds1";
        private const string Password = "D3monw0lf!";
        public const string BaseAddress = "/api/authenticate/login";

        internal static async Task<AuthUser> GetAccessTokenAsync(HttpClient client)
        {           
            var login = new LoginModel() { Username = Username, Password = Password };
            var tokenResponse = await client.PostAsJsonAsync(BaseAddress, login);

            return await tokenResponse.Content.ReadAsAsync<AuthUser>(new[] { new JsonMediaTypeFormatter() });
        }
    }
}
