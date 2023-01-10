using System.ComponentModel.DataAnnotations;
using clouddb_sdv_2022.Modules.Orders;
using clouddb_sdv_2022.Modules.Reviews;

namespace  clouddb_sdv_2022.Modules.Customers
{
    public class Customer : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }

    public class PostCustomerDTO
    {
        public string Name { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }

    public class UpdateCustomerDTO
    {
        public string? Name { get; set; }
        [EmailAddress]
        public string? EmailAddress { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
}
