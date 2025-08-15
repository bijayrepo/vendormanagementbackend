using UserManagement.Core.Entities;

namespace UserManagement.Core.Interfaces
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid userId);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid userId);

    }
}
