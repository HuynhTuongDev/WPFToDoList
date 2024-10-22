using Microsoft.IdentityModel.Tokens;
using ToDoList.Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;

namespace ToDoList
{
    public partial class CreateTask : Window
    {
        public Todo NewTodo { get; set; }
        public CreateTask()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            NewTodo = new Todo
            {
                Title = TitleTextBox.Text,
                Description = DescriptionTextbox.Text,
                DueDate = PeriodDatePicker.SelectedDate ?? DateTime.Now,
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = 1,
                CategoryId = 1
            };

            DialogResult = true;
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TitleTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            
        }

        
    }
}
