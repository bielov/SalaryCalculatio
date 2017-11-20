using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea_Forms.Logic.BusinessLogic
{
   public static class SalaryCalculationBusinessLogic
    {
        internal static void AllCalculationOperations(SalaryCalculation salcal, TimePeriod ThePeriod, int NavigationYear)
        {

            BasicSalaryCalculation(salcal,ThePeriod, NavigationYear);
            SumAccureCalculation(salcal);
            PspCalculation(salcal, NavigationYear);
            ESVCalculation(salcal);
            DeductMilitaryTexCalculation(salcal);
            DeductPDFOCalculation(salcal);
            DeductSumCalculation(salcal);
            FinalSalaryCalculation(salcal);
        }
        private static void BasicSalaryCalculation(SalaryCalculation salcal,TimePeriod ThePeriod, int NavigationYear)
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
        private static void SumAccureCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                updateSalCal.SumAccure = updateSalCal.BasicSalary + updateSalCal.VacationAccure + updateSalCal.IllnessSum + updateSalCal.Indexation;
                _db.SaveChanges();
            }
        }
        private static void PspCalculation(SalaryCalculation salcal, int NavigationYear)
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
        private static void ESVCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                var tax = _db.Taxes.FirstOrDefault(p => p.TaxName == "Єдиний соціальний внесок");
                updateSalCal.ESV = updateSalCal.SumAccure * (tax.TaxPresentage / 100);
                _db.SaveChanges();
            }
        }
        private static void DeductMilitaryTexCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                var tax = _db.Taxes.FirstOrDefault(p => p.TaxName == "Військовий збір");
                updateSalCal.DeductMilitaryTex = updateSalCal.SumAccure * (tax.TaxPresentage / 100);
                _db.SaveChanges();
            }
        }
        private static void DeductPDFOCalculation(SalaryCalculation salcal)
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
        private static void DeductSumCalculation(SalaryCalculation salcal)
        {
            using (SalDbContext _db = new SalDbContext())
            {
                var updateSalCal = _db.SalaryCalculations.FirstOrDefault(p => p.SalaryCalculationID == salcal.SalaryCalculationID);
                updateSalCal.DeductSum = updateSalCal.DeductMilitaryTex + updateSalCal.DeductPDFO;
                _db.SaveChanges();
            }
        }
        private static void FinalSalaryCalculation(SalaryCalculation salcal)
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
