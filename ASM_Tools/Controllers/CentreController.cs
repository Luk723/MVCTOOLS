using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASM_Tools.Controllers
{
    public class CentreController : Controller
    {
        // GET: Centre
        public ActionResult Index()
        {
            ViewBag.LinkText = "Centres";
            return View();
        }

        public ActionResult Singapore()
        {
            ViewBag.LinkText = "Centres";
            return View();
        }
        public ActionResult China()
        {
            ViewBag.LinkText = "Centres";
            return View();
        }
        public ActionResult Taiwan()
        {
            ViewBag.LinkText = "Centres";
            return View();
        }
        public ActionResult HongKong()
        {
            ViewBag.LinkText = "Centres";
            return View();
        }
        public ActionResult Netherland()
        {
            ViewBag.LinkText = "Centres";
            return View();
        }
    }
}