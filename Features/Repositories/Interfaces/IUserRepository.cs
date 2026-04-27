using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        Task<Users?> GetByEmailAsync(string email);
        Task<IEnumerable<Users>> GetByRoleAsync(UserRole role);
        Task<IEnumerable<Users>> GetActiveUsersAsync();
        Task<Users?> GetByResetTokenAsync(string token);
        Task<bool> ExistsAsync(string email);
    }
}
