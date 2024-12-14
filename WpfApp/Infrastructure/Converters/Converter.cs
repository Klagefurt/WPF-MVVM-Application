using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApp.Infrastructure.Converters
{
    internal abstract class Converter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public abstract object Convert(object v, Type t, object p, CultureInfo c);

        public virtual object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            throw new NotSupportedException("ConvertBack is not supported!");
        }
    }
}
