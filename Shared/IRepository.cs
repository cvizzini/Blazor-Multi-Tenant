using ExampleApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExampleApp.Context.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> GetById(int? id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate);
        Task<T> Save(T item);
        Task<T> FindByFilter(Expression<Func<T, bool>> predicate);
    }
}
