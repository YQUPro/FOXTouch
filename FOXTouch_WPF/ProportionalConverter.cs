using System;
using System.Globalization;
using System.Windows.Data;

namespace FOXTouch_WPF
{
    public class ProportionalConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double width && double.TryParse(parameter.ToString(), out double fraction))
            {
                return width * fraction;
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}