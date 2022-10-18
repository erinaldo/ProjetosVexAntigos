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
        /* strsql += " SELECT   PC.IDPRODUTOCLIENTE, PC.CODIGO,  PC.DESCRICAO,  DPL.CODIGO ENDERECO,  MVC.IDUNIDADEDEARMAZENAGEM UA,   LT.IDLOTE,  MVC.TIPO,  MVC.SALDO,   CDCLI.RAZAOSOCIALNOME,  PC.MARCA,";
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
         strsql += " ORDER BY PE.UNIDADEDOCLIENTE DESC) * UAL.SALDO PESOTOTAL, ";
         strsql += " (SELECT NOME FROM FILIAL WHERE IDFILIAL = 15 ) FILIAL  ";
         strsql += " FROM MOVIMENTACAOCLIENTE MVC    ";
         strsql += " INNER JOIN CADASTRO CDCLI ON CDCLI.IDCADASTRO = MVC.IDCLIENTE   ";
         strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = MVC.IDPRODUTOCLIENTE   ";
         strsql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = MVC.IDDEPOSITOPLANTALOCALIZACAO   ";
         strsql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDUNIDADEDEARMAZENAGEMLOTE = MVC.IDUNIDADEDEARMAZENAGEMLOTE   ";
         strsql += " INNER JOIN LOTE LT ON LT.IDLOTE = UAL.IDLOTE    ";
         strsql += " WHERE IDCDA IS NULL AND CONVERT(DATETIME, DATA, 103) = CONVERT(DATETIME, '" + Request.QueryString["data"] + "', 103)  ";
         strsql += " AND PC.IDCLIENTE= "+ Request.QueryString["idcliente"];
         strsql += " ORDER BY TIPO, PC.IDPRODUTOCLIENTE, MVC.IDUNIDADEDEARMAZENAGEM ";
         */

		/*
        strsql += " SELECT   PC.IDPRODUTOCLIENTE, PC.CODIGO,  PC.DESCRICAO,  DPL.CODIGO ENDERECO, cd.Nome Divisao, gp.Descricao grupo,  MVC.IDUNIDADEDEARMAZENAGEM UA,   LT.IDLOTE,  MVC.TIPO,  MVC.SALDO,   CDCLI.RAZAOSOCIALNOME,   ";
        strsql += " PC.MARCA,  ISNULL(MVC.VALORUNITARIO, 0) VALORUNITARIO,     ";
        strsql += " (SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA, 0) * ISNULL(P.COMPRIMENTO, 0) ";
        strsql += " FROM PRODUTOEMBALAGEM PE INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
        strsql += " WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE    ORDER BY PE.UNIDADEDOCLIENTE) M3,    ";
        strsql += " (SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA, 0) * ISNULL(P.COMPRIMENTO, 0) ";
        strsql += " FROM PRODUTOEMBALAGEM PE INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
        strsql += "  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE    ORDER BY PE.UNIDADEDOCLIENTE) *UAL.SALDO M3TOTAL,     ";
        strsql += " MVC.VALOREMESTOQUE,    ";
        strsql += " (SELECT TOP 1 P.PESOBRUTO FROM PRODUTOEMBALAGEM PE     INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
        strsql += " WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE DESC) PESOUNIT,   ";
        strsql += " (SELECT TOP 1 P.PESOBRUTO FROM PRODUTOEMBALAGEM PE ";
        strsql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO    WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
        strsql += " ORDER BY PE.UNIDADEDOCLIENTE DESC) *UAL.SALDO PESOTOTAL, ";
        strsql += " F.NOME FILIAL, '" + Request.QueryString["data"] + "' Data ";

        strsql += " FROM MOVIMENTACAOCLIENTE MVC ";
        strsql += " INNER JOIN CADASTRO CDCLI ON CDCLI.IDCADASTRO = MVC.IDCLIENTE ";
        strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = MVC.IDPRODUTOCLIENTE ";
        strsql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = MVC.IDDEPOSITOPLANTALOCALIZACAO ";
        strsql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDUNIDADEDEARMAZENAGEMLOTE = MVC.IDUNIDADEDEARMAZENAGEMLOTE ";
        strsql += " INNER JOIN LOTE LT ON LT.IDLOTE = UAL.IDLOTE ";
        strsql += " left join Estoque e on e.IDEstoque = lt.IDEstoque ";
        strsql += " left join EstoqueDivisao ed on ed.IDEstoque = e.IDEstoque ";
        strsql += " left join ClienteDivisao cd on cd.IDClienteDivisao = ed.IDClienteDivisao ";
        strsql += " left join GrupoDeProduto gp on gp.IDGrupoDeProduto = pc.IDGrupoDeProduto ";
        strsql += " left join Filial f on f.IDFilial = e.IDFilial ";
        strsql += " WHERE IDCDA IS NULL AND CONVERT(DATETIME, DATA, 103) = CONVERT(DATETIME, '" + Request.QueryString["data"] + "', 103)  ";
        strsql += " AND PC.IDCLIENTE = " + Request.QueryString["idcliente"];
        strsql += " and ed.Saldo >0";

        strsql += " ORDER BY TIPO, PC.IDPRODUTOCLIENTE, MVC.IDUNIDADEDEARMAZENAGEM, cd.Nome ";
		*/


		strsql = "  SELECT distinct  PC.IDPRODUTOCLIENTE, PC.CODIGO,  PC.DESCRICAO, DPL.Codigo Endereco, '' Divisao, gp.Descricao grupo,  MVC.IDUNIDADEDEARMAZENAGEM UA,   LT.IDLOTE,  MVC.TIPO,  UAL.SALDO,   CDCLI.RAZAOSOCIALNOME,    PC.MARCA,  ISNULL(MVC.VALORUNITARIO, 0) VALORUNITARIO,       ";
		strsql += " (SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA, 0) * ISNULL(P.COMPRIMENTO, 0)  FROM PRODUTOEMBALAGEM PE INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE    ORDER BY PE.UNIDADEDOCLIENTE) M3,      ";
		strsql += " (SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA, 0) * ISNULL(P.COMPRIMENTO, 0)  FROM PRODUTOEMBALAGEM PE INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO   WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE    ORDER BY PE.UNIDADEDOCLIENTE) *UAL.SALDO M3TOTAL,       ";
		strsql += " ISNULL(MVC.VALORUNITARIO, 0) * ual.saldo VALOREMESTOQUE,      ";
		strsql += " (SELECT TOP 1 P.PESOBRUTO FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE DESC) PESOUNIT,     ";
		strsql += " (SELECT TOP 1 P.PESOBRUTO FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO    WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE  ORDER BY PE.UNIDADEDOCLIENTE DESC) *UAL.SALDO PESOTOTAL,  F.NOME FILIAL, '"+Request.QueryString["data"] +"' Data ";
		strsql += " FROM MOVIMENTACAOCLIENTE MVC  with(nolock) ";
		strsql += " INNER JOIN CADASTRO CDCLI  with(nolock)  ON CDCLI.IDCADASTRO = MVC.IDCLIENTE ";
		strsql += " INNER JOIN PRODUTOCLIENTE PC  with(nolock)  ON PC.IDPRODUTOCLIENTE = MVC.IDPRODUTOCLIENTE ";
		strsql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL  with(nolock)  ON DPL.IDDEPOSITOPLANTALOCALIZACAO = MVC.IDDEPOSITOPLANTALOCALIZACAO ";
		strsql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL  with(nolock)  ON UAL.IDUNIDADEDEARMAZENAGEMLOTE = MVC.IDUNIDADEDEARMAZENAGEMLOTE ";
		strsql += " INNER JOIN LOTE LT  with(nolock)  ON LT.IDLOTE = UAL.IDLOTE  left join Estoque e on e.IDEstoque = lt.IDEstoque ";

		strsql += " left join GrupoDeProduto gp on gp.IDGrupoDeProduto = pc.IDGrupoDeProduto  left join Filial f on f.IDFilial = e.IDFilial ";
		strsql += " WHERE IDCDA IS NULL AND CONVERT(DATETIME, DATA, 103) = CONVERT(DATETIME, '"+Request.QueryString["data"]+"', 103) ";
		strsql += " AND PC.IDCLIENTE = "+ Request.QueryString["idcliente"]+" and UAL.Saldo > 0 ";
		strsql += " ORDER BY TIPO, PC.IDPRODUTOCLIENTE, MVC.IDUNIDADEDEARMAZENAGEM ";



		DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];
        RadGridArmazenagem.DataSource = dt;
        RadGridArmazenagem.DataBind();



        lblTitulo.Text = "Detalhe Historico Pallet | Cliente: " + dt.Rows[0]["RazaoSocialNome"].ToString();
        Session["dt"] = dt;
    }
}