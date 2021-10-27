using ExampleApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleApp.Shared
{
    public interface IBaseConnectorService<T> where T : BaseModel
    {
        string BASE_URL { get; }

        Task<T> Create(T entity);
        Task Delete(int id);
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task<T> Update(T entity);
    }
}