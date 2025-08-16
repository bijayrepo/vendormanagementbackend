using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Services
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
    }
}
