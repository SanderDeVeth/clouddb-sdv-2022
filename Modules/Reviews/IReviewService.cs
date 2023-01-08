using clouddb_sdv_2022.Modules.Reviews;
using clouddb_sdv_2022_fa.Modules.Reviews;

namespace Company.Function
{
    public interface IReviewService
    {
        Task<Review> PostReviewAsync(PostReviewDTO data);
    }
}