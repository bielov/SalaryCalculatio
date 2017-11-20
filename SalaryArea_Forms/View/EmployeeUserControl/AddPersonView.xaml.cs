using SalaryArea_Forms.ViewModel;
using System.Windows.Controls;

namespace SalaryArea_Forms.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AddPersonView : UserControl
    {

        public AddPersonView ()
        {
            InitializeComponent();
            DataContext = new PersomViewModel();
           
        }

            }
}
