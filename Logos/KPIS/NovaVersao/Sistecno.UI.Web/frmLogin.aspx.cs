using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistecno.UI.Web
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                    FormsAuthentication.SignOut();

                //   Session.Abandon();

                if (Request.Cookies["LoginSenha"] != null)
                {
                    HttpCookie cookie = Request.Cookies["LoginSenha"];
                    txtEmail.Text = cookie.Value.Split('|')[0];
                    txtSenha.Text = cookie.Value.Split('|')[1];
                }
            }
        }

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            Sistecno.DAL.BD.ConexaoPrincipal cx = new Sistecno.DAL.BD.ConexaoPrincipal("");

            try
            {

                string idCliente = "";
                string cnx = cx.RetornarCxCliente(txtEmail.Text.Trim(), txtSenha.Text, ref idCliente);


                if (cnx == "")
                    throw new Exception("Empresa não encontrada");


                Sistecno.DAL.Models.Usuario usu = new Sistecno.DAL.Models.Usuario();
                usu.Login = txtEmail.Text;
                usu.Senha = txtSenha.Text;

                Sistecno.DAL.Models.Usuario ret = new Sistecno.BLL.Usuario().Logar(usu, cnx);


                if (ret == null)
                {
                    if (ret == null)
                        throw new Exception("Usuário ou senha inválido");
                }



                //controla as informações  do suaurio
                Session["USUARIOLOGADO"] = ret;
                //Session["EMPRESA"] = idCliente;

                Session["USUARIOCLIENTE"] = null;
                Session["dtProdutos"] = null;
                Session["car"] = null;
                Session["dvs"] = null;


                Session["USUARIOCLIENTE"] = new Sistecno.BLL.Usuario().RetornarInformacoesUsuarioCliente(ret.IDUsuario, cnx);



                //controla a string de conexao com o cliente
                Session["CNX"] = cnx;
                Session["IDCLIENTE"] = idCliente;
                //controla as informações do plano do cliente
                Session["PLANOCLIENTE"] = new Sistecno.BLL.Plano().Retornar(int.Parse(idCliente), new Sistecno.DAL.BD.ConexaoPrincipal("").CxPrincipal);



                #region Grava Cookie Login

                HttpCookie cookie = new HttpCookie("LoginSenha");
                cookie.Value = txtEmail.Text + "|" + txtSenha.Text;
                DateTime dtNow = DateTime.Now;
                TimeSpan tsMinute = new TimeSpan(10, 0, 0, 0);
                cookie.Expires = dtNow + tsMinute;
                Response.Cookies.Add(cookie);

                #endregion



                ClientScript.RegisterClientScriptBlock(GetType(), "redirecionaPg", "<script> window.open('default.aspx', '_top') </script>");
                //FormsAuthentication.RedirectFromLoginPage(txtEmail.Text, true);
                FormsAuthentication.Authenticate(txtEmail.Text, txtSenha.Text);
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;

                //lblErro.Text = ex.Message;
                //Sistecno.DAL.Models.Usuario usu = new Sistecno.DAL.Models.Usuario();
                //usu.Login = txtEmail.Text;
                //usu.Senha = txtSenha.Text;

                //string idCliente = "";
                //string cnx = cx.RetornarCxCliente(txtEmail.Text.Trim(), txtSenha.Text, ref idCliente);
                //Session["IDCLIENTE"] = idCliente;
                //Session["CNX"] = new frwSistecno.ConexaoPrincipal("").CxPrincipal;
                //Sistecno.DAL.Models.Usuario ret = new Sistecno.BLL.Usuario().LogarSistecno(usu, Session["CNX"].ToString());
                //Session["USUARIOLOGADO"] = ret;

                // ClientScript.RegisterClientScriptBlock(GetType(), "redirecionaPg", "<script> window.open('Default.aspx?acao=chamados&plat=chamado', '_top') </script>");

                //                    ClientScript.RegisterClientScriptBlock(GetType(), "redirecionaPg", "<script> window.open('HELPDESK/WEB00200.aspx?titulo=CHAMADOS', '_top') </script>");


            }
        }
    }
}