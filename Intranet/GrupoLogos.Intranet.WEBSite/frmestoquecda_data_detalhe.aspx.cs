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

public partial class frmestoquecda_data_detalhe : System.Web.UI.Page
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

        string strsql = "exec RETORNARPOSICAOCDADATA " + Request.QueryString["idcliente"] +", '"+ DateTime.Parse(inifim[0]).ToString("yyyy-MM-dd") +"'" ;
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];        
        lblTitulo.Text = "cda | Cliente: " + Request.QueryString["nome"];
        ExportToExcel(dt, "cda_data"+ DateTime.Now.ToString("yyyyMMdd"));

    }
    public static void ExportToExcel(DataTable data, string fileName)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName + Guid.NewGuid() + ".xls"));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(GenerateTable(data));
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
                        td.InnerText = string.Format("{0:n}", x[y]);
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