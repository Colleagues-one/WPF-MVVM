using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WPF_MVVM.Infrastructure.Common
{
    [MarkupExtensionReturnType(typeof(int[]))]
    internal class StringToIntArray :MarkupExtension
    {
        [ConstructorArgument("Str")]
        public string Str { get; set; }

        public const char Separator = ',';
        public StringToIntArray() { }
        public StringToIntArray(string str) {  Str = str; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Str.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries)
                .DefaultIfEmpty().Select(int.Parse).ToArray();
        }
    }
}
