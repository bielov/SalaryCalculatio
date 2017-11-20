using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.Logic;
using SalaryArea_Forms.View;
using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SalaryArea_Forms.ViewModel
{
    public class SalaryCalculationViewModel : ViewModelBase
    {
        SalaryCalculationLogic _sallogic;
        SalDbContext _dbContext = null;

        public SalaryCalculationViewModel()
        {
            _dbContext = new SalDbContext();

            _sallogic = new SalaryCalculationLogic();
            TheEmployee = new Employee();            
            ThePeriod = new TimePeriod();
            TheSalaryCalculation = new SalaryCalculation();
            EmployeeCollection = new ObservableCollection<Employee>(_sallogic.GetEmployee());
            PeriodCollection = new ObservableCollection<TimePeriod>(_sallogic.GetPC());
            SalaryCalculationCollection = new ObservableCollection<SalaryCalculation>(_sallogic.GetSalaryWithPar(ThePeriod, NavigationYear));

        }
        private ObservableCollection<SalaryCalculation> _salarycalculationCollection;
        private ObservableCollection<Employee> _employeeCollection;
        private ObservableCollection<TimePeriod> _periodCollection;
        private Employee _theEmployee;
        private SalaryCalculation _theSalaryCalculation;
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
        // _resultDataGridStackPanelVis

        private bool _dataGridStackPanelVis;
        public bool DataGridStackPanelVis
        {
            get
            {
                return _dataGridStackPanelVis;
            }

            set
            {
                _dataGridStackPanelVis = value;
                OnPropertyChanged("DataGridStackPanelVis");
            }
        }
        private bool _resultDataGridStackPanelVis;
        public bool ResultDataGridStackPanelVis
        {
            get
            {
                return _resultDataGridStackPanelVis;
            }

            set
            {
                _resultDataGridStackPanelVis = value;
                OnPropertyChanged("ResultDataGridStackPanelVis");
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

        public RelayCommand SetEdittingButton
        {
            get
            {
                return new RelayCommand(SetCalculationDataGrid, true);
            }
        }
        public RelayCommand PrepareToPrint
        {
            get
            {
                return new RelayCommand(OpenPrintDocument, true);
            }
        }

        public RelayCommand SetCalculationProperties
        {
            get
            {
                return new RelayCommand(SaveUserData, true);
            }
        }
        public RelayCommand Save
        {
            get
            {
                return new RelayCommand(SaveSalaryCalculation, true);
            }
        }

        public RelayCommand Cancel
        {
            get
            {
                return new RelayCommand(ClearProperties, true);
            }
        }

        private void OpenPrintDocument()
        {
            SalaryCalculationPrintView salcalprint = new SalaryCalculationPrintView();
            RefeshCollection();
            salcalprint.ShowDialog();
        }
        private void SaveSalaryCalculation()
        {
            _sallogic.SaveUserData(TheSalaryCalculation);
            RefeshCollection();
            ClearProperties();
        }

        private void ClearProperties()
        {
            TheSalaryCalculation = new SalaryCalculation();
        }

        private void SaveUserData()
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var CheckSalCalculation = _db.SalaryCalculations.Include("Employee").Include("TimePeriod").
                       Where(p => p.CalculationYear == NavigationYear
                       && p.TimePeriodID == ThePeriod.PeriodID);
                foreach (SalaryCalculation salcal in CheckSalCalculation)
                {
                   AllCalculationOperations(salcal);

                }
                //MessageBox.Show("Success");
                RefeshCollection();
                ResultDataGridStackPanelVis = true;


            }

        }

        private void SetCalculationDataGrid()
        {
            if (NavigationYear != 0 && ThePeriod != null)
            {
                using (SalDbContext _db = new SalDbContext())
                {
                    var CheckSalCalculation = _db.SalaryCalculations.
                        FirstOrDefault(p => p.CalculationYear == NavigationYear
                        && p.TimePeriodID == ThePeriod.PeriodID);
                    if (CheckSalCalculation == null)
                    {
                        var CurrentTimeperiod = _db.TimePeriods.FirstOrDefault(p => p.PeriodID == ThePeriod.PeriodID);
                        var CurrentEmployee = _db.Employees.Where(p => p.EndDate == null);
                        foreach (Employee empl in CurrentEmployee)
                        {
                            SalaryCalculation salcal = new SalaryCalculation
                            {
                                CalculationYear = NavigationYear,
                                EmployeeID = empl.EmployeeID,
                                employee = empl,
                                TimePeriodID = CurrentTimeperiod.PeriodID,
                                timeperiod = CurrentTimeperiod

                            };
                            _db.SalaryCalculations.Add(salcal);
                        }
                        try
                        {
                            _db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.ToString(), "Зауваження");
                        }
                    }
                }
                DataGridStackPanelVis = true;
                RefeshCollection();

            }
            else
            {
                MessageBox.Show("Поля місяць та рік  є обов'язковими для введення", "Зауваження");
            }
        }

        private void RefeshCollection()
        {
            SalaryCalculationCollection = new ObservableCollection<SalaryCalculation>(_sallogic.GetSalaryWithPar(ThePeriod, NavigationYear));
        }
        private void AllCalculationOperations(SalaryCalculation salcal)
        {

            BasicSalaryCalculation(salcal);
            SumAccureCalculation(salcal);
            PspCalculation(salcal);
            ESVCalculation(salcal);
            DeductMilitaryTexCalculation(salcal);
            DeductPDFOCalculation(salcal);
            DeductSumCalculation(salcal);
            FinalSalaryCalculation(salcal);
        }
        private void BasicSalaryCalculation(SalaryCalculation salcal)
        {
            int idealnumber = 0;
            using (SalDbContext _db = new SalDbContext())
            {
                var monthday = _db.SpecialDays.First(p => p.DayTypeId == 2 && p.PeriodId == ThePeriod.PeriodID && p.SpecialDayYear == NavigationYear);
                idealnumber = monthday.DayValue;

                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                updateSalCal.BasicSalary = (updateSalCal.employee.Salary / idealnumber) * (decimal)updateSalCal.RealWorkTime;
                _db.SaveChanges();
            }
        }
        private void SumAccureCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                updateSalCal.SumAccure = updateSalCal.BasicSalary + updateSalCal.VacationAccure + updateSalCal.IllnessSum + updateSalCal.Indexation;
                _db.SaveChanges();
            }
        }
        private void PspCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                var PSPtax = _db.Taxes.FirstOrDefault(p => p.TaxName == "Податково-соціальна пільга");
                var calculationrestrictedSum = _db.LivingWageMins.FirstOrDefault(p => p.PeriodId == 1 && p.WageYear == NavigationYear);
                if (updateSalCal.PSP <= ((decimal)0.5 * calculationrestrictedSum.WageValue))
                {
                    var livingwage = _db.LivingWageMins.FirstOrDefault(p => p.PeriodId == salcal.TimePeriodID
                    && p.WageYear == NavigationYear);
                    updateSalCal.PSP = 0.5m * (decimal)livingwage.WageValue;

                }
                else
                {
                    updateSalCal.PSP = 0;
                }
                _db.SaveChanges();
            }
        }
        private void ESVCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                var tax = _db.Taxes.FirstOrDefault(p => p.TaxName == "Єдиний соціальний внесок");
                updateSalCal.ESV = updateSalCal.SumAccure * (tax.TaxPresentage / 100);
                _db.SaveChanges();
            }
        }
        private void DeductMilitaryTexCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                var tax = _db.Taxes.FirstOrDefault(p => p.TaxName == "Військовий збір");
                updateSalCal.DeductMilitaryTex = updateSalCal.SumAccure * (tax.TaxPresentage / 100);
                _db.SaveChanges();
            }
        }
        private void DeductPDFOCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                var tax = _db.Taxes.FirstOrDefault(p => p.TaxName == "Податок на доходи фізичних осіб");
                updateSalCal.DeductPDFO = (updateSalCal.SumAccure - updateSalCal.PSP) * (tax.TaxPresentage / 100);
                if (updateSalCal.DeductPDFO <= 0)
                {
                    updateSalCal.DeductPDFO = 0;
                }
                _db.SaveChanges();
            }
        }
        private void DeductSumCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                updateSalCal.DeductSum = updateSalCal.DeductMilitaryTex + updateSalCal.DeductPDFO;
                _db.SaveChanges();
            }
        }
        private void FinalSalaryCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p =>
                p.SalaryCalculationID == salcal.SalaryCalculationID);
                updateSalCal.FinalSalary = updateSalCal.SumAccure -
                    updateSalCal.DeductSum - updateSalCal.Advance -
                    updateSalCal.AnotherDeduct;
                _db.SaveChanges();
            }
        }



    }
}

