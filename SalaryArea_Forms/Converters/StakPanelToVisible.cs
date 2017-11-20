using SalaryArea_Forms.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SalaryArea_Forms.Converters
{
    public class StakPanelToVisible : IValueConverter
    {      
        public object Convert(object value, Type targetType, object parameter,
                            System.Globalization.CultureInfo culture)
        {
          
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
