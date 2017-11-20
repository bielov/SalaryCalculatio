using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea3._2.Model
{
    public class DayType
    {
        [Key]
        public int DayTypeID { get; set; }
        public string NameDayType { get; set; }
        public DayType() { }
        public virtual ICollection<SpecialDay> specialdays { get; set; }
    }
}
