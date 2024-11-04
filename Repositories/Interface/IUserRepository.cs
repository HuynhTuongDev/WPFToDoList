using BusinessObject;  
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user); 
        User UpdateUser(User user); 
        void DeleteUser(int userId); 
        List<User> GetAllUsers(); 
        Task<User> GetUserByEmailAsync(string email); 
    }
}

