using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea3._2.Model
{
    public class SalaryCalculation
    {
        [Key]
        public int SalaryCalculationID { get; set; }
        public float RealWorkTime { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal VacationAccure { get; set; }
        public int IllnessMonth { get; set; }
        public int IllnessDay { get; set; }
        public decimal IllnessSum { get; set; }
        public decimal Indexation { get; set; }
        public decimal SumAccure { get; set; }
        public decimal PSP { get; set; }
        public decimal ESV { get; set; }
        public decimal DeductMilitaryTex { get; set; }
        public decimal DeductPDFO { get; set; }
        public decimal DeductSum { get; set; }
        public decimal Advance { get; set; }
        public decimal AnotherDeduct { get; set; }
        public decimal FinalSalary { get; set; }
        public int CalculationYear { get; set; }
        public int EmployeeID { get; set; }
        public virtual Employee employee { get; set; }
        public int TimePeriodID { get; set; }
        public virtual TimePeriod timeperiod { get; set; }

        //public virtual ICollection<Employee> employees { get; set; }
        //public virtual ICollection<TimePeriod> timeperiods { get; set; }

    }
}
