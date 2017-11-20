using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace SalaryArea_Forms.Logic
{
    public class SpecialDayLogic
    {
        SalDbContext _db;
        public SpecialDayLogic()
        {
            _db = new SalDbContext();
        }
        internal IEnumerable<SpecialDay> GetSP()
        {
            using (SalDbContext _dbContext = new SalDbContext())
            {        
             return _dbContext.SpecialDays.Include("DayType").Include("TimePeriod")
                    .OrderBy(p=>p.SpecialDayYear).OrderBy(p=>p.PeriodId).ToList();              
            }
        }
        internal IEnumerable<DayType> GetDT()
        {
            using (SalDbContext _dbCon = new SalDbContext())
            {
                return _dbCon.DayTypes.ToList();
            }
        }
        internal IEnumerable<TimePeriod> GetPC()
        {
            using (SalDbContext _dbCont = new SalDbContext())
            {
                return _dbCont.TimePeriods.ToList();
            }
        }
        internal void Add(SpecialDay sd)
        {
            if (CheckValidation(sd) == true)
            {
                using (SalDbContext _db = new SalDbContext())
                {
                    try
                    {
                        _db.SpecialDays.Add(sd);
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                  

                }
                MessageBox.Show("Успішно");
            }
        }
        internal void Update(SpecialDay sd)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var newsd = _db.SpecialDays.FirstOrDefault(p => p.SpecialDayID==sd.SpecialDayID);
                newsd.SpecialDayYear = sd.SpecialDayYear;
                newsd.PeriodId = newsd.PeriodId;              
                newsd.DayTypeId = sd.DayTypeId;         
                try
                {
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }            
            MessageBox.Show("Об'єкт був вдало оновленим");
        }


        private bool CheckValidation(SpecialDay sd)
        {
            string MessageError = null;
            using (SalDbContext _db = new SalDbContext())
            {
                var newsd = _db.SpecialDays.FirstOrDefault(p => p.SpecialDayYear == sd.SpecialDayYear 
                && p.DayTypeId == sd.DayTypeId &&p.PeriodId == sd.PeriodId);
                if(newsd != null) {MessageBox.Show( "Елемент вже введений в  базу"); return false; }
                else
                {
                    if (!string.IsNullOrWhiteSpace(sd.SpecialDayYear.ToString())
                        && (!Regex.IsMatch(sd.SpecialDayYear.ToString(), @"\A[0-9]{4}\z")))
                    {
                        MessageError += "Всі полі мають бути введені";
                    }
                    if (string.IsNullOrWhiteSpace(sd.DayValue.ToString())
                        && (!Regex.IsMatch(sd.DayValue.ToString(), @"\A[0-9]{2}\z")))
                    {
                        MessageError += "Всі полі мають бути введені";
                    }
                
                    if (string.IsNullOrEmpty(MessageError))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show( MessageError.ToString(),"Mistake {0}");
                       
                        return false;
                    }
                }
            }
            }
    }
}
