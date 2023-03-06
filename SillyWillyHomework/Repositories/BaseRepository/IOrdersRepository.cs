using SillyWillyHomework.Entities;

namespace SillyWillyHomework.Repositories.BaseRepository
{
    public interface IOrdersRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetCustomerOrdersAsync(int customerId);
    }
}
