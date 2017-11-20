using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea3._2.Model
{
    public class Tax
    {
        [Key]
        public int TaxID { get; set; }
        public string TaxName { get; set; }
        public decimal TaxPresentage { get; set; }
        public Tax() { }
    }
}
