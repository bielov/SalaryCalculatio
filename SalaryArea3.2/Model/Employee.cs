using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalaryArea3._2.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Salary { get; set; }
        public virtual Person person { get; set; }
        public int PositionID { get; set; }
        public virtual Position position { get; set; }
        public virtual ICollection<SalaryCalculation> salarycalculations { get; set; }
        public Employee() { }

    }
}
