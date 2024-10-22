using System.Globalization;
using System.Windows.Data;

namespace ToDoList
{
    public class BoolToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "✔" : "✘"; // "✔" cho hoàn thành và "✘" cho chưa hoàn thành
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == "✔";
        }
    }

}
