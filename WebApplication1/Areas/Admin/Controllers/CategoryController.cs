using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var lstCategory = objbanhangEntities16.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult Details(int id)
        {
            var objCategory = objbanhangEntities16.Categories.Where(n => n.id == id).FirstOrDefault();
            return View(objCategory);

        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            try
            {
                if (objCategory.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objCategory.Avatar = fileName;
                    objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/Category"), fileName));
                }
                objCategory.CreatedOnUtc = DateTime.Now;
                objbanhangEntities16.Categories.Add(objCategory);
                objbanhangEntities16.SaveChanges();
                return RedirectToAction("Index");


            }
            catch (Exception)
            {

                return RedirectToAction("Index");

            }

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            var objCategory = objbanhangEntities16.Categories.Where(n => n.id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Delete(Category objCategory)
        {
            var lstCategory = objbanhangEntities16.Categories.Where(n => n.id == objCategory.id).FirstOrDefault();

            objbanhangEntities16.Categories.Attach(objCategory);
            objbanhangEntities16.Entry(objCategory).State = System.Data.Entity.EntityState.Deleted;
            objbanhangEntities16.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {

            var objCategory = objbanhangEntities16.Categories.Where(n => n.id == id).FirstOrDefault();
            return View(objCategory);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Category objCategory)
        {
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objCategory.Avatar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }
            objbanhangEntities16.Entry(objCategory).State = (System.Data.Entity.EntityState)EntityState.Modified;
            objbanhangEntities16.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}