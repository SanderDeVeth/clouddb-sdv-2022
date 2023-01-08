using clouddb_sdv_2022_fa.Modules.Customers;

namespace clouddb_sdv_2022.Modules.Customers
{
    public interface ICustomerService
    {
        Task<Customer> PostCustomerAsync(PostCustomerDTO postCustomerDTO);
    }
}