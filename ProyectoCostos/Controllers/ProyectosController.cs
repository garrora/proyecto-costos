using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoCostos.Models;

namespace ProyectoCostos.Controllers
{
    public class ProyectosController : Controller
    {
        // GET: Proyectos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listarProyectos()
        {
            try
            {
                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    return Json(db.SPListarProyectos().ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult listarClientes()
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarSelectClientes().ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        // GET: Proyectos/Create
        public ActionResult agregarProyecto(string txtNombre, string txtDescripcion, string txtInicio, string txtFinal, string txtMonto, string selectClientes, string selectMoneda, string txtCambio, string selectEstado)
        {
            try
            {
                string txtContrato = "Vacio";
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[i];
                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                        txtContrato = fname;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("../Carga/"), fname);
                    file.SaveAs(fname);
                }
                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    string CodProyecto = txtNombre + txtInicio;
                    db.SPInsertarProyecto(CodProyecto, txtNombre, txtDescripcion, txtInicio, txtFinal, txtMonto, txtContrato, selectClientes, selectMoneda, txtCambio, selectEstado);
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {
                return Json(ex.InnerException.Message);
            }
        }
        public ActionResult actualizarProyectoFile(string txtEditaIdProyecto, string txtEditaCodProyecto, string txtEditaNombre, string txtEditaInicio, string txtEditaFinal, string txtEditaMonto, string txtEditaDescripcion, string txtEditaContrato, string selectEditaClientes, string selectEditaMoneda, string txtEditaCambio, string selectEditaEstado)
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[i];
                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                        txtEditaContrato = fname;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("../Carga/"), fname);
                    file.SaveAs(fname);
                }
                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    string CodProyecto = txtEditaNombre + txtEditaInicio;
                    db.SPActualizarProyecto(txtEditaIdProyecto, CodProyecto, txtEditaNombre, txtEditaDescripcion, txtEditaInicio, txtEditaFinal, txtEditaMonto, txtEditaContrato, selectEditaClientes, selectEditaMoneda, txtEditaCambio, selectEditaEstado);
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {

                return Json(ex.InnerException.Message);
            }
        }
        public ActionResult actualizarProyecto(string txtEditaIdProyecto, string txtEditaCodProyecto, string txtEditaNombre, string txtEditaInicio, string txtEditaFinal, string txtEditaMonto, string txtEditaDescripcion, string txtEditaContrato, string selectEditaClientes, string selectEditaMoneda, string txtEditaCambio, string selectEditaEstado)
        {
            try
            {
                txtEditaContrato = "Vacio";
                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    string CodProyecto = txtEditaNombre + txtEditaInicio;
                    db.SPActualizarProyecto(txtEditaIdProyecto, CodProyecto, txtEditaNombre, txtEditaDescripcion, txtEditaInicio, txtEditaFinal, txtEditaMonto, txtEditaContrato, selectEditaClientes, selectEditaMoneda, txtEditaCambio, selectEditaEstado);
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {

                return Json(ex.InnerException.Message);
            }
        }
        public ActionResult eliminarProyectos(string txtEliminaProyecto)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {
                    db.SPEliminarProyecto(txtEliminaProyecto);
                    db.SaveChanges();
                    return Json(1);
                }

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }
        public ActionResult listarProyecto(string IDProyecto)
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarProyecto(IDProyecto).ToList(), JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }
    }
}
