using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        [Route ("Acceso/Login")]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo,string clave)
        {
            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.Correo == correo && u.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();


            if (oUsuario == null) 
            {
                ViewBag.Error = "Correo o contraseña invalidos";
                return View();
            }
            else
            {
                ViewBag.Error = null;
                return RedirectToAction("Index","Mantenedor");
            }
            
        }
    }
}