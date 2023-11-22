using System;
using System.Collections.Generic;

using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPF_MVVM.Infrastructure.Converters
{
    [ValueConversion(typeof(Point),typeof(string))]
    internal class LocationPointToStr : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;
            return $"Lat:{point.X}, lon:{point.Y}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string str)) return null;

            string[] comp = str.Split(',');

            return new Point()
            {
                X = (double.TryParse(comp[0].Split(':')[1], CultureInfo.InvariantCulture, out double lat))
                    ? lat
                    : default,
                Y = (double.TryParse(comp[1].Split(':')[1], CultureInfo.InvariantCulture, out double lon))
                    ? lon
                    : default
            };
        }
    }
}

