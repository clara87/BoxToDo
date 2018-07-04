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
            List<Tarea> tareas = ctx.Tarea.Where(u=>u.IdUsuario == idUsuario).ToList();
            return (tareas);
        }

        //ver esto
        public void CrearTarea(Tarea tarea, int idUsuario)
        {
            tarea.FechaCreacion = DateTime.Now;
            tarea.IdUsuario = idUsuario;
            tarea.Completada = 0;
            tarea.IdCarpeta = 1;

            ctx.Tarea.Add(tarea);
            ctx.SaveChanges();          
        }
    }
}
