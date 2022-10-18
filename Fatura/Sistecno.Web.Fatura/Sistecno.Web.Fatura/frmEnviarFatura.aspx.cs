using System;
using System.Web.UI.WebControls;
using System.Data;

namespace Sistecno.Web.Fatura
{
    public partial class frmEnviarFatura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                Session["cnx"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                DataSet ds = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC GerarLinkFatura " + TextBox1.Text, Session["cnx"].ToString());

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgument.ToString() != "" && TextBox1.Text != "")
                {
                    Sistran.Library.EnviarEmails.EnviarEmailx(txtemail.Text, "moises@sistecno.com.br", "FATURA", "Segue o Email da Fatura Número: " + e.CommandArgument.ToString() + "<br>Clique <a href='http://www2.logoslogistica.com.br/DUPLICATAS/FATURA.ASPX?IDTITULO=" + e.CommandName + "'>Aqui</a> ", "Fatura  Site");
                    Response.Write("E-mail Enviado");

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}