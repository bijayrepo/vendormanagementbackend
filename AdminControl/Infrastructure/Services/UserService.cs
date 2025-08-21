using Microsoft.EntityFrameworkCore;
using AdminControl.Core.Entities;
using AdminControl.Core.Interfaces;
using AdminControl.Infrastructure.Data;

namespace AdminControl.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
        public async Task<User?> UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = await _context.Users.FindAsync(user.UserID);
            if (existingUser == null)
                return null;

            // Update only incoming values
            existingUser.Username = user.Username;
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                existingUser.Password = user.Password;
            }

            existingUser.UserRole = user.UserRole ?? existingUser.UserRole;
            existingUser.ProfilePictureUrl = user.ProfilePictureUrl ?? existingUser.ProfilePictureUrl;
            existingUser.ProfilePictureFileName = user.ProfilePictureFileName ?? existingUser.ProfilePictureFileName;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return existingUser;
        }
    }
}
