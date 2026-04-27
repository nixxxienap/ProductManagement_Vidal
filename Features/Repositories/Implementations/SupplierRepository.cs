using Microsoft.EntityFrameworkCore;
using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Data;
using ProductManagement.Features.Repositories.Interfaces;

namespace ProductManagement.Features.Repositories.Implementations
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context) { }

        public async Task<Supplier?> GetByEmailAsync(string email) =>
            await _context.Suppliers
                .FirstOrDefaultAsync(s => s.Email!.ToLower() == email.ToLower());

        public async Task<IEnumerable<Supplier>> GetActiveSuppliersAsync() =>
            await _context.Suppliers
                .Where(s => s.IsActive)
                .OrderBy(s => s.Name)
                .ToListAsync();

        public async Task<IEnumerable<Supplier>> SearchAsync(string keyword) =>
            await _context.Suppliers
                .Where(s => s.Name.Contains(keyword) ||
                            s.Email!.Contains(keyword) ||
                            s.ContactPerson!.Contains(keyword) ||
                            s.Phone!.Contains(keyword))
                .ToListAsync();

        public async Task<bool> ExistsAsync(string email) =>
            await _context.Suppliers
                .AnyAsync(s => s.Email!.ToLower() == email.ToLower());
    
    }
}
