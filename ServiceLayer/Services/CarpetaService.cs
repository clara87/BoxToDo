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

        public List<Carpeta> ListarCarpeta(int id)
        {
            List<Carpeta> carpetaUsuario= ctx.Carpeta.Where(o => o.IdUsuario == id).ToList();            
            return (carpetaUsuario);
        }


        public void CrearCarpeta(Carpeta carpeta, int idUsuario)
        {
            carpeta.IdUsuario = idUsuario;
            carpeta.FechaCreacion = DateTime.Now;
            ctx.Carpeta.Add(carpeta);
            ctx.SaveChanges();
        }


        public List<Tarea> ListarTareas(int id, int idUsuario)
        {
            List<Tarea> misTareas = ctx.Tarea.OrderBy(a => a.Prioridad).OrderBy(a => a.FechaFin).Where(a => a.IdCarpeta == id && a.IdUsuario== idUsuario).ToList();
            return (misTareas);
        }


        public String MostrarNombreCarpeta(int id, int idUsuario)
        {
            var NombreCarpeta = ctx.Carpeta.FirstOrDefault(a => a.IdCarpeta == id && a.IdUsuario == idUsuario).Nombre;
                
            return (NombreCarpeta);
        }


        public List<Carpeta> ListarCarpetasOrdenadas(int idUsu)
        {
            List<Carpeta> carpetasOrdenadas = ctx.Carpeta.OrderBy(a => a.IdUsuario).Where(a => a.IdUsuario == idUsu).ToList();
            return (carpetasOrdenadas);

        }


        public List<Carpeta> MenuCarpetas(int id)
        {
            List<Carpeta> menuCarpetas = ctx.Carpeta.Where(o => o.IdUsuario == id).OrderBy(a => a.Nombre).ToList();
            return (menuCarpetas);
        }
    }
}
