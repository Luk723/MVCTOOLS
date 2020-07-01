using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASM_Tools.Models
{
    public class CheckBoxToolViewModel
    {
        public int Id { get; set; }
        //public string EmployeeFirstMidName { get; set; }
        //public string EmployeeLastName { get; set; }
        public Employee Employee { get; set; }

        public bool Checked { get; set; }
        
        public string Role { get; set; }
    }
}