using System.Windows;
using ToDoList.Services;
using BusinessObject;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList
{
    public partial class RegisterWindow : Window
    {
        private readonly IUserService userService;

        public RegisterWindow(IUserService userService)
        {
            InitializeComponent();
            this.userService = userService;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string fullname = FullNameTextBox.Text;
            string password = UserPasswordBox.Password;
            string passwordConfirm = PasswordConfirmPasswordBox.Password;

            if (!ValidateUser(email, fullname, password, passwordConfirm)) return;

            bool emailExists = await userService.GetUserByEmailAsync(email);
            if (emailExists)
            {
                MessageBox.Show("Email đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User
            {
                Email = email,
                FullName = fullname,
                Password = password,
                Role = 1
            };

            await userService.RegisterUserAsync(newUser);
            MessageBox.Show("Đăng ký thành công. Đang chuyển hướng đến cửa sổ đăng nhập.", "Thành công");

            var loginWindow = new LoginWindow(userService);
            loginWindow.Show();
            Close();
        }

        private bool ValidateUser(string email, string fullname, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(fullname) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Tất cả các trường đều bắt buộc!", "Trường cần thiết", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu không khớp với xác nhận!", "Không khớp thông tin", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = App.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Close();
        }
    }
}
