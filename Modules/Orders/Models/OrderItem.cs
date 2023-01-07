using System.Text.Json.Serialization;
using FluentValidation;

namespace clouddb_sdv_2022_fa.Modules.Orders
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public virtual Product Product { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Order Order { get; set; }
        public Guid OrderId { get; set; }


        // public OrderItem(Product product, int quantity)
        // {
        //     Product = product;
        //     Quantity = quantity;
        //     UnitPrice = product.Price;
        // }

        public decimal GetTotal()
        {
            return Quantity * UnitPrice;
        }

        public class OrderItemValidator : AbstractValidator<OrderItem>
        {
            public OrderItemValidator()
            {
                RuleFor(i => i.Quantity).GreaterThan(0);
                RuleFor(i => i.UnitPrice).GreaterThan(0);
                RuleFor(i => i.ProductId).NotNull();
                RuleFor(i => i.OrderId).NotNull();
            }
        }
    }
}