namespace ProductApi.Models
{
    public class ProductRepository : IProductRepository
    {
        IList<Product> _products;
        public ProductRepository()
        {
            if (_products == null)
            {
                _products = new List<Product>();
                _products.Add(new Product() { Id = 1, Code = "1001", Name = "SamsungTV", Quantity = 10, Price = 10000, Description = "LED TV", Image = "" });
                _products.Add(new Product() { Id = 2, Code = "1002", Name = "XiaomiMobile", Quantity = 5, Price = 5000, Description = "Mobile", Image = "" });
                _products.Add(new Product() { Id = 3, Code = "1003", Name = "Walkmate", Quantity = 11, Price = 500, Description = "Slippers", Image = "" });

            }
        }
        public async Task<Product> AddProduct(Product product)
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

        public async Task DeleteProduct(int productId)
        {
            var result = _products.FirstOrDefault(x => x.Id == productId);

            if (result != null)
            {
                _products.Remove(result);
            }
        }

        public async Task<Product> GetProduct(int productId)
        {
            var result = _products.FirstOrDefault(x => x.Id == productId);
            return result;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return _products.ToList();
        }

        public async Task<Product> UpdateProduct(Product product)
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

                return result;
            }

            return null;
        }

    }
}
