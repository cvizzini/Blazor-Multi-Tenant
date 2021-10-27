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

    public class TenantRepository : ITenantRepository
    {
        protected readonly DefaultContext context;
        string errorMessage = string.Empty;
        public TenantRepository(DefaultContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Tenant>> GetAll()
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<Tenant>().AsNoTracking();
            return await entities.ToListAsync();
        }
        public async Task<Tenant> GetById(int id)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<Tenant>().AsNoTracking(); ;
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Tenant> GetById(int? id)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<Tenant>().AsNoTracking(); ;
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Tenant> Insert(Tenant entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            using var dbContext = context.Create();
            var entities = dbContext.Set<Tenant>();
            await entities.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Tenant> Update(Tenant entity)
        {
            return await Save(entity);
        }
        public async Task Delete(int id)
        {
            if (id == 0) throw new ArgumentNullException("entity");

            using var dbContext = context.Create();
            var entities = dbContext.Set<Tenant>();
            var entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            entities.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Tenant> Save(Tenant entity)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<Tenant>();
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

        public async Task<IEnumerable<Tenant>> Filter(Expression<Func<Tenant, bool>> predicate)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<Tenant>().AsNoTracking(); ;
            var result = await entities.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<Tenant> FindByFilter(Expression<Func<Tenant, bool>> predicate)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<Tenant>().AsNoTracking(); ;
            var result = await entities.Where(predicate).FirstOrDefaultAsync();
            return result;
        }
    }

}
