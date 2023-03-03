using AutoMapper;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;

namespace SillyWillyHomework.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<Customer, CustomerDto>()
                .ReverseMap();
        }
    }
}
