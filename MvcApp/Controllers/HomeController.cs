using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : Controller // корневой контроллер
    {
        // GET: Home
        [Authorize]
        public ActionResult Index() 
        {
            return RedirectToRoute("Main"); // при переходе на корень, перенаправлять пользователя на /main 
        }
    }
}