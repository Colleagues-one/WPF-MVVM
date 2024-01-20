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
    [MarkupExtensionReturnType(typeof(AddConverter))]
    internal class AddConverter : BaseConverter
    {
        [ConstructorArgument("B")] public double B { get; set; } = 1;

        public AddConverter()
        {
        }

        public AddConverter(double b) => this.B = b;


        public override object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            var x = System.Convert.ToDouble(value, culture);
            return x + B;
        }

        public override object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            var x = double.TryParse(value.ToString(), out double result) ? result : default;
            return x - B;
        }
    }
}

