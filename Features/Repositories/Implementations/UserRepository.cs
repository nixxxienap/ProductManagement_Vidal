using Microsoft.EntityFrameworkCore;
using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Data;
using ProductManagement.Features.Repositories.Interfaces;

namespace ProductManagement.Features.Repositories.Implementations
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<Users?> GetByEmailAsync(string email) =>
            await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && !u.IsDeleted);

        public async Task<IEnumerable<Users>> GetByRoleAsync(UserRole role) =>
        await _context.Users
                .Where(u => u.Role == role && !u.IsDeleted)
                .OrderBy(u => u.FullName)
                .ToListAsync();

        public async Task<IEnumerable<Users>> GetActiveUsersAsync() =>
        await _context.Users
                .Where(u => u.Status == UserStatus.Active && !u.IsDeleted)
                .OrderBy(u => u.FullName)
                .ToListAsync();

        public async Task<Users?> GetByResetTokenAsync(string token) =>
            await _context.Users
                .FirstOrDefaultAsync(u => u.PasswordResetToken == token &&
                                          u.PasswordResetTokenExpiry > DateTime.UtcNow &&
                                          !u.IsDeleted);

        public async Task<bool> ExistsAsync(string email) =>
            await _context.Users
                .AnyAsync(u => u.Email.ToLower() == email.ToLower() && !u.IsDeleted);

    }
}
