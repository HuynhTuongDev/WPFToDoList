using BusinessObject;  
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user); 
        Task<User> UpdateUserAsync(User user); 
        Task DeleteUserAsync(int userId); 
        Task<List<User>> GetAllUsersAsync(); 
        Task<User> GetUserByIdAsync(int userId); 
        Task<User> GetUserByEmailAsync(string email); 
    }
}

