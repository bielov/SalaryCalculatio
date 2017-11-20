using SalaryArea_Forms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalaryArea_Forms.View.InformationUserControl
{
    /// <summary>
    /// Логика взаимодействия для AddWageMinimum.xaml
    /// </summary>
    public partial class AddWageMinimum : UserControl
    {
        public AddWageMinimum()
        {
            InitializeComponent();
            DataContext = new WageMinimumViewModel();
        }
    }
}
