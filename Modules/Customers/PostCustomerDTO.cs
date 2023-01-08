using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace clouddb_sdv_2022.Modules.Customers
{
    public class PostCustomerDTO
    {
        public string Name { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }

    public class PostCustomerValidator : AbstractValidator<PostCustomerDTO>
    {
        public PostCustomerValidator()
        {
            RuleFor(c => c.Name).Length(0,30).NotEmpty();
            RuleFor(c => c.EmailAddress).EmailAddress();
            RuleFor(c => c.DateOfBirth).InclusiveBetween(DateOnly.FromDateTime(DateTime.Now.AddYears(-100)), DateOnly.FromDateTime(DateTime.Now));
        }
    }
}