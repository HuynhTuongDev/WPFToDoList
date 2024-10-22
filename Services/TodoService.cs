using BusinessObject;
using ToDoList.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // Lấy danh sách tất cả công việc (tasks)
        public async Task<List<Todo>> GetTodosAsync()
        {
            return await _todoRepository.GetTodosAsync();
        }

        // Lấy thông tin chi tiết của một công việc dựa trên id
        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _todoRepository.GetTodoByIdAsync(id);
        }

        // Thêm mới một công việc
        public async Task AddTodoAsync(Todo todo)
        {
            await _todoRepository.AddTodoAsync(todo);
        }

        // Cập nhật thông tin công việc
        public async Task UpdateTodoAsync(Todo todo)
        {
            await _todoRepository.UpdateTodoAsync(todo);
        }

        // Xóa một công việc dựa trên id
        public async Task DeleteTodoAsync(int id)
        {
            await _todoRepository.DeleteTodoAsync(id);
        }
        public async Task<List<Todo>> GetUserTasks(int userId)
        {
            return await _todoRepository.GetUserTasks(userId); // Gọi phương thức từ repository
        }
    }
}
