using ExampleApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleApp.Shared
{
    public interface IUserTenantAccessService
    {
        Task Create(UserTenantAccess userTenantAccess);
        Task Delete(int id);
        Task<UserTenantAccess> Get(int id);
        Task<List<UserTenantAccess>> GetAll();
        Task<List<UserAccessDTO>> GetUsersByTenantId(int tenantId);
        Task Update(UserTenantAccess userTenantAccess);
    }
}