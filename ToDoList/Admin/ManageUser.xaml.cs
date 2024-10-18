using BusinessObject;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ToDoList
{
    public partial class ManageUser : UserControl
    {
        private List<User> users;

        public ManageUser()
        {
            InitializeComponent();
            users = new List<User>();
            UserDataGrid.ItemsSource = users; // Liên kết danh sách người dùng với DataGrid
        }

        // Phương thức xử lý khi nhấn nút "Thêm"
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ AddUserWindow để thêm người dùng mới
            AddUserWindow addUserWindow = new AddUserWindow(users);
            addUserWindow.ShowDialog();

            if (addUserWindow.DialogResult == true)
            {
                // Cập nhật lại DataGrid nếu người dùng mới được thêm thành công
                UserDataGrid.Items.Refresh();
            }
        }

        // Phương thức xử lý khi nhấn nút "Sửa"
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem != null)
            {
                User selectedUser = (User)UserDataGrid.SelectedItem;
                EditUserWindow editWindow = new EditUserWindow(selectedUser);

                if (editWindow.ShowDialog() == true)
                {
                    // Cập nhật lại DataGrid sau khi chỉnh sửa
                    UserDataGrid.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please select a user to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Phương thức xử lý khi nhấn nút "Xóa"
        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem != null)
            {
                User selectedUser = (User)UserDataGrid.SelectedItem;
                users.Remove(selectedUser); // Xóa người dùng được chọn khỏi danh sách
                UserDataGrid.Items.Refresh(); // Cập nhật lại DataGrid sau khi xóa
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}




