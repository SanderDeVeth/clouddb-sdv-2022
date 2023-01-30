using System.Net;
using clouddb_sdv_2022.Modules.Products;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace clouddb_sdv_2022
{
    public class AddProduct
    {
        private readonly ILogger _logger;
        private readonly IProductService _productService;

        public AddProduct(ILoggerFactory loggerFactory, IProductService productService)
        {
            _logger = loggerFactory.CreateLogger<AddProduct>();
            _productService = productService;
        }

        [Function(nameof(AddProduct))]
        [OpenApiOperation(operationId: "addProduct", tags: new[] { "Products" }, Summary = "Add a new product", Description = "This can only be done by the logged in user.")]
        [OpenApiRequestBody("application/json", typeof(AddProductDTO), Description = "Product object that needs to be added to the store")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(AddProductDTO), Description = "Product added")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(AddProductDTO), Description = "Invalid input")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var addProduct = await req.ReadFromJsonAsync<AddProductDTO>();
            dynamic response;
            if(addProduct == null)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteAsJsonAsync(new { message = "Invalid request body" });
                return response;
            }

            // Happy path
            await _productService.AddProductAsync(addProduct);
            response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(addProduct);
            return response;
        }
    }
}
