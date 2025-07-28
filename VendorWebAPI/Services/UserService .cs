using Microsoft.EntityFrameworkCore;
using VendorWebAPI.Data;
using VendorWebAPI.DTOs;
using VendorWebAPI.Interfaces;

namespace VendorWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUserAsync(RegisterUserDto dto)
        {
            if (await IsEmailTakenAsync(dto.Email))
            {
                return false; // Email already taken
            }
            var user = new Model.User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password // Hash the password
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
