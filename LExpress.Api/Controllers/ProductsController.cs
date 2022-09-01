using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using LExpress.Api.DTOs.Product;
using LExpress.Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LExpress.Api.Controllers
{
    public class ProductsController : BaseController
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
        public async Task<ActionResult<IReadOnlyList<ProductGetDto>>> GetProducts(string sort)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(sort);
            var products = await _productsRepository.ListAsync(spec);
            var response = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductGetDto>>(products);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductGetDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepository.GetEntityWithSpecAsync(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            var response = _mapper.Map<Product, ProductGetDto>(product);
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
