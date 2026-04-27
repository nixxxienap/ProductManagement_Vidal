using Microsoft.EntityFrameworkCore;
using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Data;
using ProductManagement.Features.Repositories.Interfaces;

namespace ProductManagement.Features.Repositories.Implementations
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context) { }

        public async Task<Brand?> GetByNameAsync(string name) =>
        await _context.Brands
                .FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower());

        public async Task<IEnumerable<Brand>> GetActiveBrandsAsync() =>
            await _context.Brands
                .Where(b => b.IsActive)
                .OrderBy(b => b.Name)
                .ToListAsync();

        public async Task<bool> ExistsAsync(string name) =>
            await _context.Brands
                .AnyAsync(b => b.Name.ToLower() == name.ToLower());
    }
}
