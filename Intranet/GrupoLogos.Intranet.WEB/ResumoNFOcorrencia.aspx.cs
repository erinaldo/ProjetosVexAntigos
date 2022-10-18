using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
//using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using ChartDirector;

public partial class ResumoNFOcorrencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ChartDirector.WebChartViewer.OnPageInit(Page);
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (!IsPostBack)
            {
                RadTabStrip1.SelectedIndex = 0;
                rmp.PageViews[1].Selected = true;
                rmp.PageViews[0].Selected = true;


                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                btnGerarReport.Visible = false;
                btnPDF.Visible = false;
                btnGerarReport.Attributes.Add("onClick", "FullWindow('frmRptResumoOcorrencia.aspx?n=" + Guid.NewGuid() + "&tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela2', 'yes')");
                btnPDF.Attributes.Add("onClick", "FullWindow('frmRptResumoOcorrencia.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
                //Carregar();
                RadTabStrip1.Visible = false;
                rpw1.Visible = false;
                rpw2.Visible = false;

                RadioButtonList1.SelectedIndex = 0;
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

    private void MontarTableOcorrenciaPorFlilial()
    {
        DataTable dtOcoFilial = NotasFiscais.ListarHomeNotasFiscaisComOcorrenciasFilial(Sistran.Library.FuncoesUteis.retornarClientes(), Session["Conn"].ToString(), Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), (RadioButtonList1.SelectedIndex == 1 ? true : false), false);
        PHOcorrenciaPorFilial.Controls.Clear();

        PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));
        if (dtOcoFilial.Rows.Count > 0)
        {
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7.5pt;'>Filial"));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</td>"));

            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7.5pt;'>N.F."));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</td>"));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</tr>"));

            int tot = 0;
            foreach (DataRow item in dtOcoFilial.Rows)
            {
                PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<tr>"));
                PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:7.5pt;height:10px' align='left'>" + item["NOME"].ToString()));
                PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</td>"));

                PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7.5pt;height:10px'>" + Convert.ToDecimal(item["Notas"]).ToString("#0")));
                PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</td>"));
                PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</tr>"));
                tot += Convert.ToInt32(item["Notas"]);
            }
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7.5pt;'>Total:"));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</td>"));

            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7.5pt;'>" + tot));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</td>"));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<tr>"));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</td>"));
            PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</tr>"));

        }
        PHOcorrenciaPorFilial.Controls.Add(new LiteralControl(@"</table>"));

        if (dtOcoFilial.Rows.Count > 0)
        {
            Session["dtOcoFilial"] = dtOcoFilial;
            pnlGrafFilial.Controls.Add(cGraficos.GerarGraficosGeralFilial(dtOcoFilial));
        }
    }

    private void MontarTableComOcorrenciasResponsabilidades()
    {
        DataTable dtResp = NotasFiscais.ListarHomeNotasFiscaisComOcorrenciasResponsabilidade(Sistran.Library.FuncoesUteis.retornarClientes(), Session["Conn"].ToString(), Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), (RadioButtonList1.SelectedIndex == 1 ? true : false), false);
        phResponsabulidadeCliente.Controls.Clear();
        phResponsabulidadeTransporte.Controls.Clear();

        phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));
        phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));
        if (dtResp.Rows.Count > 0)
        {
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7.5pt;Width=1%'>Ocorrência"));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</td>"));

            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7.5pt;Width=1%'>N.F."));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</td>"));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</tr>"));


            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7.5pt;'>Ocorrência"));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</td>"));

            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7.5pt;'>N.F."));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</td>"));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</tr>"));


            int totCliente = 0;
            int totTransporte = 0;
            foreach (DataRow item in dtResp.Rows)
            {
                if (item["RESPONSABILIDADE"].ToString().ToUpper() == "CLIENTE")
                {
                    phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<tr>"));
                    phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap  style='font-size:7.5pt;height:10px' align='left'><a href='NotasFiscaisOcorrenciaFiltro.aspx?idOcorrencia=" + Server.UrlEncode(item["Codigo"].ToString().Trim()) + "' class='link'>" + item["Codigo"].ToString() + "-" + item["Nome"].ToString() + "</a>"));
                    phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</td>"));

                    phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7.5pt;height:10px'>" + Convert.ToDecimal(item["Notas"]).ToString("#0")));
                    phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</td>"));
                    phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</tr>"));
                    totCliente += Convert.ToInt32(item["Notas"]);

                }
                else
                {
                    phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<tr>"));
                    phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap  style='font-size:7.5pt;height:10px' align='left'><a href='NotasFiscaisOcorrenciaFiltro.aspx?idOcorrencia=" + Server.UrlEncode(item["Codigo"].ToString().Trim()) + "' class='link'>" + item["Codigo"].ToString() + "-" + item["Nome"].ToString() + "</a>"));
                    phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</td>"));

                    phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7.5pt;height:10px'>" + Convert.ToDecimal(item["Notas"]).ToString("#0")));
                    phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</td>"));
                    phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</tr>"));
                    totTransporte += Convert.ToInt32(item["Notas"]);
                }
            }
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7.5pt;'>Total:"));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</td>"));

            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7.5pt;'>" + totCliente));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</td>"));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</tr>"));


            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7.5pt;'>Total:"));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</td>"));

            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7.5pt;'>" + totTransporte));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</td>"));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<tr>"));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</td>"));
            phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</tr>"));

            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<tr>"));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</td>"));
            phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</tr>"));
        }
        phResponsabulidadeCliente.Controls.Add(new LiteralControl(@"</table>"));
        phResponsabulidadeTransporte.Controls.Add(new LiteralControl(@"</table>"));

        if (dtResp.Rows.Count > 0)
        {
            pnlGrafRespCliente.Controls.Add(cGraficos.GraficoGeralResponsabilidade(dtResp, "CLIENTE"));
            pnlGrafRespTransporte.Controls.Add(cGraficos.GraficoGeralResponsabilidade(dtResp, "TRANSPORTE"));

        }
        Session["dtResp"] = dtResp;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Carregar();
    }

    private void Carregar()
    {
        MontarTableComOcorrenciasGeral();
        MontarTableComOcorrenciasResponsabilidades();
        MontarTableOcorrenciaPorFlilial();

        DataTable dtRespFiliais = SistranBLL.NotasFiscais.ListarHomeNotasFiscaisComOcorrenciasFilialResponsaveis(Sistran.Library.FuncoesUteis.retornarClientes(), "", Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), (RadioButtonList1.SelectedIndex == 1 ? true : false), false);
        Session["dtRespFiliais"] = dtRespFiliais;
        if (dtRespFiliais.Rows.Count > 0)
        {
            pnlGrafRespFilialCliente.Controls.Add(cGraficos.MontarGraficoFilialResponsavel(dtRespFiliais, "CLIENTE"));
            pnlGrafRespFilialTransporte.Controls.Add(cGraficos.MontarGraficoFilialResponsavel(dtRespFiliais, "TRANSPORTE"));

        }

        RadTabStrip1.Visible = true;
        rpw1.Visible = true;
        rpw2.Visible = true;
    }

    protected void MontarTableComOcorrenciasGeral()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

        DataTable dtOcoGeral = NotasFiscais.ListarHomeNotasFiscaisComOcorrencias(Sistran.Library.FuncoesUteis.retornarClientes(), Session["Conn"].ToString(), Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), (RadioButtonList1.SelectedIndex == 1 ? true : false), false);
        PlaceHolder3.Controls.Clear();

        PlaceHolder3.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1  >"));
        if (dtOcoGeral.Rows.Count > 0)
        {
            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;'>Ocorrência"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>N.F."));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));

            int tot = 0;
            foreach (DataRow item in dtOcoGeral.Rows)
            {
                PlaceHolder3.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap  style='font-size:8pt;height:10px' align='left'><a href='NotasFiscaisOcorrenciaFiltro.aspx?idOcorrencia=" + Server.UrlEncode(item["Codigo"].ToString().Trim()) + "' class='link'>" + item["Codigo"].ToString() + "-" + item["Nome"].ToString() + "</a>"));
                PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToDecimal(item["Notas"]).ToString("#0")));
                PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));
                tot += Convert.ToInt32(item["Notas"]);
            }
            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + tot));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));

        }
        PlaceHolder3.Controls.Add(new LiteralControl(@"</table>"));

        if (dtOcoGeral.Rows.Count > 0)
        {
            btnGerarReport.Visible = true;
            btnPDF.Visible = true;
            Session["dtOcoGeral"] = dtOcoGeral;
        }

        if (dtOcoGeral.Rows.Count > 0)
        {
            pnlGrafAcum.Controls.Add( cGraficos.GerarGraficosGeral(dtOcoGeral));            
        }
    }

    protected void btnGerarReport_Click(object sender, EventArgs e)
    {
        MontarTableComOcorrenciasGeral();
        MontarTableComOcorrenciasResponsabilidades();
        MontarTableOcorrenciaPorFlilial();
        DataTable dts = SistranBLL.NotasFiscais.ListarHomeNotasFiscaisComOcorrenciasFilialResponsaveis(Sistran.Library.FuncoesUteis.retornarClientes(), "", Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), (RadioButtonList1.SelectedIndex == 1 ? true : false),false);
        if (dts.Rows.Count > 0)
        {
            pnlGrafRespFilialCliente.Controls.Add(cGraficos.MontarGraficoFilialResponsavel(dts, "CLIENTE"));
            pnlGrafRespFilialTransporte.Controls.Add(cGraficos.MontarGraficoFilialResponsavel(dts, "TRANSPORTE"));
        }
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        MontarTableComOcorrenciasGeral();
        MontarTableComOcorrenciasResponsabilidades();
        MontarTableOcorrenciaPorFlilial();
        DataTable dtRespFiliais = SistranBLL.NotasFiscais.ListarHomeNotasFiscaisComOcorrenciasFilialResponsaveis(Sistran.Library.FuncoesUteis.retornarClientes(), "", Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), (RadioButtonList1.SelectedIndex == 1 ? true : false),false);
        Session["dtRespFiliais"] = dtRespFiliais;
        if (dtRespFiliais.Rows.Count > 0)
        {
            pnlGrafRespFilialCliente.Controls.Add(cGraficos.MontarGraficoFilialResponsavel(dtRespFiliais, "CLIENTE"));
            pnlGrafRespFilialTransporte.Controls.Add(cGraficos.MontarGraficoFilialResponsavel(dtRespFiliais, "TRANSPORTE"));
        }

    }    

}