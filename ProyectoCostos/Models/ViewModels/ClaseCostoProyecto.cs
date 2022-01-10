using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCostos.Models.ViewModels
{
    public class ClaseCostoProyecto
    {

        public int idProyecto { get; set; }
        public string Cliente { get; set; }
        public long numeroIdentificacion { get; set; }
        public string Proyecto { get; set; }


    }
}