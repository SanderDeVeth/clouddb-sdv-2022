using System.Net;
using clouddb_sdv_2022.Modules.Customers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace clouddb_sdv_2022.Modules.Customers
{
    public class PostCustomer
    {
        private readonly ICustomerService _customerService;

        public PostCustomer(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Function("PostCustomer")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var postCustomer = await req.ReadFromJsonAsync<PostCustomerDTO>();
            dynamic response;
            if (postCustomer == null)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteAsJsonAsync(new { message = "Invalid request body" });
                return response;
            }

            await _customerService.AddCustomerAsync(postCustomer);
            response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(postCustomer);
            return response;
        }
    }
}
