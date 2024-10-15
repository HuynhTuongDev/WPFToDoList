using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        public User User { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Xử lý khi văn bản trong ô tìm kiếm thay đổi
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý khi nút tìm kiếm được nhấn
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý khi nút thêm công việc được nhấn
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý khi nút chỉnh sửa công việc được nhấn
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý khi nút xóa công việc được nhấn
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Gọi phương thức để dọn dẹp thông tin người dùng
            ClearUserSession();

            // Chuyển hướng đến màn hình đăng nhập
            OpenLoginWindow();
        }

        private void ClearUserSession()
        {
            // Xóa thông tin người dùng
            User = null; // Đặt User về null

            // Thông báo người dùng về việc đăng xuất thành công
            MessageBox.Show("Đã đăng xuất thành công.");
        }

        private void OpenLoginWindow()
        {
            // Mở cửa sổ đăng nhập
            var loginWindow = App.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Close(); // Đóng cửa sổ hiện tại
        }
    }
}
