using ProyectoCostos.Models;
using ProyectoCostos.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ProyectoCostos.Controllers
{
    public class ClientesController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listarClientes()
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    return Json(db.SPListarClientes().ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult listarCliente(string numeroIdentificacion)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarCliente(long.Parse(numeroIdentificacion)).ToList(), JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }


        public ActionResult agregarClientes(string txtDireccion, string selectProvincias, string selectDistritos, string selectCantones, string txtCorreoElectronicoDos, string txtCorreoElectronico, string txtNumeroTelefonico, string txtNombre, string selectTipoIdentificacion, string txtNumeroIdentificacion, string txtCodigoTelefonico)
        {
            try
            {

                if(selectProvincias == null)
                {
                    selectProvincias = "8";
                    selectCantones = "84";
                    selectDistritos = "497";
                }

                if (txtCodigoTelefonico.Equals(""))
                {
                    txtCodigoTelefonico = null;
                }

                if (txtNumeroTelefonico.Equals(""))
                {
                    txtNumeroTelefonico = null;
                }

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                 {


                    db.SPInsertarClientes(long.Parse(txtNumeroIdentificacion), selectTipoIdentificacion, txtNombre,
                        Convert.ToInt32(txtNumeroTelefonico), txtCorreoElectronico, txtCorreoElectronicoDos, Convert.ToInt32(txtCodigoTelefonico),
                        Convert.ToInt32(selectProvincias), Convert.ToInt32(selectCantones), Convert.ToInt32(selectDistritos), txtDireccion);
                    db.SaveChanges();
                     return Json(1);
                 }

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }

        public ActionResult actualizarClientes(string txtEditaNumeroIdentificacion, string txtTipoIdentificacion, string txtEditaNombre, string txtEditaNumeroTelefonico, string txtEditaCorreoElectronico, string txtEditaCorreoElectronicoDos, string selectEditaProvincias, string selectEditaCantones, string selectEditaDistritos, string txtEditaDireccion, string txtEditaCodigoTelefonico)
        {
            try
            {


                if (selectEditaProvincias == null)
                {
                    selectEditaProvincias = "8";
                    selectEditaCantones = "84";
                    selectEditaDistritos = "497";
                }

                if (txtEditaCodigoTelefonico.Equals(""))
                {
                    txtEditaCodigoTelefonico = null;
                }

                if (txtEditaNumeroTelefonico.Equals(""))
                {
                    txtEditaNumeroTelefonico = null;
                }

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {


                     db.SPActualizarCliente(long.Parse(txtEditaNumeroIdentificacion), txtTipoIdentificacion, txtEditaNombre, Convert.ToInt32(txtEditaNumeroTelefonico), txtEditaCorreoElectronico, txtEditaCorreoElectronicoDos, Convert.ToInt32(txtEditaCodigoTelefonico), Convert.ToInt32(selectEditaProvincias), Convert.ToInt32(selectEditaCantones), Convert.ToInt32(selectEditaDistritos), txtEditaDireccion);
                   
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }

        public ActionResult eliminarClientes(string txtEliminaNumeroIdentificacion)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {


                    db.SPEliminarCliente(long.Parse(txtEliminaNumeroIdentificacion));
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }

        public ActionResult listarProvincias()
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    
                    return Json(db.SPListarProvincias().ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult listarCantones(string idProvincia)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarCantones(Convert.ToInt32(idProvincia)).ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult listarProvincia(string idProvincia)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarProvincia(Convert.ToInt32(idProvincia)).ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult listarDistritos(string idCanton)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarDistritos(Convert.ToInt32(idCanton)).ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult listarDistrito()
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarDistrito().ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult cerrarSesion()
        {
            Session["administrador"] = null;
            return Redirect("~/InicioSesion/Index");
            
        }


    }
}