using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace Books_Store_Management_App.Helpers
{
    /// <summary>
    /// Lớp chuyển đổi giữa kiểu dữ liệu string và kiểu dữ liệu Visibility.
    /// </summary>
    public class StringToVisibableConverter : IValueConverter
    {
        // Converts a string to a Visibility value.
        // If the string is null or empty, returns Collapsed; otherwise, returns Visible.
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Check if the value is a string and if it's empty or null.
            return string.IsNullOrEmpty(value as string) ? Visibility.Collapsed : Visibility.Visible;
        }

        // Not implemented as the converter is only used for one-way data binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException(); // ConvertBack is not needed in this case.
        }
    }
}
