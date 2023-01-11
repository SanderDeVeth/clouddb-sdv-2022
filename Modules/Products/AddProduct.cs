using System.Net;
using clouddb_sdv_2022.Modules.Products;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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
