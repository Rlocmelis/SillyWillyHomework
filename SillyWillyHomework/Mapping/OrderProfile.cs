using AutoMapper;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;

namespace SillyWillyHomework.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
                .ReverseMap();
        }
    }
}
