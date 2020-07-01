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
    public class Tool
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Remote("IsToolIDExist", "EditorTool", ErrorMessage = "Tool ID already taken")]
        public int ToolID { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Tool Description")]
        public string Description { get; set; }
        [DisplayName("Tag")]
        public string Tag { get; set; }
        [DisplayName("Cover Image")]
        public string CoverImagePath { get; set; }
        [DisplayName("Images for gallery")]
        public string GalleryPath { get; set; }
        [DisplayName("Documentation")]
        public string DocumentationPath { get; set; }
        [DisplayName("Installation File")]
        public string InstallationPath { get; set; }
        [DisplayName("Video Presentation")]
        public string VideoPath { get; set; }
        public ICollection<ToolToEmployee> ToolToEmployees { get; set; }

        public Team team { get; set; }
        [NotMapped]
        public HttpPostedFileBase CoverImageFile { get; set; }

        [NotMapped]
        public HttpPostedFileBase[] DocumentationFiles { get; set; }
        [NotMapped]
        public HttpPostedFileBase[] InstallationFiles { get; set; }
        [NotMapped]
        public HttpPostedFileBase[] VideoFiles { get; set; }
        [NotMapped]
        public HttpPostedFileBase[] GalleryFiles { get; set; }

    }
    
}