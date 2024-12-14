using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApp.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(ToArray))]
    internal class ToArray : MultiConverter
    {
        public override object Convert(object[] values, Type targetType, object p, CultureInfo c)
        {
            var collection = new CompositeCollection();
            foreach (var item in values) 
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
