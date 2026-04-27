using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<IEnumerable<Product>> GetByBrandAsync(int brandId);
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetLowStockAsync(int threshold);
        Task<IEnumerable<Product>> SearchAsync(string keyword);
    }
}
