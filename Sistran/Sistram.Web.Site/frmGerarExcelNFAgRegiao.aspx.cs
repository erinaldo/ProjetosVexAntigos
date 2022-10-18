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
using iTextSharp;
using System.IO;
using SistranBLL;
using iTextSharp.text.html.simpleparser;
public partial class frmGerarExcelNFAgRegiao : System.Web.UI.Page
{

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["tipo"] == null)
        {

            string clientesSelecionados = Sistran.Library.FuncoesUteis.retornarClientesResumoFilial(false);
            string strsql = "";
            strsql += "SELECT ";
            strsql += "            AG.NOME AS GRUPO,      ";
            strsql += "REG.NOME AS REGIAO,      ";
            strsql += "CASE WHEN NOT RICAD.RAZAOSOCIALNOME IS NULL THEN 'CLIENTE ESPECIAL:'  + RICAD.RAZAOSOCIALNOME   WHEN NOT RIST.NOME IS NULL THEN 'SETOR:' +  CAST(RIST.CODIGO AS VARCHAR(5)) +' '+ RIST.NOME   WHEN NOT RICID.NOME IS NULL THEN RICID.NOME   WHEN NOT RIEST.UF IS NULL THEN RIEST.UF   WHEN NOT RIPAI.NOME IS NULL THEN RIPAI.NOME   END SETOR,      ";
            strsql += "CT.IDDOCUMENTO CTIDDOCUMENTO, ";
            strsql += "CT.NUMERO CTNUMERO, ";
            strsql += "NF.IDDOCUMENTO, ";
            strsql += "NF.NUMERO [NOTA FISCAL],   ";
            strsql += "CONVERT(VARCHAR(10), NF.DATADEENTRADA, 103)DATADEENTRADA, ";
            strsql += "NF.CODIGODEBARRASRECEXP, ";
            strsql += "CONVERT(VARCHAR(10), NF.DATAPLANEJADA, 103) DATAPLANEJADA, CONVERT(VARCHAR(10), NF.DATADESAIDA, 103)DATADESAIDA , ";
            
            strsql += "CADREM.CNPJCPF [CNPJ REMEMTENTE],   ";
            strsql += "CADREM.RAZAOSOCIALNOME REMETENTE,   ";
            strsql += "CADDEST.CNPJCPF [CNPJ DESTINATARIO],   ";
            strsql += "CADDEST.RAZAOSOCIALNOME DESTINATARIO,   ";
            strsql += "CIDDEST.NOME CIDADE, ";
            strsql += "ESTDEST.UF, ";
            strsql += "CADCLI.RAZAOSOCIALNOME CLIENTE, ";
            strsql += "CAST(COALESCE(NF.VOLUMES,0) AS NUMERIC (10,0)) VOLUMES,      ";
            strsql += "COALESCE(NF.PESOBRUTO,0) PESOBRUTO,      ";
            strsql += "COALESCE(NF.METRAGEMCUBICA,0) METRAGEMCUBICA,      ";
            strsql += "COALESCE(NF.PESOCUBADO,0) PESOCUBADO,      ";
            strsql += "COALESCE(NF.VALORDANOTA,0) VALORDANOTA,      ";
            strsql += "COALESCE(DF.ICMSISS,0) ICMSISS  , ";
            strsql += "CONVERT(VARCHAR(10), NF.DATADEEMISSAO, 103) DATADEEMISSAO,";
            strsql += "abs(DATEDIFF(DAY, GETDATE(), NF.DATADEENTRADA)) [DIAS AGUARDANDO EMBARQUE] ";
            strsql += " FROM DOCUMENTO NF    ";
            strsql += " INNER JOIN DOCUMENTOFILIAL FL ON (FL.IDDOCUMENTO = NF.IDDOCUMENTO)    ";
            strsql += " LEFT JOIN DOCUMENTORELACIONADO DOCREL ON (DOCREL.IDDOCUMENTOFILHO = NF.IDDOCUMENTO)    ";
            strsql += " LEFT JOIN DOCUMENTO CT ON (CT.IDDOCUMENTO = DOCREL.IDDOCUMENTOPAI AND CT.TIPODEDOCUMENTO='CONHECIMENTO')    ";
            strsql += " LEFT JOIN REGIAOITEM RI ON (RI.IDREGIAOITEM = FL.IDREGIAOITEMFILIAL)   LEFT JOIN REGIAO REG ON (REG.IDREGIAO = RI.IDREGIAO)    ";
            strsql += " LEFT JOIN AGRUPAMENTOREGIAO AGR ON (AGR.IDREGIAO = REG.IDREGIAO)   LEFT JOIN AGRUPAMENTO AG ON (AG.IDAGRUPAMENTO = AGR.IDAGRUPAMENTO)    ";
            strsql += " LEFT JOIN DOCUMENTOFRETE DF ON (DF.IDDOCUMENTO = NF.IDDOCUMENTO AND DF.PROPRIETARIO = 'CLIENTE' )    ";
            strsql += " LEFT JOIN CADASTRO RICAD ON (RICAD.IDCADASTRO = RI.IDCADASTRO)   LEFT JOIN SETOR RIST ON (RIST.IDSETOR = RI.IDSETOR)    ";
            strsql += " LEFT JOIN CIDADE RICID ON (RICID.IDCIDADE = RI.IDCIDADE)  LEFT JOIN ESTADO RIEST ON (RIEST.IDESTADO = RI.IDESTADO)    ";
            strsql += " LEFT JOIN PAIS RIPAI ON (RIPAI.IDPAIS = RI.IDPAIS)    ";
            strsql += " inner JOIN CADASTRO CADREM ON CADREM.IDCADASTRO = NF.IDREMETENTE ";
            strsql += " inner JOIN CADASTRO CADDEST ON CADDEST.IDCADASTRO = NF.IDDESTINATARIO  ";
            strsql += " inner JOIN CIDADE CIDDEST ON CIDDEST.IDCIDADE = CADDEST.IDCIDADE ";
            strsql += " inner JOIN ESTADO ESTDEST ON ESTDEST.IDESTADO = CIDDEST.IDESTADO ";
            strsql += " inner JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = NF.IDCLIENTE ";
            strsql += " WHERE   FL.SITUACAO ='AGUARDANDO EMBARQUE'   ";
            strsql += " AND   (  NF.IDREMETENTE IN(" + clientesSelecionados + ")    ";
            strsql += " OR NF.IDCLIENTE IN(" + clientesSelecionados + ")   )    ";
            strsql += " ORDER BY 1,2,3 ";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);


            ExportToExcel(dt, "Notas");
        }
        else
        {

            gerarPDF();
        }
    }

    private void gerarPDF()
    {
        CarregarGridToPDF();
    }

    private void CarregarGridToPDF()
    {

        string clientes = Sistran.Library.FuncoesUteis.retornarClientes();
        System.Text.StringBuilder ssql = new System.Text.StringBuilder();


        ssql.Append(" SELECT   ");
        ssql.Append(" AG.NOME AS GRUPO,  ");
        ssql.Append(" REG.NOME AS REGIAO,  ");
        ssql.Append(" CASE WHEN NOT RICAD.RAZAOSOCIALNOME IS NULL THEN 'CLIENTE ESPECIAL:'  + RICAD.RAZAOSOCIALNOME  ");
        ssql.Append(" WHEN NOT RIST.NOME IS NULL THEN 'SETOR:' +  CAST(RIST.CODIGO AS VARCHAR(5)) +' '+ RIST.NOME  ");
        ssql.Append(" WHEN NOT RICID.NOME IS NULL THEN RICID.NOME  ");
        ssql.Append(" WHEN NOT RIEST.UF IS NULL THEN RIEST.UF  ");
        ssql.Append(" WHEN NOT RIPAI.NOME IS NULL THEN RIPAI.NOME  ");
        ssql.Append(" END SETOR,  ");
        ssql.Append(" CAST(COALESCE(SUM(NF.VOLUMES),0) AS NUMERIC (10,0)) VOLUMES,  ");
        ssql.Append(" COALESCE(SUM(NF.PESOBRUTO),0) PESOBRUTO,  ");
        ssql.Append(" COALESCE(SUM(NF.METRAGEMCUBICA),0) METRAGEMCUBICA,  ");
        ssql.Append(" COALESCE(SUM(NF.PESOCUBADO),0) PESOCUBADO,  ");
        ssql.Append(" COALESCE(SUM(NF.VALORDANOTA),0) VALORDANOTA,  ");
        ssql.Append(" COALESCE(SUM(DF.FRETE),0) FRETE,  ");
        ssql.Append(" COALESCE(SUM(DF.ICMSISS),0) ICMSISS,  ");
        ssql.Append(" COUNT(*) NOTAS  ");
        ssql.Append(" FROM DOCUMENTO NF  ");
        ssql.Append(" INNER JOIN DOCUMENTOFILIAL FL ON (FL.IDDOCUMENTO = NF.IDDOCUMENTO)  ");
        ssql.Append(" LEFT JOIN DOCUMENTORELACIONADO DOCREL ON (DOCREL.IDDOCUMENTOFILHO = NF.IDDOCUMENTO)  ");
        ssql.Append(" LEFT JOIN DOCUMENTO CT ON (CT.IDDOCUMENTO = DOCREL.IDDOCUMENTOPAI AND CT.TIPODEDOCUMENTO='CONHECIMENTO')  ");
        ssql.Append(" LEFT JOIN REGIAOITEM RI ON (RI.IDREGIAOITEM = FL.IDREGIAOITEMFILIAL)  ");
        ssql.Append(" LEFT JOIN REGIAO REG ON (REG.IDREGIAO = RI.IDREGIAO)  ");
        ssql.Append(" LEFT JOIN AGRUPAMENTOREGIAO AGR ON (AGR.IDREGIAO = REG.IDREGIAO)  ");
        ssql.Append(" LEFT JOIN AGRUPAMENTO AG ON (AG.IDAGRUPAMENTO = AGR.IDAGRUPAMENTO)  ");
        ssql.Append(" LEFT JOIN DOCUMENTOFRETE DF ON (DF.IDDOCUMENTO = NF.IDDOCUMENTO AND DF.PROPRIETARIO = 'CLIENTE' )  ");
        ssql.Append(" LEFT JOIN CADASTRO RICAD ON (RICAD.IDCADASTRO = RI.IDCADASTRO)  ");
        ssql.Append(" LEFT JOIN SETOR RIST ON (RIST.IDSETOR = RI.IDSETOR)  ");
        ssql.Append(" LEFT JOIN CIDADE RICID ON (RICID.IDCIDADE = RI.IDCIDADE) ");
        ssql.Append(" LEFT JOIN ESTADO RIEST ON (RIEST.IDESTADO = RI.IDESTADO)  ");
        ssql.Append(" LEFT JOIN PAIS RIPAI ON (RIPAI.IDPAIS = RI.IDPAIS)  ");
        ssql.Append(" WHERE  ");
        ssql.Append(" FL.SITUACAO ='AGUARDANDO EMBARQUE' ");
        ssql.Append(" AND  ");
        ssql.Append(" ( ");
        ssql.Append(" NF.IDREMETENTE IN(" + clientes + ")  ");
        ssql.Append(" OR NF.IDCLIENTE IN(" + clientes + ")  ");
        ssql.Append(" )  ");
        ssql.Append(" GROUP BY AG.NOME, REG.NOME, RICAD.RAZAOSOCIALNOME,RIST.NOME,RICID.NOME, RIEST.UF,RIPAI.NOME, RIST.CODIGO  ");
        ssql.Append(" ORDER BY 1,2,3 ");


        DataTable dt = new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), ssql.ToString()).Tables[0];


        DataTable distinctTable = dt.DefaultView.ToTable(true, "GRUPO");

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' border='1' cellspacing='1' celpanding='1' width='99%'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='8' style='background-color:silver'> NF AGUARD. EMBARQUE POR REGIÃO "));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        for (int i = 0; i < distinctTable.Rows.Count; i++)
        {
            int notas = 0;
            int volumes = 0;
            decimal pesoBruto = 0;
            decimal metragem = 0;
            decimal pesocubado = 0;
            decimal vlnota = 0;
            //decimal frete = 0;

            string par = distinctTable.Rows[i]["GRUPO"].ToString();

            if (distinctTable.Rows[i]["GRUPO"].ToString() == "")
                par = "GRUPO is null";
            else
                par = "Grupo='" + distinctTable.Rows[i]["GRUPO"].ToString() + "'";

            DataRow[] orw = dt.Select(par, "SETOR");



           /* PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            for (int ii = 0; ii < orw.Length; ii++)
            {

                if (ii == 0)
                {

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;' nowrap='nowrap'  valign='top' >GRUPO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' colspan='8' align='left'  style='font-size:7pt;' nowrap='nowrap'  valign='top' >" + (orw[ii]["GRUPO"].ToString() == "" ? " - " : orw[ii]["GRUPO"].ToString())));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >REGIÃO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >SETOR"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >NOTAS FISCAIS"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >VOLUMES"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >PESO BRUTO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >M3"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >PESO CUBADO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >VALOR DA NOTA"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >FRETE"));
                    //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                }



                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + orw[ii]["REGIAO"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + orw[ii]["SETOR"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                notas += int.Parse(orw[ii]["notas"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + orw[ii]["notas"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                volumes += int.Parse(orw[ii]["VOLUMES"].ToString().Replace(",", "."));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + orw[ii]["VOLUMES"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                pesoBruto += decimal.Parse(orw[ii]["PESOBRUTO"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;' nowrap='nowrap'  valign='top' >" + orw[ii]["PESOBRUTO"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                metragem += decimal.Parse(orw[ii]["METRAGEMCUBICA"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + orw[ii]["METRAGEMCUBICA"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                pesocubado += decimal.Parse(orw[ii]["PESOCUBADO"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'   valign='top' >" + orw[ii]["PESOCUBADO"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                vlnota += decimal.Parse(orw[ii]["VALORDANOTA"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'   valign='top' >" + orw[ii]["VALORDANOTA"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                //frete += decimal.Parse(orw[ii]["FRETE"].ToString());
                //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'   valign='top' >" + orw[ii]["FRETE"].ToString()));
                //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


            }

            ////totais
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' colspan='2' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >total"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >"));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >" + notas.ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + volumes.ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >" + pesoBruto.ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >" + metragem.ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >" + pesocubado));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >" + vlnota));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >"+ frete));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            //////////



            //PlaceHolder1.Controls.Add(new LiteralControl(@"<hr>"));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"<br>"));


        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
            * */

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            for (int ii = 0; ii < orw.Length; ii++)
            {

                if (ii == 0)
                {

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;' nowrap='nowrap'  valign='top' >GRUPO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' colspan='8' align='left'  style='font-size:7pt;' nowrap='nowrap'  valign='top' >" + (orw[ii]["GRUPO"].ToString() == "" ? " - " : orw[ii]["GRUPO"].ToString())));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >REGIÃO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >SETOR"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >NOTAS FISCAIS"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >VOLUMES"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >PESO BRUTO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >M3"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >PESO CUBADO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >VALOR DA NOTA"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >FRETE"));
                    //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                }



                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + orw[ii]["REGIAO"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + orw[ii]["SETOR"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                notas += int.Parse(orw[ii]["notas"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + orw[ii]["notas"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                volumes += int.Parse(orw[ii]["VOLUMES"].ToString().Replace(",", "."));//.ToString("#,0.000")
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + decimal.Parse(orw[ii]["VOLUMES"].ToString()).ToString("#,000")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                pesoBruto += decimal.Parse(orw[ii]["PESOBRUTO"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;' nowrap='nowrap'  valign='top' >" + decimal.Parse(orw[ii]["PESOBRUTO"].ToString()).ToString("#,0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                metragem += decimal.Parse(orw[ii]["METRAGEMCUBICA"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + orw[ii]["METRAGEMCUBICA"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                pesocubado += decimal.Parse(orw[ii]["PESOCUBADO"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'   valign='top' >" + decimal.Parse(orw[ii]["PESOCUBADO"].ToString()).ToString("#,0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                vlnota += decimal.Parse(orw[ii]["VALORDANOTA"].ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'   valign='top' >R$ " + decimal.Parse(orw[ii]["VALORDANOTA"].ToString()).ToString("#,0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                //frete += decimal.Parse(orw[ii]["FRETE"].ToString());
                //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;'   valign='top' >" + orw[ii]["FRETE"].ToString()));
                //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


            }

            ////totais
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' colspan='2' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >total"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >"));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >" + notas.ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >" + volumes.ToString("#,000")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >" + pesoBruto.ToString("#,0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >" + metragem.ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >" + pesocubado.ToString("#,0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >R$ " + vlnota.ToString("#,0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'  nowrap='nowrap' valign='top' >"+ frete));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            //////////



            //PlaceHolder1.Controls.Add(new LiteralControl(@"<hr>"));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"<br>"));


        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Notas" + Guid.NewGuid() + ".pdf");
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        PlaceHolder1.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());

        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3, 10.0F, 10.0F, 10.0F, 0.0F);

        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);


        //Dim pdfDoc As New Document(PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F)
        //Dim htmlparser As New HTMLWorker(pdfDoc)
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();


        //Response.Buffer = true;
        //Response.ClearContent();
        //Response.ClearHeaders();

        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=notas.pdf");

        //EnableViewState = false;

        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);

        ////---Renders the DataGrid and then dumps it into the HtmlTextWriter Control
        //PlaceHolder1.RenderControl(hw);

        ////---Utilize the Response Object to write the StringWriter to the page
        //Response.Write(sw.ToString());
        //Response.Flush();
        //Response.Close();
        //Response.End();
    }
}