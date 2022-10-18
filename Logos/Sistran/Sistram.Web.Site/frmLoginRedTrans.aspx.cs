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

public partial class frmLoginRedTrans : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (!IsPostBack)
        {
            Session.Clear();
            Request.Cookies.Clear();
        }


    }
    protected void btnLogar_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Request.Cookies.Clear();
        try
        {
            if (txtSenha.Text == "" || txtUser.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('Digite o usuário e senha.')", true);
                return;

            }

            //se o navegador enviar o parametro de banco de dados
            // a bliblioteca procura a conexao correspondente
            string cnn = "";
            if (Request.QueryString["b"] != null)
            {
                cnn = new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"].ToString());
            }
            else
            {
                //se o login não vier parametro de banco de dados 
                // a biblioteca procura os usuarios em todas as bases de dados
                // cadastrados no app.config
                Sistran.Logins.Acesso oAcesso = new Sistran.Logins.Acesso();
                cnn = oAcesso.ConexaoPorUsuario(txtUser.Text, txtSenha.Text);
            }

            if (cnn == "")
            {
                //Response.Write("<script>Alert('Usuário e senha não encontrados.');</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('Usuário não encontrado. Verifique o usuário e senha.')", true);
                Label1.Text = "Usuário e senha não encontrados.";
            }
            else
            {                
                //busca o id e o nome do usuario que esta se logando
                List<SistranMODEL.Usuario> ILusuario = SistranBLL.Usuario.Login_Alone(txtUser.Text.ToUpper(), txtSenha.Text.ToUpper(), cnn, false);

                Session["USUARIO"] = ILusuario;
                Session["ProfileIndex"] = 0;
                Session["Conn"] = "";
                Session["ConnLogin"] = cnn;
                Session.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeoutSession"].ToString());
                
                //busca as divisoes da empresa logada (a primeira empresa)
                Session["Divisoes"] = SistranDAO.Cliente.RetornaDivisoesClientes(ILusuario[0].UsuarioId.ToString(), ILusuario[0].EmpresaId.ToString());
                Session["campos_reports"] = FuncoesGerais.LoadDataSetCamposRepots(ILusuario[0].UsuarioId);
                
//                ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open('Default.aspx?acao=login');</script>");
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "LOGOU NO SITE", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath),  cnn);
                ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open('index.htm', '_top');</script>");

            }
        }
        catch (Exception wx)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('" + wx.Message.Replace("'","´") + "')", true);
            return;
        }

    }
}