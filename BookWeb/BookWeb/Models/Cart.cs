using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWeb.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set;}
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void add(CartItem item,int quantity)
        {
            var check=Items.FirstOrDefault(x=>x.BookId==item.BookId);
            if (check != null)
            {
                check.Quantity += quantity;
                check.Total = check.Price* check.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public void remove(int id)
        {
            var check = Items.SingleOrDefault(x => x.BookId == id);
            if(check!= null)
            {
                Items.Remove(check);
            }
        }

        public void updateQuantity(int id,int quantity)
        {
            var check=Items.SingleOrDefault(x => x.BookId == id);
            if (check!= null) {
                check.Quantity = quantity;
                check.Total=check.Price*check.Quantity;
            }
        }

        public int getTotal()
        {
            return Items.Sum(o=>o.Total);
        }

        public int getTotalQuantity()
        {
            return Items.Sum(o => o.Quantity);
        }

        public void clear()
        {
            Items.Clear();
        }
    }

    public class CartItem
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookImage { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int Price;
        public int Total;
    }
}