using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  clouddb_sdv_2022.Modules.Customers;

namespace clouddb_sdv_2022.Modules.Customers
{
    public class CustomerRepository : EntityBaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(WidgetAndCoContext context) : base(context)
        {

        }
    }
}