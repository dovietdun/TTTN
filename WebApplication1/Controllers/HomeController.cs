using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;



namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();

        public banhangEntities16 GetObjbanhangEntities16()
        {
            return objbanhangEntities16;
        }

        public ActionResult Index(banhangEntities16 objbanhangEntities16)
        {
            
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objbanhangEntities16.Categories.ToList();
            objHomeModel.ListProduct = objbanhangEntities16.Products.ToList();
            return View(objHomeModel); 
        }

        [HttpGet]
        public ActionResult Register()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objbanhangEntities16.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objbanhangEntities16.Configuration.ValidateOnSaveEnabled = false;
                    objbanhangEntities16.Users.Add(_user);
                    objbanhangEntities16.SaveChanges();
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.error = "Email đã đăng ký";
                    return View();
                }


            }
            return View("Index");
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid) //ktra du lieu hop le
            {


                var f_password = GetMD5(password);
                var data = objbanhangEntities16.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


    }
}