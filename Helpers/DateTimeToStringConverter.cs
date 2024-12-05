using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace Books_Store_Management_App.Helpers
{
    // This class implements the IValueConverter interface, which is used for converting data
    // between the source (ViewModel) and the target (UI) in a XAML binding. 
    public class DateTimeToStringConverter : IValueConverter
    {
        // The Convert method is called when the data binding is evaluated. 
        // It converts a DateTime value into a formatted string for display.
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Check if the value is of type DateTime
            if (value is DateTime dateTime)
            {
                // Convert the DateTime to a formatted string.
                // The format used is "MM/dd/yyyy - dddd hh:mm tt", which represents:
                // - MM/dd/yyyy for the date in month/day/year format
                // - dddd for the full name of the day of the week
                // - hh:mm tt for the time in 12-hour format with AM/PM
                return dateTime.ToString("MM/dd/yyyy - dddd hh:mm tt", CultureInfo.InvariantCulture);
            }
            return null; // If value is not a DateTime, return null
        }

        // The ConvertBack method is not implemented because this converter is only intended 
        // for one-way conversion (from DateTime to String). 
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // This is not implemented because it’s not needed in this case.
            // If bidirectional conversion was required (e.g., from String to DateTime), 
            // you would implement this method.
            throw new NotImplementedException();
        }

        // This internal method seems unnecessary as it is not used in the converter implementation.
        // It seems to be a placeholder for some other functionality that might not have been implemented yet.
        internal string Convert(Func<string> toString)
        {
            throw new NotImplementedException();
        }
    }
}
