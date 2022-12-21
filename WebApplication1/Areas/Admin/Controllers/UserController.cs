using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();
        // GET: Admin/User
        public ActionResult Index()
        {
            var lstUser = objbanhangEntities16.Users.ToList();
            return View(lstUser);
        }
        public ActionResult Details(int id)
        {
            var objUser = objbanhangEntities16.Users.Where(n => n.id == id).FirstOrDefault();
            return View(objUser);

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objUser = objbanhangEntities16.Users.Where(n => n.id == id).FirstOrDefault();
            return View(objUser);
        }


        [HttpPost]
        public ActionResult Delete(User objUser)
        {
            var objUsers = objbanhangEntities16.Users.Where(n => n.id == objUser.id).FirstOrDefault();

            objbanhangEntities16.Users.Remove(objUser);
            objbanhangEntities16.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(User objUser)
        {
            
                objbanhangEntities16.Users.Add(objUser);
                objbanhangEntities16.SaveChanges();
                return View();
        }

    }
}