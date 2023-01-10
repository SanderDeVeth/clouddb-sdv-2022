using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clouddb_sdv_2022.Modules.OrderItems
{
    public class OrderItemRepository : EntityBaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(WidgetAndCoContext context) : base(context)
        {
        }
    }
}