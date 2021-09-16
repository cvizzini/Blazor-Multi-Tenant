using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ExampleApp.Shared;
using ExampleApp.Shared.Models;

namespace ExampleApp.Client.Data
{
    public class UserTenantAccessService : IUserTenantAccessService
    {
        private readonly IHttpService _httpClient;
        private const string USER_TENANT_ACCESS_ROUTE = "/api/UserTenantAccess";

        public UserTenantAccessService(IHttpService httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(UserTenantAccess userTenantAccess)
        {
            await _httpClient.Post($"{USER_TENANT_ACCESS_ROUTE}", userTenantAccess);
        }

        public async Task Delete(int id)
        {
            await _httpClient.Delete($"{USER_TENANT_ACCESS_ROUTE}/{id}");
        }

        public async Task<List<UserTenantAccess>> GetAll()
        {
            return await _httpClient.Get<List<UserTenantAccess>>($"{USER_TENANT_ACCESS_ROUTE}");
        }

        public async Task<UserTenantAccess> Get(int id)
        {
            var tenant = await _httpClient.Get<UserTenantAccess>($"{USER_TENANT_ACCESS_ROUTE}/{id}");
            return tenant;
        }

        public async Task<List<UserAccessDTO>> GetUsersByTenantId(int tenantId)
        {
            var tenant = await _httpClient.Get<List<UserAccessDTO>>($"{USER_TENANT_ACCESS_ROUTE}/tenant/{tenantId}");
            return tenant;
        }

        public async Task Update(UserTenantAccess userTenantAccess)
        {
            await _httpClient.Put($"{USER_TENANT_ACCESS_ROUTE}", userTenantAccess);
        }
    }
}