using BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks; // Thêm namespace này để sử dụng Task

namespace ToDoList.Repositories
{
    public interface IUserRepository
    {
        Task<User> LoginUserAsync(string email, string hashPassword); 
        Task<User> AddUserAsync(User user); 
        Task<bool> GetUserByEmailAsync( string email);
    }
}
