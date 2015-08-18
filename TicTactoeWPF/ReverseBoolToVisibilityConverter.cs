using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TicTactoeWPF
{
    public class ReverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var v = (bool)value;
                return v ? Visibility.Collapsed : Visibility.Visible;
            }
            catch (InvalidCastException)
            {
                return Visibility.Collapsed;
            }
        }
        // Not doing 2 way binding so no need for converting value back
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var v = (Visibility)value;
                if (v == Visibility.Visible)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
    }
}
