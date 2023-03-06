using AutoMapper;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Models.Requests;

namespace SillyWillyHomework.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<Customer, CustomerDto>()
                .ReverseMap();

            CreateMap<CustomerDto, CustomerRequest>()
                .ReverseMap();
        }
    }
}
