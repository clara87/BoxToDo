using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using System.Net;
using System.Data.Entity;

namespace WebBoxToDo.Controllers
{
    public class CarpetasController : Controller
    {
        BoxToDo_Contexto ctx = new BoxToDo_Contexto();

        // GET: Carpetas
        public ActionResult Index()
        {
            List<Carpeta> carpetas = ctx.Carpeta.OrderBy(a =>a.Nombre).ToList();
            return View(carpetas);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Carpeta carpeta)
        {
            if (ModelState.IsValid)
            {
                carpeta.FechaCreacion = DateTime.Now;
                ctx.Carpeta.Add(carpeta);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carpeta);
        }



        public ActionResult Tareas(int id)
        {
            List<Tarea> misTareas = ctx.Tarea.OrderBy(a => a.Prioridad).OrderBy(a => a.FechaFin).Where(a => a.IdCarpeta == id).ToList();
            ViewBag.NombreCarpeta = ctx.Carpeta.FirstOrDefault(a => a.IdCarpeta == id).Nombre;
            return View(misTareas);
        }

        //para otro select
        public ActionResult ListarCarpetasOrdenadas()
        {
            List<Carpeta> carpetasOrdenadas = ctx.Carpeta.OrderBy(a => a.IdUsuario).Where(a => a.IdUsuario != null).ToList();
            return PartialView(carpetasOrdenadas);

        }

        public ActionResult MenuCarpetas()
        {
            List<Carpeta> menuCarpetas = ctx.Carpeta.OrderBy(a => a.Nombre).ToList();
            return PartialView(menuCarpetas);
        }
    }

}
