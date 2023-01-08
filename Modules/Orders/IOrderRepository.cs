using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022_fa.Modules.Orders;
using clouddb_sdv_2022_fa.Modules.Orders.Models;

namespace clouddb_sdv_2022.Modules.Orders
{
    public interface IOrderRepository : IBaseRepository<Order>
    {

    }
}