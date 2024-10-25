using BusinessObject;
using System.Windows;
using ToDoList.Services;

namespace ToDoList
{
    public partial class UpdateTask : Window
    {
        private readonly ITodoService _todoService;
        private readonly ICategoryService _categoryService;
        private Todo _currentTask;

        public UpdateTask(Todo task, ITodoService todoService, ICategoryService categoryService)
        {
            InitializeComponent();
            _currentTask = task;
            _todoService = todoService;
            _categoryService = categoryService;

            // Hiển thị thông tin công việc đã chọn trong giao diện
            TitleTextBox.Text = _currentTask.Title;
            DescriptionTextbox.Text = _currentTask.Description;

            // Nạp danh sách danh mục vào CategoryComboBox (giả sử bạn có phương thức GetCategories trong ICategoryService)
            LoadCategories();
            CategoryComboBox.SelectedValue = _currentTask.CategoryId; // Chọn danh mục hiện tại

            CompletionCheckBox.IsChecked = _currentTask.IsCompleted;
        }

        private async void LoadCategories()
        {
            var categories =  _categoryService.GetCategories(); // Phương thức lấy danh mục
            CategoryComboBox.ItemsSource = categories; // Gán danh sách danh mục cho ComboBox
            CategoryComboBox.DisplayMemberPath = "Name"; // Giả sử bạn có thuộc tính "Name" trong Category
            CategoryComboBox.SelectedValuePath = "Id"; // Giả sử bạn có thuộc tính "Id" trong Category
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật thông tin công việc từ giao diện
            _currentTask.Title = TitleTextBox.Text;
            _currentTask.Description = DescriptionTextbox.Text;
            _currentTask.CategoryId = (int)CategoryComboBox.SelectedValue;
            _currentTask.IsCompleted = CompletionCheckBox.IsChecked ?? false;

            // Gọi dịch vụ để cập nhật công việc
            await _todoService.UpdateTodoAsync(_currentTask);

            // Đóng cửa sổ và trả về kết quả
            DialogResult = true;
            Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Đóng cửa sổ mà không lưu
            Close();
        }
    }
}
