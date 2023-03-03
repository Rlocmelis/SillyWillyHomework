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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderDto model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            await _ordersService.AddAsync(model);
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] OrderDto model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            await _ordersService.UpdateAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _ordersService.DeleteAsync(id);
            return NoContent();
        }
    }
}
