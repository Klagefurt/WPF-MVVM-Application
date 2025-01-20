using MapControl;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApp.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(PointToMapLocation))]
    [ValueConversion(typeof(Point), typeof(Location))]
    internal class PointToMapLocation : Converter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            if (!(v is Point point)) return null;
            return new Location(point.X, point.Y);
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            if (!(v is Location location)) return null;
            return new Location(location.Latitude, location.Longitude);
        }
    }

}
