using System.Windows.Markup;

namespace WpfApp.Infrastructure.Common
{
    [MarkupExtensionReturnType(typeof(int[]))]
    internal class StringToIntArray : MarkupExtension
    {
        [ConstructorArgument("Str")]
        public string Str {  get; set; }
        public char Separator { get; set; } = ';';
        public StringToIntArray() { }
        public StringToIntArray(string str) {  this.Str = str; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Str.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries)
                .DefaultIfEmpty()
                .Select(int.Parse)
                .ToArray();
        }
    }
}
