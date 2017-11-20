using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SalaryArea_Forms.Logic
{
    public class EmployeeLogic
    {
        SalDbContext _dbContext = null;
        public EmployeeLogic()
        {
            _dbContext = new SalDbContext();
         //   Employeelist = new ObservableCollection<Employee>();
        }
        internal IEnumerable<Employee> Get()
        {
           return  _dbContext.Employees.Include("Person").Include("Position").ToList();
        }

        
        internal IEnumerable<Person> GetPersonWithNull()
        {
            return _dbContext.Persons.Where(p=>p.employee== null).ToList();
        }
        internal void Add(Employee emp)
        {
            if (CheckValidation(emp) == true)
            {
                try
                {
                    _dbContext.Employees.Add(emp);
                    _dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: {0}", ex.ToString());
                }
                MessageBox.Show("Працівник збережений");
            }

        }
        internal  bool Excitatnce(Employee emp)
        {
            var employeeID = _dbContext.Employees
               .FirstOrDefault(p => p.person == emp.person);
            
            if (employeeID != null)
            {
                return false;
            }
            return true;

        }

        private bool CheckValidation(Employee emp)
        {
            string MessageError = null;
            if (string.IsNullOrWhiteSpace(emp.PositionID.ToString()))
            {
                MessageError="Поле не може бути пустим";              
            }
           if (emp.StartDate > emp.EndDate)
            {
                MessageError += "Дата звільнення не може бути більшою за дату прийняття";
            }
            if (string.IsNullOrWhiteSpace(emp.Salary.ToString()))
            {
                MessageError += "Поле оклад не може бути пустим";
            }
            if (string.IsNullOrEmpty(MessageError))
            {              
                return true;
            }
            else
            {
                MessageBox.Show(MessageError.ToString(), "Помилка при валідації " );
                return false;
            }

        }
        internal void Update(Employee emp)
        {
            var updateEmp = _dbContext.Employees.FirstOrDefault(p => p.EmployeeID == emp.EmployeeID);
            updateEmp.position = emp.position;
            updateEmp.PositionID = emp.PositionID;
            updateEmp.Salary = emp.Salary;
            updateEmp.StartDate = emp.StartDate;
            try
            {
                _dbContext.SaveChanges();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Помилка при валідації ");
            }
            MessageBox.Show("Сутність успіщно оновлена");
        }
       internal void Delete(Employee emp)
        {
            if (MessageBox.Show("Ви впевнені, що бажаєте виділити даного співробітника?",
              "Підтвердіть рішення.", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (emp.salarycalculations.Count() ==0)
                {
                    try
                    {
                        _dbContext.Employees.Remove(emp);
                        _dbContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error");
                    }
                    MessageBox.Show("Сутність успіщно видалена");
                }
                else
                {
                    MessageBox.Show("Сутність видалити неможливо. Вже збергіаються дані про її заробітню платню. \n Якщо все ж таки бажаєте видалати, почніть з обрахунку", "Зауваження");
                }
            }
        }
    }
}
