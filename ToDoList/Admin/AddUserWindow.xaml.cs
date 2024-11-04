using BusinessObject;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.Services;

namespace ToDoList
{
    public partial class AddUserWindow : Window
    {
        private readonly IUserService _userService;
        private List<User> users;
        public AddUserWindow(List<User> users, IUserService userService)
        {
            InitializeComponent();
            this.users = users; 
            _userService = userService;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var validation = new Validation(); // Đảm bảo rằng bạn đã khởi tạo đối tượng Validation
            var hashPassword = new HashPassword(); // Đảm bảo rằng bạn đã khởi tạo đối tượng HashPassword
            // Kiểm tra tính hợp lệ của các trường dữ liệu
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra định dạng email
            if (!validation.IsValidEmail(EmailTextBox.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra mật khẩu
            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var hashPass = hashPassword.HashPass(PasswordBox.Password);
            // Tạo đối tượng User mới
            var newUser = new User
            {
                FullName = UsernameTextBox.Text,
                Email = EmailTextBox.Text,
                Password = hashPass,
                Role = 1,
            };

            try
            {
                // Kiểm tra xem người dùng đã tồn tại chưa
                var existingUser = await _userService.GetUserByEmailAsync(newUser.Email);
                if (existingUser != null)
                {
                    MessageBox.Show("This email is already registered. Please use a different email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Gọi UserService để thêm người dùng
                await _userService.AddUserAsync(newUser);

                // Đóng cửa sổ và báo hiệu thêm thành công
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
