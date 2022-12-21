using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var lstOrder = objbanhangEntities16.Orders.ToList();
            return View(lstOrder);
        }
    }
}