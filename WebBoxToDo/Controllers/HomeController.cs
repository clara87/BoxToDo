using System;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;
using Newtonsoft.Json.Linq;
using System.Net;
using ServiceLayer.Services;

namespace WebBoxToDo.Controllers
{
    public class HomeController : Controller
    {
        HomeService homeService = new  HomeService();

        //Verifica que las cookies existan.    
        public ActionResult Index()
        {
            if (Request.Cookies["recordarme"] != null)
            {                
                if (Request.Cookies["recordarme"]["estado"] == Seguridad.GetSHA1("verdadero"))
                {
                    int id = Convert.ToInt32(Request.Cookies["recordarme"]["idUsuario"]);                    
                    Usuario UsLog = homeService.BuscarUsuarioPorId(id);
                    Session["login"] = true;
                    Session["id"] = UsLog.IdUsuario;
                    
                    return View("Home", UsLog);
                }
            }
            
            return View();
        }

        //Devuelve la vista.
        public ActionResult Registracion()
        {
            return View();
        }

        //Recibe el formulario de registro.
        [HttpPost]
        public ActionResult Registracion(UsuarioRegistro UsReg)
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = "6Ld3NWAUAAAAAJJ2Mco-UNBPCdXwIZwIeNs1r6fC";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";
          

            if (ModelState.IsValid && status == true)
            {
                BoxToDo_Contexto ctx = new BoxToDo_Contexto();
                Usuario UsEncontrado = homeService.BuscarUsuarioPorEmailInactivo(UsReg);

                if (UsEncontrado == null)
                {
                    homeService.CrearUsuario(UsReg, result);
                }
                else if (UsEncontrado.Activo == 0)
                {
                    homeService.ReactivarUsuario(UsReg,UsEncontrado);                  
                    return View("Login");           
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(UsReg);
            }

        }

        //Devuelve la vista
        public ActionResult Home()
        {            
            if (Session["login"] is true)
            {
                return View();
            }
            else
            {
                TempData["url"] = Request.Url.AbsolutePath;
                return View("Login");
            }            
        }

        //Devuelve la vista.
        public ActionResult Login()
        {
            return View();
        }

        //Recibe el formulario de login.
        [HttpPost]
        public ActionResult Login(UsuarioLogin Us)
        {           
            Usuario UsLog = homeService.BuscarUsuarioPorEmail(Us);

            if (ModelState.IsValid && UsLog != null)
            {
                if (UsLog.Activo == 1)
                {
                    if ((UsLog.Email == Us.Email) && (UsLog.Contrasenia == Us.Contrasenia))
                    {
                        if (Us.Recordarme is true)
                        {
                            // encriptamos la cadena inicial                            
                            string idUsuario = UsLog.IdUsuario.ToString();
                            string estado = Seguridad.GetSHA1("verdadero");
                            HttpCookie recordarme = new HttpCookie("recordarme");
                            recordarme["estado"] = estado;
                            recordarme["idUsuario"] = idUsuario;
                            recordarme.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(recordarme);
                        }

                        Session["login"] = true;
                        Session["id"] = UsLog.IdUsuario;
                        
                        if (TempData["url"] == null)
                        {
                            return View("Home", UsLog);
                        }
                        else
                        {
                            string url = TempData["url"].ToString();
                            Response.Redirect(url);
                        }
                        
                    }
                    else
                    {
                        ViewData["Invalido"] = "Verifique usuario y / o contraseña.";
                    }
                }
                else if (UsLog.Activo == 0)
                {
                    ViewData["Inactivo"] = "Usuario inactivo.";
                }
            }
            else
            {
                ViewData["Invalido"] = "Verifique usuario y / o contraseña.";
            }

            return View("Login", Us);
        }

        //Finaliza la sesion del usuario.
        public ActionResult Logout()
        {
            if (Request.Cookies["recordarme"] != null)
            {
                Response.Cookies["recordarme"].Expires = DateTime.Now.AddDays(-1);
            }
            Session.Abandon();

            return RedirectToAction("Index");
        }
    }
}