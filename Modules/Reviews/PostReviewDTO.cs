using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace clouddb_sdv_2022.Modules.Reviews
{
    public class PostReviewDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public Guid ProductId { get; set; }
    }

    public class PostReviewValidator : AbstractValidator<PostReviewDTO>
    {
        public PostReviewValidator()
        {
            RuleFor(r => r.ReviewText).Length(0, 1000).NotEmpty();
            RuleFor(r => r.Rating).InclusiveBetween(1, 5);
            RuleFor(r => r.CustomerId).NotNull();
            RuleFor(r => r.ProductId).NotNull();
        }
    }
}