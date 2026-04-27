using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<(bool Success, string Message)> CreateCategoryAsync(Category category);
        Task<(bool Success, string Message)> UpdateCategoryAsync(Category category);
        Task<(bool Success, string Message)> DeleteCategoryAsync(int id);
    }
}
