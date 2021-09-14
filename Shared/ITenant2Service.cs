using ExampleApp.Shared.Models;
using System.Threading.Tasks;

namespace ExampleApp.Shared
{
    public interface ITenant2Service
    {
        Task AddTenant(Tenant Tenant);
        Task DeleteTenant(int id);
        Task<string> GetAllTenants();
        Task<Tenant> GetTenantData(int id);
        Task UpdateTenant(Tenant Tenant);
    }
}