using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoCostos.Models;

namespace ProyectoCostos.Controllers
{
    public class CostosController : Controller
    {
        // GET: Costos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listarCostos()
        {
            try
            {
                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    return Json(db.SPListarCostos().ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }
        public ActionResult agregarCosto(string txtNombre, string txtFecha, string txtMonto, string selectTipoCosto, string selectMoneda, string txtCambio, string selectEstado, string txtAgregaIDProyecto)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    db.SPInsertarCosto(txtNombre, txtFecha, txtMonto, selectTipoCosto, txtAgregaIDProyecto, selectMoneda, txtCambio, selectEstado);
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {
                return Json(ex.InnerException.Message);
            }
        }
        public ActionResult actualizarCosto(string txtEditaNombre, string txtEditaFecha, string txtEditaMonto, string selectEditaTipoCosto, string selectEditaMoneda, string txtEditaCambio, string selectEditaEstado, string txtEditaIDCosto)
        {
            try
            {
                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    db.SPActualizarCosto(txtEditaIDCosto, txtEditaNombre, txtEditaFecha, txtEditaMonto, selectEditaTipoCosto, selectEditaMoneda, txtEditaCambio, selectEditaEstado);
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {

                return Json(ex.InnerException.Message);
            }
        }
        public ActionResult eliminarCosto(string txtEliminaIDCosto)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    db.SPEliminarCosto(txtEliminaIDCosto);
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }
        public ActionResult listarCosto(string IDCosto)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarCosto(IDCosto).ToList(), JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }
    }
}
