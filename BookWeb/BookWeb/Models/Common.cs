using BookWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWeb.Models
{
    public class Common
    {
        public Common() { }
        public IEnumerable<Category> Categories
        {
            get
            {
                BookDBEntities bookDB = new BookDBEntities();
                return bookDB.Categories;
            }
        }

        public IEnumerable<Book> Books
        {
            get
            {
               BookDBEntities bookDB = new BookDBEntities();
               return bookDB.Books;
            }
        }
    }
}