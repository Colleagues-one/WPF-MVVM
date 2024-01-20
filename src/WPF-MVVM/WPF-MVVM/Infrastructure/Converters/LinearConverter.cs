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
    [MarkupExtensionReturnType(typeof(LinearConverter))]//чтобы в Xaml были видны свойства
    internal class LinearConverter : BaseConverter
    {
        [ConstructorArgument("K")]
        public double K { get; set; } = 10;

        [ConstructorArgument("B")]
        public double B { get; set; } = 1;


        public LinearConverter() { }

        public LinearConverter(double K)=>this.K = K;
        
        public LinearConverter(double K, double B):this(K) => this.B = B;
        

        public override object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            if(value is null) return null;

            var x = System.Convert.ToDouble(value, culture);
            return K * x + B;
        }

        public override object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
           

            var y = double.TryParse(value?.ToString(), out double result) ? result : 0;
            return (y - B) / K;
        }
    }
}
