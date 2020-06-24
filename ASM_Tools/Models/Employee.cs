using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Team team { get; set; }
        public virtual Employee Manager { get; set; }

        public virtual ICollection<ToolToEmployee> EmployeeToTools { get; set; }

        [NotMapped]
        public HttpPostedFileBase DisplayPhotoFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase ResumeFile { get; set; }

    }
    public enum Salutation
    {
        Mr, Mrs, Miss, Mdm, Ms, Mstr, Dr, Prof
    };
    public enum Team
    {
        ATS, COC, AHK, ATC, ATW
    }
}