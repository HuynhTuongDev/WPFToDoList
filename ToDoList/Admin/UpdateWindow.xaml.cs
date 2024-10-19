using System;
using System.Windows;
using ToDoList.Services;
using BusinessObject;

namespace ToDoList
{
    public partial class UpdateCategoryWindow : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly Category _currentCategory;

        // Constructor nhận một đối tượng Category hiện tại
        public UpdateCategoryWindow(Category category)
        {
            InitializeComponent();

            // Lấy instance của CategoryService từ ServiceProvider
            _categoryService = (ICategoryService)App.ServiceProvider.GetService(typeof(ICategoryService));

            if (_categoryService == null)
            {
                MessageBox.Show("Không thể lấy CategoryService.", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            // Gán category hiện tại vào biến
            _currentCategory = category;

            // Hiển thị dữ liệu hiện tại lên giao diện
            txtName.Text = _currentCategory.Name;
            txtDescription.Text = _currentCategory.Description;
        }

        // Sự kiện khi nhấn nút "Update Category"
        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu mới từ giao diện
            string newName = txtName.Text;
            string newDescription = txtDescription.Text;

            // Kiểm tra nếu tên và mô tả không rỗng
            if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newDescription))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Cập nhật dữ liệu mới vào đối tượng Category
            _currentCategory.Name = newName;
            _currentCategory.Description = newDescription;

            // Cập nhật danh mục thông qua CategoryService
            try
            {
                _categoryService.UpdateCategory(_currentCategory);
                MessageBox.Show("Danh mục đã được cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Close(); // Đóng cửa sổ sau khi cập nhật thành công
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi cập nhật danh mục: {ex.Message}", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện khi nhấn nút "Close"
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Đóng cửa sổ
        }

    }
}
