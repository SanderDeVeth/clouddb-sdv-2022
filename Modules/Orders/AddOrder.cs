using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

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
        [OpenApiOperation(operationId: "addOrder", tags: new[] { "Orders" }, Summary = "Add a new order", Description = "This can only be done by the logged in user.")]
        [OpenApiRequestBody("application/json", typeof(AddOrderDTO), Description = "Order object that needs to be added to the store")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(AddOrderDTO), Description = "Order added")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(AddOrderDTO), Description = "Invalid input")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal server error")]
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
