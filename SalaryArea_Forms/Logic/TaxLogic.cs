using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SalaryArea_Forms.Logic
{
    public class TaxLogic
    {
        SalDbContext _dbContext = null;
        public TaxLogic()
        {
            _dbContext = new SalDbContext();
        }
        internal IEnumerable<Tax> Get()
        {
            using (SalDbContext _db = new SalDbContext())
            {

                return _db.Taxes.ToList();
            }

        }
        internal void Addt(Tax tax)
        {
           // MessageBox.Show("Add tax", "Помилка");
            if (CheckValidation(tax) == true)
            {
                try
                {
                    using (SalDbContext _db = new SalDbContext())
                    {
                        _db.Taxes.Add(tax);
                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
                MessageBox.Show("Success");
            }
            else
            {MessageBox.Show("Введені дані не валідні");}
        }

        private bool CheckValidation(Tax tax)
        {
            string MessageError = null;
           
                var newtax = _dbContext.Taxes.FirstOrDefault(p => p.TaxName == tax.TaxName);

                if (newtax != null) {
                    MessageError = "Об'єкт вже є в базі";
                    return false;
                }
            else
            {
                if (string.IsNullOrWhiteSpace(tax.TaxName.ToString()))
                {
                    MessageError = "Назва податку не може бути пустою";
                }
                if (string.IsNullOrWhiteSpace(tax.TaxPresentage.ToString()))
                // ||
                //(!Regex.IsMatch(tax.TaxPresentage.ToString(), @"\A[0-9]{5}\z")))   //Переробити регулярку 
                {  MessageError = "Розмір податку має містити лише видимі цифрові символи";
                }
            }
        
                if (string.IsNullOrEmpty(MessageError))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show(MessageError.ToString(), "Mistake");
                    return false;
                }
            
        }

        internal void Delete(Tax tax)
        {
            if (MessageBox.Show("Ви впевнені, що бажаєте виділити даний податок?",
               "Підтвердіть рішення.", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    using (SalDbContext _db = new SalDbContext())
                    {
                        _db.Taxes.Remove(tax);
                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }

        }
        internal void Update(Tax tax)
        {
            using (SalDbContext _dbContext = new SalDbContext())
            {
                Tax selectedtax = _dbContext.Taxes
                .First(p => p.TaxID == tax.TaxID);
                selectedtax.TaxName = tax.TaxName;
                selectedtax.TaxPresentage = tax.TaxPresentage;
                try
                {
                    _dbContext.SaveChanges();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }
        }


    }
}
