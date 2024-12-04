using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace Books_Store_Management_App.Helpers
{
    // This class implements the IValueConverter interface, which is used for converting a boolean value to a Visibility value.
    // It's typically used for controlling the visibility of UI elements based on some condition (true/false).
    public class BoolToVisibleConverter : IValueConverter
    {
        // The Convert method is called when the data binding is evaluated.
        // It converts a boolean value to a Visibility value (Visible or Collapsed).
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Check if the incoming value is of type bool
            if (value is bool b)
            {
                // If the boolean value is true, return Visibility.Visible
                // If the boolean value is false, return Visibility.Collapsed
                return b ? Visibility.Visible : Visibility.Collapsed;
            }

            // If the value is not a boolean, return Visibility.Collapsed by default
            return Visibility.Collapsed;
        }

        // The ConvertBack method is called when the data binding is reversed.
        // It converts a Visibility value back to a boolean value (true/false).
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Check if the incoming value is of type Visibility
            if (value is Visibility visibility)
            {
                // If the visibility is Visible, return true
                // If the visibility is Collapsed (or Hidden), return false
                return visibility == Visibility.Visible;
            }

            // If the value is not of type Visibility, return false by default
            return false;
        }
    }
}
