using BusinessObject;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TaskManagementContext _context;

        public TodoRepository(TaskManagementContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả các công việc (tasks)
        public async Task<List<Todo>> GetTodosAsync()
        {
            return await _context.Tasks
                .Include(t => t.User) // Kết hợp bảng User với Todo thông qua khóa ngoại UserId
                .Include(t => t.Category) // Kết hợp bảng Category với Todo thông qua khóa ngoại CategoryId
                .Select(t => new Todo
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    CategoryId = t.CategoryId,
                    UserId = t.UserId,
                    CreatedBy = t.CreatedBy,
                    User = new User
                    {
                        UserId = t.User.UserId,
                        Role = t.User.Role,
                        FullName = t.User.FullName
                    },
                    Category = new Category
                    {
                        Id = t.Category.Id,
                        Name = t.Category.Name
                    }
                })
                .ToListAsync();
        }

        // Lấy thông tin chi tiết của một công việc dựa trên id
        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        // Thêm mới một công việc
        public async Task AddTodoAsync(Todo todo)
        {
            _context.Tasks.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin công việc
        public async Task UpdateTodoAsync(Todo todo)
        {
            _context.Tasks.Update(todo);
            await _context.SaveChangesAsync();
        }

        // Xóa một công việc
        public async Task DeleteTodoAsync(int id)
        {
            var todo = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (todo != null)
            {
                _context.Tasks.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Todo>> GetUserTasks(int userId)
        {
            // Thực hiện truy vấn đến cơ sở dữ liệu để lấy danh sách công việc theo userId
          return await _context.Tasks
                    .Where(todo => todo.UserId == userId)
                    .ToListAsync(); // Sử dụng ToListAsync để lấy danh sách công việc
        }
    }
}
