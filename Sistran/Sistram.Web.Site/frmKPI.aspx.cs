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

public partial class frmKPI : System.Web.UI.Page
{
    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = FuncoesGerais.RetornarIntervaloDiasPesqusa();
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
                CarregarCboLinha();
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;


        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");

        }
    }

    private void CarregarCboLinha()
    {
        cboTipoRem = new SistranBLL.Produto.GrupoProduto().ListarGrupoDeProdutos(cboTipoRem);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MontarTable();
    }

    protected void MontarTable()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

        decimal total = Convert.ToDecimal(0);
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
        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=2 celpanding=4 width='99%'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center'   style='font-size:7pt;' COLSPAN='5' >IDENTIFICAÇÃO</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center'   style='font-size:7pt;' COLSPAN='2' >META</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='rigth'   style='font-size:7pt;'></td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='CENTER' style='font-size:7pt;width:2%'>&nbsp;&nbsp;&nbsp;ITEM&nbsp;&nbsp;&nbsp;"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;' nowrap=nowrap>SUB PROCESSO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  style='font-size:7pt;width:20%'>INDICADOR"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;width:1%'>HELP"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'   style='font-size:7pt;'>FORMULA"));
        //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'    style='font-size:7pt;'>UNIDADE DE MEDIDA"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'    style='font-size:7pt;width:80px'>VALOR"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'    style='font-size:7pt;'>GATILHO PARA DISPARAR AÇÕES"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'    style='font-size:7pt;'>RESULTADO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        for (int i = 1; i <= 17; i++)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>" + i.ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            switch (i)
            {
                case 1:
                    MontarLinhaTB01(i.ToString());
                    break;

                case 2:

                    MontarLinhaTB02(i.ToString());
                    break;

                case 3:
                     MontarLinhaTB03(i.ToString());
                    break;

                case 4:
                   MontarLinhaTB04(i.ToString());
                   break;

                case 5:
                   MontarLinhaTB05(i.ToString());
                    break;

                case 6:
                    MontarLinhaTB06(i.ToString());
                    break;

                case 7:
                    MontarLinhaTB07(i.ToString());
                    break;

                case 8:
                    MontarLinhaTB08(i.ToString());
                    break;

                case 9:
                    MontarLinhaTB09(i.ToString());
                    break;

                case 10:
                    MontarLinhaTB10(i.ToString());
                    break;

                case 11:
                    MontarLinhaTB11(i.ToString());
                    break;

                case 12:
                    MontarLinhaTB12(i.ToString());
                    break;

                case 13:
                    MontarLinhaTB13(i.ToString());
                    break;

                case 14:
                    MontarLinhaTB14(i.ToString());
                    break;

                case 15:
                    MontarLinhaTB15(i.ToString());
                    break;

                case 16:
                    MontarLinhaTB16(i.ToString());
                    break;

                case 17:
                    MontarLinhaTB17(i.ToString());
                    break;
            }
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
    }

    private void MontarLinhaTB17(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det17.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>RECEBIMENTO</A>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Inbound Accuracy"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar o % de acurracidade acumulada de auditorias de cargas",
                                                    "FINALIDADE ITEM: " + p.ToString(), "(ƩQtde. de linhas corretas / ƩQtde. de linhas recebidas) * 100%", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99,8%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99,7%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha17(ref select) +"%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
      

        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB16(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det16.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>EXPEDIÇÃO</A>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Outbound Accuracy"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar o % de acurracidade acumulada de auditorias de cargas",
                                                    "FINALIDADE ITEM: " + p.ToString(), "(ƩQtde. de linhas corretas / ƩQtde. de linhas solicitados) * 100%", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99,8%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99,7%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha16(ref select)+ "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
       

        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB15(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det15.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>EXPEDIÇÃO</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Aguardando agendamento"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensura qtde de volumes aguardando agendamento para entrega",
                                                    "FINALIDADE ITEM: " + p.ToString(), "ƩQtde. de volumes aguardando agendamento", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>0"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>N/A"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>N/A"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha15(ref select)));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

       
        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB14(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det14.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>EXPEDIÇÃO</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Picking Accuracy"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mesurar  % de qualidade em atendimento aos clientes",
                                                    "FINALIDADE ITEM: " + p.ToString(), "Qtde de pedidos reclamados / qtde de pedidos expedidos", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>98,7%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha14(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        

        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB13(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det13.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>EXPEDIÇÃO</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Outbound Leadtime"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mesurar o tempo de permanencia dos pedidos em doca",
                                                    "FINALIDADE ITEM: " + p.ToString(), "(Qtde. de pedidos embarcados - qtde de pedidos agendados) / qtde de pedidos enviados via interface", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>0"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>98%" ));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>97,9%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha13(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

       
        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB12(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det12.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>CONFERÊNCIA</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Lines TO BE Processed"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar total de linhas pendentes de envio Interface", "FINALIDADE ITEM: " + p.ToString(), "ƩQtde. de linhas atendidas - ƩQtde. de linhas respondidas via interface", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>0"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>N/A"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>N/A"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha12(ref select)));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB11(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'  nowrap='nowrap'  style='font-size:7pt;height:10px'><a class='link' href='kpi/det11.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>GESTÃO DE ESTOQUE</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>RO gerado no estoque"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar a qtde de avarias causadas nos produtos durante a armazenagem", "FINALIDADE ITEM: " + p.ToString(), "Qtde de RO's", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>0,03%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>0,05%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha11(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
             

        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB10(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap='nowrap'   style='font-size:7pt;height:10px'><a class='link' href='kpi/det10.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>GESTÃO DE ESTOQUE</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Giro ABCD"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar o % de acurracidade da classificação ABCD do produto", "FINALIDADE ITEM: " + p.ToString(), "(ƩQtde. de posições corretas / ƩQtde. de posições contadas) * 100%", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>98,5%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha10(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        
        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB09(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det09.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>GESTÃO DE ESTOQUE</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Stock Accuracy"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar o % de acurracidade da quantidade na posição", "FINALIDADE ITEM: " + p.ToString(), "(ƩQtde. de peças corretas / ƩQtde. de peças do estoque Irwin) * 100%", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99,8%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99,5%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha9(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

     

        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB08(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det08.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>GESTÃO DE ESTOQUE</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Stock Location accuracy"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar o % de acurracidade acumulada do estoque", "FINALIDADE ITEM: " + p.ToString(), "(ƩQtde. de posições corretas / ƩQtde. de posições contadas) * 100%", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>98,5%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha8(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

           CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB07(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det07.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>GESTÃO DE ESTOQUE</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Ocupação do Estoque"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar o % de Ocupação do Estoque disponível por linha de produto", "FINALIDADE ITEM: " + p.ToString(), "(Qtde. de posições ocupadas / Qtde. de posições disponíveis) * 100%", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>80%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>85%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha7(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

      
        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB06(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det06.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>GESTÃO DE ESTOQUE</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Lines in Short Pick"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar o % de linhas não atendidas pelo Sistema", "FINALIDADE ITEM: " + p.ToString(), "ƩQtde. de linhas não atendidas / Linhas Solicitadas ", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>0"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99,7%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha6(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

       
        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB05(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:7pt;height:10px' nowrap='nowrap'><a class='link' href='kpi/det05.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>GESTÃO DE ESTOQUE</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Actual Lines Invoiced"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'  style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p.ToString(), "Mensurar o % de linhas atendidas", "FINALIDADE ITEM: " + p.ToString(), "ƩQtde. de linhas atendidas/ Linhas Solicitadas", "FORMULA ITEM: " + p.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>0"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99,7%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha5(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

       
        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB04(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det04.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>SEPARAÇÃO</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Actual Lines Interfaced"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'    style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p, "Mensurar Número de linhas conferidas enviadas pela interface para atendimento", "FINALIDADE ITEM: " + p, "(Qtde. de linhas conferidas / Qtde. de linhas atendidas) * 100%", "FORMULA ITEM: " + p)));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99,7%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha4(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

          CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB03(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det03.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>RECEBIMENTO</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Putaway pendente"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'    style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p, "Mensurar qtde de UA pendente de armazenamento", "FINALIDADE ITEM: " +p, "Qtde de UA em status de recebimento ", "FORMULA ITEM: " + p)));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>0"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>N/A"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>N/A"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha3(ref select)));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

      

        CriarBotaoSql(p, select);
    }

    private void MontarLinhaTB02(string p)
    {
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'    style='font-size:7pt;height:10px'><a class='link' href='kpi/det02.aspx?ini=" + txtI.Text + "&fim=" + txtF.Text + "' target='_blank'>SEPARAÇÃO</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'   style='font-size:7pt;height:10px'>Fill Rate"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'    style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p, "Mensurar a qtde de pedidos que sofreram corte de itens", "FINALIDADE ITEM: " + p, "(Qtde de linhas atendidas / qtde de linhas solicitadas)*100", "FORMULA ITEM: " + p)));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>99.9%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>98,7%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        string select = "";
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>" + CalcularLinha2(ref select) + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        CriarBotaoSql("02", select);

    }
    string select = "";
    private void MontarLinhaTB01( string p)
    {        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:7pt;height:10px'><a class='link' href='kpi/det01.aspx?ini="+txtI.Text+"&fim="+ txtF.Text+"' target='_blank'>RECEBIMENTO</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'  style='font-size:7pt;height:10px' nowrap=nowrap>Tempo médio gasto no recebimento &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'    style='font-size:7pt;height:10px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(p, "Mensurar a qtde de pedidos que sofreram corte de itens", "FINALIDADE ITEM: " + p, "(Qtde de linhas atendidas / qtde de linhas solicitadas)", "FORMULA ITEM: " + p)));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' style='font-size:7pt;height:10px'>HORAS"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' style='font-size:7pt;height:10px'>5 horas"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'    style='font-size:7pt;height:10px'>5,5 horas"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' style='font-size:7pt;height:10px'>" + CalcularLinha1(ref select) + " horas"));       
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));        
       
        CriarBotaoSql("01", select);
       
    }

    private string CalcularLinha1(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form01((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }
    
    decimal ItensCancelados;
    private string CalcularLinha2(ref string select)
    {
        ItensCancelados = new SistranBLL.KPI().Form06((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        decimal LinhasAtendidas = new SistranBLL.KPI().Form02((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);

        decimal LinhasSolicitadas = ItensCancelados + LinhasAtendidas;

        if (LinhasSolicitadas > 0)
        {
            return Convert.ToDecimal((LinhasAtendidas / LinhasSolicitadas) * 100).ToString("#0.00"); ;
        }
        else
            return Convert.ToDecimal(0).ToString("#0.00");

       
    }

    private string CalcularLinha3(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form03((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0");
    }

    private string CalcularLinha4(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form04((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }

    decimal caclTemp = Convert.ToDecimal(0);
    private string CalcularLinha5(ref string select)
    {
        caclTemp = new SistranBLL.KPI().Form05((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
       decimal m = (caclTemp / (caclTemp + ItensCancelados)) * 100;
       return m.ToString("#0.00");
    }

    private string CalcularLinha6(ref string select)
    {
        decimal m = (ItensCancelados / (caclTemp + ItensCancelados)) * 100;
        return m.ToString("#0.00");        
    }

    private string CalcularLinha7(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form07((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }

    private string CalcularLinha8(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form08((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }

    private string CalcularLinha9(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form09((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }

    private string CalcularLinha10(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form10((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }

    private string CalcularLinha11(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form11((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }

    private string CalcularLinha12(ref string select)
    {
        decimal LinhasAtendidas = new SistranBLL.KPI().Form02((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);

        decimal m = new SistranBLL.KPI().Form12((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);

        return (LinhasAtendidas - m).ToString("#0");
    }

    private string CalcularLinha13(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form13((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }

    private string CalcularLinha14(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form14((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }

    private string CalcularLinha16(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form16((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
        
    }

    private string CalcularLinha15(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form15((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0");
    }

    private string CalcularLinha17(ref string select)
    {
        decimal m = new SistranBLL.KPI().Form17((cboTipoRem.SelectedIndex == 0 ? "Todos" : cboTipoRem.SelectedValue), txtI.Text, txtF.Text, ref select);
        return m.ToString("#0.00");
    }

    public string CriarBotaoExpandir(string Linha, string Texto, string Titulo, string Texto2, string Titulo2)
    {
        string m = "";
        m += "<div style='font-size:11px;cursor:Hand;background-image:url(ajuda.jpg); height:12px; width:14px;' OnClick=Expandir('" + Linha + "');>";
        m +="</div>";
        m += "<div id='" + Linha + "' style='position: absolute; top: 30%; left: 45%; text-align: center; display: none; width:300px; border-color:Silver; border-style:solid; border-width:1px;display:none'>";
        m += "<TABLE border='0' cellspacing='2' cellpading='2' style='background-color:#FFFFDD' width=100%>";
        m += "<TR>";
        m += "<TD><b><center>" + Titulo;
        m += "</center></b></TD>";
        m += "</TR>";
        m += "<TR>";
        m += "<TD>";
        m += "<hr></TD>";
        m += "</TR>";
        m += "<TR>";
        m += "<TD>" + Texto;
        m += "</TD>";
        m += "</TR>";
        m += "<TR>";
        m += "<TD>";
        m += "<hr></TD>";
        m += "</TR>";
        m += "</TABLE>";
        m += "<TABLE border='0' cellspacing='2' cellpading='2' style='background-color:#FFFFDD' width=100%>";
        m += "<TR>";
        m += "<TD><b><center>" + Titulo2;
        m += "</center></b></TD>";
        m += "</TR>";
        m += "<TR>";
        m += "<TD>";
        m += "<hr></TD>";
        m += "</TR>";
        m += "<TR>";
        m += "<TD>" + Texto2;
        m += "</TD>";
        m += "</TR>";
        m += "<TR>";
        m += "<TD>";
        m += "<hr></TD>";
        m += "</TR>";
        m += "<TR style='height:20px;text-align:right'>";
        m += "<TD style='height:20px;text-align:right'><a class='button' style='heigth:19px; font-size:9px' href='#'  OnClick=Expandir('" + Linha + "');>Fechar [X]</a>";
        m += "</TD>";
        m += "</TR>";
        m += "</TABLE>";
        m += "</div>";
        return m;
    }


    public void CriarBotaoSql(string Linha, string Texto )
    {
        string m = "";
        if (HttpContext.Current.Request.UserHostAddress.Contains("192.168.10.") || HttpContext.Current.Request.UserHostAddress == "127.0.0.1")
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' style='height:10px;text-align:center'>"));

            m += "<div style='font-size:11px;cursor:Hand; background-image:url(ajuda.jpg);height:12px; width:14px;' OnClick=Expandir('sql" + Linha + "');>..";
            m += "</div>";
            m += "<div id='sql" + Linha + "' style='position: absolute; top: 5%; left: 10%; text-align: center; display: none; width:450px; border-color:Silver; border-style:solid; border-width:1px;display:none'>";
            m += "<TABLE border='0' cellspacing='2' cellpading='2' style='background-color:#FFFFDD' width=100%>";
            m += "<TR>";
            m += "<TD>" + Texto;
            m += "</TD>";
            m += "</TR>";
            m += "<TR>";
            m += "<TD>";
            m += "<hr></TD>";
            m += "</TR>";
            m += "<TR style='height:20px;text-align:right'>";
            m += "<TD style='height:20px;text-align:right'><a class='button' style='heigth:19px; font-size:9px' href='#'  OnClick=Expandir('sql" + Linha + "');>Fechar [X]</a>";
            m += "</TD>";
            m += "</TR>";
            m += "</TABLE>";
            m += "</div>";

            PlaceHolder1.Controls.Add(new LiteralControl(m));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }
   }
}