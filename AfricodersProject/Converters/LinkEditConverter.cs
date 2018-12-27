using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AfricodersProject.AfricoderLinksModel;
namespace DataConverters
{
    class LinkEditConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            Datum datum = (Datum)value;

            Visibility result = (datum.user.id == datum.LoggedInID) ? Visibility.Visible : Visibility.Hidden;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
