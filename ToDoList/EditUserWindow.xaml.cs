using System.Windows;

namespace ToDoList
{
    public partial class EditUserWindow : Window
    {
        public User UpdatedUser { get; private set; }

        public EditUserWindow(User userToEdit)
        {
            InitializeComponent();
            // Gán giá trị cho các trường từ người dùng được chỉnh sửa
            UserNameTextBox.Text = userToEdit.Username; // Thay đổi từ FullName thành Username
            EmailTextBox.Text = userToEdit.Email;
            PhoneNumberTextBox.Text = userToEdit.PhoneNumber;
            UpdatedUser = userToEdit; // Giữ bản sao để cập nhật sau
        }

        // Sự kiện nhấn nút OK
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật thông tin người dùng
            UpdatedUser.Username = UserNameTextBox.Text; // Cập nhật Username
            UpdatedUser.Email = EmailTextBox.Text;
            UpdatedUser.PhoneNumber = PhoneNumberTextBox.Text;

            DialogResult = true; // Đặt kết quả của cửa sổ là true
            Close(); // Đóng cửa sổ
        }

        // Sự kiện nhấn nút Cancel
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Đặt kết quả của cửa sổ là false
            Close(); // Đóng cửa sổ
        }
    }
}

