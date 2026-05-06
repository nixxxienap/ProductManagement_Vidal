using Microsoft.EntityFrameworkCore;
using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Data;
using ProductManagement.Features.Repositories.Interfaces;

namespace ProductManagement.Features.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .OrderBy(p => p.Name)
                .ToListAsync();

        public async Task<IEnumerable<Product>> GetActiveProductsAsync() =>
            await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();

        public async Task<IEnumerable<Product>> GetByBrandAsync(int brandId) =>
            await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.BrandId == brandId)
                .ToListAsync();

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId) =>
            await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

        public async Task<IEnumerable<Product>> GetLowStockAsync(int threshold) =>
            await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.Stock <= threshold && p.IsActive)
                .OrderBy(p => p.Stock)
                .ToListAsync();

        public async Task<IEnumerable<Product>> SearchAsync(string keyword) =>
            await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(keyword) ||
                            (p.Description != null && p.Description.Contains(keyword)))
                .ToListAsync();
    }
}
