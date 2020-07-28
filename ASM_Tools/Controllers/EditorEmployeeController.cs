using ASM_Tools.DAL;
using ASM_Tools.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ASM_Tools.Controllers
{
    public class EditorEmployeeController : Controller
    {
        private ToolContext db = new ToolContext();

        // GET: EditorEmployee
        public ActionResult Index()
        {
            ViewBag.LinkText = "Editor";
            return View(db.Employees.ToList());
        }

        // GET: EditorEmployee/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.LinkText = "Editor";
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
                              tool = t,
                              Checked = ((from te in db.ToolToEmployees
                                          where (te.EmployeeID == id) & (te.ToolID == t.ToolID)
                                          select te).Count() > 0),
                              Role = ((from te in db.ToolToEmployees
                                          where (te.EmployeeID == id) & (te.ToolID == t.ToolID)
                                          select te.Role).FirstOrDefault())

                          };

            var MyViewmodel = new EmployeeViewModel();

            MyViewmodel.employee = employee;

            var MyCheckBoxList = new List<CheckBoxEmployeeViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxEmployeeViewModel { tool = item.tool, Checked = item.Checked, Role = item.Role });
            }

            MyViewmodel.Tools = MyCheckBoxList;

            return View(MyViewmodel);


        }

        public JsonResult IsEmployeeIDExist(int ID)
        {
            var validateID = db.Employees.FirstOrDefault(x => x.ID == ID);
            if (validateID != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: EditorEmployee/Create
        public ActionResult Create()
        {
            ViewBag.LinkText = "Editor";
            return View();
        }

        // POST: EditorEmployee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,salutation,JoinedDate,JobTitle,Department,CompanyEmail,ResumePath,DisplayPhotoPath,team,DisplayPhotoFile,ResumeFile,EmployeeNumber")] Employee employee)
        {

            string root = Server.MapPath("~/Assets/Employees/" + employee.ID); // ~/Assets/products/software1
            string PhotoFolder = root + "\\photo folder";
            string ResumeFolder = root + "\\resume";

            string PhotoFullName = "";
            //cover photo
            if (employee.DisplayPhotoFile != null)
            {
                string imageName = Path.GetFileNameWithoutExtension(employee.DisplayPhotoFile.FileName);  //image
                string imageExtension = Path.GetExtension(employee.DisplayPhotoFile.FileName);  //.jpg
                PhotoFullName = imageName + imageExtension;  //image.jpg

                employee.DisplayPhotoPath = "~/Assets/Employees/" + employee.ID + "/photo folder/" + PhotoFullName;  //  ~/Assets/products/software1/cover image/image.jpg
                PhotoFullName = Path.Combine(Server.MapPath("~/Assets/Employees/" + employee.ID + "/photo folder/" + PhotoFullName));
            }

            string ResumeFullName = "";
            //resume
            if (employee.ResumeFile != null)
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
            ViewBag.LinkText = "Editor";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            TempData["DisplayPhotoPath"] = employee.DisplayPhotoPath;
            TempData["ResumePath"] = employee.ResumePath;

            var Results = from t in db.Tools
                          select new
                          {
                              tool = t,
                              Checked = ((from te in db.ToolToEmployees
                                          where (te.EmployeeID == id) & (te.ToolID == t.ToolID)
                                          select te).Count() > 0),
                              Role = ((from te in db.ToolToEmployees
                                       where (te.EmployeeID == id) & (te.ToolID == t.ToolID)
                                       select te.Role).FirstOrDefault())
                          };

            var MyViewmodel = new EmployeeViewModel();

            MyViewmodel.employee = employee;

            var MyCheckBoxList = new List<CheckBoxEmployeeViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxEmployeeViewModel { tool = item.tool, Checked = item.Checked, Role = item.Role});
            }

            MyViewmodel.Tools = MyCheckBoxList;

            return View(MyViewmodel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel employeeView)
        {
            if (ModelState.IsValid)
            {

                var MyEmployee = db.Employees.Find(employeeView.ID);

                if (employeeView.DisplayPhotoFile != null)
                {
                    //cover photo
                    string imageName = Path.GetFileNameWithoutExtension(employeeView.DisplayPhotoFile.FileName);  //image
                    string imageExtension = Path.GetExtension(employeeView.DisplayPhotoFile.FileName);  //.jpg
                    string PhotoFullName = imageName + imageExtension;  //image.jpg

                    string oldPhoto = Request.MapPath(TempData["DisplayPhotoPath"].ToString());

                    MyEmployee.DisplayPhotoPath = "~/Assets/Employees/" + employeeView.employee.ID + "/photo folder/" + PhotoFullName;  //  ~/Assets/products/software1/cover image/image.jpg
                    PhotoFullName = Path.Combine(Server.MapPath("~/Assets/Employees/" + employeeView.employee.ID + "/photo folder/" + PhotoFullName));

                    //Delete old cover image
                    if (System.IO.File.Exists(oldPhoto))
                    {
                        System.IO.File.Delete(oldPhoto);
                    }

                    MyEmployee.DisplayPhotoFile = employeeView.DisplayPhotoFile;

                    MyEmployee.DisplayPhotoFile.SaveAs(PhotoFullName);

                }
                //if no file chosen keep the original
                else
                {
                    if(MyEmployee.DisplayPhotoPath != null)
                    {
                        MyEmployee.DisplayPhotoPath = TempData["DisplayPhotoPath"].ToString();
                    }
                }

                if (employeeView.employee.ResumeFile != null)
                {
                    //cover photo
                    string resumeName = Path.GetFileNameWithoutExtension(employeeView.employee.ResumeFile.FileName);  //image
                    string resumeExtension = Path.GetExtension(employeeView.employee.ResumeFile.FileName);  //.jpg
                    string ResumeFullName = resumeName + resumeExtension;  //image.jpg

                    string oldResume = Request.MapPath(TempData["ResumePath"].ToString());

                    MyEmployee.ResumePath = "~/Assets/Employees/" + employeeView.employee.ID + "/resume folder/" + ResumeFullName;  //  ~/Assets/products/software1/cover image/image.jpg
                    ResumeFullName = Path.Combine(Server.MapPath("~/Assets/Employees/" + employeeView.employee.ID + "/resume folder/" + ResumeFullName));

                    //Delete old cover image
                    if (System.IO.File.Exists(oldResume))
                    {
                        System.IO.File.Delete(oldResume);
                    }

                    MyEmployee.ResumeFile = employeeView.ResumeFile;
                    MyEmployee.ResumeFile.SaveAs(ResumeFullName);

                }
                //if no file chose keep the original
                else
                {
                    if (MyEmployee.ResumePath != null)
                    {
                        MyEmployee.ResumePath = TempData["ResumePath"].ToString();
                    }
                }


                foreach (var item in db.ToolToEmployees)
                {
                    if (item.ID == employeeView.employee.ID)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var item in employeeView.Tools)
                {
                    if (item.Checked)
                    {
                        var role = item.Role;
                        var tee = from te in db.ToolToEmployees
                                  where te.EmployeeID == employeeView.employee.ID && te.ToolID == item.tool.ToolID
                                  select te;
                        if (tee.Count() == 0)
                        {
                            db.ToolToEmployees.Add(new ToolToEmployee() { EmployeeID = employeeView.employee.ID, ToolID = item.tool.ToolID, Role = item.Role });
                        }
                        else
                        {
                            tee.First().Role = role;
                        }
                    }
                    else
                    {
                        var tee = from te in db.ToolToEmployees
                                  where te.EmployeeID == employeeView.employee.ID && te.ToolID == item.tool.ToolID
                                  select te;
                        if (tee.Count() != 0)
                        {
                            db.ToolToEmployees.Remove(tee.First());
                        }
                    }
                }

                MyEmployee.LastName = employeeView.employee.LastName;
                MyEmployee.FirstMidName = employeeView.employee.FirstMidName;
                MyEmployee.JoinedDate = employeeView.employee.JoinedDate;
                MyEmployee.Department = employeeView.employee.Department;
                MyEmployee.JobTitle = employeeView.employee.JobTitle;
                MyEmployee.CompanyEmail = employeeView.employee.CompanyEmail;
                MyEmployee.team = employeeView.employee.team;
                MyEmployee.salutation = employeeView.employee.salutation;



                db.Entry(MyEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeView);
        }


        // GET: EditorEmployee/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.LinkText = "Editor";
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
            string fullPath = Server.MapPath("~/Assets/Employees/" + employee.ID);
            if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath, true);
            }
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
