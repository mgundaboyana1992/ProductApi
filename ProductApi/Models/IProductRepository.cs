namespace ProductApi.Models
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(int productId);
        public Task<Product> AddProduct(Product product);
        public Task<Product> UpdateProduct(Product product);
        public Task DeleteProduct(int productId);

    }
}
