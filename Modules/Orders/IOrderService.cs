using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  clouddb_sdv_2022.Modules.Orders;

namespace clouddb_sdv_2022.Modules.Orders
{
    public interface IOrderService
    {
        Task<Order> PostOrderAsync(PostOrderDTO data);
        Task<Order> ShipOrderAsync(Guid Id);
        Task<Order> GetOrderAsync(Guid Id);
    }
}