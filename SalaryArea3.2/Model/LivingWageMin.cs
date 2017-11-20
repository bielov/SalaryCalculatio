using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea3._2.Model
{
    public class LivingWageMin
    {
        [Key]
        public int LivingWageMinID { get; set; }
        public int WageYear { get; set; }
        public decimal WageValue { get; set; }
        public LivingWageMin() { }
        public int PeriodId { get; set; }
        public virtual TimePeriod timeperiod { get; set; }
    }
}
