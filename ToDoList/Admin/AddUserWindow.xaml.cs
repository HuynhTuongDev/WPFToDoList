using BusinessObject;
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
            this.users = users; // Truyền danh sách người dùng hiện tại
            _userService = userService;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra tính hợp lệ của các trường dữ liệu
            if (
                string.IsNullOrWhiteSpace(UsernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Tạo đối tượng User mới
            var newUser = new User
            {
                FullName = UsernameTextBox.Text,
                Email = EmailTextBox.Text,
            };

            try
            {
                // Gọi UserService để thêm người dùng
                var createdUser = await _userService.AddUserAsync(newUser);

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



