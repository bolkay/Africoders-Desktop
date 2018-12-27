using AfricodersProject.AfricodersBlogModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DataConverters
{
    class BlogEditConverter:IValueConverter
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
