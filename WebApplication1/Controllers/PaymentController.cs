using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;


namespace WebApplication1.Controllers
{
    public class PaymentController : Controller
    {
        banhangEntities16 objbanhangEntities16 = new banhangEntities16();

        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"]== null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "Donhang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objbanhangEntities16.Orders.Add(objOrder);
                objbanhangEntities16.SaveChanges();

                int intOrderId = objOrder.id;
                List<OrderDetail> lstOrderDetails = new List<OrderDetail>();
                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.OrderId = item.Product.id;
                    lstOrderDetails.Add(obj);
                }
                objbanhangEntities16.OrderDetails.AddRange(lstOrderDetails);
                objbanhangEntities16.SaveChanges();
            }
           return View();
        }
    }
}