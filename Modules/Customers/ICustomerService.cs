using  clouddb_sdv_2022.Modules.Customers;
using clouddb_sdv_2022.Modules.Main;

namespace clouddb_sdv_2022.Modules.Customers
{
    public interface ICustomerService : IBaseService<Customer>
    {
        Task<Customer> AddCustomerAsync(PostCustomerDTO postCustomerDTO);
    }
}