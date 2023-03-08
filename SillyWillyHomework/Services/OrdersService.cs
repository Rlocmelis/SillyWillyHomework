using AutoMapper;
using FluentValidation;
using SillyWillyHomework.Business.Discounts;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Exceptions;
using SillyWillyHomework.Models;
using SillyWillyHomework.Models.Requests;
using SillyWillyHomework.Repositories.BaseRepository;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Services
{
    public class OrdersService : BaseService<Order, OrderDto>, IOrdersService
    {
        private readonly IMapper _mapper;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IBaseRepository<Customer> _customerRepository;
        private readonly IProductsService _productsService;
        private readonly IProductDiscountService _productDiscountService;
        private readonly IValidator<OrderRequest> _validator;

        public OrdersService(IBaseRepository<Order> repository,
            IBaseRepository<Customer> customerRepository,
            IOrdersRepository ordersRepository,
            IMapper mapper, 
            IProductsService productsService,
            IProductDiscountService productDiscountService,
            IValidator<OrderRequest> validator) : base(repository, mapper)
        {
            _ordersRepository = ordersRepository;
            _customerRepository = customerRepository;
            _productsService = productsService;
            _productDiscountService = productDiscountService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<OrderDto> PrepareOrder(OrderRequest receivedOrder)
        {
            await _validator.ValidateAndThrowAsync(receivedOrder);

            var customer = await _customerRepository.GetByIdAsync(receivedOrder.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException("Customer does not exist");
            }

            var totalPrice = 0m;

            // Create the order entity
            var order = new OrderDto
            {
                CustomerId = receivedOrder.CustomerId,
                ExpectedDeliveryDate = receivedOrder.ExpectedDeliveryDate,
                Items = new List<OrderItemDto>(),
                TotalPrice = totalPrice
            };

            // Validate, add received items to the order and calculate the total price
            foreach(var item in receivedOrder.Items)
            {
                var itemProduct = await _productsService.GetByIdAsync(item.ProductId);

                if (itemProduct == null)
                {
                    throw new NotFoundException("Product does not exist.");
                }

                totalPrice = _productDiscountService.CalculateTotalPrice(itemProduct.Price, item.Quantity) + totalPrice;

                order.Items.Add(new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = itemProduct.Price,
                });
            }

            order.TotalPrice = totalPrice;

            return order;
        }

        public async Task<List<OrderDto>> GetCustomerOrders(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new NotFoundException("Customer does not exist");
            }

            var model = await _ordersRepository.GetCustomerOrdersAsync(customerId);
            var result = _mapper.Map<List<OrderDto>>(model);

            return result;
        }
    }
}
