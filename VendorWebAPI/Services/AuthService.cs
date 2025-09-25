using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VendorWebAPI.Data;
using VendorWebAPI.Interfaces;
using VendorWebAPI.Model;

namespace VendorWebAPI.Services
{
    public class  AuthService:IAuthService, IUserAuthService
    {
        private readonly AppDbContext _context;
        private readonly string _jwtSecret;
        public AuthService(AppDbContext context, string jwtSecret)
        {
            _context = context;
            _jwtSecret = jwtSecret;
        }
        public async Task<string> GenerateJwtTokenAsync(string email, string password)
        {
            
            var user = _context.Users.FirstOrDefault(u => string.Equals(u.Email.ToLower(), email.ToLower()));
            if (user == null || password != user.Password)
            {
                return string.Empty; // Or throw exception
            }

            //  Create token claims
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Username.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserRole??string.Empty)
        };

            // 3️⃣ Generate JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "nextdata.cloud",         // optional
                audience: "nextdata.cloud",       // optional
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> ValidateTokenAsync(string token)
        {
            return true;
        }
        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            return string.Empty;
        }
        public async Task<bool> RevokeTokenAsync(string token)
        {
            return true;
        }
        public async Task<UserResponse> UserLogin(string email, string password)
        {
            var user = new User();
            if (password != null || email != null)
            {
                user = _context.Users.FirstOrDefault(u => string.Equals(u.Email.ToLower(), email.ToLower()));
                if(user == null)
                {
                    // user = _context.Users.Find(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                    return new UserResponse
                    {
                        StatusCode = 401,
                        Token = Guid.NewGuid().ToString(),
                        Message = "Invalid username or password",
                        data = new User(),
                    };
                }
            }
            else if (email == null || password == null)
            {
                return new UserResponse
                {
                    StatusCode = 400,
                    Token = Guid.NewGuid().ToString(),
                    Message = "Email and password are required",
                    data = new User(),
                };
            }
            // Generate JWT token
            var jwtToken="JWT-Token";
            //var jwtToken = await GenerateJwtTokenAsync(email, password);

            return new UserResponse
            {
                StatusCode = 200,
                Token = jwtToken,
                Message = "Login successful",
                data = user
            };

        }
    }
}
