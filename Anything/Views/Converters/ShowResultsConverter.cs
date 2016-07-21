using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Anything.Views.Converters
{
    public class ShowResultsConverter : MarkupExtension, IValueConverter
    {
        public ShowResultsConverter()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return GridLength.Auto;
            }
            else
            {
                return new GridLength(0, GridUnitType.Pixel);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((GridLength) value).IsAuto;
        }
    }
}
