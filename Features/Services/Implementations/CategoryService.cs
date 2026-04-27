using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync() =>
            await _categoryRepository.GetAllAsync();

        public async Task<IEnumerable<Category>> GetActiveCategoriesAsync() =>
            await _categoryRepository.GetActiveCategoriesAsync();

        public async Task<Category?> GetCategoryByIdAsync(int id) =>
            await _categoryRepository.GetByIdAsync(id);

        public async Task<(bool Success, string Message)> CreateCategoryAsync(Category category)
        {
            if (await _categoryRepository.ExistsAsync(category.Name))
                return (false, $"Category '{category.Name}' already exists.");

            await _categoryRepository.AddAsync(category);
            return (true, "Category created successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateCategoryAsync(Category category)
        {
            var existing = await _categoryRepository.GetByIdAsync(category.Id);
            if (existing is null)
                return (false, "Category not found.");

            await _categoryRepository.UpdateAsync(category);
            return (true, "Category updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteCategoryAsync(int id)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing is null)
                return (false, "Category not found.");

            await _categoryRepository.DeleteAsync(id);
            return (true, "Category deleted successfully.");
        }

    }
}
