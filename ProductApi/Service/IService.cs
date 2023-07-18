using ProductApi.Models;

namespace ProductApi.Service
{
    public interface IService<T>
    {
        public Task<IEnumerable<T>> Get();
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> Search(int? category, int? subcategory, string? name);
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task Delete(int id);
        public bool Validate(T entity);

    }
}
