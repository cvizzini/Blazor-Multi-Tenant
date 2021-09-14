using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ExampleApp.Client.Data;
using ExampleApp.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExampleApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddScoped<IAlertService, AlertService>();
            // builder.RootComponents.Add<App>("#app");
           
         

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IAuthenticateUserService, AuthenticationUserService>();
            builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ITenant2Service, Tenant2Service>();
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            builder.Services.AddScoped<ITokenHelper, TokenHelper>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();                   

            await builder.Build().RunAsync();
        }
    }
}
