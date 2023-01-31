using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  clouddb_sdv_2022.Modules.Customers;

namespace clouddb_sdv_2022.Modules.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> AddCustomerAsync(AddCustomerDTO addCustomerDTO)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = addCustomerDTO.Name,
                EmailAddress = addCustomerDTO.EmailAddress,
                DateOfBirth = addCustomerDTO.DateOfBirth,
            };

            _customerRepository.Add(customer);
            await _customerRepository.CommitAsync();
            return customer;
        }

        public async Task CommitAsync()
        {
            await _customerRepository.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Customer deleteCustomer = new()
            {
                Id = id
            };
            _customerRepository.Delete(deleteCustomer);
            await _customerRepository.CommitAsync();
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            return await _customerRepository.GetSingleAsync(id);
        }

        public async Task<Customer> UpdateCustomerAsync(UpdateCustomerDTO entity, Guid id)
        {
            Customer replaceCustomer = await _customerRepository.GetSingleAsync(id);
            if (entity.Name != null) replaceCustomer.Name = entity.Name;
            if (entity.EmailAddress != null) replaceCustomer.EmailAddress = entity.EmailAddress;
            if (entity.DateOfBirth != null) replaceCustomer.DateOfBirth = (DateOnly)entity.DateOfBirth;

            _customerRepository.Update(replaceCustomer);
            await _customerRepository.CommitAsync();
            return replaceCustomer;
        }
    }
}