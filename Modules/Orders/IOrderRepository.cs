using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  clouddb_sdv_2022.Modules.Orders;

namespace clouddb_sdv_2022.Modules.Orders
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
    }
}