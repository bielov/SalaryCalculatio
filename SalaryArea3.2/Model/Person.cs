using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryArea3._2.Model
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string IndentificalCode { get; set; }
        public string PasportCode { get; set; }
        public DateTime BirthDay { get; set; }

        public Gender gender { get; set; }
        public enum Gender { Male, Female }
        public virtual Employee employee { get; set; }
        public Person() { }
    }
}
