using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookWeb.Models;

namespace BookWeb.Controllers
{
    public class CartController : Controller
    {
        private BookDBEntities db= new BookDBEntities();
        // GET: Cart
        public ActionResult Index()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                return View(cart.Items);
            }
            return View();
        }

        public ActionResult count()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.Items.Count},JsonRequestBehavior.AllowGet);
            }
            return Json(new {Count=0}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult add(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1 ,count=0};
            var db = new BookDBEntities();
            var check=db.Books.FirstOrDefault(x=>x.Id== id);
            if (check != null)
            {
                Cart cart = (Cart)Session["Cart"];
                if (cart == null)
                {
                    cart = new Cart();
                }
                CartItem item = new CartItem
                {
                    BookId = check.Id,
                    BookTitle = check.Title,
                    CategoryName = check.Category.name,
                    Price = (int)check.Price,
                    Quantity = quantity
                };
                if (check.Image != null)
                {
                    item.BookImage = check.Image;
                }
                item.Total = item.Price * item.Quantity;
                cart.add(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm vaò giỏ hàng thành công", code = -1,count=cart.Items.Count };
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, count = 0 };
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                var db = new BookDBEntities();
                var check = db.Books.FirstOrDefault(x => x.Id == id);
                if(check!= null)
                {
                    cart.remove(id);
                    code = new { Success = true, msg = "", code = -1, count = cart.Items.Count };
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult clear() {
            Cart cart = (Cart)Session["Cart"];
            if(cart != null)
            {
                cart.clear();
                return Json(new { Success = true });
            }
            return Json(new {Success=false});
        }

        public ActionResult checkOut()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                return View(cart.Items);
            }
            return View();
        }

        [HttpPost]
        public ActionResult checkOut(String address,String payment)
        {
            Cart cart = (Cart)Session["Cart"];
            Customer customer = (Customer)Session["Customer"];
            if (cart != null)
            {
                Order order = new Order
                {
                    CustomerId = customer.Id,
                    Address= address,
                    Payment= payment,
                    Status=1,
                    Total=cart.getTotal()
                };
                db.Orders.Add(order);
                foreach(CartItem item in cart.Items)
                {
                    OrdersItem ordersItem = new OrdersItem
                    {
                        OrderId=order.Id,
                        Order = order,
                        BookId=item.BookId,
                        Quantity=item.Quantity,
                        Subtotal=item.Total
                    };
                    db.OrdersItems.Add(ordersItem);
                }
                db.SaveChanges();
                Session["Cart"] = null;
            }
            ViewBag.Mess = "Bạn Đã Đặt Hàng Thành Công.Goozi Sẽ Xác Nhận Đơn Hàng Qua Email              ";
            return View();
        }

        public ActionResult partialCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                return View(cart.Items);
            }
            return View();
        }
    }
}