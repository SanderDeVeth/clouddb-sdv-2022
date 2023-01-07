using clouddb_sdv_2022_fa.Modules.Customers;
using Company.Function;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddLogging();

        // add endpoints
        services.AddScoped<ProcessNewOrders>();

        // add validators
        services.AddScoped<IValidator<Customer>, CustomerValidator>();
    })
    .Build();

await host.RunAsync();