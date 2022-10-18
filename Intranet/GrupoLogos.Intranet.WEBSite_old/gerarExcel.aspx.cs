using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Reflection;

using System.IO;
using SistranBLL;
public partial class KPI_gerarExcel : System.Web.UI.Page
{

    public static void ExportToExcel(DataTable data, string fileName)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName+Guid.NewGuid() + ".xls"));
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
                        DateTime XMLDate = (DateTime)x[y];
                        string XMLDatetoString = ""; //Excel Converted Date
                        XMLDatetoString = XMLDate.Year.ToString() +
                             "-" +
                             (XMLDate.Month < 10 ? "0" +
                             XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                             "-" +
                             (XMLDate.Day < 10 ? "0" +
                             XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                             "T" +
                             (XMLDate.Hour < 10 ? "0" +
                             XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                             ":" +
                             (XMLDate.Minute < 10 ? "0" +
                             XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                             ":" +
                             (XMLDate.Second < 10 ? "0" +
                             XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                             ".000";
                        td.InnerText = XMLDatetoString;
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
                        td.InnerText = string.Format("{0:n4}", x[y]);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        ExportToExcel((DataTable)Session["dt"], "Notas");     
    }
}


