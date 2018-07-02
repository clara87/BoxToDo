using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace ServiceLayer.Services
{
   public class HomeService
    {
        BoxToDo_Contexto ctx = new BoxToDo_Contexto();

        public Usuario BuscarUsuarioPorId(int id)
        {           
             Usuario UsLog = ctx.Usuario.FirstOrDefault(o => o.IdUsuario == id);
             return UsLog;
        }

        public Usuario BuscarUsuarioPorEmailInactivo(UsuarioRegistro UsReg)
        {
            Usuario UsEncontrado = ctx.Usuario.FirstOrDefault(o => o.Email == UsReg.Email && o.Activo == 0);
            return UsEncontrado;
        }


        public void CrearUsuario(UsuarioRegistro UsReg, string result)
        {
           Usuario UsNuevo = new Usuario();
            UsNuevo.Nombre = UsReg.Nombre;
            UsNuevo.Apellido = UsReg.Apellido;
            UsNuevo.Email = UsReg.Email;
            UsNuevo.Contrasenia = UsReg.Contrasenia;
            UsNuevo.FechaRegistracion = DateTime.Now;
            UsNuevo.CodigoActivacion = result;
            ctx.Usuario.Add(UsNuevo);
            ctx.SaveChanges();
        }

        public void ReactivarUsuario(UsuarioRegistro UsReg, Usuario UsEncontrado)
        {
            UsEncontrado.Nombre = UsReg.Nombre;
            UsEncontrado.Apellido = UsReg.Apellido;
            UsEncontrado.Contrasenia = UsReg.Contrasenia;
            UsEncontrado.Activo = 1;
            UsEncontrado.FechaActivacion = DateTime.Now;
            ctx.SaveChanges();

        }

        public Usuario BuscarUsuarioPorEmail(UsuarioLogin Us)
        {
            Usuario UsLog = ctx.Usuario.FirstOrDefault(o => o.Email == Us.Email);
            return UsLog;
        }
       
    }
}
