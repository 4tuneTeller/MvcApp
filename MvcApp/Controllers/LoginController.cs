using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
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
                    return RedirectToRoute("Main");
                }
                else
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