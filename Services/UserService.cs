using BusinessObject;
using System.Security.Cryptography;
using System.Text;
using ToDoList.Repositories;

namespace ToDoList.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            var hashedPassword = HashPassword(password);
            return await _userRepository.LoginUserAsync(email, hashedPassword);
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            user.Password = HashPassword(user.Password);
            return await _userRepository.AddUserAsync(user);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
