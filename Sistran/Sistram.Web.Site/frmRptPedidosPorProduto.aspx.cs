using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
/*//using Microsoft.Reporting.WebForms;*/
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class frmRptPedidosPorProduto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               // Session["operacao"] = null;
               
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
                throw new Exception("Informe um filtro.");
            }

            DataTable dt = new SistranBLL.Produto().ListarProdutoIniciais(txtCodigo.Text, txtDescricao.Text, Session["IDEmpresa"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    lblIdProdutoCliente.Text = dt.Rows[0]["IDProdutoCliente"].ToString();
                    dvPesquisa.Visible = false;
                    txtDescricao.Text = dt.Rows[0]["Descricao"].ToString();
                    txtCodigo.Text = dt.Rows[0]["Codigo"].ToString();
                    CarregarRepeater();
                    btnImprimir.Visible = true;
                    btnImprimir.Attributes.Add("OnClick", "javascript:window.open('rptPedidoProduto.aspx?codigo=" + txtCodigo.Text + "')");
        
                }
                else
                {
                    dvPesquisa.Visible = true;
                    RadGrid16.DataSource = dt;
                    RadGrid16.DataBind();
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
            CarregarRepeater();

            if (rptGrid.Items.Count > 0)
            {
                btnImprimir.Visible = true;
                btnImprimir.Attributes.Add("OnClick", "javascript:window.open('rptPedidoProduto.aspx?codigo=" + txtCodigo.Text + "')");
            }
                          else
            {
                btnImprimir.Visible = false;
            }
        }
    }

    private void CarregarRepeater()
    {
        try
        {
            rptGrid.DataSource = new Produto().ListarRptPedidos(txtCodigo.Text);
            rptGrid.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
        
    }

    //DataTable dtItens;
    //private void Button_ClickDivisao(object sender, System.EventArgs e)
    //{
    //    try
    //    {

    //        if (lblIdProdutoCliente.Text == "0")
    //        {
    //            txtCodigo.Focus();
    //            throw new Exception("Selecione um produto.");
    //        }

    //        rptGrid.Visible = true;

    //        Button b = (Button)sender;
    //        if (Session["operacao"] == null)
    //        {
    //            dtItens = new DataTable("tblOperacao");
    //            dtItens.Columns.Add("Codigo");
    //            dtItens.Columns.Add("Descricao");
    //            dtItens.Columns.Add("Entrada");
    //            dtItens.Columns.Add("Divisao");
    //            dtItens.Columns.Add("IdClienteDivisao");
    //            dtItens.Columns.Add("IdProdutoCliente");
    //        }
    //        else
    //        {
    //            dtItens = (DataTable)Session["operacao"];
    //        }

    //        for (int i = 0; i < dtItens.Rows.Count; i++)
    //        {
    //            if (dtItens.Rows[i]["IdClienteDivisao"].ToString() == b.ID.ToString() && dtItens.Rows[i]["IdProdutoCliente"].ToString() == lblIdProdutoCliente.Text)
    //            {
    //                throw new Exception("Item já selecionado.");
    //            }
    //        }

    //        DataRow d = dtItens.NewRow();
    //        d["Codigo"] = txtCodigo.Text;
    //        d["Descricao"] = txtDescricao.Text;
    //        d["Entrada"] = "0";
    //        d["Divisao"] = b.Text;
    //        d["IdClienteDivisao"] = b.ID;
    //        d["IdProdutoCliente"] = lblIdProdutoCliente.Text;
    //        dtItens.Rows.Add(d);

    //        rptGrid.DataSource = dtItens;
    //        rptGrid.DataBind();

    //        Session["operacao"] = dtItens;
    //        btnConfirmar.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
    //    }
    //}

    //protected void txtrptEntrada_Changed(object sender, EventArgs e)
    //{
    //    TextBox txt = (TextBox)sender;
    //    dtItens = (DataTable)Session["operacao"];

    //    for (int i = 0; i < dtItens.Rows.Count; i++)
    //    {
    //        if (dtItens.Rows[i]["IDCLIENTEDIVISAO"].ToString() + "||" + dtItens.Rows[i]["IdProdutoCliente"].ToString() == txt.Attributes["chave"].ToString())
    //        {
    //            dtItens.Rows[i]["Entrada"] = txt.Text;
    //        }
    //    }

    //    Session["operacao"] = dtItens;
    //}

 
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        lblIdProdutoCliente.Text = "0";
        txtDescricao.Text = "";
        txtCodigo.Text = "";
        txtCodigo.Focus();
    }
    protected void rptGrid_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //Label lblIdProdutoCliente = (Label)e.Item.FindControl("lblrptIdProdutoCliente");
        //Label lblrptIdClienteDivisao = (Label)e.Item.FindControl("lblrptIdClienteDivisao");

        //dtItens = (DataTable)Session["operacao"];

        //if (e.CommandArgument.ToString() == "Apagar")
        //{
        //    for (int i = 0; i < dtItens.Rows.Count; i++)
        //    {
        //        if (dtItens.Rows[i]["IdClienteDivisao"].ToString() == lblrptIdClienteDivisao.Text && dtItens.Rows[i]["IdProdutoCliente"].ToString() == lblIdProdutoCliente.Text)
        //        {
        //            dtItens.Rows.RemoveAt(i);
        //            Session["operacao"] = dtItens;
        //            rptGrid.DataSource = dtItens;
        //            rptGrid.DataBind();
        //            return;
        //        }
        //    }
        //}
    }
    protected void rptGrid_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //TextBox txtrptEntrada = (TextBox)e.Item.FindControl("txtrptEntrada");
        //Label lblIdProdutoCliente = (Label)e.Item.FindControl("lblrptIdProdutoCliente");
        //Label lblrptIdClienteDivisao = (Label)e.Item.FindControl("lblrptIdClienteDivisao");

        //if (txtrptEntrada != null)
        //{
        //    txtrptEntrada.Attributes.Add("chave", lblrptIdClienteDivisao.Text + "||" + lblIdProdutoCliente.Text);
        //    txtrptEntrada.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
        //    txtrptEntrada.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");
        //}
    }

    protected void btnFecharDiv_Click(object sender, EventArgs e)
    {
        lblIdProdutoCliente.Text = "0";
        txtDescricao.Text = "";
        txtCodigo.Text = "";
        txtCodigo.Focus();
        dvPesquisa.Visible = false;
    }

    
    //protected void btnConfirmar_Click(object sender, EventArgs e)
    //{
    //    SistranBLL.Produto.Estoque o = new SistranBLL.Produto.Estoque();

    //    try
    //    {
    //        if (rptGrid.Items.Count > 0)
    //        {
    //            for (int i = 0; i < rptGrid.Items.Count; i++)
    //            {
    //                TextBox txtrptEntrada = (TextBox)rptGrid.Items[i].FindControl("txtrptEntrada");
                    
    //                if (txtrptEntrada.Text != "0")
    //                {
    //                    Label lblrptIdProdutoCliente = (Label)rptGrid.Items[i].FindControl("lblrptIdProdutoCliente");
    //                    Label lblrptIdClienteDivisao = (Label)rptGrid.Items[i].FindControl("lblrptIdClienteDivisao");

    //                    string idEstoqueDestino = new SistranBLL.Produto.Estoque().ConsultarEstoqueByIDProdutoCliente(Convert.ToInt32(lblrptIdProdutoCliente.Text)).ToString();

    //                    if (idEstoqueDestino == "0")
    //                    {
    //                        idEstoqueDestino = new SistranBLL.Produto.Estoque().InserirEstoque(lblrptIdProdutoCliente.Text, "1", txtrptEntrada.Text).ToString();
    //                    }
    //                    else
    //                    {
    //                        o.AdicionarSaldoTabelaEstoque(idEstoqueDestino, txtrptEntrada.Text);
    //                    }

    //                    string IdEstoqueDivisaoDestino = new SistranBLL.Produto.Estoque().ConsultarEstoqueDivisao(Convert.ToInt32(idEstoqueDestino), Convert.ToInt32(lblrptIdClienteDivisao.Text)).ToString();

    //                    if (IdEstoqueDivisaoDestino == "0")
    //                    {
    //                        IdEstoqueDivisaoDestino = new SistranBLL.Produto.Estoque().InserirEstoqueDivisao(Convert.ToInt32(idEstoqueDestino), Convert.ToInt32(lblrptIdClienteDivisao.Text), Convert.ToInt32(txtrptEntrada.Text)).ToString();
    //                    }
    //                    else
    //                    {
    //                        o.AtualizarEstoqueDivisao(Convert.ToInt32(IdEstoqueDivisaoDestino), Convert.ToInt32(txtrptEntrada.Text), "+");
    //                    }
    //                    List<SistranMODEL.Usuario> luser = (List<SistranMODEL.Usuario>)Session["USUARIO"];
    //                    o.InserirMovimentacaoEntradaInicial(IdEstoqueDivisaoDestino, luser[0].UsuarioId.ToString(), txtrptEntrada.Text);                      

    //                }
    //            }
    //            Session["operacao"] = null;
    //            txtCodigo.Text = "";
    //            txtDescricao.Text = "";
    //            dtItens.Rows.Clear();
    //            rptGrid.DataSource = dtItens;
    //            rptGrid.DataBind();
    //            btnConfirmar.Visible = false;
    //            txtCodigo.Focus();
    //            rptGrid.Visible = false;
    //            lblIdProdutoCliente.Text = "0";
    //            throw new Exception("Operação Efetuada com Sucesso.");
    //        }
    //        else
    //        {
    //            throw new Exception("Não existem itens configurados para acerto de estoque.");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
    //    }
    //}
    protected void btnImprimir_Click(object sender, EventArgs e)
    {

    }
}