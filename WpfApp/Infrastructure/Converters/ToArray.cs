using System.Globalization;
using System.Windows.Data;

namespace WpfApp.Infrastructure.Converters
{
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
