using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace clouddb_sdv_2022.Modules.Orders
{
    public class ProcessNewOrders
    {
        private readonly ILogger _logger;

        public ProcessNewOrders(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ProcessNewOrders>();
        }

        [Function("ProcessNewOrders")]
        public void Run([QueueTrigger("new-orders-queue", Connection = "AzureWebJobsStorage")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
