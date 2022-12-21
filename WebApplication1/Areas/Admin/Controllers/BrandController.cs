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
    public class BrandController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();
        // GET: Admin/Brand
        public ActionResult Index()
        {
            var lstBrand = objbanhangEntities16.Brands.ToList();
            return View(lstBrand);
        }

        public ActionResult Details(int id)
        {
            var objBrand = objbanhangEntities16.Brands.Where(n => n.id == id).FirstOrDefault();
            return View(objBrand);

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            try
            {
                if (objBrand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objBrand.Avatar = fileName;
                    objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/Brand"), fileName));
                }
                objBrand.CreatedOnUtc = DateTime.Now;
                objbanhangEntities16.Brands.Add(objBrand);
                objbanhangEntities16.SaveChanges();
                return RedirectToAction("Index");


            }
            catch (Exception)
            {

                return RedirectToAction("Index");

            }


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var objBrand = objbanhangEntities16.Brands.Where(n => n.id == id).FirstOrDefault();
            return View(objBrand);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Brand objBrand)
        {
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                fileName = fileName + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objBrand.Avatar = fileName;
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }
            objbanhangEntities16.Entry(objBrand).State = (System.Data.Entity.EntityState)EntityState.Modified;
            objbanhangEntities16.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}