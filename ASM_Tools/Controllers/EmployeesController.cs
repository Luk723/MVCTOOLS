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

namespace ASM_Tools.Controllers
{
    public class EmployeesController : Controller
    {
        private ToolContext db = new ToolContext();

        // GET: Employees
        public ActionResult Index(string team, string searchString)
        {


            var teamList = new List<string>();

            var TeamQry = from e in db.Employees
                           orderby e.team
                           select e.team.ToString();

            teamList.AddRange(TeamQry.Distinct());
            ViewBag.team = new SelectList(teamList);

            var Employees = from e in db.Employees
                            select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                Employees = Employees.Where(e => e.LastName.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(team))
            {
                Employees = Employees.Where(e => e.team.ToString() == team);
            }

            return View(Employees.ToList());
        }

        // GET: Employees/Details/5
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
            var Results = from t in db.Tools
                          select new
                          {
                              t.ToolID,
                              t.Title,
                              Checked = ((from te in db.ToolToEmployees
                                          where (te.EmployeeID == id) & (te.ToolID == t.ToolID)
                                          select te).Count() > 0)
                          };

            var MyViewmodel = new EmployeeViewModel();

            MyViewmodel.employee = employee;

            var MyCheckBoxList = new List<CheckBoxEmployeeViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxEmployeeViewModel { Id = item.ToolID, ToolName = item.Title, Checked = item.Checked });
            }

            MyViewmodel.Tools = MyCheckBoxList;

            return View(MyViewmodel);

        }

        //// GET: Employees/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////POST: Employees/Create
        ////To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,salutation,JoinedDate,JobTitle,Department,CompanyEmail,ResumePath,DisplayPhotoPath,team")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Employees.Add(employee);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(employee);
        //}

        //// GET: Employees/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //// POST: Employees/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,salutation,JoinedDate,JobTitle,Department,CompanyEmail,ResumePath,DisplayPhotoPath,team")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(employee).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(employee);
        //}

        //// GET: Employees/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //// POST: Employees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    db.Employees.Remove(employee);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
