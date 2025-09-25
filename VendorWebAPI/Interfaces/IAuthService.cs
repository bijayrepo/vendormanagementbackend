using VendorWebAPI.Model;

namespace VendorWebAPI.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwtTokenAsync(string email, string password);
        Task<bool> ValidateTokenAsync(string token);
        Task<string> RefreshTokenAsync(string refreshToken);
        Task<bool> RevokeTokenAsync(string token);
    }
    public interface IUserAuthService
    {
        Task<UserResponse> UserLogin(string email, string password);
    }
}
