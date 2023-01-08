using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022_fa.Modules.Customers;
using FluentValidation;

namespace clouddb_sdv_2022.Modules.Orders.Models
{
    public class PostOrderDTO
    {
        public Guid CustomerId { get; set; }
    }

    public class PostOrderValidator : AbstractValidator<PostOrderDTO>
    {
        public PostOrderValidator()
        {
            RuleFor(o => o.CustomerId).NotNull();
        }
    }
}