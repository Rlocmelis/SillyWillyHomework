using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Repositories.BaseRepository;
using SillyWillyHomework.Services.BaseService;
using System.Linq.Expressions;

namespace SillyWillyHomework.Services
{
    public class CustomersService : BaseService<Customer, CustomerDto>, ICustomersService
    {
        private readonly IValidator<CustomerDto> _validator;

        public CustomersService(IBaseRepository<Customer> repository, IMapper mapper, IValidator<CustomerDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override async Task<CustomerDto> AddAsync(CustomerDto model)
        {
            await _validator.ValidateAsync(model);

            return await base.AddAsync(model);
        }
    }
}
