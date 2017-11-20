using SalaryArea3._2.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SalaryArea3._2.Context
{
    public class SalDbContext : DbContext
    {

        public SalDbContext() : base("SalaryDBCon") { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<DayType> DayTypes { get; set; }
        public DbSet<SpecialDay> SpecialDays { get; set; }
        public DbSet<LivingWageMin> LivingWageMins { get; set; }
        public DbSet<TimePeriod> TimePeriods { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<SalaryCalculation> SalaryCalculations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(p => p.IndentificalCode)
                .HasMaxLength(10);
            modelBuilder.Entity<Tax>().Property(p => p.TaxPresentage)
                .HasPrecision(4, 2);


            modelBuilder.Entity<Person>().Property(p => p.PersonID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Position>().Property(p => p.PositionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // worker will have same ID with Person.It will copy it on form
            modelBuilder.Entity<Employee>().Property(p => p.EmployeeID)
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Company>().Property(p => p.CompanyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<DayType>().Property(p => p.DayTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<LivingWageMin>().Property(p => p.LivingWageMinID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<SpecialDay>().Property(p => p.SpecialDayID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Tax>().Property(p => p.TaxID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TimePeriod>().Property(p => p.PeriodID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<SalaryCalculation>().Property(p => p.SalaryCalculationID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<Employee>().HasRequired(p => p.position)
                .WithMany(p => p.Employees);
            modelBuilder.Entity<Position>().HasMany(p => p.Employees);
            modelBuilder.Entity<Employee>().HasRequired(p => p.person)
                .WithOptional(p => p.employee);

            modelBuilder.Entity<LivingWageMin>().HasRequired(p => p.timeperiod)
                 .WithMany(p => p.wagemins);
            modelBuilder.Entity<TimePeriod>().HasMany(p => p.wagemins);
            modelBuilder.Entity<LivingWageMin>().HasRequired(p => p.timeperiod);
         
            modelBuilder.Entity<SpecialDay>().HasRequired(p => p.timeperiod)
                .WithMany(p => p.specialdays);
            modelBuilder.Entity<TimePeriod>().HasMany(p => p.specialdays);

            modelBuilder.Entity<SpecialDay>().HasRequired(p => p.daytype)
                .WithMany(p => p.specialdays);
            modelBuilder.Entity<DayType>().HasMany(p => p.specialdays);

            modelBuilder.Entity<SalaryCalculation>().HasRequired(p => p.employee)
                .WithMany(p => p.salarycalculations);
            modelBuilder.Entity<Employee>().HasMany(p => p.salarycalculations);
            modelBuilder.Entity<SalaryCalculation>().HasRequired(p => p.timeperiod)
                .WithMany(p => p.salarycalculations);
            modelBuilder.Entity<TimePeriod>().HasMany(p => p.salarycalculations);
        }
    
    }
}
