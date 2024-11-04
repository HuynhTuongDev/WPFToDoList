using System.Windows;
using ToDoList.Services;
using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using Services;
namespace ToDoList
{
    public partial class RegisterWindow : Window
    {
        private readonly IUserService userService;
        private readonly Validation validation;
        private readonly HashPassword hashPassword;
        public RegisterWindow(IUserService userService, Validation validation, HashPassword hashPassword)
        {
            InitializeComponent();
            this.userService = userService;
            this.validation = validation; 
            this.hashPassword = hashPassword; 
        }

        // Sự kiện khi nhấn nút "Đăng ký"
        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ các textbox
            string email = EmailTextBox.Text;
            string fullname = FullNameTextBox.Text;
            string password = UserPasswordBox.Password;
            string passwordConfirm = PasswordConfirmPasswordBox.Password;

            // Kiểm tra thông tin hợp lệ
            if (!ValidateUser(email, fullname, password, passwordConfirm)) return;

            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await userService.GetUserByEmailAsync(email);
            if (existingUser != null) // Nếu email đã tồn tại trong hệ thống
            {
                MessageBox.Show("Email đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Mã hóa mật khẩu
            string hashedPassword = hashPassword.HashPass(password);

            // Tạo đối tượng người dùng mới
            var newUser = new User
            {
                Email = email,
                FullName = fullname,
                Password = hashedPassword, // Lưu mật khẩu đã băm
                Role = 1,
                State = "ACTIVE",
            };

            // Đăng ký người dùng mới
            await userService.RegisterUserAsync(newUser);
            MessageBox.Show("Đăng ký thành công. Đang chuyển hướng đến cửa sổ đăng nhập.", "Thành công");

            // Chuyển sang cửa sổ đăng nhập
            var loginWindow = App.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Close();
        }

        // Kiểm tra hợp lệ thông tin người dùng
        private bool ValidateUser(string email, string fullname, string password, string confirmPassword)
        {
            // Kiểm tra nếu có trường nào bỏ trống
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(fullname) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Tất cả các trường đều bắt buộc!", "Trường cần thiết", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Kiểm tra tính hợp lệ của email
            if (!validation.IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi email", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Kiểm tra nếu mật khẩu và xác nhận mật khẩu không khớp
            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu không khớp với xác nhận!", "Không khớp thông tin", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Thông tin hợp lệ
            return true;
        }

        // Sự kiện khi nhấn nút "Đăng nhập"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ đăng nhập
            var loginWindow = App.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Close();
        }
    }
}
