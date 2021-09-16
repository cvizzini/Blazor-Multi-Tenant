using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ExampleApp.Shared;
using ExampleApp.Shared.Models;

namespace ExampleApp.Client.Data
{
    public class TenantService : ITenantService
    {
        private readonly HttpClient _httpClient;
        private const string Tenant_ROUTE = "/api/Tenant";

        public TenantService(HttpClient httpClient)
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

        public async Task<List<Tenant>> GetAllTenants()
        {
            return await _httpClient.GetFromJsonAsync<List<Tenant>>($"{Tenant_ROUTE}");
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