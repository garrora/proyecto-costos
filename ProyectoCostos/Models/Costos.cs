//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoCostos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Costos
    {
        public int IDCosto { get; set; }
        public string Nombre { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int Tipo { get; set; }
        public int IdProyecto { get; set; }
        public string Estado { get; set; }
    }
}
