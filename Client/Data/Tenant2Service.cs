using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ExampleApp.Shared;
using ExampleApp.Shared.Models;

namespace ExampleApp.Client.Data
{
    public class Tenant2Service : ITenant2Service
    {
        private readonly HttpClient _httpClient;
        private const string Tenant_ROUTE = "/api/Tenant/";

        public Tenant2Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddTenant(Tenant Tenant)
        {
            await _httpClient.PostAsJsonAsync($"{Tenant_ROUTE}", Tenant);
        }

        public async Task DeleteTenant(int id)
        {
            await _httpClient.DeleteAsync($"{Tenant_ROUTE}/{id}");
        }

        private async Task<Tenant[]> GetAllTenants(string s)
        {
            return await _httpClient.GetFromJsonAsync<Tenant[]>($"{Tenant_ROUTE}");
        }

        public async Task<string> GetAllTenants()
        {
            return await _httpClient.GetFromJsonAsync<string>($"{Tenant_ROUTE}");
        }


        public async Task<Tenant> GetTenantData(int id)
        {
            var TenantResponse = await _httpClient.GetAsync($"{Tenant_ROUTE}/{id}");
            var Tenant = await TenantResponse.Content.ReadFromJsonAsync<Tenant>();
            return Tenant;
        }

        public async Task UpdateTenant(Tenant Tenant)
        {
            await _httpClient.PutAsJsonAsync($"{Tenant_ROUTE}", Tenant);
        }
    }
}