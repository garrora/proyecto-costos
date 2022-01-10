using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCostos.Reports
{
    public class ClaseReportesConexion
    {
        
        public static CrystalDecisions.Shared.ConnectionInfo GetConnection()
        {

            CrystalDecisions.Shared.ConnectionInfo connectionInfo = new CrystalDecisions.Shared.ConnectionInfo();
            connectionInfo.ServerName = @"localhost";
            connectionInfo.DatabaseName = "ProyectoCostos";
            connectionInfo.IntegratedSecurity = true;

            return connectionInfo;

        }

    }
}