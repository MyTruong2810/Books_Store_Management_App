using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace Books_Store_Management_App.Helpers
{

    public class StringFormatConverter : IValueConverter
    {
        /// <summary>
        /// Chuyển đổi hiển thị của một giá trị theo định dạng chuỗi được truyền vào
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && parameter != null)
            {
                return string.Format((string)parameter, value);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
