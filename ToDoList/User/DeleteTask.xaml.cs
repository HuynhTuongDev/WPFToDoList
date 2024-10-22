using System;
using System.Windows;

namespace ToDoList
{
    public partial class DeleteTask
    {
        private readonly Action _deleteTaskAction;
        public DeleteTask(Action deleteTaskAction)
        {
            InitializeComponent();
            _deleteTaskAction = deleteTaskAction;
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            _deleteTaskAction?.Invoke();
            this.Close();
        }

        private void CancelTask_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
