using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Repository;
using ProductApi.Service;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ProductApi.Tests
{
    public class ProductsServiceTest
    {
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly Mock<IRepository<Product>> _mockProductRepository;
        private IList<Product> _products;
        public ProductsServiceTest()
        {
            _mockLogger = new Mock<ILogger<ProductService>>();
            _mockProductRepository = new Mock<IRepository<Product>>();
        }

        [Fact]
        public async Task Get_ReturnsProductsAsync_Success()
        {
            var fixture = CreateSut();
            _mockProductRepository.Setup(x => x.Get()).ReturnsAsync(_products);
            var result = await fixture.Get();
            Assert.NotNull(result);
        }


        [Fact]
        public async Task Get_ReturnsProduct_Success()
        {
            var fixture = CreateSut();
            _mockProductRepository.Setup(x => x.Get(1)).ReturnsAsync(_products[1]);
            var result = await fixture.Get(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get_ReturnsProduct_Failure()
        {
            var fixture = CreateSut();
            _mockProductRepository.Setup(x => x.Get(1)).ThrowsAsync(new Exception("Bad Request"));
            try
            {
                var result = await fixture.Get(1);
            }
            catch (Exception ex)
            {
                Assert.Equal("Bad Request", ex.Message);
            }

        }

        [Fact]
        public async Task Create_ReturnsProduct_Success()
        {
            var fixture = CreateSut();
            Product? newProduct = new Product() { Id = 4, Code = "1004", Name = "Test", Quantity = 11, Price = 500, Description = "Slippers", Image = "", Category = 3, SubCategory = 6 };
            _mockProductRepository.Setup(x => x.Add(newProduct)).ReturnsAsync(newProduct);
            var result = await fixture.Add(newProduct);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_ReturnsProduct_Failure()
        {
            var fixture = CreateSut();
            Product? newProduct = new Product() { Id = 4, Code = "1004", Name = "Test", Quantity = 11, Price = 500, Description = "Slippers", Image = "", Category = 3, SubCategory = 6 };
            _mockProductRepository.Setup(x => x.Add(newProduct)).ThrowsAsync(new Exception("Bad Request"));

            try
            {
                var result = await fixture.Add(newProduct);
            }
            catch (Exception ex)
            {
                Assert.Equal("Bad Request", ex.Message);
            }
        }

        [Fact]
        public async Task Update_ReturnsProduct()
        {
            var fixture = CreateSut();
            Product? newProduct = new Product() { Id = 3, Code = "1003", Name = "Test1", Quantity = 11, Price = 500, Description = "Slippers", Image = "", Category = 3, SubCategory = 6 };
            _mockProductRepository.Setup(x => x.Update(newProduct)).ReturnsAsync(newProduct);
            var result = await fixture.Update(newProduct);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var fixture = CreateSut();
            _mockProductRepository.Setup(x => x.Delete(3));
            await fixture.Delete(3);
        }

        private ProductService CreateSut()
        {
            GetPoducts();
            return new ProductService(_mockLogger.Object, _mockProductRepository.Object);
        }

        private IEnumerable<Product> GetPoducts()
        {
            if (_products == null)
            {
                _products = new List<Product>();
                _products.Add(new Product() { Id = 1, Code = "1001", Name = "SamsungTV", Quantity = 10, Price = 10000, Description = "LED TV", Image = "", Category = 1, SubCategory = 1 });
                _products.Add(new Product() { Id = 2, Code = "1002", Name = "XiaomiMobile", Quantity = 5, Price = 5000, Description = "Mobile", Image = "", Category = 1, SubCategory = 2 });
                _products.Add(new Product() { Id = 3, Code = "1003", Name = "Walkmate", Quantity = 11, Price = 500, Description = "Slippers", Image = "", Category = 3, SubCategory = 6 });
            }

            return _products;
        }
    }
}