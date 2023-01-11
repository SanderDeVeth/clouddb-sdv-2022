using  clouddb_sdv_2022.Modules.Customers;
using clouddb_sdv_2022.Modules.Main;

namespace clouddb_sdv_2022.Modules.Customers
{
    public interface ICustomerService : IBaseService<Customer>
    {
        Task<Customer> AddCustomerAsync(AddCustomerDTO addCustomerDTO);
        Task<Customer> UpdateCustomerAsync(UpdateCustomerDTO updateCustomerDTO, Guid Id);
    }
}