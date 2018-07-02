using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;



namespace ServiceLayer.Services
{
    public class CarpetaService
    {
        BoxToDo_Contexto ctx = new BoxToDo_Contexto();

        public List<Carpeta> ListarCarpeta()
        {
            List<Carpeta> carpetas = ctx.Carpeta.OrderBy(a => a.Nombre).ToList();
            return (carpetas);
        }


        public void CrearCarpeta(Carpeta carpeta)
        {
            carpeta.FechaCreacion = DateTime.Now;
            ctx.Carpeta.Add(carpeta);
            ctx.SaveChanges();
        }


        public List<Tarea> ListarTareas(int id)
        {
            List<Tarea> misTareas = ctx.Tarea.OrderBy(a => a.Prioridad).OrderBy(a => a.FechaFin).Where(a => a.IdCarpeta == id).ToList();
            return (misTareas);
        }

        public String MostrarNombreCarpeta(int id)
        {
            var NombreCarpeta = ctx.Carpeta.FirstOrDefault(a => a.IdCarpeta == id).Nombre;
            return (NombreCarpeta);
        }


        public List<Carpeta> ListarCarpetasOrdenadas()
        {
            List<Carpeta> carpetasOrdenadas = ctx.Carpeta.OrderBy(a => a.IdUsuario).Where(a => a.IdUsuario != null).ToList();
            return (carpetasOrdenadas);

        }

        public List<Carpeta> MenuCarpetas()
        {
            List<Carpeta> menuCarpetas = ctx.Carpeta.OrderBy(a => a.Nombre).ToList();
            return (menuCarpetas);
        }
    }
}
