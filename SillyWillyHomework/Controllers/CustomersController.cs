using Microsoft.AspNetCore.Mvc;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Services;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Controllers
{
    public class CustomersController : BaseApiController
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }


        [HttpGet("{id}/orders")]
        public async Task<ActionResult<List<OrderDto>>> Get(int id)
        {
            var orders = await _customersService.GetCustomerByIdWithOrdersAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }
    }
}
