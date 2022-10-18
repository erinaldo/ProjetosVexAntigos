using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using ChartDirector;
using System.Web.UI.WebControls;

public partial class FRMMVPROCTERQUASAR : System.Web.UI.Page
{

    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
        ChartDirector.WebChartViewer.OnPageInit(Page);
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = 365;
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                btnGerarReport.Visible = false;
                btnPDF.Visible = false;
                btnGerarReport.Attributes.Add("onClick", "FullWindow('frmRptDesempenhoFilial.aspx?n=" + Guid.NewGuid() + "&tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela2', 'yes')");
                btnPDF.Attributes.Add("onClick", "FullWindow('frmRptDesempenhoFilial.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");

                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
            return;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MontarTable();
    }
    
    protected void MontarTable()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
        
        TimeSpan ts = Convert.ToDateTime(txtF.Text) - Convert.ToDateTime(txtI.Text);
        if (Convert.ToDateTime(txtF.Text) < Convert.ToDateTime(txtI.Text))
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A data inicial não pode ser maior que a data final.')", true);
            return;
        }

        if (ts.Days > intervalo)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + intervalo.ToString() + " dias.')", true);
            return;
        }

        DataTable dt = new SistranBLL.ProcterQuasar().RerotnarLista(DateTime.Parse(txtI.Text), DateTime.Parse(txtF.Text));
        
        RadGrid16.DataSource = dt;
        RadGrid16.DataBind();
        
        #region Place Holder
        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=99%  runat='server' >"));

        #region cabecalho
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>CNPJ REMETENTE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>REMETENTE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>UF"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>CIDADE REMETENTE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>VOLUMES"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>PESO BRUTO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>VALOR DA NOTA"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>QUANTIDADE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>FRETE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>FATURADO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>NÃO FATURADO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>PAGO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>EM ABERTO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        #endregion
        
        #region itens

        foreach (DataRow item in dt.Rows)
        {


            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr >"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'><a target='_blank' class='link' href='FRMMVPROCTERQUASARDetalhe.aspx?opc=Mov. Procter Quasar &cnpj=" + item["CNPJREMETENTE"].ToString() + "'>" + item["CNPJREMETENTE"].ToString() + "</A>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["NOMEREMETENTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["UFREMETENTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["CIDADEREMETENTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + item["VOLUMES"].ToString().ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["PESOBRUTO"].ToString()).ToString("#,0.000"))); //item["PESOBRUTO"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["VALORDANOTA"].ToString()).ToString("#,0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["QUANTIDADEDENOTAS"].ToString()).ToString("#0"))); //item["QUANTIDADEDENOTAS"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["FRETE"].ToString()).ToString("#,0.00"))); //item["FRETE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["FATURADO"].ToString()).ToString("#,0.00"))); //item["FATURADO"].ToString()));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["FATURADO"].ToString()).ToString("###,###.##"))); //item["FATURADO"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["NAOFATURADO"].ToString()).ToString("#,0.00"))); //item["NAOFATURADO"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["PAGO"].ToString()).ToString("#,0.00"))); //item["PAGO"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["ABERTO"].ToString()).ToString("#,0.00"))); //item["ABERTO"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        }
        #endregion

        #region rodape
        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(dt.Compute("SUM(VOLUMES)", "")).ToString("#0")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(dt.Compute("SUM(PESOBRUTO)", "")).ToString("#000,0.000")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(dt.Compute("SUM(VALORDANOTA)", "")).ToString("#000,0.00")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(dt.Compute("SUM(QUANTIDADEDENOTAS)", "")).ToString("#0")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(dt.Compute("SUM(FRETE)", "")).ToString("#000,0.00")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(dt.Compute("SUM(faturado)", "")).ToString("#000,0.00")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(dt.Compute("SUM(naofaturado)", "")).ToString("#000,0.00")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(dt.Compute("SUM(pago)", "")).ToString("#000,0.00")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(dt.Compute("SUM(aberto)", "")).ToString("#000,0.00")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion
        
        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

        #endregion
        
    }

    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        
    }

    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        MontarTable();
    }
}
