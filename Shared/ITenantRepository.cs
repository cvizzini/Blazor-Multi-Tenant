using ExampleApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExampleApp.Shared
{
    public interface ITenantRepository
    {
        Task Delete(int id);
        Task<IEnumerable<Tenant>> Filter(Expression<Func<Tenant, bool>> predicate);
        Task<Tenant> FindByFilter(Expression<Func<Tenant, bool>> predicate);
        Task<IEnumerable<Tenant>> GetAll();
        Task<Tenant> GetById(int id);
        Task<Tenant> GetById(int? id);
        Task<Tenant> Insert(Tenant entity);
        Task<Tenant> Save(Tenant entity);
        Task<Tenant> Update(Tenant entity);
    }
}