using System;
using System.Windows;
using ToDoList.Services;
using BusinessObject;

namespace ToDoList
{
    public partial class AddWindow : Window
    {
        private readonly ICategoryService _categoryService;

        public AddWindow()
        {
            InitializeComponent();

            // Lấy instance của CategoryService từ ServiceProvider
            if (App.ServiceProvider == null)
            {
                MessageBox.Show("ServiceProvider chưa được khởi tạo.", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            _categoryService = (ICategoryService)App.ServiceProvider.GetService(typeof(ICategoryService));

            if (_categoryService == null)
            {
                MessageBox.Show("Không thể lấy CategoryService.", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
        }

        // Sự kiện khi nhấn nút "Add Category"
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ các TextBox
            string Name = txtName.Text;
            string Description = txtDescription.Text;

            // Kiểm tra các trường dữ liệu không được để trống
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tạo một đối tượng Category mới
            Category newCategory = new Category
            {
                Name = Name,
                Description = Description
            };

            // Gọi phương thức SaveCategory từ CategoryService để lưu thông tin danh mục mới
            try
            {
                _categoryService.SaveCategory(newCategory);
                MessageBox.Show("Danh mục đã được thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Close(); // Đóng cửa sổ sau khi thêm thành công
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                MessageBox.Show($"Có lỗi xảy ra khi thêm danh mục: {ex.Message}", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện khi nhấn nút "Close"
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Đóng cửa sổ
        }
    }
}
