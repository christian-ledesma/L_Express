using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using LExpress.Api.DTOs.Product;
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
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepository,
                                  IGenericRepository<ProductBrand> brandsRepository,
                                  IGenericRepository<ProductType> typesRepository,
                                  IMapper mapper)
        {
            _productsRepository = productsRepository;
            _brandsRepository = brandsRepository;
            _typesRepository = typesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductGetDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepository.ListAsync(spec);
            return products.Select(response => new ProductGetDto
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                Price = response.Price,
                PictureUrl = response.PictureUrl,
                ProductBrand = response.ProductBrand.Name,
                ProductType = response.ProductType.Name
            }).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductGetDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var response = await _productsRepository.GetEntityWithSpecAsync(spec);
            var product = _mapper.Map<ProductGetDto>(response);
            return product;
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
