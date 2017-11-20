using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.Logic;
using SalaryArea3._2.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace SalaryArea_Forms.ViewModel
{
    public class DayTypeViewModel :ViewModelBase
    {
        DayType _theDayType;
        DayTypeLogic _dtlogic;
        public DayTypeViewModel()
        {
            _dtlogic = new DayTypeLogic();
            TheDayType = new DayType();
            DayTypeCollection = new ObservableCollection<DayType>(_dtlogic.Get());
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
        
        private bool _updateDayTypeVis;
        public bool UpdateDayTypeVis
        {
            get
            {
                return _updateDayTypeVis;
            }

            set
            {               
                _updateDayTypeVis = value;
                OnPropertyChanged("UpdateDayTypeVis");
            }
        }
        private bool _addDayTypeVis;
        public bool AddDaytypeVis
        {
            get
            {
                return _addDayTypeVis;
            }

            set
            {
                _addDayTypeVis = value;
                OnPropertyChanged("AddDaytypeVis");
            }
        }
        private bool _dayTypeStackPanelVis;
        public bool DayTypeStackPanelVis
        {
            get
            {
                return _dayTypeStackPanelVis;
            }

            set
            {
                _dayTypeStackPanelVis = value;
                OnPropertyChanged("DayTypeStackPanelVis");
            }
        }

        public RelayCommand Update
        {
            get
            {
                return new RelayCommand(UpdateDayType, true);
            }
        }
        public RelayCommand Delete
        {
            get
            {
                return new RelayCommand(DeleteDaytype, true);
            }
        }
        public RelayCommand Add
        {
            get
            {
                return new RelayCommand(AddDayType, true);
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
        private void ClearProperties()
        {
            TheDayType = null;
            DayTypeStackPanelVis = false;
        }
        private void AddDayType()
        {       
            _dtlogic.Add(TheDayType);
            RefreshCollection();
        }

        private void UpdateDayType()
        {
            _dtlogic.Update(TheDayType);
            RefreshCollection();
        }
        private void DeleteDaytype()
        {            
            _dtlogic.Delete(TheDayType);
            RefreshCollection();           
        }

        private void RefreshCollection()
        {
            DayTypeCollection = new ObservableCollection<DayType>(_dtlogic.Get());
        }

        private void SetAddingProperties()
        {
            DayTypeStackPanelVis = true;
            UpdateDayTypeVis = false;            
        }
        private void SetUpdateProperties()
        {
            if (TheDayType != null)
            {
                DayTypeStackPanelVis = true;
                UpdateDayTypeVis = true;
            }
            else
            {
                MessageBox.Show("Для того щоб оновити потрібно вибрати елемент", "Помилка");
            }
        }
    }
}
