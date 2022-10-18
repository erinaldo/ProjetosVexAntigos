using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Configuration;
using System.Globalization;
using System.Threading;
using ChartDirector;
using System.Web.UI.WebControls;
using System.Data;

public partial class OperacaoClienteDetalhe : System.Web.UI.Page
{
    string datas = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         
        if (!IsPostBack)
        {

            if (datas == "")
            {
                datas = Server.HtmlDecode(Request.QueryString["dia"]);
                datas = DateTime.Parse(datas).ToString("yyyy-MM-dd");
            }
                Gerar(datas);

                if (Request.QueryString["t"] == null)
                this.Title = "Operação Cliente - " + Request.QueryString["Nome"];
                else
                    this.Title = "Operação Cliente - "+Request.QueryString["Nome"]+" (Nao Entregues)";
         
            
        }
    }

    private void Gerar(string datas)
    {

        string sstr = "";

        if (Request.QueryString["idcliente"] == "9")
                sstr = " EXEC RETORNAROPERACAOCLIENTE_DET_ricoy 9, '" + datas + "',"+ (Request.QueryString["t"] == null?"'AGUARDANDO EMBARQUE'": " null ");
        else
                sstr = " EXEC RETORNAROPERACAOCLIENTE_DET "+ (Request.QueryString["idcliente"] + ", '" + datas + "',"+ (Request.QueryString["t"] == null?"'AGUARDANDO EMBARQUE'": " null "));
        

        DataTable ds = Sistran.Library.GetDataTables.RetornarDataSetWS(sstr, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

        sstr = "<table class='table' cellspacing=1 celpanding=1 >";

        sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
        
        if (Request.QueryString["t"] == null)
            sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='10'> OPERAÇÃO CLIENTE "+ Request.QueryString["Nome"]+" - NOTAS FISCAIS EMITIDAS (SEM DT EMITIDA) </td>";
        else
            sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='10'> OPERAÇÃO CLIENTE " + Request.QueryString["Nome"] + "- NOTAS FISCAIS EMITIDAS (NÃO ENTREGUES) </td>";

        sstr += "</tr>";
        sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
        sstr += "<td class='tdpCabecalho'  align='rigth' nowrap=nowrap style='font-size:8pt;width:1%'>#</td>";
        sstr += "<td class='tdpCabecalho'  align='rigth' nowrap=nowrap style='font-size:8pt;width:1%'>NOTA FISCAL</td>";
        sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%'>SÉRIE</td>";
        sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL</td>";
        sstr += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>EMISSÃO</td>";
        sstr += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA DE ENTRADA</td>";
        sstr += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>PREVISÃO DE SAÍDA</td>";
        sstr += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA PLANEJADA</td>";
        sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%'>REMETENTE</td>";
        sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%'>DESTINATÁRIO</td>";
        sstr += "</tr>";

        for (int i = 0; i < ds.Rows.Count; i++)
        {
            sstr += "<tr'>";
            sstr += "<td class='tdpR' nowrap=nowrap style='font-size:8pt;width:1%'>" + (i+1).ToString() + "</td>";

            sstr += "<td class='tdpR' nowrap=nowrap style='font-size:8pt;width:1%'>" + ds.Rows[i]["NUMERO"].ToString() + "</td>";
            sstr += "<td class='tdp' nowrap=nowrap style='font-size:8pt;width:1%'>" + ds.Rows[i]["SERIE"].ToString() + "</td>";
            sstr += "<td class='tdp' nowrap=nowrap style='font-size:8pt;width:1%'>" + ds.Rows[i]["FILIAL"].ToString() + "</td>";
            sstr += "<td class='tdpCenter' nowrap=nowrap style='font-size:8pt;width:1%'>" + ds.Rows[i]["DATADEEMISSAO"].ToString() + "</td>";
            sstr += "<td class='tdpCenter' nowrap=nowrap style='font-size:8pt;width:1%'>" + ds.Rows[i]["DATADEENTRADA"].ToString() + "</td>";
            sstr += "<td class='tdpCenter' nowrap=nowrap style='font-size:8pt;width:1%'>" + ds.Rows[i]["PREVISAODESAIDA"].ToString() + "</td>";
            sstr += "<td class='tdpCenter' nowrap=nowrap style='font-size:8pt;width:1%'>" + ds.Rows[i]["DATAPLANEJADA"].ToString() + "</td>";
            sstr += "<td class='tdp' nowrap=nowrap style='font-size:8pt;width:1%'>" + ds.Rows[i]["REMETENTE"].ToString() + "</td>";
            sstr += "<td class='tdp' nowrap=nowrap style='font-size:8pt;width:1%'>" + ds.Rows[i]["DESTINATARIO"].ToString() + "</td>";


           
            sstr += "</tr>";
        }        
        sstr += "</table>";
        string Tba = "<html><head>";
        Tba += " <STYLE type='text/css'>";
        Tba += " body ";
        Tba += " { ";
        Tba += " margin: 0px; ";
        Tba += " background-color: #f8f8f8; ";
        Tba += " font-family: Verdana; ";
        Tba += " text-align: left; ";
        Tba += " font-size: 12PX; }";
        Tba += " .table  ";
        Tba += " { ";
        Tba += " background-color: #E0E0E0; ";
        //Tba += " width: 50%; ";
        Tba += " font-family: Arial, Helvetica, sans-serif; ";
        Tba += " font-size: 7pt; ";
        Tba += " font-weight: bold; ";
        Tba += " } ";

        Tba += " .tableFundoClaro ";
        Tba += " { ";
        Tba += " background-color: #F8F8F8; ";
        //Tba += " width: 100%; ";
        Tba += " font-family: Arial, Helvetica, sans-serif; ";
        Tba += " font-size: 7pt; ";
        Tba += " font-weight: bold; ";
        Tba += " } ";

        Tba += " .tableSemCorFundo ";
        Tba += " {	 ";
        //Tba += " width: 50%; ";
        Tba += " font-family: Arial, Helvetica, sans-serif; ";
        Tba += " font-size: 7pt; ";
        Tba += " font-weight: bold; ";
        Tba += " } ";

        Tba += " .table2 ";
        Tba += " { ";
        Tba += " background-color:#E0E0E0 ;  ";
        Tba += " font-family: Arial, Helvetica, sans- ";
        Tba += " } ";

        Tba += " .tdpCenter ";
        Tba += " { ";
        Tba += " border: 0.1pt solid #FFFFFF; ";
        Tba += " font-size:8pt; ";
        Tba += " font-family:Verdana; ";
        Tba += " text-align: center ; ";
        Tba += " nowrap:nowrap; ";
        Tba += " font-weight:normal; ";
        Tba += " } ";

        Tba += " .tdp ";
        Tba += " { ";
        Tba += " border: 0.1pt solid #FFFFFF; ";
        Tba += " font-size:8pt; ";
        Tba += " font-family:Verdana; ";
        Tba += " nowrap:nowrap; ";
        Tba += " font-weight:normal; ";
        Tba += " text-align: left; ";
        Tba += " vertical-align:middle; ";

        Tba += " } ";
        Tba += " .tdpSemAlign ";
        Tba += " { ";
        Tba += " border: 0.5pt solid #FFFFFF; ";
        Tba += " font-size:8pt; ";
        Tba += " font-family:Verdana; ";
        Tba += " nowrap:nowrap; ";
        Tba += " font-weight:normal; ";
        Tba += " } ";

        Tba += " .tdpSemAlignGray ";
        Tba += " { ";
        Tba += " border: 0.5pt solid #FFFFFF; ";
        Tba += " font-size:8pt; ";
        Tba += " font-family:Verdana; ";
        Tba += " nowrap:nowrap; ";
        Tba += " font-weight:normal; ";
        Tba += " background-color:GrayText; ";
        Tba += " } ";


        Tba += " .tdpCenter ";
        Tba += " { ";
        Tba += " border: 0.1pt solid #FFFFFF; ";
        Tba += " font-size:8pt; ";
        Tba += " font-family:Verdana; ";
        Tba += " text-align: center ; ";
        Tba += " nowrap:now ";
        Tba += " } ";
        Tba += " .tdpR ";
        Tba += " { ";
        Tba += " border: 0.1pt solid #FFFFFF; ";
        Tba += " font-size:8pt; ";
        Tba += " font-family:Verdana; ";
        Tba += " text-align:right; ";
        Tba += " nowrap:nowrap; ";
        Tba += " font-weight:normal;	 ";
        Tba += " } ";

        Tba += "  .tdpVerdana ";
        Tba += " { ";
        Tba += " border: 0.1pt solid #FFFFFF; ";
        Tba += " font-size:8pt; ";
        Tba += " font-family:Verdana; ";
        //Tba += " text-align: left; ";
        Tba += " nowrap:nowrap; ";
        Tba += " } ";

        Tba += " .tdpCabecalho ";
        Tba += " { ";
        Tba += " border: 0.1pt solid #FFFFFF; ";
        Tba += " height: 13pt; ";
        Tba += " font-size:9pt; ";
        Tba += " font-family:Verdana; ";
        Tba += " font-weight:bold; ";
        Tba += " text-transform: uppercase;	 ";
        Tba += " } ";

        Tba += " .tdpRVerdanaVerde ";
        Tba += " { ";
        Tba += " border: 0.1pt solid #FFFFFF; ";
        Tba += " font-size:8pt; ";
        Tba += " font-family:Verdana; ";
        Tba += " text-align: right;	 ";
        Tba += " nowrap:nowrap; ";
        Tba += " background-color:#20AE3F; ";
        Tba += " } ";

        Tba += " .tdpRVerdanaAmarelo ";
        Tba += " { ";
        Tba += " border: 0.1pt solid #FFFFFF; ";
        Tba += " font-size:8pt; ";
        Tba += " font-family:Verdana; ";
        Tba += " text-align: right;	 ";
        Tba += " nowrap:nowrap; ";
        Tba += " background-color:#DEDE40; ";
        Tba += " } ";

        Tba += " .tdpRVerdanaVermelho ";
        Tba += " { ";
        Tba += " 	border: 0.1pt solid #FFFFFF; ";
        Tba += " 	font-size:8pt; ";
        Tba += " 	font-family:Verdana; ";
        Tba += " 	text-align: right;	 ";
        Tba += " 	nowrap:nowrap; ";
        Tba += " 	background-color:#DE4040; ";
        Tba += "} ";

        Tba += " </STYLE> ";
        Tba += "</HEAD>";


        string juncao = Tba + sstr;
        juncao += "</body></head><html>";

        phDetalhe.Controls.Add(new LiteralControl(juncao));
    }
}