using AdminControl.Core.Entities;

namespace AdminControl.Core.Interfaces
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> LoginAsync(string username, string password);
        Task<User?> UpdateUserAsync(User user);

    }
}
