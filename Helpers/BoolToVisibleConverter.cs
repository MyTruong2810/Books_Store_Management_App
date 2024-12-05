using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Books_Store_Management_App.Helpers
{
    /// <summary>
    /// Lớp này triển khai giao diện IValueConverter, được sử dụng để chuyển đổi giá trị boolean thành giá trị Visibility. Nó thường được sử dụng để kiểm soát sự hiển thị của các phần tử UI dựa trên một điều kiện (true/false).
    /// </summary>
    public class BoolToVisibleConverter : IValueConverter
    {
        /// <summary>
        /// Phương thức Convert được gọi khi đánh giá liên kết dữ liệu.Nó chuyển đổi giá trị boolean thành giá trị Visibility (Visible hoặc Collapsed).
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Kiểm tra nếu giá trị đầu vào là kiểu bool
            if (value is bool b)
            {
                // Nếu giá trị boolean là true, trả về Visibility.Visible
                // Nếu giá trị boolean là false, trả về Visibility.Collapsed
                return b ? Visibility.Visible : Visibility.Collapsed;
            }

            // Nếu giá trị không phải là boolean, trả về Visibility.Collapsed mặc định
            return Visibility.Collapsed;
        }
        /// <summary>
        /// Phương thức ConvertBack được gọi khi liên kết dữ liệu bị đảo ngược.Nó chuyển đổi giá trị Visibility trở lại giá trị boolean(true/false).
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Kiểm tra nếu giá trị đầu vào là kiểu Visibility
            if (value is Visibility visibility)
            {
                // Nếu visibility là Visible, trả về true
                // Nếu visibility là Collapsed (hoặc Hidden), trả về false
                return visibility == Visibility.Visible;
            }

            // Nếu giá trị không phải là kiểu Visibility, trả về false mặc định
            return false;
        }
    }
}
