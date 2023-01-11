using clouddb_sdv_2022.Modules.Main;
using clouddb_sdv_2022.Modules.Reviews;

namespace clouddb_sdv_2022.Modules.Reviews
{
    public interface IReviewService : IBaseService<Review>
    {
        Task<Review> AddReviewAsync(AddReviewDTO data);
        Task<Review> UpdateReviewAsync(UpdateReviewDTO data, Guid Id);
    }
}