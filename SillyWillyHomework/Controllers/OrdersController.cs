using Microsoft.AspNetCore.Mvc;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Services;

namespace SillyWillyHomework.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var order = await _ordersService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("customer/{id}")]
        public async Task<ActionResult<List<OrderDto>>> GetCustomerOrders(int id)
        {
            var orders = await _ordersService.GetCustomerOrders(id);
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> PlaceOrder([FromBody] OrderRequest orderDto)
        {
            var order = await _ordersService.PrepareOrder(orderDto);

            // Add the order to the database
            var orderResult = await _ordersService.AddAsync(order);

            return Ok(orderResult);
        }
    }
}
