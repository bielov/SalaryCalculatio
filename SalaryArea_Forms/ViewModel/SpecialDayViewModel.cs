using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.Logic;
using SalaryArea3._2.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace SalaryArea_Forms.ViewModel
{
    public class SpecialDayViewModel :ViewModelBase
    {
        SpecialDayLogic _specLogic;
        SpecialDay _theSpecialDay;
        DayType _theDayType;
        public SpecialDayViewModel()
        {
            _specLogic = new SpecialDayLogic();
            TheSpecialDay = new SpecialDay();
            TheMonth = new TimePeriod();
            TheDayType = new DayType();
            SpecialDayCollection = new ObservableCollection<SpecialDay>(_specLogic.GetSP());
            DayTypeCollection = new ObservableCollection<DayType>(_specLogic.GetDT());
            PeriodCollection = new ObservableCollection<TimePeriod>(_specLogic.GetPC());            
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
        private TimePeriod _theMonth;
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
        public SpecialDay TheSpecialDay
        {
            get
            {
                return _theSpecialDay;
            }

            set
            {
                _theSpecialDay = value;
                OnPropertyChanged("TheSpecialDay");
            }
        }
        public DayType TheDayType
        {
            get
            {
                return _theDayType;
            }

            set
            {
                _theDayType = value;
                OnPropertyChanged("TheDayType");
            }
        }
        private ObservableCollection<SpecialDay> _specialDayCollection;
        public ObservableCollection<SpecialDay> SpecialDayCollection
        {
            get
            {
                return _specialDayCollection;
            }

            set
            {
                _specialDayCollection = value;
                OnPropertyChanged("SpecialDayCollection");
            }
        }
        private ObservableCollection<DayType> _dayTypeCollection;
        public ObservableCollection<DayType> DayTypeCollection
        {
            get
            {
                return _dayTypeCollection;
            }

            set
            {
                _dayTypeCollection = value;
                OnPropertyChanged("DayTypeCollection");
            }
        }

        private bool _SDStackPanelVis;
        private bool _updateSDVis;
        public bool UpdateSDVis
        {
            get
            {
                return _updateSDVis;
            }

            set
            {
                if (value == _updateSDVis) return;
                _updateSDVis = value;
                OnPropertyChanged("UpdateSDVis");
            }
        }
        private bool _addSDVis;
        public bool AddSDVis
        {
            get
            {
                return _addSDVis;
            }

            set
            {
                if (value == _addSDVis) return;
                _addSDVis = value;
                OnPropertyChanged("AddSDVis");
            }
        }
        public bool SDStackPanelVis
        {
            get
            {
                return _SDStackPanelVis;
            }

            set
            {
                _SDStackPanelVis = value;
                OnPropertyChanged("SDStackPanelVis");
            }
        }

        public RelayCommand Update
        {
            get
            {
                return new RelayCommand(UpdateSD, true);
            }
        }
        public RelayCommand Add
        {
            get
            {
                return new RelayCommand(AddSD, true);
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
            SDStackPanelVis = false;
            TheSpecialDay = new SpecialDay();
            TheMonth = new TimePeriod();
            TheDayType = new DayType();
        }
        private void AddSD()
        {
          
            TheSpecialDay.DayTypeId = TheDayType.DayTypeID;    
            TheSpecialDay.PeriodId = TheMonth.PeriodID;         
            _specLogic.Add(TheSpecialDay);
            RefreshCollection();
            ClearProperties();
        }

        private void UpdateSD()
        {
            TheSpecialDay.DayTypeId = TheDayType.DayTypeID;
            TheSpecialDay.daytype = TheDayType;
            _specLogic.Update(TheSpecialDay);
            RefreshCollection();
            ClearProperties();
        }

        private void RefreshCollection()
        {
            SpecialDayCollection = new ObservableCollection<SpecialDay>(_specLogic.GetSP());
            DayTypeCollection = new ObservableCollection<DayType>(_specLogic.GetDT());
            PeriodCollection = new ObservableCollection<TimePeriod>(_specLogic.GetPC());
        }
        private void SetAddingProperties()
        {
            TheMonth = new TimePeriod();
            TheSpecialDay = new SpecialDay();
            TheDayType = new DayType();
            SDStackPanelVis = true;
            UpdateSDVis = false;
        }
        private void SetUpdateProperties()
        {
            if (TheSpecialDay != null)
            {
                SDStackPanelVis = true;
                UpdateSDVis = true;
            }
            else
            {
                MessageBox.Show("Для того щоб оновити потрібно вибрати елемент", "Помилка");
            }
        }


    }
}
