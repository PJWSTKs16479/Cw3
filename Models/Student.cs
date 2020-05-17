using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Studies { get; set; }
        public int Semester { get; set; }

        public int IdStudent { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string IndexNumber { get; set; }
        //public string BirthDate { get; set; }
        public string IdEnrollment { get; set; }
        public string Name { get; set; }
        //public string Semester { get; set; }
        public string IdStudy { get; set; }
        public string StartDate { get; set; }
    }
}
