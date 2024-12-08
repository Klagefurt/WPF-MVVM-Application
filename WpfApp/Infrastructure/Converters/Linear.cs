using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Infrastructure.Converters
{
    /// <summary>
    /// Implementing f(x) -> k * x + b
    /// </summary>
    internal class Linear : Converter
    {
        public double K { get; set; } = 1;
        public double B { get; set; }
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            if (v is null) return null;
            var x = System.Convert.ToDouble(v, c);
            return K * x + B;
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            if (v is null) return null;
            var y = System.Convert.ToDouble(v, c);

            return (y - B) / K;
        }
    }
}
