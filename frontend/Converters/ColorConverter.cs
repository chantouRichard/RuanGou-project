using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace frontend.Converters
{
    public class ColorConverter : IValueConverter
    {
        // 字符串 -> SolidColorBrush
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string colorString && !string.IsNullOrWhiteSpace(colorString))
            {
                try
                {
                    var converter = new BrushConverter();
                    return converter.ConvertFromString(colorString) as SolidColorBrush;
                }
                catch
                {
                    return Brushes.Black; // 默认颜色
                }
            }
            return Brushes.Black;
        }

        // SolidColorBrush -> 字符串（如果你想实现反向绑定）
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                return brush.Color.ToString();
            }
            return "#000000";
        }
    }
}
