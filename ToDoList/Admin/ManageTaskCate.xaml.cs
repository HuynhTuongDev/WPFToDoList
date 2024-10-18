using BusinessObject;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Services;

namespace ToDoList
{
    public partial class ManageTaskCate : UserControl
    {
        private readonly ICategoryService _categoryService;

        public ManageTaskCate()
        {
            InitializeComponent();

            // Lấy instance của CategoryService từ ServiceProvider
            _categoryService = (ICategoryService)App.ServiceProvider.GetService(typeof(ICategoryService));
        }

        // Sự kiện khi UserControl được tải
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTaskCategories();
        }

        // Load danh sách các danh mục tác vụ
        private void LoadTaskCategories()
        {
            // Lấy danh sách từ service
            List<Category> categories = _categoryService.GetCategories();

            // Gán danh sách vào DataGrid
            TaskCategoriesDataGrid.ItemsSource = categories;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic xóa danh mục
            var selectedCategory = (Category)TaskCategoriesDataGrid.SelectedItem;
            if (selectedCategory != null)
            {
                _categoryService.DeleteCategory(selectedCategory);
                LoadTaskCategories(); // Refresh DataGrid
                MessageBox.Show("Category removed successfully!");
            }
            else
            {
                MessageBox.Show("Please select a category to remove.");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCategory = (Category)TaskCategoriesDataGrid.SelectedItem;
            if (selectedCategory != null)
            {
                UpdateWindow updateWindow = new UpdateWindow();
                updateWindow.ShowDialog();
                LoadTaskCategories(); // Refresh DataGrid
            }
            else
            {
                MessageBox.Show("Please select a category to remove.");
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateSelected_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
