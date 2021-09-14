using ExampleApp.Shared;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ExampleApp.Tester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
            var token = await TokenHelper.GetAccessTokenAsync(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            var authorizedResponse = await client.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
            Console.WriteLine(authorizedResponse);
        }
    }
}
