using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Web.UI.HtmlControls;
using ChartDirector;
using System.Linq;

public partial class frmPainelFaturamento : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (!IsPostBack)
            {
                contadorCliente = 0;
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
///               SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
                montarExibicaoMesAtual();

                rmp.TabIndex = 0;
                RadTabStrip1.SelectedIndex = 0;
                rpvUsuarios.Selected = true;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    int contadorCliente = 0;
    private void montarExibicaoMesAtual()
    {
        DataTable dtCliente = SistranBLL.Cliente.RetornarListaClienteFaturamento();

        #region Cabeçalho

        DataTable dtMesesCliente = GerarMesesClientes();
        DataTable dtDiasCliente = GerarDiasClientes();


        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 width='200px'>"));

        //linha de cabeçalho 1
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>código"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>cliente"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



        //calcula as colunas meses
        int mesAtual = DateTime.Now.Month;
        int anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='" + DateTime.Now.Day + "' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'> Mês " + DateTime.Now.Month + " / " + DateTime.Now.Year));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='1' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>total"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        //linha de cabeçalho 2

        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='2' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        for (int i = 1; i <= DateTime.Now.Day; i++)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + i));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Faturado"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion

        #region itens / dias

        foreach (DataRow item in dtCliente.Rows)
        {
            contadorCliente++;

            decimal totvlFaturado = 0;

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["IDCLIENTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["RAZAOSOCIALNOME"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["FILIAL"].ToString()));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            for (int i = 1; i <= DateTime.Now.Day; i++)
            {
                DataRow[] or = dtDiasCliente.Select("dia=" + i + " and idcliente=" + item["idcliente"].ToString() );

                decimal vlFaturado = 0;
                if (or != null && or.Length > 0)
                {
                    vlFaturado = decimal.Parse(dtDiasCliente.Compute("sum(faturado)", "dia=" + i + "   and idcliente=" + item["idcliente"].ToString()).ToString());
                    totvlFaturado += vlFaturado;
                }

                if (vlFaturado > 0)
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>" + vlFaturado.ToString("##,0.00")));
                else
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));


                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            }

            if (totvlFaturado > 0)
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado.ToString("##,0.00") + "</b>"));
            else
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        }

        #endregion


        #region Linha final  Total

        decimal totvlFaturado_ = 0;

        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr  style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='left' style='font-size:7pt;height:10px'><b>Total</b>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap  style='font-size:7pt;height:10px'><b>" + contadorCliente + " </b>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>"));
        //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        decimal tottot = 0;
        for (int i = 1; i <= DateTime.Now.Day; i++)
        {
            totvlFaturado_ = 0;
            DataRow[] or = dtDiasCliente.Select("dia=" + i);

            decimal vlFaturado_ = 0;
            if (or != null && or.Length > 0)
            {
                vlFaturado_ = decimal.Parse(dtDiasCliente.Compute("sum(faturado)", "dia=" + i).ToString());
                totvlFaturado_ += vlFaturado_;
            }

            if (vlFaturado_ > 0)
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado_.ToString("##,0.00") + "</b>"));
            else
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            tottot += totvlFaturado_;

        }

        if (tottot > 0)
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt;'><b>" + tottot.ToString("##,0.00") + "</b>"));
        else
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt;'>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        


        #endregion

        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

        montarExibicaoHistorico(dtMesesCliente, dtDiasCliente);
        montarExibicaoMesAtualPorFilial();

    }

    private void montarExibicaoMesAtualPorFilial()
    {
        DataTable dtCliente = SistranBLL.Cliente.RetornarListaClienteFaturamento(true);

        #region Cabeçalho

        DataTable dtMesesCliente = GerarMesesClientes();
        DataTable dtDiasCliente = GerarDiasClientes();


        phFilial.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 width='200px'>"));

        //linha de cabeçalho 1
        phFilial.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

        phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>código"));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));

        phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>cliente"));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));

        phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>filial"));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));

        //calcula as colunas meses
        int mesAtual = DateTime.Now.Month;
        int anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

        phFilial.Controls.Add(new LiteralControl(@"<td colspan='" + DateTime.Now.Day + "' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'> Mês " + DateTime.Now.Month + " / " + DateTime.Now.Year));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));

        phFilial.Controls.Add(new LiteralControl(@"<td colspan='1' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>total"));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));


        //linha de cabeçalho 2

        phFilial.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

        phFilial.Controls.Add(new LiteralControl(@"<td colspan='3' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>"));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));

        for (int i = 1; i <= DateTime.Now.Day; i++)
        {
            phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + i));
            phFilial.Controls.Add(new LiteralControl(@"</td>"));
        }

        phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Faturado"));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));


        phFilial.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion

        #region itens / dias

        foreach (DataRow item in dtCliente.Rows)
        {
            contadorCliente++;

            decimal totvlFaturado = 0;

            phFilial.Controls.Add(new LiteralControl(@"<tr>"));
            phFilial.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["IDCLIENTE"].ToString()));
            phFilial.Controls.Add(new LiteralControl(@"</td>"));

            phFilial.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["RAZAOSOCIALNOME"].ToString()));
            phFilial.Controls.Add(new LiteralControl(@"</td>"));

            phFilial.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["FILIAL"].ToString()));
            phFilial.Controls.Add(new LiteralControl(@"</td>"));

            for (int i = 1; i <= DateTime.Now.Day; i++)
            {
                DataRow[] or = dtDiasCliente.Select("dia=" + i + " and idcliente=" + item["idcliente"].ToString() + " AND IDFILIAL=" + item["IDFILIAL"].ToString());

                decimal vlFaturado = 0;
                if (or != null && or.Length > 0)
                {
                    vlFaturado = decimal.Parse(dtDiasCliente.Compute("sum(faturado)", "dia=" + i + "   and idcliente=" + item["idcliente"].ToString() + " AND IDFILIAL=" + item["IDFILIAL"].ToString()).ToString());
                    totvlFaturado += vlFaturado;
                }

                if (vlFaturado > 0)
                    phFilial.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>" + vlFaturado.ToString("##,0.00")));
                else
                    phFilial.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));


                phFilial.Controls.Add(new LiteralControl(@"</td>"));

            }

            if (totvlFaturado > 0)
                phFilial.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado.ToString("##,0.00") + "</b>"));
            else
                phFilial.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>"));


            phFilial.Controls.Add(new LiteralControl(@"</td>"));
            phFilial.Controls.Add(new LiteralControl(@"</tr>"));

        }

        #endregion


        #region Linha final  Total

        decimal totvlFaturado_ = 0;

        phFilial.Controls.Add(new LiteralControl(@"<tr  style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='left' style='font-size:7pt;height:10px'><b>Total</b>"));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));

        phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap  style='font-size:7pt;height:10px'><b>" + contadorCliente + " </b>"));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));

        phFilial.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>"));
        phFilial.Controls.Add(new LiteralControl(@"</td>"));

        decimal tottot = 0;
        for (int i = 1; i <= DateTime.Now.Day; i++)
        {
            totvlFaturado_ = 0;
            DataRow[] or = dtDiasCliente.Select("dia=" + i);

            decimal vlFaturado_ = 0;
            if (or != null && or.Length > 0)
            {
                vlFaturado_ = decimal.Parse(dtDiasCliente.Compute("sum(faturado)", "dia=" + i).ToString());
                totvlFaturado_ += vlFaturado_;
            }

            if (vlFaturado_ > 0)
                phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado_.ToString("##,0.00") + "</b>"));
            else
                phFilial.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));


            phFilial.Controls.Add(new LiteralControl(@"</td>"));

            tottot += totvlFaturado_;

        }

        if (tottot > 0)
            phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt;'><b>" + tottot.ToString("##,0.00") + "</b>"));
        else
            phFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt;'>"));


        phFilial.Controls.Add(new LiteralControl(@"</td>"));
        phFilial.Controls.Add(new LiteralControl(@"</tr>"));




        #endregion

        phFilial.Controls.Add(new LiteralControl(@"</table>"));

        //montarExibicaoHistorico(dtMesesCliente, dtDiasCliente);

    }
    
    private void montarExibicaoHistorico(DataTable dtMesesCliente, DataTable dtDiasCliente )
    {
        #region Cabeçalho
        DataTable dtCliente = SistranBLL.Cliente.RetornarListaClienteFaturamento(false);
        DataTable dtEmAbertoAnterior = SistranBLL.Cliente.RetornarEmAbertoAnteriores("");

        PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 width='200px'>"));

        //linha de cabeçalho 1
        PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>código"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>cliente"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>Em Aberto "));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));



        //calcula as colunas meses
        int mesAtual = DateTime.Now.Month;
        int anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

        for (int i = 1; i <= 12; i++)
        {
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='2' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>" + mesAtual + "/" + anoMinimo));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            mesAtual++;

            if (mesAtual > 12)
            {
                mesAtual = 1;
                anoMinimo++;
            }
        }

        PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='2' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>Totais" ));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

        //linha de cabeçalho 2

        PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='2' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


        mesAtual = DateTime.Now.Month;
        anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

        PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='1' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>(Anterior à " + mesAtual + "/" + anoMinimo + ")"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

        // calcula as colunas meses
        mesAtual = DateTime.Now.Month;
        anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

        for (int i = 1; i <= 12; i++)
        {
      

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Faturado"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Aberto"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            mesAtual++;

            if (mesAtual > 12)
            {
                mesAtual = 1;
                anoMinimo++;
            }
        }

        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Faturado"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Aberto"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion

        decimal totAbertoAnteriores = 0;

        foreach (DataRow item in dtCliente.Rows)
        {
            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["IDCLIENTE"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["RAZAOSOCIALNOME"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            decimal abertoAnterioCliente = 0;
            if (dtEmAbertoAnterior.Compute("max(EMABERTO)", "idcliente=" + item["IDCLIENTE"].ToString()) != DBNull.Value)
            {
                abertoAnterioCliente = decimal.Parse((dtEmAbertoAnterior.Compute("max(EMABERTO)", "idcliente=" + item["IDCLIENTE"].ToString()).ToString()));
            }

            if(abertoAnterioCliente >0)
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px; color:red'>" + abertoAnterioCliente.ToString("#,0.00")));
            else
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px; color:red'>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            totAbertoAnteriores += abertoAnterioCliente;


            mesAtual = DateTime.Now.Month;
            anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

            decimal totvlFaturado = 0;
            decimal totvlAberto = 0;

            for (int i = 1; i <= 12; i++)
            {

                DataRow[] or = dtMesesCliente.Select("ano=" + anoMinimo + " and mes=" + mesAtual + " and idcliente=" + item["idcliente"].ToString());

                decimal vlFaturado = 0;
                decimal vlAberto = 0;

                if (or != null && or.Length > 0)
                {

                    vlFaturado = decimal.Parse(dtMesesCliente.Compute("sum(faturado)", "ano=" + anoMinimo + " and mes=" + mesAtual + "   and idcliente=" + item["idcliente"].ToString()).ToString());
                    totvlFaturado += vlFaturado;

                    vlAberto = decimal.Parse(dtMesesCliente.Compute("sum(emAberto)", "ano=" + anoMinimo + " and mes=" + mesAtual + "   and idcliente=" + item["idcliente"].ToString()).ToString());
                    totvlAberto += vlAberto;
                }

                if (vlFaturado > 0)
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>" + vlFaturado.ToString("#,0.00")));
                else
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;"));


                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                if (vlAberto > 0)
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:7pt; color:red'>" + vlAberto.ToString("#,0.00")));
                else
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;"));


                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                mesAtual++;

                if (mesAtual > 12)
                {
                    mesAtual = 1;
                    anoMinimo++;
                }
            }




            if(totvlFaturado>0)
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado.ToString("#,0.00") + "</b>"));
            else
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>" ));

            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            if(totvlAberto>0)
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;color:red'><b>" + totvlAberto.ToString("#,0.00") + "</b>"));
            else
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

        }


        #region Linha Total Final

        PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' >"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>Total"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;color:red'><b>" + (totAbertoAnteriores==0?"": totAbertoAnteriores.ToString("#,0.00")  ) + "</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            mesAtual = DateTime.Now.Month;
            anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

            decimal totvlFaturado_ = 0;
            decimal totvlAberto_ = 0;

            for (int i = 1; i <= 12; i++)
            {

                DataRow[] or = dtMesesCliente.Select("ano=" + anoMinimo + " and mes=" + mesAtual);

                decimal vlFaturado_ = 0;
                decimal vlAberto_ = 0;

                if (or != null && or.Length > 0)
                {

                    vlFaturado_ = decimal.Parse(dtMesesCliente.Compute("sum(faturado)", "ano=" + anoMinimo + " and mes=" + mesAtual).ToString());
                    totvlFaturado_ += vlFaturado_;

                    vlAberto_ = decimal.Parse(dtMesesCliente.Compute("sum(emAberto)", "ano=" + anoMinimo + " and mes=" + mesAtual ).ToString());
                    totvlAberto_ += vlAberto_;
                }

                if (vlFaturado_ > 0)
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'><b>" + vlFaturado_.ToString("#,0.00") + "</b>"));
                else
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;"));


                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

               // vlAberto_ += totAbertoAnteriores;

                if (vlAberto_ > 0)
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt; color:red;'><b>" + vlAberto_.ToString("#,0.00") + "</b>"));
                else
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;"));


                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                mesAtual++;

                if (mesAtual > 12)
                {
                    mesAtual = 1;
                    anoMinimo++;
                }
            }

            if(totvlFaturado_>0)
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado_.ToString("#,0.00") + "</b>"));
            else
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            if (totvlAberto_ > 0)
            {
                //PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt; color:red'><b>" + totvlAberto_.ToString("#,0.00") + "</b>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt; color:red'><b>" + (decimal.Parse(dtMesesCliente.Compute("sum(emAberto)", "").ToString()) + totAbertoAnteriores).ToString("#,0.00") + "</b>"));

               
            }
            else
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));


        #endregion

        PlaceHolder2.Controls.Add(new LiteralControl(@"</table>"));

    }

    protected DataTable GerarMesesClientes()
    {
        return SistranBLL.Cliente.GerarMesesClienteFaturamento();
    }

    protected DataTable GerarDiasClientes()
    {
        return SistranBLL.Cliente.GerarDiasClienteFaturamento();

    }
}