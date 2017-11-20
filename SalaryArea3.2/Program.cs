using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SalDbContext db = new SalDbContext())
            {
                db.Database.CreateIfNotExists();

                Person per1 = new Person
                {
                    Surname = "Petrov",
                    FirstName = "Max",
                    MidleName = "Petrovich",
                    IndentificalCode = "7895231456",
                    PasportCode = "AT54782",
                    BirthDay = new DateTime(1987, 02, 05),
                    gender = Person.Gender.Male,
                        // PersonID = 1
                    };
    
        Position pos1 = new Position()
        {
            PositionName = "Director",
            //PositionId = 1
        };
        Position pos2 = new Position()
        {
            PositionName = "Acountant",
            // PositionId = 2
        };
    
        Employee emp1 = new Employee
        {
            EmployeeID = per1.PersonID,
            StartDate = new DateTime(2008, 05, 02),
            EndDate = new DateTime(2018, 05, 02),
            position = pos1
    
        };
        Tax tax1 = new Tax
        {
            //  TaxID = 1,
            TaxName = "Військовий збір",
            TaxPresentage = 18
        };
        DayType daytype1 = new DayType
        {
            //  DayTypeID = 1,
            NameDayType = "Каледнарні дні"
        };
        TimePeriod tp1 = new TimePeriod
        {
            PeriodID = 1,
            PeriodName = "Січень"
        };
        SpecialDay specday1 = new SpecialDay
        {
            //  SpecialDayID = 1,
            daytype = daytype1,
            SpecialDayYear = 2017,
            DayValue = 20,
            PeriodId = tp1.PeriodID,
            DayTypeId = daytype1.DayTypeID,
            timeperiod = tp1
    
        };
        LivingWageMin wagemin1 = new LivingWageMin()
        {
            //   WageID = 1,
            WageYear = 2017,
            WageValue = 1600,
            PeriodId = tp1.PeriodID,
            timeperiod = tp1
    
    
        };
        Company comp1 = new Company
        {
            //   CompanyID = 1,
            CompanyName = "ІП Симоненко",
            ErdpoyCode = "8745632179985",
            CompanyAdress = "Леніна 24",
            InformationCode = "АП 87512",
            RegistrationDate = "5/07/2014",
            RegistrationAdress = "Вінницьке Рувд "
        };
        SalaryCalculation salcal1 = new SalaryCalculation
        {
            SalaryCalculationID = 1,
            Advance = 234.5m,
            EmployeeID = emp1.EmployeeID,
            TimePeriodID = tp1.PeriodID,
            timeperiod = tp1,
            
            
    
    
        };
        db.Companys.Add(comp1);
                    db.LivingWageMins.Add(wagemin1);
                    db.DayTypes.Add(daytype1);
                    db.SpecialDays.Add(specday1);
                    db.Taxes.Add(tax1);
                    //db.SaveChanges();
                    db.Employees.Add(emp1);
                    db.Positions.Add(pos1);
                    db.Positions.Add(pos2);
                    db.TimePeriods.Add(tp1);
                    db.Persons.Add(per1);
                    db.SalaryCalculations.Add(salcal1);
                    try
                    {
                        //db.SaveChanges();
                    }
                    catch (DbUpdateException e)
                    {
                        Console.WriteLine("\n\n*** {0}\n\n", e.InnerException);
                    }
                    Console.WriteLine("Success");
                }
            Console.ReadKey(true);
       }
    }
}
