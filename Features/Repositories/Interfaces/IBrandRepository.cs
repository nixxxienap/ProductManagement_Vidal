using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Implementations;

namespace ProductManagement.Features.Repositories.Interfaces
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<Brand?> GetByNameAsync(string name);
        Task<IEnumerable<Brand>> GetActiveBrandsAsync();
        Task<bool> ExistsAsync(string name);
    }
}
