using clouddb_sdv_2022.Modules.Customers;
using clouddb_sdv_2022.Modules.Products;

namespace  clouddb_sdv_2022.Modules.Reviews
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
    
    public class PostReviewDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public Guid ProductId { get; set; }
    }

    public class UpdateReviewDTO
    {
        public string? ReviewText { get; set; }
        public int? Rating { get; set; }
    }
}