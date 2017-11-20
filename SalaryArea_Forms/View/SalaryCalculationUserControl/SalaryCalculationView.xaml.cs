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

namespace SalaryArea_Forms.View
{
    /// <summary>
    /// Логика взаимодействия для SalaryCalculationView.xaml
    /// </summary>
    public partial class SalaryCalculationView : UserControl
    {
        public SalaryCalculationView()
        {
            InitializeComponent();
            DataContext = new SalaryCalculationViewModel();
        }

    
    }
}
