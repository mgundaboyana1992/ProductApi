using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IRepository<Product> _productRepository;

        public ProductsController(ILogger<ProductsController> logger, IRepository<Product> productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                return Ok(await _productRepository.Get());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving products list");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            
            try
            {
                var result = await _productRepository.Get(id);
                if(result == null)
                {
                    return NotFound($"ProductId {id} not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting products list");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();
                var createdProduct = await _productRepository.Add(product);

                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating products list");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id , Product product)
        {
            try
            {
                if(id!=product.Id)
                {
                    return BadRequest("Product Id Mismatch");
                }   

                var updatingProduct = await _productRepository.Get(id);

                if (updatingProduct == null)
                    return NotFound($"ProductId {id} not found");               

                return Ok(await _productRepository.Update(product));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating product");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {              
                var deletingProduct = await _productRepository.Get(id);
                if (deletingProduct == null)
                    return NotFound($"ProductId {id} not found");

                await _productRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating product");
            }
        }

        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsBySearch(int? category, int? subcategory, string? name)
        {
            try
            {
                var result = await _productRepository.Search(category, subcategory, name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error searching the products list");
            }
        }
    }
}