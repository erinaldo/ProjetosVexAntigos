﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using SistranMODEL;
using System.IO;
using System.Data;
using System.Net;
using System.Xml.Serialization;
using System.Xml;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class Site : System.Web.UI.MasterPage
{
    #region Events

    private void CarregarCboCLiente()
    {
        LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
        Cliente cli = new Cliente();
        if (cli.ClienteImagen != null)
        {
            MemoryStream ms = new MemoryStream(cli.ClienteImagen);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
        }

        if (LUser[0].NomeEmpresa.Trim().ToUpper() == "DICATE")
        {
            HtmlTableCell tr0 = (HtmlTableCell)this.FindControl("tdDivivisoriaRetratil");
            tr0.Style.Add("vertical-align", "top");            
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string cnn = "";

        try
        {

            if (Request.QueryString["b"] != null)
            {
                cnn = new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"].ToString());
                //Session["ConexaoCliete"] = cnn;
            }

            if (Session["Usuario"] == null)
            {
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
                    cnn = oAcesso.ConexaoPorUsuario(Request.QueryString["u"], Request.QueryString["s"]);

                    try
                    {
                        //Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Login de " + txtUser.Text, "Usuario logou cnn: " + cnn + " data: " + DateTime.Now.ToString() + " IP: " + System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(), "mail.sistecno.com.br", "mo2404", "moises@sistecno.com.br");
                    }
                    catch (Exception)
                    {
                    }
                }


                // VerificaLicenca();             

                //busca o id e o nome do usuario que esta se logando
                List<SistranMODEL.Usuario> ILusuario = SistranBLL.Usuario.Login_Alone2(Request.QueryString["u"], cnn, false);



                Session["USUARIO"] = ILusuario;
                Session["ProfileIndex"] = 0;
                Session["Conn"] = cnn;
                Session["ConnLogin"] = cnn;                
                Session["campos_reports"] = FuncoesGerais.LoadDataSetCamposRepots(ILusuario[0].UsuarioId);

                Session.Timeout = 99999; //Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeoutSession"].ToString());
                Session["AtivarBaseAntiga"] = ConfigurationSettings.AppSettings["AtivarBaseAntiga"].ToString();

                //busca as divisoes da empresa logada (a primeira empresa)
                Session["Divisoes"] = SistranDAO.Cliente.RetornaDivisoesClientes(ILusuario[0].UsuarioId.ToString(), ILusuario[0].EmpresaId.ToString());
                Session["DivisoesCompleta"] = SistranDAO.Cliente.DivisoesCompleta(ILusuario[0].EmpresaId.ToString());
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "LOGOU NO SITE", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());


            }

            if (Session["USUARIO"] == null)
            {
                Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");

            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            //tr0.Height = "600";            

           if(tr0.Width != "200px")
               tr0.Style.Add("width", "200px");

            if (!IsPostBack)
            {
                if (Session["USUARIO"] == null)
                {
                    Response.Redirect("Erro.aspx");
                }

                tdDivivisoriaRetratil.Attributes.Add("onclick", "javascript:Ocultar('" + tr0.ClientID + "', '" + tdDivivisoriaRetratil.ClientID + "','s')");
                tdDivivisoriaRetratil.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
                tr0.Style.Add("width", "200px");
                //tr0.Style.Add("height", "800px");

                

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                int dia = DateTime.Now.Day;
                int ano = DateTime.Now.Year;
                string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                string data = diasemana + ", " + dia + " de " + mes + " de " + ano;
                lblDataHora.Text = data;


                if (Session["ProfileIndex"] != null && !string.IsNullOrEmpty(Session["ProfileIndex"].ToString()) && Convert.ToInt32(Session["ProfileIndex"].ToString()) >= 0)
                {
                    CarregaImagemPrincipal();

                    LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
                    SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
                    Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];
                    lblUserName.Text = "Usuário: " + Usuario.UsuarioNome;
                    this.Page.Title = Usuario.UsuarioNome;


                    CarregarCboCLiente();

                    if (LUser.Count == 1)
                    {
                        tb_troca.Visible = false;
                    }

                    if (Session["IDEmpresa"].ToString() != "")
                    {
                        foreach (var item in LUser)
                        {
                            if (item.EmpresaId.ToString() == Session["IDEmpresa"].ToString())
                            {
                                lblCodEmpresa.Text = item.EmpresaId.ToString();
                                Session["IDEmpresa"] = item.EmpresaId.ToString();                                
                                CarregraMenu2();
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblCodEmpresa.Text = Usuario.EmpresaId.ToString();
                        Session["IDEmpresa"] = Usuario.EmpresaId.ToString();
                    }
                    CarregaMenu();
                    this.Page.Title = Usuario.UsuarioNome;
                }
                else
                {
                    Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");
                    Response.End();

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('"+ ex.Message.Replace("'","´´" )+"')</script>");
           
        }
    }

    private void CarregraMenu2()
    {     
        TreeNode n = new TreeNode();
        LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
        DataTable dtMenu = SistranBLL.Menu.MontarMenu(LUser[0].UsuarioId);
        DataRow[] m = dtMenu.Select("PROGRAMA='HOME'");

        if (m.Length > 0)
        {
            n = new TreeNode("Home", "Home");
            n.NavigateUrl = m[0]["link"].ToString() + "?opc=" + "Home";
            Session["pgInicial"] = m[0]["link"].ToString() + "?opc=Home";
            TreeViewMenu.Nodes.Add(n);
        }
        else
            Session["pgInicial"] ="Vazio.aspx?opc=" + "Home";
        

        foreach (DataRow item in dtMenu.Rows)
        {
            if (item["Link"].ToString() != "")
            {
                bool jaExiste = false;
                for (int i = 0; i < TreeViewMenu.Nodes.Count; i++)
                {
                    if (item["Menu"].ToString() == TreeViewMenu.Nodes[i].Text)
                    {
                        jaExiste = true;
                    }
                }

                if (jaExiste == false)
                {
                    string menu = item["Menu"].ToString().Replace("Fatur amento", "Faturamento");

                    n = new TreeNode(menu, menu);
                    n.NavigateUrl = item["Link"].ToString() + "?opc=" + menu;
                    TreeViewMenu.Nodes.Add(n);
                }
            }
        }

        if (Session["ConnLogin"].ToString().ToLower().Contains("dicate") || Session["ConnLogin"].ToString().ToLower().Contains("sistranweb"))
        {
            n = new TreeNode("Manual");
            n.NavigateUrl = "help/Manual.pdf";
            n.Target = "_blank";
            TreeViewMenu.Nodes.Add(n);
        }

        Session["TreeViewMenu"] = TreeViewMenu;
    }

    private void CarregaImagemPrincipal()
    {
        string m =FuncoesGerais.LoadDataSetLogo(Session["ConexaoCliete"].ToString());

        if (m != "")
        {
            imgLogoPrincipal.Src = null;
            imgLogoPrincipal.Src = "LogoCliente/" + m;
        }
        else
        {
            imgLogoPrincipal.Src = null;
            imgLogoPrincipal.Src = "Imagens/LOGOS-LOGTRANSP-03.jpg";
        }
    }  

    
    protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
    {       
       
        LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        foreach (var item in LUser)
        {
            if (item.EmpresaId.ToString() == lblCodEmpresa.Text)
            {               
                Session["IDEmpresa"] = lblCodEmpresa.Text;
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    #endregion

    #region Methods
    private static List<SistranMODEL.Usuario> LUser;

    protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
    {        
        List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
        Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];
        lblUserName.Text = Usuario.UsuarioNome;
    }

    private void CarregaMenu()
    {

        SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
        Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];

        List<SistranMODEL.Menu> menuPai = new List<SistranMODEL.Menu>();
        menuPai = SistranBLL.Menu.GetMenuParent(Usuario.UsuarioId, Session["Conn"].ToString());
        //nodes pais
        foreach (var item in menuPai)
        {
            TreeNode newNode = new TreeNode(item.Titulo.ToString(), item.Id.ToString());
            if (item.Titulo == "Home")
            {
                newNode.NavigateUrl = "Inicial.aspx";
            }
            TreeViewMenu.Nodes.Add(newNode);

            //adiciona os nos filhos
            //AddChildNodes(newNode, item.Id);

            if (newNode.ChildNodes.Count == 0 && item.Titulo !="Home")
            {
                TreeViewMenu.Nodes.Remove(newNode);
            }
        }
    }

    //private void AddChildNodes(TreeNode parentNode, int parentId)
    //{
    //    SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
    //    Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];

    //    List<SistranMODEL.menuChildren> menuChildren = new List<SistranMODEL.menuChildren>();
    //    menuChildren = SistranBLL.menuChildren.GetMenuChildren(Usuario.UsuarioId, parentId, Session["Conn"].ToString());

    //    foreach (var childNodeId in menuChildren)
    //    {
    //        TreeNode childNode = new TreeNode(childNodeId.Titulo.ToString(), childNodeId.Id.ToString());
    //        childNode.NavigateUrl = childNodeId.Link +"?opc="+ Server.UrlEncode(childNodeId.Titulo.ToString());
    //        //add to parent        

    //        if (childNodeId.Link != "" || menuChildren.Count > 0)
    //            parentNode.ChildNodes.Add(childNode);

    //        //call same function recursively
    //        if (menuChildren.Count > 0)
    //        {
    //            if (childNodeId.Link != "" || menuChildren.Count > 0)
    //                AddChildNodes(childNode, childNodeId.Id);
    //        }
    //    }
    //}
    #endregion


    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Fim.aspx");
    }
    protected void lnkLogout_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Fim.aspx");
    }
}
