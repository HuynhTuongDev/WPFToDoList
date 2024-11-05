using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ToDoList.Services;
using MessageBox = System.Windows.MessageBox;

namespace ToDoList
{
    public partial class AddUserWindow : Window
    {
        private readonly IUserService _userService;
        private List<User> users;
        private readonly Validation _validation;
        private readonly HashPassword _hashPassword;
        public AddUserWindow(List<User> users, IUserService userService, Validation validation, HashPassword hashPassword)
        {
            InitializeComponent();
            this.users = users;
            _userService = userService;
            _validation = validation;
            _hashPassword = hashPassword;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {

            // Lấy thông tin từ các textbox
            string email = EmailTextBox.Text;
            string fullname = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string passwordConfirm = ConfirmPasswordBox.Password;

            // Kiểm tra thông tin hợp lệ
            if (!ValidateUser(email, fullname, password, passwordConfirm)) return;

            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await _userService.GetUserByEmailAsync(email);
            if (existingUser != null) // Nếu email đã tồn tại trong hệ thống
            {
                MessageBox.Show("Email đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Mã hóa mật khẩu
            string hashedPassword = _hashPassword.HashPass(password);

            // Tạo đối tượng người dùng mới
            var newUser = new User
            {
                Email = email,
                FullName = fullname,
                Password = hashedPassword,
                Role = 1,
                State = "ACTIVE",
            };

            // Đăng ký người dùng mới
            await _userService.RegisterUserAsync(newUser);
            this.DialogResult = true;
            Close();
        }
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
            if (!_validation.IsValidEmail(email))
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
    }
}