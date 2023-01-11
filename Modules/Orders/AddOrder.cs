using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace clouddb_sdv_2022.Modules.Orders
{
    public class AddOrder
    {
        private readonly IOrderService _orderService;

        public AddOrder(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Function(nameof(AddOrder))]
        [QueueOutput("new-orders-queue", Connection = "AzureWebJobsStorage")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var addOrder = await req.ReadFromJsonAsync<AddOrderDTO>();
            dynamic response;
            if(addOrder == null)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteAsJsonAsync(new { message = "Invalid request body" });
                return response;
            }

            // Happy path
            await _orderService.AddOrderAsync(addOrder);
            response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(addOrder);
            return response;
        }
    }
}
