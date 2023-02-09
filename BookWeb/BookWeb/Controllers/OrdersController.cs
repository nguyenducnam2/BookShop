using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookWeb.Models;

namespace BookWeb.Controllers
{
    public class OrdersController : Controller
    {
        private BookDBEntities db = new BookDBEntities();

        // GET: Orders
        public ActionResult Index()
        {
            Customer customer = (Customer)Session["Customer"];
            if (customer == null)
            {
                return RedirectToAction("login", "Home");
            }
            var orders = db.Orders.Include(o => o.Customer);
            return View(orders.ToList().Where(o=>o.CustomerId==customer.Id));
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Customer"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
