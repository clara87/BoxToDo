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
            var idUsuario = Session["id"];
            int idUsu = Convert.ToInt32(idUsuario);

            if (Session["login"] is true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }


       
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


        public ActionResult TareasIncompletas()
        {
            var idUsuario = Session["id"];
            int idUsu = Convert.ToInt32(idUsuario);

            if (Session["login"] is true)
            {
                List<Tarea> tareasIncompletas = tareaService.ListarTareasIncompletas(idUsu);
                return PartialView(tareasIncompletas);
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult TareasCompletas()
        {
            var idUsuario = Session["id"];
            int idUsu = Convert.ToInt32(idUsuario);

            if (Session["login"] is true)
            {
                List<Tarea> tareasCompletas = tareaService.ListarTareasCompletas(idUsu);
                return PartialView("TareasIncompletas", tareasCompletas);
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Detalle(int id)
            {
                if (Session["login"] is true)
                {
                    var idUsuario = Session["id"];
                    int idUsu = Convert.ToInt32(idUsuario);
                    ViewBag.nombreCarpeta = tareaService.NombreCarpeta(id);
                    Tarea miTareas = tareaService.DetalleTarea(id, idUsu);
           

                    return View(miTareas);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }

        public ActionResult ListarComentarios(int id)
        {
            List<ComentarioTarea> comentarios = tareaService.Comentarios(id);
            return PartialView(comentarios);
        }

        
    }
}