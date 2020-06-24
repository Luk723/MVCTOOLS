using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASM_Tools.Models
{
    public class ToolToEmployee
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int ToolID { get; set; }
        public string Role { get; set; }
        public virtual Employee employee {  get; set; }
        public virtual Tool tool { get; set; }
    }
}