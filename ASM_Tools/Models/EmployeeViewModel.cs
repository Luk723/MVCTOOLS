using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASM_Tools.Models
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }
        public Employee employee { get; set; }
        public List<CheckBoxEmployeeViewModel> Tools { get; set; }
        [NotMapped]
        public HttpPostedFileBase DisplayPhotoFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase ResumeFile { get; set; }

    }
}