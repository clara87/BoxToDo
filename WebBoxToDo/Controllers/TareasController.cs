using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace WebBoxToDo.Controllers
{
    public class TareasController : Controller
    {
        BoxToDo_Contexto ctx = new BoxToDo_Contexto();
        // GET: Tareas
        public ActionResult Index()
        {
            List<Tarea> tareas = ctx.Tarea.ToList();
            return View(tareas);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Tarea tarea)
        {
            tarea.FechaCreacion = DateTime.Now;
            tarea.IdUsuario = 2;
            tarea.Completada = 0;
            ctx.Tarea.Add(tarea);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}