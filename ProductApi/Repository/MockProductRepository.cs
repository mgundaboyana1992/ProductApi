using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> AddProduct(Product product)
        {
            var result = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteProduct(int productId)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (result != null)
            {
                 _dbContext.Products.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProduct(int productId)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            return result;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySearch(int? category, int? subcategory, string name)
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
                query = query.Where(x => x.Name.ToLower().Trim().Contains(name.ToLower().Trim()));
            }

            return await query.ToListAsync();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result =await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

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

    }
}
