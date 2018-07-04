using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace ServiceLayer.Services
{
    public class TareaService
    {
        BoxToDo_Contexto ctx = new BoxToDo_Contexto();

        public List<Tarea> ListarTareas(int idUsuario)
        {
            List<Tarea> tareas = ctx.Tarea.Where(o => o.IdUsuario == idUsuario).OrderBy(a => a.Nombre).ToList();
            return (tareas);
        }

        public List<Tarea> ListarTareasIncompletas(int idUsuario)
        {
            List<Tarea> tareas = ctx.Tarea.Where(o => o.IdUsuario == idUsuario && o.Completada == 0).OrderBy(a => a.Nombre).ToList();
            return (tareas);
        }


        //ver esto
        public void CrearTarea(Tarea tarea, int idUsuario)
        {
            tarea.FechaCreacion = DateTime.Now;
            tarea.IdUsuario = idUsuario;
            tarea.Completada = 0;       

            ctx.Tarea.Add(tarea);
            ctx.SaveChanges();          
        }

        

        public Tarea DetalleTarea(int id,int idUsuario)
        {
           Tarea tarea = ctx.Tarea.FirstOrDefault(u => u.IdTarea == id && u.IdUsuario == idUsuario);
            return (tarea);
        }

        public List<ComentarioTarea> Comentarios(int id)
        {
            List<ComentarioTarea> listaComentarios = ctx.ComentarioTarea.Where(a => a.IdTarea == id).OrderByDescending(a => a.FechaCreacion).ToList();
            return listaComentarios;
        }

        public string NombreCarpeta(int id)
        {
            Tarea tarea = ctx.Tarea.FirstOrDefault(a => a.IdTarea == id);
            string carpeta = ctx.Carpeta.FirstOrDefault(c => c.IdCarpeta == tarea.IdCarpeta).Nombre;
            return carpeta;

        }
    }
}
