using System.Net;
using clouddb_sdv_2022.Modules.Customers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

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
        [OpenApiOperation(operationId: "addCustomer", tags: new[] { "Customers" }, Summary = "Add a new customer", Description = "This can only be done by the logged in user.")]
        [OpenApiRequestBody("application/json", typeof(AddCustomerDTO), Description = "Customer object that needs to be added to the store")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(AddCustomerDTO), Description = "Customer added")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(AddCustomerDTO), Description = "Invalid input")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal server error")]
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
