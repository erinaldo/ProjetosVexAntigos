using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
//using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;

public partial class ConsultarEstoquePorDivisao : System.Web.UI.Page
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if(Request.QueryString["opc"] !=null)
                lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

            CarregarMenuDivisao();

            if (!IsPostBack)
            {
                RadGrid1.Visible = false;
                HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
                HtmlTableCell tdDivivisoriaRetratil = (HtmlTableCell)Master.FindControl("tdDivivisoriaRetratil");

                
                tr0.Style.Add("display", "none");
                lblMensagem.Text = "Selecione uma divisão.";
                tdDivivisoriaRetratil.Height = "600px";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    private void CarregarMenuDivisao()
    {
        List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        SistranBLL.Cliente.Divisao pdBLL = new SistranBLL.Cliente.Divisao();
        DataTable dtPai = pdBLL.RetornarPais(LUser[0].EmpresaId);

        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<table class='tableMenu' cellspacing=0 celpanding=0 widht=200px>"));

        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr >"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td align='left' nowrap=nowrap style='font-size:9pt; '>POR DIVISÃO"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));

        foreach (DataRow item in dtPai.Rows)
        {
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left' >"));
            PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(item["NOME"].ToString(), true, item["IDClienteDivisao"].ToString()));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));

            DataTable dtFilho = pdBLL.RetornarFlihos(LUser[0].EmpresaId, Convert.ToInt32(item["IDClienteDivisao"]));

            foreach (DataRow itemFilho in dtFilho.Rows)
            {
                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left' >&nbsp;&nbsp;"));
                PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemFilho["NOME"].ToString(), false, itemFilho["IDClienteDivisao"].ToString()));
                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));

                DataTable dtFilhoFilho = pdBLL.RetornarFlihos(LUser[0].EmpresaId, Convert.ToInt32(itemFilho["IDClienteDivisao"]));

                foreach (DataRow itemff in dtFilhoFilho.Rows)
                {
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;"));
                    PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemff["NOME"].ToString(), false, itemff["IDClienteDivisao"].ToString()));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));
                }

            }
        }
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</table>"));
    }

    private Button criarBotaoDivisao(string p, bool bold, string Codigo)
    {
        Button bot = new Button();
        bot.Text = p;
        bot.BorderStyle = BorderStyle.None;       
        bot.Font.Name = "Arial";
        //bot.Font.Size = 7;
        bot.Style.Add("font-size", "7pt");
        bot.Font.Bold = bold;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Click += new EventHandler(Button_ClickDivisao);
        bot.ID = Codigo;
        bot.BackColor = System.Drawing.Color.White;
        
        return bot;
    }

    private void Button_ClickDivisao(object sender, System.EventArgs e)
    {
        Button b = (Button)sender;
        List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
        DataTable dt = new SistranBLL.Produto().ListarProdutoByDivisaoCliente(Convert.ToInt32(b.ID), LUser[0].EmpresaId,true );
        RadGrid1.PageSize = Convert.ToInt32(cboTipoDes0.SelectedValue);
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
        Session["click"] = b.ID;

        lblMensagem.Text = "VOCÊ FILTROU POR: " + b.Text.ToUpper();
        if (dt.Rows.Count > 0)
        {
            RadGrid1.Visible = true;
            Session["dt_click"] = dt;
        }
    }

    protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        CarregarPesquisa();
    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarPesquisa();
    }

    protected void CarregarPesquisa()
    {
        DataTable dt = new SistranBLL.Produto().ListarProdutoByDivisaoCliente(Convert.ToInt32(Session["click"]), Convert.ToInt32(Session["IDEmpresa"]), true);
        RadGrid1.PageSize = Convert.ToInt32(cboTipoDes0.SelectedValue); ;
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();

        if (dt.Rows.Count > 0)
            RadGrid1.Visible = true;        
    }

    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        CarregarPesquisa();
    }

    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
     
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        CarregarPesquisaBotao(); 
    }

    private void CarregarPesquisaBotao()
    {        
        DataTable dt = new SistranBLL.Produto().ListarProdutoByCodigos(txtCodigo.Text, txtCodigo0.Text, Session["IDEmpresa"].ToString(), true);
        RadGrid1.PageSize = Convert.ToInt32(cboTipoDes0.SelectedValue); ;
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();

        if (dt.Rows.Count > 0)
        {
            RadGrid1.Visible = true;
            Session["dt_click"] = dt;
        }
    }

    protected void cboTipoDes0_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RadGrid1_PageSizeChanged(object source, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
    {
       // CarregarPesquisa();
    }
}