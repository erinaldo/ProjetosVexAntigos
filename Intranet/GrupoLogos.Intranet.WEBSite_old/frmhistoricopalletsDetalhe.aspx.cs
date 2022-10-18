using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using ChartDirector;

public partial class frmhistoricopalletsDetalhe : System.Web.UI.Page
{
    #region Events

    public int intervalo;
    string clientesSelecionados = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ChartDirector.WebChartViewer.OnPageInit(Page);

        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            CarregarDados();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");
        }
    }

    #endregion

    protected void CarregarDados()
    {
        clientesSelecionados = Sistran.Library.FuncoesUteis.retornarClientesResumoFilial(true);

        string strsql = "";
        strsql += " SELECT   PC.IDPRODUTOCLIENTE, PC.CODIGO,  PC.DESCRICAO,  DPL.CODIGO ENDERECO,  MVC.IDUNIDADEDEARMAZENAGEM UA,   LT.IDLOTE,  MVC.TIPO,  MVC.SALDO,   CDCLI.RAZAOSOCIALNOME,  PC.MARCA,";
        strsql += "  ISNULL(MVC.VALORUNITARIO,0) VALORUNITARIO,   ";
        strsql += " (SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA,0) * ISNULL(P.COMPRIMENTO,0)     ";
        strsql += " FROM PRODUTOEMBALAGEM PE    ";
        strsql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO   ";
        strsql += " WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ";
        strsql += " ORDER BY PE.UNIDADEDOCLIENTE) M3, ";

        strsql += "  (SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA,0) * ISNULL(P.COMPRIMENTO,0)     ";
        strsql += " FROM PRODUTOEMBALAGEM PE    ";
        strsql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO   ";
        strsql += " WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ";
        strsql += " ORDER BY PE.UNIDADEDOCLIENTE) * UAL.SALDO M3TOTAL,   ";
        strsql += " VALOREMESTOQUE,  ";
        strsql += " (SELECT TOP 1 P.PESOBRUTO  ";
        strsql += " FROM PRODUTOEMBALAGEM PE    ";
        strsql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  ";
        strsql += " WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE  ";
        strsql += " ORDER BY PE.UNIDADEDOCLIENTE DESC) PESOUNIT, ";
        strsql += " (SELECT TOP 1 P.PESOBRUTO  ";
        strsql += " FROM PRODUTOEMBALAGEM PE    ";
        strsql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO   ";
        strsql += " WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE  ";
        strsql += " ORDER BY PE.UNIDADEDOCLIENTE DESC) * UAL.SALDO PESOTOTAL ";
        strsql += " FROM MOVIMENTACAOCLIENTE MVC    ";
        strsql += " INNER JOIN CADASTRO CDCLI ON CDCLI.IDCADASTRO = MVC.IDCLIENTE   ";
        strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = MVC.IDPRODUTOCLIENTE   ";
        strsql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = MVC.IDDEPOSITOPLANTALOCALIZACAO   ";
        strsql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDUNIDADEDEARMAZENAGEMLOTE = MVC.IDUNIDADEDEARMAZENAGEMLOTE   ";
        strsql += " INNER JOIN LOTE LT ON LT.IDLOTE = UAL.IDLOTE    ";
        strsql += " WHERE IDCDA IS NULL AND CONVERT(DATETIME, DATA, 103) = CONVERT(DATETIME, '" + Request.QueryString["data"] + "', 103)  ";
        strsql += " AND PC.IDCLIENTE= "+ Request.QueryString["idcliente"];
        strsql += " ORDER BY TIPO, PC.IDPRODUTOCLIENTE, MVC.IDUNIDADEDEARMAZENAGEM ";

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];
        RadGridArmazenagem.DataSource = dt;
        RadGridArmazenagem.DataBind();



        lblTitulo.Text = "Detalhe Historico Pallet | Cliente: " + dt.Rows[0]["RazaoSocialNome"].ToString();
        Session["dt"] = dt;
    }
}