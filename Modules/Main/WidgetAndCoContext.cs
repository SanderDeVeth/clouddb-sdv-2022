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
            // string connstring = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            builder.UseCosmos(
               Environment.GetEnvironmentVariable("ConnectionString:AccountEndpoint"),
               Environment.GetEnvironmentVariable("ConnectionString:AccountKey"),
               Environment.GetEnvironmentVariable("clouddb-sdv-2022-cosmos")
            );

            // ConfigWrapper config = new(new ConfigurationBuilder()
            //     .AddAzureKeyVault(new Uri("https://clouddb-sdv-2022-kv.vault.azure.net/"), new DefaultAzureCredential())
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

        // public class ConfigWrapper
        // {
        //     private readonly IConfiguration _config;

        //     public ConfigWrapper(IConfiguration config)
        //     {
        //         _config = config;

        //     }

        //     public string DBNAME
        //     {
        //         get { return _config["DBNAME"]; }
        //     }

        //     public string DBHost
        //     {
        //         get { return _config["DBHost"]; }

        //     }

        //     public string DBCONNECTION
        //     {
        //         get { return _config["DBCONNECTION"]; }
        //     }
        // }
    }
}