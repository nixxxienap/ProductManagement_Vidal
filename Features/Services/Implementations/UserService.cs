using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;
using ProductManagement.Features.Helpers;

namespace ProductManagement.Features.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private const int MaxFailedAttempts = 5;
        private const int LockoutMinutes = 15;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync() =>
            await _userRepository.GetAllAsync();

        public async Task<IEnumerable<Users>> GetActiveUsersAsync() =>
            await _userRepository.GetActiveUsersAsync();

        public async Task<Users?> GetUserByIdAsync(int id) =>
            await _userRepository.GetByIdAsync(id);

        public async Task<(bool Success, string Message)> RegisterAsync(
            string fullName, string email, string password, UserRole role)
        {
            if (await _userRepository.ExistsAsync(email))
                return (false, "Email is already registered.");

            var salt = PasswordHelper.GenerateSalt();
            var user = new Users
            {
                FullName = fullName,
                Email = email,
                Salt = salt,
                PasswordHash = PasswordHelper.HashPassword(password, salt),
                Role = role,
                Status = UserStatus.Active
            };

            await _userRepository.AddAsync(user);
            return (true, "User registered successfully.");
        }

        public async Task<(bool Success, string Message, Users? User)> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user is null)
                return (false, "Invalid email or password.", null);

            if (!user.IsActive)
                return (false, "Account is inactive or suspended.", null);

            if (user.IsLocked)
                return (false, $"Account is locked. Try again after {user.LockedUntil:HH:mm}.", null);

            if (!PasswordHelper.VerifyPassword(password, user.Salt, user.PasswordHash))
            {
                user.FailedLoginAttempts++;
                if (user.FailedLoginAttempts >= MaxFailedAttempts)
                {
                    user.LockedUntil = DateTime.UtcNow.AddMinutes(LockoutMinutes);
                    await _userRepository.UpdateAsync(user);
                    return (false, $"Too many failed attempts. Account locked for {LockoutMinutes} minutes.", null);
                }

                await _userRepository.UpdateAsync(user);
                return (false, $"Invalid email or password. {MaxFailedAttempts - user.FailedLoginAttempts} attempts remaining.", null);
            }

            // Successful login
            user.FailedLoginAttempts = 0;
            user.LockedUntil = null;
            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            return (true, "Login successful.", user);
        }

        public async Task<(bool Success, string Message)> UpdateUserAsync(Users user)
        {
            var existing = await _userRepository.GetByIdAsync(user.Id);
            if (existing is null)
                return (false, "User not found.");

            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
            return (true, "User updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteUserAsync(int id)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing is null)
                return (false, "User not found.");

            // Soft delete
            existing.IsDeleted = true;
            existing.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(existing);
            return (true, "User deleted successfully.");
        }

        public async Task<(bool Success, string Message)> ChangePasswordAsync(
            int userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
                return (false, "User not found.");

            if (!PasswordHelper.VerifyPassword(oldPassword, user.Salt, user.PasswordHash))
                return (false, "Current password is incorrect.");

            var newSalt = PasswordHelper.GenerateSalt();
            user.Salt = newSalt;
            user.PasswordHash = PasswordHelper.HashPassword(newPassword, newSalt);
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return (true, "Password changed successfully.");
        }

        public async Task<(bool Success, string Message)> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null)
                return (false, "No account found with that email.");

            user.PasswordResetToken = PasswordHelper.GenerateResetToken();
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);
            await _userRepository.UpdateAsync(user);

            // In production: send token via email here
            return (true, $"Reset token generated: {user.PasswordResetToken}");
        }

        public async Task<(bool Success, string Message)> ResetPasswordAsync(string token, string newPassword)
        {
            var user = await _userRepository.GetByResetTokenAsync(token);
            if (user is null)
                return (false, "Invalid or expired reset token.");

            var newSalt = PasswordHelper.GenerateSalt();
            user.Salt = newSalt;
            user.PasswordHash = PasswordHelper.HashPassword(newPassword, newSalt);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return (true, "Password reset successfully.");
        }
    }
}
