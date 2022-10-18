using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
////using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class frmListKPI_IRWIN_V2 : System.Web.UI.Page
{
    public int intervalo;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
                CarregarCboLinha();

               
                txtMesAno.Text = ("0" + DateTime.Now.Month.ToString()).Replace("00","0") + "/" + DateTime.Now.Year.ToString();

                if(txtMesAno.Text.Contains("010/"))
                    txtMesAno.Text = txtMesAno.Text.Replace("010/", "10/");

                if (txtMesAno.Text.Contains("011/"))
                    txtMesAno.Text = txtMesAno.Text.Replace("011/", "11/");

                if (txtMesAno.Text.Contains("012/"))
                    txtMesAno.Text = txtMesAno.Text.Replace("012/", "12/");



                divDetalhes.Visible = false;
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void CarregarCboLinha()
    {
        cboLinha = new SistranBLL.Produto.GrupoProduto().ListarGrupoDeProdutos(cboLinha);
    }

    protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        CarregarGrid();
    }

    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        CarregarGrid();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CarregarGrid();
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU.", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
    }

    private void CarregarGrid()
    {
        //try
        //{
            if (cboLinha.SelectedIndex == 0)
            {
                cboLinha.Focus();
                throw new Exception("Selecione a Linha");
            }

            if (txtMesAno.Text.Trim() != "")
            {
                if (txtMesAno.Text.Trim().Length != 7)
                {
                    txtMesAno.Focus();
                    throw new Exception("Informe o Ano / Mês no formato: MM/AAAA");
                }


                if (Convert.ToInt32(txtMesAno.Text.Trim().Substring(0, 2)) < 0 || Convert.ToInt32(txtMesAno.Text.Trim().Substring(0, 2)) > 12)
                {
                    txtMesAno.Focus();
                    txtMesAno.Text = "";
                    throw new Exception("Informe o Ano / Mês no formato: MM/AAAA");
                }
            }
            else
            {
                txtMesAno.Focus();
                throw new Exception("Informe o Ano / Mês no formato: DD/AAAA");
            }
                       
            grdResultado.Enabled = true;
            
            DataTable dtemp = new SistranBLL.KPI_Irwin().GerarPlanilha(txtMesAno.Text, cboLinha.SelectedValue, "V2");        

            grdResultado.DataSource = dtemp;
            grdResultado.DataBind();
            Session["mesano"] = txtMesAno.Text;
            Session["linha"] = cboLinha.SelectedValue;

        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        //}

    }
       
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        grdResultado.Enabled = true;
        TreeView TreeViewMenu = (TreeView)Master.FindControl("TreeViewMenu");
        TreeViewMenu.Enabled = true;
        Button1.Enabled = true;
        divDetalhes.Visible = false;
        txtValor.Text = "";
        lblCodigoMov.Text = "";
    }

    protected void grdResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            DataRowView d = (DataRowView)e.Row.DataItem;

            if (d.Row["chave"].ToString() == "10.3" || d.Row["chave"].ToString() == "10.4" || d.Row["chave"].ToString() == "10.5" || d.Row["chave"].ToString() == "6.13" || d.Row["chave"].ToString() == "6.14" || d.Row["chave"].ToString() == "5.5.1" || d.Row["chave"].ToString() == "6.13" || d.Row["chave"].ToString() == "4.15" || d.Row["chave"].ToString() == "4.5" || d.Row["chave"].ToString() == "6.4" || d.Row["chave"].ToString() == "6.7" || d.Row["chave"].ToString() == "6.6" || d.Row["chave"].ToString() == "1.7" || d.Row["chave"].ToString() == "9.2.1" || d.Row["chave"].ToString() == "5.5" || d.Row["chave"].ToString() == "6.15" || d.Row["chave"].ToString() == "6.14" || d.Row["chave"].ToString() == "6.8.1" || d.Row["chave"].ToString() == "6.8" || d.Row["chave"].ToString() == "6.16" || d.Row["chave"].ToString() == "8.12" || d.Row["chave"].ToString() == "9.2" || d.Row["chave"].ToString().Contains("7.") || d.Row["chave"].ToString() == "11.1" || d.Row["chave"].ToString() == "7.1" || d.Row["chave"].ToString() == "7.2" || d.Row["chave"].ToString() == "7.3")
            {
                e.Row.CssClass = "linhaManual";
            }

        }
    }

    protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        grdResultado.Enabled = false;
        TreeView TreeViewMenu = (TreeView)Master.FindControl("TreeViewMenu");
        TreeViewMenu.Enabled = false;
        Button1.Enabled = false;
        divDetalhes.Visible = true;
        txtValor.Text = e.CommandArgument.ToString().Replace("-", "").Replace("%","");
        txtValor.Focus();
        lblCodigoMov.Text = e.CommandName.ToString();
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        string m = "UPDATE KPIIRWINMOV_V2 SET VALOR=" + (txtValor.Text != "" ? Convert.ToDecimal(txtValor.Text).ToString().Replace(",", ".") : "NULL") + "  WHERE IdKPIIRWINMOV=" + lblCodigoMov.Text;
        Sistran.Library.GetDataTables.ExecutarSemRetorno(m, "");
        TreeView TreeViewMenu = (TreeView)Master.FindControl("TreeViewMenu");
        TreeViewMenu.Enabled = true;
        Button1.Enabled = true;
        divDetalhes.Visible = false;
        txtValor.Text = "";
        lblCodigoMov.Text = "";
        CarregarGrid();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (grdResultado.Rows.Count + 1 < 65536)
        {
            grdResultado.AllowPaging = false;
            grdResultado.DataBind();

            StringWriter tw = new StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            HtmlForm frm = new HtmlForm();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=kpi_irwin.xls");
            Response.Charset = "";
            EnableViewState = false;

            Controls.Add(frm);
            frm.Controls.Add(grdResultado);
            frm.RenderControl(hw);

            Response.Write(tw.ToString());
            Response.End();

            grdResultado.AllowPaging = true;
            grdResultado.DataBind();
        }
    }
}