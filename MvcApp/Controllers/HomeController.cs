using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;
using System.Web.Security;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
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

        // Get: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            if (username == "Test1" && password == "Test2")
            {
                FormsAuthentication.SetAuthCookie(username, true);
                return RedirectToAction("Index");
            }
            else
                return View();
        }
    }
}