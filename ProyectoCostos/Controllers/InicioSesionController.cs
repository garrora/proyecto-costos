using ProyectoCostos.Models;
using ProyectoCostos.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCostos.Controllers
{
    public class InicioSesionController : Controller
    {
        // GET: InicioSesion
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult iniciarSesion(string txtUsuario, string txtContrasena)
        {

            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    var query = from d in db.Usuarios
                                where d.Usuario == txtUsuario && d.Contrasena == txtContrasena
                                select d;

                    if(query.Count() > 0)
                    {
                        Usuarios oUsuarios = query.First();
                        Session["administrador"] = oUsuarios;
                        return Json(1); // Usuario encontrado
                    } else
                    {
                        return Json("Usuario y/o contraseña incorrectos"); // Usuario no encontrado
                    }
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }

        }
    }
}