using AutoMapper;
using LExpress.Api.DTOs.Product;
using LExpress.Api.Errors;
using LExpress.Api.Helpers;
using LExpress.Core.Entities;
using LExpress.Core.Interfaces;
using LExpress.Core.Specifications;
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
        public async Task<ActionResult<Pagination<ProductGetDto>>> GetProducts([FromQuery] ProductSpecParams productSpecParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productSpecParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productSpecParams);
            var totalItems = await _productsRepository.CountAsync(countSpec);
            var productList = await _productsRepository.ListAsync(spec);
            var products = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductGetDto>>(productList);
            var response = new Pagination<ProductGetDto>(productSpecParams.PageIndex, productSpecParams.PageSize, totalItems, products);
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
