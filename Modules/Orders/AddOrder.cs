using System.Dynamic;
using System.Net;
using clouddb_sdv_2022.Modules.Orders;
using clouddb_sdv_2022.Modules.Orders.Models;
using clouddb_sdv_2022_fa.Modules.Orders;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class AddOrder
    {
        private readonly IOrderService _orderService;

        public AddOrder(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Function("AddOrder")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var postOrder = await req.ReadFromJsonAsync<PostOrderDTO>();
            dynamic response;
            if(postOrder == null)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteAsJsonAsync(new { message = "Invalid request body" });
                return response;
            }

            // if(!_postOrderValidator.Validate(postOrder).IsValid)
            // {
            //     response = req.CreateResponse(HttpStatusCode.BadRequest);
            //     await response.WriteAsJsonAsync(postOrder);
            //     return response;
            // }

            // Happy path
            await _orderService.PostOrderAsync(postOrder);
            response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(postOrder);
            return response;
        }
    }
}
