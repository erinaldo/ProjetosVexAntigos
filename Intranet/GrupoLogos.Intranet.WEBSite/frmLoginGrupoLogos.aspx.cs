﻿using System;
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
using AprovacaoRequisicao.Library;

public partial class frmLoginGrupoLogos : System.Web.UI.Page
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

            input = Encoding.UTF8.GetBytes(valor); chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));
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
            DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            Session["DataConf"] = inicio + "|" + DateTime.Now.ToShortDateString();


            if (txtSenha.Text == "" || txtUser.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('Digite o usuário e senha.')", true);
                return;
            }

            //se o navegador enviar o parametro de banco de dados
            // a bliblioteca procura a conexao correspondente
            string cnn = "";
            if (Request.QueryString["b"] != null)            
                cnn = new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"].ToString());            
            else
            {
                //se o login não vier parametro de banco de dados 
                // a biblioteca procura os usuarios em todas as bases de dados
                // cadastrados no app.config
                Sistran.Logins.AcessoIntranet oAcessoIntranet = new Sistran.Logins.AcessoIntranet();
                cnn = oAcessoIntranet.ConexaoPorUsuario(txtUser.Text, txtSenha.Text);
                Session["Conn"] = cnn;

               // new cEmail().enviarEmail("Login de " + txtUser.Text + " na INTRANET", "Usuario logou cnn: " + cnn.Replace("@logos09022005$", "xxxxxxxx") + " data: " + DateTime.Now.ToString() + " IP: " + System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(), "moises@sistecno.com.br", "AlertaDoSite");

               // Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Login de " + txtUser.Text + " na INTRANET", "Usuario logou cnn: " + cnn.Replace("@logos09022005$", "xxxxxxxx") + " data: " + DateTime.Now.ToString() + " IP: " + System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(), "mail.sistecno.com.br", "mo2404");
            }


            if (cnn == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('Usuário não encontrado. Verifique o usuário e senha.')", true);
                Label1.Text = "Usuário e senha não encontrados.";
            }
            else
            {

                //busca o id e o nome do usuario que esta se logando
                List<SistranMODEL.Usuario> ILusuario = SistranBLL.Usuario.Login_Alone(txtUser.Text.ToUpper(), txtSenha.Text.ToUpper(), cnn, true);

                Session["USUARIO"] = ILusuario;
                Session["ProfileIndex"] = 0;                
                Session["ConnLogin"] = cnn;
                Session["Conn"] = cnn;
                Session.Timeout = 300000;           
                

                
                //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "LOGOU NO SITE", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
                //ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.location.href='index.htm';</script>");
                //ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.location.href='default.aspx';</script>");

                Response.Redirect("index.htm");
                
            }
        }
        catch (Exception wx)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "akey", "javascript:window.alert('" + wx.Message.Replace("'", "´") + "')", true);
            return;
        }
    }
}