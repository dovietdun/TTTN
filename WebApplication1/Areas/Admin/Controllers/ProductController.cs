using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;
using static WebApplication1.Commom;

namespace WebApplication1.Areas.Admin.Controllers
{
  
    public class ProductController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>(); 
            if(SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if(!string.IsNullOrEmpty(SearchString))
            {
                //lay theo search
                lstProduct = objbanhangEntities16.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lay trong product
                lstProduct = objbanhangEntities16.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            // so item 1 trang = 4
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //sap xeo theo id, sp moi len dau trang
            lstProduct = lstProduct.OrderByDescending(n => n.id).ToList();


            return View(lstProduct.ToPagedList(pageNumber, pageSize));
            /* var lstProduct = objbanhangEntities16.Products.ToList();
           // var lstProduct = objbanhangEntities16.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            return View(lstProduct);*/
        }

        [HttpGet]
        public ActionResult Create()
        {
           this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
           /* if (ModelState.IsValid)*/
            
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objbanhangEntities16.Products.Add(objProduct);
                    objbanhangEntities16.SaveChanges();
                    return RedirectToAction("Index");



                }

                catch (Exception)
                {

                    return RedirectToAction("Index", "Home");

                }
            
            return View(objProduct);

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objbanhangEntities16.Products.Where(n=>n.id==id).FirstOrDefault();
            return View(objProduct);  
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objbanhangEntities16.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }


        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objbanhangEntities16.Products.Where(n => n.id == objPro.id).FirstOrDefault();

            objbanhangEntities16.Products.Remove(objProduct);
            objbanhangEntities16.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objProduct = objbanhangEntities16.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
           
            this.LoadData();
          

                if (objProduct.ImageUpload != null)
                {
               

                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                }
                objbanhangEntities16.Entry(objProduct).State = (System.Data.Entity.EntityState)EntityState.Modified;
           
                objbanhangEntities16.SaveChanges();
                return RedirectToAction("Index");
        }


        void LoadData()
        {
            Commom commom = new Commom();
            //lay duoi db 
            var lstCat = objbanhangEntities16.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = commom.ToSelectList(dtCategory, "Id", "Name");



            var lstBrand = objbanhangEntities16.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = commom.ToSelectList(dtBrand, "Id", "Name");



            List<ProdType> lstProdType = new List<ProdType>();
            ProdType objProdType = new ProdType();
            objProdType.Id = 01;
            objProdType.Name = "Giảm giá sốc";
            lstProdType.Add(objProdType);


            objProdType = new ProdType();
            objProdType.Id = 02;
            objProdType.Name = "Đề xuất";
            lstProdType.Add(objProdType);

            DataTable dtProdType = converter.ToDataTable(lstProdType);
            ViewBag.ProdType = commom.ToSelectList(dtProdType, "Id", "Name");
        }


    }
}