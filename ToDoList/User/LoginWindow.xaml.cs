using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using ToDoList.Services;

namespace ToDoList
{
    public partial class LoginWindow : Window
    {
        private readonly IUserService _userService;
        private readonly ITodoService _todoServices;

        public LoginWindow(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = UserPasswordBox.Password;

            // Kiểm tra xem email và mật khẩu có trống hay không
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ShowErrorMessage("Email hoặc mật khẩu không được để trống!", "Cần có trường");
                return;
            }

            // Gọi dịch vụ để đăng nhập
            var user = await _userService.LoginUserAsync(email, password);
            if (user == null)
            {
                ShowErrorMessage("Email hoặc mật khẩu không chính xác!", "Thông tin không chính xác");
                return;
            }

            // Ẩn cửa sổ đăng nhập
            Hide();
            OpenUserWindow(user);
            Close();
        }

        private void OpenUserWindow(User user)
        {
            var todoService = App.ServiceProvider.GetRequiredService<ITodoService>();
            var categoryService = App.ServiceProvider.GetRequiredService<ICategoryService>();

            var userService = App.ServiceProvider.GetRequiredService<IUserService>();
            if (user.Role == 0)
            {
                var adminWindow = new Admin(user, todoService, categoryService, userService);
                adminWindow.ShowDialog();
            }
            else if (user.Role == 1)
            {
                var mainWindow = new MainWindow(user, todoService, categoryService);
                mainWindow.ShowDialog();
            }
            else
            {
                ShowErrorMessage("Bạn không có quyền truy cập hệ thống", "Không được phép");
            }
        }

        private void ShowErrorMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = App.ServiceProvider.GetRequiredService<RegisterWindow>();
            registerWindow.Show();
            Close();
        }
    }
}
