using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System.Windows;
using System.Windows.Input;
using ToDoList.Services;

namespace ToDoList
{
    public partial class LoginWindow : Window
    {
        private readonly IUserService _userService;
        private readonly ITodoService _todoServices;
        private readonly HashPassword _hashPassword;
        private readonly Validation _validation;

        public LoginWindow(IUserService userService, HashPassword hashPassword, Validation validation)
        {
            InitializeComponent();
            _userService = userService;
            _hashPassword = hashPassword;
            _validation = validation;
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

            // Mã hóa mật khẩu nhập vào để so sánh
            string hashedPassword = _hashPassword.HashPass(password);

            // Kiểm tra tính hợp lệ của email
            if (!_validation.IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Gọi dịch vụ để đăng nhập với email và mật khẩu đã băm
                var user = await _userService.LoginUserAsync(email, hashedPassword);
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
            catch (Exception ex)
            {
                ShowErrorMessage("Đã xảy ra lỗi trong quá trình đăng nhập: " + ex.Message, "Lỗi hệ thống");
            }
        }

        private void OpenUserWindow(User user)
        {
            var todoService = App.ServiceProvider.GetRequiredService<ITodoService>();
            var categoryService = App.ServiceProvider.GetRequiredService<ICategoryService>();
            var userService = App.ServiceProvider.GetRequiredService<IUserService>();

            Window userWindow;

            // Mở cửa sổ tương ứng dựa vào vai trò người dùng
            switch (user.Role)
            {
                case 0:
                    userWindow = new Admin(user, todoService, categoryService, userService);
                    break;
                case 1:
                    userWindow = new MainWindow(user, todoService, categoryService);
                    break;
                default:
                    ShowErrorMessage("Bạn không có quyền truy cập hệ thống", "Không được phép");
                    return;
            }

            userWindow.ShowDialog();
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
