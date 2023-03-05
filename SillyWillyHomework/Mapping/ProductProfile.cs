using AutoMapper;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;

namespace SillyWillyHomework.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                    .ReverseMap();
        }
    }
}
