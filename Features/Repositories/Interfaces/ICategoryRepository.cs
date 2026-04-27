using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Implementations;

namespace ProductManagement.Features.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();
        Task<bool> ExistsAsync(string name);
    }
}
