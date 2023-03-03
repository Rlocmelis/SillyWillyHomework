using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Services
{
    public interface ICustomersService : IBaseService<Customer, CustomerDto>
    {
        Task<CustomerDto> GetCustomerByIdWithOrdersAsync(int customerId);
    }
}
