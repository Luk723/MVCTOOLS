using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASM_Tools.DAL;
using ASM_Tools.Models;

namespace ASM_Tools.Controllers
{
    public class ToolsController : Controller
    {
        private ToolContext db = new ToolContext();

        // GET: Tools
        public ActionResult Index(string searchString)
        {
            ViewBag.LinkText = "Tools";

            var Tools = from t in db.Tools
                        select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                Tools = Tools.Where(t => t.Title.Contains(searchString));
            }
            return View(Tools.ToList());
            
        }

        // GET: Tools/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.LinkText = "Tools";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tool tool = db.Tools.Find(id);
            if (tool == null)
            {
                return HttpNotFound();
            }

            var Results = from e in db.Employees
                          select new
                          {
                              employee = e,
                              Checked = ((from te in db.ToolToEmployees
                                          where (te.ToolID == id) & (te.EmployeeID == e.ID)
                                          select te).Count() > 0),
                              Role = ((from te in db.ToolToEmployees
                                       where (te.ToolID == id) & (te.EmployeeID == e.ID)
                                       select te.Role).FirstOrDefault())

                          };

            var MyViewmodel = new ToolViewModel();

            MyViewmodel.tool = tool;

            var MyCheckBoxList = new List<CheckBoxToolViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxToolViewModel { Employee = item.employee, Checked = item.Checked, Role = item.Role });
            }

            List<Employee> ees = db.Employees.ToList();
            ViewBag.E = ees;

            MyViewmodel.Employees = MyCheckBoxList;

            return View(MyViewmodel);

        }

        //// GET: Tools/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Tools/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ToolID,Title,Description,Tag,CoverImagePath,GalleryPath,DocumentationPath,InstallationPath,VideoPath")] Tool tool)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tools.Add(tool);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(tool);
        //}

        //// GET: Tools/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tool tool = db.Tools.Find(id);
        //    if (tool == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tool);
        //}

        //// POST: Tools/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ToolID,Title,Description,Tag,CoverImagePath,GalleryPath,DocumentationPath,InstallationPath,VideoPath")] Tool tool)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tool).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(tool);
        //}

        //// GET: Tools/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tool tool = db.Tools.Find(id);
        //    if (tool == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tool);
        //}

        //// POST: Tools/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Tool tool = db.Tools.Find(id);
        //    db.Tools.Remove(tool);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public FilePathResult DownloadFile(string FileName)
        {
            return File(FileName, System.Net.Mime.MediaTypeNames.Application.Octet, Server.UrlEncode(FileName));
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
