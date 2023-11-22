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
    [ValueConversion(typeof(double), typeof(double))]
    internal class RatioConverter : BaseConverter
    {
        [ConstructorArgument("K")] public double K { get; set; } = 1;

        public RatioConverter()
        {
        }

        public RatioConverter(double K) => this.K = K;


        public override object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            var x = System.Convert.ToDouble(value, culture);
            return x * K;
        }

        public override object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            var x = double.TryParse(value.ToString(), out double result)? result : default;
            return x / K;
        }
    }
}
