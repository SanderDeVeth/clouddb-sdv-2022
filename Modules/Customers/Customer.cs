using System.ComponentModel.DataAnnotations;
using clouddb_sdv_2022_fa.Modules.Orders;
using FluentValidation;

namespace clouddb_sdv_2022_fa.Modules.Customers
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name).Length(0,30).NotEmpty();
            RuleFor(c => c.EmailAddress).EmailAddress();
            RuleFor(c => c.DateOfBirth).InclusiveBetween(DateOnly.FromDateTime(DateTime.Now.AddYears(-100)), DateOnly.FromDateTime(DateTime.Now));
        }
    }
}
