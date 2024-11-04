using System;
using System.Globalization;
using System.Windows.Data;

namespace ToDoList
{
    public class RoleConverter : IValueConverter
    {
        // Converts integer role to string representation
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int role)
            {
                return role == 1 ? "User" : "Admin"; // 1 -> User, 0 -> Admin
            }
            return "Unknown"; // Invalid value
        }

        // Converts string representation back to integer role
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string role)
            {
                return role switch
                {
                    "User" => 1, // User role
                    "Admin" => 0, // Admin role
                };
            }
            return -1; // If the value is not a string, return -1
        }
    }
}
