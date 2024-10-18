﻿using System;
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

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for ManageTask.xaml
    /// </summary>
    public partial class ManageTask : UserControl
    {
        public ManageTask()
        {
            InitializeComponent();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();
            addTaskWindow.ShowDialog();
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            EditTaskWindow editTaskWindow = new EditTaskWindow();   
            editTaskWindow.ShowDialog();
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
