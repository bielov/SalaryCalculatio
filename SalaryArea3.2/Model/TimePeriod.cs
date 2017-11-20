using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea3._2.Model
{
    public class TimePeriod
    {
        [Key]
        public int PeriodID { get; set; }
        public string PeriodName { get; set; }
        public virtual ICollection<SpecialDay> specialdays { get; set; }
        public virtual ICollection<LivingWageMin> wagemins { get; set; }
        public virtual ICollection<SalaryCalculation> salarycalculations { get; set; }
        public TimePeriod() { }

    }
}
