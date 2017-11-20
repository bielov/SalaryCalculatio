using SalaryArea_Forms.Interfaces;
using SalaryArea_Forms.Logic;
using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System.Collections.ObjectModel;
using System.Windows;
using static SalaryArea3._2.Model.Person;

namespace SalaryArea_Forms.ViewModel
{
    public class PersomViewModel : ViewModelBase
    {
        Person _person;
        PersonLogic _perlog;
    
        public PersomViewModel()
        {
            
            ThePerson = new Person();
            _perlog = new PersonLogic();
            SelectedGender = new Gender();
            PersonCollection = new ObservableCollection<Person>(_perlog.Get());
            Genders = new ObservableCollection<Gender>();

        }
        public  Person ThePerson { 
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
        
        private ObservableCollection<Person> _personCollection;
        public ObservableCollection<Person> PersonCollection
        {
            get {

                return _personCollection;
            }
            set
            {
                _personCollection = value;
                OnPropertyChanged("PersonCollection");
            }
        }
  
        private ObservableCollection<Gender> genders = new ObservableCollection<Gender>()
    { Gender.Male, Gender.Female };

        public ObservableCollection<Gender> Genders
        {
            get { return genders; }
            set
            {
                genders = value;
                OnPropertyChanged("Genders");
            }
        }

        private Gender selectedGender;

        public Gender SelectedGender
        {
            get { return selectedGender; }
            set
            {
                selectedGender = value;
                OnPropertyChanged("SelectedGender");
            }
        }
        private bool _personStackPanelVis;
        public bool PersonStackPanelVis
        {
            get
            {
                return _personStackPanelVis;
            }

            set
            {
                if (value == _personStackPanelVis) return;
                _personStackPanelVis = value;
                OnPropertyChanged("PersonStackPanelVis");
            }
        }
        private bool _updatePersonVis;
        public bool UpdatePersonVis
        {
            get
            {
                return _updatePersonVis;
            }

            set
            {
                if (value == _updatePersonVis) return;
                _updatePersonVis = value;
                OnPropertyChanged("UpdatePersonVis");
            }
        }

        public RelayCommand Update
        {
            get
            {
                return new RelayCommand(UpdatePerson, true);
            }
        }

        public RelayCommand Delete
        {
            get
            {
                return new RelayCommand(DeletePerson, true);
            }
        }
        public RelayCommand Add
        {
            get
            {
                return new RelayCommand(AddPerson, true);
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
            ThePerson = null;
            PersonStackPanelVis = false;
        }
         private void SetAddingProperties()
        {
            ThePerson = new Person();
            PersonStackPanelVis = true;
            UpdatePersonVis = false;
        }
        private void SetUpdateProperties()
        {
            if (ThePerson != null)
            {
                PersonStackPanelVis = true;
                UpdatePersonVis = true;
            }
            else
            {
                MessageBox.Show("Для того щоб оновити потрібно вибрати людину", "Помилка");
            }
        }
        private void UpdatePerson()
        {
            _perlog.Change(ThePerson);
            RefreshCollection();
            ClearProperties();
        }

        private void RefreshCollection()
        {
            PersonCollection = new ObservableCollection<Person>(_perlog.Get());
            
        }

        private void DeletePerson()
        {
            _perlog.Delete(ThePerson);
            RefreshCollection();
        }

      
        private void AddPerson()
        {
            //if (ThePerson.gender.ToString() == "Male")
            //    ThePerson.gender = Gender.Male;
            //else { 
            //    ThePerson.gender = Gender.Female; }
            ThePerson.gender = SelectedGender;
            _perlog.Add(ThePerson);
            RefreshCollection();
            ClearProperties();
        }
   
        
       
    }
}
