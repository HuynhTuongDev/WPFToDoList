using System;
using System.Globalization;
using System.Windows.Data;

namespace ToDoList
{
    public class StateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Logic để chuyển đổi trạng thái sang giá trị hiển thị
            return value; // Thay đổi logic nếu cần
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Logic để chuyển đổi ngược
            return value; // Thay đổi logic nếu cần
        }
    }
}
