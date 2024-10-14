using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using ToDoList.Services;
namespace ToDoList
{
    public partial class LoginWindow : Window
    {
        private readonly IUserService userService;

        public LoginWindow(IUserService userService)
        {
            InitializeComponent();
            this.userService = userService;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = UserPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Email hoặc mật khẩu trống!", "Cần có trường", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = await userService.LoginUserAsync(email, password);
            if (user == null)
            {
                MessageBox.Show("Email hoặc mật khẩu không chính xác!", "Thông tin không chính xác", MessageBoxButton.OK);
                return;
            }

            Hide();
            if (user.Role == 0)
            {
                var adminWindow = new Admin();
                adminWindow.ShowDialog();
            }
            else if (user.Role == 1)
            {
                var mainWindow = new MainWindow { User = user };
                mainWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập hệ thống", "Không được phép", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = App.ServiceProvider.GetRequiredService<RegisterWindow>();
            registerWindow.Show();
            Close();
        }
    }
}
