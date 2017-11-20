using SalaryArea_V2._2.Context;
using SalaryArea_V2._2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SalaryArea_Forms.Converters
{
    class CollectionToDataTableConverter :  IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //    fullemployeetable = new ObservableCollection<Employee>;
            //    using (SalDBContext _dbContext = new SalDBContext())
            //    {
            //        varfullemployeetable =
            //    }
            //    return  _dbContext.Employees.ToList();  
            throw new NotImplementedException();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        
    }
 
    }