using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022_fa.Modules.Customers;
using System.Text.Json.Serialization;
using FluentValidation;

namespace clouddb_sdv_2022_fa.Modules.Orders
{
    public class Order
    {
        public Guid Id { get; set; }
        public virtual Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly? ShippingDate { get; set; }
        public virtual List<OrderItem> Items { get; set; }

        public Order(Customer customer)
        {
            Customer = customer;
            Items = new List<OrderItem>();
        }

        public bool OrderProcessed()
        {
            if(ShippingDate == null)
            {
                return false;
            }
            return ShippingDate.Value.CompareTo(OrderDate) >= 0;
        }

        public decimal GetTotal()
        {
            return Items.Sum(i => i.GetTotal());
        }

        public class OrderValidator : AbstractValidator<Order>
        {
            public OrderValidator()
            {
                RuleFor(o => o.CustomerId).NotNull();
                RuleFor(o => o.OrderDate).NotNull();
                RuleFor(o => o.Items).NotEmpty();
            }
        }
    }
}
