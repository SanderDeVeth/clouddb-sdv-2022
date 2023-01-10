using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clouddb_sdv_2022.Modules.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task CommitAsync()
        {
            await _productRepository.CommitAsync();
            return;
        }

        public async Task DeleteAsync(Guid id)
        {
            Product deleteProduct = new Product{
                Id = id
            };
            _productRepository.Delete(deleteProduct);
            await _productRepository.CommitAsync();
            return;
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _productRepository.GetSingleAsync(id);
        }

        public async Task UpdateAsync(UpdateProductDTO entity, Guid id)
        {
            Product replaceProduct = await _productRepository.GetSingleAsync(id);
            replaceProduct.Name = entity.Name;
            replaceProduct.Price = (decimal)entity.Price;
            replaceProduct.Description = entity.Description;
            replaceProduct.ImageUrl = entity.ImageUrl;

            _productRepository.Update(replaceProduct);
            await _productRepository.CommitAsync();
            return;
        }
    }
}