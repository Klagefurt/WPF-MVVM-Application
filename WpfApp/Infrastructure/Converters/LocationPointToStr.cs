using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp.Infrastructure.Converters
{
    [ValueConversion(typeof(Point), typeof(string))]
    internal class LocationPointToStr : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;
            return $"Lat:{point.X};Lon:{point.Y}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string str)) return null;

            var components = str.Split(';');

            var lat_str = components[0].Split(':')[1];
            var lon_str = components[1].Split(':')[1];

            return new Point(double.Parse(lat_str), double.Parse(lon_str));
        }
    }
}
