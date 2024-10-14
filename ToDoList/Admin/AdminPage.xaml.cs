
using System.Windows;

namespace ToDoList
{
    public partial class Admin : Window
    {

        public Admin()
        {
            InitializeComponent();
            LoadUserDetails();
        }

        // Phương thức để tải thông tin người dùng vào giao diện
        private void LoadUserDetails()
        {
        }

        // Sự kiện cho nút Đăng xuất
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Thực hiện đăng xuất và trở về màn hình đăng nhập
            // Ví dụ: 
            MessageBox.Show("Bạn đã đăng xuất thành công!");
            this.Close(); // Đóng cửa sổ admin
            // Điều hướng về màn hình đăng nhập nếu cần
        }

        // Sự kiện cho nút Quản lý người dùng
        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
           
        }

        // Sự kiện cho nút Quản lý danh mục tác vụ
        private void ManageTaskCategories_Click(object sender, RoutedEventArgs e)
        {
        }

        // Sự kiện cho nút Quản lý tác vụ
        private void ManageTasks_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
