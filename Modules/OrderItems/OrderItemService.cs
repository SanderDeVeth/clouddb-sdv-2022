using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022.Modules.OrderItems;

namespace clouddb_sdv_2022.Modules.OrderItems
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderItem> AddOrderItemAsync(AddOrderItemDTO entity)
        {
            OrderItem newOrderItem = new()
            {
                Id = Guid.NewGuid(),
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity
            };
            _orderItemRepository.Add(newOrderItem);
            await _orderItemRepository.CommitAsync();
            return newOrderItem;
        }

        public async Task CommitAsync()
        {
            await _orderItemRepository.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            OrderItem deleteOrderItem = new()
            {
                Id = id
            };
            _orderItemRepository.Delete(deleteOrderItem);
            await _orderItemRepository.CommitAsync();
        }

        public async Task<OrderItem> GetAsync(Guid id)
        {
            return await _orderItemRepository.GetSingleAsync(id);
        }

        public async Task<OrderItem> UpdateOrderItemAsync(UpdateOrderItemDTO entity, Guid id)
        {
            OrderItem replaceOrderItem = await _orderItemRepository.GetSingleAsync(id);
            replaceOrderItem.OrderId = entity.OrderId;
            replaceOrderItem.ProductId = entity.ProductId;
            replaceOrderItem.Quantity = (int)entity.Quantity;
            _orderItemRepository.Update(replaceOrderItem);
            await _orderItemRepository.CommitAsync();
            return replaceOrderItem;
        }
    }
}