using ExampleApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExampleApp.Shared
{
    public interface IUserTenantRepository
    {
        Task Delete(int id);
        Task<IEnumerable<ApplicationUserDTO>> ExcludeTenant(int tenantId);
        Task<IEnumerable<UserTenantAccess>> Filter(Expression<Func<UserTenantAccess, bool>> predicate, string[] includes = null);
        Task<UserTenantAccess> FindByFilter(Expression<Func<UserTenantAccess, bool>> predicate);
        Task<IEnumerable<UserTenantAccess>> GetAll();
        Task<UserTenantAccess> GetById(int id);
        Task<UserTenantAccess> GetById(int? id);
        Task<UserTenantAccess> Insert(UserTenantAccess entity);
        Task<UserTenantAccess> Save(UserTenantAccess entity);
        Task<UserTenantAccess> Update(UserTenantAccess entity);
    }
}