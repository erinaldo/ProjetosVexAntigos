using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;

namespace Sistran.Library.TV
{
    public static class cDBTV
    {

        public static DataTable SelecionarDadosProcedure(string Conn, int idFilial, DateTime Inicio, DateTime fim)
        {
            SqlConnection cn = new SqlConnection(Conn);
            SqlCommand cmd = new SqlCommand(" ", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_FILIAL", idFilial);
            cmd.Parameters.AddWithValue("@DT_INICIAL", Inicio);
            cmd.Parameters.AddWithValue("@DT_FINAL", fim);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
}
