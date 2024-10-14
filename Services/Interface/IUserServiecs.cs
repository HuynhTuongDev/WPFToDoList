using BusinessObject;

namespace ToDoList.Services
{
    public interface IUserService
    {
        Task<bool> GetUserByEmailAsync(string email);
        Task<User> LoginUserAsync(string email, string password);
        Task<User> RegisterUserAsync(User user);
    }
}
