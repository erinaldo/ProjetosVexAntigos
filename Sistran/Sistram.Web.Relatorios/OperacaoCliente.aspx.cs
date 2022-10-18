using System;
using System.Web.UI;
using System.Data;

public partial class OperacaoCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button1.Attributes.Add("OnClick", "window.open('frmRptLista.aspx?data=" + DateTime.Now.ToShortDateString() + "', '','fullscreen=yes, scrollbars=auto'); return false;");
            Gerar();
            this.Title = "Operação Cliente - " + Request.QueryString["Nome"];
        }
    }

    private void Gerar()
    {
        string sstr = "";

        DataTable ds;

        if (Request.QueryString["idcliente"] == "9")
            ds = Sistran.Library.GetDataTables.RetornarDataSetWS("exec RETORNAROPERACAOCLIENTE_RICOY 9", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
        else
        {
            ds = Sistran.Library.GetDataTables.RetornarDataSetWS("exec RETORNAROPERACAOCLIENTE " + Request.QueryString["idcliente"], Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
        }
        
        sstr = "<table class='table' cellspacing=1 celpanding=1 >"; 
        sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
        sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='9'> " + Request.QueryString["Nome"] + " - NOTAS FISCAIS EMITIDAS </td>";
        sstr += "</tr>";


        sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
        sstr += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA</td>";

        sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FISCAIS EMITIDAS</td>";
        sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO</td>";

        sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FISCAIS SEM DT </td>";
        sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO</td>";

        sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FISCAIS COM DT</td>";
        sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FISCAIS ENTREGUES</td>";
        sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FISCAIS NAO ENTREGUES</td>";
        sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% DE ENTREGUES</td>";
        sstr += "</tr>";

        for (int i = 0; i < ds.Rows.Count; i++)
        {
            sstr += "<tr'>";
            if(int.Parse(ds.Rows[i]["SEMDT"].ToString())>0)
                sstr += "<td class='tdpCenter'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'><a href='operacaoClienteDetalhe.aspx?idcliente="+Request.QueryString["idcliente"]+"&nome="+ Request.QueryString["Nome"]+"&dia=" + Server.HtmlEncode(DateTime.Parse(ds.Rows[i]["DATADEEMISSAO"].ToString()).ToString("dd/MM/yyyy")) + "' target='_blank' class='link'>" + DateTime.Parse(ds.Rows[i]["DATADEEMISSAO"].ToString()).ToString("dd/MM/yyyy") + "</a></td>";
            else
                sstr += "<td class='tdpCenter'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>" + DateTime.Parse(ds.Rows[i]["DATADEEMISSAO"].ToString()).ToString("dd/MM/yyyy") + "</td>";

            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Rows[i]["NFEMITIDAS"].ToString()) + "</td>";
            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + decimal.Parse(ds.Rows[i]["PESOEMITIDAS"].ToString()).ToString("#,0") + "</td>";

            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Rows[i]["SEMDT"].ToString()) + " </td>";
            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + decimal.Parse(ds.Rows[i]["PESOSEMDT"].ToString()).ToString("#,0") + "</td>";



            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Rows[i]["COMDT"].ToString()) + "</td>";
            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Rows[i]["ENTREGUES"].ToString()) + "</td>";

            if (int.Parse(ds.Rows[i]["NAOENTREGUES"].ToString()) > 0)
                sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'><a href='operacaoClienteDetalhe.aspx?idcliente=" + Request.QueryString["idcliente"] + "&nome=" + Request.QueryString["Nome"] + "&t=n&dia=" + Server.HtmlEncode(DateTime.Parse(ds.Rows[i]["DATADEEMISSAO"].ToString()).ToString("dd/MM/yyyy")) + "' target='_blank' class='link'>" + int.Parse(ds.Rows[i]["NAOENTREGUES"].ToString()) + "</a></td>";
            else
                sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Rows[i]["NAOENTREGUES"].ToString()) + "</td>";
            
            decimal x = decimal.Parse(ds.Rows[i]["PEC_ENTREGAS"].ToString());
            string s = "tdpR";

         
            sstr += "<td class='" + s + "'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + decimal.Parse(ds.Rows[i]["PEC_ENTREGAS"].ToString()).ToString("#0.00") + "</td>";
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
        Tba += " font-family: Arial, Helvetica, sans-serif; ";
        Tba += " font-size: 7pt; ";
        Tba += " font-weight: bold; ";
        Tba += " } ";

        Tba += " .tableFundoClaro ";
        Tba += " { ";
        Tba += " background-color: #F8F8F8; ";

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