using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicosWEB.Sistranet.Coletor
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string sql = "SELECT * FROM USUARIO WHERE LOGIN='" + txtLogin.Text + "' AND SENHA='" + txtSenha.Text + "' AND ATIVO='SIM' ";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

            if (dt.Rows.Count > 0)
            {
                Session["User"] = dt;
                Response.Redirect("default.aspx", false);
            }
            else
                lblMensagem.Text = "Usuário Inválido";
        }

       
    }
}