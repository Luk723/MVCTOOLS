using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASM_Tools.Models
{
    public class CheckBoxViewModel
    {
        public int Id { get; set; }
        public string ToolName { get; set; }

        public bool Checked { get; set; }
    }
}