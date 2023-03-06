﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SillyWillyHomework.Entities;

namespace SillyWillyHomework.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }


        public static void SeedData(ApplicationDbContext context)
        {
            var myProducts = new List<Product>
            {
                new Product(1, "DNA testing kit", 98.99m)
            };

            var myCustomers = new List<Customer>
            {
                new Customer(1, "Eglītes")
            };

            context.Products.AddRange(myProducts);
            context.Customers.AddRange(myCustomers);
            context.SaveChanges();
        }
    }
}
