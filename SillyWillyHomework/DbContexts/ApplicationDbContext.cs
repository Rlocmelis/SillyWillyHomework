using Microsoft.EntityFrameworkCore;
using SillyWillyHomework.Entities;

namespace SillyWillyHomework.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

    }
}
