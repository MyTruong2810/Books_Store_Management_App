using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Helpers
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("MM/dd/yyyy - dddd hh:mm tt", CultureInfo.InvariantCulture);
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
