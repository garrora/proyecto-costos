using ProyectoCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCostos.Controllers
{
    public class ConfiguracionController : Controller
    {
        // GET: Configuracion
        public ActionResult Index()
        {
            return View();
        }


       public ActionResult cambiarContrasena(string txtContrasena)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {


                    db.SPActulizarContrasena("admin", txtContrasena);
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }
    }
}