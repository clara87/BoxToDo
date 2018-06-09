using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBoxToDo.Models;

namespace WebBoxToDo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registracion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string contrasenia)
        {

            return View();
        }
    }
}