using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();
        // GET: Admin/Home
        public ActionResult Index()
        {
            //ktra user dang nhap
              /*  if (Session["idUser"] != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }*/
            return View();

        }
    }
}