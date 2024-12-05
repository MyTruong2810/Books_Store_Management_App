using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Helpers
{
    /// <summary>
    /// Chuyển đổi số thực thành định dạng phần trăm.
    /// </summary>
    public class DoubleToPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double d)
            {
                return $"{d:P}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // If needed, implement the reverse conversion logic here
            throw new NotImplementedException();
        }

        internal string Convert(Func<string> toString)
        {
            throw new NotImplementedException();
        }
    }

}
