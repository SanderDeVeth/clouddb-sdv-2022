using clouddb_sdv_2022.Modules.Customers;
using clouddb_sdv_2022.Modules.OrderItems;

namespace  clouddb_sdv_2022.Modules.Orders
{
    public class Order : IBaseEntity
    {
        public Guid Id { get; set; }
        public virtual Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly? ShippingDate { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

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
            return OrderItems.Sum(i => i.GetTotal());
        }
    }

    public class AddOrderDTO
    {
        public Guid CustomerId { get; set; }
    }
}
