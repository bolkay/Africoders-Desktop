using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AfricodersProject.ForumModel;
namespace DataConverters
{
    class ForumCommentConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //AfricodersProject.ForumModel.Datum datum = (AfricodersProject.ForumModel.Datum)value;

            Datum2 datum = (Datum2)value;

            Visibility result = (datum.LoggedInID == datum.user.id) ? Visibility.Visible : Visibility.Hidden;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
