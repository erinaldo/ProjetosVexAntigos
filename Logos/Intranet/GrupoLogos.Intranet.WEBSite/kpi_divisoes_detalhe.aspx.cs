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
using System.IO;
using System.Web.UI.HtmlControls;

public partial class kpi_divisoes_detalhe : System.Web.UI.Page
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

        string[] inifim = Session["DataConf"].ToString().Split('|');

        string strsql = "";
        strsql += " SELECT    ";
        strsql += " CAST(DAY(MCD.DATA) AS NVARCHAR(2)) + '/' + CAST(MONTH(MCD.DATA) AS NVARCHAR(2)) + '/' + CAST(YEAR(MCD.DATA) AS NVARCHAR(4)) DATA,    ";
        strsql += " CD.IDCLIENTEDIVISAO,   ";
        strsql += " CD.NOME PASTA,    ";
        strsql += " GP.DESCRICAO GRUPO ,";
        strsql += " PC.IDPRODUTOCLIENTE, ";
        strsql += " PC.CODIGO,   ";
        strsql += " PC.DESCRICAO,   ";
        strsql += " PC.MARCA, ";       
        strsql += " ISNULL((SELECT TOP 1 ISNULL(VALORUNITARIO,0) FROM PRODUTOEMBALAGEM PEI WHERE PEI.IDPRODUTOCLIENTE=PC.IDPRODUTOCLIENTE ORDER BY VALORUNITARIO DESC),0) VLUNITARIO,";
        strsql += "  CAST(SUM(MCD.SALDO) AS INT) SALDO, ";
        strsql += " (SELECT TOP 1 P.PESOBRUTO FROM PRODUTOEMBALAGEM PE   INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE  ORDER BY PE.UNIDADEDOCLIENTE DESC) PESO, ";
        strsql += " (SELECT TOP 1 P.PESOBRUTO  FROM PRODUTOEMBALAGEM PE   INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE  ORDER BY PE.UNIDADEDOCLIENTE DESC) * SUM(MCD.SALDO) PESOTOTAL, ";
        strsql += " (SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA,0) * ISNULL(P.COMPRIMENTO,0) FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE) [M3 UNIT.], ";
        strsql += " (SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA,0) * ISNULL(P.COMPRIMENTO,0) FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE) * SUM(MCD.SALDO) M3TOTAL ,";
        strsql += " MCD.IdFilial, F.NOME [FILIAL] ";
        strsql += " FROM   ";
        strsql += " MOVIMENTACAOCLIENTEDIVISAO MCD ";
        strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = MCD.IDPRODUTOCLIENTE ";
        strsql += " INNER JOIN CLIENTEDIVISAO CD ON CD.IDCLIENTEDIVISAO = MCD.IDCLIENTEDIVISAO ";
        strsql += " LEFT JOIN GRUPODEPRODUTO GP ON GP.IDGRUPODEPRODUTO = PC.IDGRUPODEPRODUTO ";
        strsql += " LEFT join FILIAL F ON F.IDFILIAL = MCD.IDFILIAL ";
        strsql += " WHERE PC.IDCLIENTE = " + Request.QueryString["idcliente"];
        strsql += " AND MCD.DATA BETWEEN '" + DateTime.Parse(inifim[0]).ToString("yyyy-MM-dd") + "' AND '" + DateTime.Parse(inifim[1]).ToString("yyyy-MM-dd") + "' ";
        strsql += " GROUP BY MCD.DATA, CD.IDCLIENTEDIVISAO, CD.NOME , GP.DESCRICAO,  PC.IDPRODUTOCLIENTE, PC.CODIGO , PC.DESCRICAO , PC.MARCA, MCD.IdFilial, F.NOME";
        strsql += " ORDER BY MCD.DATA, CD.NOME , PC.DESCRICAO , PC.MARCA";

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];        
        lblTitulo.Text = "Detalhe Historico Pallet | Cliente: " + Request.QueryString["nome"];
        dt.Columns.Add("[VALOR TOTAL]");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Response.Write(dt.Rows[i]["IDPRODUTOCLIENTE"].ToString() + "<br> - " + i.ToString()+ "<br>");

            dt.Rows[i]["[VALOR TOTAL]"] = (float.Parse(dt.Rows[i]["SALDO"].ToString()) * float.Parse(dt.Rows[i]["VLUNITARIO"].ToString())).ToString("#0,00");
        }

        ExportToExcel(dt, Request.QueryString["idcliente"]);

    }   

    public static void ExportToExcel(DataTable dt, string fileName)
    {
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName  + ".xls"));
        //HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.Write(GenerateTable(data));
        //HttpContext.Current.Response.End();


        string attachment = "attachment; filename="+fileName+".xls";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        foreach (DataColumn dc in dt.Columns)
        {
            HttpContext.Current.Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        HttpContext.Current.Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write(tab + dr[i].ToString());
                tab = "\t";
            }
            HttpContext.Current.Response.Write("\n");
        }
        HttpContext.Current.Response.End();
    }

    private static string GenerateTable(DataTable source)
    {

        HtmlTable table = new HtmlTable();
        HtmlTableRow headerRow = new HtmlTableRow();

        for (int x = 0; x < source.Columns.Count; x++)
        {
            HtmlTableCell th = new HtmlTableCell("th");
            th.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#337490");
            th.Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
            th.InnerText = source.Columns[x].ColumnName;
            headerRow.Cells.Add(th);
        }
        table.Rows.Add(headerRow);

        foreach (DataRow x in source.Rows)
        {
            HtmlTableRow tableRow = new HtmlTableRow();

            for (int y = 0; y < source.Columns.Count; y++)
            {
                System.Type rowType;
                rowType = x[y].GetType();
                HtmlTableCell td = new HtmlTableCell();

                switch (rowType.ToString())
                {
                    case "System.String":
                        string XMLstring = x[y].ToString();
                        XMLstring = XMLstring.Trim();
                        XMLstring = XMLstring.Replace("&", "&");
                        XMLstring = XMLstring.Replace(">", ">");
                        XMLstring = XMLstring.Replace("<", "<");
                        td.InnerText = XMLstring;
                        break;

                    case "System.DateTime":
                        td.InnerText = ((DateTime)x[y]).ToString("dd/MM/yyyy");
                        break;

                    case "System.Boolean":
                        td.InnerText = x[y].ToString();
                        break;

                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Byte":
                        td.InnerText = x[y].ToString();
                        break;
                    case "System.Decimal":
                    case "System.Double":
                        //td.InnerText = string.Format("{0:n}", x[y]);
                        td.InnerText = float.Parse(x[y].ToString()).ToString("#0.0000000");

                        break;
                    case "System.DBNull":
                        td.InnerText = string.Empty;
                        break;
                }
                tableRow.Cells.Add(td);
            }
            table.Rows.Add(tableRow);
        }

        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        table.RenderControl(htw);
        return sw.ToString();
    }
}