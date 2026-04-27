using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync() =>
            await _brandRepository.GetAllAsync();

        public async Task<IEnumerable<Brand>> GetActiveBrandsAsync() =>
            await _brandRepository.GetActiveBrandsAsync();

        public async Task<Brand?> GetBrandByIdAsync(int id) =>
            await _brandRepository.GetByIdAsync(id);

        public async Task<(bool Success, string Message)> CreateBrandAsync(Brand brand)
        {
            if (await _brandRepository.ExistsAsync(brand.Name))
                return (false, $"Brand '{brand.Name}' already exists.");

            await _brandRepository.AddAsync(brand);
            return (true, "Brand created successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateBrandAsync(Brand brand)
        {
            var existing = await _brandRepository.GetByIdAsync(brand.Id);
            if (existing is null)
                return (false, "Brand not found.");

            await _brandRepository.UpdateAsync(brand);
            return (true, "Brand updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteBrandAsync(int id)
        {
            var existing = await _brandRepository.GetByIdAsync(id);
            if (existing is null)
                return (false, "Brand not found.");

            await _brandRepository.DeleteAsync(id);
            return (true, "Brand deleted successfully.");
        }
    }
}
