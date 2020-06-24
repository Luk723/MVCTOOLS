using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace ASM_Tools.Models
{
    public class Carousel
    {
        public int id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class CarouselDBContext : DbContext 
    { 
        public DbSet<Carousel> CarouselImages { get; set; }    
    }
}