using BusinessObject;

namespace ToDoList.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        User UpdateUser(User user);
        void DeleteUser(int userId);
        List<User> GetAllUsers();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> RegisterUserAsync(User user);
        Task<User> LoginUserAsync(string email, string password);
    }
}
