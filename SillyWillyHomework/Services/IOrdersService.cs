using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Services
{
    public interface IOrdersService : IBaseService<Order, OrderDto>
    {
        Task<OrderDto> PrepareOrder(OrderRequest model);
        Task<List<OrderDto>> GetCustomerOrders(int customerId);
    }
}
