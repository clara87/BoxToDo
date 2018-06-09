using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBoxToDo.Models;

namespace WebBoxToDo.Controllers
{
    public class CarpetasController : Controller
    {
        BoxToDo_Contexto ctx = new BoxToDo_Contexto();

        // GET: Carpetas
        public ActionResult Index()
        {
            List<Carpeta> carpetas = ctx.Carpeta.ToList();
            return View(carpetas);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Carpeta carpeta)
        {
            carpeta.FechaCreacion = DateTime.Now;


            ctx.Carpeta.Add(carpeta);

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Tareas(int idCarpeta)
        {
            //if (idCarpeta != null)
            //{
            //    List<Tarea> misTareas = ctx.Tarea.Where(a => a.IdCarpeta == idCarpeta , a => a.IdCarpeta == null).ToList();
            //    return View(misTareas);
            //}
                return View("Index");
        }
    }
}