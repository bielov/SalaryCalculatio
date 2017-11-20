using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SalaryArea_Forms.Logic
{
    public class DayTypeLogic
    {
        SalDbContext _db = null;
        public DayTypeLogic()
        {
            _db = new SalDbContext();
        }
        internal IEnumerable<DayType> Get()
        {
            using (SalDbContext _dbCon = new SalDbContext())
            {
                return _dbCon.DayTypes.ToList();
            }

        }
        internal void Add(DayType dt)
        {
            if (CheckValidation(dt) == true)
            {
                using (SalDbContext _db = new SalDbContext())
                {
                    try
                    {
                        _db.DayTypes.Add(dt);
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: {0}", ex.ToString());
                    }

                }
            }
        }
        internal void Update(DayType dt)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var checkeddt = _db.DayTypes.FirstOrDefault(p => p.DayTypeID == dt.DayTypeID);
                checkeddt.NameDayType = dt.NameDayType;                
                try
                {
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: {0}", ex.ToString());
                }
                MessageBox.Show("Тип днів збережений");
                //}
            }
        }
        internal void Delete(DayType dt)
        {
            if (MessageBox.Show("Ви впевнені, що бажаєте виділити дану персону?",
               "Підтвердіть рішення.", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (SalDbContext _db = new SalDbContext())
                {
                    try
                    {
                        DayType daytype = _db.DayTypes.FirstOrDefault(p => p.DayTypeID == dt.DayTypeID);
                        _db.Entry(daytype).Collection(c => c.specialdays).Load();
                        _db.DayTypes.Remove(daytype);
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error");
                    }
                    MessageBox.Show("Видалено");
                }
            }
        }
        private bool CheckValidation(DayType dt)
        {
            string MessageError = null;
            using (SalDbContext _db = new SalDbContext())
            {
                var checkedwage = _db.DayTypes.FirstOrDefault(p => p.NameDayType == dt.NameDayType);
                if (checkedwage != null) { MessageError += "Елемент вже є в базі"; return false; }
                else
                {
                    if (string.IsNullOrWhiteSpace(dt.NameDayType.ToString()))
                    {
                        MessageError += "Поле не може бути пустим";
                    }
                }
            }
            if (string.IsNullOrEmpty(MessageError)) {
                return true;
            }
            else {
                MessageBox.Show("Mistake {0}", MessageError.ToString());
                return false;
            }
        }
    }
}

