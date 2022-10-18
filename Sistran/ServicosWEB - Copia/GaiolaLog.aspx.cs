using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

namespace ServicosWEB
{
    public partial class GaiolaLog : System.Web.UI.Page
    {
        string cnx = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();           
        }

        DataTable dx = null;
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                GridView1.DataSource = null;
                GridView1.DataBind();

                string sql = "SELECT TOP 100  GH.IDGAIOLA GAIOLA, GH.DATA , GH.ACAO , U.LOGIN , U.NOME USUARIO ";
                sql += " FROM GAIOLAHISTORICO GH ";
                sql += " LEFT JOIN GAIOLA G ON G.IDGAIOLA = GH.IDGAIOLA ";
                sql += " LEFT JOIN USUARIO U ON U.IDUSUARIO = GH.IDUSUARIO ";
         
                if (txtGaiola.Text.Length > 0)
                    sql += " WHERE GH.IDGAIOLA =  " + txtGaiola.Text;

                sql += " ORDER BY GH.IDGAIOLA DESC, GH.DATA DESC ";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                Session["dx"] = dt;

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnPesquisar0_Click(object sender, EventArgs e)
        {
            GridView1.PagerSettings.Visible = false;


            GridView1.DataSource = ((DataTable)Session["dx"]);
            GridView1.DataBind();


            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            GridView1.PagerSettings.Visible = true;

            GridView1.DataSource = ((DataTable)Session["dx"]);
            GridView1.DataBind();
        }
    }
}