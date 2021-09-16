using ExampleApp.Shared;
using ExampleApp.Shared.Models;
using ExampleApp.TenantContext.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExampleApp.TenantContext.Repository
{

    public class UserTenantRepository : IUserTenantRepository
    {
        protected readonly DefaultContext context;
        string errorMessage = string.Empty;
        public UserTenantRepository(DefaultContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<UserTenantAccess>> GetAll()
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<UserTenantAccess>().AsNoTracking();
            return await entities.ToListAsync();
        }
        public async Task<UserTenantAccess> GetById(int id)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<UserTenantAccess>().AsNoTracking(); ;
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<UserTenantAccess> GetById(int? id)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<UserTenantAccess>().AsNoTracking(); ;
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<UserTenantAccess> Insert(UserTenantAccess entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            using var dbContext = context.Create();
            var entities = dbContext.Set<UserTenantAccess>();
            await entities.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserTenantAccess> Update(UserTenantAccess entity)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<UserTenantAccess>();
            if (entity == null) throw new ArgumentNullException("entity");
            await dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task Delete(int id)
        {
            if (id == 0) throw new ArgumentNullException("entity");

            using var dbContext = context.Create();
            var entities = dbContext.Set<UserTenantAccess>();
            var entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            entities.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<UserTenantAccess> Save(UserTenantAccess entity)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<UserTenantAccess>();
            var existing = await entities.FindAsync(entity.Id);
            if (existing == null)
            {
                entities.Add(entity);
            }
            else
            {
                dbContext.Entry(existing).CurrentValues.SetValues(entity);
            }

            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<UserTenantAccess>> Filter(Expression<Func<UserTenantAccess, bool>> predicate, string[] includes = null)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<UserTenantAccess>().AsNoTracking();
            foreach (var include in includes)
            {
                entities = entities.Include(include);
            }
            var result = await entities.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<UserTenantAccess> FindByFilter(Expression<Func<UserTenantAccess, bool>> predicate)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<UserTenantAccess>().AsNoTracking(); 
            var result = await entities.Where(predicate).FirstOrDefaultAsync();
            return result;
        }
    }

}
