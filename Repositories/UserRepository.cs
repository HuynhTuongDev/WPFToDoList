using BusinessObject;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ToDoList.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagementContext _context;

        public UserRepository(TaskManagementContext context)
        {
            _context = context;
        }

        public async Task<bool> GetUserByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> LoginUserAsync(string email, string hashPassword)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == hashPassword);
        }

        public async Task<User> AddUserAsync(User user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return user;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; // Ném lại ngoại lệ để xử lý ở nơi khác
            }
        }
    }
}
