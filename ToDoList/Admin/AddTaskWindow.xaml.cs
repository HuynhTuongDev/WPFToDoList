using BusinessObject;
using Services;
using System;
using System.Windows;
using ToDoList.Repositories;
using ToDoList.Services;

namespace ToDoList
{
    public partial class AddTaskWindow : Window
    {
        private readonly TodoService todoService;
        private readonly CategoryService categoryService;
        private readonly UserService userService;
        private readonly int _userId;

        public AddTaskWindow(int UserId, ITodoRepository todoRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            InitializeComponent();
            todoService = new TodoService(todoRepository);
            categoryService = new CategoryService(categoryRepository);
            userService = new UserService(userRepository);
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

            if (lstAssignTo.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please assign at least one user to this task.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (User selectedUser in lstAssignTo.SelectedItems)
            {
                Todo todo = new Todo
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    DueDate = dpDueDate.SelectedDate.Value,
                    IsCompleted = false,
                    CategoryId = (int)CategoryComboBox.SelectedValue,
                    UserId = selectedUser.UserId, 
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = _userId,
                };

                await todoService.AddTodoAsync(todo);
            }

            MessageBox.Show("Add task successfully!", "Notification", MessageBoxButton.OK);
            this.Close();
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
        private void LoadUsers()
        {
            try
            {
                var users = userService.GetAllUsers();
                if (users != null && users.Count > 0)
                {
                    lstAssignTo.ItemsSource = users;
                    lstAssignTo.DisplayMemberPath = "FullName";
                    lstAssignTo.SelectedValuePath = "UserId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategories();
            LoadUsers();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
