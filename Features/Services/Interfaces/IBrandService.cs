using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<IEnumerable<Brand>> GetActiveBrandsAsync();
        Task<Brand?> GetBrandByIdAsync(int id);
        Task<(bool Success, string Message)> CreateBrandAsync(Brand brand);
        Task<(bool Success, string Message)> UpdateBrandAsync(Brand brand);
        Task<(bool Success, string Message)> DeleteBrandAsync(int id);
    }
}
