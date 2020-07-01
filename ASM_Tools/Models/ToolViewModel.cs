using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASM_Tools.Models
{
    public class ToolViewModel
    {
        public int ID { get; set; }
        public Tool tool { get; set; }
        public List<CheckBoxToolViewModel> Employees { get; set; }
        [NotMapped]
        public HttpPostedFileBase CoverImageFile { get; set; }
        public string Role { get; set; }
    }
}