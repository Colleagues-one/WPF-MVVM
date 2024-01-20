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
    internal abstract class BaseConverter : MarkupExtension, IValueConverter 
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public abstract object Convert(object value, Type type, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Не поддерживается обратная конвертация.");
        }
    }

}
