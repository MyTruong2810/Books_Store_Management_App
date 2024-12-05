using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Books_Store_Management_App.Helpers
{
    /// <summary>
    /// Lớp chuyển dữ liệu 13 chữ số thành định dạng ISBN-13.
    /// </summary>
    internal class StringToISBNConverter : IValueConverter
    {
        private static readonly Regex ISBN13Regex = new Regex(@"^\d{13}$", RegexOptions.Compiled);

        /// <summary>
        /// Chuyển dữ liệu từ chuỗi 13 chữ số sang định dạng ISBN-13.
        /// </summary>
        /// <param name="value">Chuỗi cần định dạng</param>
        /// <param name="targetType">Định dạng cần đổi</param>
        /// <param name="parameter">Đối số không sử dụng</param>
        /// <param name="language">Đối số không sử dụng</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string stringValue = (string)value;
            if (stringValue.Length != 13 || !long.TryParse(stringValue, out _))
            {
                throw new ArgumentException("Invalid ISBN-13 format.");
            }

            string isbn13 = stringValue;

            // Format the ISBN into the custom format
            return $"{isbn13.Substring(0, 3)}-{isbn13.Substring(3, 3)}-{isbn13.Substring(6, 2)}-{isbn13.Substring(8, 4)}-{isbn13.Substring(12, 1)}";
        }
        /// <summary>
        /// Hàm chuyển ngược lại định dạng ban đầu, chưa sử dụng.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
