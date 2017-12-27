using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UI.Desktop.ValueConverter
{
    class IntToVisibilityConverter : IValueConverter
    {

        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public IntToVisibilityConverter() { }
        #endregion

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int bValue = (int)value;
            if (bValue > 0)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return 1;
            else
                return 0;
        }
        #endregion
    }
    
}
