using VendorWebAPI.DTOs;

namespace VendorWebAPI.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto dto);
        Task<bool> IsEmailTakenAsync(string email);
        //Task<bool> UpdateUserAsync(RegisterUserDto dto);
    }
   
}
