using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ServicosWEB
{
    public partial class GaiolaRelatoriosDetalhes : System.Web.UI.Page
    {
        string cnx = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           // lblMensagem.Text = "";
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (!IsPostBack)
            {
                if (Request.QueryString["tipo"] == "mov")
                {
                    lbltitulo.Text = "Relátorio Movimento - Filial: " + Request.QueryString["filial"] + " <br>Data: " + DateTime.Parse(Session["ini"].ToString()).ToString("yyyy-MM-dd HH:mm:00") + "  até: " + DateTime.Parse(Session["fim"].ToString()).ToString("yyyy-MM-dd HH:mm:59");
                    carregar();
                }
                else
                {
                    lbltitulo.Text = "Relátorio Divergências - Filial: " + Request.QueryString["filial"] +  DateTime.Parse(Session["ini"].ToString()).ToString("yyyy-MM-dd HH:mm:00") + "  até: " + DateTime.Parse(Session["fim"].ToString()).ToString("yyyy-MM-dd HH:mm:59");
                    carregarDiv();
                }

                
            }
        }

        private void carregarDiv()
        {
            string di = Request.QueryString["di"];
            string df = Request.QueryString["df"];
            string filial = Request.QueryString["filial"].Split('-')[0].Trim();

            string sql = "";

            sql = "EXEC PRC_RPT_GAIOLA_DIVERG '" + DateTime.Parse(di).ToString("yyyy-MM-dd") + "', '" + DateTime.Parse(df).ToString("yyyy-MM-dd") + "', " + int.Parse(filial);
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            GridView1.DataBind();
        }

        private void carregar()
        {
            string di = Request.QueryString["di"];
            string df = Request.QueryString["df"];            
            string filial = Request.QueryString["filial"].Split('-')[0].Trim();

            string sql ="";

            sql = "EXEC PRC_RPT_GAIOLA '" + DateTime.Parse(Session["ini"].ToString()).ToString("yyyy-MM-dd HH:mm:00") + "', '" + DateTime.Parse(Session["fim"].ToString()).ToString("yyyy-MM-dd HH:mm:59") + "', " + int.Parse(filial);
//            sql = "EXEC PRC_RPT_GAIOLA '" + DateTime.Parse(di).ToString("yyyy-MM-dd") + "', '" + DateTime.Parse(df).ToString("yyyy-MM-dd 23:59:59") + "', " + int.Parse(filial);
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            GridView1.DataBind();
        }

        //protected void btnPesquisar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridView1.DataSource = null;
        //        GridView1.DataBind();

        //        if (txtDataI.Text == "" || txtDataF.Text == "")
        //        {
        //            lblMensagem.Text = "Informe o período.";
        //            return;
        //        }


        //        DateTime di = DateTime.Parse(txtDataI.Text);
        //        DateTime df = DateTime.Parse(txtDataF.Text);

        //        string sql = "";
        //        if (DropDownList1.SelectedIndex == 0)
        //            sql = "exec PRC_RPT_VOLUMES '" + di.ToString("yyyy/MM/dd") + "', '" + df.ToString("yyyy/MM/dd") + "'";
        //        else
        //            sql = "exec PRC_RPT_VOLUMES_DIVERG '" + di.ToString("yyyy/MM/dd") + "', '" + df.ToString("yyyy/MM/dd") + "'";

        //        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

        //        GridView1.DataSource = dt;
        //        GridView1.DataBind();

        //        DataRow r = dt.NewRow();
        //        r[0] = "";
        //        r[1] = "TOTAL";
        //        r[2] = int.Parse((((DataTable)GridView1.DataSource).Compute("sum(Gaiolas)", "").ToString()));
        //        r[3] = int.Parse((((DataTable)GridView1.DataSource).Compute("sum(Volumes)", "").ToString()));
                

        //        //int totalg = int.Parse((((DataTable)GridView1.DataSource).Compute("sum(Gaiolas)", "").ToString()));
        //        //int totalv = int.Parse((((DataTable)GridView1.DataSource).Compute("sum(Volumes)", "").ToString()));


        //        lblTotais.Text = "TOTAL DE GAIOLAS: " + totalg.ToString() + "<br>TOTAL DE VOLUMES: " + totalv.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensagem.Text = ex.Message;
        //    }
        //}

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink HyperLink1 = (HyperLink)e.Row.FindControl("HyperLink1");

            

            if (HyperLink1 != null)
            {

                System.Data.DataRowView o = (System.Data.DataRowView)e.Row.DataItem;

                HyperLink1.NavigateUrl = "GaiolaRelatoriosVolumesDetalhes.aspx?tipo=" + Request.QueryString["tipo"] + "&filial=" + Request.QueryString["filial"] + "&di=" + Request.QueryString["di"] + "&df=" + Request.QueryString["df"] + "&id=" + o["GAIOLA"].ToString();
                HyperLink1.Text = o["Data"].ToString().Replace(" 00:00:00", "");
                HyperLink1.Target = "_blank";

            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}