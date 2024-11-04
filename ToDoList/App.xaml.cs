using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ToDoList.Repositories;
using ToDoList.Services;
using System.Windows;
using Services;

namespace ToDoList
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        private void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            services.AddSingleton(Configuration);

            // Configure DbContext via DI
            services.AddDbContext<TaskManagementContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories, services, etc.
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITodoService, TodoService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<LoginWindow>();
            services.AddTransient<RegisterWindow>();
            services.AddTransient<ManageTaskCate>();
            services.AddTransient<ManageUser>();
            services.AddTransient<Validation>(); 
            services.AddTransient<HashPassword>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure ServiceCollection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Build ServiceProvider
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Open login window
            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
    }
}
