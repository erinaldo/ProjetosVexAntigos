using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
//using System.Data.OracleClient;
//using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicosWEB.Financeiro
{
    public partial class TitulosSistranCs : System.Web.UI.Page
    {
        string cnx = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin("prc_RelEnvioCsTitulo " + TextBox1.Text, cnx);

                Label1.Text = dt.Rows.Count.ToString() + " Titulos encontrados.";

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "ver")
            {
                carregarOracle(e.CommandName);
            }
        }

        private void carregarOracle(string commandName)
        {
            try
            {
               
                OracleConnection conn = new OracleConnection();
               

                string oradb = "Data Source=(DESCRIPTION="
                     + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))"
                     + "(CONNECT_DATA=(SERVICE_NAME=csorcl)));"
                     + "User Id=csintegracao;Password=wxmj6evc8k;";

                conn.ConnectionString = oradb;
                conn.Open();

                string sqlora = "select * from Fatura where Idtitulo  = " + commandName;
                var oracleCommand = new OracleCommand(sqlora, conn);

                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);

                DataTable dt = new DataTable();
                oracleDataAdapter.Fill(dt);

                conn.Close();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
            

        }
    }
}