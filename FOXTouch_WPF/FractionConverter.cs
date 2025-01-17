using System;
using System.Globalization;
using System.Windows.Data;


namespace FOXTouch_WPF
{
    public class FractionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width && double.TryParse(parameter.ToString(), out double fraction))
            {
                double result = width * fraction;
                System.Diagnostics.Debug.WriteLine($"Width: {width}, Fraction: {fraction}, Result: {result}");
                return result;
            }
            System.Diagnostics.Debug.WriteLine($"Invalid width or fraction: Width={value}, Fraction={parameter}");
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}