using AutoMapper;
using FluentValidation;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Repositories.BaseRepository;
using SillyWillyHomework.Services.BaseService;
using SillyWillyHomework.Validation.Validators;

namespace SillyWillyHomework.Services
{
    public class OrdersService : BaseService<Order, OrderDto>, IOrdersService
    {
        private readonly IValidator<OrderDto> _validator;

        public OrdersService(IBaseRepository<Order> repository, IMapper mapper, IValidator<OrderDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override async Task<OrderDto> AddAsync(OrderDto model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var validationResult = _validator.Validate(model);


            return await base.AddAsync(model);
        }
    }
}
