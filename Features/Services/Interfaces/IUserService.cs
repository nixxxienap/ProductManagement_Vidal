using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<IEnumerable<Users>> GetActiveUsersAsync();
        Task<Users?> GetUserByIdAsync(int id);
        Task<(bool Success, string Message)> RegisterAsync(string fullName, string email, string password, UserRole role);
        Task<(bool Success, string Message, Users? User)> LoginAsync(string email, string password);
        Task<(bool Success, string Message)> UpdateUserAsync(Users user);
        Task<(bool Success, string Message)> DeleteUserAsync(int id);
        Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task<(bool Success, string Message)> GeneratePasswordResetTokenAsync(string email);
        Task<(bool Success, string Message)> ResetPasswordAsync(string token, string newPassword);
    }
}
