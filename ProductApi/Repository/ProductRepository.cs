using Microsoft.EntityFrameworkCore;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Service;

namespace ProductApi.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ILogger<ProductRepository> logger,AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task<Product> Add(Product product)
        {
            try
            {
                var result = await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (result != null)
                {
                    _dbContext.Products.Remove(result);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Product> Get(int id)
        {
            try
            {
                var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                return await _dbContext.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> Search(int? category, int? subcategory, string? name)
        {
            try
            {
                IQueryable<Product> query = _dbContext.Products;

                if (category != null)
                {
                    query = query.Where(x => x.Category == category);
                }

                if (subcategory != null)
                {
                    query = query.Where(x => x.SubCategory == subcategory);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Trim().Contains(name.ToLower().Trim()));
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Product> Update(Product product)
        {
            try
            {
                var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

                if (result != null)
                {
                    result.Code = product.Code;
                    result.Name = product.Name;
                    result.Description = product.Description;
                    result.Quantity = product.Quantity;
                    result.Price = product.Price;
                    result.Image = product.Image;
                    result.Category = product.Category;
                    result.SubCategory = product.SubCategory;

                    await _dbContext.SaveChangesAsync();

                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

    }
}
