using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clouddb_sdv_2022.Modules.Orders
{
    public class OrderRepository : EntityBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(WidgetAndCoContext context) : base(context)
        {
        }
    }
}