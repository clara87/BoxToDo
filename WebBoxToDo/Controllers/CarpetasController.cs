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
        CarpetaService carpetaService = new CarpetaService();

        // GET: Carpetas
        public ActionResult Index()
        {
            var idUsuario = Session["id"];
            int idUsu = Convert.ToInt32(idUsuario);

            if (Session["login"] is true)
            {
                List<Carpeta> ListaCarpetas = carpetaService.ListarCarpeta(idUsu);
                return View(ListaCarpetas);
            }
            else
            {
                //Session["login"] = "Carpetas/Index";
                TempData["url"] = Request.Url.AbsolutePath;
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
                //Session["login"] = "Carpetas/Crear";
                TempData["url"] = Request.Url.AbsolutePath;
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        public ActionResult Crear(Carpeta carpeta)
        {
            var idUsuario = Session["id"];
            int idUsu = Convert.ToInt32(idUsuario);
            if (Session["login"] is true)
            {
                if (ModelState.IsValid)
                {
                    carpetaService.CrearCarpeta(carpeta, idUsu);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(carpeta);
                }
            }
            else
            {
                TempData["url"] = Request.Url.AbsolutePath;
                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult Tareas(int id)
        {
            if (Session["login"] is true)
            {
                var idUsuario = Session["id"];
                int idUsu = Convert.ToInt32(idUsuario);
                List<Tarea> misTareas = carpetaService.ListarTareas(id, idUsu);
                ViewBag.NombreCarpeta = carpetaService.MostrarNombreCarpeta(id);
                return View(misTareas);
            }
            else
            {
                //Session["login"] = "Carpetas/Tareas";
                TempData["url"] = Request.Url.AbsolutePath;
                return RedirectToAction("Login", "Home");
            }
        }

        ////para otro select
        public ActionResult ListarCarpetasOrdenadas()
        {
            if (Session["login"] is true)
            {
                var idUsuario = Session["id"];
                int idUsu = Convert.ToInt32(idUsuario);
                List<Carpeta> carpetasOrdenadas = carpetaService.ListarCarpetasOrdenadas(idUsu);
                return PartialView(carpetasOrdenadas);
            }
            else
            {
                TempData["url"] = Request.Url.AbsolutePath;
                return RedirectToAction("Login", "Home");
            }

        }

        public ActionResult MenuCarpetas()
        {
            var idUsuario = Session["id"];
            int idUsu = Convert.ToInt32(idUsuario);

            if (Session["login"] is true)
            {
                List<Carpeta> menuCarpetas = carpetaService.MenuCarpetas(idUsu);
                return PartialView(menuCarpetas);
            }
            else
            {
                TempData["url"] = Request.Url.AbsolutePath;
                return RedirectToAction("Login", "Home");
            }


        }
    }

}
