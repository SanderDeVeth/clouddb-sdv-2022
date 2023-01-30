using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace clouddb_sdv_2022.Modules.Reviews
{
    public class AddReview
    {
        private readonly IReviewService _reviewService;

        public AddReview(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Function(nameof(AddReview))]
        [OpenApiOperation(operationId: "addReview", tags: new[] { "Reviews" }, Summary = "Add a new review", Description = "This can only be done by the logged in user.")]
        [OpenApiRequestBody("application/json", typeof(AddReviewDTO), Description = "Review object that needs to be added to the store")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(AddReviewDTO), Description = "Review added")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(AddReviewDTO), Description = "Invalid input")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var addReview = await req.ReadFromJsonAsync<AddReviewDTO>();
            dynamic response;
            if(addReview == null)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteAsJsonAsync(new { message = "Invalid request body" });
                return response;
            }

            // Happy path
            await _reviewService.AddReviewAsync(addReview);
            response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(addReview);
            return response;
        }
    }
}
