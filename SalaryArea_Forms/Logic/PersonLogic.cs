using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SalaryArea_Forms.Logic
{
    public class PersonLogic 
    {
        SalDbContext _dbContext = null;

        //public IEnumerable<Person> PersonList { get; private set; }

        public PersonLogic()
        {
            _dbContext = new SalDbContext();
        }
        internal IEnumerable<Person> Get()
        {
            return _dbContext.Persons.ToList(); 
          

        }
        internal void Delete(Person person)
        {
            if (MessageBox.Show("Ви впевнені, що бажаєте виділити дану персону?",
               "Підтвердіть рішення.", MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
            {
                if (person.employee == null)
                {
                    try
                    {
                        _dbContext.Persons.Remove(person);
                        _dbContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Елемент не може бути видалений через те що вже введений як співробітник. Видаліть спочатку попередню сутність", "Помилка");
                }
            }
        }
        internal void Add(Person person)
        {
            if (CheckValidation(person) == true)
            {
                try
                {
                        _dbContext.Persons.Add(person);
                        _dbContext.SaveChanges();
                                    }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: {0}", ex.ToString());
                }
            }
        }
        internal bool Excitatnce(Person per)
        {

            var personexcictance = _dbContext.Persons
               .FirstOrDefault(p => p.PersonID == per.PersonID && p.employee == null);

            if (personexcictance != null)
            {
                return true;
            }
            return false;

        }
        internal void Change(Person addperson)
        {

            if (CheckValidation(addperson) == true)
            {
                
                    Person selectedperson = _dbContext.Persons.First
                    (p => p.PersonID == addperson.PersonID);
                    selectedperson.FirstName = addperson.FirstName;
                    selectedperson.Surname = addperson.Surname;
                    selectedperson.MidleName = addperson.MidleName;
                    selectedperson.IndentificalCode = addperson.IndentificalCode;
                    selectedperson.PasportCode = addperson.PasportCode;
                    selectedperson.BirthDay = addperson.BirthDay;
                    selectedperson.gender = addperson.gender;
                    try
                    {
                        //_dbContext.Persons.Add(selectedperson);
                        _dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: {0}", ex.ToString());
                    }

                

            }
        }

        private bool CheckValidation(Person person)
        {
            string MessageError = "";
           
                var newperson = _dbContext.Persons
                 .FirstOrDefault(p => p.Surname == person.Surname 
                 && p.FirstName ==person.FirstName
                 &&p.IndentificalCode == person.IndentificalCode
                 && p.BirthDay==person.BirthDay);
            if (newperson != null)
            {
                MessageError += "Людина вже є базі";
            }
            else
            {
                if ((string.IsNullOrWhiteSpace(person.IndentificalCode.ToString())))
                {
                    //  ||
                    //(!Regex.IsMatch(person.IndentificalCode, @"\A[0-9]{10}\z"))
                    MessageError += "Індефікаційний код має бути 10 символьним";
                }

                if (string.IsNullOrWhiteSpace(person.FirstName))
                {
                    MessageError += "FirstName,";
                }
                if (string.IsNullOrWhiteSpace(person.Surname))
                {
                    MessageError += "Surname,";
                }
                if (string.IsNullOrWhiteSpace(person.MidleName))
                {
                    MessageError += "MidleName,";
                }
                if (string.IsNullOrWhiteSpace(person.BirthDay.ToString())
                    && person.BirthDay > DateTime.Now)
                {
                    MessageError += "MidleName,";
                }
                if ((string.IsNullOrWhiteSpace(person.PasportCode)))
                   // ||
                   //(!Regex.IsMatch(person.PasportCode.ToString(), @"\A[0-9]{10}\z"))))
                {
                    MessageError += "Паспортний код має бути 10 символьним";
                }
            }
            if (string.IsNullOrEmpty(MessageError))
            {              
                return true;
            }
            else
            {
                MessageBox.Show("Mistake {0}", MessageError.ToString());
                return false;
            }
        }
    }
}
