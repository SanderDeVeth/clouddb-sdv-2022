using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022_fa.Modules.Customers;

namespace clouddb_sdv_2022.Modules.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> PostCustomerAsync(PostCustomerDTO postCustomerDTO)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = postCustomerDTO.Name,
                EmailAddress = postCustomerDTO.EmailAddress,
                DateOfBirth = postCustomerDTO.DateOfBirth,
            };

            _customerRepository.Add(customer);
            await _customerRepository.CommitAsync();

            return customer;
        }
    }
}