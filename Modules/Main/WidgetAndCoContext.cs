using clouddb_sdv_2022.Modules.Customers;
using clouddb_sdv_2022.Modules.Orders;
using clouddb_sdv_2022.Modules.OrderItems;
using clouddb_sdv_2022.Modules.Reviews;
using clouddb_sdv_2022.Modules.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Azure.Identity;

namespace clouddb_sdv_2022
{
    public class WidgetAndCoContext : DbContext
    {
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Review>? Reviews { get; set; }
        public DbSet<Product>? Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToContainer("Customers");
            modelBuilder.Entity<Order>().ToContainer("Orders");
            modelBuilder.Entity<Product>().ToContainer("Products");
            modelBuilder.Entity<OrderItem>().ToContainer("OrderItems");
            modelBuilder.Entity<Review>().ToContainer("Reviews");

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(i => i.Order);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Product);

            modelBuilder.Entity<OrderItem>()
                .HasOne(i => i.Product);
            modelBuilder.Entity<OrderItem>()
                .HasOne(i => i.Order);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseCosmos(
               Environment.GetEnvironmentVariable("ConnectionString:AccountEndpoint"),
               Environment.GetEnvironmentVariable("ConnectionString:AccountKey"),
               Environment.GetEnvironmentVariable("clouddb-sdv-2022-cosmos")
            );

            builder.UseLazyLoadingProxies();

            base.OnConfiguring(builder);
        }
    }
}