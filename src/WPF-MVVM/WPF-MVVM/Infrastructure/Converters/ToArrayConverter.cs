using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_MVVM.Infrastructure.Converters
{
    internal class ToArrayConverter : MultiConverter
    {
        public ToArrayConverter() { }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = new CompositeCollection();
            foreach (var item in values)
            {
                collection.Add(item);
            }

            return collection;
        }

      /*  public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return value as object[];
        }*/
    }
}
