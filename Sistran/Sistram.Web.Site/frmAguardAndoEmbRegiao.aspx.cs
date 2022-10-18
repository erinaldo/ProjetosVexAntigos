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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using ChartDirector;

public partial class frmAguardAndoEmbRegiao : System.Web.UI.Page
{
    #region Events

    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
        ChartDirector.WebChartViewer.OnPageInit(Page);
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            intervalo = FuncoesGerais.RetornarIntervaloDiasPesqusa();
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

            // GerarGraficos();

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                //btnGerarReport.Visible = false;
                //btnPDF.Visible = false;

                //btnGerarReport.Attributes.Add("onClick", "window.open('frmRptDesempenhoPorDia.aspx?tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela2', 'yes')");
                //btnPDF.Attributes.Add("onClick", "window.open('frmRptDesempenhoPorDia.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");
                ////DateTime primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //txtF.Text = DateTime.Now.ToShortDateString();
                //txtI.Text = primeiroDiaMes.ToShortDateString();
                string[] DataConf = FuncoesGerais.DataConf();
                //txtI.Text = DataConf[0];
                //txtF.Text = DataConf[1];
                CarregarGrid();
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            //Session["DataConf"] = txtI.Text + "|" + txtF.Text;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");

        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CarregarGrid();
    }

    #endregion

    #region Methods


    private void CarregarGrid()
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

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width='99%'>"));


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

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' nowrap='nowrap' valign='top' >"+ metragem.ToString()));
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
        btnGerarReport.Visible = true;
        btnPDF.Visible = true;

    }

    //protected void MontarTable()
    //{
    //    List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
    //    SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

    //    decimal total = Convert.ToDecimal(0);
    //    TimeSpan ts = Convert.ToDateTime(txtF.Text) - Convert.ToDateTime(txtI.Text);
    //    if (Convert.ToDateTime(txtF.Text) < Convert.ToDateTime(txtI.Text))
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A data inicial não pode ser maior que a data final.')", true);
    //        return;
    //    }

    //    if (ts.Days > intervalo)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + intervalo.ToString() + " dias.')", true);
    //        return;
    //    }



    //    DataTable dt = NotasFiscais.ListarDesempenhoEntregaDia(Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), Sistran.Library.FuncoesUteis.retornarClientesResumoFilial(false), Session["Conn"].ToString());
    //    PlaceHolder1.Controls.Clear();
    //    DataTable dtNaoEntregues = NotasFiscais.ListarDesempenhoEntregaDiaNaoEntregue(Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), Sistran.Library.FuncoesUteis.retornarClientesResumoFilial(false), Session["Conn"].ToString());

    //    Session["dtNaoEntregues"] = dtNaoEntregues;

    //    if (dt.Rows.Count > 0)
    //        pnlGrafico.Visible = true;
    //    else
    //        pnlGrafico.Visible = false;


    //    PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 width='200px'>"));
    //    if (dt.Rows.Count > 0)
    //    {
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;' COLSPAN='5' >N.F. ENTREGUES"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Transit Time"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Horas"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>N.F. Entregues"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;width:20%'>% N.F."));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>% Acumulado"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

    //        total = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDecimal(dt.Compute("SUM(NOTASNAOFISCAISENTREGUE)", ""));
    //        decimal itac = Convert.ToDecimal("0.00");
    //        decimal calc = Convert.ToDecimal("0.00");

    //        foreach (DataRow item in dt.Rows)
    //        {

    //            decimal it = Convert.ToDecimal(item["NOTASFISCAISENTREGUE"]);

    //            if (total > 0)
    //                calc = (it / total) * 100;

    //            itac += calc;

    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='center' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["PRAZOUTILIZADO"]).ToString("#0")));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='center' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["PRAZOUTILIZADO"]) * 24));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["NOTASFISCAISENTREGUE"]).ToString("#0")));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px'>" + calc.ToString("#0.00").ToString() + "%"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + itac.ToString("#0.00").ToString() + "%"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
    //        }


    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>SUB-TOTAL N.F. ENTREGUES"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + (total - Convert.ToDecimal(dt.Compute("SUM(NOTASNAOFISCAISENTREGUE)", ""))).ToString("#0")));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;width:20%'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


    //        if (dtNaoEntregues.Rows.Count > 0)
    //        {
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;' COLSPAN='5' >N.F. NÃO ENTREGUES"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>STATUS"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>QTD. NF."));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;width:20%'>% N.F."));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>% Acumulado"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
    //        }

    //        foreach (DataRow item in dtNaoEntregues.Rows)
    //        {
    //            if (total > 0)
    //                calc = (Convert.ToDecimal(item["NOTAS"].ToString()) / total) * 100;
    //            itac += calc;

    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr >"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'><a class='link' href='NotasFiscaisAbertoFiltro.aspx?situacao=" + Server.UrlEncode(item["SITUACAO"].ToString()) + "'>" + (item["SITUACAO"].ToString() == "AGUARDANDO SOLUCAO" ? "COM OCORRÊNCIA" : item["SITUACAO"].ToString()).ToString() + "</A>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["NOTAS"].ToString()));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + calc.ToString("#0.00") + "%"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + itac.ToString("#0.00") + "%"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
    //        }


    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'><B>SUB-TOTAL N.F. NÃO ENTREGUE</b>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='right' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(dt.Compute("SUM(NOTASNAOFISCAISENTREGUE)", "")).ToString("#0")));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='right' style='font-size:7pt;height:10px'>" + ((Convert.ToDecimal(dt.Compute("SUM(NOTASNAOFISCAISENTREGUE)", "")) / total) * 100).ToString("#0.00") + "%"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='right' style='font-size:7pt;height:10px'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>TOTAL N.F. "));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + (total).ToString("#0")));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;width:20%'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
    //    }
    //    else
    //    {
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
    //        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
    //    }

    //    PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
    //    if (dt.Rows.Count > 0)
    //    {
    //        //btnGerarReport.Visible = true;
    //        //btnPDF.Visible = true;
    //        //Session["dt"] = dt;
    //    }

    //    if (dt.Rows.Count > 0)
    //        GerarGraficos(dt, dtNaoEntregues);
    //}

    private void GerarGraficos(DataTable dt, DataTable dtNaoEntregue)
    {
        //pnlGrafPerc.Visible = true;
        //pnlGrafPerc0.Visible = true;
        //pnlGrafPerc.Controls.Add((WebChartViewer)Grafico(dt));
        //pnlGrafAcum.Controls.Add((WebChartViewer)Grafico2(dt));
        //pnlGrafPerc0.Controls.Add((WebChartViewer)GraficoNaoEntregue(dtNaoEntregue));
    }

    //pizza
    private WebChartViewer Grafico(DataTable table)
    {
        MultiChart chart = new MultiChart(400, 400);
        //        MultiChart chart = new MultiChart(300, 300);
        double[] numArray = new double[table.Rows.Count + 1];
        double[] data = new double[table.Rows.Count + 1];
        double[] numArray3 = new double[table.Rows.Count + 1];
        string[] labels = new string[table.Rows.Count + 1];
        string[] texts = new string[table.Rows.Count + 1];
        int[] colors = new int[table.Rows.Count + 1];
        int sectorNo = 0;
        int index = 0;

        foreach (DataRow row in table.Rows)
        {
            numArray3[index] = (Convert.ToDouble(row["NOTASFISCAISENTREGUE"]) / (Convert.ToDouble(table.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", ""))) * 100);
            labels[index] = row["PRAZOUTILIZADO"].ToString();
            colors[index] = 0x33868e;
            index++;
        }

        numArray3[index] = (Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", "")) / (Convert.ToDouble(table.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", ""))) * 100);
        labels[index] = "N.F. Não Entregue<*BR*>";
        colors[index] = 0x33868e;

        //pizza
        //PieChart chart3 = new PieChart(300, 300);
        PieChart chart3 = new PieChart(530, 540);


        chart3.setPieSize(185, 70, 80);
        ///// chart3.setPieSize(160, 135, 110);

        chart3.set3D(20);
        chart3.setStartAngle(160.0);
        chart3.setLabelLayout(0);
        chart3.setLabelStyle().setBackground(-65529, -16777216, Chart.glassEffect());
        chart3.setLineColor(-65529, 0);
        chart3.setData(numArray3, labels);
        chart3.setLabelStyle("verdana.ttf", 6.5);

        chart3.setColors(Chart.transparentPalette);

        chart3.setExplode(sectorNo);
        chart.addChart(5, 30, chart3);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);

        return viewer;
    }

    //barras
    private WebChartViewer Grafico2(DataTable table)
    {
        MultiChart chart = new MultiChart(400, 400);
        double[] numArray = new double[table.Rows.Count + 1];
        double[] data = new double[table.Rows.Count + 1];
        double[] numArray3 = new double[table.Rows.Count + 1];
        string[] labels = new string[table.Rows.Count + 1];
        string[] texts = new string[table.Rows.Count + 1];
        int[] colors = new int[table.Rows.Count + 1];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(Convert.ToDouble(table.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", "")));

        int index = 0;

        decimal itac = Convert.ToDecimal("0.00");
        double calc = 0.0;
        decimal total;

        total = Convert.ToDecimal(Convert.ToDouble(table.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", "")));

        foreach (DataRow row in table.Rows)
        {
            decimal it = Convert.ToDecimal(row["NOTASFISCAISENTREGUE"]);

            if (total > 0)
                calc = Convert.ToDouble((it / total) * 100);

            itac += Convert.ToDecimal(calc);
            data[index] = Convert.ToDouble(itac);
            numArray3[index] = Convert.ToDouble(itac);
            labels[index] = row["PRAZOUTILIZADO"].ToString();
            texts[index] = itac.ToString("N2") + "%";
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }

            index++;
        }

        //itac += Convert.ToDecimal(calc);
        data[index] = Convert.ToDouble((100 - itac));
        numArray3[index] = Convert.ToDouble(itac);
        labels[index] = "N.F. Não <*br*>Entregue";
        texts[index] = (100 - itac).ToString("N2") + "%";
        //colors[index] = 0x33868e;        
        num2 = numArray3[index];
        sectorNo = index;


        XYChart c = new XYChart(400, 440);
        c.addExtraField(texts);
        c.setPlotArea(50, 5, 300, 340, 0xf8f8f8, 0xffffff);
        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        layer.setAggregateLabelStyle("verdana.ttf", 7.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("%", "verdana.ttf", 7.0);
        c.xAxis().setTitle("TRANSIT TIME", "verdana.ttf", 7.0);
        c.xAxis().setLabelStyle("verdana.ttf", 7.0, 0);
        chart.addChart(0, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }

    private WebChartViewer GraficoNaoEntregue(DataTable table)
    {
        MultiChart chart = new MultiChart(400, 250);
        double[] numArray = new double[table.Rows.Count + 1];
        double[] data = new double[table.Rows.Count + 1];
        double[] numArray3 = new double[table.Rows.Count + 1];
        string[] labels = new string[table.Rows.Count + 1];
        string[] texts = new string[table.Rows.Count + 1];
        int[] colors = new int[table.Rows.Count + 1];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(Convert.ToDouble(table.Compute("SUM(notas)", "") == DBNull.Value ? Convert.ToDouble(0) : Convert.ToDouble(table.Compute("SUM(notas)", ""))));

        int index = 0;

        decimal itac = Convert.ToDecimal("0.00");
        decimal total;

        total = Convert.ToDecimal(Convert.ToDouble(table.Compute("SUM(notas)", "") == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(table.Compute("SUM(notas)", ""))));

        foreach (DataRow row in table.Rows)
        {
            decimal it = Convert.ToDecimal(row["notas"]);

            data[index] = Convert.ToDouble(it);
            numArray3[index] = Convert.ToDouble(it);
            labels[index] = (row["SITUACAO"].ToString() == "AGUARDANDO SOLUCAO" ? "COM OCORRÊNCIA" : row["SITUACAO"].ToString()).ToString().Replace(" ", "<*BR*>");
            texts[index] = it.ToString();
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }

            index++;
        }

        XYChart c = new XYChart(400, 300);
        c.addExtraField(texts);
        c.setPlotArea(130, 5, 250, 220, 0xf8f8f8, 0xffffff);
        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        layer.setAggregateLabelStyle("verdana.ttf", 7.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("%", "verdana.ttf", 7.0);
        c.xAxis().setTitle("", "verdana.ttf", 7.0);
        c.xAxis().setLabelStyle("verdana.ttf", 7.0, 0);
        chart.addChart(0, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }

    //protected void btnGerarReport_Click(object sender, EventArgs e)
    //{
    //    MontarTable();
    //}

    //protected void btnPDF_Click(object sender, EventArgs e)
    //{
    //    MontarTable();
    //}
}
    #endregion