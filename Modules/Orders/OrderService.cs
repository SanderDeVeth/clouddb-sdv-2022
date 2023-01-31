using clouddb_sdv_2022.Modules.Customers;
using clouddb_sdv_2022.Modules.OrderItems;

namespace clouddb_sdv_2022.Modules.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task CommitAsync()
        {
            await _orderRepository.CommitAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            Order deleteOrder = new()
            {
                Id = id
            };
            _orderRepository.Delete(deleteOrder);
            return _orderRepository.CommitAsync();
        }

        public async Task<Order> GetAsync(Guid id)
        {
            return await _orderRepository.GetSingleAsync(id);
        }

        public async Task<Order> AddOrderAsync(AddOrderDTO data)
        {
            Order newOrder = new()
            {
                Id = Guid.NewGuid(),
                Customer = await _customerRepository.GetSingleAsync(data.CustomerId),
                CustomerId = data.CustomerId,
                OrderItems = new List<OrderItem>(),
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                ShippingDate = null
            };
            _orderRepository.Add(newOrder);
            await _orderRepository.CommitAsync();
            return newOrder;
        }

        public async Task<Order> ShipOrderAsync(Guid Id)
        {
            Order replaceOrder = await _orderRepository.GetSingleAsync(Id);
            replaceOrder.ShippingDate = DateOnly.FromDateTime(DateTime.Now);
            _orderRepository.Update(replaceOrder);
            return replaceOrder;
        }
    }
}