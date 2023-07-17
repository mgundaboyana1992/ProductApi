using ProductApi.Models;

namespace ProductApi.Repository
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(int productId);
        public Task<IEnumerable<Product>> GetProductsBySearch(int? category, int? subcategory, string Name);
        public Task<Product> AddProduct(Product product);
        public Task<Product> UpdateProduct(Product product);
        public Task DeleteProduct(int productId);

    }
}
