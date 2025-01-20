using System.Globalization;
using System.Windows.Markup;

namespace WpfApp.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(Add))]
    internal class Add : Converter
    {
        [ConstructorArgument("K")]
        public double B { get; set; } = 1;

        public Add() { }

        public Add(double B) => this.B = B;

        public override object Convert(object value, Type t, object p, CultureInfo c)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, c);

            return x + B;
        }

        public override object ConvertBack(object value, Type t, object p, CultureInfo c)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, c);

            return x - B;
        }
    }
}
