using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataAccessLayer;
using ServiceLayer.Services;

namespace WebBoxToDo.Controllers
{
    public class TareasController : Controller
    {
        BoxToDo_Contexto ctx = new BoxToDo_Contexto();
        TareaService tareaService = new TareaService();
        CarpetaService carpetaService = new CarpetaService();

        // GET: Tareas
        public ActionResult Index()
        {
            if (Session["login"] is true)
            {
                var idUsuario = Session["id"];
                int idUsu = Convert.ToInt32(idUsuario);

                List<Tarea> tareas = tareaService.ListarTareas(idUsu);
                
                foreach (var item in tareas)
                {
                    Carpeta carpetaDeLaTarea = carpetaService.IdDeCarpeta(item.IdCarpeta);
                    item.NombreCarpeta = carpetaDeLaTarea != null ? carpetaDeLaTarea.Nombre : string.Empty;
                    
                }

                return View(tareas);
            }
            else
            {
                Session["login"] = "Tareas/Index";
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
                Session["login"] = "Tareas/Crear";
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

                foreach (var item in tareasIncompletas)
                {
                    Carpeta carpetaDeLaTarea = carpetaService.IdDeCarpeta(item.IdCarpeta);
                    item.NombreCarpeta = carpetaDeLaTarea != null ? carpetaDeLaTarea.Nombre : string.Empty;
                    
                }
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

                foreach (var item in tareasCompletas)
                {
                    Carpeta carpetaDeLaTarea = carpetaService.IdDeCarpeta(item.IdCarpeta);
                    item.NombreCarpeta = carpetaDeLaTarea != null ? carpetaDeLaTarea.Nombre : string.Empty;
                    
                }

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
                Session["login"] = "Tareas/Index";
                return RedirectToAction("Login", "Home");
            }
        }

        public void CompletarCkeckTarea(int id)
        {
            Tarea tarea = tareaService.IdDeTarea(id);
            tareaService.CheckboxCompletarTarea(tarea);
        }

        public ActionResult ListarComentarios(int id)
        {
            if (Session["login"] is true)
            {
                var idUsuario = Session["id"];
                int idUsu = Convert.ToInt32(idUsuario);
                List<ComentarioTarea> comentarios = tareaService.ListarComentarios(id);
                return PartialView(comentarios);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ListarArchivos(int id)
        {
            if (Session["login"] is true)
            {
                var idUsuario = Session["id"];
                int idUsu = Convert.ToInt32(idUsuario);
                List<ArchivoTarea> archivos = tareaService.ListarArchivos(id);
                return PartialView(archivos);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult AgregarComentario(int id)
        {
            ComentarioTarea comentario = new ComentarioTarea();
            comentario.IdTarea = id;
            return PartialView(comentario);
        }

        [HttpPost]
        public ActionResult AgregarComentario(ComentarioTarea comentario)
        {
            tareaService.AgregarComentario(comentario);
            return RedirectToAction("Detalle", new { id = comentario.IdTarea });
        }

        public ActionResult AgregarArchivo(int id)
        {
            ArchivoTarea archivo = new ArchivoTarea();
            archivo.IdTarea = id;
            return PartialView(archivo);
        }

        [HttpPost]
        public ActionResult AgregarArchivo(ArchivoTarea archivo)
        {
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                archivo.RutaArchivo = tareaService.GuardarArchivo(Request.Files[0], Request.Files[0].FileName, archivo.IdTarea);
                tareaService.AgregarArchivo(archivo);
            }

            return RedirectToAction("Detalle", new { id = archivo.IdTarea });
        }

        public ActionResult DescargarArchivo(int idTarea, string nombre)
        {
            string carpetaAdjuntos = System.Configuration.ConfigurationManager.AppSettings["CarpetaAdjuntos"];
            carpetaAdjuntos = carpetaAdjuntos.Replace("{idTarea}", idTarea.ToString());
            carpetaAdjuntos = string.Format("/{0}/", carpetaAdjuntos.TrimStart('/').TrimEnd('/'));
            string pathDestino = System.Web.Hosting.HostingEnvironment.MapPath("~") + carpetaAdjuntos;

            string path = pathDestino;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + nombre);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nombre);
        }
    }
}