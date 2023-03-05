using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SillyWillyHomework.Entities;

namespace SillyWillyHomework.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }


        public static void SeedData(ApplicationDbContext context)
        {
            var myModels = new List<Product>
            {
                new Product(1, "DNA testing kit", 98.99m)
            };

            context.Products.AddRange(myModels);
            context.SaveChanges();
        }
    }
}
