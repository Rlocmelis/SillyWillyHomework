using AutoMapper;
using FluentValidation;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Models.OrderRequests;
using SillyWillyHomework.Repositories.BaseRepository;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Services
{
    public class OrdersService : BaseService<Order, OrderDto>, IOrdersService
    {
        private readonly IProductsService _productsService;
        private readonly IValidator<OrderRequestDto> _validator;
        private readonly IValidator<OrderRequestItemDto> _itemValidator;

        public OrdersService(IBaseRepository<Order> repository, 
            IMapper mapper, 
            IProductsService productsService, 
            IValidator<OrderRequestDto> validator,
            IValidator<OrderRequestItemDto> itemValidator) : base(repository, mapper)
        {
            _productsService = productsService;
            _validator = validator;
            _itemValidator = itemValidator;
        }

        public async Task<OrderDto> PrepareOrder(OrderRequestDto receivedOrder)
        {
            await _validator.ValidateAndThrowAsync(receivedOrder);

            var amount = receivedOrder.Items.Sum(x => x.Quantity);

            // Calculate the price and discount for the order
            var basePrice = 98.99m;

            var price = basePrice * amount;
            var discount = 0.0m;
            if (amount >= 10 && amount < 50)
            {
                discount = 0.05m;
            }
            else if (amount >= 50)
            {
                discount = 0.15m;
            }
            var discountAmount = Math.Round(price * discount, 2);
            var totalPrice = Math.Round(price - discountAmount, 2);

            // Create the order entity
            var order = new OrderDto
            {
                CustomerId = receivedOrder.CustomerId,
                ExpectedDeliveryDate = receivedOrder.ExpectedDeliveryDate,
                TotalPrice = totalPrice
            };

            foreach(var item in receivedOrder.Items )
            {
                await _itemValidator.ValidateAndThrowAsync(item);

                var itemProduct = await _productsService.GetByIdAsync(item.ProductId);

                order.Items.Add(new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = itemProduct.Price
                });
            }

            return await base.AddAsync(order);
        }
    }
}
