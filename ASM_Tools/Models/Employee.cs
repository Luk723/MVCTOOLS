using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASM_Tools.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Remote("IsEmployeeIDExist", "EditorEmployee",ErrorMessage = "Employee ID already taken")]
        public int ID { get; set; }

        [DisplayName("Last Name")]
        [StringLength(60, MinimumLength = 2)]
        public string LastName { get; set; }

        [DisplayName("First Name")]
        [StringLength(60, MinimumLength = 2)]
        public string FirstMidName { get; set; }

        public Salutation salutation { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime JoinedDate { get; set; }

        [DisplayName("Job Title")]
        public string JobTitle { get; set; }

        [DisplayName("Department")]
        public string Department { get; set; }

        [DisplayName("Email")]
        public string CompanyEmail { get; set; }

        [DisplayName("Resume")]
        public string ResumePath { get; set; }

        [DisplayName("Profile Picture")]
        public string DisplayPhotoPath { get; set; }

        [DisplayName("ASM Centre")]
        public Team team { get; set; }

        public virtual Employee Manager { get; set; }


        public virtual ICollection<ToolToEmployee> EmployeeToTools { get; set; }

        //[NotMapped]
        //public int EmployeeNumber { get; set; }

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