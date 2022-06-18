using AutoMapper;
using Core.Entities;
using LExpress.Api.DTOs.Product;

namespace LExpress.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductGetDto>()
                .ForMember(p => p.ProductBrand, o => o.MapFrom(x => x.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(x => x.ProductType.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();
        }
    }
}
