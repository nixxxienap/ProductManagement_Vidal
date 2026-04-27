using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Repositories.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<Supplier?> GetByEmailAsync(string email);
        Task<IEnumerable<Supplier>> GetActiveSuppliersAsync();
        Task<IEnumerable<Supplier>> SearchAsync(string keyword);
        Task<bool> ExistsAsync(string email);
    }
}
