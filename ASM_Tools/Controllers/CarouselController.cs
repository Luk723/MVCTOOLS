using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASM_Tools.Models;
using System.IO;

namespace ASM_Tools.Controllers
{
    public class CarouselController : Controller
    {
        private CarouselDBContext db = new CarouselDBContext();

        // GET: Carousel
        public ActionResult Index()
        {
            ViewBag.LinkText = "Editor";
            return View(db.CarouselImages.ToList());
        }

        // GET: Carousel/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.LinkText = "Editor";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carousel carousel = db.CarouselImages.Find(id);
            if (carousel == null)
            {
                return HttpNotFound();
            }
            return View(carousel);
        }

        // GET: Carousel/Create
        public ActionResult Create()
        {
            ViewBag.LinkText = "Editor";
            return View();
        }

        // POST: Carousel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ImagePath,Title,Content,ImageFile")] Carousel carousel)
        {

            string imageName = Path.GetFileNameWithoutExtension(carousel.ImageFile.FileName);
            string imageExtension = Path.GetExtension(carousel.ImageFile.FileName);
            string fullName = imageName + imageExtension;
            carousel.ImagePath = "~/Assets/images/" + fullName;
            fullName = Path.Combine(Server.MapPath("~/Assets/images/" + fullName));

            if (ModelState.IsValid)
            {
                carousel.ImageFile.SaveAs(fullName);
                db.CarouselImages.Add(carousel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carousel);
        }

        // GET: Carousel/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.LinkText = "Editor";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carousel carousel = db.CarouselImages.Find(id);
            if (carousel == null)
            {
                return HttpNotFound();
            }
            return View(carousel);
        }

        // POST: Carousel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ImagePath,Title,Content")] Carousel carousel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carousel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carousel);
        }

        // GET: Carousel/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.LinkText = "Editor";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carousel carousel = db.CarouselImages.Find(id);
            if (carousel == null)
            {
                return HttpNotFound();
            }
            return View(carousel);
        }

        // POST: Carousel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carousel carousel = db.CarouselImages.Find(id);

            string fullPath = Server.MapPath(carousel.ImagePath);
            if (System.IO.File.Exists(fullPath))
            {

                System.IO.File.Delete(fullPath);
            }

            db.CarouselImages.Remove(carousel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
