using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022.Modules.Main;

namespace clouddb_sdv_2022.Modules.OrderItems
{
    public interface IOrderItemService : IBaseService<OrderItem>
    {
        Task<OrderItem> AddOrderItemAsync(AddOrderItemDTO entity);
        Task<OrderItem> UpdateOrderItemAsync(UpdateOrderItemDTO entity, Guid id);
    }
}