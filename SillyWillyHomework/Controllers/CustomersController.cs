using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Models.Requests;
using SillyWillyHomework.Services;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Controllers
{
    public class CustomersController : BaseApiController
    {
        private readonly ICustomersService _customersService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomersService customersService, IMapper mapper)
        {
            _customersService = customersService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get(int id)
        {
            var customerDto = await _customersService.GetByIdAsync(id);

            if (customerDto == null)
            {
                return NotFound();
            }
            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerRequest customerRequest)
        {
            var customerDto = _mapper.Map<CustomerDto>(customerRequest);

            var result = await _customersService.AddAsync(customerDto);

            return Ok(result);
        }
    }
}
