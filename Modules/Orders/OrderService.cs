using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022.Modules.Customers;
using clouddb_sdv_2022_fa.Modules.Orders;
using clouddb_sdv_2022_fa.Modules.Orders.Models;

namespace clouddb_sdv_2022.Modules.Orders.Models
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

        public async Task<Order> PostOrderAsync(PostOrderDTO data)
        {
            var Customer = await _customerRepository.GetSingleAsync(data.CustomerId);
            if (Customer == null)
            {
                throw new Exception("Customer not found");
            }
            var Order = new Order
            {
                Id = Guid.NewGuid(),
                Customer = Customer,
                CustomerId = Customer.Id,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                Items = new List<OrderItem>(),
            };
            
            Customer.Orders.Add(Order);
            _orderRepository.Add(Order);
            await _customerRepository.CommitAsync();
            await _orderRepository.CommitAsync();

            return Order;
        }

        public async Task<Order> GetOrderAsync(Guid Id)
        {
            try
            {
                var order = await _orderRepository.GetSingleAsync(Id);
                if (order == null)
                {
                    throw new Exception("Order not found");
                }
                return order;
            }
            catch(Exception e)
            {
                throw new Exception("An error occurred: " + e.Message);
            }
        }

        public Task<Order> ShipOrderAsync(Guid Id)
        {
            var order = _orderRepository.GetSingleAsync(Id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Result.ShippingDate = DateOnly.FromDateTime(DateTime.Now);
            _orderRepository.Update(order.Result);
            _orderRepository.CommitAsync();
            return order;
        }
    }
}