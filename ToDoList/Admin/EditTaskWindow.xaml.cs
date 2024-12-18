﻿using BusinessObject;
using DataAccessLayer;
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

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public Todo editTodo { get; set; } = null;

        public EditTaskWindow()
        {
            InitializeComponent();
        }

        private void btnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            if (editTodo != null)
            {
                editTodo.Title = txtTitle.Text;
                editTodo.Description = txtDescription.Text;
                editTodo.DueDate = (DateTime)dpDueDate.SelectedDate;
                editTodo.IsCompleted = (bool)chkIsCompleted.IsChecked;
                editTodo.UpdatedAt = DateTime.Now;
                UpdateTodoInDataSource(editTodo);

                MessageBox.Show("Task updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No task to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void FillElement(Todo todo)
        {
            if (todo == null)
            {
                return;
            }
            txtTitle.Text = todo.Title;
            txtDescription.Text = todo.Description;
            dpDueDate.SelectedDate = todo.DueDate;
            chkIsCompleted.IsChecked = todo.IsCompleted;
        }

        private void UpdateTodoInDataSource(Todo todo)
        {
            using (var context = new TaskManagementContext()) // Thay YourDbContext bằng tên ngữ cảnh của bạn
            {
                var existingTodo = context.Tasks.Find(todo.Id);
                if (existingTodo != null)
                {
                    existingTodo.Title = todo.Title;
                    existingTodo.Description = todo.Description;
                    existingTodo.DueDate = todo.DueDate;
                    existingTodo.IsCompleted = todo.IsCompleted;
                    existingTodo.UpdatedAt = todo.UpdatedAt;

                    context.SaveChanges();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillElement(editTodo);
        }
    }
}