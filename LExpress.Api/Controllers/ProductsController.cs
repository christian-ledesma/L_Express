using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LExpress.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IGenericRepository<ProductBrand> _brandsRepository;
        private readonly IGenericRepository<ProductType> _typesRepository;

        public ProductsController(IGenericRepository<Product> productsRepository,
                                  IGenericRepository<ProductBrand> brandsRepository,
                                  IGenericRepository<ProductType> typesRepository)
        {
            _productsRepository = productsRepository;
            _brandsRepository = brandsRepository;
            _typesRepository = typesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var response = await _productsRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var response = await _productsRepository.GetByIdAsync(id);
            return response;
        }

        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var response = await _brandsRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var response = await _typesRepository.GetAllAsync();
            return Ok(response);
        }
    }
}
