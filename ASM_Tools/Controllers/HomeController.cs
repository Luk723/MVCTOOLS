using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASM_Tools.Models;

namespace ASM_Tools.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            CarouselDBContext db = new CarouselDBContext();
            var data = (from d in db.CarouselImages select d).ToList();
            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}