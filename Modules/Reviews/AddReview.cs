using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

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
