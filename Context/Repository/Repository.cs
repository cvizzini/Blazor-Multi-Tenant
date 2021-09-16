using ExampleApp.Context.Context;
using ExampleApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExampleApp.Context.Repository
{

    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly EmployeeContext context;
        string errorMessage = string.Empty;
        public Repository(EmployeeContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<T>().AsNoTracking();
            return await entities.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<T>().AsNoTracking(); ;
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<T> GetById(int? id)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<T>().AsNoTracking(); ;
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<T> Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            using var dbContext = context.Create();
            var entities = dbContext.Set<T>();
            await entities.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<T>();
            if (entity == null) throw new ArgumentNullException("entity");
            await dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task Delete(int id)
        {
            if (id == 0) throw new ArgumentNullException("entity");

            using var dbContext = context.Create();
            var entities = dbContext.Set<T>();
            T entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            entities.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<T> Save(T entity)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<T>();
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

        public async Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<T>().AsNoTracking(); ;
            var result = await entities.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<T> FindByFilter(Expression<Func<T, bool>> predicate)
        {
            using var dbContext = context.Create();
            var entities = dbContext.Set<T>().AsNoTracking(); ;
            var result = await entities.Where(predicate).FirstOrDefaultAsync();
            return result;
        }
    }

}
