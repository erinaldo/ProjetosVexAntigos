using System;
using Sistran;
using System.Data;
using System.Threading;
using System.Globalization;
using SistranBLL;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;

public partial class CotacaoFornecedor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (Request.QueryString["i"] == null)
        {
            dvErro.Visible = true;
            lblMensagem.Text = "COTAÇÃO NÃO ENCONTRADA.";
            Panel1.Enabled = false;
            Panel1.Visible = false;
            btnVoltarDv.Focus();
            btnSairDv.Visible = true;
            return;
        }

        carregar();
        btnsAIR.Attributes.Add("onClick", "javascript:confirmarFechamnento();");

        lblTotal.Visible = false;
    }

    private void carregar()
    {
        try
        {


            DataTable dt;

            if (Session["dtContacao"] == null)
                dt = Retornar();
            else
                dt = (DataTable)Session["dtContacao"];


            Session["dtContacao"] = dt;

            txtI.Text = DateTime.Now.Date.ToShortDateString();
            if (dt.Rows.Count == 0)
            {
                dvErro.Visible = true;
                lblMensagem.Text = "COTAÇÃO NÃO ENCONTRADA.";
                Panel1.Visible = false;
                btnVoltarDv.Focus();
                btnSairDv.Visible = true;
                return;

            }   //Response.Redirect("http://www.grupologos.com.br");



            Label lblUserName = (Label)Master.FindControl("lblUserName");
            lblUserName.Text = "Bem Vindo: " + dt.Rows[0]["FORNECEDOR"].ToString();

            if (Request.QueryString["bl"] != null)
            {
                lblUserName.Text = "Fornecedor: " + dt.Rows[0]["FORNECEDOR"].ToString();
                txtResponsavel.Enabled = false;
                cboParcelas.Enabled = false;
                txtI.Enabled = false;
            }




            if (!IsPostBack)
            {
                if (dt.Rows[0]["DATADEENTREGA"].ToString() != "")
                    txtI.Text = Convert.ToDateTime(dt.Rows[0]["DATADEENTREGA"].ToString()).ToShortDateString();
                else
                    txtI.Text = DateTime.Now.ToShortDateString();

                txtResponsavel.Text = dt.Rows[0]["RESPONSAVEL"].ToString();


            }



            if (dt.Rows[0]["ConcluidoSite"].ToString() == "SIM" || Request.QueryString["bl"] != null)
            {
                btnConfirmar.Text = "Cotação já Efetuada";
                btnConfirmar.Width = 300;
                btnConfirmar.Enabled = false;




                dvErro.Visible = true;
                lblMensagem.Text = "Cotação enviada em: " + Convert.ToDateTime(dt.Rows[0]["DATADEENTREGA"].ToString()).ToShortDateString() + ". <BR> Indisponivel para alteração. ";
                btnVoltarDv.Visible = true;
                btnVoltarDv.Text = "OK";
                Button1.Visible = false;
                Panel1.Enabled = false;
                LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
                lnkLogout.Focus();

            }

            PlaceHolder1.Controls.Clear();
            PlaceHolder2.Controls.Clear();
            PlaceHolder3.Controls.Clear();
            phParcelas.Controls.Clear();

            PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

            #region Solicitante

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='5' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DADOS DO SOLICITANTE"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>EMPRESA:"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["SOLICTANTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CNPJ: "));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["CNPJSOLICITANTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));



            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>ENDEREÇO:"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["ENDERECOSOLICITANTE"].ToString() + ", " + dt.Rows[0]["NUMEROSOLICITANTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CIDADE: "));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["CIDADESOLICITANTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>ESTADO:"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CEP: "));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["CEPSOLICITANTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='5' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'><HR>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
            #endregion

            #region Fornecedor

            PlaceHolder3.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td colspan='5' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DADOS DO FORNECEDOR"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>EMPRESA:"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["FORNECEDOR"].ToString()));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CNPJ: "));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["CNPJSFORNECEDOR"].ToString()));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));



            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>ENDEREÇO:"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["ENDERECFORNECEDOR"].ToString() + ", " + dt.Rows[0]["NUMEROFORNECEDOR"].ToString()));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CIDADE: "));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["CIDADEFORNECEDOR"].ToString()));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));


            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>ESTADO:"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CEP: "));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + dt.Rows[0]["CEPFORNECEDOR"].ToString()));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));


            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td colspan='5' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'><HR>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"</table>"));
            #endregion

            #region itens

            PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='12' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'><HR>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='12' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>ITENS (Informe os valores e clique em calcular)"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));



            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>CÓDIGO <br> PRODUTO</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>DESCRIÇÃO</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>UNIDADE <br> MEDIDA</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>QUANTIDADE</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>VALOR <br>UNITÁRIO</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>TOTAL <br>ITEM</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>% IPI</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>IPI</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>% ICMS</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>ICMS</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>&nbsp;&nbsp;&nbsp;<b>TOTAL</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' class='tdpCabecalhoMenor' align='CENTER' nowrap=nowrap style='font-size:8pt;'><b>OBSERVAÇÃO (MAX. 300 CARACTERES)</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

            decimal totlinhaValorProdutoAcumula = 0;
            decimal totlinhaValorICMSAcumula = 0;
            decimal totlinhaValorIPIAcumula = 0;
            decimal tottalGeral = 0;

            foreach (DataRow rw in dt.Rows)
            {
                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + rw["CODIGO"].ToString()));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                string desc = rw["DESCRICAO"].ToString();
                string descA = "";

                bool quebra = false;

                for (int i = 0; i < desc.Length; i++)
                {
                    descA += desc.Substring(i, 1);

                    if (i == 25 || i == 50)
                        quebra = true;


                    if (quebra == true && desc.Substring(i, 1) == " ")
                    {
                        descA += "<BR>";
                        quebra = false;
                    }
                }

                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + descA));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCenter' align='CENTER' nowrap=nowrap style='font-size:8pt;'>" + rw["UnidadeDeMedida"].ToString()));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>" + decimal.Parse(rw["SALDO"].ToString()).ToString("#0.000")));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>"));
                PlaceHolder2.Controls.Add(CriarTXT(rw["IDCOTACAODECOMPRAITEM"].ToString(), decimal.Parse(rw["VALORUNITARIO"].ToString()), dt.Rows[0]["ConcluidoSite"].ToString()));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                decimal torlinha = decimal.Parse(rw["SALDO"].ToString()) * decimal.Parse(rw["VALORUNITARIO"].ToString());
                totlinhaValorProdutoAcumula += torlinha;

                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>" + torlinha.ToString("#0.0000")));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>"));
                PlaceHolder2.Controls.Add(CriarTXT("IPI" + rw["IDCOTACAODECOMPRAITEM"].ToString(), decimal.Parse(rw["AliquotaDeIpi"].ToString()), dt.Rows[0]["ConcluidoSite"].ToString()));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


                decimal valorIPI = (torlinha * (decimal.Parse(rw["AliquotaDeIpi"].ToString()) / 100));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>" + valorIPI.ToString("#0.00")));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                totlinhaValorIPIAcumula += valorIPI;


                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>"));
                PlaceHolder2.Controls.Add(CriarTXT("ICMS" + rw["IDCOTACAODECOMPRAITEM"].ToString(), decimal.Parse(rw["AliquotaDeIcms"].ToString()), dt.Rows[0]["ConcluidoSite"].ToString()));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                decimal valorICMS = torlinha * (decimal.Parse(rw["AliquotaDeIcms"].ToString()) / 100);
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>" + valorICMS.ToString("#0.00")));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                totlinhaValorICMSAcumula += valorICMS;


                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>" + (torlinha + valorIPI).ToString("#0.0000")));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                tottalGeral += torlinha + valorIPI;


                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:8pt;'>"));
                PlaceHolder2.Controls.Add(CriarObservacao("OBS" + rw["IDCOTACAODECOMPRAITEM"].ToString(), rw["OBSERVACAO"].ToString(), dt.Rows[0]["ConcluidoSite"].ToString()));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
            }


            #region totais
            //totais

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>TOTAIS:"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>" + totlinhaValorProdutoAcumula.ToString("#0.00")));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>" + totlinhaValorIPIAcumula.ToString("#0.00")));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>" + totlinhaValorICMSAcumula.ToString("#0.00")));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>&nbsp;&nbsp;&nbsp;<b>" + (totlinhaValorProdutoAcumula + totlinhaValorIPIAcumula).ToString("#0.00")));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</b></td>"));


            lblTotal.Visible = false;
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

            // fim de totais
            #endregion

            #region Frete
            //totais

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>FRETE:</b> "));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(CriarTXT("Frete", decimal.Parse(dt.Rows[0]["FRETE"].ToString()), dt.Rows[0]["ConcluidoSite"].ToString()));

            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            lblTotal.Visible = false;
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

            // fim de totais
            #endregion

            #region TotalGeral
            //totais

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>TOTAL: </b> "));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><B> " + (decimal.Parse(dt.Rows[0]["FRETE"].ToString()) + tottalGeral).ToString("#0.00")));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</B></td>"));

            lblTotal.Visible = false;
            lblTotal.Text = (tottalGeral + decimal.Parse(dt.Rows[0]["FRETE"].ToString())).ToString("#0.00");
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

            // fim de totais
            #endregion

            PlaceHolder2.Controls.Add(new LiteralControl(@"</table>"));

            #endregion

            #region Parcelas

            phParcelas.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

            phParcelas.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>#"));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));

            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>VENCIMENTO(DIAS)"));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));

            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>DATA"));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));

            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor'  class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>%"));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));

            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor'  class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>VALOR"));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));

            phParcelas.Controls.Add(new LiteralControl(@"</tr>"));

            DataTable dtcCondPagto = Sistran.Library.GetDataTables.RetornarDataSetWS("SELECT IDCotacaoFornecedorCondPgto, IDCOTACAOFORNECEDOR, VENCIMENTO, VALOR , '' VENC FROM CotacaoFornecedorCondPgto WHERE IDCOTACAOFORNECEDOR=" + Request.QueryString["I"], new Sistran.Logins.Acesso().ConexaoPorNomeBase("GrupoLogos").ToString()).Tables[0];
            //Session["dtcCondPagto"] = dtcCondPagto;

            if (dtcCondPagto.Rows.Count == 0 && Session["dtcCondPagto"] == null)
            {
                for (int i = 0; i < Convert.ToInt32(cboParcelas.SelectedValue); i++)
                {

                    phParcelas.Controls.Add(new LiteralControl(@"<tr>"));

                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCenter' align='left' nowrap=nowrap style='font-size:8pt;'>" + (i + 1).ToString()));
                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));

                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCenter' align='left' nowrap=nowrap style='font-size:8pt;'>"));

                    phParcelas.Controls.Add(CriarTXTVencimeto(i.ToString(), int.Parse((30 * (i + 1)).ToString()), dt.Rows[0]["ConcluidoSite"].ToString()));

                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));

                    DateTime d = DateTime.Now;
                    d = d.AddDays(30 * (i + 1));

                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCenter' align='left' nowrap=nowrap style='font-size:8pt;'>"));
                    phParcelas.Controls.Add(CriarTXTDataVencimeto(i.ToString(), d, dt.Rows[0]["ConcluidoSite"].ToString()));
                    //phParcelas.Controls.Add(CriarTXTDataVencimetoMascara(i.ToString(), d, dt.Rows[0]["ConcluidoSite"].ToString()));
                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));

                    decimal perc = (1 / Convert.ToDecimal(cboParcelas.SelectedValue));

                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>" + (perc * 100).ToString("#0.00") + "%"));
                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));

                    decimal valorParc = (tottalGeral + decimal.Parse(dt.Rows[0]["FRETE"].ToString())) * perc;

                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>" + valorParc.ToString("#0.00")));
                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));
                    phParcelas.Controls.Add(new LiteralControl(@"</tr>"));

                    DataRow r = dtcCondPagto.NewRow();
                    r["IDCotacaoFornecedorCondPgto"] = i;
                    r["IDCOTACAOFORNECEDOR"] = Request.QueryString["I"];
                    r["VENCIMENTO"] = d.ToShortDateString();
                    r["VALOR"] = valorParc.ToString("#0.00");
                    r["VENC"] = (30 * (i + 1)).ToString();

                    dtcCondPagto.Rows.Add(r);
                }


                Session["dtcCondPagto"] = dtcCondPagto;

            }
            else
            {
                dtcCondPagto = (DataTable)Session["dtcCondPagto"];

                if (dtcCondPagto == null)
                    dtcCondPagto = dtcCondPagto = Sistran.Library.GetDataTables.RetornarDataSetWS("SELECT IDCotacaoFornecedorCondPgto, IDCOTACAOFORNECEDOR, VENCIMENTO, VALOR , '' VENC FROM CotacaoFornecedorCondPgto WHERE IDCOTACAOFORNECEDOR=" + Request.QueryString["I"], new Sistran.Logins.Acesso().ConexaoPorNomeBase("GrupoLogos").ToString()).Tables[0]; ;



                for (int i = 0; i < Convert.ToInt32(dtcCondPagto.Rows.Count); i++)
                {
                    phParcelas.Controls.Add(new LiteralControl(@"<tr>"));

                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCenter' align='left' nowrap=nowrap style='font-size:8pt;'>" + (i + 1).ToString()));
                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));

                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCenter' align='left' nowrap=nowrap style='font-size:8pt;'>"));

                    int dias = 0;
                    if (dtcCondPagto.Rows[i]["VENC"].ToString() == "")
                    {
                        TimeSpan ts = DateTime.Now - Convert.ToDateTime(DateTime.Parse(dtcCondPagto.Rows[i]["VENCIMENTO"].ToString()));
                        dias = Math.Abs(ts.Days) + 1;
                        cboParcelas.SelectedValue = dtcCondPagto.Rows.Count.ToString();
                    }
                    else
                        dias = int.Parse(dtcCondPagto.Rows[i]["VENC"].ToString());


                    phParcelas.Controls.Add(CriarTXTVencimeto(i.ToString(), dias, dt.Rows[0]["ConcluidoSite"].ToString()));

                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));


                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpCenter' align='left' nowrap=nowrap style='font-size:8pt;'>"));
                    phParcelas.Controls.Add(CriarTXTDataVencimeto(i.ToString(), DateTime.Parse(dtcCondPagto.Rows[i]["VENCIMENTO"].ToString()), dt.Rows[0]["ConcluidoSite"].ToString()));
                    //phParcelas.Controls.Add(CriarTXTDataVencimetoMascara(i.ToString(), DateTime.Parse(dtcCondPagto.Rows[i]["VENCIMENTO"].ToString()), dt.Rows[0]["ConcluidoSite"].ToString()));
                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));

                    decimal perc = (1 / Convert.ToDecimal(cboParcelas.SelectedValue));

                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>" + (perc * 100).ToString("#0.00") + "%"));
                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));

                    decimal valorParc = (tottalGeral + decimal.Parse(dt.Rows[0]["FRETE"].ToString())) * perc;

                    phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' nowrap=nowrap style='font-size:8pt;'>" + valorParc.ToString("#0.00")));
                    phParcelas.Controls.Add(new LiteralControl(@"</td>"));
                    phParcelas.Controls.Add(new LiteralControl(@"</tr>"));
                }
                Session["dtcCondPagto"] = dtcCondPagto;
            }

            //totais


            phParcelas.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));

            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));


            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpR' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>"));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));

            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>TOTAL:"));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));

            phParcelas.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'>" + (tottalGeral + decimal.Parse(dt.Rows[0]["FRETE"].ToString())).ToString("#0.00")));
            phParcelas.Controls.Add(new LiteralControl(@"</td>"));


            phParcelas.Controls.Add(new LiteralControl(@"</tr>"));

            // fim de totais

            phParcelas.Controls.Add(new LiteralControl(@"</table>"));
            #endregion
        }
        catch (Exception)
        {
            //carregar();
        }
    }

    public DataTable Retornar()
    {
        string strsql = "";
        strsql += " SELECT ISNULL(ConcluidoSite, 'NAO') ConcluidoSite , ";
        strsql += " CF.IDCOTACAODECOMPRA, ";
        strsql += " CCI.IDCOTACAODECOMPRAITEM, ";
        strsql += " CADFIL.RAZAOSOCIALNOME SOLICTANTE,   ";
        strsql += " CADFIL.CNPJCPF CNPJSOLICITANTE,  ";
        strsql += " CADFIL.ENDERECO ENDERECOSOLICITANTE, ";
        strsql += " CADFIL.NUMERO NUMEROSOLICITANTE, ";
        strsql += " CIDSOLIC.NOME CIDADESOLICITANTE, ";
        strsql += " CADFIL.CEP CEPSOLICITANTE, ";
        strsql += " CADFORN.RAZAOSOCIALNOME FORNECEDOR,   ";
        strsql += " CADFORN.CNPJCPF CNPJSFORNECEDOR,  ";
        strsql += " CADFORN.ENDERECO ENDERECFORNECEDOR, ";
        strsql += " CADFORN.NUMERO NUMEROFORNECEDOR, ";
        strsql += " CIDSFORN.NOME CIDADEFORNECEDOR, ";
        strsql += " CADFORN.CEP CEPFORNECEDOR, ";
        strsql += " PC.DESCRICAO, ";
        strsql += " ISNULL(CCI.ValorDeIpi,0) VALORIPI,";
        strsql += " ISNULL(CCI.ValorDeIcms,0) VALORICMS,";
        strsql += " ISNULL(CCI.AliquotaDeIcms,0) AliquotaDeIcms,";
        strsql += " ISNULL(CCI.AliquotaDeIpi,0) AliquotaDeIpi,";
        strsql += " ISNULL(ValorDeFrete,0) FRETE,";

        strsql += " PC.CODIGO, RESPONSAVEL,";
        strsql += " CCI.SALDO, ISNULL(CCI.VALORUNITARIO, '0.00') VALORUNITARIO, DATADEENTREGA, ";
        strsql += " PE.UnidadeDeMedida, ";
        strsql += " CCI.OBSERVACAO ";
        strsql += " FROM COTACAODECOMPRA CC ";
        strsql += " INNER JOIN COTACAOFORNECEDOR CF ON CF.IDCOTACAODECOMPRA = CC.IDCOTACAODECOMPRA ";
        strsql += " INNER JOIN COTACAODECOMPRAITEM CCI ON CCI.IDCOTACAOFORNECEDOR = CF.IDCOTACAOFORNECEDOR ";
        strsql += " INNER JOIN FORNECEDOR FORNEC ON FORNEC.IDFORNECEDOR = CF.IDFORNECEDOR ";
        strsql += " INNER JOIN CADASTRO CADFORN ON CADFORN.IDCADASTRO = FORNEC.IDFORNECEDOR ";
        strsql += " INNER JOIN FILIAL FL ON FL.IDFILIAL = CC.IDFILIAL ";
        strsql += " INNER JOIN CADASTRO CADFIL ON CADFIL.IDCADASTRO = FL.IDCADASTRO ";
        strsql += " left JOIN CIDADE CIDSOLIC ON  CIDSOLIC.IDCIDADE = CADFIL.IDCIDADE ";
        strsql += " left JOIN CIDADE CIDSFORN ON  CIDSFORN.IDCIDADE = CADFORN.IDCIDADE ";
        strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = CCI.IDPRODUTOCLIENTE ";
        strsql += " LEFT JOIN PRODUTOEMBALAGEM PE  ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
        strsql += " WHERE CF.IDCOTACAOFORNECEDOR = " + Request.QueryString["i"];


        return Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, new Sistran.Logins.Acesso().ConexaoPorNomeBase("GrupoLogos").ToString()).Tables[0];
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtI.Text == "" || DateTime.Parse(txtI.Text).AddDays(23).AddMinutes(59) < DateTime.Now)
            {
                txtI.Focus();
                throw new Exception("Data Inválida ou Vazia.");
            }

            decimal valorAntes = decimal.Parse(lblTotal.Text);
            Calcular();

            if (decimal.Parse(lblTotal.Text) == Convert.ToDecimal(0))
            {
                throw new Exception("<br>Preencha a cotação.");

            }

            if (decimal.Parse(lblTotal.Text) != valorAntes)
            {
                throw new Exception("Favor Calcular a Cotação.");
            }

            bool achou = false;
            DataTable dt = (DataTable)Session["dtContacao"];
            DataTable dtcCondPagto = (DataTable)Session["dtcCondPagto"];

            string strsqlDatas = "UPDATE COTACAOFORNECEDOR SET  BASEDEICMS=cast(@BASEICMS AS NUMERIC(18,2)) , VALORDEIPI = cast(@VALORIPI AS NUMERIC(18,2)) , VALORDEFRETE = cast(@VALORFRETE AS NUMERIC(18,2)), VALORTOTALDECOMPRA = cast(@VALORTOTAL AS NUMERIC(18,2)) ,RESPONSAVEL='" + txtResponsavel.Text.ToUpper().Replace("'", "") + "', CONCLUIDOSITE='SIM', DATADEENTREGA= CONVERT(DATETIME, '" + txtI.Text + "', 103) WHERE IDCOTACAOFORNECEDOR=" + Request.QueryString["i"] + "   ";
            string strsqlItens = "";
            string strsqlCondPag = "";

            decimal valortotalCotacao = 0;
            decimal valordEipi = 0;
            decimal valorBaseICMS = 0;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Decimal.Parse(dt.Rows[i]["VALORUNITARIO"].ToString()) > 0)
                {


                    decimal vtitem = (decimal.Parse(dt.Rows[i]["VALORUNITARIO"].ToString()) * decimal.Parse(dt.Rows[i]["SALDO"].ToString())) * (1 + decimal.Parse(dt.Rows[i]["ALIQUOTADEIPI"].ToString()) / 100);
                    decimal vIPI = (decimal.Parse(dt.Rows[i]["VALORUNITARIO"].ToString()) * decimal.Parse(dt.Rows[i]["SALDO"].ToString())) * (decimal.Parse(dt.Rows[i]["ALIQUOTADEIPI"].ToString()) / 100);
                    decimal vICMS = (decimal.Parse(dt.Rows[i]["VALORUNITARIO"].ToString()) * decimal.Parse(dt.Rows[i]["SALDO"].ToString())) * (decimal.Parse(dt.Rows[i]["ALIQUOTADEICMS"].ToString()) / 100);

                    strsqlItens += " UPDATE COTACAODECOMPRAITEM SET AliquotaDeIpi=CAST(" + dt.Rows[i]["ALIQUOTADEIPI"].ToString().Replace(",", ".") + " as NUMERIC(18,2)) " +
                                   ", ALIQUOTADEICMS=CAST(" + dt.Rows[i]["ALIQUOTADEICMS"].ToString().Replace(",", ".") + " as NUMERIC(18,2)) " +
                                   ", VALORUNITARIO = CAST(" + dt.Rows[i]["VALORUNITARIO"].ToString().Replace(",", ".") + " as NUMERIC(18,4)) " +
                                   ", VALORTOTALDOITEM = CAST(" + vtitem.ToString().Replace(",", ".") + " AS NUMERIC(18,4)) " +
                                   ", VALORDEIPI=CAST(" + vIPI.ToString().Replace(",", ".") + " AS NUMERIC(18,2)) " +
                                   ", VALORDEICMS=CAST(" + vICMS.ToString().Replace(",", ".") + " AS NUMERIC(18,2)) " +
                                   ", OBSERVACAO = '" + dt.Rows[i]["OBSERVACAO"].ToString().ToUpper() + "' " +
                                   " WHERE IDCOTACAODECOMPRAITEM=" + dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString();

                    valortotalCotacao += vtitem;
                    valordEipi += vIPI;
                    valorBaseICMS += (decimal.Parse(dt.Rows[i]["VALORUNITARIO"].ToString()) * decimal.Parse(dt.Rows[i]["SALDO"].ToString()));
                    achou = true;
                }
            }

            strsqlDatas = strsqlDatas.Replace("@VALORTOTAL", valortotalCotacao.ToString().Replace(",", "."));
            strsqlDatas = strsqlDatas.Replace("@VALORFRETE", dt.Rows[0]["FRETE"].ToString().Replace(",", "."));
            strsqlDatas = strsqlDatas.Replace("@VALORIPI", valordEipi.ToString().Replace(",", "."));
            strsqlDatas = strsqlDatas.Replace("@BASEICMS", valorBaseICMS.ToString().Replace(",", "."));


            strsqlCondPag = "delete from CotacaoFornecedorCondPgto where IDCOTACAOFORNECEDOR=" + Request.QueryString["I"];
            decimal valorDiv = decimal.Parse(lblTotal.Text) / int.Parse(cboParcelas.SelectedValue);

            for (int i = 0; i < dtcCondPagto.Rows.Count; i++)
            {
                string cod = Sistran.Library.GetDataTables.RetornarIdTabela("CotacaoFornecedorCondPgto", new Sistran.Logins.Acesso().ConexaoPorNomeBase("GrupoLogos").ToString());

                strsqlCondPag += " INSERT INTO CotacaoFornecedorCondPgto (IDCotacaoFornecedorCondPgto, IDCOTACAOFORNECEDOR, VENCIMENTO, VALOR) ";
                strsqlCondPag += " VALUES (" + cod + ", " + Request.QueryString["I"] + ", CONVERT(DATETIME, '" + DateTime.Parse(dtcCondPagto.Rows[i]["VENCIMENTO"].ToString()).ToShortDateString() + "', 103), " + valorDiv.ToString().Replace(",", ".") + ") ";
            }

            if (achou == true)
            {
                string m = strsqlItens + " " + strsqlDatas + " " + strsqlCondPag + " ";
                Sistran.Library.GetDataTables.InserirCotacao(new Sistran.Logins.Acesso().ConexaoPorNomeBase("GrupoLogos").ToString(), m);

                DataTable dtf = (DataTable)Session["dtContacao"];
                string corpoEmail = "<html>";
                corpoEmail += "<body>";
                corpoEmail += "O Fornecedor " + dtf.Rows[0]["CNPJSFORNECEDOR"] + " - " + dtf.Rows[0]["FORNECEDOR"] + ", preencheu a cotação de compra as " + DateTime.Now.ToString() + ".<br><br><br>";
                corpoEmail += "Para visualizar clique <a href='http://www.grupologos.com.br/SistranWeb.NET/cotacao/COTACAOFORNECEDOR.ASPX?I=" + Request.QueryString["i"] + "' >aqui</a>.";
                corpoEmail += "</body>";
                corpoEmail += "</html>";

                List<listaEmailIrwin> lem = new List<listaEmailIrwin>();
                listaEmailIrwin em = new listaEmailIrwin();
                lem.Add(em);
                em.email = "compras@grupologos.com.br";

                em = new listaEmailIrwin();
                lem.Add(em);
                em.email = "jorge@sistecno.com.br";

                Sistran.Library.EnviarEmails.enviarEmailGrupoLogos("Preenchimento de Cotação", corpoEmail, lem);
            }

            dvErro.Visible = true;
            lblMensagem.Text = "<br>Obrigado por preencher sua cotação. <br><br><br> Clique em OK para finalizar.";
            btnVoltarDv.Visible = false;
            btnSairDv.Visible = true;
            Panel1.Enabled = false;
            LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
            lnkLogout.Focus();
        }
        catch (Exception ex)
        {
            gerarErro("Ocorreu algum problema, clique Voltar e Corrigir e verifique os campos. " + ex.Message);
        }
    }

    private void gerarErro(string msg)
    {
        dvErro.Visible = true;
        lblMensagem.Text = msg;
        btnVoltarDv.Visible = true;
        btnSairDv.Visible = false;
        Panel1.Enabled = false;
        btnVoltarDv.Focus();
        LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
        lnkLogout.Focus();
    }

    protected void btnVoltarDv_Click(object sender, EventArgs e)
    {
        dvErro.Visible = false;
        btnSairDv.Visible = false;
        Panel1.Enabled = true;
    }

    protected void cboParcelas_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["dtcCondPagto"] = null;
        carregar();
    }

    #region T X T  D I N A M I C O

    public TextBox CriarTXT(string idContacaoDeCompraItem, decimal valor, string finalizadaSite)
    {
        TextBox txt = new TextBox();
        txt.ID = "txt" + idContacaoDeCompraItem.ToString();
        txt.Width = 60;
        txt.CssClass = "txtValor";

        if (txt.ID.Contains("IPI") || txt.ID.Contains("ICMS"))
            txt.Text = valor.ToString("#0.00");
        else
            txt.Text = valor.ToString("#0.0000");



        txt.Attributes.Add("onkeypress", "return SomenteNumeroDecimal(event)");
        txt.Attributes.Add("onFocus", "LimpaValoresZerados(this)");
        txt.Attributes.Add("onBlur", "ColocaValoresZerados(this)");

        if (finalizadaSite == "SIM")
            txt.ReadOnly = true;

        if (Request.QueryString["bl"] != null)
            txt.Enabled = false;

        return txt;
    }

    private void txt_TextChanged(object sender, System.EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtContacao"];
        DataTable dtCP = (DataTable)Session["dtcCondPagto"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            TextBox txt = (TextBox)sender;
            if (dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString() == txt.ID.Replace("txt", ""))
            {
                dt.Rows[i]["VALORUNITARIO"] = txt.Text;
                Session["dtContacao"] = dt;

                for (int ii = 0; ii < dtCP.Rows.Count; ii++)
                {
                    dtCP.Rows[ii]["VALOR"] = decimal.Parse(lblTotal.Text) / decimal.Parse(cboParcelas.SelectedValue);
                }

                //carregar();
                return;
            }

            //carregar();
        }

    }

    public TextBox CriarObservacao(string idContacaoDeCompraItem, string valor, string finalizadaSite)
    {
        TextBox txt = new TextBox();
        txt.ID = "txt" + idContacaoDeCompraItem.ToString();
        txt.Width = 250;
        txt.CssClass = "txt";
        txt.MaxLength = 300;
        txt.TextMode = TextBoxMode.MultiLine;
        txt.Height = 30;

        txt.Text = valor.ToUpper();

        if (finalizadaSite == "SIM")
            txt.ReadOnly = true;

        if (Request.QueryString["bl"] != null)
            txt.Enabled = false;


        return txt;
    }


    public TextBox CriarTXTVencimeto(string IdCotacaoFornecedorCondicaoPagamento, int valor, string finalizadaSite)
    {
        TextBox txt = new TextBox();
        txt.ID = "txtVencimento" + IdCotacaoFornecedorCondicaoPagamento.ToString();
        txt.Width = 70;
        txt.CssClass = "txtValor";
        txt.Text = valor.ToString("#0");
        txt.Attributes.Add("onkeypress", "return SomenteNumero(event)");

        txt.AutoPostBack = true;

        if (finalizadaSite == "SIM")
            txt.ReadOnly = true;

        if (Request.QueryString["bl"] != null)
            txt.Enabled = false;

        txt.TextChanged += txt_TextChangedVencimento;

        return txt;
    }

    private void txt_TextChangedVencimento(object sender, System.EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtcCondPagto"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TextBox txt = (TextBox)sender;
            if (dt.Rows[i]["IDCotacaoFornecedorCondPgto"].ToString() == txt.ID.Replace("txtVencimento", ""))
            {
                dt.Rows[i]["VENC"] = txt.Text;
                dt.Rows[i]["VENCIMENTO"] = DateTime.Now.AddDays(int.Parse(txt.Text)).ToShortDateString();
                dt.Rows[i]["VALOR"] = decimal.Parse(lblTotal.Text) / decimal.Parse(cboParcelas.SelectedValue);

                Session["dtcCondPagto"] = dt;
                carregar();
                return;
            }
        }
    }

    public TextBox CriarTXTDataVencimeto(string IdCotacaoFornecedorCondPgto, DateTime valor, string finalizadaSite)
    {
        TextBox txt = new TextBox();
        txt.ID = "txtDataVencimento" + IdCotacaoFornecedorCondPgto.ToString();
        txt.Width = 70;
        txt.CssClass = "txt";
        txt.Font.Size = 8;
        txt.Text = valor.ToShortDateString();
        txt.AutoPostBack = true;

        if (finalizadaSite == "SIM")
            txt.ReadOnly = true;

        if (Request.QueryString["bl"] != null)
            txt.Enabled = false;

        txt.TextChanged += txt_TextChangedDataVencimento;

        return txt;
    }

    private void txt_TextChangedDataVencimento(object sender, System.EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtcCondPagto"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TextBox txt = (TextBox)sender;
            if (dt.Rows[i]["IDCotacaoFornecedorCondPgto"].ToString() == txt.ID.Replace("txtDataVencimento", ""))
            {
                TimeSpan ts = DateTime.Now - Convert.ToDateTime(txt.Text);
                dt.Rows[i]["VENC"] = Math.Abs(ts.Days);
                dt.Rows[i]["VENCIMENTO"] = txt.Text;
                dt.Rows[i]["VALOR"] = decimal.Parse(lblTotal.Text) / decimal.Parse(cboParcelas.SelectedValue);
                Session["dtcCondPagto"] = dt;
                carregar();
                return;
            }
        }
    }

    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
        Calcular();
    }

    private void Calcular()
    {
        DataTable dt = (DataTable)Session["dtContacao"];
        DataTable dtCP = (DataTable)Session["dtcCondPagto"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            TextBox txt = (TextBox)PlaceHolder2.FindControl("txt" + dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString());
            if ("txt" + dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString() == txt.ID)
            {
                dt.Rows[i]["VALORUNITARIO"] = txt.Text;
            }

            TextBox txtIPI = (TextBox)PlaceHolder2.FindControl("txtIPI" + dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString());
            if ("txtIPI" + dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString() == txtIPI.ID)
            {
                dt.Rows[i]["AliquotaDeIpi"] = txtIPI.Text;
            }

            TextBox txtICMS = (TextBox)PlaceHolder2.FindControl("txtICMS" + dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString());
            if ("txtICMS" + dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString() == txtICMS.ID)
            {
                dt.Rows[i]["AliquotaDeIcms"] = txtICMS.Text;
            }


            TextBox txtOBS = (TextBox)PlaceHolder2.FindControl("txtObs" + dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString());
            if ("txtOBS" + dt.Rows[i]["IDCOTACAODECOMPRAITEM"].ToString() == txtOBS.ID)
            {
                if (txtOBS.Text.Length > 300)
                    txtOBS.Text = txtOBS.Text.Substring(0, 300);

                dt.Rows[i]["observacao"] = txtOBS.Text;
            }

            TextBox txtFrete = (TextBox)PlaceHolder2.FindControl("txtFrete");
            if (txtFrete != null)
                dt.Rows[i]["FRETE"] = txtFrete.Text;

        }

        Session["dtContacao"] = dt;
        carregar();
    }
}