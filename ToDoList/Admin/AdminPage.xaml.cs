using BusinessObject;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Repositories;
using ToDoList.Services;

namespace ToDoList
{
    public partial class Admin : Window
    {
        public User User { get; set; }

        private readonly IUserService _userService;

        // Constructor không tham số để tránh lỗi CS1729

        private readonly ITodoService _todoServices;
        private readonly ICategoryService _categoryServices;

        public Admin()
        {
            InitializeComponent();
        }


        public Admin(User user, ITodoService todoService, ICategoryService categoryService, IUserService userService)

        {
            User = user;
            _todoServices = todoService;
            _categoryServices = categoryService;
            InitializeComponent();
            _userService = userService;
        }

        public Admin(User user, IUserService userService)
        {
            InitializeComponent();
            User = user;
            _userService = userService;
        }

        // Sự kiện khi nhấn nút "Quản lý người dùng"
        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị nội dung mới cho Quản lý người dùng
            MainContent.Content = new ManageUser();       
        }

        // Sự kiện khi nhấn nút "Quản lý danh mục tác vụ"
        private void ManageTaskCategories_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị nội dung mới cho Quản lý danh mục tác vụ
            MainContent.Content = new ManageTaskCate();
        }

        // Sự kiện khi nhấn nút "Quản lý tác vụ"  
        private void ManageTasks_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị nội dung mới cho Quản lý tác vụ  
            var todoRepository = App.ServiceProvider.GetRequiredService<ITodoRepository>();
            var categoryRepository = App.ServiceProvider.GetRequiredService<ICategoryRepository>();
            var userRepository = App.ServiceProvider.GetRequiredService<IUserRepository>();
            MainContent.Content = new ManageTask(todoRepository, categoryRepository, User.UserId, userRepository); 
        }

        // Sự kiện khi nhấn nút "Đăng xuất"
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ClearUserSession();
            OpenLoginWindow();
        }

        private void ClearUserSession()
        {
            // Xóa thông tin người dùng
            User = null; // Đặt User về null

            // Thông báo người dùng về việc đăng xuất thành công
            MessageBox.Show("Đã đăng xuất thành công.");
        }

        private void OpenLoginWindow()
        {
            // Mở cửa sổ đăng nhập
            var loginWindow = App.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Close(); // Đóng cửa sổ hiện tại
        }

        }
 }



