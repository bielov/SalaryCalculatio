using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.Logic;
using SalaryArea3._2.Model;
using System.Collections.ObjectModel;
using System;

namespace SalaryArea_Forms.ViewModel
{
    public class PositionViewModel : ViewModelBase
    {
        Position _pos;      
     //   SalDBContext _db;
        PositionLogic _poclog;
           
        public PositionViewModel()
        {           
          // _db = new SalDBContext();                         
            ThePosition = new Position();
           _poclog = new PositionLogic(); 
            PositionCollection = new ObservableCollection<Position>(_poclog.Get());
          
        }
        private ObservableCollection<Position> _positionCollection;
        public ObservableCollection<Position> PositionCollection
        {
            get
            {
                
                return _positionCollection;
            }

            set
            {
                _positionCollection = value;
                OnPropertyChanged("PositionCollection");
            }

        }
        public Position ThePosition
        {
            get
            {
                return _pos;
            }

            set
            {
                _pos = value;
                OnPropertyChanged("ThePosition");
            }
        }     
   
        private bool _addstackPanelVis;
        public bool AddStackPanelVis
        {
            get
            {
                return _addstackPanelVis;
            }

            set
            {
                if (value == _addstackPanelVis) return;
                _addstackPanelVis = value;
                OnPropertyChanged("AddStackPanelVis");
            }
        }
        private bool _updateStackPanelVis;
        public bool UpdateStackPanelVis
        {
            get
            {
                return _updateStackPanelVis;
            }

            set
            {
                if (value == _updateStackPanelVis) return;
                _updateStackPanelVis = value;
                OnPropertyChanged("UpdateStackPanelVis");
            }
        }
      
        #region RelayCommand
        public RelayCommand AddPosition_btn
        {
            get
            {
                    return new RelayCommand(AddPosition, true);  
            }
        }
        public RelayCommand SetUpdateButton
        {
            get
            {
                return new RelayCommand(SetUpdateProp, true);
            }
        }
        public RelayCommand UpdatePosition
        {
            get
            {
                return new RelayCommand(Update, true);
            }
        }
        public RelayCommand DeletePosition_btn
        {
            get
            {
                return new RelayCommand(DeletePosition, true);
            }
        }
        public RelayCommand SetAddingButton
        {
            get
            {
                return new RelayCommand(SetAddingProperties, true);
            }
        }
        public RelayCommand CancelPosition_btn
        {
            get
            {
                return new RelayCommand(CancelProperties, true);
            }
        }
   
        #endregion

        private void CancelProperties()
        {
            AddStackPanelVis = false;

        }

        private void SetAddingProperties()
        {
            ThePosition = new Position();
            AddStackPanelVis = true;
            UpdateStackPanelVis = false;
        }
        private void DeletePosition()
        {
            if (_poclog.Exіstence(ThePosition) == true)
            {
                _poclog.Delete(ThePosition);
                RefreshCollection();
            }
        }


        private void Update()
        {
            _poclog.Update(ThePosition);
            CancelProperties();
            RefreshCollection();
        }

        private void AddPosition()
        {
            _poclog.Add(ThePosition);
            CancelProperties();
            RefreshCollection();

        }

        private void SetUpdateProp()
        {
            AddStackPanelVis = true;
            UpdateStackPanelVis = true;
        }


        private void RefreshCollection()
        {
            PositionCollection = new ObservableCollection<Position>(_poclog.Get());
        }
    }
}
