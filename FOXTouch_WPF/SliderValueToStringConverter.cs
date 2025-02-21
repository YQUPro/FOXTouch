using System;
using System.Globalization;
using System.Windows.Data;

namespace FOXTouch_WPF
{
    public class SliderValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double sliderValue)
            {
                return $"{sliderValue} %";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text && text.EndsWith(" %"))
            {
                if (double.TryParse(text.Substring(0, text.Length - 2), out double result))
                {
                    return result;
                }
            }
            return 0.0;
        }
    }
}
