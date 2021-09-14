using System.Net.Http.Json;
using System.Threading.Tasks;
using ExampleApp.Shared;

namespace ExampleApp.Client.Data
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IHttpService _httpClient;
        private readonly ITokenHelper tokenHelper;

        public WeatherForecastService(IHttpService httpClient)
        {
            _httpClient = httpClient;            
        }
        
        public async Task<WeatherForecast[]> GetForecastAsync()
        {
              return await _httpClient.Get<WeatherForecast[]>("WeatherForecast");
        }
    }
}