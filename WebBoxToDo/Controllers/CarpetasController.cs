using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;
using System.Net;
using System.Data.Entity;
using ServiceLayer.Services;


namespace WebBoxToDo.Controllers
{
    public class CarpetasController : Controller
    {
        // BoxToDo_Contexto ctx = new BoxToDo_Contexto();
        CarpetaService carpetaService = new CarpetaService();

        // GET: Carpetas
        public ActionResult Index()
        {
            List<Carpeta> ListaCarpetas = carpetaService.ListarCarpeta();
            return View(ListaCarpetas);
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
                carpetaService.CrearCarpeta(carpeta);
                return RedirectToAction("Index");
            }
            return View(carpeta);
        }



        public ActionResult Tareas(int id)
        {
            List<Tarea> misTareas = carpetaService.ListarTareas(id);
            ViewBag.NombreCarpeta = carpetaService.MostrarNombreCarpeta(id);
            return View(misTareas);
        }

        ////para otro select
        public ActionResult ListarCarpetasOrdenadas()
        {
            List<Carpeta> carpetasOrdenadas = carpetaService.ListarCarpetasOrdenadas();
            return PartialView(carpetasOrdenadas);

        }

        public ActionResult MenuCarpetas()
        {
            List<Carpeta> menuCarpetas = carpetaService.MenuCarpetas();
            return PartialView(menuCarpetas);
        }
    }

}
