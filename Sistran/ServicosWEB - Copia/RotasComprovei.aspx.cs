using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Data;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace ServicosWEB
{
    public partial class RotasComprovei : System.Web.UI.Page
    {
        string cnx = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();


            if (!IsPostBack)
            {
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("SELECT F.IDFilial, NomeComprovei FROM FilialEnviaRotasComprovei FE INNER JOIN FILIAL F ON  FE.IDfILIAL = F.IDFILIAL ORDER BY 2", cnx);

                DropDownList1.DataSource = dt;
                DropDownList1.DataTextField = "NomeComprovei";
                DropDownList1.DataValueField = "IdFilial";
                DropDownList1.DataBind();

                DropDownList1.Items.Insert(0, new ListItem("Todas"));
                pesquisar();
            }
        }

        private void pesquisar()
        {

            string sqlEnviados = "";
            sqlEnviados = "SELECT distinct  top 50 DT.IDDT, DT.NUMERO, R.IDROMANEIO ROMANEIO, MOT.RAZAOSOCIALNOME MOTORISTA , mot.CNPJcpf [CPF Motorista], case when DT.DATADESAIDA  is null then DATEADD(mi, -1,H.DATAHORA) else 	case when H.DATAHORA<DT.DATADESAIDA   then DATEADD(mi, -1,H.DATAHORA) else DT.DATADESAIDA end  end LIBERACAOPORTARIA, DT.Emissao [EMISSAO DT],  H.DATAHORA HORAENVIO, H.HISTORICO STATUSCOMPROVEI, f.NomeComprovei FILIAL ";

            sqlEnviados += " FROM DT WITH (NOLOCK)   left join HISTORICOENVIOROTA H WITH(NOLOCK)  on dt.IdDt = h.Iddt ";
            sqlEnviados += " INNER JOIN CADASTRO MOT WITH (NOLOCK) ON MOT.IDCADASTRO = DT.IDPRIMEIROMOTORISTA ";
            sqlEnviados += " left JOIN DTROMANEIO DTR  WITH (NOLOCK)ON DTR.IDDT = DT.IDDT ";
            sqlEnviados += " left JOIN ROMANEIO R  WITH (NOLOCK)ON R.IDROMANEIO = DTR.IDROMANEIO ";

            sqlEnviados += " inner join Filial f on f.IdFilial = dt.IdFilial ";
            if (TextBox1.Text != "")
                sqlEnviados += " WHERE (CAST(DT.IDDT AS VARCHAR(50) ) = '" + TextBox1.Text + "' OR DT.NUMERO ='" + TextBox1.Text + "') ";

            if (DropDownList1.SelectedIndex > 0)
                sqlEnviados += " AND DT.IDFILIAL="+ DropDownList1.SelectedValue;

            if (cboStatus.SelectedIndex > 0)
                sqlEnviados += " AND Historico LIKE '"+ cboStatus.SelectedValue +"'";

            if (txtData.Text != "" && txtDataAte.Text != "")
            {
                try
                {
                    DateTime di = DateTime.Parse(txtData.Text);
                    DateTime df = DateTime.Parse(txtDataAte.Text);

                    sqlEnviados += " AND DT.DATADESAIDA between '" + di.ToString("yyyy-MM-dd") + " 00:00:00' and '" + di.ToString("yyyy-MM-dd") + " 23:59:59' ";
                }
                catch (Exception)
                {
                    sqlEnviados += " And DataDeSaida >=getdate()-5";
                }
            }
            else
                sqlEnviados += " And DataDeSaida >=getdate()-5";

                sqlEnviados += " ORDER BY   H.DATAHORA DESC, DT.IDDT ASC ";

            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sqlEnviados.ToUpper(), cnx);
            GridView1.DataBind();
            txtProcessado.Text = " Último Processamento: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            Session["dt"] = GridView1.DataSource;
        }

        
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            pesquisar();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "r")
            {
                string sql = "Update dt set ROTAENVIADACOMPROVEI=null  where IdDt = " + e.CommandName.ToString() + ";update romaneio set Andamento='EM ENTREGA' where IDRomaneio in (Select IDRomaneio from DTRomaneio where IDDT =" + e.CommandName.ToString() + "); delete from HistoricoEnvioRota where IdDt=" + e.CommandName.ToString() + "; Select 1;";
                Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);
                Label1.Text = "DT Liberada Para Errnvio.";
                pesquisar();
            }

            if (e.CommandArgument.ToString() == "pausar")
            {
                string sql = "Update dt set ROTAENVIADACOMPROVEI='Pausado em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " '  where IdDt = " + e.CommandName.ToString() + "; Select 1;";
                Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

                Label1.Text = "DT IDDT: " + e.CommandName.ToString() + " Pausada com sucesso..";
                pesquisar();
            }


            if (e.CommandArgument.ToString() == "pendentes")
            {
                string sql = " SELECT  DD.IDDocumento, dd.Numero FROM DOCUMENTO DD  INNER JOIN ROMANEIODOCUMENTO RDD ON RDD.IDDOCUMENTO = DD.IDDOCUMENTO  WHERE RDD.IDROMANEIO = "+e.CommandName+ " AND(DD.ENVIADOCOMPROVEI IS NULL or DD.ENVIADOCOMPROVEI='NAO' OR DD.ENVIADOCOMPROVEI ='SUBIR') AND DD.IDCLIENTE IN(SELECT IDCADASTRO FROM CLIENTESCOMPROVEI WHERE ATIVO = 'SIM')  ";
                DataTable dtNfP =  Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

                if(dtNfP.Rows.Count>0)
                {
                    GridView2.Visible = true;
                    GridView2.DataSource = dtNfP;
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.Visible = false;
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }

            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label1.Text = "";
            pesquisar();

        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;            
            pesquisar();
        }

        //protected void ExportToExcel(object sender, EventArgs e)
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment;filename=RotasComprovei_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";

        //    pesquisar();

        //    if (GridView1.Visible == true)
        //    {
        //        using (StringWriter sw = new StringWriter())
        //        {
        //            HtmlTextWriter hw = new HtmlTextWriter(sw);

        //            GridView1.HeaderRow.BackColor = Color.White;
        //            foreach (TableCell cell in GridView1.HeaderRow.Cells)
        //            {
        //                cell.BackColor = GridView1.HeaderStyle.BackColor;
        //            }
        //            foreach (GridViewRow row in GridView1.Rows)
        //            {
        //                row.BackColor = Color.White;
        //                foreach (TableCell cell in row.Cells)
        //                {
        //                    if (row.RowIndex % 2 == 0)
        //                    {
        //                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
        //                    }
        //                    else
        //                    {
        //                        cell.BackColor = GridView1.RowStyle.BackColor;
        //                    }
        //                    cell.CssClass = "textmode";
        //                }
        //            }

        //            GridView1.RenderControl(hw);
        //            //style to format numbers to string
        //            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        //            Response.Write(style);
        //            Response.Output.Write(sw.ToString());
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //    else
        //    {
        //        using (StringWriter sw = new StringWriter())
        //        {
        //            HtmlTextWriter hw = new HtmlTextWriter(sw);

        //            GridView1.HeaderRow.BackColor = Color.White;
        //            foreach (TableCell cell in GridView1.HeaderRow.Cells)
        //            {
        //                cell.BackColor = GridView1.HeaderStyle.BackColor;
        //            }
        //            foreach (GridViewRow row in GridView1.Rows)
        //            {
        //                row.BackColor = Color.White;
        //                foreach (TableCell cell in row.Cells)
        //                {
        //                    if (row.RowIndex % 2 == 0)
        //                    {
        //                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
        //                    }
        //                    else
        //                    {
        //                        cell.BackColor = GridView1.RowStyle.BackColor;
        //                    }
        //                    cell.CssClass = "textmode";
        //                }
        //            }

        //            GridView1.RenderControl(hw);
        //            //style to format numbers to string
        //            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        //            Response.Write(style);
        //            Response.Output.Write(sw.ToString());
        //            Response.Flush();
        //            Response.End();
        //        }


        //    }
        //}

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

        protected void Button2_Click(object sender, EventArgs e)
        {
            ExportToExcel((DataTable)Session["dt"], "");
        }
    }
}