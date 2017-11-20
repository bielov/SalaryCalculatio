using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea_Forms.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            EmoloyeeViewVisible = false;
            InformationViewVisible = false;
            SalaryCalculationVisible = false;

        }
        //EmoloyeeViewVisible SalaryCalculationVisible
        private bool _emoloyeeViewVisible;
        public bool EmoloyeeViewVisible
        {
            get
            {
                return _emoloyeeViewVisible;
            }

            set
            {

                _emoloyeeViewVisible = value;
                OnPropertyChanged("EmoloyeeViewVisible");
            }
        }
        private bool _informationViewVisible;
        public bool InformationViewVisible
        {
            get
            {
                return _informationViewVisible;
            }

            set
            {

                _informationViewVisible = value;
                OnPropertyChanged("InformationViewVisible");
            }
        }
        private bool _salaryCalculationVisible;
        public bool SalaryCalculationVisible
        {
            get
            {
                return _salaryCalculationVisible;
            }

            set
            {

                _salaryCalculationVisible = value;
                OnPropertyChanged("SalaryCalculationVisible");
            }
        }



        public RelayCommand NewSalaryCalculation
        {
            get
            {
                return new RelayCommand(OpenNewSal, true);
            }
        }
        public RelayCommand ShowSalaryCalculation
        {
            get
            {
                return new RelayCommand(OpenSalList, true);
            }
        }
        // GeneralEmployeeView ShowSalaryCalculation
        public RelayCommand OpenGeneralEmployeeView
        {
            get
            {
                return new RelayCommand(OpenGenEmployee, true);
            }
        }
        // OpenGeneralinformationView

        public RelayCommand OpenGeneralInformationView
        {
            get
            {
                return new RelayCommand(OpenGenInformation, true);
            }
        }

        private void OpenGenInformation()
        {
            EmoloyeeViewVisible = false;
            SalaryCalculationVisible = false;
            InformationViewVisible = true;
        }

        private void OpenGenEmployee()
        {
            InformationViewVisible = false;
            SalaryCalculationVisible = false;
            EmoloyeeViewVisible = true;
          
        }


        private void OpenSalList()
        {
            SalaryCalculationResultView salcalres = new SalaryCalculationResultView();
            salcalres.Show();
        }

        private void OpenNewSal()
        {
           
            EmoloyeeViewVisible = false;
            InformationViewVisible = false;
            SalaryCalculationVisible = true;
        }

    }
}
