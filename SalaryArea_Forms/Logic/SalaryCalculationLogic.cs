using System.Collections.Generic;
using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System.Linq;
using System;
using System.Windows;

namespace SalaryArea_Forms.Logic
{
    public  class SalaryCalculationLogic
    {
        SalDbContext _dbContext = null;
        public SalaryCalculationLogic()
        {
            _dbContext = new SalDbContext();

        }
        internal IEnumerable<Employee> GetEmployee()
        {
          return _dbContext.Employees.Where(p => p.EndDate == null);
         }
        internal IEnumerable<TimePeriod> GetPC()
        {
                return _dbContext.TimePeriods.ToList();
        }
        internal IEnumerable<SalaryCalculation> GetSalarybyName(Employee TheEmployee)
        {
            return _dbContext.SalaryCalculations.Include("Employee").Include("TimePeriod").Where(p=>p.employee.person.PersonID == TheEmployee.person.PersonID)
                .OrderByDescending(p => p.CalculationYear).OrderByDescending(p => p.TimePeriodID).ToList();
        }
        internal IEnumerable<SalaryCalculation> GetSalarybySalary(decimal Salary)
        {
            return _dbContext.SalaryCalculations.Include("Employee").Include("TimePeriod").Where(p => p.FinalSalary == Salary)
                .OrderByDescending(p => p.CalculationYear).OrderByDescending(p => p.TimePeriodID).ToList();
        }
        internal IEnumerable<SalaryCalculation> GetSalarybyPosition(Position pos)
        {
            return _dbContext.SalaryCalculations.Include("Employee").Include("TimePeriod")
                .Where(p => p.employee.position.PositionId == pos.PositionId).OrderByDescending(p => p.CalculationYear)
                .OrderByDescending(p => p.TimePeriodID).ToList();
        }
        internal IEnumerable<SalaryCalculation> GetSalary()
        {
            return _dbContext.SalaryCalculations.Include("Employee").Include("TimePeriod")
                .OrderByDescending(p=>p.CalculationYear).OrderByDescending(p=>p.TimePeriodID).ToList();
        }

        internal IEnumerable<SalaryCalculation> GetSalaryWithPar(TimePeriod thePeriod, int navigationYear)
        {
            return _dbContext.SalaryCalculations.Include("Employee").Include("TimePeriod").Where(p => p.CalculationYear == navigationYear
                        && p.TimePeriodID == thePeriod.PeriodID).ToList();
        }
        internal void Add(SalaryCalculation salcal)
        {
            if (CheckValidation(salcal) == true)
            {
                using (SalDbContext _db = new SalDbContext())
                {
                    _db.SalaryCalculations.Add(salcal);
                    _db.SaveChanges();
                }             
            }
        }
        internal void SaveUserData(SalaryCalculation salcal)
        {
            if (CheckValidation(salcal) == true)
            {
                using (SalDbContext _db = new SalDbContext())
                {
                    var updatedSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                    updatedSalCal.RealWorkTime = salcal.RealWorkTime;
                    updatedSalCal.VacationAccure = salcal.VacationAccure;
                    updatedSalCal.IllnessDay = salcal.IllnessDay;
                    updatedSalCal.IllnessMonth = salcal.IllnessMonth;
                    updatedSalCal.IllnessSum = salcal.IllnessSum;
                    updatedSalCal.Indexation = salcal.Indexation;
                    updatedSalCal.AnotherDeduct = salcal.AnotherDeduct;
                    updatedSalCal.Advance = salcal.Advance;
                    try
                    {
                        _db.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Помилка");
                    }
                }
            }
        }
      

        private bool CheckValidation(SalaryCalculation salcal)
        {
            return true;
        }
    }
}
