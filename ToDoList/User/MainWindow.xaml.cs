using BusinessObject;
using System.Windows;
using System.Windows.Controls;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
    }

}