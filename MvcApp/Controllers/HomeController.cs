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
                contacts = context.AddressBook.GroupBy(c => c.Department).AsEnumerable().Select(g => new GroupedContacts { Group = g.Key, Contacts = g.OrderBy(c => c.FullName).ToArray() }).ToArray();
            }

            return View(contacts);
        }

        // Get: Home/Login
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
            try
            {
                if (username == "Test1" && password == "Test2")
                {
                    FormsAuthentication.SetAuthCookie(username, true);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Неверная комбинация логина и пароля";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ошибка! " + ex.Message;
                return View();
            }
        }

        public struct GroupedContacts
        {
            public string Group;
            public AddressBook[] Contacts;

            public GroupedContacts(string group, AddressBook[] contacts)
            {
                Group = group;
                Contacts = contacts;
            }
        }
    }
}