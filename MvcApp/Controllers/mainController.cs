using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class MainController : Controller
    {
        // GET: main
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