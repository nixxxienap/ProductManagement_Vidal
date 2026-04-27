using Microsoft.EntityFrameworkCore;
using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Data;
using ProductManagement.Features.Repositories.Interfaces;

namespace ProductManagement.Features.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<Category?> GetByNameAsync(string name) =>
            await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task<IEnumerable<Category>> GetActiveCategoriesAsync() =>
            await _context.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<bool> ExistsAsync(string name) =>
            await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }
}
