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
    }
}
