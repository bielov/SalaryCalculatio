using SalaryArea_Forms.ViewModel;
using System.Windows.Controls;

namespace SalaryArea_Forms.View
{
    /// <summary>
    /// Логика взаимодействия для Position.xaml
    /// </summary>
    public partial class AddPositionView : UserControl
    {
      
        public AddPositionView()
        {

            InitializeComponent();
            DataContext = new PositionViewModel();
         
        }
    }
}
