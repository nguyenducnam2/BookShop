using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookWeb.Models;

namespace BookWeb.Controllers
{
    public class ProductsController : Controller
    {
        private BookDBEntities db = new BookDBEntities();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            return View(db.Books.Find(id));
        }
    }
}