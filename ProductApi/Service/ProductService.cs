using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Repository;

namespace ProductApi.Service
{
    public class ProductService : IService<Product>
    {
        private readonly ILogger<Product> _logger;
        private readonly IRepository<Product> _productRepository;
        public ProductService(ILogger<Product> logger, IRepository<Product> productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        public async Task<Product> Add(Product entity)
        {
            try
            {
                Product? createdProduct =null ;
                if (Validate(entity))
                {
                   createdProduct = await _productRepository.Add(entity);
                }
                return createdProduct;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _productRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                return await _productRepository.Get();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Product> Get(int id)
        {
            try
            {
                var result = await _productRepository.Get(id);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> Search(int? category, int? subcategory, string name)
        {
            try
            {
                var result = await _productRepository.Search(category, subcategory, name);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Product> Update(Product entity)
        {
            try
            {
                Product result = null;
                if (Validate(entity))
                {
                    result = await _productRepository.Update(entity);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Validate(Product entity)
        {
            if (entity.Price < 0)
                return false;
            if (entity.Quantity < 0)
                return false;

            return true;

        }
    }
}
