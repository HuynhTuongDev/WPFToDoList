using Services;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Services;

namespace ToDoList
{
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        // Sự kiện khi nhấn nút "Quản lý người dùng"
        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị nội dung mới cho Quản lý người dùng
            MainContent.Content = new ManageUser();
        }

        // Sự kiện khi nhấn nút "Quản lý danh mục tác vụ"
        private void ManageTaskCategories_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị nội dung mới cho Quản lý danh mục tác vụ
            MainContent.Content = new ManageTaskCate();
        }


        // Sự kiện khi nhấn nút "Quản lý tác vụ"
        private void ManageTasks_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị nội dung mới cho Quản lý tác vụ
            MainContent.Content = new ManageTask();
        }

        // Sự kiện khi nhấn nút "Đăng xuất"
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý đăng xuất
            MessageBox.Show("Đăng xuất thành công!");
            this.Close(); // Hoặc điều hướng sang màn hình đăng nhập
        }
    }
}
