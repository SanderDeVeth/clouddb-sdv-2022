using clouddb_sdv_2022.Modules.Orders;
using clouddb_sdv_2022.Modules.Products;

namespace clouddb_sdv_2022.Modules.OrderItems
{
    public class OrderItem : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

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
    }

    public class OrderItemDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateOrderItemDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }
    }

    public class AddOrderItemDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}