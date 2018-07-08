using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;


namespace ServiceLayer.Services
{
    public class TareaService
    {
        BoxToDo_Contexto ctx = new BoxToDo_Contexto();

        public List<Tarea> ListarTareas(int idUsuario)
        {
            List<Tarea> tareas = ctx.Tarea.Where(o => o.IdUsuario == idUsuario)
                                          .OrderByDescending(a => a.FechaCreacion)
                                          .ToList();

            return tareas;
        }


        public void CheckboxCompletarTarea(Tarea tarea)
        {
            tarea.Completada = 1;
            ctx.SaveChanges();
        }


        //Actualiza en la base la tarea completa
        public void Completar(int idTarea, int idUsuario)
        {
            Tarea TareaEncontrada = ctx.Tarea.FirstOrDefault(t => t.IdTarea == idTarea && t.IdUsuario == idUsuario);

            if (TareaEncontrada != null)
            {
                TareaEncontrada.Completada = 1;
                ctx.SaveChanges();
            }
        }


        public void CrearTarea(Tarea tarea, int idUsuario)
        {
            tarea.FechaCreacion = DateTime.Now;
            tarea.IdCarpeta = tarea.IdCarpeta;
            tarea.IdUsuario = idUsuario;
            tarea.Completada = 0;

            ctx.Tarea.Add(tarea);
            ctx.SaveChanges();
        }


        public Tarea DetalleTarea(int id, int idUsuario)
        {
            Tarea tarea = ctx.Tarea.Include("ComentarioTarea")
                                    .Include("ArchivoTarea")
                                    .FirstOrDefault(u => u.IdTarea == id && u.IdUsuario == idUsuario);
            if (tarea != null)
            {
                tarea.ComentarioTarea = tarea.ComentarioTarea.OrderByDescending(a => a.FechaCreacion)
                                                          .ToList();
                tarea.ArchivoTarea = tarea.ArchivoTarea.OrderByDescending(a => a.FechaCreacion)
                                                             .ToList();
            }
            
            return tarea;
        }


        public List<Tarea> ListarTareasIncompletas(int idUsuario)
        {
            List<Tarea> tareasIncompletas = ctx.Tarea.Where(o => o.IdUsuario == idUsuario)
                                                     .Where(o => o.Completada == 0)
                                                     .OrderBy(a => a.Prioridad)
                                                     .ThenBy(a => a.FechaFin)
                                                     .ToList();
            return (tareasIncompletas);
        }


        public List<Tarea> ListarTareasCompletas(int idUsuario)
        {
            List<Tarea> tareasCompletas = ctx.Tarea.Where(o => o.IdUsuario == idUsuario)
                                                     .Where(o => o.Completada == 1)
                                                     .OrderBy(a => a.Prioridad)
                                                     .ThenBy(a => a.FechaFin)
                                                     .ToList();
            return (tareasCompletas);
        }


        public Tarea IdDeTarea(int idTarea)
        {
            Tarea tarea = ctx.Tarea.FirstOrDefault(a => a.IdTarea == idTarea);
            return tarea;
        }


        public void AgregarComentario(ComentarioTarea comentario)
        {
            comentario.FechaCreacion = DateTime.Now;
            ctx.ComentarioTarea.Add(comentario);
            ctx.SaveChanges();
        }


        public List<ArchivoTarea> ListarArchivos(int idTarea)
        {
            List<ArchivoTarea> archivos = ctx.ArchivoTarea.Where(a => a.IdTarea == idTarea)
                                                          .OrderBy(a => a.FechaCreacion)
                                                          .ToList();
            return archivos;
        }


        public List<ComentarioTarea> ListarComentarios(int idTarea)
        {
            List<ComentarioTarea> comentarios = ctx.ComentarioTarea.Where(a => a.IdTarea == idTarea)
                                                          .OrderBy(a => a.FechaCreacion)
                                                          .ToList();
            return comentarios;
        }


        public void AgregarArchivo(ArchivoTarea archivo)
        {
            archivo.FechaCreacion = DateTime.Now;
            ctx.ArchivoTarea.Add(archivo);
            ctx.SaveChanges();
        }


        public string NombreCarpeta(int id)
        {
            string carpeta = "";
            Tarea tarea = ctx.Tarea.FirstOrDefault(a => a.IdTarea == id);
            carpeta = ctx.Carpeta.FirstOrDefault(c => c.IdCarpeta == tarea.IdCarpeta).Nombre;
            return carpeta;

        }


        public string GuardarArchivo(HttpPostedFileBase archivo, string nombre, int idTarea)
        {
            string carpetaAdjuntos = ConfigurationManager.AppSettings["CarpetaAdjuntos"];
            carpetaAdjuntos = carpetaAdjuntos.Replace("{idTarea}", idTarea.ToString());
            carpetaAdjuntos = string.Format("/{0}/", carpetaAdjuntos.TrimStart('/').TrimEnd('/'));
            string pathDestino = System.Web.Hosting.HostingEnvironment.MapPath("~") + carpetaAdjuntos;

            if (!Directory.Exists(pathDestino))
            {
                Directory.CreateDirectory(pathDestino);
            }

            string nombreArchivoFinal = GenerarNombreUnico(nombre);
            nombreArchivoFinal = string.Concat(nombreArchivoFinal, Path.GetExtension(archivo.FileName));
            archivo.SaveAs(string.Concat(pathDestino, nombreArchivoFinal));

            return string.Concat(carpetaAdjuntos, nombreArchivoFinal);
        }

        
        private static string GenerarNombreUnico(string nombre)
        {
            string randomString = System.Web.Security.Membership.GeneratePassword(20, 0);
            Random rnd = new Random();

            randomString = Regex.Replace(randomString, @"[^a-zA-Z0-9]", m => "");

            nombre = Regex.Replace(nombre.Trim(), @"[^a-zA-Z0-9]", m => "").ToLower();

            return string.Format("{0}-{1}", nombre, randomString);
        }

    }
}
