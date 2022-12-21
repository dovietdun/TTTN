using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();

        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objbanhangEntities16.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objbanhangEntities16.Products.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct); 
        }
    }
}