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

public partial class frmRptPedidos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {             
               
                if (Request.QueryString["opc"] != null)
                    lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
                
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCodigo.Text.Trim().Length == 0 && txtDescricao.Text.Trim().Length == 0)
            {
                throw new Exception("Informe um produto.");
            }
            CarregarRepeater();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void RadGrid16_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandArgument.ToString().ToUpper() == "FECHAR")
        {
            LinkButton lnkCodigo = (LinkButton)e.Item.FindControl("lnkCodigo");
            LinkButton lnkDescricao = (LinkButton)e.Item.FindControl("lnkDescricao");
            dvPesquisa.Visible = false;
            lblIdProdutoCliente.Text = lnkDescricao.CommandName;
            txtDescricao.Text = lnkDescricao.Text;
            txtCodigo.Text = lnkCodigo.Text; 
            
        }
    }

    private void CarregarRepeater()
    {
        try
        {
            DateTime? i = null;
            DateTime? f = null;
            int? dte = null;
            

            if (txtI.Text != "")
                i = Convert.ToDateTime(txtI.Text);


            if (txtF.Text != "")
                f = Convert.ToDateTime(txtF.Text);

            if (hdIdDestinatario.Value != "0")
                dte = Convert.ToInt32(hdIdDestinatario.Value);

            GridResultado.Visible = true;
            GridResultado.DataSource = new Produto().ListarRptPedidosConsolidados(txtCodigo.Text, i, f, dte);
            GridResultado.DataBind();

            if (GridResultado.Items.Count > 0)
            {
                btnImprimir.Visible = true;
                btnImprimir.Attributes.Add("OnClick", "javascript:window.open('frmrptPedidoConsolidado.aspx?tit=" + lblTitulo.Text + "')");
                Session["dts"] = (DataTable)GridResultado.DataSource;
            }
            else
                btnImprimir.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
        
    }
 
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        lblIdProdutoCliente.Text = "0";
        txtDescricao.Text = "";
        txtCodigo.Text = "";
        txtI.Text = "";
        txtF.Text = "";
        txtDest.Text = "";
        GridResultado.DataSource = null;
        GridResultado.DataBind();
        txtCodigo.Focus();
        GridResultado.Visible = false;
    }

    protected void btnFecharDiv_Click(object sender, EventArgs e)
    {
        lblIdProdutoCliente.Text = "0";
        txtDescricao.Text = "";
        txtCodigo.Text = "";
        txtCodigo.Focus();
        dvPesquisa.Visible = false;
    }

    protected void btnFecharDiv2_Click(object sender, EventArgs e)
    {
        hdIdDestinatario.Value = "0";
        txtDest.Text = "";

        DivPesqDestinatario.Visible = false;
        RadGrid1.DataSource = null;
        RadGrid1.DataBind();
    }
    
    protected void txtCodigo_TextChanged(object sender, EventArgs e)
    {
        if (txtCodigo.Text.Length > 0)
        {
            MostrarGrid();
        }
    }

    protected void txtDescricao_TextChanged(object sender, EventArgs e)
    {
        if (txtDescricao.Text.Length > 0)
        {
            MostrarGrid();
        }
    }

    public void MostrarGrid()
    {
        try
        {

            GridResultado.DataSource = null;
            GridResultado.DataBind();
            GridResultado.Visible = false;

            DataTable dt = new SistranBLL.Produto().ListarProdutoIniciais(txtCodigo.Text, txtDescricao.Text, Session["IDEmpresa"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    lblIdProdutoCliente.Text = dt.Rows[0]["IDProdutoCliente"].ToString();
                    dvPesquisa.Visible = false;
                    txtDescricao.Text = dt.Rows[0]["Descricao"].ToString();
                    txtCodigo.Text = dt.Rows[0]["Codigo"].ToString();
         //           CarregarRepeater();
                    //btnImprimir.Visible = true;
                    //btnImprimir.Attributes.Add("OnClick", "javascript:window.open('rptPedidoProduto.aspx?codigo=" + txtCodigo.Text + "')");

                }
                else
                {
                    dvPesquisa.Visible = true;
                    RadGrid16.DataSource = dt;
                    RadGrid16.DataBind();
                    //GridResultado.Visible = true;
                }
            }
            else
            {
                dvPesquisa.Visible = false;
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Nenhum Produto Encontrado.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void txtDest_TextChanged(object sender, EventArgs e)
    {
        if (txtDest.Text.Length == 0)
        {
            txtDest.Focus();
            throw new Exception("Digite ao menos as iniciais.");
        }

        DataTable d = new SistranBLL.Destinatario().ListarDestinatario(txtDest.Text);

        if (d.Rows.Count > 0)
        {
            if (d.Rows.Count == 1)
            {
                txtDest.Text = d.Rows[0]["RAZAOSOCIALNOME"].ToString();
                hdIdDestinatario.Value = d.Rows[0]["IDCADASTRO"].ToString();
                //cboDestinatario.SelectedValue = d.Rows[0]["IDCADASTRO"].ToString();
                //btnConfirma.Visible = true;
            }
            else
            {
                DivPesqDestinatario.Visible = true;
                RadGrid1.DataSource = d;
                RadGrid1.DataBind();
            }
            //carregarDadosDestinatario();
        }
        else
        {
            txtDest.Text = "";
            txtDest.Focus();
            //cboDestinatario.SelectedIndex = 0;
            throw new Exception("Nenhum Destinatário Encontrado.");
        }
    }

    protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        LinkButton lnkCodigo = (LinkButton)e.Item.FindControl("lnkCodigo");
        Label lblNomes = (Label)e.Item.FindControl("lblNomes");
        if (lnkCodigo != null)
        {            
            txtDest.Text = lblNomes.Text.ToUpper();
            RadGrid1.DataSource = null;
            RadGrid1.DataBind();
            DivPesqDestinatario.Visible = false;
            hdIdDestinatario.Value = lnkCodigo.Text;   

        }
    }
}