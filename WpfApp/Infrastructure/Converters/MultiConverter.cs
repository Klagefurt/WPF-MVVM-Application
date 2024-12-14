using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApp.Infrastructure.Converters
{
    internal abstract class MultiConverter : IMultiValueConverter
    {
        public abstract object Convert(object[] values, Type targetType, object p, CultureInfo c);

        public virtual object[] ConvertBack(object value, Type[] targetTypes, object p, CultureInfo c)
        {
            throw new NotSupportedException("ConvertBAck method is not implemented");
        }
    }
}
