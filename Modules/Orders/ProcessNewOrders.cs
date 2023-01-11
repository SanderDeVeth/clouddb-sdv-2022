using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace clouddb_sdv_2022.Modules.Orders
{
    public class ProcessNewOrders
    {
        private readonly ILogger _logger;
        private readonly IOrderService _orderService;
        private WidgetAndCoContext _context;

        public ProcessNewOrders(ILoggerFactory loggerFactory, IOrderService orderService, WidgetAndCoContext context)
        {
            _logger = loggerFactory.CreateLogger<ProcessNewOrders>();
            _orderService = orderService;
            _context = context;
        }

        [Function(nameof(ProcessNewOrders))]
        public void Run([QueueTrigger("new-orders-queue", Connection = "AzureWebJobsStorage")] Order myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            _orderService.ShipOrderAsync(myQueueItem.Id);
        }
    }
}