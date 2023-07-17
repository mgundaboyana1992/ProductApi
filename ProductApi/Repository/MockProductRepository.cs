using ProductApi.Models;

namespace ProductApi.Repository
{
    public class MockProductRepository : IRepository<Product>
    {
        IList<Product> _products;
        public MockProductRepository()
        {
            if (_products == null)
            {
                _products = new List<Product>();
                _products.Add(new Product() { Id = 1, Code = "1001", Name = "SamsungTV", Quantity = 10, Price = 10000, Description = "LED TV", Image = "", Category = 1, SubCategory = 1 });
                _products.Add(new Product() { Id = 2, Code = "1002", Name = "XiaomiMobile", Quantity = 5, Price = 5000, Description = "Mobile", Image = "", Category = 1, SubCategory = 2 });
                _products.Add(new Product() { Id = 3, Code = "1003", Name = "Walkmate", Quantity = 11, Price = 500, Description = "Slippers", Image = "", Category = 3, SubCategory = 6 });

            }
        }
        public async Task<Product> Add(Product product)
        {
            if (_products == null || _products.Count == 0)
            {
                product.Id = 1;
            }
            else
            {
                product.Id = _products.Max(x => x.Id) + 1;
            }
            _products.Add(product);

            return product;
        }

        public async Task Delete(int id)
        {
            var result = _products.FirstOrDefault(x => x.Id == id);

            if (result != null)
            {
                _products.Remove(result);
            }
        }

        public async Task<Product> Get(int id)
        {
            var result = _products.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return _products.ToList();
        }

        public async Task<IEnumerable<Product>> Search(int? category, int? subcategory, string name)
        {
            IEnumerable<Product> query = _products;

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

            return query.ToList();
        }

        public async Task<Product> Update(Product product)
        {
            var result = _products.FirstOrDefault(x => x.Id == product.Id);

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

                return result;
            }

            return null;
        }

    }
}
