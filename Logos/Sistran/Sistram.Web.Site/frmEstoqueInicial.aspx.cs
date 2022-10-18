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
//using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class frmEstoqueInicial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CarregarMenuDivisao();

            if (!IsPostBack)
            {
                Session["operacao"] = null;
                pnlMenu.Visible = false;
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
                    CarregarMenuDivisao();
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
            CarregarMenuDivisao();
        }
    }

    DataTable dtItens;
    private void Button_ClickDivisao(object sender, System.EventArgs e)
    {
        try
        {

            if (lblIdProdutoCliente.Text == "0")
            {
                txtCodigo.Focus();
                throw new Exception("Selecione um produto.");
            }

            rptGrid.Visible = true;

            Button b = (Button)sender;
            if (Session["operacao"] == null)
            {
                dtItens = new DataTable("tblOperacao");
                dtItens.Columns.Add("Codigo");
                dtItens.Columns.Add("Descricao");
                dtItens.Columns.Add("Entrada");
                dtItens.Columns.Add("Divisao");
                dtItens.Columns.Add("IdClienteDivisao");
                dtItens.Columns.Add("IdProdutoCliente");
            }
            else
            {
                dtItens = (DataTable)Session["operacao"];
            }

            for (int i = 0; i < dtItens.Rows.Count; i++)
            {
                if (dtItens.Rows[i]["IdClienteDivisao"].ToString() == b.ID.ToString() && dtItens.Rows[i]["IdProdutoCliente"].ToString() == lblIdProdutoCliente.Text)
                {
                    throw new Exception("Item já selecionado.");
                }
            }

            DataRow d = dtItens.NewRow();
            d["Codigo"] = txtCodigo.Text;
            d["Descricao"] = txtDescricao.Text;
            d["Entrada"] = "0";
            d["Divisao"] = b.Text;
            d["IdClienteDivisao"] = b.ID;
            d["IdProdutoCliente"] = lblIdProdutoCliente.Text;
            dtItens.Rows.Add(d);

            rptGrid.DataSource = dtItens;
            rptGrid.DataBind();

            Session["operacao"] = dtItens;
            btnConfirmar.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void txtrptEntrada_Changed(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        dtItens = (DataTable)Session["operacao"];

        for (int i = 0; i < dtItens.Rows.Count; i++)
        {
            if (dtItens.Rows[i]["IDCLIENTEDIVISAO"].ToString() + "||" + dtItens.Rows[i]["IdProdutoCliente"].ToString() == txt.Attributes["chave"].ToString())
            {
                dtItens.Rows[i]["Entrada"] = txt.Text;
            }
        }

        Session["operacao"] = dtItens;
    }

    #region Divisao
    private void CarregarMenuDivisao()
    {

        pnlMenu.Visible = true;
        PlaceHolderMenuDivisao.Controls.Clear();
        //List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        SistranBLL.Cliente.Divisao pdBLL = new SistranBLL.Cliente.Divisao();
        DataTable dtPai = pdBLL.RetornarPais(Convert.ToInt32(Session["IDEmpresa"]));

        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<table  cellspacing=0 celpanding=0 widht=200px bgcolor='#EBEBEB' >"));

        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr >"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td align='left' nowrap=nowrap style='font-size:10px; border-bottom: 1px solid #dddddd'><b>Selecione a Divisão</b>"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));

        int c = 0;
        foreach (DataRow item in dtPai.Rows)
        {
            if (c == 0)
                Session["PrimeiraDivisao"] = item["IDClienteDivisao"].ToString() + "|" + item["NOME"].ToString();

            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>"));
            PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(item["NOME"].ToString(), false, item["IDClienteDivisao"].ToString()));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));

            DataTable dtFilho = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(item["IDClienteDivisao"]));

            foreach (DataRow itemFilho in dtFilho.Rows)
            {

                if (ProcurarBotaoMenuDivisao(itemFilho["IDClienteDivisao"].ToString()) == false)
                {
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;"));
                    PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemFilho["NOME"].ToString(), false, itemFilho["IDClienteDivisao"].ToString()));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));
                }

                DataTable dtFilhoFilho = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemFilho["IDClienteDivisao"]));

                foreach (DataRow itemff in dtFilhoFilho.Rows)
                {
                    if (ProcurarBotaoMenuDivisao(itemff["IDClienteDivisao"].ToString()) == false)
                    {

                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;"));
                        PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemff["NOME"].ToString(), false, itemff["IDClienteDivisao"].ToString()));
                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));


                        DataTable dtFilhoNivel3 = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemff["IDClienteDivisao"]));
                        foreach (DataRow itemNivel3 in dtFilhoNivel3.Rows)
                        {
                            //nivel 3
                            if (ProcurarBotaoMenuDivisao(itemNivel3["IDClienteDivisao"].ToString()) == false)
                            {

                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                                PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemNivel3["NOME"].ToString(), false, itemNivel3["IDClienteDivisao"].ToString()));
                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));


                                DataTable dtFilhoNivel4 = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemNivel3["IDClienteDivisao"]));
                                foreach (DataRow itemNivel4 in dtFilhoNivel4.Rows)
                                {
                                    //nivel 4
                                    if (ProcurarBotaoMenuDivisao(itemNivel4["IDClienteDivisao"].ToString()) == false)
                                    {

                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                                        PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemNivel4["NOME"].ToString(), false, itemNivel4["IDClienteDivisao"].ToString()));
                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));


                                        DataTable dtFilhoNivel5 = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemNivel4["IDClienteDivisao"]));
                                        foreach (DataRow itemNivel5 in dtFilhoNivel5.Rows)
                                        {
                                            //nivel 4
                                            if (ProcurarBotaoMenuDivisao(itemNivel5["IDClienteDivisao"].ToString()) == false)
                                            {

                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                                                PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemNivel5["NOME"].ToString(), false, itemNivel5["IDClienteDivisao"].ToString()));
                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));
                                            }

                                        }

                                    }

                                }

                            }
                        }
                    }
                }
            }
        }
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</table>"));

    }

    private bool ProcurarBotaoMenuDivisao(string id)
    {
        foreach (Control cc in PlaceHolderMenuDivisao.Controls)
        {
            Button tb = cc as Button;

            if (tb != null)
            {
                if (tb.ID == id)
                {
                    return true;
                }

            }
        }
        return false;
    }

    private Button criarBotaoDivisao(string p, bool bold, string IdClienteDivisao)
    {
        Button bot = new Button();
        bot.Text = p;
        bot.BorderStyle = BorderStyle.None;
        bot.Font.Name = "Verdana";
        bot.Style.Add("font-size", "7pt");
        bot.Font.Bold = bold;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Click += new EventHandler(Button_ClickDivisao);
        bot.ID = IdClienteDivisao;
        bot.BackColor = System.Drawing.Color.Transparent;
        return bot;
    }

    #endregion
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        lblIdProdutoCliente.Text = "0";
        txtDescricao.Text = "";
        txtCodigo.Text = "";
        txtCodigo.Focus();
    }
    protected void rptGrid_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label lblIdProdutoCliente = (Label)e.Item.FindControl("lblrptIdProdutoCliente");
        Label lblrptIdClienteDivisao = (Label)e.Item.FindControl("lblrptIdClienteDivisao");

        dtItens = (DataTable)Session["operacao"];

        if (e.CommandArgument.ToString() == "Apagar")
        {
            for (int i = 0; i < dtItens.Rows.Count; i++)
            {
                if (dtItens.Rows[i]["IdClienteDivisao"].ToString() == lblrptIdClienteDivisao.Text && dtItens.Rows[i]["IdProdutoCliente"].ToString() == lblIdProdutoCliente.Text)
                {
                    dtItens.Rows.RemoveAt(i);
                    Session["operacao"] = dtItens;
                    rptGrid.DataSource = dtItens;
                    rptGrid.DataBind();
                    return;
                }
            }
        }
    }
    protected void rptGrid_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        TextBox txtrptEntrada = (TextBox)e.Item.FindControl("txtrptEntrada");
        Label lblIdProdutoCliente = (Label)e.Item.FindControl("lblrptIdProdutoCliente");
        Label lblrptIdClienteDivisao = (Label)e.Item.FindControl("lblrptIdClienteDivisao");

        if (txtrptEntrada != null)
        {
            txtrptEntrada.Attributes.Add("chave", lblrptIdClienteDivisao.Text + "||" + lblIdProdutoCliente.Text);
            txtrptEntrada.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
            txtrptEntrada.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");
        }
    }

    protected void btnFecharDiv_Click(object sender, EventArgs e)
    {
        lblIdProdutoCliente.Text = "0";
        txtDescricao.Text = "";
        txtCodigo.Text = "";
        txtCodigo.Focus();
        dvPesquisa.Visible = false;
    }


    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        SistranBLL.Produto.Estoque o = new SistranBLL.Produto.Estoque();

        try
        {
            if (rptGrid.Items.Count > 0)
            {
                for (int i = 0; i < rptGrid.Items.Count; i++)
                {
                    TextBox txtrptEntrada = (TextBox)rptGrid.Items[i].FindControl("txtrptEntrada");

                    if (txtrptEntrada.Text != "0")
                    {
                        Label lblrptIdProdutoCliente = (Label)rptGrid.Items[i].FindControl("lblrptIdProdutoCliente");
                        Label lblrptIdClienteDivisao = (Label)rptGrid.Items[i].FindControl("lblrptIdClienteDivisao");

                        string idEstoqueDestino = new SistranBLL.Produto.Estoque().ConsultarEstoqueByIDProdutoCliente(Convert.ToInt32(lblrptIdProdutoCliente.Text)).ToString();

                        if (idEstoqueDestino == "0")
                        {
                            idEstoqueDestino = new SistranBLL.Produto.Estoque().InserirEstoque(lblrptIdProdutoCliente.Text, "1", txtrptEntrada.Text).ToString();
                        }
                        else
                        {
                            o.AdicionarSaldoTabelaEstoque(idEstoqueDestino, txtrptEntrada.Text);
                        }

                        string IdEstoqueDivisaoDestino = new SistranBLL.Produto.Estoque().ConsultarEstoqueDivisao(Convert.ToInt32(idEstoqueDestino), Convert.ToInt32(lblrptIdClienteDivisao.Text)).ToString();

                        if (IdEstoqueDivisaoDestino == "0")
                        {
                            IdEstoqueDivisaoDestino = new SistranBLL.Produto.Estoque().InserirEstoqueDivisao(Convert.ToInt32(idEstoqueDestino), Convert.ToInt32(lblrptIdClienteDivisao.Text), Convert.ToInt32(txtrptEntrada.Text)).ToString();
                        }
                        else
                        {
                            o.AtualizarEstoqueDivisao(Convert.ToInt32(IdEstoqueDivisaoDestino), Convert.ToInt32(txtrptEntrada.Text), "+");
                        }
                        List<SistranMODEL.Usuario> luser = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                        o.InserirMovimentacaoEntradaInicial(IdEstoqueDivisaoDestino, luser[0].UsuarioId.ToString(), txtrptEntrada.Text);

                    }
                }
                Session["operacao"] = null;
                txtCodigo.Text = "";
                txtDescricao.Text = "";
                dtItens.Rows.Clear();
                rptGrid.DataSource = dtItens;
                rptGrid.DataBind();
                btnConfirmar.Visible = false;
                txtCodigo.Focus();
                rptGrid.Visible = false;
                lblIdProdutoCliente.Text = "0";
                throw new Exception("Operação Efetuada com Sucesso.");
            }
            else
            {
                throw new Exception("Não existem itens configurados para acerto de estoque.");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SistranBLL.Produto.Estoque o = new SistranBLL.Produto.Estoque();

        DataTable dt = Sistran.Library.GetDataTables.PlanilhaEstoque();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable dIdprodCliente = new SistranBLL.Produto().ConsultarProdutoClienteCodigo(dt.Rows[i]["Codigo"].ToString());

            if (dIdprodCliente.Rows.Count > 0)
            {
                string idEstoqueDestino = new SistranBLL.Produto.Estoque().ConsultarEstoqueByIDProdutoCliente(Convert.ToInt32(dIdprodCliente.Rows[0]["IDPRODUTOCLIENTE"].ToString())).ToString();

                if (idEstoqueDestino == "0")
                {
                    idEstoqueDestino = new SistranBLL.Produto.Estoque().InserirEstoque(dIdprodCliente.Rows[0]["IDPRODUTOCLIENTE"].ToString(), "1", "0").ToString();
                }

                for (int ic = 0; ic < dt.Columns.Count; ic++)
                {
                    int qtd = 0;
                    int idClienteDivisao = 0;
                    if (dt.Columns[ic].ToString() == "TRADE")
                    {
                        qtd = Convert.ToInt32(dt.Rows[i][ic]);
                        idClienteDivisao = 37;
                    }

                    if (dt.Columns[ic].ToString() == "NORDESTE")
                    {
                        qtd = Convert.ToInt32(dt.Rows[i][ic]);
                        idClienteDivisao = 47;
                    }

                    if (dt.Columns[ic].ToString().Trim().Replace(" ", "") == "MG/NO")
                    {
                        qtd = Convert.ToInt32(dt.Rows[i][ic]);
                        idClienteDivisao = 25;
                    }

                    if (dt.Columns[ic].ToString().Trim().Replace(" ", "") == "RIODEJANEIRO")
                    {
                        qtd = Convert.ToInt32(dt.Rows[i][ic]);
                        idClienteDivisao = 19;
                    }

                    if (dt.Columns[ic].ToString().Trim().Replace(" ", "") == "SPCAPITAL")
                    {
                        qtd = Convert.ToInt32(dt.Rows[i][ic]);
                        idClienteDivisao = 1;
                    }

                    if (dt.Columns[ic].ToString().Trim().Replace(" ", "") == "SPINTERIOR")
                    {
                        qtd = Convert.ToInt32(dt.Rows[i][ic]);
                        idClienteDivisao = 7;
                    }

                    if (dt.Columns[ic].ToString().Trim().Replace(" ", "") == "SUL")
                    {
                        qtd = Convert.ToInt32(dt.Rows[i][ic]);
                        idClienteDivisao = 13;
                    }

                    if (dt.Columns[ic].ToString().Trim().Replace(" ", "") == "KA")
                    {
                        qtd = Convert.ToInt32(dt.Rows[i][ic]);
                        idClienteDivisao = 31;
                    }


                    if (qtd > 0 && idClienteDivisao > 0)
                    {
                        o.AdicionarSaldoTabelaEstoque(idEstoqueDestino, qtd.ToString());

                        try
                        {
                            string X = "SELECT IDCLIENTEDIVISAO FROM ClienteDivisao WHERE IDParente=" + idClienteDivisao + " AND Nome LIKE '%" + dt.Rows[i]["DIVISAO"].ToString() + "%'";
                            idClienteDivisao = int.Parse(Sistran.Library.GetDataTables.ExecutarRetornoIDs(X, "").ToString());

                            string IdEstoqueDivisaoDestino = new SistranBLL.Produto.Estoque().ConsultarEstoqueDivisao(Convert.ToInt32(idEstoqueDestino), Convert.ToInt32(idClienteDivisao)).ToString();

                            if (IdEstoqueDivisaoDestino == "0")
                            {
                                IdEstoqueDivisaoDestino = new SistranBLL.Produto.Estoque().InserirEstoqueDivisao(Convert.ToInt32(idEstoqueDestino), Convert.ToInt32(idClienteDivisao), qtd).ToString();
                            }
                            else
                            {
                                o.AtualizarEstoqueDivisao(Convert.ToInt32(IdEstoqueDivisaoDestino), qtd, "+");
                            }
                        }
                        catch (Exception)
                        {

                        }

                    }


                }
                string s = "update PlanilhaEstoque set Processado='SIM' where codigo='" + dt.Rows[i]["Codigo"].ToString() + "'";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(s, "");
            }
        }
    }
}