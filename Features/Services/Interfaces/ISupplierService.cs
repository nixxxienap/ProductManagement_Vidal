using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<IEnumerable<Supplier>> GetActiveSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task<IEnumerable<Supplier>> SearchSuppliersAsync(string keyword);
        Task<(bool Success, string Message)> CreateSupplierAsync(Supplier supplier);
        Task<(bool Success, string Message)> UpdateSupplierAsync(Supplier supplier);
        Task<(bool Success, string Message)> DeleteSupplierAsync(int id);
    }
}
