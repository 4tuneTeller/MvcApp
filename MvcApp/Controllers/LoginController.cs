using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcApp.Controllers
{
    public class LoginController : Controller // контроллер страницы авторизации
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            FormsAuthentication.SignOut(); // при переходе на строницу login пользователь автоматически разлогинивается (log out)
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password) // обработка POST-запроса Login
        {
            try
            {
                if (username == "Test1" && password == "Test2") // проверка введенных пользователем данных
                {
                    FormsAuthentication.SetAuthCookie(username, true); // если данные верны - логиним пользователя, сохранив куки
                    return RedirectToRoute("Main"); // перенаправляем на страницу main
                }
                else // если данные неверны, отображаем на странице сообщение
                {
                    ViewBag.Message = "Неверная комбинация логина и пароля";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ошибка! " + ex.Message;
                return View("Index");
            }
        }
    }
}