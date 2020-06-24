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
    public class EditorToolController : Controller
    {
        private ToolContext db = new ToolContext();

        // GET: Editor
        public ActionResult Index(string searchString)
        {

            var Tools = from t in db.Tools
                        select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                Tools = Tools.Where(t => t.Title.Contains(searchString));
            }
            return View(Tools.ToList());
        }

        // GET: Editor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tool tool = db.Tools.Find(id);
            if (tool == null)
            {
                return HttpNotFound();
            }
            return View(tool);
        }

        // GET: Editor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToolID,Title,Description,Tag,CoverImagePath,GalleryPath,DocumentationPath,InstallationPath,VideoPath,CoverImageFile,DocumentationFiles,InstallationFiles,VideoFiles,GalleryFiles")] Tool tool)
        {

            string root = Server.MapPath("~/Assets/Tools/" + tool.Title); // ~/Assets/products/software1
            string coverFolder = root + "\\cover folder";
            string documentationFolder = root + "\\documentation";
            string galleryFolder = root + "\\gallery";
            string installationFolder = root + "\\installation";
            string videoFolder = root + "\\video";

            int ID = tool.ToolID;

            //cover photo
            string imageName = Path.GetFileNameWithoutExtension(tool.CoverImageFile.FileName);  //image
            string imageExtension = Path.GetExtension(tool.CoverImageFile.FileName);  //.jpg
            string coverFullName = imageName + imageExtension;  //image.jpg

            tool.CoverImagePath = "~/Assets/Tools/" + tool.Title + "/cover folder/" + coverFullName;  //  ~/Assets/products/software1/cover image/image.jpg
            coverFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/cover folder/" + coverFullName));


            //documentation 
            //string documentationFullName = "";
            //if(tool.DocumentationFiles != null)
            //{
            //    string documentation = Path.GetFileNameWithoutExtension(tool.DocumentationFiles.FileName);  //documentation
            //    string documentationExtension = Path.GetExtension(tool.DocumentationFiles.FileName);  //.pdf
            //    documentationFullName = documentation + documentationExtension;  //documentation.pdf

            //    tool.DocumentationPath = "~/Assets/Tools/" + tool.Title + "/documentation/" + documentationFullName;  //  ~/Assets/products/software1/cover image/image.jpg
            //    documentationFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/documentation/" + documentationFullName));
            //}


            //installation 
            //string installationFullName = "";
            //if(tool.InstallationFiles != null)
            //{
            //    string installation = Path.GetFileNameWithoutExtension(tool.InstallationFiles.FileName);  //installation
            //    string installationExtension = Path.GetExtension(tool.InstallationFiles.FileName);  //.exe
            //    installationFullName = installation + installationExtension;  //installation.exe

            //    tool.InstallationPath = "~/Assets/Tools/" + tool.Title + "/installation/" + installationFullName;  //  ~/Assets/products/software1/cover image/image.jpg
            //    installationFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/installation/" + installationFullName));
            //}


            //video
            //string videoFullName = "";
            //if(tool.VideoFile != null)
            //{
            //    string video = Path.GetFileNameWithoutExtension(tool.VideoFile.FileName);  //video
            //    string videoExtension = Path.GetExtension(tool.VideoFile.FileName);  //.mp4
            //    videoFullName = video + videoExtension;  //video.mp4

            //    tool.VideoPath = "~/Assets/Tools/" + tool.Title + "/video/" + videoFullName;  //  ~/Assets/products/software1/cover image/image.jpg
            //    videoFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/video/" + videoFullName));

            //}

            
            tool.GalleryPath = "~/Assets/Tools/" + tool.Title + "/gallery/";  //  ~/Assets/products/software1/gallery/
            tool.DocumentationPath = "~/Assets/Tools/" + tool.Title + "/documentation/";
            tool.InstallationPath = "~/Assets/Tools/" + tool.Title + "/Installation/";
            tool.VideoPath = "~/Assets/Tools/" + tool.Title + "/Video/";

            if (ModelState.IsValid && !Directory.Exists(root))
            {
                //Create folders
                Directory.CreateDirectory(root);
                Directory.CreateDirectory(documentationFolder);
                Directory.CreateDirectory(coverFolder);
                Directory.CreateDirectory(galleryFolder);
                Directory.CreateDirectory(installationFolder);
                Directory.CreateDirectory(videoFolder);

                //save files to respective folders
                //if(tool.DocumentationFile != null)
                //{
                //    tool.DocumentationFile.SaveAs(documentationFullName);
                //}
                //if(tool.VideoFile != null)
                //{
                //    tool.VideoFile.SaveAs(videoFullName);
                //}
                //if(tool.InstallationFile != null)
                //{
                //    tool.InstallationFile.SaveAs(installationFullName);
                //}
                tool.CoverImageFile.SaveAs(coverFullName);

                foreach (HttpPostedFileBase file in tool.GalleryFiles)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        string galleryFileName = Path.GetFileNameWithoutExtension(file.FileName);   //image
                        string galleryFileExtention = Path.GetExtension(file.FileName);  //.jpg
                        string galleryFileFullName = galleryFileName + galleryFileExtention;

                        galleryFileFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/gallery/" + galleryFileFullName)); ;  //image.jpg
                        //Save file to server folder  
                        file.SaveAs(galleryFileFullName);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        
                    }

                }

                foreach (HttpPostedFileBase file in tool.DocumentationFiles)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        string documentFileName = Path.GetFileNameWithoutExtension(file.FileName);   //image
                        string documentFileExtention = Path.GetExtension(file.FileName);  //.jpg
                        string documentFileFullName = documentFileName + documentFileExtention;

                        documentFileFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/documentation/" + documentFileFullName)); ;  //image.jpg
                        //Save file to server folder  
                        file.SaveAs(documentFileFullName);
                        //assigning file uploaded status to ViewBag for showing message to user.  

                    }

                }

                foreach (HttpPostedFileBase file in tool.InstallationFiles)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        string installationFileName = Path.GetFileNameWithoutExtension(file.FileName);   //image
                        string installationExtention = Path.GetExtension(file.FileName);  //.jpg
                        string installationFileFullName = installationFileName + installationExtention;

                        installationFileFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/installation/" + installationFileFullName)); ;  //image.jpg
                        //Save file to server folder  
                        file.SaveAs(installationFileFullName);
                        //assigning file uploaded status to ViewBag for showing message to user.  

                    }

                }

                foreach (HttpPostedFileBase file in tool.InstallationFiles)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        string videoFileName = Path.GetFileNameWithoutExtension(file.FileName);   //image
                        string videoExtention = Path.GetExtension(file.FileName);  //.jpg
                        string videoFileFullName = videoFileName + videoExtention;

                        videoFileFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/video/" + videoFileFullName)); ;  //image.jpg
                        //Save file to server folder  
                        file.SaveAs(videoFileFullName);
                        //assigning file uploaded status to ViewBag for showing message to user.  

                    }

                }



                db.Tools.Add(tool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tool);
        }

        // GET: Editor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tool tool = db.Tools.Find(id);

            TempData["CoverImagePath"] = tool.CoverImagePath;
            TempData["DocumentationPath"] = tool.DocumentationPath;
            TempData["InstallationPath"] = tool.InstallationPath;
            TempData["VideoPath"] = tool.VideoPath;
            TempData["GalleryPath"] = tool.GalleryPath;

            if (tool == null)
            {
                return HttpNotFound();
            }
            return View(tool);
        }

        // POST: Editor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToolID,Title,Description,Tag,CoverImagePath,GalleryPath,DocumentationPath,InstallationPath,VideoPath,CoverImageFile,DocumentationFile,InstallationFile,VideoFile,GalleryFiles")] Tool tool)
        {

            //var ID = tool.CoverImagePath;

            if (ModelState.IsValid)
            {


                //Edit cover image
                if (tool.CoverImageFile != null)
                {
                    //cover photo
                    string imageName = Path.GetFileNameWithoutExtension(tool.CoverImageFile.FileName);  //image
                    string imageExtension = Path.GetExtension(tool.CoverImageFile.FileName);  //.jpg
                    string coverFullName = imageName + imageExtension;  //image.jpg

                    string oldCoverImage = Request.MapPath(TempData["CoverImagePath"].ToString());

                    tool.CoverImagePath = "~/Assets/Tools/" + tool.Title + "/cover folder/" + coverFullName;  //  ~/Assets/products/software1/cover image/image.jpg
                    coverFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/cover folder/" + coverFullName));

                    //Delete old cover image
                    if (System.IO.File.Exists(oldCoverImage))
                    {
                        System.IO.File.Delete(oldCoverImage);
                    }
                    tool.CoverImageFile.SaveAs(coverFullName);

                }
                //if no file chose keep the original
                else
                {
                    tool.CoverImagePath = TempData["CoverImagePath"].ToString();
                }

                //Edit video
                if (tool.CoverImageFile != null)
                {
                    //cover photo
                    string imageName = Path.GetFileNameWithoutExtension(tool.CoverImageFile.FileName);  //image
                    string imageExtension = Path.GetExtension(tool.CoverImageFile.FileName);  //.jpg
                    string coverFullName = imageName + imageExtension;  //image.jpg

                    string oldCoverImage = Request.MapPath(TempData["CoverImagePath"].ToString());

                    tool.CoverImagePath = "~/Assets/Tools/" + tool.Title + "/cover folder/" + coverFullName;  //  ~/Assets/products/software1/cover image/image.jpg
                    coverFullName = Path.Combine(Server.MapPath("~/Assets/Tools/" + tool.Title + "/cover folder/" + coverFullName));

                    //Delete old cover image
                    if (System.IO.File.Exists(oldCoverImage))
                    {
                        System.IO.File.Delete(oldCoverImage);
                    }
                    tool.CoverImageFile.SaveAs(coverFullName);

                }
                else
                {
                    tool.CoverImagePath = TempData["CoverImagePath"].ToString();
                }



                db.Entry(tool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tool);
        }

        // GET: Editor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tool tool = db.Tools.Find(id);
            if (tool == null)
            {
                return HttpNotFound();
            }
            return View(tool);
        }

        // POST: Editor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tool tool = db.Tools.Find(id);


        
            string fullPath = Server.MapPath("~/Assets/Tools/" + tool.Title);
            if (Directory.Exists(fullPath))
            {

                Directory.Delete(fullPath, true);
            }
            db.Tools.Remove(tool);
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
        
        
        public ActionResult ViewTool(int ID, string type)
        {
            Tool tool = db.Tools.Find(ID);
            ViewBag.Type = type;
            return PartialView(tool);
        }


        [HttpPost]
        public ActionResult UploadFiles()
        {
            var data = System.Web.HttpContext.Current.Request.Params["id"];
            var type = System.Web.HttpContext.Current.Request.Params["type"];
            var id = Int32.Parse(data);
            //var id = data.get('id');
            Tool tool = db.Tools.Find(id);

            if (type.Equals("gallery", StringComparison.InvariantCultureIgnoreCase))
            {
                string path = Server.MapPath(tool.GalleryPath);
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    file.SaveAs(path + file.FileName);
                }
                return PartialView("GalleryEditor", tool);
            }
            else if (type.Equals("documentation", StringComparison.InvariantCultureIgnoreCase))
            {
                string path = Server.MapPath(tool.DocumentationPath);
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    file.SaveAs(path + file.FileName);
                }
                return PartialView("DocumentationEditor", tool);
            }
            else
            {
                return PartialView("GalleryEditor", tool);
            }

        }    


        [HttpPost]
        public ActionResult DeleteFile()
        {
            var path = System.Web.HttpContext.Current.Request.Params["path"];
            var data = System.Web.HttpContext.Current.Request.Params["id"];
            var type = System.Web.HttpContext.Current.Request.Params["type"];
            
            int id = Int32.Parse(data);
            Tool tool = db.Tools.Find(id);
            if (System.IO.File.Exists(Server.MapPath(path)))
            {
                System.IO.File.Delete(Server.MapPath(path));
            }

            if(type.Equals("gallery", StringComparison.InvariantCultureIgnoreCase))
            {
                return PartialView("GalleryEditor", tool);
            }
            else if(type.Equals("documentation", StringComparison.InvariantCultureIgnoreCase))
            {
                return PartialView("DocumentationEditor", tool);
            }
            else
            {
                return PartialView("GalleryEditor", tool);
            }
        }

        public FileResult PreviewFile(string FileName)
        {
            byte[] FileBytes = System.IO.File.ReadAllBytes(FileName);
            return File(FileBytes, "application/pdf");
        }
    }
}
