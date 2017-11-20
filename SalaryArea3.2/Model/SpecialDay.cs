using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea3._2.Model
{
    public class SpecialDay
    {
        [Key]
        public int SpecialDayID { get; set; }
        public int SpecialDayYear { get; set; }
        public int DayValue { get; set; }
        public SpecialDay() { }
        public int DayTypeId { get; set; }
        public virtual DayType daytype { get; set; }
        public int PeriodId { get; set; }
        public virtual TimePeriod timeperiod { get; set; }

    }
}
