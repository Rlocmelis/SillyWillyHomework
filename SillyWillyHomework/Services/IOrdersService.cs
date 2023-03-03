using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Services
{
    public interface IOrdersService : IBaseService<Order, OrderDto>
    {
    }
}
