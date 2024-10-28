using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Books_Store_Management_App.Helpers
{
    internal class StringToISBNConverter : IValueConverter
    {
        private static readonly Regex ISBN13Regex = new Regex(@"^\d{13}$", RegexOptions.Compiled);

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

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
