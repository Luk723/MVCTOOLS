using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASM_Tools.DAL;
using ASM_Tools.Models;
using System.IO;

namespace ASM_Tools.Controllers
{
    public class EditorEmployeeController : Controller
    {
        private ToolContext db = new ToolContext();

        // GET: EditorEmployee
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: EditorEmployee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: EditorEmployee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditorEmployee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,salutation,JoinedDate,JobTitle,Department,CompanyEmail,ResumePath,DisplayPhotoPath,team,DisplayPhotoFile,ResumeFile")] Employee employee)
        {

            string root = Server.MapPath("~/Assets/Employees/" + employee.ID); // ~/Assets/products/software1
            string PhotoFolder = root + "\\photo folder";
            string ResumeFolder = root + "\\resume";

            string PhotoFullName = "";
            //cover photo
            if(employee.DisplayPhotoFile != null)
            {
                string imageName = Path.GetFileNameWithoutExtension(employee.DisplayPhotoFile.FileName);  //image
                string imageExtension = Path.GetExtension(employee.DisplayPhotoFile.FileName);  //.jpg
                PhotoFullName = imageName + imageExtension;  //image.jpg

                employee.DisplayPhotoPath = "~/Assets/Employees/" + employee.ID + "/photo folder/" + PhotoFullName;  //  ~/Assets/products/software1/cover image/image.jpg
                PhotoFullName = Path.Combine(Server.MapPath("~/Assets/Employees/" + employee.ID + "/photo folder/" + PhotoFullName));
            }

            string ResumeFullName = "";
            //resume
            if(employee.ResumeFile != null)
            {

                string resumeName = Path.GetFileNameWithoutExtension(employee.ResumeFile.FileName);  //image
                string resumeExtension = Path.GetExtension(employee.ResumeFile.FileName);  //.jpg
                ResumeFullName = resumeName + resumeExtension;  //image.jpg

                employee.ResumePath = "~/Assets/Employees/" + employee.ID + "/resume/" + ResumeFullName;  //  ~/Assets/products/software1/cover image/image.jpg
                ResumeFullName = Path.Combine(Server.MapPath("~/Assets/Employees/" + employee.ID + "/resume/" + ResumeFullName));
            }



            if (ModelState.IsValid)
            {
                Directory.CreateDirectory(root);
                Directory.CreateDirectory(PhotoFolder);
                Directory.CreateDirectory(ResumeFolder);

                if (employee.DisplayPhotoFile != null)
                {
                    employee.DisplayPhotoFile.SaveAs(PhotoFullName);
                }
                if (employee.ResumeFile != null) 
                {
                    employee.ResumeFile.SaveAs(ResumeFullName);
                }


                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: EditorEmployee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: EditorEmployee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,salutation,JoinedDate,JobTitle,Department,CompanyEmail,ResumePath,DisplayPhotoPath,team")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: EditorEmployee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: EditorEmployee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
