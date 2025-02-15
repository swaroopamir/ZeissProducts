using Microsoft.AspNetCore.Mvc;
using ZeissProducts.Business.Services.Products;
using ZeissProducts.Data.Models;

namespace ZeissProducts.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get all the products.
        /// </summary>
        /// <returns>list of products</returns>
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productService.GetProductsAsync();

            return result.Match<IActionResult>(Ok, error => NotFound(error.ErrorMessage));
        }

        /// <summary>
        /// Get product based on the product identifier.
        /// </summary>
        /// <param name="id">product identifier</param>
        /// <returns>product</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            return result.Match<IActionResult>(Ok,error => NotFound(error.ErrorMessage));
        }

        /// <summary>
        /// Add product to the product list.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] Product product)
        {
            var result = await _productService.AddProductAsync(product);
            
            return result.Match<IActionResult>(Ok, error => BadRequest(error.ErrorMessage));
        }

        /// <summary>
        /// Updates the product for the product ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProductAsync(int id, Product product)
        {
            var result = await _productService.UpdateProductAsync(id, product);

            return result.Match<IActionResult>(Ok, error => BadRequest(error.ErrorMessage));
        }

        /// <summary>
        /// Decrement the stock for the product.
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut("decrement-stock/{id:int}/{quantity:int}")]
        public async Task<IActionResult> RemoveProductStocksAsync(int id, int quantity)
        {
            var result = await _productService.RemoveProductStocksAsync(id, quantity);

            return result.Match<IActionResult>(Ok, error => BadRequest(error.ErrorMessage));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut("add-to-stock/{id:int}/{quantity:int}")]
        public async Task<IActionResult> AddProductStocksAsync(int id, int quantity)
        {
            var result = await _productService.AddProductStocksAsync(id, quantity);

            return result.Match<IActionResult>(Ok, error => BadRequest(error.ErrorMessage));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductAsync(int id) 
        {
            var result = await _productService.DeleteProductAsync(id);

            return result.Match<IActionResult>(Ok, error => BadRequest(error.ErrorMessage));
        }

    }
}
