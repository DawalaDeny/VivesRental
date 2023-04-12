using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using VivesRental.Services;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Requests;

namespace VivesRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductService productService;


        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }
        
        //Get all products
        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            var products = await productService.FindAsync();
            return Ok(products);
        }

        //Get a product with its ID
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var product = await productService.GetAsync(id);
            return Ok(product);
        }

        //Create a new product
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductRequest request)
        {
            var product = await productService.CreateAsync(request);
            return Ok(product);
        }

        //Edit a product
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductRequest request)
        {
            var product = await productService.EditAsync(id, request);
            return Ok(product);
        }
        //Removes a product
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await productService.RemoveAsync(id);
            return Ok();
        }
        //Create a new product
        [HttpPost("{productId:Guid}")]
        public async Task<IActionResult> GenerateArticlesAsync(Guid productId, int amount)
        {
            var product = await productService.GenerateArticlesAsync(productId, amount);
            return Ok(product);
        }
    }
}
