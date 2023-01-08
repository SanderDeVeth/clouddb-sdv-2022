using System.Text.Json.Serialization;
using clouddb_sdv_2022;
using clouddb_sdv_2022_fa.Modules.Customers;
using clouddb_sdv_2022_fa.Modules.Orders;
using FluentValidation;

namespace clouddb_sdv_2022_fa.Modules.Reviews
{
    public class Review : IBaseEntity
    {
        public Guid Id { get; set; }
        public virtual Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public string ReviewText { get; set; }
        public DateOnly ReviewDate { get; set; }
        public int Rating { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator()
        {
            RuleFor(r => r.ReviewText).Length(0, 1000).NotEmpty();
            RuleFor(r => r.Rating).InclusiveBetween(1, 5);
            RuleFor(r => r.CustomerId).NotNull();
            RuleFor(r => r.ProductId).NotNull();
        }
    }
}