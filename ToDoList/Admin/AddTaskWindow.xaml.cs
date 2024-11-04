using BusinessObject;
using Microsoft.VisualBasic.ApplicationServices;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoList.Repositories;
using ToDoList.Services;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        private TodoService todoService;
        private CategoryService categoryService;
        private readonly int _userId;
        public AddTaskWindow( int UserId,ITodoRepository todoRepository,ICategoryRepository categoryRepository)
        {
            InitializeComponent();
            todoService = new TodoService(todoRepository);
            categoryService = new CategoryService(categoryRepository);
          
            _userId = UserId;
        }

        private async void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dpDueDate.SelectedDate == null)
            {
                MessageBox.Show("Due date is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Todo todo = new();
            todo.Title = txtTitle.Text;
            todo.Description = txtDescription.Text;
            todo.DueDate = dpDueDate.SelectedDate.Value;
            todo.IsCompleted = chkIsCompleted.IsChecked == false;
            todo.CategoryId = (int)CategoryComboBox.SelectedValue;
            todo.UserId = _userId; 
            todo.CreatedAt = DateTime.Now;
            todo.UpdatedAt = DateTime.Now;
            
            await todoService.AddTodoAsync(todo);

            MessageBox.Show("Add task successfully!", "Notification", MessageBoxButton.OK);
            
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void LoadCategories()
        {
            try
            {
                var categories = categoryService.GetCategories();
                if (categories != null && categories.Count > 0)
                {
                    CategoryComboBox.ItemsSource = categories;
                    CategoryComboBox.DisplayMemberPath = "Name"; 
                    CategoryComboBox.SelectedValuePath = "Id"; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategories();
        }
    }
}
