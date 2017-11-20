using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea3._2.Model
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ErdpoyCode { get; set; }
        public string CompanyAdress { get; set; }
        public string RegistrationAdress { get; set; }
        public string InformationCode { get; set; }
        public string RegistrationDate { get; set; }
    }
}
