using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPF_MVVM.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(CompositeConverter))]//чтобы в Xaml были видны свойства
    internal class CompositeConverter : BaseConverter
    {
        [ConstructorArgument("First")]
        public IValueConverter FirstConverter { get; set; }
        [ConstructorArgument("Second")]
        public IValueConverter SecondConverter { get; set; }

        public CompositeConverter()
        {
        }
        public CompositeConverter(IValueConverter First) => FirstConverter = First;
        
        public CompositeConverter(IValueConverter First, IValueConverter Second):this(First) => SecondConverter = Second;
        public override object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            var result1 = FirstConverter?.Convert(value, type, parameter, culture) ?? value;
            var result2 = SecondConverter?.Convert(result1, type, parameter, culture) ?? result1;
            return result2;
        }

        public override object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            var result2 = SecondConverter?.ConvertBack(value, type, parameter, culture) ?? value;
            var result1 = FirstConverter?.ConvertBack(result2, type, parameter, culture) ?? result2;
            return result1;
        }
    }

}
