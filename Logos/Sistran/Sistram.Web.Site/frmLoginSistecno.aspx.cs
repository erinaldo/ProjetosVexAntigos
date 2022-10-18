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
using System.Net;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;

public partial class frmLoginSistecno : System.Web.UI.Page
{

    private static byte[] chave = { };
    private static byte[] iv = { 12, 34, 56, 78, 90, 102, 114, 126 };

    public static string Criptografar(string valor, string chaveCriptografia) 
    {
        DESCryptoServiceProvider des; 
        MemoryStream ms; 
        CryptoStream cs; byte[] input; 
        try
        { 
            des = new DESCryptoServiceProvider(); 
            ms = new MemoryStream(); 

            input = Encoding.UTF8.GetBytes(valor);chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8)); 
            cs = new CryptoStream(ms, des.CreateEncryptor(chave, iv), CryptoStreamMode.Write); 
            cs.Write(input, 0, input.Length); 
            cs.FlushFinalBlock(); 

            return Convert.ToBase64String(ms.ToArray()); 
        }
        catch (Exception ex) 
        { 
            throw ex; 
        } 
    } 

    public static string Descriptografar(string valor, string chaveCriptografia) 
    {
        DESCryptoServiceProvider des; 
        MemoryStream ms; 
        CryptoStream cs; byte[] input; 

        try 
        { 
            des = new DESCryptoServiceProvider(); 
            ms = new MemoryStream(); 

            input = new byte[valor.Length];
            input = Convert.FromBase64String(valor.Replace(" ", "+")); 

            chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8)); 

            cs = new CryptoStream(ms, des.CreateDecryptor(chave, iv), CryptoStreamMode.Write); 
            cs.Write(input, 0, input.Length); 
            cs.FlushFinalBlock(); 

            return Encoding.UTF8.GetString(ms.ToArray()); 
        } 
        catch (Exception ex) 
        { 
            throw ex; 
        }
    }

    private void VerificaLicenca()
    {
        string m = FuncoesGerais.VerificarLicenca(Session["ConexaoCliete"].ToString());

        // tem licença, ou seja, tem prazo de validade
        //grava um xmlserealizado na pasta config/clientes
        if (m != "")
        {
            string data = "";

            try
            {
                DataTable dt = new DataTable("xx");
                dt.Columns.Add("x");
                WebClient webClient = new WebClient();
                data = webClient.DownloadString("http://www.sistecno.com.br/novodotnet/web/licserver/" + m).ToString();

                string s = Criptografar(data, "#!$a36?@");



                DataRow or = dt.NewRow();
                or[0] = s;
                dt.Rows.Add(or);


                dt.WriteXml(Server.MapPath(@"~/config/clientes/") + m.Replace(".txt", ".xml"));
                dt.Dispose();

               
            }
            catch (Exception )
            {
            }


            DataSet dtLocal = new DataSet();
            dtLocal.ReadXml(Server.MapPath(@"~/config/clientes/") + m.Replace(".txt", ".xml"));

            try
            {
                if (dtLocal.Tables[0].Rows.Count == 0)
                    throw new Exception("erro");
            }
            catch (Exception)
            {

                Response.Write("<script>alert('Arquivo de Licenca invalido Contacte a Sistecno.'); window.location.href('fim.aspx');</script>");

            }

            DateTime dataAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dataArquivo = Convert.ToDateTime(Descriptografar(dtLocal.Tables[0].Rows[0][0].ToString(), "#!$a36?@"));

            try
            {
                if (dataAtual > dataArquivo)
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br",
                        ConfigurationSettings.AppSettings["emailPedido"].ToString(),
                        "Licenca Exirada ",
                        "LICENCA EXPIRADA " + m,
                        ConfigurationSettings.AppSettings["smtp"].ToString(), ConfigurationSettings.AppSettings["senhasmtp"].ToString(), "moises@sistecno.com.br");

                    throw new Exception("erro");
                }

                TimeSpan d = dataArquivo - dataAtual;

                int diasFaltentes = d.Days;

                if (diasFaltentes <= 5)
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br",
                        ConfigurationSettings.AppSettings["emailPedido"].ToString(),
                        "Licenca Proxima de Expirar ",
                        "Licenca Proxima de Expirar " + m,
                        ConfigurationSettings.AppSettings["smtp"].ToString(), ConfigurationSettings.AppSettings["senhasmtp"].ToString(), "moises@sistecno.com.br");
                }
                
            }
            catch (Exception)
            {

                Response.Write("<script>alert('Arquivo de Licenca invalido Contacte a Sistecno.'); window.location.href('fim.aspx');</script>");
            }
        }
    }      

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmensagem.Text = "";


        if (!IsPostBack)
        {
            Session.Clear();
            Request.Cookies.Clear();
           // txtUser.Text = DateTime.Now.ToShortDateString();
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
                //Session["ConexaoCliete"] = cnn;
            }
            else
            {
                //se o login não vier parametro de banco de dados 
                // a biblioteca procura os usuarios em todas as bases de dados
                // cadastrados no app.config
                Sistran.Logins.Acesso oAcesso = new Sistran.Logins.Acesso();
                cnn = oAcesso.ConexaoPorUsuario(txtUser.Text, txtSenha.Text);

                try
                {
                    //Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Login de " + txtUser.Text, "Usuario logou cnn: " + cnn + " data: " + DateTime.Now.ToString() + " IP: " + System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(), "mail.sistecno.com.br", "mo2404", "moises@sistecno.com.br");
                }
                catch (Exception)
                {
                }

                //Response.Write(cnn);
            }


            if (cnn == "")
            {                
                ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('Usuário não encontrado. Verifique o usuário e senha.')", true);
                lblmensagem.Text = "Usuário e senha não encontrados.";
                return;
            }
            else
            {
                
                VerificaLicenca();             

                //busca o id e o nome do usuario que esta se logando
                List<SistranMODEL.Usuario> ILusuario = SistranBLL.Usuario.Login_Alone(txtUser.Text.ToUpper(), txtSenha.Text.ToUpper(), cnn, false);

                if (ILusuario[0].Ativo != "SIM")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('Usuário Desabilitado.')", true);
                    lblmensagem.Text = "Usuário Desabilitado";
                    return;
                }

                Session["USUARIO"] = ILusuario;
                Session["ProfileIndex"] = 0;
                Session["Conn"] = "";
                Session["ConnLogin"] = cnn;
                Session.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeoutSession"].ToString());
                Session["AtivarBaseAntiga"] = ConfigurationSettings.AppSettings["AtivarBaseAntiga"].ToString();
                                
                //busca as divisoes da empresa logada (a primeira empresa)
                Session["Divisoes"] = SistranDAO.Cliente.RetornaDivisoesClientes(ILusuario[0].UsuarioId.ToString(), ILusuario[0].EmpresaId.ToString());
                Session["DivisoesCompleta"] = SistranDAO.Cliente.DivisoesCompleta(ILusuario[0].EmpresaId.ToString());

//                ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open('Default.aspx?acao=login');</script>");
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "LOGOU NO SITE", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
                //FormsAuthentication.Authenticate = true;
               // ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.location.href='index.htm';</script>");

                ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open('index.htm');</script>");

                try
                {


                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~\\imgReport"));
                    FileInfo[] files = dir.GetFiles();

                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].LastWriteTime < DateTime.Now.AddMinutes(-60))
                        {
                            File.Delete(Server.MapPath("~\\imgReport\\") + files[i].ToString());
                        }
                    }
                }
                catch (Exception)
                {
                }



            }
        }
        catch (Exception wx)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('" + wx.Message.Replace("'","´") + "')", true);
            return;
        }
    }

   

}