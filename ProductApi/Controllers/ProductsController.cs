using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository;
using ProductApi.Service;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IService<Product> _productService;

        public ProductsController(ILogger<ProductsController> logger, IService<Product> productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _productService.Get());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving products list");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> Get(int id)
        {

            try
            {
                var result = await _productService.Get(id);
                if (result == null)
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
        public async Task<ActionResult<Product>> Create(Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var result = await _productService.Add(product);

                if (result == null)
                {
                    return BadRequest("Price/Quantity cannot be negative");
                }

                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating products list");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> Update(int id, Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest("Product Id Mismatch");
                }

                var updatingProduct = await _productService.Get(id);

                if (updatingProduct == null)
                    return NotFound($"ProductId {id} not found");

                var result = await _productService.Update(product);

                if (result == null)
                {
                    return BadRequest("Price/Quantity cannot be negative");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating product");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deletingProduct = await _productService.Get(id);
                if (deletingProduct == null)
                    return NotFound($"ProductId {id} not found");

                await _productService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating product");
            }
        }

        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Product>>> Search(int? category, int? subcategory, string? name)
        {
            try
            {
                var result = await _productService.Search(category, subcategory, name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error searching the products list");
            }
        }
    }
}