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

        public List<Tarea> ListarTareas()
        {
            List<Tarea> tareas = ctx.Tarea.ToList();
            return (tareas);
        }

        public void CrearTarea(Tarea tarea)
        {
            tarea.FechaCreacion = DateTime.Now;
            tarea.IdUsuario = 2;
            tarea.Completada = 0;
            ctx.Tarea.Add(tarea);
            ctx.SaveChanges();          
        }
    }
}
