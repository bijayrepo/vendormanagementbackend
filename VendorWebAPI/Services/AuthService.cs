using VendorWebAPI.Interfaces;

namespace VendorWebAPI.Services
{
    public abstract class  AuthService:IAuthService
    {
        public async Task<string> GenerateJwtTokenAsync(string email, string password)
        {
            return string.Empty;
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
    }
}
