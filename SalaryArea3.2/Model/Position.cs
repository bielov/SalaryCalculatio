using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalaryArea3._2.Model
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public Position() { }

    }
}
