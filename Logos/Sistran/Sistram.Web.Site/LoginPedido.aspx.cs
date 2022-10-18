using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SistranBLL;
using System.Collections.Generic;

public partial class LoginPedido : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (!IsPostBack)
        {
            Session.Clear();           

            if(this.Request.QueryString["e"] != null)
            {
                string em = Server.UrlDecode(Request.QueryString["e"]).Replace("|||", "@").Replace("***", ".");
                txtUser.Text = Request.QueryString["l"];
                txtUser.Enabled = false;
            }             
        }
    }
    protected void btnLogar_Click(object sender, EventArgs e)
    {
        Session.Clear();
        try
        {
            if (txtSenha.Text == "" || txtUser.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('Digite o usuário e senha.')", true);
                return;

            }

            Sistran.Logins.Acesso oAcesso = new Sistran.Logins.Acesso();
            string cnn = oAcesso.ConexaoPorUsuario(txtUser.Text, txtSenha.Text);


            if (cnn == "")
            {
                //Response.Write("<script>Alert('Usuário e senha não encontrados.');</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('Usuário não encontrado. Verifique o usuário e senha.')", true);
                Label1.Text = "Usuário e senha não encontrados.";
            }
            else
            {
                //Label1.Text = cnn;

                //busca o id e o nome do usuario que esta se logando
                List<SistranMODEL.Usuario> ILusuario = SistranBLL.Usuario.Login_Alone(txtUser.Text.ToUpper(), txtSenha.Text.ToUpper(), cnn, false);

                Session["USUARIO"] = ILusuario;
                Session["ProfileIndex"] = 0;
                Session["Conn"] = "";
                Session["ConnLogin"] = cnn;
                Session["Conn"] = cnn;
                Session.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeoutSession"].ToString());

                //busca as divisoes da empresa logada (a primeira empresa)
                Session["Divisoes"] = SistranDAO.Cliente.RetornaDivisoesClientes(ILusuario[0].UsuarioId.ToString(), ILusuario[0].EmpresaId.ToString());
                Session["DivisoesCompleta"] = SistranDAO.Cliente.DivisoesCompleta(ILusuario[0].EmpresaId.ToString());

                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "LOGOU NO SITE", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                Response.Redirect("default.aspx?id_documento=" + Request.QueryString["id_documento"] + "&tipo=" + Request.QueryString["tipo"]);

                ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open('default.aspx?id_documento=" + Request.QueryString["id_documento"] + "&tipo="+Request.QueryString["tipo"]+"');</script>");


            }
        }
        catch (Exception wx)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('" + wx.Message.Replace("'","´") + "')", true);
            return;
        }

    }
}