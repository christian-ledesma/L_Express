using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LExpress.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var response = await _productRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var response = await _productRepository.GetByIdAsync(id);
            return response;
        }
    }
}
