using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Supplier>> GetActiveSuppliersAsync()
        {
            var all = await _repository.GetAllAsync();
            return all.Where(s => s.IsActive);
        }

        public async Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Supplier>> SearchSuppliersAsync(string keyword)
        {
            var all = await _repository.GetAllAsync();
            return all.Where(s => s.Name.Contains(keyword) ||
                                 (s.ContactPerson != null && s.ContactPerson.Contains(keyword)) ||
                                 (s.Email != null && s.Email.Contains(keyword)));
        }

        public async Task<(bool Success, string Message)> CreateSupplierAsync(Supplier supplier)
        {
            try
            {
                await _repository.AddAsync(supplier);
                return (true, "Supplier created successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error creating supplier: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> UpdateSupplierAsync(Supplier supplier)
        {
            try
            {
                await _repository.UpdateAsync(supplier);
                return (true, "Supplier updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating supplier: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteSupplierAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return (true, "Supplier deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting supplier: {ex.Message}");
            }
        }
    }
}
