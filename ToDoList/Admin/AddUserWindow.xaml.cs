using BusinessObject;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ToDoList
{
    public partial class AddUserWindow : Window
    {
        private List<User> users;

        public AddUserWindow(List<User> users)
        {
            InitializeComponent();
            this.users = users; // Truyền danh sách người dùng hiện tại
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra tính hợp lệ của các trường dữ liệu
            if (string.IsNullOrWhiteSpace(IdTextBox.Text) ||
                string.IsNullOrWhiteSpace(UsernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra nếu ID có phải là số
            if (!int.TryParse(IdTextBox.Text, out int id))
            {
                MessageBox.Show("ID must be a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tạo đối tượng User mới
            User newUser = new User
            {
                UserId = id,
                FullName = UsernameTextBox.Text,
                Email = EmailTextBox.Text
            };

            // Thêm vào danh sách người dùng
            users.Add(newUser);

            // Đóng cửa sổ và báo hiệu thêm thành công
            this.DialogResult = true;
            this.Close();
        }
    }
}


