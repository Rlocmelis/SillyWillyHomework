using AutoMapper;
using FluentValidation;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Repositories.BaseRepository;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Services
{
    public class CustomersService : BaseService<Customer, CustomerDto>, ICustomersService
    {
        private readonly IValidator<CustomerDto> _validator;

        public CustomersService(IBaseRepository<Customer> repository, IMapper mapper, IValidator<CustomerDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public async Task<CustomerDto> GetCustomerByIdWithOrdersAsync(int customerId)
        {
            var include = "Order";

            return await GetByIdAsync(customerId, include);
        }
    }
}
