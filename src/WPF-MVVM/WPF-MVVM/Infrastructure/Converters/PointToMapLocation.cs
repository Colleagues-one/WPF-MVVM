using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using MapControl;

namespace WPF_MVVM.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(PointToMapLocation))]
    [ValueConversion(typeof(Point),typeof(Location))]
    internal class PointToMapLocation : BaseConverter
    {
        public override object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;
            return new Location(point.X, point.Y);
        }
        public override object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
           if(!(value is Location location)) return null;
           return new Point(location.Latitude, location.Longitude);
        }
    }
}
