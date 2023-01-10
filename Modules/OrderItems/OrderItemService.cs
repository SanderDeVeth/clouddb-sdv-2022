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

        public async Task AddAsync(AddOrderItemDTO entity)
        {
            OrderItem newOrderItem = new OrderItem{
                Id = Guid.NewGuid(),
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity
            };
            _orderItemRepository.Add(newOrderItem);
            await _orderItemRepository.CommitAsync();
            return;
        }

        public async Task CommitAsync()
        {
            await _orderItemRepository.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            OrderItem deleteOrderItem = new OrderItem{
                Id = id
            };
            _orderItemRepository.Delete(deleteOrderItem);
            await _orderItemRepository.CommitAsync();
            return;
        }

        public async Task<OrderItem> GetAsync(Guid id)
        {
            return await _orderItemRepository.GetSingleAsync(id);
        }

        public async Task UpdateAsync(UpdateOrderItemDTO entity, Guid id)
        {
            OrderItem replaceOrderItem = await _orderItemRepository.GetSingleAsync(id);
            replaceOrderItem.OrderId = entity.OrderId;
            replaceOrderItem.ProductId = entity.ProductId;
            replaceOrderItem.Quantity = (int)entity.Quantity;
            _orderItemRepository.Update(replaceOrderItem);
            await _orderItemRepository.CommitAsync();
            return;
        }
    }
}