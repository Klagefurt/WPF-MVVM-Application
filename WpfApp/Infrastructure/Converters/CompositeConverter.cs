﻿using System.Globalization;
using System.Windows.Data;

namespace WpfApp.Infrastructure.Converters
{
    internal class CompositeConverter : Converter
    {
        public IValueConverter First {  get; set; }
        public IValueConverter Second { get; set; } 


        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var result1 = First?.Convert(v, t, p, c) ?? v;
            var result2 = Second?.Convert(result1, t, p, c) ?? result1;
            return result2;
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            var result1 = Second?.ConvertBack(v, t, p, c) ?? v;
            var result2 = First?.ConvertBack(result1, t, null, c) ?? result1;
            return result2;
        }
    }
}
