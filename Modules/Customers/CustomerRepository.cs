using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022_fa.Modules.Customers;

namespace clouddb_sdv_2022.Modules.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        

        public CustomerRepository()
        {
        }

        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetSingleAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}