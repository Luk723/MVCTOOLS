using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASM_Tools.Models
{
    public class EmployeeViewModel
    {
        public int ID{ get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<CheckBoxViewModel> Tools { get; set; }

    }
}