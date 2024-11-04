using BusinessObject;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoList.Repositories;

namespace ToDoList
{
    /// <summary>  
    /// Interaction logic for ManageTask.xaml  
    /// </summary>  
    public partial class ManageTask : UserControl
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly int _userId; // Add this line to declare _userId

        //public Todo editTodo { get; set; } = null;

        public ManageTask(ITodoRepository todoRepository, ICategoryRepository categoryRepository, int userId,IUserRepository userRepository) // Add userId parameter
        {
            _todoRepository = todoRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _userId = userId; 
            InitializeComponent();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var addTaskWindow = new AddTaskWindow(_userId, _todoRepository, _categoryRepository, _userRepository);
            addTaskWindow.ShowDialog();
            FillTaskDataGrid();
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Todo? selected = TaskDataGrid.SelectedItem as Todo;
            if (selected == null)
            {
                MessageBox.Show("Please select row/ an task before editing", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                var editTaskWindow = new EditTaskWindow();
                editTaskWindow.editTodo = selected;
                editTaskWindow.ShowDialog();
                FillTaskDataGrid();
            }
        }

        private async void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskDataGrid.SelectedItem is Todo selectedTodo)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the task '{selectedTodo.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _todoRepository.DeleteTodoAsync(selectedTodo.Id);
                    FillTaskDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "No Task Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TaskDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public async void FillTaskDataGrid()
        {
            TaskDataGrid.ItemsSource = null;
            var Todos = await _todoRepository.GetTodosAsync();
            TaskDataGrid.ItemsSource = Todos;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillTaskDataGrid();
        }
    }
}
