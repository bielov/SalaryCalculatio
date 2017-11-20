using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace SalaryArea_Forms.Logic
{
    public class WageLogic
    {
        SalDbContext _db = null;
        public WageLogic()
        {
            _db = new SalDbContext();
        }
        internal IEnumerable<LivingWageMin> Get()
         {
            using (SalDbContext _db = new SalDbContext())
            {
                return _db.LivingWageMins.Include("TimePeriod").OrderByDescending(p=>p.WageYear)
                    .OrderBy(p=>p.PeriodId).ToList();
            }
        }
        internal IEnumerable<TimePeriod> GetPC()
        {
            using (SalDbContext _db = new SalDbContext())
            {
                return _db.TimePeriods.Include("wagemins")
                .Include("specialdays").Include("salarycalculations").ToList();
            }

        }
        internal void Add(LivingWageMin wage)
        {
            using (SalDbContext _db = new SalDbContext())
            {

                if (CheckValidation(wage) == true)
                {
                    try
                    {
                        _db.LivingWageMins.Add(wage);
                        _db.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.ToString(), "Помилка");
                    }
                    MessageBox.Show("Успішно");
                }
            }
        }
        internal void Update(LivingWageMin wage)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var checkedwage = _db.LivingWageMins.FirstOrDefault(p => p.LivingWageMinID == wage.LivingWageMinID);
                checkedwage.WageValue = wage.WageValue;
                checkedwage.WageYear = wage.WageYear;
                checkedwage.PeriodId = checkedwage.PeriodId;
               
               try
                {
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: {0}", ex.ToString());
                }
                MessageBox.Show("Успішно");
            }
        }
        internal void Delete(LivingWageMin wage)
        {
            if (MessageBox.Show("Ви впевнені, що бажаєте виділити даний прожитковий мінімум?",
               "Підтвердіть рішення.", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (SalDbContext _db = new SalDbContext())
                {
                    try
                    {
                        _db.LivingWageMins.Remove(wage);
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error");
                    }                   
                } MessageBox.Show("Успішно");

            }
        }
        private bool CheckValidation(LivingWageMin wage)
        {
                    string MessageError = null;
                    if(string.IsNullOrWhiteSpace(wage.WageYear.ToString()) &&
                       !Regex.IsMatch(wage.WageYear.ToString(), @"\A[0-9]{4}\z"))
                    {
                        MessageError += "рік має бути введеним чслом";
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
