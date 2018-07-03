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

        TareaService tareaService = new TareaService();

        // GET: Tareas
        public ActionResult Index()
        {
            if (Session["login"] is true)
            {
                var idUsuario = Session["id"];
                int idUsu = Convert.ToInt32(idUsuario);
                List<Tarea> tareas = tareaService.ListarTareas(idUsu);
                return View(tareas);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Crear()
        {
            return View();
        }


        //VER idCarpeta
        [HttpPost]
        public ActionResult Crear(Tarea tarea)
        {
            var idUsuario = Session["id"];
            int idUsu = Convert.ToInt32(idUsuario);

            if (Session["login"] is true)
            {
                tareaService.CrearTarea(tarea, idUsu);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

    }
}