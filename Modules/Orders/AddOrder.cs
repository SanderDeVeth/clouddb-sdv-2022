using System.Net;
using clouddb_sdv_2022.Modules.Orders;
using clouddb_sdv_2022_fa.Modules.Orders;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class OrderController : Controller
    {
        private IValidator<Order> _orderValidator;
        private IOrderRepository _repository;

        public OrderController(IValidator<Order> orderValidator, IOrderRepository repository)
        {
            _orderValidator = orderValidator;
            _repository = repository;
        }

        [Function("AddOrder")]
        public static async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var order = await req.ReadFromJsonAsync<Order>();
            return null;


            // await _db.CreateAsync(order);
            // return await req.CreatedAtResponse(nameof(OrdersFindById), new { id = order.Id }, order);
        }

    }
}
