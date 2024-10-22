using BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Repositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetTodosAsync();
        Task<Todo> GetTodoByIdAsync(int id);
        Task AddTodoAsync(Todo todo);
        Task UpdateTodoAsync(Todo todo);
        Task DeleteTodoAsync(int id);
        Task<List<Todo>> GetUserTasks(int userId);
    }
}
