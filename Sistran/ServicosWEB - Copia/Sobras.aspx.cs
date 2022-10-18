using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Web.UI;
using System.Web;

namespace ServicosWEB
{
    public partial class Sobras : System.Web.UI.Page
    {
        string cnx = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (Session["User"] == null)
                Response.Redirect("login.aspx");

            if (!IsPostBack)
            {
                carregarComboRegioes();                
            }
            

        }

        private void carregarComboRegioes()
        {
            string sql = "SELECT DISTINCT CodigoRegiao, NomeRegiao FROM REPOSICAOROGE ";
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);
            cboStatus.DataSource = dt;
            cboStatus.DataTextField = "NomeRegiao";
            cboStatus.DataValueField = "CodigoRegiao";
            cboStatus.DataBind();
            cboStatus.Items.Insert(0, "SELECIONE");
        }        

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatus.SelectedIndex == 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
                string sql = " SELECT ";
                sql += " RG.IDREPOSICAOROGE[COD], RG.CHAVE, RG.STATUS, RG.CLIENTEESPECIAL ESPECIAL, UsuarioColetor USUARIO, ";
                sql += " (SELECT COUNT(*) FROM REPOSICAOROGEVOLUME RV WHERE RV.IDRESPOSICAOROGE = RG.IDREPOSICAOROGE) VOLUMES, ";
                sql += " (SELECT COUNT(*) FROM REPOSICAOROGEVOLUME RV WHERE RV.IDRESPOSICAOROGE = RG.IDREPOSICAOROGE AND CONFERIDO ='SIM') [VOL.CONFERIDOS], ";
                sql += " (SELECT COUNT(*) FROM REPOSICAOROGEITEM RV WHERE RV.IDREPOSICAOROGE = RG.IDREPOSICAOROGE) ITENS, ";
                sql += " (SELECT COUNT(*) FROM REPOSICAOROGEITEM RV WHERE RV.IDREPOSICAOROGE = RG.IDREPOSICAOROGE AND QUANTIDADELIDO>0) [ITENS CONFERIDOS],  ";
                sql += "  DataEnvioRoge[DATA ENVIO ROGE], DescricaoEnvioRoge [RESULTADO ENVIO ROGE], VALOR [TOTAL(R$)]";
                sql += " FROM REPOSICAOROGE RG ";

                sql += " WHERE CODIGOREGIAO =" + cboStatus.SelectedValue;
                sql += " and DataDaInclusao >=getDate()-30";
                //   sql += " AND STATUS = 'AGUARDANDO CONFERENCIA' ";
                sql += " ORDER BY  1 DESC ";

                GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {


        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "VER")
            {
                Response.Redirect("EnviarConferenciaDET.aspx?id=" + e.CommandName.ToString(), false);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void CarregarGrid()
        {


            string sql = " PRC_SOBRAS_ROGE  @CODIGO_REGIAO, '@CHAVE', '@DT_INI', '@DT_FIM'";

            if (cboStatus.SelectedIndex > 0)
                sql = sql.Replace("@CODIGO_REGIAO", cboStatus.SelectedValue);
            else
                sql = sql.Replace("@CODIGO_REGIAO", "NULL");
            
            sql = sql.Replace("@CHAVE", txtChave.Text);


            if (txtDataI.Text.Length > 0 && txtDataF.Text.Length>0)
            {
                sql = sql.Replace("@DT_INI", DateTime.Parse(txtDataI.Text).ToString("yyyy-MM-dd"));
                sql = sql.Replace("@DT_FIM", DateTime.Parse(txtDataF.Text).ToString("yyyy-MM-dd"));
            }
            else
            {
                sql = sql.Replace("'@DT_INI'", "NULL");
                sql = sql.Replace("'@DT_FIM'", "NULL");
            }


            lblQuantidadeDocumentos.Text = "";

            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);
            GridView1.DataBind();

            Session["dtPesq"] = (DataTable)GridView1.DataSource;

            lblQuantidadeDocumentos.Text = ((DataTable)GridView1.DataSource).Rows.Count+ " Itens";

            if (((DataTable)GridView1.DataSource).Rows.Count > 0)
                GridView1.PageIndex = 0;

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Excel"+Guid.NewGuid()+".xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //am getting my grid's column headers


            for (int j = 0; j < ((DataTable)Session["dtPesq"]).Columns.Count; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(((DataTable)Session["dtPesq"]).Columns[j].ColumnName);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");

            foreach (DataRow row in ((DataTable)Session["dtPesq"]).Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < ((DataTable)Session["dtPesq"]).Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");

                    if(((DataTable)Session["dtPesq"]).Columns[i].ColumnName.ToUpper() == "CHAVE")
                        HttpContext.Current.Response.Write("'"+ row[i].ToString());
                    else
                       HttpContext.Current.Response.Write(row[i].ToString() );

                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }
    }
}