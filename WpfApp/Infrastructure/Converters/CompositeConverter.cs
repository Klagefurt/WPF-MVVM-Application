﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApp.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(CompositeConverter))]
    internal class CompositeConverter : Converter
    {
        [ConstructorArgument("First")]
        public IValueConverter First {  get; set; }
        [ConstructorArgument("Second")]
        public IValueConverter Second { get; set; }

        public CompositeConverter() { }

        public CompositeConverter(IValueConverter First) => this.First = First;
        public CompositeConverter(IValueConverter First, IValueConverter Second) : this(First) => this.Second = Second;


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
