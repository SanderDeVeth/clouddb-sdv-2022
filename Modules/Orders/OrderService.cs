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

        public Task<Order> GetOrderAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> PostOrderAsync(PostOrderDTO data)
        {
            throw new NotImplementedException();
        }

        public Task<Order> ShipOrderAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}