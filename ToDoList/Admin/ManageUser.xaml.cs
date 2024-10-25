using BusinessObject;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Services;

namespace ToDoList
{
    public partial class ManageUser : UserControl
    {
        private readonly IUserService _userService;
        List<User> users;

        public ManageUser()
        {
            InitializeComponent();
            _userService = (IUserService)App.ServiceProvider.GetService(typeof(IUserService));
            //users = new List<User>();

        }

        // Sự kiện khi UserControl được tải
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUser();
        }

        // Load danh sách các danh mục tác vụ
        private async void LoadUser()
        {
            // Lấy danh sách từ service
            List<User> users = await _userService.GetAllUsersAsync();

            // Gán danh sách vào DataGrid
            UserDataGrid.ItemsSource = users;
        }

        private async void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow(users, _userService);
            addUserWindow.ShowDialog();

            if (addUserWindow.DialogResult == true)
            {
                LoadUser();
            }
        }

        private async void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem != null)
            {
                User selectedUser = (User)UserDataGrid.SelectedItem;
                EditUserWindow editWindow = new EditUserWindow(selectedUser, _userService);

                if (editWindow.ShowDialog() == true)
                {
                    LoadUser(); 
                }
            }
            else
            {
                MessageBox.Show("Please select a user to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem != null)
            {
                User selectedUser = (User)UserDataGrid.SelectedItem;

                try
                {
                    await _userService.DeleteUserAsync(selectedUser.UserId);
                    users.Remove(selectedUser);
                    if (UserDataGrid != null)
                    {
                        UserDataGrid.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("UserDataGrid is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}




