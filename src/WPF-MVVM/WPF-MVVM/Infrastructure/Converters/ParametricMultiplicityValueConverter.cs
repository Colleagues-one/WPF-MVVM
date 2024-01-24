using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPF_MVVM.Infrastructure.Converters
{
    internal class ParametricMultiplicityValueConverter : Freezable, IValueConverter
    {
        #region Value Dependency Property : double - Прибавляемое значение

        /// <summary>
        /// Прибавляемое значение Property Register
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(ParametricMultiplicityValueConverter),
                new PropertyMetadata(1.0,(d, e)=>{}));


        /// <summary>
        /// Прибавляемое значение Property 
        /// </summary>
        //[Category("")]
        [Description("Прибавляемое значение")]
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion 

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value, culture) * Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value, culture) / Value;
        }

        protected override Freezable CreateInstanceCore()
        {
           return new ParametricMultiplicityValueConverter() { Value = Value };
        }
    }
}
