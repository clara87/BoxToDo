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

        public List<Carpeta> ListarCarpeta(int idUsuario)
        {
            List<Carpeta> carpeta = ctx.Carpeta.Where(o => o.IdUsuario == idUsuario || o.IdUsuario == null)
                                               .ToList();            
            return (carpeta);
        }

        public Carpeta IdDeCarpeta(int idCarpeta)
        {
            Carpeta carpeta = ctx.Carpeta.Find(idCarpeta);
            return carpeta;
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
            List<Tarea> misTareas = ctx.Tarea.OrderByDescending(a => a.Prioridad)
                                             .ThenBy(a => a.FechaFin)
                                             .Where(a => a.IdCarpeta == id && a.IdUsuario== idUsuario)
                                             .ToList();
            return (misTareas);
        }


        public String MostrarNombreCarpeta(int id)
        {
            var NombreCarpeta = ctx.Carpeta.FirstOrDefault(a => a.IdCarpeta == id).Nombre;
                
            return (NombreCarpeta);
        }


        public List<Carpeta> ListarCarpetasOrdenadas(int idUsu)
        {
            List<Carpeta> carpetasOrdenadas = ctx.Carpeta.OrderBy(a => a.IdUsuario).Where(a => a.IdUsuario == idUsu).ToList();
            return (carpetasOrdenadas);

        }


        public List<Carpeta> MenuCarpetas(int id)
        {
            List<Carpeta> menuCarpetas = ctx.Carpeta.Where(o => o.IdUsuario == id || o.IdUsuario == null).OrderBy(a => a.Nombre).ToList();
            return (menuCarpetas);
        }
    }
}
