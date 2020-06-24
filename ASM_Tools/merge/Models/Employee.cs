using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASM_Tools.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public Salutation salutation { get; set; }
        public DateTime JoinedDate { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string CompanyEmail { get; set; }
        public string ResumePath { get; set; }
        public string DisplayPhotoPath { get; set; }

        public virtual ICollection<ToolToEmployee> EmployeeToTools { get; set; }
    }
    public enum Salutation
    {
        Mr, Mrs, Miss, Mdm, Ms, Mstr, Dr, Prof
    };
}