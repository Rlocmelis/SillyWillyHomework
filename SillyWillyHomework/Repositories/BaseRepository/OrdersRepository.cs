using Microsoft.EntityFrameworkCore;
using SillyWillyHomework.DbContexts;
using SillyWillyHomework.Entities;

namespace SillyWillyHomework.Repositories.BaseRepository
{
    public class OrdersRepository : BaseRepository<Order>, IOrdersRepository
    {
        private readonly DbContext _dbContext;
        public OrdersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(int customerId)
        {
            var result = await _dbContext.Set<Order>().Where(x => x.CustomerId == customerId)
                .Include(x => x.Items)
                .ToListAsync();

            return result;
        }
    }
}
