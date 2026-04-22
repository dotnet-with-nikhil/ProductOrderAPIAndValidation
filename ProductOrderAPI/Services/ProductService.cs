using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Models;
using dotnet_example_clean_arch_with_entity_framework.Repositories.Interfaces;
using dotnet_example_clean_arch_with_entity_framework.Services.Interfaces;

namespace dotnet_example_clean_arch_with_entity_framework.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository productRepository) {
            _repo = productRepository;
        }

        public async Task<IEnumerable<Products>> GetAll()
        {
            return await _repo.GetAll();
        }
        public async Task<Products> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<int> Add(Products product)
        {
            var productModel = new Products
            {
                Name = product.Name,
                Price = product.Price
            };
            return await _repo.Add(productModel);
        }

        public async Task<bool> Update(int id, Products product)
        {
            var productModel = await _repo.GetById(id);

            if (productModel == null)
                throw new Exception("Product not found");

            productModel.Name = product.Name;
            productModel.Price = product.Price;
            await _repo.Update(productModel);
            return true;
        }

        public async Task<bool> Patch(int id, UpdateProductDto dto)
        {
            var product = await _repo.GetById(id);

            if (product == null)
                return false;

            if (dto.Name != null)
                product.Name = dto.Name;

            if (dto.Price.HasValue)
                product.Price = dto.Price.Value;

            await _repo.Update(product);
            return true;
        }

        public async Task<bool> Detele(int id)
        {
            var product = await _repo.GetById(id);

            if (product == null)
                return false;

            await _repo.Delete(product);
            return true;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _repo.Exists(id);
        }
    }
}
