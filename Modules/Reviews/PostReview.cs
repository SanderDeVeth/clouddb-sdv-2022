using System.Net;
using clouddb_sdv_2022.Modules.Reviews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class PostReview
    {
        private readonly IReviewService _reviewService;

        public PostReview(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Function("AddReview")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var postReview = await req.ReadFromJsonAsync<PostReviewDTO>();
            dynamic response;
            if(postReview == null)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteAsJsonAsync(new { message = "Invalid request body" });
                return response;
            }

            // Happy path
            await _reviewService.PostReviewAsync(postReview);
            response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(postReview);
            return response;
        }
    }
}