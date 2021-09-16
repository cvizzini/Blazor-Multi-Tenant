using ExampleApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleApp.Shared
{
    public interface ITenantService
    {
        Task AddTenant(Tenant Tenant);
        Task DeleteTenant(int id);
        Task<List<Tenant>> GetAllTenants();
        Task<Tenant> GetTenantData(int id);
        Task UpdateTenant(Tenant Tenant);
    }
}