using System.ComponentModel;

namespace SalaryArea_Forms.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
       // Basic interface for MVVM
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}