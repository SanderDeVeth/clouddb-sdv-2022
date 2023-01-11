using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022.Modules.Main;

namespace clouddb_sdv_2022.Modules.Products
{
    public interface IProductService : IBaseService<Product>
    {
        Task<Product> AddProductAsync(AddProductDTO data);
        Task<Product> UpdateProductAsync(UpdateProductDTO data, Guid Id);
    }
}