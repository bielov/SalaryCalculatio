using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.Logic;
using SalaryArea3._2.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace SalaryArea_Forms.ViewModel
{
    public class TaxViewModel :ViewModelBase
    {
        TaxLogic _taxlogic;
        Tax _theTax;
        public TaxViewModel()
        {
            _taxlogic = new TaxLogic();
            TheTax = new Tax();
            TaxCollection = new ObservableCollection<Tax>(_taxlogic.Get());

        }      
        public Tax TheTax
        {
            get
            {
                return _theTax;
            }

            set
            {
                _theTax = value;
                OnPropertyChanged("TheTax");
            }            
        }
        private ObservableCollection<Tax> _taxCollection;

        public ObservableCollection<Tax> TaxCollection
        {
            get
            {
                return _taxCollection;
            }

            set
            {
                _taxCollection = value;
                OnPropertyChanged("TaxCollection");
            }
        }
         private bool _taxStackPanelVis;
        private bool _updateTaxVis;
       

        public bool UpdateTaxVis
        {
            get
            {
                return _updateTaxVis;
            }

            set
            {
                if (value == _updateTaxVis) return;
                _updateTaxVis = value;
                OnPropertyChanged("UpdateTaxVis");
            }
        }
        private bool _addTaxVis;
        public bool AddTaxVis
        {
            get
            {
                return _addTaxVis;
            }

            set
            {
                if (value == _addTaxVis) return;
                _addTaxVis = value;
                OnPropertyChanged("AddTaxVis");
            }
        }
        public bool TaxStackPanelVis
        {
            get
            {
                return _taxStackPanelVis;
            }

            set
            {
                _taxStackPanelVis = value;
                OnPropertyChanged("TaxStackPanelVis");
            }
        }

        public RelayCommand Update
        {
            get
            {
                return new RelayCommand(UpdateTax, true);
            }
        }             
        public RelayCommand Delete
        {
            get
            {
                return new RelayCommand(DeleteTax, true);
            }
        }     
        public RelayCommand AddTax
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
            TheTax = null;
            TaxStackPanelVis = false;
        }
        private void Add()
        {
            _taxlogic.Addt(TheTax);
            RefreshCollection();
        }

        private void UpdateTax()
        {
            _taxlogic.Update(TheTax);
            RefreshCollection();
        }
        private void DeleteTax()
        {
            _taxlogic.Delete(TheTax);
            RefreshCollection();           
        }

        private void RefreshCollection()
        {
            TaxCollection = new ObservableCollection<Tax>(_taxlogic.Get());
        }

        private void SetAddingProperties()
        {
            TheTax = new Tax();
            TaxStackPanelVis = true;
            UpdateTaxVis = false;
           

        }
        private void SetUpdateProperties()
        {
            if (TheTax != null)
            {
                TaxStackPanelVis = true;
                UpdateTaxVis = true;
            }
            else
            {
                MessageBox.Show("Для того щоб оновити потрібно вибрати податок", "Помилка");
            }
        }
    }
}
