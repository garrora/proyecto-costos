using ProyectoCostos.Controllers;
using ProyectoCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCostos.Filters
{
    public class ClaseVerifySession : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var oUser = (Usuarios)HttpContext.Current.Session["administrador"];

            if(oUser == null)
            {
                if(filterContext.Controller is InicioSesionController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/InicioSesion/Index");
                }
            } else
            {
                if (filterContext.Controller is InicioSesionController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Clientes/Index");
                }
            }


            base.OnActionExecuting(filterContext);
        }



    }
}