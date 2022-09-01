using AutoMapper;
using LExpress.Api.DTOs.Product;
using LExpress.Core.Entities;

namespace LExpress.Api.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductGetDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductGetDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}
