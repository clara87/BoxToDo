using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using ServiceLayer.Services;

namespace WebBoxToDo.Controllers
{
    public class TareasController : Controller
    {
        BoxToDo_Contexto ctx = new BoxToDo_Contexto();
        TareaService tareaService = new TareaService();

        // GET: Tareas
        public ActionResult Index()
        {
            List<Tarea> tareas = tareaService.ListarTareas();
            return View(tareas);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Tarea tarea)
        {
            tareaService.CrearTarea(tarea);
            return RedirectToAction("Index");
        }

    }
}