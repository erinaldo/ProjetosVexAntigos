using System;
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
                //CarregarClienteFiltro();

                carregarEmpresasSelecionadas();
                if (Request.QueryString["trocar"] != null)
                {
                    MostrarListaClientes();
                    CarregarClienteFiltro();
                }


                CarregaImagemPrincipal();

                TreeViewMenu.ExpandAll();
            }
            tdTitCliente.Visible = false;
            grd.Visible = false;
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void carregarEmpresasSelecionadas()
    {
        if (Session["ListClientes"] != null)
        {
            ListBox lstTemp = (ListBox)Session["ListClientes"];
            string x = "";
            for (int i = 0; i < lstTemp.Items.Count; i++)
            {
                x += lstTemp.Items[i].Text + ",";
            }

            if (x.Length > 0)
                x = x.Substring(0, x.Length - 1);

            if (x == "")
            {
                x = "0";
            }

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("SELECT isnull(fantasiaapelido,RAZAOSOCIALNOME) NOME, CNPJCPF CNPJ FROM CADASTRO WHERE IDCADASTRO IN (" + x + ") ORDER BY 2");
            grd.DataSource = dt;
            grd.DataBind();

            if (x == "0")
                tdTitCliente.Visible = false;
            else
                tdTitCliente.Visible = false;
        }
        else
        {
            tdTitCliente.Visible = false;
            //CarregarClienteFiltro();
        }

    }

    private void CarregraMenu2()
    {
        int kpi = 0;

        TreeNode n = new TreeNode();

        List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
        DataTable dtMenu = SistranBLL.Menu.MontarMenu(LUser[0].UsuarioId);

        DataRow[] m = dtMenu.Select("PROGRAMA='HOME'");

        if (m.Length > 0)
        {
             Session["pgInicial"] = "Vazio.aspx?opc=" + "Home";
        }
        else
        {
            Session["pgInicial"] = "Vazio.aspx?opc=" + "Home";
        }

        DirectoryInfo dir = new DirectoryInfo(Server.MapPath(@"~"));
        FileInfo[] files =  dir.GetFiles();

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

                
                bool existeFile = false;

                for (int i = 0; i < files.Length; i++)
                {
                    if (item["Link"].ToString().ToLower() == files[i].Name.ToLower())
                    {
                        existeFile = true;
                    }
                }

                if (jaExiste == false && existeFile)
                {
                    if (item["Menu"].ToString().Contains("KPI"))
                    {
                        if (kpi == 0)
                        {
                            n = new TreeNode("KPI", "KPI");
                            n.NavigateUrl = "#";
                            TreeViewMenu.Nodes.Add(n);
                            kpi++;


                            DataRow[] d = dtMenu.Select("Menu like '%KPI -%'", "");

                            for (int i = 0; i < d.Length; i++)
                            {
                                TreeNode nf = new TreeNode(d[i]["Menu"].ToString(), d[i]["Menu"].ToString());
                                nf.NavigateUrl = d[i]["Link"].ToString() + "?opc=" + d[i]["Menu"].ToString();
                                n.ChildNodes.Add(nf);
                            }                        
                            
                        }                    
                    }
                    else
                    {

                        n = new TreeNode(item["Menu"].ToString(), item["Menu"].ToString());
                        n.NavigateUrl = item["Link"].ToString() + "?opc=" + item["Menu"].ToString();
                        TreeViewMenu.Nodes.Add(n);
                    }
                }
            }
        }

        n = new TreeNode("Liberação de Pedidos", "Liberação de Pedidos");
        n.NavigateUrl = "http://www.grupologos.com.br/webteste/frmControleLiberacaoPedido.aspx" + "?opc=Liberação de Pedidos";
        n.Target = "_blank";
        TreeViewMenu.Nodes.Add(n);

   


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

      //  List<SistranMODEL.menuChildren> menuChildren = new List<SistranMODEL.menuChildren>();
     //   menuChildren = SistranBLL.menuChildren.GetMenuChildren(Usuario.UsuarioId, parentId, Session["Conn"].ToString());

        //foreach (var childNodeId in menuChildren)
        //{
        //    TreeNode childNode = new TreeNode(childNodeId.Titulo.ToString(), childNodeId.Id.ToString());
        //    childNode.NavigateUrl = childNodeId.Link + "?opc=" + Server.UrlEncode(childNodeId.Titulo.ToString());
        //    //add to parent        

        //    if (childNodeId.Link != "" || menuChildren.Count > 0)
        //        parentNode.ChildNodes.Add(childNode);

        //    //call same function recursively
        //    if (menuChildren.Count > 0)
        //    {
        //        if (childNodeId.Link != "" || menuChildren.Count > 0)
        //            AddChildNodes(childNode, childNodeId.Id);
        //    }
        //}
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
            CarregarClienteFiltro();

            if (chkEscolhidos.Items.Count > 0)
                btnConfirmar.Visible = true;
        }
    }

    private void MostrarListaClientes()
    {
        this.TreeViewMenu.Enabled = false;

        if (Session["ListClientes"] != null)
        {
            dvEscolheCliente.Visible = false;
            dvEscolheCliente.Visible = true;
            txtFiltro.Text = "";
            txtFiltro.Focus();
            chkEscolhidos.Visible = true;


            ListBox lstTemp = (ListBox)Session["ListClientes"];
            rdListClientes.Items.Clear();
            chkEscolhidos.Items.Clear();

            string cliente = "";
            for (int i = 0; i < lstTemp.Items.Count; i++)
            {
                chkEscolhidos.Items.Add(new ListItem(lstTemp.Items[i].Value, lstTemp.Items[i].Text));
                chkEscolhidos.Items[i].Selected = true;

                cliente += lstTemp.Items[i].Text + ",";
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
            txtFiltro.Focus();
            chkEscolhidos.Visible = false;
            rdListClientes.Items.Clear();
            btnConfirmar.Visible = false;
            pnlClientes.Visible = false;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        CarregarClienteFiltro();
    }

    private void CarregarClienteFiltro()
    {
        rdListClientes.Items.Clear();
        btnConfirmar.Visible = false;
        DataTable dtRet = new SistranBLL.Cliente().RetornarClientesIntranet(txtFiltro.Text, "");
        for (int i = 0; i < dtRet.Rows.Count; i++)
        {
            string nome = dtRet.Rows[i]["CNPJCPF"].ToString() + " - " + dtRet.Rows[i]["RAZAOSOCIALNOME"].ToString().ToUpper() + "-" + dtRet.Rows[i]["cidade"].ToString();
            rdListClientes.Items.Insert(i, new ListItem(nome, dtRet.Rows[i]["IDCLIENTE"].ToString() ));
            
        }
        pnlClientes.Visible = true;
    }

    protected void rdListClientes_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void carregarListClientesRelacionados(string p)
    {
        //CheckBoxList1.Visible = true;
        DataTable dtRet = new SistranBLL.Cliente().RetornarClientasRelacionados(p);

        //CheckBoxList1.Items.Clear();
        for (int i = 0; i < dtRet.Rows.Count; i++)
        {
            string nome = dtRet.Rows[i]["CNPJCPF"].ToString() + " - " + dtRet.Rows[i]["RAZAOSOCIALNOME"].ToString().ToUpper();
            //if (nome.Length > 35)
            //    nome = nome.Substring(0, 35) + "...";

            bool existe = false;

            for (int ii = 0; ii < chkEscolhidos.Items.Count; ii++)
            {
                if (chkEscolhidos.Items[ii].Value == dtRet.Rows[i]["IDCLIENTE"].ToString())
                {
                    existe = true;
                    return;
                }
            }

            if (existe == false)
                chkEscolhidos.Items.Insert(i, new ListItem(nome, dtRet.Rows[i]["IDCLIENTE"].ToString()));


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
        Session["ListClientes"] = "";
        

        for (int i = 0; i < chkEscolhidos.Items.Count; i++)
        {
            if (chkEscolhidos.Items[i].Selected == true)
                ListClientes.Items.Add(new ListItem(chkEscolhidos.Items[i].Value,chkEscolhidos.Items[i].Text ));
        }
        Session["ListClientes"] = ListClientes;
        this.TreeViewMenu.Enabled = true;
        dvEscolheCliente.Visible = false;

        Response.Redirect("default.aspx?opc=INTRANET");
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

    protected void rdListClientes_SelectedIndexChanged1(object sender, EventArgs e)
    {
        for (int i = 0; i < rdListClientes.Items.Count; i++)
        {
            if (rdListClientes.Items[i].Selected == true)
            {
                carregarListClientesRelacionados(rdListClientes.Items[i].Value);
            }
        }
    }
    
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (!this.cphMain.Page.Page.ToString().Contains("default"))
        {
            Response.Redirect("default.aspx?opc=INTRANET&trocar=sim");
        }
        else
        {
            MostrarListaClientes();
            CarregarClienteFiltro();
        }
    }

    private void CarregaImagemPrincipal()
    {
        string m = FuncoesGerais.LoadDataSetLogo(Session["ConexaoCliete"].ToString());

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
    protected void btnFecharEscolheClie_Click(object sender, EventArgs e)
    {
       this.dvEscolheCliente.Visible = false;
       this.TreeViewMenu.Enabled = true;
       this.Panel3.Enabled = true;
        
    }
}