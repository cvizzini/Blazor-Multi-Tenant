using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleApp.Shared.Models;

namespace ExampleApp.Client.Data
{
    public abstract class BaseConnectorService<T> where T : BaseModel
    { 
        private readonly IHttpService _httpClient;   

        public abstract string BASE_URL { get; }

        public BaseConnectorService(IHttpService httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<T>> GetAll()
        {
            return await _httpClient.Get<List<T>>(BASE_URL);
        }
        public async Task<T> Get(int id)
        {
            return await _httpClient.Get<T>($"{BASE_URL}/{id}");
        }

        public async Task<T> Create(T entity)
        {
            return await _httpClient.Post<T>(BASE_URL, entity);
        }

        public async Task<T> Update(T entity)
        {
            return await _httpClient.Put<T>(BASE_URL, entity);
        }

        public async Task Delete(int id)
        {
            await _httpClient.Delete($"{BASE_URL}/{id}");
        }
    }
}