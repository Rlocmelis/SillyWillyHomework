using Microsoft.AspNetCore.Mvc;
using SillyWillyHomework.Models;
using SillyWillyHomework.Services;

namespace SillyWillyHomework.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService) 
        {
            _productsService= productsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _productsService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
