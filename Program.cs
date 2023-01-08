using clouddb_sdv_2022_fa.Modules.Customers;
using Company.Function;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // add endpoints
        services.AddScoped<ProcessNewOrders>();
        services.AddScoped<PostReview>();
        services.AddScoped<AddOrder>();

        // add validators
        // services.AddScoped<IValidator<Customer>, CustomerValidator>();
        // services.AddScoped<IValidator<PostOrderDTO>, PostOrderValidator>();
    })
    .Build();

await host.RunAsync();