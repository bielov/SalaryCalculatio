using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SalaryArea_Forms.Converters
{
    public class PersonToAbreviation : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // throw new NotImplementedException();
            string surname = values[0] as string;
            string FirstName = values[1] as string;
            string MidleName = values[2] as string;
           return  AbbreviationGenerator(surname, FirstName, MidleName);

        }
        private string AbbreviationGenerator(string Surname, string FirstName, string MidleName)
        {
           
            StringBuilder sb = new StringBuilder();
            sb.Append(Surname + " ");           
            sb.Append(FirstName[0] + ".");
            sb.Append(MidleName[0] + ".");
            return sb.ToString();

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
