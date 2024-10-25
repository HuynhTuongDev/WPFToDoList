using BusinessObject;
using System;
using System.Windows;
using ToDoList.Services;

namespace ToDoList
{
    public partial class EditUserWindow : Window
    {
        private readonly IUserService _userService;
        public User UpdatedUser { get; private set; }

        public EditUserWindow(User userToEdit, IUserService userService)
        {
            InitializeComponent();

            // Kiểm tra nếu userToEdit là null
            if (userToEdit == null)
            {
                MessageBox.Show("User to edit is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra nếu các TextBox không được khởi tạo
            if (UserNameTextBox == null || EmailTextBox == null)
            {
                MessageBox.Show("UI controls are not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Gán giá trị cho các TextBox
            UserNameTextBox.Text = userToEdit.FullName;
            EmailTextBox.Text = userToEdit.Email;
            UpdatedUser = userToEdit;
            _userService = userService;
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật thông tin người dùng
            UpdatedUser.FullName = UserNameTextBox.Text;
            UpdatedUser.Email = EmailTextBox.Text;

            try
            {
                // Gọi UserService để cập nhật người dùng
                await _userService.UpdateUserAsync(UpdatedUser);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Đóng cửa sổ khi người dùng nhấn nút Cancel
            this.Close();
        }
    }
}
