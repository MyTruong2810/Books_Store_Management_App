using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace Books_Store_Management_App.Helpers
{
    public class DoubleToUsdCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double doubleValue)
            {
                return doubleValue.ToString("C", CultureInfo.GetCultureInfo("en-US"));
            }
            return "$0.00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string strValue && double.TryParse(strValue, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out double result))
            {
                return result;
            }
            return 0.0;
        }
    }
}
