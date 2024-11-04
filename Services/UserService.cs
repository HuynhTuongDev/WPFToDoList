using BusinessObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<User> RegisterUserAsync(User user)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Email đã tồn tại. Vui lòng sử dụng email khác.");
            }

            // Đăng ký người dùng mới
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<User> AddUserAsync(User user) => await _userRepository.AddUserAsync(user);
        public User UpdateUser(User user) => _userRepository.UpdateUser(user);
        public void DeleteUser(int userId) => _userRepository.DeleteUser(userId);
        public List <User> GetAllUsers() =>  _userRepository.GetAllUsers();
        public async Task<User> GetUserByEmailAsync(string email) => await _userRepository.GetUserByEmailAsync(email);

        public async Task<User> LoginUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || user.Password != password || user.State != "ACTIVE")
            {
                return null;
            }
            return user;
        }
    }
}
