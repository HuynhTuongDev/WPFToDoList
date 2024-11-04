using System.Windows;
using System.Windows.Controls;
using ToDoList.Services;

namespace ToDoList
{
    public partial class ManageUser : UserControl
    {
        private readonly IUserService _userService;
        private List<User> users; // Khai báo biến users ở đây để tránh lặp lại

        public static readonly DependencyProperty RoleListProperty = DependencyProperty.Register(
            "RoleList", typeof(List<string>), typeof(ManageUser), new PropertyMetadata(new List<string> { "Admin", "User" }));

        public static readonly DependencyProperty StateListProperty = DependencyProperty.Register(
            "StateList", typeof(List<string>), typeof(ManageUser), new PropertyMetadata(new List<string> { "ACTIVE", "INACTIVE" }));

        public ManageUser()
        {
            InitializeComponent();
            _userService = (IUserService)App.ServiceProvider.GetService(typeof(IUserService));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUser();
        }

        private void LoadUser()
        {
            users = _userService.GetAllUsers();
            UserDataGrid.ItemsSource = users;
        }

        private async void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow(users, _userService);
            addUserWindow.ShowDialog();

            if (addUserWindow.DialogResult == true)
            {
                LoadUser(); // Làm mới danh sách sau khi thêm người dùng
            }
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem != null)
            {
                var selectedUser = (User)((ComboBox)sender).DataContext;

                if (selectedUser != null)
                {
                    try
                    {
                        _userService.UpdateUser(selectedUser);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _userService.DeleteUser(selectedUser.UserId);
                        LoadUser(); // Làm mới danh sách sau khi xóa người dùng
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
