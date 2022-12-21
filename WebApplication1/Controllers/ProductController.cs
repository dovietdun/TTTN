using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objbanhangEntities16.Products.Where(n => n.id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}