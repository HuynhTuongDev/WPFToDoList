using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        public User User { get; set; }
        public ObservableCollection<Todo> Todos { get; set; }
        public ObservableCollection<Todo> DoneTodos { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Todos = new ObservableCollection<Todo>();
            DoneTodos = new ObservableCollection<Todo>();
            UserTasksListView.ItemsSource = Todos;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();

            var filteredTodos = Todos.Where(todo =>
                todo.Title.ToLower().Contains(keyword) ||
                todo.Description.ToLower().Contains(keyword)) 
                .OrderBy(todo => todo.IsCompleted)
                .ThenBy(todo => todo.DueDate)
                .ToList();

            UserTasksListView.ItemsSource = filteredTodos;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();

            var filteredTodos = Todos.Where(todo =>
                todo.Title.ToLower().Contains(keyword) ||
                todo.Description.ToLower().Contains(keyword))
                .OrderBy(todo => todo.IsCompleted)
                .ThenBy(todo => todo.DueDate)
                .ToList();

            UserTasksListView.ItemsSource = filteredTodos;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTask createTaskWindow = new CreateTask();
            if (createTaskWindow.ShowDialog() == true)
            {
                var newTodo = createTaskWindow.NewTodo;

                Todos.Add(newTodo);
            }
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTodo = (Todo)UserTasksListView.SelectedItem;

            if (selectedTodo != null)
            {
                UpdateTask updateTask = new UpdateTask(selectedTodo);
                if (updateTask.ShowDialog() == true)
                {
                    UserTasksListView.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một công việc để chỉnh sửa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTodo = (Todo)UserTasksListView.SelectedItem;

            if (selectedTodo != null)
            {
                DeleteTask deleteTask = new DeleteTask(() =>
                {
                    Todos.Remove(selectedTodo);
                    MessageBox.Show($"Công việc '{selectedTodo.Title}' đã được xóa.", "Xóa Thành Công");
                });

                deleteTask.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một công việc để xóa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UserTasksListView_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UserTasksListView.ItemsSource = Todos.OrderBy(todo => todo.IsCompleted).ThenBy(todo => todo.DueDate).ToList();
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
