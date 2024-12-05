using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace Books_Store_Management_App.Helpers
{
    /// <summary>
    /// Lớp này triển khai giao diện IValueConverter, được sử dụng để chuyển đổi dữ liệu giữa nguồn (ViewModel) và đích (UI) trong một liên kết XAML.
    /// </summary>
    public class DateTimeToStringConverter : IValueConverter
    {
        // Phương thức Convert được gọi khi đánh giá liên kết dữ liệu.
        // Nó chuyển đổi giá trị DateTime thành chuỗi định dạng để hiển thị.
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Kiểm tra nếu giá trị là kiểu DateTime
            if (value is DateTime dateTime)
            {
                // Chuyển đổi DateTime thành chuỗi định dạng.
                // Định dạng sử dụng là "MM/dd/yyyy - dddd hh:mm tt", đại diện cho:
                // - MM/dd/yyyy cho ngày theo định dạng tháng/ngày/năm
                // - dddd cho tên đầy đủ của ngày trong tuần
                // - hh:mm tt cho thời gian theo định dạng 12 giờ với AM/PM
                return dateTime.ToString("MM/dd/yyyy - dddd hh:mm tt", CultureInfo.InvariantCulture);
            }
            return null; // Nếu giá trị không phải là DateTime, trả về null
        }

        // Phương thức ConvertBack không được triển khai vì bộ chuyển đổi này chỉ dành cho
        // chuyển đổi một chiều (từ DateTime sang String).
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Điều này không được triển khai vì không cần thiết trong trường hợp này.
            // Nếu cần chuyển đổi hai chiều (ví dụ: từ String sang DateTime),
            // bạn sẽ triển khai phương thức này.
            throw new NotImplementedException();
        }

        // Phương thức nội bộ này có vẻ không cần thiết vì nó không được sử dụng trong triển khai bộ chuyển đổi.
        // Nó dường như là một chỗ giữ chỗ cho một số chức năng khác có thể chưa được triển khai.
        internal string Convert(Func<string> toString)
        {
            throw new NotImplementedException();
        }
    }
}
