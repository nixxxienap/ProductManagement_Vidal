using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await productRepository.GetAllAsync();

        public async Task<IEnumerable<Product>> GetActiveProductsAsync() =>
            await productRepository.GetActiveProductsAsync();

        public async Task<Product?> GetProductByIdAsync(int id) =>
            await productRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10) =>
            await productRepository.GetLowStockAsync(threshold);

        public async Task<IEnumerable<Product>> SearchProductsAsync(string keyword) =>
            await productRepository.SearchAsync(keyword);

        public async Task<(bool Success, string Message)> CreateProductAsync(Product product)
        {
            await productRepository.AddAsync(product);
            return (true, "Product created successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateProductAsync(Product product)
        {
            var existing = await productRepository.GetByIdAsync(product.Id);
            if (existing is null)
                return (false, "Product not found.");

            await productRepository.UpdateAsync(product);
            return (true, "Product updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteProductAsync(int id)
        {
            var existing = await productRepository.GetByIdAsync(id);
            if (existing is null)
                return (false, "Product not found.");

            await productRepository.DeleteAsync(id);
            return (true, "Product deleted successfully.");
        }
    }
}
