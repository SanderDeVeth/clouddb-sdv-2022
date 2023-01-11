using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clouddb_sdv_2022.Modules.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBlobStorageService _blobStorageService;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddProductAsync(AddProductDTO data)
        {
            Product newProduct = new Product{
                Id = Guid.NewGuid(),
                Name = data.Name,
                Price = (decimal)data.Price,
                Description = data.Description,
                ImageUrl = data.ImageUrl
            };

            _productRepository.Add(newProduct);
            await _productRepository.CommitAsync();
            return newProduct;
        }

        public async Task CommitAsync()
        {
            await _productRepository.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Product deleteProduct = new Product{
                Id = id
            };
            _productRepository.Delete(deleteProduct);
            await _productRepository.CommitAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _productRepository.GetSingleAsync(id);
        }

        public async Task<Product> UpdateProductAsync(UpdateProductDTO entity, Guid id)
        {
            Product replaceProduct = await _productRepository.GetSingleAsync(id);
            replaceProduct.Name = entity.Name;
            replaceProduct.Price = (decimal)entity.Price;
            replaceProduct.Description = entity.Description;
            replaceProduct.ImageUrl = entity.ImageUrl;

            _productRepository.Update(replaceProduct);
            await _productRepository.CommitAsync();
            return replaceProduct;
        }
    }
}