using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLayer
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Todo> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa mối quan hệ giữa User và Todo
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);

            // Định nghĩa mối quan hệ giữa Category và Todo
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Todos)
                .HasForeignKey(t => t.CategoryId);
        }
    }

    public class TaskManagementContextFactory : IDesignTimeDbContextFactory<TaskManagementContext>
    {
        public TaskManagementContext CreateDbContext(string[] args = null)
        {
            var builder = new DbContextOptionsBuilder<TaskManagementContext>();

            // Lấy đường dẫn đến thư mục gốc của dự án
            var basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)); // Di chuyển lên ba cấp thư mục
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Lấy chuỗi kết nối từ cấu hình
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new TaskManagementContext(builder.Options);
        }
    }

}
