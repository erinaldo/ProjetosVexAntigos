using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using System.Threading;

namespace ServicosWEB
{
    public partial class JosaparEstoqueValidade : System.Web.UI.Page
    {
        string cnx = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (!IsPostBack)
            {
                CarregarPorSelecionado();
            }

            

        }

        



       

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

                  
        }

        private void CarregarPorSelecionado()
        {
            string sql = "PRC_Saldo_Josapar_vencimento " + txtCodigoBarrasBandeira.Text;
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            GridView1.DataBind();
        }

       

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarPorSelecionado();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
           
        }

        protected void btnPesquisar0_Click(object sender, EventArgs e)
        {
           
        }

        protected void GridView1_Unload(object sender, EventArgs e)
        {
            
        }

        protected void GridView1_Load(object sender, EventArgs e)
        {
            
        }
    }
}