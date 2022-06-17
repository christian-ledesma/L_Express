using AutoMapper;
using Core.Entities;
using LExpress.Api.DTOs.Product;

namespace LExpress.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductGetDto>().ReverseMap();
        }
    }
}
