using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Repositories;
using ToDoList.Services;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        public User User { get; set; }
        private readonly ITodoService _todoServices;
        private readonly ICategoryService _categoryServices;
        private List<Todo> _originalTasks;

        public MainWindow(User user, ITodoService todoService, ICategoryService categoryService)
        {
            InitializeComponent();
            User = user;
            _todoServices = todoService;
            _categoryServices = categoryService;
            LoadUserTasks();
        }

        // Tải danh sách công việc của người dùng
        private async void LoadUserTasks()
        {
            try
            {
                if (User == null)
                {
                    MessageBox.Show("User is not logged in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var userTasks = await _todoServices.GetUserTasks(User.UserId);

                if (userTasks != null && userTasks.Count > 0)
                {
                    UserTasksListView.ItemsSource = userTasks;
                    _originalTasks = userTasks.ToList();
                }
                else
                {
                    UserTasksListView.ItemsSource = null;
                    MessageBox.Show("No tasks found for this user.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Toggle trạng thái hoàn thành của công việc
        private async void ToggleCompletion_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Todo todoItem = button.DataContext as Todo;

                if (todoItem != null)
                {
                    todoItem.IsCompleted = !todoItem.IsCompleted;
                    await _todoServices.UpdateTodoAsync(todoItem);
                    LoadUserTasks();
                }
            }
        }

        // Tìm kiếm công việc
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadUserTasks();
                return;
            }

            var filteredTasks = (UserTasksListView.ItemsSource as List<Todo>)
                ?.Where(task => RemoveDiacritics(task.Title.ToLower()).Contains(searchText) ||
                                RemoveDiacritics(task.Description.ToLower()).Contains(searchText))
                .ToList();

            UserTasksListView.ItemsSource = filteredTasks;

            if (filteredTasks == null || filteredTasks.Count == 0)
            {
                UserTasksListView.ItemsSource = _originalTasks;
            }
        }

        // Xóa dấu trong chuỗi
        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        // Xử lý sự kiện khi nhấn nút tìm kiếm
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox_TextChanged(sender, null);
        }

        // Xử lý sự kiện khi TextBox có tiêu điểm
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Tìm kiếm công việc...")
            {
                SearchTextBox.Text = string.Empty;
            }
        }

        // Xử lý sự kiện khi TextBox mất tiêu điểm
        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchTextBox.Text = "Tìm kiếm công việc...";
            }
        }

        // Thêm công việc mới
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var todoService = App.ServiceProvider.GetRequiredService<ITodoService>();
            var categoryService = App.ServiceProvider.GetRequiredService<ICategoryService>();
            CreateTask createTask = new CreateTask(User.UserId, todoService, categoryService);
            createTask.ShowDialog();
            LoadUserTasks();
        }

        // Chỉnh sửa công việc đã chọn
        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = UserTasksListView.SelectedItem as Todo;

            if (selectedTask == null)
            {
                MessageBox.Show("Vui lòng chọn một công việc để chỉnh sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (User.UserId != selectedTask.CreatedBy)
            {
                MessageBox.Show("Không thể chỉnh sửa nội dung công việc của Admin !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            UpdateTask updateTaskWindow = new UpdateTask(selectedTask, _todoServices, _categoryServices);

            if (updateTaskWindow.ShowDialog() == true)
            {
                LoadUserTasks();
            }
        }

        // Xóa công việc đã chọn
        private async void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = UserTasksListView.SelectedItem as Todo;

            if (selectedTask == null)
            {
                MessageBox.Show("Vui lòng chọn một công việc để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if ( User.UserId != selectedTask.CreatedBy)
            {
                MessageBox.Show("Không thể xóa công việc của Admin !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa công việc '{selectedTask.Title}'?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                await _todoServices.DeleteTodoAsync(selectedTask.Id);
                LoadUserTasks();
            }
        }

        // Đăng xuất người dùng
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ClearUserSession();
            OpenLoginWindow();
        }

        // Dọn dẹp thông tin người dùng
        private void ClearUserSession()
        {
            User = null;
            MessageBox.Show("Đã đăng xuất thành công.");
        }

        // Mở cửa sổ đăng nhập
        private void OpenLoginWindow()
        {
            var loginWindow = App.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Close();
        }
    }
}
