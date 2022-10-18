using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
///*u*/sing Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using ChartDirector;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Homes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "TELA INICIAL DO SITE", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
                Timer1.Enabled = true;
                Timer1.Interval = Convert.ToInt32(ConfigurationSettings.AppSettings["IntervaloRefresh"]) * 60000;

                imgHome.Visible = false;
                if (File.Exists(Server.MapPath("imgInicialDicate") + "\\inicial.jpg"))
                {
                    imgHome.Visible = true;
                    imgHome.ImageUrl = "imgInicialDicate\\inicial.jpg";

                    //HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
                    //tr0.Style.Add("display", "none");

                    Panel3.Visible = true;
                    if (ILusuario[0].Login == "DICATE")
                    {
                        imgHome.Attributes.Add("OnClick", "return window.open('imgInicialDicate/Default.aspx' ,'ajuda','width=460,height=500, scrollbars=yes' );");
                    }
                }

                Button5.Attributes.Add("OnClick", "javascript:window.open('frmImportTeste.aspx');");
                                               
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            MontarTableAguardandoAutorizacao();
            MontarTableEnviarParaFaturamento();
            MontarTableEnvidoParaFaturamento();
            lblTempo.Text = "Última Atualização: " + DateTime.Now.ToShortTimeString();
    }
    
    protected void MontarTableAguardandoAutorizacao()
    {
        const string Situacao = "AguardandoClienteAutorizar";
        DataTable dtAgAutorizacao = new Pedido().PedidoPendentes(FuncoesGerais.LoadDataSetConstantes(Situacao));
        PHAutorizacao.Controls.Clear();

        PHAutorizacao.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        
        if (dtAgAutorizacao.Rows.Count > 0)
        {
            PHAutorizacao.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PHAutorizacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;'>Data"));
            PHAutorizacao.Controls.Add(new LiteralControl(@"</td>"));

            PHAutorizacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Pedidos"));
            PHAutorizacao.Controls.Add(new LiteralControl(@"</td>"));
            PHAutorizacao.Controls.Add(new LiteralControl(@"</tr>"));

            int tot = 0;
            foreach (DataRow item in dtAgAutorizacao.Rows)
            {
                PHAutorizacao.Controls.Add(new LiteralControl(@"<tr>"));
                //PHAutorizacao.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:8pt;height:10px'><a class='link'>" + item["DATA"].ToString() + "</a>"));
                PHAutorizacao.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:8pt;height:10px'><a href='ConsultarPedido.aspx?dt=" + Server.UrlEncode(item["DATA"].ToString()) + "&sit=" + FuncoesGerais.LoadDataSetConstantes(Situacao) + "&opc=Consultar Pedidos' class='link'>" + Convert.ToDateTime(item["DATA"]).ToShortDateString() + "</a>"));

                PHAutorizacao.Controls.Add(new LiteralControl(@"</td>"));

                PHAutorizacao.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToDecimal(item["QTD"]).ToString("#0")));
                PHAutorizacao.Controls.Add(new LiteralControl(@"</td>"));
                PHAutorizacao.Controls.Add(new LiteralControl(@"</tr>"));
                tot += Convert.ToInt32(item["QTD"]);
            }
            PHAutorizacao.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PHAutorizacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
            PHAutorizacao.Controls.Add(new LiteralControl(@"</td>"));

            PHAutorizacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + tot));
            PHAutorizacao.Controls.Add(new LiteralControl(@"</td>"));
            PHAutorizacao.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            PHAutorizacao.Controls.Add(new LiteralControl(@"<tr>"));
            PHAutorizacao.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PHAutorizacao.Controls.Add(new LiteralControl(@"</td>"));
            PHAutorizacao.Controls.Add(new LiteralControl(@"</tr>"));

        }
        PHAutorizacao.Controls.Add(new LiteralControl(@"</table>"));
    }

    protected void MontarTableEnviarParaFaturamento()
    {
        const string Situacao = "EnviarPraFaturamento";
        DataTable dtAgAutorizacao = new Pedido().PedidoPendentes(FuncoesGerais.LoadDataSetConstantes(Situacao));
        PHEnviarParaFaturamento.Controls.Clear();

        PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

        if (dtAgAutorizacao.Rows.Count > 0)
        {
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;'>Data"));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));

            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Pedidos"));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</tr>"));

            int tot = 0;
            foreach (DataRow item in dtAgAutorizacao.Rows)
            {
                PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<tr>"));
                //PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:8pt;height:10px'><a class='link'>" + item["DATA"].ToString() + "</a>"));
                PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:8pt;height:10px'><a href='ConsultarPedido.aspx?dt=" + Server.UrlEncode(item["DATA"].ToString()) + "&sit=" + FuncoesGerais.LoadDataSetConstantes(Situacao) + "&opc=Consultar Pedidos' class='link'>" + Convert.ToDateTime(item["DATA"]).ToShortDateString() + "</a>"));

                PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));

                PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToDecimal(item["QTD"]).ToString("#0")));
                PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));
                PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</tr>"));
                tot += Convert.ToInt32(item["QTD"]);
            }
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));

            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + tot));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<tr>"));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));
            PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</tr>"));

        }
        PHEnviarParaFaturamento.Controls.Add(new LiteralControl(@"</table>"));
    }

    protected void MontarTableEnvidoParaFaturamento()
    {
        const string Situacao = "EnviadoPraFaturamento";
        DataTable dtAgAutorizacao = new Pedido().PedidoPendentes(FuncoesGerais.LoadDataSetConstantes(Situacao));
        PHEnviadoParaFaturamento.Controls.Clear();

        PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

        if (dtAgAutorizacao.Rows.Count > 0)
        {
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;'>Data"));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));

            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Pedidos"));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</tr>"));

            int tot = 0;
            foreach (DataRow item in dtAgAutorizacao.Rows)
            {
                PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<tr>"));
                PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:8pt;height:10px'><a href='ConsultarPedido.aspx?dt=" + Server.UrlEncode(item["DATA"].ToString()) + "&sit=" + FuncoesGerais.LoadDataSetConstantes(Situacao) + "&opc=Consultar Pedidos' class='link'>" + Convert.ToDateTime(item["DATA"]).ToShortDateString() + "</a>"));
                PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));

                PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToDecimal(item["QTD"]).ToString("#0")));
                PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));
                PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</tr>"));
                tot += Convert.ToInt32(item["QTD"]);
            }
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));

            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + tot));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<tr>"));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</td>"));
            PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</tr>"));

        }
        PHEnviadoParaFaturamento.Controls.Add(new LiteralControl(@"</table>"));
    }

    int faltamin = Convert.ToInt32( ConfigurationSettings.AppSettings["IntervaloRefresh"]);
   
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        
    }
}