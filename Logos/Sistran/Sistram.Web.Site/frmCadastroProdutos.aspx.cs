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

public partial class frmCadastroProdutos : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadGrid1.Visible = false;

            if (Request.QueryString["opc"] != null)
                lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

            btnNovo.Attributes.Add("onClick", "window.open('frmCadastrarProduto.aspx?id=novo&opc=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");
           

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        carregarGrid();

        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU: " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
    }

    private void carregarGrid()
    {
        RadGrid1.PageSize = Convert.ToInt16(20);
        DataTable dt = new SistranBLL.Produto().ListarTelaCadastro(txtCodigo.Text, txtCodigo0.Text, txtDescricao.Text);
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
        dt.Columns.Add("numero");
        //Session["GRID"] = dt;
        if (dt.Rows.Count > 0)
            RadGrid1.Visible = true;
    }

    protected void txtNf_TextChanged(object sender, EventArgs e)
    {
        carregarGrid();
    }

    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        carregarGrid();
    }

    protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        carregarGrid();
    }

    protected void cboTipoDes0_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        
    }
}