using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace clouddb_sdv_2022.Modules.Orders.Models
{
    public class OrderItemDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderItemValidator : AbstractValidator<OrderItemDTO>
    {
        public OrderItemValidator()
        {
            RuleFor(i => i.OrderId).NotNull();
            RuleFor(i => i.ProductId).NotNull();
            RuleFor(i => i.Quantity).InclusiveBetween(1, 100);
        }
    }
}