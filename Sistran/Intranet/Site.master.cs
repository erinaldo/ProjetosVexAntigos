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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["USUARIO"] == null)
            {
                Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");

            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            tr0.Height = "600";

            if (!IsPostBack)
            {
                if (Session["USUARIO"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Redirect("frmLoginGrupoLogos.aspx");
                }

                KKKKKK2.Attributes.Add("onclick", "javascript:Ocultar('" + tr0.ClientID + "', '" + KKKKKK2.ClientID + "','s')");
                KKKKKK2.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");

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
                    List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
                    SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
                    Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];
                    lblUserName.Text = "Usuário: " + Usuario.UsuarioNome;
                    this.Page.Title = Usuario.UsuarioNome;
                    CarregraMenu2();

                    this.Page.Title = "Intranet";
                }
                else
                {
                    Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");
                    Response.End();
                }

                if (Session["ListClientes"] == null || Request.QueryString["trocar"] != null)
                {
                    MostrarListaClientes();
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.Replace("'", "´´") + "')</script>");

        }
    }

    private void CarregraMenu2()
    {
        TreeNode n = new TreeNode();
        n = new TreeNode("Resumo Por Filial", "Resumo Por Filial");
        n.NavigateUrl = "ResumoPorFilial.aspx" + "?opc=" + "Resumo Por Filial";
        TreeViewMenu.Nodes.Add(n);

        //List<SistranMODEL.Usuario>  LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
        //DataTable dtMenu = SistranBLL.Menu.MontarMenu(LUser[0].UsuarioId);

        //DataRow[] m = dtMenu.Select("PROGRAMA='HOME'");

        //if (m.Length > 0)
        //{
        //    n = new TreeNode("Home", "Home");
        //    n.NavigateUrl = m[0]["link"].ToString() + "?opc=" + "Home";
        //    Session["pgInicial"] = m[0]["link"].ToString() + "?opc=Home";
        //    TreeViewMenu.Nodes.Add(n);
        //}
        //else
        //{
        //    Session["pgInicial"] ="Vazio.aspx?opc=" + "Home";
        //}

        //foreach (DataRow item in dtMenu.Rows)
        //{
        //    if (item["Link"].ToString() != "")
        //    {
        //        bool jaExiste = false;
        //        for (int i = 0; i < TreeViewMenu.Nodes.Count; i++)
        //        {
        //            if (item["Menu"].ToString() == TreeViewMenu.Nodes[i].Text)
        //            {
        //                jaExiste = true;
        //            }
        //        }

        //        if (jaExiste == false)
        //        {
        //            n = new TreeNode(item["Menu"].ToString(), item["Menu"].ToString());
        //            n.NavigateUrl = item["Link"].ToString() + "?opc=" + item["Menu"].ToString();
        //            TreeViewMenu.Nodes.Add(n);
        //        }
        //    }
        //}

        //if (Session["ConnLogin"].ToString().ToLower().Contains("dicate") || Session["ConnLogin"].ToString().ToLower().Contains("sistranweb"))
        //{
        //    n = new TreeNode("Manual");
        //    n.NavigateUrl = "help/Manual.pdf";
        //    n.Target = "_blank";
        //    TreeViewMenu.Nodes.Add(n);
        //}

        Session["TreeViewMenu"] = TreeViewMenu;
    }

    protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
    {

        List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        foreach (var item in LUser)
        {
            if (item.EmpresaId.ToString() == lblCodEmpresa.Text)
            {
                Session["IDEmpresa"] = lblCodEmpresa.Text;
                Response.Redirect("default.aspx");
            }
        }
    }

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
            AddChildNodes(newNode, item.Id);

            if (newNode.ChildNodes.Count == 0 && item.Titulo != "Home")
            {
                TreeViewMenu.Nodes.Remove(newNode);
            }
        }
    }

    private void AddChildNodes(TreeNode parentNode, int parentId)
    {
        SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
        Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];

        List<SistranMODEL.menuChildren> menuChildren = new List<SistranMODEL.menuChildren>();
        menuChildren = SistranBLL.menuChildren.GetMenuChildren(Usuario.UsuarioId, parentId, Session["Conn"].ToString());

        foreach (var childNodeId in menuChildren)
        {
            TreeNode childNode = new TreeNode(childNodeId.Titulo.ToString(), childNodeId.Id.ToString());
            childNode.NavigateUrl = childNodeId.Link + "?opc=" + Server.UrlEncode(childNodeId.Titulo.ToString());
            //add to parent        

            if (childNodeId.Link != "" || menuChildren.Count > 0)
                parentNode.ChildNodes.Add(childNode);

            //call same function recursively
            if (menuChildren.Count > 0)
            {
                if (childNodeId.Link != "" || menuChildren.Count > 0)
                    AddChildNodes(childNode, childNodeId.Id);
            }
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("erro.aspx");
    }

    protected void lnkLogout_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Fim.aspx");
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (!this.cphMain.Page.Page.ToString().Contains("default"))
        {
            Response.Redirect("default.aspx?opc=INTRANET&trocar=sim");
        }
        else
        {
            MostrarListaClientes();
        }
    }

    private void MostrarListaClientes()
    {

        if (Session["ListClientes"] != null)
        {
            dvEscolheCliente.Visible = false;
            dvEscolheCliente.Visible = true;
            txtFiltro.Text = "";
            chkEscolhidos.Visible = true;


            ListBox lstTemp = (ListBox)Session["ListClientes"];
            rdListClientes.Items.Clear();

            string cliente = "";
            for (int i = 0; i < lstTemp.Items.Count; i++)
            {
                //chkEscolhidos.Items.Add(new ListItem(lstTemp.Items[i].Value, lstTemp.Items[i].Text));
                //chkEscolhidos.Items[i].Selected = true;

                cliente += lstTemp.Items[i].Value + ",";
            }

            if (cliente.Length > 2)
            {
                cliente = cliente.Substring(0, cliente.Length - 1);
            }

            DataTable dtRet = new SistranBLL.Cliente().RetornarClientesIntranet("", cliente);


            for (int i = 0; i < dtRet.Rows.Count; i++)
            {

                bool existe = false;
                for (int ii = 0; ii < chkEscolhidos.Items.Count; ii++)
                {
                    if (dtRet.Rows[i][0].ToString() == chkEscolhidos.Items[ii].Value)
                    {
                        existe = true;
                        break;
                    }
                }

                if (existe == false)
                {
                    string nome = dtRet.Rows[i]["CNPJCPF"].ToString() + " - " + dtRet.Rows[i]["RAZAOSOCIALNOME"].ToString().ToUpper();
                    chkEscolhidos.Items.Add(new ListItem(nome, dtRet.Rows[i][0].ToString()));
                    chkEscolhidos.Items[i].Selected = true;
                }
            }
        }
        else
        {
            dvEscolheCliente.Visible = false;
            dvEscolheCliente.Visible = true;
            txtFiltro.Text = "";
            chkEscolhidos.Visible = false;
            rdListClientes.Items.Clear();
            btnConfirmar.Visible = false;
            pnlClientes.Visible = false;

        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {


        rdListClientes.Items.Clear();
        btnConfirmar.Visible = false;


        if (txtFiltro.Text.Trim() == "")
        {
            return;
        }
        DataTable dtRet = new SistranBLL.Cliente().RetornarClientesIntranet(txtFiltro.Text, "");
        for (int i = 0; i < dtRet.Rows.Count; i++)
        {
            string nome = dtRet.Rows[i]["CNPJCPF"].ToString() + " - " + dtRet.Rows[i]["RAZAOSOCIALNOME"].ToString().ToUpper();

            if (nome.Length > 40)
                nome = nome.Substring(0, 40) + "...";


            rdListClientes.Items.Insert(i, new ListItem(nome, dtRet.Rows[i]["CODIGODOCLIENTE"].ToString()));
            pnlClientes.Visible = true;
        }
    }

    protected void rdListClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //for (int i = 0; i < rdListClientes.Items.Count; i++)
        //{
        //    if (rdListClientes.Items[i].Selected == true)
        //    {                
        //        carregarListClientesRelacionados(rdListClientes.SelectedValue);
        //    }
        //    CheckBoxList1.Visible = true;
        //}
    }

    private void carregarListClientesRelacionados(string p)
    {
        //CheckBoxList1.Visible = true;
        DataTable dtRet = new SistranBLL.Cliente().RetornarClientasRelacionados(p);

        //CheckBoxList1.Items.Clear();
        for (int i = 0; i < dtRet.Rows.Count; i++)
        {
            string nome = dtRet.Rows[i]["CNPJCPF"].ToString() + " - " + dtRet.Rows[i]["RAZAOSOCIALNOME"].ToString().ToUpper();
            if (nome.Length > 40)
                nome = nome.Substring(0, 40) + "...";


            bool existe = false;

            for (int ii = 0; ii < chkEscolhidos.Items.Count; ii++)
            {
                if (chkEscolhidos.Items[ii].Value == dtRet.Rows[i]["CODIGODOCLIENTE"].ToString())
                {
                    existe = true;
                    return;
                }
            }

            if (existe == false)
                chkEscolhidos.Items.Insert(i, new ListItem(nome, dtRet.Rows[i]["CODIGODOCLIENTE"].ToString()));


            chkEscolhidos.Items[i].Selected = true;
            btnConfirmar.Visible = true;
        }
        if (chkEscolhidos.Items.Count > 0)
        {
            chkEscolhidos.Visible = true;
        }
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        ListBox ListClientes = new ListBox();
        ListClientes.Items.Clear();

        for (int i = 0; i < chkEscolhidos.Items.Count; i++)
        {
            ListClientes.Items.Insert(i, chkEscolhidos.Items[i].Value);
        }
        Session["ListClientes"] = ListClientes;

        Response.Redirect("ResumoPorFilial.aspx?opc=RESUMO POR FILIAL - INTRANET");
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < rdListClientes.Items.Count; i++)
        {
            if (rdListClientes.Items[i].Selected == true)
            {
                carregarListClientesRelacionados(rdListClientes.Items[i].Value);
            }
        }
    }

    protected void chkEscolhidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkEscolhidos.Items.Count > 0)
        {
            for (int i = 0; i < chkEscolhidos.Items.Count; i++)
            {
                if (chkEscolhidos.Items[i].Selected == false)
                {
                    chkEscolhidos.Items.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
