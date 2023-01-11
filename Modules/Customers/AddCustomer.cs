using System.Net;
using clouddb_sdv_2022.Modules.Customers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace clouddb_sdv_2022.Modules.Customers
{
    public class AddCustomer
    {
        private readonly ICustomerService _customerService;

        public AddCustomer(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Function(nameof(AddCustomer))]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            AddCustomerDTO addCustomer = await req.ReadFromJsonAsync<AddCustomerDTO>();
            dynamic response;
            if (addCustomer == null)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteAsJsonAsync(new { message = "Invalid request body" });
                return response;
            }

            await _customerService.AddCustomerAsync(addCustomer);
            response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(addCustomer);
            return response;
        }
    }
}
