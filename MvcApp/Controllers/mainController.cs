using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class MainController : Controller // контроллер страницы с таблицей (main)
    {
        // GET: main
        [Authorize] // доступ к этой странице разрешен только авторизованным пользователем, если пользователь не авторизован - его перенаправляет на страницу login (настроено в  Web.config)
        public ActionResult Index()
        {
            try
            {
                Array contacts;
                using (AddressBookEntities context = new AddressBookEntities())
                {
                    // запрос в базу данных: группируем записи по Департаменту, а затем в каждой группе сортируем записи по ФИО
                    contacts = context.AddressBook.GroupBy(c => c.Department).AsEnumerable().Select(g => new GroupedContacts { Group = g.Key, Contacts = g.OrderBy(c => c.FullName).ToArray() }).ToArray();
                }

                return View(contacts);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ошибка! " + ex.Message;
                return View();
            }
        }

        // структура, которую пришлось ввести для сортировки записей внутри группы linq-запросом
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