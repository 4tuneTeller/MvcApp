using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Array contacts;
            using (AddressBookEntities context = new AddressBookEntities())
            {
                var query = from c in context.AddressBook
                            orderby c.FullName
                            select c;
                contacts = query.ToArray();
            }

            return View(contacts);
        }
    }
}