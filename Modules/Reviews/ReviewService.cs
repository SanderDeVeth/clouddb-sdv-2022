using clouddb_sdv_2022.Modules.Customers;

namespace clouddb_sdv_2022.Modules.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReviewService(IReviewRepository reviewRepository, ICustomerRepository customerRepository)
        {
            _reviewRepository = reviewRepository;
            _customerRepository = customerRepository;
        }

        public async Task CommitAsync()
        {
            await _reviewRepository.CommitAsync();
            return;
        }

        public async Task DeleteAsync(Guid id)
        {
            Review deleteReview = new Review{
                Id = id
            };
            _reviewRepository.Delete(deleteReview);
            await _reviewRepository.CommitAsync();
            return;
        }

        public async Task<Review> GetAsync(Guid id)
        {
            return await _reviewRepository.GetSingleAsync(id);
        }

        public async Task<Review> UpdateAsync(UpdateReviewDTO entity, Guid id)
        {
            Review replaceReview = await _reviewRepository.GetSingleAsync(id);
            if (entity.Rating != null) replaceReview.Rating = (int)entity.Rating;
            if (entity.ReviewText != null) replaceReview.ReviewText = entity.ReviewText;

            _reviewRepository.Update(replaceReview);
            await _reviewRepository.CommitAsync();
            return replaceReview;
        }

        public async Task<Review> AddReviewAsync(AddReviewDTO data)
        {
            var customer = await _customerRepository.GetSingleAsync(data.CustomerId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            var Review = new Review
            {
                Id = Guid.NewGuid(),
                Customer = customer,
                CustomerId = customer.Id,
                ReviewDate = DateOnly.FromDateTime(DateTime.Now),
                Rating = data.Rating,
                ReviewText = data.ReviewText,
            };
            customer.Reviews.Add(Review);
            _reviewRepository.Add(Review);
            await _customerRepository.CommitAsync();
            return Review;
        }

        public async Task<Review> UpdateReviewAsync(UpdateReviewDTO data, Guid Id)
        {
            Review replaceReview = await _reviewRepository.GetSingleAsync(Id);
            if (data.Rating != null) replaceReview.Rating = (int)data.Rating;
            if (data.ReviewText != null) replaceReview.ReviewText = data.ReviewText;
            await _reviewRepository.CommitAsync();
            return replaceReview;
        }
    }
}
