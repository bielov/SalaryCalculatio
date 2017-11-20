using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.Logic;
using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SalaryArea_Forms.ViewModel
{
    public class WageMinimumViewModel : ViewModelBase
    {
        LivingWageMin _theWage;
        TimePeriod _theMonth;
        WageLogic _wagelogic;
        SalDbContext _db;
        public WageMinimumViewModel()
        {
            _db = new SalDbContext();
            _wagelogic = new WageLogic();
            TheWage = new LivingWageMin();
            TheMonth = new TimePeriod();
            PeriodCollection = new ObservableCollection<TimePeriod>(_wagelogic.GetPC());     
            WageCollection = new ObservableCollection<LivingWageMin>(_wagelogic.Get());
           

        }
        public LivingWageMin TheWage
        {
            get
            {
                return _theWage;
            }
            set
            {
                _theWage = value;
                OnPropertyChanged("TheWage");
            }            
        }
        private ObservableCollection<LivingWageMin> _wageCollection;
        public ObservableCollection<LivingWageMin> WageCollection
        {
            get
            {
                return _wageCollection;
            }
            set
            {
                  _wageCollection= value;
                OnPropertyChanged("WageCollection");
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
        public TimePeriod TheMonth
        {
            get
            {
                return _theMonth;
            }
            set
            {
                _theMonth = value;
                OnPropertyChanged("TheMonth");
            }
        }


        private bool _wageStackPanelVis;
        private bool _updateWageVis;


        public bool UpdateWageVis
        {
            get
            {
                return _updateWageVis;
            }

            set
            {
                if (value == _updateWageVis) return;
                _updateWageVis = value;
                OnPropertyChanged("UpdateWageVis");
            }
        }
        private bool _addWageVis;
        public bool AddWageVis
        {
            get
            {
                return _addWageVis;
            }

            set
            {
                if (value == _addWageVis) return;
                _addWageVis = value;
                OnPropertyChanged("AddWageVis");
            }
        }
        public bool WageStackPanelVis
        {
            get
            {
                return _wageStackPanelVis;
            }

            set
            {
                _wageStackPanelVis = value;
                OnPropertyChanged("WageStackPanelVis");
            }
        }

        public RelayCommand Update
        {
            get
            {
                return new RelayCommand(UpdateWage, true);
            }
        }
        public RelayCommand Delete
        {
            get
            {
                return new RelayCommand(DeleteWage, true);
            }
        }
        public RelayCommand AddWage
        {
            get
            {
                return new RelayCommand(Add, true);
            }
        }
        public RelayCommand SetAddingTableButton
        {
            get
            {
                return new RelayCommand(SetAddingProperties, true);
            }
        }


        public RelayCommand SetUpdateTableButton
        {
            get
            {
                return new RelayCommand(SetUpdateProperties, true);
            }
        }


        public RelayCommand Cancel
        {
            get
            {
                return new RelayCommand(ClearProperties, true);
            }
        }

       

        private void ClearProperties()
        {
            TheWage = null;
            WageStackPanelVis = false;
        }
        private void Add()
        {
            TheWage.PeriodId = TheMonth.PeriodID;        
            _wagelogic.Add(TheWage);
            RefreshCollection();
        }

        private void UpdateWage()
        {
            _wagelogic.Update(TheWage);
            RefreshCollection();
            ClearProperties();
        }
        private void DeleteWage()
        {
            _wagelogic.Delete(TheWage);
            RefreshCollection();

        }

        private void RefreshCollection()
        {
            WageCollection = new ObservableCollection<LivingWageMin>(_wagelogic.Get());
            PeriodCollection = new ObservableCollection<TimePeriod>(_wagelogic.GetPC());

        }

        private void SetAddingProperties()
        {
            TheWage = new LivingWageMin();
            TheMonth = new TimePeriod();
            WageStackPanelVis = true;
            UpdateWageVis = false;

        }
        private void SetUpdateProperties()
        {
            if (TheWage != null)
            {
                WageStackPanelVis = true;
                UpdateWageVis = true;
            }
            else
            {
                MessageBox.Show("Для того щоб оновити потрібно вибрати елемент", "Помилка");
            }
        }



    }
}
