using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_MVVM.Infrastructure.Converters
{
    internal class CompositeConverter : BaseConverter
    {
        public IValueConverter FirstConverter { get; set; }
        public IValueConverter SecondConverter { get; set; }

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
