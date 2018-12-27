using AfricodersProject.AfricoderModels;
using AfricodersProject.LoginService;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Linq;
namespace DataConverters
{
    /// <summary>
    /// The user can only edit posts made by him or her.
    /// </summary>
    class EditVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            Datum datum = (Datum) value;

            Visibility result = (datum.user.id == datum.LoggedInID) ? Visibility.Visible : Visibility.Hidden;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
