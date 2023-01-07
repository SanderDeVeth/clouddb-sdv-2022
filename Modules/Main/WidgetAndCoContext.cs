using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022_fa.Modules.Customers;
using clouddb_sdv_2022_fa.Modules.Orders;
using Microsoft.EntityFrameworkCore;

namespace clouddb_sdv_2022
{
    public class WidgetAndCoContext : DbContext
    {
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Order>? Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToContainer("Customers");
            modelBuilder.Entity<Order>().ToContainer("Orders");
            modelBuilder.Entity<Product>().ToContainer("Products");
            modelBuilder.Entity<Review>().ToContainer("Reviews");

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string connstring = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            builder.UseCosmos(
               Environment.GetEnvironmentVariable("ConnectionString:AccountEndpoint"),
               Environment.GetEnvironmentVariable("ConnectionString:AccountKey"),
               Environment.GetEnvironmentVariable("clouddb-sdv-2022-cosmos")
            );

            // ConfigWrapper config = new(new ConfigurationBuilder()
            //     .AddAzureKeyVault(new Uri("https://key-verfsolutions002.vault.azure.net/"), new DefaultAzureCredential())
            //     .Build());

            // Environment.SetEnvironmentVariable("DBNAME", config.DBNAME);
            // Environment.SetEnvironmentVariable("DBHost", config.DBHost);
            // Environment.SetEnvironmentVariable("DBCONNECTION", config.DBCONNECTION);

            // builder.UseCosmos(
            //     Environment.GetEnvironmentVariable("DBHost"),
            //     Environment.GetEnvironmentVariable("DBCONNECTION"),
            //     databaseName: Environment.GetEnvironmentVariable("DBNAME"));

            builder.UseLazyLoadingProxies();
            // builder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));

            //builder.LogTo(m => Console.WriteLine(m), new[] { DbLoggerCategory.Name }, LogLevel.Trace);
            //builder.EnableSensitiveDataLogging();

            base.OnConfiguring(builder);
        }
    }
}