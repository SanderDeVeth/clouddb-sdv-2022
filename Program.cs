using clouddb_sdv_2022.Modules.Customers;
using clouddb_sdv_2022.Modules.OrderItems;
using clouddb_sdv_2022.Modules.Reviews;
using clouddb_sdv_2022.Modules.Products;
using clouddb_sdv_2022.Modules.Orders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Azure.Identity;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // add Modules\Customer\
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<PostCustomer>();

        // add Modules\Order\
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ProcessNewOrders>();
        services.AddScoped<AddOrder>();

        // add Modules\OrderItem
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderItemService, OrderItemService>();

        // add Modules\Product
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        // add Modules\Review
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IReviewService>();
        services.AddScoped<PostReview>();

    })
    .Build();

await host.RunAsync();