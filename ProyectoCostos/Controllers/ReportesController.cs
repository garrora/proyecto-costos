using ProyectoCostos.Models;
using ProyectoCostos.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ProyectoCostos.Reports;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoCostos.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reporte(long NumeroIdentificacion, int tipoReporte, int IdProyecto, int IDTipo, string Estado, string Fecha, string FechaF, string Mes, string Anno)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ProyectoCostosReportes"].ConnectionString;
                SqlConnection conx = new SqlConnection(connectionString);
                conx.Open();
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.AsyncRendering = false;
                reportViewer.SizeToReportContent = true;
                reportViewer.Width = Unit.Percentage(900);
                reportViewer.Height = Unit.Percentage(900);
                switch (tipoReporte)
                {

                    case 1:
                        string SQL = String.Empty;

                        // Nombre del procedimiento almacenado
                        SQL = "SPListarReporteCantidadProyectosCliente";
                        SqlCommand sqlCommand1 = new SqlCommand(SQL, conx);
                        ReportDataSource reportDataSource = null;

                        // Parametros del store procedure
                        sqlCommand1.CommandType = CommandType.StoredProcedure;
                        sqlCommand1.Parameters.AddWithValue("@NumeroIdentificacion", NumeroIdentificacion);
                        sqlCommand1.ExecuteNonQuery();
                        SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(sqlCommand1);
                        DataTable dataTable1 = new DataTable();
                        sqlDataAdapter1.Fill(dataTable1);
                        reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReporteCantidadProyectosCliente.rdlc";
                        reportViewer.LocalReport.DataSources.Clear();
                        reportDataSource = new ReportDataSource("DataSet1", dataTable1);
                        reportViewer.LocalReport.DataSources.Add(reportDataSource);
                        reportViewer.LocalReport.Refresh();
                        ViewBag.ReportViewer = reportViewer;
                        break;


                    case 2:
                        string SQL2 = String.Empty;

                        // Nombre del procedimiento almacenado
                        SQL2 = "SPListarReporteCostosProyecto";
                        SqlCommand sqlCommand2 = new SqlCommand(SQL2, conx);
                        ReportDataSource reportDataSource2 = null;

                        // Parametros del store procedure
                        sqlCommand2.CommandType = CommandType.StoredProcedure;
                        sqlCommand2.Parameters.AddWithValue("@IdProyecto", IdProyecto);
                        sqlCommand2.ExecuteNonQuery();

                        SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2);
                        DataTable dataTable2 = new DataTable();
                        sqlDataAdapter2.Fill(dataTable2);

                        reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReporteCostosProyecto.rdlc";
                        reportViewer.LocalReport.DataSources.Clear();
                        reportDataSource2 = new ReportDataSource("DataSet2", dataTable2);
                        reportViewer.LocalReport.DataSources.Add(reportDataSource2);
                        reportViewer.LocalReport.Refresh();
                        ViewBag.ReportViewer = reportViewer;
                        break;

                    case 3:
                        string SQL3 = String.Empty;

                        // Nombre del procedimiento almacenado
                        SQL3 = "SPListarReportreCostosProyectoTipoCosto";
                        SqlCommand sqlCommand3 = new SqlCommand(SQL3, conx);
                        ReportDataSource reportDataSource3 = null;

                        // Parametros del store procedure
                        sqlCommand3.CommandType = CommandType.StoredProcedure;
                        sqlCommand3.Parameters.AddWithValue("@IdProyecto", IdProyecto);
                        sqlCommand3.Parameters.AddWithValue("@IDTipo", IDTipo);
                        sqlCommand3.ExecuteNonQuery();

                        SqlDataAdapter sqlDataAdapter3 = new SqlDataAdapter(sqlCommand3);
                        DataTable dataTable3 = new DataTable();
                        sqlDataAdapter3.Fill(dataTable3);

                        reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReporteCostosProyectoTipoCosto.rdlc";
                        reportViewer.LocalReport.DataSources.Clear();
                        reportDataSource3 = new ReportDataSource("DataSet3", dataTable3);
                        reportViewer.LocalReport.DataSources.Add(reportDataSource3);
                        reportViewer.LocalReport.Refresh();
                        ViewBag.ReportViewer = reportViewer;
                        break;

                    case 4:
                        string SQL4 = String.Empty;

                        // Nombre del procedimiento almacenado
                        SQL4 = "SPListarReporteFacturasProyecto";
                        SqlCommand sqlCommand4 = new SqlCommand(SQL4, conx);
                        ReportDataSource reportDataSource4 = null;

                        // Parametros del store procedure
                        sqlCommand4.CommandType = CommandType.StoredProcedure;
                        sqlCommand4.Parameters.AddWithValue("@IdProyecto", IdProyecto);
                        sqlCommand4.Parameters.AddWithValue("@Estado", Estado);
                        sqlCommand4.ExecuteNonQuery();

                        SqlDataAdapter sqlDataAdapter4 = new SqlDataAdapter(sqlCommand4);
                        DataTable dataTable4 = new DataTable();
                        sqlDataAdapter4.Fill(dataTable4);

                        reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReporteFacturasProyecto.rdlc";
                        reportViewer.LocalReport.DataSources.Clear();
                        reportDataSource4 = new ReportDataSource("DataSet4", dataTable4);
                        reportViewer.LocalReport.DataSources.Add(reportDataSource4);
                        reportViewer.LocalReport.Refresh();
                        ViewBag.ReportViewer = reportViewer;
                        break;


                    case 5:
                        SqlCommand cmd5 = new SqlCommand("Select Cl.Nombre AS 'Cliente', Cl.NumeroTelefonico, Cl.CorreoElectronico, Cl.NumeroIdentificacion, Cl.Direccion, Co.Nombre AS 'Gasto', Co.Fecha, Co.Monto, Tc.Nombre AS 'TipoCosto', Pr.Nombre AS 'Proyecto' from Proyectos as Pr join Clientes as Cl on Pr.NumeroIdentificacion = Cl.NumeroIdentificacion inner join Costos as Co on Pr.IdProyecto = Co.IdProyecto inner join TipoCosto as Tc on Co.Tipo = Tc.IDTipo where Co.IdProyecto = '" + IdProyecto + "' and Co.Fecha >= '" + Fecha + "' and Co.Fecha <= '" + FechaF + "' and Co.Tipo = 1", conx);
                        SqlDataAdapter adp5 = new SqlDataAdapter(cmd5);
                        DataTable dt5 = new DataTable("Planilla");
                        adp5.Fill(dt5);
                        SqlCommand cmd5_1 = new SqlCommand("Select Cl.Nombre AS 'ClienteD', Cl.NumeroTelefonico AS 'NumeroTelefonicoD', Cl.CorreoElectronico AS 'CorreoElectronicoD', Cl.NumeroIdentificacion AS 'NumeroIdentificacionD', Cl.Direccion AS 'DireccionD', Co.Nombre AS 'GastoD', Co.Fecha AS 'FechaD', Co.Monto AS 'MontoD', Tc.Nombre AS 'TipoCostoD', Pr.Nombre AS 'ProyectoD' from Proyectos as Pr join Clientes as Cl on Pr.NumeroIdentificacion = Cl.NumeroIdentificacion inner join Costos as Co on Pr.IdProyecto = Co.IdProyecto inner join TipoCosto as Tc on Co.Tipo = Tc.IDTipo where Co.IdProyecto = '" + IdProyecto + "' and Co.Fecha >= '" + Fecha + "' and Co.Fecha <= '" + FechaF + "' and Co.Tipo = 2", conx);
                        SqlDataAdapter adp5_1 = new SqlDataAdapter(cmd5_1);
                        DataTable dt5_1 = new DataTable("Directo");
                        adp5_1.Fill(dt5_1);
                        SqlCommand cmd5_2 = new SqlCommand("Select Cl.Nombre AS 'ClienteI', Cl.NumeroTelefonico AS 'NumeroTelefonicoI', Cl.CorreoElectronico AS 'CorreoElectronicoI', Cl.NumeroIdentificacion AS 'NumeroIdentificacionI', Cl.Direccion AS 'DireccionI', Co.Nombre AS 'GastoI', Co.Fecha AS 'FechaI', Co.Monto AS 'MontoI', Tc.Nombre AS 'TipoCostoI', Pr.Nombre AS 'ProyectoI' from Proyectos as Pr join Clientes as Cl on Pr.NumeroIdentificacion = Cl.NumeroIdentificacion inner join Costos as Co on Pr.IdProyecto = Co.IdProyecto inner join TipoCosto as Tc on Co.Tipo = Tc.IDTipo where Co.IdProyecto = '" + IdProyecto + "' and Co.Fecha >= '" + Fecha + "' and Co.Fecha <= '" + FechaF + "' and Co.Tipo = 3", conx);
                        SqlDataAdapter adp5_2 = new SqlDataAdapter(cmd5_2);
                        DataTable dt5_2 = new DataTable("Indirecto");
                        adp5_2.Fill(dt5_2);
                        reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReporteCostosFechas.rdlc";
                        reportViewer.LocalReport.DataSources.Clear();
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Planilla", dt5));
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Directo", dt5_1));
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Indirecto", dt5_2));
                        reportViewer.LocalReport.Refresh();
                        ViewBag.ReportViewer = reportViewer;
                        break;
                    case 6:
                        string FechaIni = Anno + "-" + Mes + "-01";
                        string FechaFin = "";
                        if (Mes == "12")
                        {
                            int A = int.Parse(Anno) + 1;
                            FechaFin = A.ToString() + "-01-01";
                        }
                        else
                        {
                            int A = int.Parse(Mes) + 1;
                            FechaFin = Anno + "-" + A.ToString() + "-01";
                        }
                        SqlCommand cmd6 = new SqlCommand("Select Cl.Nombre AS 'Cliente', Cl.NumeroTelefonico, Cl.CorreoElectronico, Cl.NumeroIdentificacion, Cl.Direccion, Co.Nombre AS 'Gasto', Co.Fecha, Co.Monto, Tc.Nombre AS 'TipoCosto', Pr.Nombre AS 'Proyecto' from Proyectos as Pr join Clientes as Cl on Pr.NumeroIdentificacion = Cl.NumeroIdentificacion inner join Costos as Co on Pr.IdProyecto = Co.IdProyecto inner join TipoCosto as Tc on Co.Tipo = Tc.IDTipo where Co.IdProyecto = '" + IdProyecto + "' and Co.Fecha >= '" + FechaIni + "' and Co.Fecha < '" + FechaFin + "' and Co.Tipo = 1", conx);
                        SqlDataAdapter adp6 = new SqlDataAdapter(cmd6);
                        DataTable dt6 = new DataTable("Planilla");
                        adp6.Fill(dt6);
                        SqlCommand cmd6_1 = new SqlCommand("Select Cl.Nombre AS 'ClienteD', Cl.NumeroTelefonico AS 'NumeroTelefonicoD', Cl.CorreoElectronico AS 'CorreoElectronicoD', Cl.NumeroIdentificacion AS 'NumeroIdentificacionD', Cl.Direccion AS 'DireccionD', Co.Nombre AS 'GastoD', Co.Fecha AS 'FechaD', Co.Monto AS 'MontoD', Tc.Nombre AS 'TipoCostoD', Pr.Nombre AS 'ProyectoD' from Proyectos as Pr join Clientes as Cl on Pr.NumeroIdentificacion = Cl.NumeroIdentificacion inner join Costos as Co on Pr.IdProyecto = Co.IdProyecto inner join TipoCosto as Tc on Co.Tipo = Tc.IDTipo where Co.IdProyecto = '" + IdProyecto + "' and Co.Fecha >= '" + FechaIni + "' and Co.Fecha < '" + FechaFin + "' and Co.Tipo = 2", conx);
                        SqlDataAdapter adp6_1 = new SqlDataAdapter(cmd6_1);
                        DataTable dt6_1 = new DataTable("Directo");
                        adp6_1.Fill(dt6_1);
                        SqlCommand cmd6_2 = new SqlCommand("Select Cl.Nombre AS 'ClienteI', Cl.NumeroTelefonico AS 'NumeroTelefonicoI', Cl.CorreoElectronico AS 'CorreoElectronicoI', Cl.NumeroIdentificacion AS 'NumeroIdentificacionI', Cl.Direccion AS 'DireccionI', Co.Nombre AS 'GastoI', Co.Fecha AS 'FechaI', Co.Monto AS 'MontoI', Tc.Nombre AS 'TipoCostoI', Pr.Nombre AS 'ProyectoI' from Proyectos as Pr join Clientes as Cl on Pr.NumeroIdentificacion = Cl.NumeroIdentificacion inner join Costos as Co on Pr.IdProyecto = Co.IdProyecto inner join TipoCosto as Tc on Co.Tipo = Tc.IDTipo where Co.IdProyecto = '" + IdProyecto + "' and Co.Fecha >= '" + FechaIni + "' and Co.Fecha < '" + FechaFin + "' and Co.Tipo = 3", conx);
                        SqlDataAdapter adp6_2 = new SqlDataAdapter(cmd6_2);
                        DataTable dt6_2 = new DataTable("Indirecto");
                        adp6_2.Fill(dt6_2);
                        reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReporteCostosMes.rdlc";
                        reportViewer.LocalReport.DataSources.Clear();
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Planilla", dt6));
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Directo", dt6_1));
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Indirecto", dt6_2));
                        reportViewer.LocalReport.Refresh();
                        ViewBag.ReportViewer = reportViewer;
                        break;
                    case 7:
                        SqlCommand cmd7 = new SqlCommand("Select Cl.Nombre AS 'Cliente', Cl.NumeroTelefonico, Cl.CorreoElectronico, Cl.NumeroIdentificacion, Cl.Direccion, Co.Nombre AS 'Gasto', Co.Fecha, Co.Monto, Tc.Nombre AS 'TipoCosto', Pr.Nombre AS 'Proyecto' from Proyectos as Pr join Clientes as Cl on Pr.NumeroIdentificacion = Cl.NumeroIdentificacion inner join Costos as Co on Pr.IdProyecto = Co.IdProyecto inner join TipoCosto as Tc on Co.Tipo = Tc.IDTipo where Co.IdProyecto = '" + IdProyecto + "' and Co.Fecha >= '" + Fecha + "' and Co.Fecha <= '" + FechaF + "' and Co.Tipo = 1", conx);
                        SqlDataAdapter adp7 = new SqlDataAdapter(cmd7);
                        DataTable dt7 = new DataTable("Planilla");
                        adp7.Fill(dt7);
                        SqlCommand cmd7_1 = new SqlCommand("Select Cl.Nombre AS 'ClienteD', Cl.NumeroTelefonico AS 'NumeroTelefonicoD', Cl.CorreoElectronico AS 'CorreoElectronicoD', Cl.NumeroIdentificacion AS 'NumeroIdentificacionD', Cl.Direccion AS 'DireccionD', Co.Nombre AS 'GastoD', Co.Fecha AS 'FechaD', Co.Monto AS 'MontoD', Tc.Nombre AS 'TipoCostoD', Pr.Nombre AS 'ProyectoD' from Proyectos as Pr join Clientes as Cl on Pr.NumeroIdentificacion = Cl.NumeroIdentificacion inner join Costos as Co on Pr.IdProyecto = Co.IdProyecto inner join TipoCosto as Tc on Co.Tipo = Tc.IDTipo where Co.IdProyecto = '" + IdProyecto + "' and Co.Fecha >= '" + Fecha + "' and Co.Fecha <= '" + FechaF + "' and Co.Tipo = 2", conx);
                        SqlDataAdapter adp7_1 = new SqlDataAdapter(cmd7_1);
                        DataTable dt7_1 = new DataTable("Directo");
                        adp7_1.Fill(dt7_1);
                        SqlCommand cmd7_2 = new SqlCommand("Select Cl.Nombre AS 'ClienteI', Cl.NumeroTelefonico AS 'NumeroTelefonicoI', Cl.CorreoElectronico AS 'CorreoElectronicoI', Cl.NumeroIdentificacion AS 'NumeroIdentificacionI', Cl.Direccion AS 'DireccionI', Co.Nombre AS 'GastoI', Co.Fecha AS 'FechaI', Co.Monto AS 'MontoI', Tc.Nombre AS 'TipoCostoI', Pr.Nombre AS 'ProyectoI' from Proyectos as Pr join Clientes as Cl on Pr.NumeroIdentificacion = Cl.NumeroIdentificacion inner join Costos as Co on Pr.IdProyecto = Co.IdProyecto inner join TipoCosto as Tc on Co.Tipo = Tc.IDTipo where Co.IdProyecto = '" + IdProyecto + "' and Co.Fecha >= '" + Fecha + "' and Co.Fecha <= '" + FechaF + "' and Co.Tipo = 3", conx);
                        SqlDataAdapter adp7_2 = new SqlDataAdapter(cmd7_2);
                        DataTable dt7_2 = new DataTable("Indirecto");
                        adp7_2.Fill(dt7_2);
                        reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReporteCostosAnno.rdlc";
                        reportViewer.LocalReport.DataSources.Clear();
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Planilla", dt7));
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Directo", dt7_1));
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Indirecto", dt7_2));
                        reportViewer.LocalReport.Refresh();
                        ViewBag.ReportViewer = reportViewer;
                        break;
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult listarCantidadProyectosCliente()
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarClientesProyecto().ToList(), JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult listarCostosProyecto()
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    var query = (from c in db.Clientes join p in db.Proyectos on c.NumeroIdentificacion equals p.NumeroIdentificacion join a in db.Costos on p.IdProyecto equals a.IdProyecto group c by new { a.IdProyecto, c.Nombre, c.NumeroIdentificacion, Proyecto = p.Nombre } into aa select new ClaseCostoProyecto { idProyecto = aa.Key.IdProyecto, Cliente =  aa.Key.Nombre, numeroIdentificacion = aa.Key.NumeroIdentificacion, Proyecto = aa.Key.Proyecto}).ToList();
                    return Json(query, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult listarTiposCostos()
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarSelectTipoCostos().ToList(), JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

        public ActionResult listarEstadoCostos()
        {
            try
            {

                using (ProyectoCostosEntities db = new ProyectoCostosEntities())
                {

                    return Json(db.SPListarEstadoCostos().ToList(), JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error comuníquese con los desarrolladores");
            }
        }

    }
}