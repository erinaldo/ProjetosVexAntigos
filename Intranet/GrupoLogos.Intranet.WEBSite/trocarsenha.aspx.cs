using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class trocarsenha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                HttpContext.Current.Session["ConnLogin"] = Sistran.Library.Helpers.RetornarCnnIntranet();
                DataTable dt = new SistranBLL.Usuario().Consultar(Request.QueryString["id"]);

                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Não foi possivel realizar a operação.')", true);
                    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "f", "window.close();", true);
                }
                txtUsuario.Text = dt.Rows[0]["Login"].ToString();
                txtSenhaAtual.Focus();


            }
        }
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        if (txtNovasenha.Text =="" || txtConfirmaNovaSenha.Text=="" || txtSenhaAtual.Text =="")
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Todos os campos são obrigatórios.')", true);

        if (txtNovasenha.Text != txtConfirmaNovaSenha.Text)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Nova senha e confirmação de senhas não coincidem.')", true);
            txtNovasenha.Text = "";
            txtConfirmaNovaSenha.Text = "";
            txtNovasenha.Focus();
        }

        DataTable dt = new SistranBLL.Usuario().Consultar(Request.QueryString["id"]);

        string senhaAtual = dt.Rows[0]["SENHA"].ToString().ToUpper().Trim();

        if (senhaAtual != txtSenhaAtual.Text.Trim().ToUpper())
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Senha Invalida.')", true);
            txtSenhaAtual.Text = "";
            txtSenhaAtual.Focus();
        }

        string comando = "UPDATE USUARIO SET SENHA='" + txtNovasenha.Text.ToUpper().Trim() + "' WHERE IDUSUARIO=" + Request.QueryString["id"].ToString();
        Sistran.Library.GetDataTables.ExecutarSemRetorno(comando, Sistran.Library.Helpers.RetornarCnnIntranet());

        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Alteração efetuada com sucesso.')", true);

        string Carta = "Caro Usuario " + dt.Rows[0]["NOME"].ToString().ToUpper().Trim()  + "<br>";
        Carta += " Sua nova senha é: " + txtNovasenha.Text.Trim().ToUpper();
        Carta += "<BR>";
        Carta += "<BR>";
        Carta += "<BR> PARA ALTERAR SUA SENHA CLIQUE <A HREF=http://www.grupologos.com.br/intranet/trocarsenha.aspx?id=" + Request.QueryString["id"].ToString() + ">AQUI</A>";

        string email = dt.Rows[0]["EMAIL"].ToString().ToUpper().Trim();

        if (email == "")
            email = "moises@sistecno.com.br";
        Sistran.Library.EnviarEmails.EnviarEmail(email, "sistema@grupologos.com.br", "Alteração de Senha", Carta, "mail.grupologos.com.br", "logos0902");
        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "f", "window.close();", true);


    }
}