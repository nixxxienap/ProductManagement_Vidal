using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Implementations;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await _productRepository.GetAllAsync();

        public async Task<IEnumerable<Product>> GetActiveProductsAsync() =>
            await _productRepository.GetActiveProductsAsync();

        public async Task<Product?> GetProductByIdAsync(int id) =>
            await _productRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10) =>
            await _productRepository.GetLowStockAsync(threshold);

        public async Task<IEnumerable<Product>> SearchProductsAsync(string keyword) =>
            await _productRepository.SearchAsync(keyword);

        public async Task<(bool Success, string Message)> CreateProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            return (true, "Product created successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateProductAsync(Product product)
        {
            var existing = await _productRepository.GetByIdAsync(product.Id);
            if (existing is null)
                return (false, "Product not found.");

            await _productRepository.UpdateAsync(product);
            return (true, "Product updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteProductAsync(int id)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing is null)
                return (false, "Product not found.");

            await _productRepository.DeleteAsync(id);
            return (true, "Product deleted successfully.");
        }
    }
}
