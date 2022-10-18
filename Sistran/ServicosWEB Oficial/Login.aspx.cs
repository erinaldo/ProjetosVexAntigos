using System;
using System.Data;

namespace ServicosWEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Logar();
        }

        private void Logar()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string sql = "SELECT * FROM USUARIO WHERE LOGIN='" + TextBox1.Text + "' AND SENHA='" + TextBox2.Text + "' AND ATIVO='SIM' ";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

            if (dt.Rows.Count > 0)
            {
                Session["User"] = dt;
                Response.Redirect("EnviarConferencia.aspx", false);
            }
            else
                lblMensagem.Text = "Usuário Inválido";

        }
    }
}