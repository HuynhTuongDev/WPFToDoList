
using System.Globalization;
using System.Windows.Data;

namespace ToDoList
{
    public class RoleConverter : IValueConverter
    {
        // Converts integer role to string representation
        // 1 -> "User" (created by user), 0 -> "Admin" (created by admin)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int role)
            {
                return role == 1 ? "User" : "Admin"; // 1 means created by user, 0 means created by admin
            }
            return "Unknown"; // Return "Unknown" if value is invalid
        }

        // Converts string representation back to integer role
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string role)
            {
                return role switch
                {
                    "User" => 1, // User role maps to 1
                    "Admin" => 0, // Admin role maps to 0
                    _ => -1 // If the role is unrecognized, return -1
                };
            }
            return -1; // Return -1 if value is not a string
        }
    }
}
