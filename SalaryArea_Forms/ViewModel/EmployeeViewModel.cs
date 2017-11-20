using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.Logic;
using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System;

namespace SalaryArea_Forms.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        Position _pos;
        Person _person;
        Employee _emp;
   
        EmployeeLogic _emplog;
        PositionLogic _poclog;
        PersonLogic _perlog;
        public EmployeeViewModel()
        {
           
            ThePosition = new Position();
            TheEmployee = new Employee();


            _poclog = new PositionLogic();
            _emplog = new EmployeeLogic();
            ThePerson = new Person();
            ThePerson = null;
            _perlog = new PersonLogic();
            PositionCollection = new ObservableCollection<Position>(_poclog.Get());
            PersonCollection = new ObservableCollection<Person>(_emplog.GetPersonWithNull());
            EmployeeCollection = new ObservableCollection<Employee>(_emplog.Get());
            //
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
        private ObservableCollection<Person> _personCollection;
        public ObservableCollection<Person> PersonCollection
        {
            get { return _personCollection; }
            set
            {
                _personCollection = value;
                OnPropertyChanged("_personCollection");
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
        public Person ThePerson
        {
            get
            {
                return _person;
            }

            set
            {
                _person = value;
                OnPropertyChanged("ThePerson");
            }
        }
        public Employee TheEmployee
        {
            get
            {
                return _emp;
            }

            set
            {
                _emp = value;
                OnPropertyChanged("TheEmployee");
            }
        }


        private bool _tableaddStackPanelVis;       

        public bool TableaddStackPanelVis
        {
            get
            {
                return _tableaddStackPanelVis;
            }

            set
            {
                if (value == _tableaddStackPanelVis) return;
                _tableaddStackPanelVis = value;
                OnPropertyChanged("TableaddStackPanelVis");
            }
        }
        private string selectedSurnameText;
    
        private bool _updateEmployeeVis;
        public bool UpdateEmployeeVis
        {
            get
            {
                return _updateEmployeeVis;
            }

            set
            {
                _updateEmployeeVis = value;
                OnPropertyChanged("UpdateEmployeeVis");
            }
        }
        public string SelectedSurnameText
        {
            get
            {
                return selectedSurnameText;
            }

            set
            {
                selectedSurnameText = value;
                OnPropertyChanged("SelectedSurnameText");
            }
        }


        public RelayCommand SetAddingTableButton
        {
            get
            {
                return new RelayCommand(SetAddTableVIisProperties, true);
            }
        }
        public RelayCommand SetUpdateTableButton
        {
            get
            {
                return new RelayCommand(SetUpdateTableVIisProperties, true);
            }
        }
       

        public RelayCommand Add
        {
            get
            {
                return new RelayCommand(AddEmployee, true);
            }
        }
        public RelayCommand Update
        {
            get
            {
                return new RelayCommand(UpdateEmployee, true);
            }
        }
        public RelayCommand Delete
        {
            get
            {
                return new RelayCommand(DeleteEmployee, true);
            }
        }
        public RelayCommand Cancel
        {
            get
            {
                return new RelayCommand(CacncelEmployee, true);
            }
        }
        public RelayCommand RefresehDataGridCollection
        {
            get
            {
                return new RelayCommand(RefreshEmployeeCollection, true);
            }
        }
        

        private void CacncelEmployee()
        {
            RefreshEmployeeCollection();
            ClearProperties();
        }

        private void DeleteEmployee()
        {
            _emplog.Delete(TheEmployee);
            RefreshEmployeeCollection();
            ClearProperties();
        }

        private void UpdateEmployee()
        {
            TheEmployee.PositionID = ThePosition.PositionId;
            _emplog.Update(TheEmployee);
            RefreshEmployeeCollection();
            ClearProperties();
          
            
        }

        private void AddEmployee()
        {
           
            TheEmployee.PositionID = ThePosition.PositionId;
            TheEmployee.EmployeeID = ThePerson.PersonID;
            _emplog.Add(TheEmployee);
            RefreshEmployeeCollection();
            ClearProperties();
        }

        private void RefreshEmployeeCollection()
        {
            EmployeeCollection = new ObservableCollection<Employee>(_emplog.Get());
            PositionCollection = new ObservableCollection<Position>(_poclog.Get());
            PersonCollection = new ObservableCollection<Person>(_emplog.GetPersonWithNull());


        }
        private void ClearProperties()
        {
            TheEmployee = new Employee();
            TableaddStackPanelVis = false;
        }

     

        private void SetAddTableVIisProperties()
        {
            if (ThePerson == null) { MessageBox.Show("Виберіть елемент", "Зауваження"); }
            else if (_perlog.Excitatnce(ThePerson) == false)
            {
                MessageBox.Show("Табель вже є базі даних ");
            }
            else
            {
                TheEmployee = new Employee();
                TableaddStackPanelVis = true;
                
                TheEmployee.EmployeeID = ThePerson.PersonID;
                SelectedSurnameText = AbbreviationGenerator(ThePerson);
            }
        }
        private void SetUpdateTableVIisProperties()
        {
            if (TheEmployee.person == null) { MessageBox.Show("Виберіть елемент", "Зауваження"); }
            else
            {
                TableaddStackPanelVis = true;
                UpdateEmployeeVis = true;
                SelectedSurnameText = AbbreviationGenerator(TheEmployee.person);
            }
        }
        private string  AbbreviationGenerator(Person per)
        {           
            StringBuilder sb = new StringBuilder();
            sb.Append(per.Surname+ " ");
            sb.Append(per.FirstName[0]+".");
            sb.Append(per.MidleName[0] + ".");
            return sb.ToString();
        }
    }
    }
