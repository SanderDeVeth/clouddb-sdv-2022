using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022.Modules.Customers;
using clouddb_sdv_2022_fa.Modules.Reviews;
using Company.Function;

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

        public async Task<Review> PostReviewAsync(PostReviewDTO data)
        {
            var Customer = await _customerRepository.GetSingleAsync(data.CustomerId);
            if (Customer == null)
            {
                throw new Exception("Customer not found");
            }
            
            var Review = new Review
            {
                Id = Guid.NewGuid(),
                Customer = Customer,
                CustomerId = Customer.Id,
                ReviewDate = DateOnly.FromDateTime(DateTime.Now),
                Rating = data.Rating,
                ReviewText = data.ReviewText,
            };

            Customer.Reviews.Add(Review);
            _reviewRepository.Add(Review);
            await _customerRepository.CommitAsync();
            await _reviewRepository.CommitAsync();

            return Review;
        }
    }
}