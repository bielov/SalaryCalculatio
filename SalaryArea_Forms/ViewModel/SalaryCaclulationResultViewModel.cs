using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.Logic;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea_Forms.ViewModel
{
   public class SalaryCaclulationResultViewModel :ViewModelBase
    {
        SalaryCalculationLogic _salcalLogic;
        public SalaryCaclulationResultViewModel()
        {
            _salcalLogic = new SalaryCalculationLogic();
            TheSalaryCalculation = new SalaryCalculation();
            ThePeriod = new TimePeriod();
            TheEmployee = new Employee();
            EmployeeCollection = new ObservableCollection<Employee>(_salcalLogic.GetEmployee());
            PeriodCollection = new ObservableCollection<TimePeriod>(_salcalLogic.GetPC());          
            SalaryCalculationCollection = new ObservableCollection<SalaryCalculation>();
        }
        private SalaryCalculation _theSalaryCalculation;
        public SalaryCalculation TheSalaryCalculation
        {
            get
            {
                return _theSalaryCalculation;
            }

            set
            {
                _theSalaryCalculation = value;
                OnPropertyChanged("TheSalaryCalculation");
            }
        }
        private ObservableCollection<SalaryCalculation> _salarycalculationCollection;
        public ObservableCollection<SalaryCalculation> SalaryCalculationCollection
        {
            get
            {
                return _salarycalculationCollection;
            }

            set
            {
                    _salarycalculationCollection = value;
                OnPropertyChanged("SalaryCalculationCollection");
            }
        }
        private TimePeriod _thePeriod;
        public TimePeriod ThePeriod
        {
            get
            {
                return _thePeriod;
            }

            set
            {
                _thePeriod = value;
                OnPropertyChanged("ThePeriod");
            }
        }
        private ObservableCollection<TimePeriod> _periodCollection;
        public ObservableCollection<TimePeriod> PeriodCollection
        {
            get
            {
                return _periodCollection;
            }

            set
            {
                _periodCollection = value;
                OnPropertyChanged("PeriodCollection");

            }
        }
        private Employee _theEmployee;
        public Employee TheEmployee
        {
            get
            {
                return _theEmployee;
            }

            set
            {
                _theEmployee = value;
                OnPropertyChanged("TheEmployee");
            }
        }
        private ObservableCollection<Employee> _employeeCollection;
        public ObservableCollection<Employee> EmployeeCollection
        {
            get
            {
                return _employeeCollection;
            }

            set
            {
                _employeeCollection = value;
                OnPropertyChanged("EmployeeCollection");

            }
        }

        private bool _setEmployeeSearch;
        public bool SetEmployeeSearch
        {
            get
            {
                return _setEmployeeSearch;
            }

            set
            {
                _setEmployeeSearch = value;
                OnPropertyChanged("SetEmployeeSearch");
            }
        }
        private bool _setDateSearch;
        public bool SetDateSearch
        {
            get
            {
                return _setDateSearch;
            }

            set
            {
                _setDateSearch = value;
                OnPropertyChanged("SetDateSearch");
            }
        }
        private bool _setSalrySearch;
        public bool SetSalrySearch
        {
            get
            {
                return _setSalrySearch;
            }

            set
            {
                _setSalrySearch = value;
                OnPropertyChanged("SetSalrySearch");
            }
        }
        private bool _showSalaryCollectionCollection;
        public bool ShowSalaryCollectionCollection
        {
            get
            {
                return _showSalaryCollectionCollection;
            }

            set
            {
                _showSalaryCollectionCollection = value;
                OnPropertyChanged("ShowSalaryCollectionCollection");
            }
        }
        private string _textHeaderResult;

        public string TextHeaderResult
        {
            get
            {
                return _textHeaderResult;
            }

            set
            {
                _textHeaderResult = value;
                OnPropertyChanged("TextHeaderResult");
            }
        }
        private int _navigationYear;
        public int NavigationYear
        {
            get
            {
                return _navigationYear;
            }

            set
            {

                _navigationYear = value;
                OnPropertyChanged("NavigationYear");
            }
        }
        private decimal _theSalary;
        public decimal TheSalary
        {
            get
            {
                return _theSalary;
            }

            set
            {

                _theSalary = value;
                OnPropertyChanged("TheSalary");
            }
        }
       
        public RelayCommand ShowEmployeeSearchProperties
        {
            get
            {
                return new RelayCommand(SetEmployeeSearchProperties, true);
            }
        }
        public RelayCommand  ShowDateSearchProperties
        {
            get
            {
                return new RelayCommand(SetDateSearchProperties, true);
            }
        }
        public RelayCommand ShowSalarySearchProperties
        {
            get
            {
                return new RelayCommand(SetSalarySearchProperties, true);
            }
        }       

        public RelayCommand ShowDataGridProperties
        {
            get
            {
                return new RelayCommand(SetDataGridProperties, true);
            }
        }

        private void SetDataGridProperties()
        {
            if(SetDateSearch == true)
            {
                SalaryCalculationCollection = new ObservableCollection<SalaryCalculation>(_salcalLogic.GetSalaryWithPar(ThePeriod, NavigationYear));
                ShowSalaryCollectionCollection = true;
            }
            else if(SetEmployeeSearch == true)
            {
                SalaryCalculationCollection = new ObservableCollection<SalaryCalculation>(_salcalLogic.GetSalarybyName(TheEmployee));
                ShowSalaryCollectionCollection = true;
            }
            else if(SetSalrySearch == true)
            {
                SalaryCalculationCollection = new ObservableCollection<SalaryCalculation>(_salcalLogic.GetSalarybySalary(TheSalary));
                ShowSalaryCollectionCollection = true;
            }
            }

        private void SetEmployeeSearchProperties()
        {
            TextHeaderResult = "Перелік працівників за їхнім ПІБ";
            SetDateSearch = false;
            SetSalrySearch = false;
            ShowSalaryCollectionCollection = false;
            SetEmployeeSearch = true;
        }

        private void SetDateSearchProperties()
        {
            TextHeaderResult = "Перелік заробітніх плат за датою";
            SetSalrySearch = false;
            ShowSalaryCollectionCollection = false;
            SetEmployeeSearch = false;
            SetDateSearch = true;
        }
        private void SetSalarySearchProperties()
        {
            TextHeaderResult = "Перелік заробітніх плат за розміром зп";
            ShowSalaryCollectionCollection = false;
            SetEmployeeSearch = false;
            SetDateSearch = false;
            SetSalrySearch = true;
        }
    }
}
