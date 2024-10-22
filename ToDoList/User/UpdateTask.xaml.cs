using BusinessObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ToDoList
{
    public partial class UpdateTask
    {
        public Todo SelectedTodo { get; set; }
        public UpdateTask(Todo todo)
        {
            InitializeComponent();
            SelectedTodo = todo;

            // Populate fields with the selected task's data
            TitleTextBox.Text = SelectedTodo.Title;
            DescriptionTextbox.Text = SelectedTodo.Description;
            PeriodDatePicker.SelectedDate = SelectedTodo.DueDate.Date;
            HourComboBox.SelectedItem = SelectedTodo.DueDate.Hour.ToString();
            MinuteComboBox.SelectedItem = SelectedTodo.DueDate.Minute.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTodo.Title = TitleTextBox.Text;
            SelectedTodo.Description = DescriptionTextbox.Text;
            SelectedTodo.DueDate = PeriodDatePicker.SelectedDate.Value
                .AddHours(int.Parse(HourComboBox.SelectedItem.ToString()))
                .AddMinutes(int.Parse(MinuteComboBox.SelectedItem.ToString()));

            if (IsCompletedCheckBox.IsChecked == true)
            {
                SelectedTodo.IsCompleted = true;
            }

            // C?p nh?t danh sách Todos
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                // Xóa công vi?c c?
                mainWindow.Todos.Remove(SelectedTodo);

                // Thêm công vi?c ?ã c?p nh?t vào danh sách
                mainWindow.Todos.Add(SelectedTodo);

                // S?p x?p l?i danh sách Todos: công vi?c ?ã hoàn thành s? ???c ??y xu?ng d??i
                mainWindow.Todos = new ObservableCollection<Todo>(mainWindow.Todos.OrderBy(todo => todo.IsCompleted).ThenBy(todo => todo.DueDate));
            }

            MessageBox.Show("Công vi?c ?ã ???c c?p nh?t thành công!", "C?p nh?t thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();

            MessageBox.Show("Công vi?c ?ã ???c c?p nh?t thành công!", "C?p nh?t thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TitleTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
        private void DescriptionTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void HourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void MinuteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

    }
}
