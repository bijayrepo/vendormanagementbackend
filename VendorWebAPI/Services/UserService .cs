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
            try
            {

                if (await IsEmailTakenAsync(dto.Email))
                {
                    return false; // Email already taken
                }
                var user = new Model.User
                {
                    VendorID = dto.VendorID,
                    UserID = Guid.NewGuid(),
                    FullName = dto.FullName,
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = dto.Password // Hash the password
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
