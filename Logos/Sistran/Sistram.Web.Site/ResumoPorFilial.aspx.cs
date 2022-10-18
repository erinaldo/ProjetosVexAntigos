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

public partial class ResumoPorFilial : System.Web.UI.Page
{
    public int intervalo;
    public DataTable dtRegra;
    string clientesSelecionados = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ChartDirector.WebChartViewer.OnPageInit(Page);
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = FuncoesGerais.RetornarIntervaloDiasPesqusa();           

            clientesSelecionados = Sistran.Library.FuncoesUteis.retornarClientesResumoFilial(false);


            if (!IsPostBack)
            {
                btnOrigemDados.Visible = false;
                txtQtdTransitTime.Text = "2";
                tdNaoEntregue.Visible = false;
                tdNaoEntregue2.Visible = false;
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                //btnGerarReport.Visible = false;
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];

                // carrega as informações com as regras para calculo das colunas
                if (clientesSelecionados != "")
                    dtRegra = Sistran.Library.GetDataTables.RetornarDataTable("SELECT DISTINCT IDCLIENTEMETA, PRAZO, COR, PERCENTUAL FROM CLIENTEMETA WHERE IDCLIENTE IN (" + clientesSelecionados + ")");

                if (dtRegra == null || dtRegra.Rows.Count == 0)
                    dtRegra = Sistran.Library.GetDataTables.RetornarDataTable("SELECT DISTINCT IDCLIENTEMETA, PRAZO, COR, PERCENTUAL FROM CLIENTEMETA WHERE IDCLIENTE IN (444)");
               
       


                Session["dtRegra"] = dtRegra;
                int qtdMinimaTT = (dtRegra.Compute("Max(PRAZO)", "") == DBNull.Value ? 0 : int.Parse(dtRegra.Compute("Max(PRAZO)", "").ToString()));

                if (qtdMinimaTT == 0)
                    txtQtdTransitTime.Text = "3";
                else
                    txtQtdTransitTime.Text = (qtdMinimaTT + 1).ToString();
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            //btnGerarReport.Attributes.Add("onClick", "window.open('frmRptResumoFilial.aspx?tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "&DI=" + txtI.Text + "&DF=" + txtF.Text + "', 'NovaJanela2', 'yes')");


            Session["DataConf"] = txtI.Text + "|" + txtF.Text;

            dtRegra = (DataTable)Session["dtRegra"];
            //monta a legenda

            PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='tableSemTamanho'  cellspacing='1' celpanding='1' border='1' >"));

            int qtdMaxPraxo = (dtRegra.Compute("Max(PRAZO)", "") == DBNull.Value ? 0 : int.Parse(dtRegra.Compute("Max(PRAZO)", "").ToString()));

            if (dtRegra.Rows.Count > 0)
            {
                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                for (int i = 1; i <= qtdMaxPraxo; i++)
                {
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (i * 24).ToString() + "hs"));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                }
                PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));


                for (int ilinhas = 3; ilinhas >= 1; ilinhas--)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

                    #region Linha 1
                    if (ilinhas == 1)
                    {
                        for (int iColuna = 1; iColuna <= qtdMaxPraxo; iColuna++)
                        {
                            string cor = "Verde";
                            DataRow[] p = dtRegra.Select("PRAZO=" + iColuna + " AND COR='" + cor + "'", "");

                            string perc = "";

                            if (p.Length > 0)
                            {
                                perc = "Até: " + p[0]["Percentual"].ToString() + "%";
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana" + cor + "' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + perc));
                            }
                            else
                            {
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + perc));
                            }
                            
                            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                        }
                    }
                    #endregion


                    #region Linha 2
                    if (ilinhas == 2)
                    {
                        for (int iColuna = 1; iColuna <= qtdMaxPraxo; iColuna++)
                        {
                            string cor = "Amarelo";
                            DataRow[] p = dtRegra.Select("PRAZO=" + iColuna + " AND COR='" + cor + "'", "");

                            string perc = "";

                            if (p.Length > 0)
                            {
                                perc = "Até: " + p[0]["Percentual"].ToString() + "%";
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana" + cor + "' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + perc));
                            }
                            else
                            {
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + perc));
                            }

                            //PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana" + cor + "' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + perc));
                            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                        }
                    }
                    #endregion


                    #region Linha 3
                    if (ilinhas == 3)
                    {
                        for (int iColuna = 1; iColuna <= qtdMaxPraxo; iColuna++)
                        {
                            string cor = "Vermelho";
                            DataRow[] p = dtRegra.Select("PRAZO=" + iColuna + " AND COR='" + cor + "'", "");

                            string perc = "";

                            if (p.Length > 0)
                            {
                                perc = "Até: " + p[0]["Percentual"].ToString() + "%";
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana" + cor + "' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + perc));
                            }
                            else
                            {
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + perc));
                            }

                            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                        }
                    }
                    #endregion

                    PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
                }

                PlaceHolder2.Controls.Add(new LiteralControl(@"</table>"));

            }
            else
                PlaceHolder2.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
           
            int qtdMaxPraxo = (dtRegra.Compute("Max(PRAZO)", "") == DBNull.Value ? 0 : int.Parse(dtRegra.Compute("Max(PRAZO)", "").ToString()));
            
              MontarTable(false);

            //if (RadioButtonList1.SelectedItem.Value.ToUpper() == "FILIAL")
            //    btnGerarReport.Visible = true;
            //else
            //    btnGerarReport.Visible = false;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected bool CalcularMaiorZeros(DataTable dts, int tt)
    {
        int? qtd = 0;
        qtd = Convert.ToInt32(dts.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + tt.ToString()) == DBNull.Value ? 0 : dts.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + tt.ToString()));

        int maxTT = Convert.ToInt32(dts.Compute("max(PRAZOUTILIZADO)", ""));

        if ((qtd == 0 && maxTT > 3) || tt == 0)
            return false;
        else
            return true;
    }

    protected void MontarTable(bool DesprezarNaoEntregues)
    {
        try
        {
            tbGraf.Visible = false;
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

            Sistran.Library.Enuns.tipoReportResumoFilial en = new Sistran.Library.Enuns.tipoReportResumoFilial();

            if (RadioButtonList1.SelectedIndex == 0)
                en = Sistran.Library.Enuns.tipoReportResumoFilial.filial;

            if (RadioButtonList1.SelectedIndex == 1)
                en = Sistran.Library.Enuns.tipoReportResumoFilial.cidadeDestinatario;

            if (RadioButtonList1.SelectedIndex == 2)
                en = Sistran.Library.Enuns.tipoReportResumoFilial.destinatario;

            if (RadioButtonList1.SelectedIndex == 3)
                en = Sistran.Library.Enuns.tipoReportResumoFilial.estado;


            Session["Conn"] = HttpContext.Current.Session["ConnLogin"].ToString();
            string[] dadosPesquisa = new string[6];
            dadosPesquisa[0] = txtI.Text;
            dadosPesquisa[1] = txtF.Text;
            dadosPesquisa[2] = Session["Conn"].ToString();
            dadosPesquisa[3] = chkSeries.Items[0].Selected.ToString();
            dadosPesquisa[4] = chkSeries.Items[2].Selected.ToString();
            dadosPesquisa[5] = chkSeries.Items[1].Selected.ToString();

            Session["dadosPesquisa"] = dadosPesquisa;
            btnOrigemDados.Attributes.Add("onClick", "window.open('kpi/gerarexcel.aspx', 'NovaJanela2', 'yes'); return false;");

            DataTable dt = NotasFiscais.ListarResumoPorFilial(
                                                                Convert.ToDateTime(txtI.Text), 
                                                                Convert.ToDateTime(txtF.Text), 
                                                                clientesSelecionados, Session["Conn"].ToString(), en,
                                                                chkSeries.Items[0].Selected,
                                                                chkSeries.Items[1].Selected,
                                                                chkSeries.Items[2].Selected               
                );
            dtRegra = (DataTable)Session["dtRegra"];

            if (dt.Rows.Count == 0)
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                return;
            }

            btnOrigemDados.Visible = true;
            PlaceHolder1.Controls.Clear();

            int QtdFiliais =  (from DataRow dr in dt.Rows orderby (string)dr["NOMEDAFILIAL"] select (string)dr["NOMEDAFILIAL"]).Distinct().Count();


            //acerta as colunas

            int qtdColunas = 0;

            qtdColunas = (3 * Convert.ToInt32(txtQtdTransitTime.Text)) + 4;


            decimal qtdTotalNf = (Convert.ToDecimal(dt.Compute("MAX(TOTALDENOTAS)", "")));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='tableSemTamanho' cellspacing='1' celpanding='1'  runat='server' >"));
            int contDefColun = 0;
            string NomeColuna = "";

            #region Cabeçalho

            //cabeçalho
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            int TT = 0;
            for (int i = 0; i < qtdColunas; i++)
            {
                if (i == 0)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + RadioButtonList1.SelectedItem.Text.ToUpper()));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                }
                else
                {
                    if ((CalcularMaiorZeros(dt, TT) && TT <= Convert.ToInt32(txtQtdTransitTime.Text)) || TT == Convert.ToInt32(txtQtdTransitTime.Text))
                    {
                        switch (contDefColun)
                        {
                            case 0:
                                contDefColun += 1;
                                break;

                            case 1:
                                if (TT < Convert.ToInt32(txtQtdTransitTime.Text))
                                    NomeColuna = "&nbsp;&nbsp;&nbsp;" + (TT * 24).ToString() + " HS.";
                                else
                                    NomeColuna = "&nbsp;" + (TT * 24).ToString() + " HS. ACIMA";
                                //NomeColuna = "&nbsp;&nbsp;&nbsp;NF ENTREGUES";
                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold;'>" + NomeColuna));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                contDefColun += 1;
                                break;

                            case 2:
                                NomeColuna = "&nbsp;&nbsp;&nbsp;% NF";
                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + NomeColuna));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                contDefColun = 0;
                                TT++;
                                break;
                        }
                    }
                    else
                    {
                        TT++;
                    }
                }

            }


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold;width:1%'>&nbsp;&nbsp;&nbsp;TOTAL NF ENTREGUES"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td style='background-color: #FFFFFF;' rowspan=" + (QtdFiliais + 2).ToString() + ">&nbsp;&nbsp;&nbsp"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold;width:1%'>&nbsp;&nbsp;&nbsp;NF NÃO ENTREGUES"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;&nbsp;% NF"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;&nbsp;TOTAL DE NOTAS"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            #endregion

            int qtdColunasTemp = Convert.ToInt32(txtQtdTransitTime.Text);
            decimal[] tot = new decimal[qtdColunasTemp + 1];

            int qtdQtdLinhas = 0;
            #region Itens
            for (int i = 0; i < qtdColunas; i++)
            {
                string NomeFilial = "";
                for (int ii = 0; ii < dt.Rows.Count; ii++)
                {
                    if (i == 0)
                    {

                        if (NomeFilial != dt.Rows[ii]["NOMEDAFILIAL"].ToString())
                        {
                            string filtemp = dt.Rows[ii]["NOMEDAFILIAL"].ToString();

                            //if(filtemp.Contains("SAO JOSE DOS CAMPOS"))
                            //    filtemp = "SJC";

                            //if (filtemp.Contains("SAO BERNARDO DO CAMPO"))
                            //    filtemp = "SBC";

                            //if (filtemp.Contains("PROMOCIONAL JANDIRA"))
                            //    filtemp = "JANDIRA";


                            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal; width:1%'>" + filtemp));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                            qtdQtdLinhas++;

                            DataRow[] orw = dt.Select("NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'");

                            int contador = 0;
                            int row = 0;
                            decimal porcAtualAcmulado = Convert.ToDecimal("0.00");

                            foreach (DataRow item in orw)
                            {
                                //CRIA AS COLUNAS ATE ENCONTRAR UM COLUNA VALIDA
                                if (contador < Convert.ToInt32(item["PRAZOUTILIZADO"]) )
                                {
                                    for (int kkkk = contador; kkkk < Convert.ToInt32(item["PRAZOUTILIZADO"]); kkkk++)
                                    {
                                        string cs = "tdpR";
                                        //se nao tiver a regra continua a regra padrao
                                        if ((contador == 1 || contador == 2) && dtRegra.Rows.Count==0)
                                        {

                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));     

                                            switch (contador)
                                            {
                                                case 1:
                                                    if (porcAtualAcmulado >= Convert.ToDecimal(80))
                                                        cs = "tdpRVerdanaVerde";
                                                    else if (porcAtualAcmulado >= Convert.ToDecimal(75) && porcAtualAcmulado < Convert.ToDecimal(80))
                                                        cs = "tdpRVerdanaAmarelo";
                                                    else
                                                        cs = "tdpRVerdanaVermelho";

                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                    break;

                                                case 2:
                                                    if (porcAtualAcmulado >= Convert.ToDecimal(90))
                                                        cs = "tdpRVerdanaVerde";
                                                    else if (porcAtualAcmulado >= Convert.ToDecimal(85) && porcAtualAcmulado < Convert.ToDecimal(90))
                                                        cs = "tdpRVerdanaAmarelo";
                                                    else
                                                        cs = "tdpRVerdanaVermelho";

                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                    break;

                                                default:
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                    break;
                                            }                                            
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                        }
                                        else if (contador <= int.Parse((dtRegra.Compute("Max(PRAZO)", "").ToString() == "" ? "0" : dtRegra.Compute("Max(PRAZO)", "").ToString())) && contador > 0 && contador < Convert.ToInt32(txtQtdTransitTime.Text))
                                        {
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                            DataRow[] oroeCor = dtRegra.Select("PRAZO=" + contador, "");
                                            cs = "tdpR";
                                            for (int icor = 0; icor < oroeCor.Length; icor++)
                                            {
                                                if (decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString()) >= porcAtualAcmulado)
                                                {
                                                    cs = "tdpRVerdana" + oroeCor[icor]["Cor"].ToString();
                                                    //break;
                                                }
                                            }


                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                            

                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                        }
                                        else if (contador <= int.Parse(txtQtdTransitTime.Text))
                                        {
                                            if (int.Parse(txtQtdTransitTime.Text) == contador)
                                            {
                                                decimal qtdItemAtual = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + txtQtdTransitTime.Text + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                                decimal qtdTotal = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                                decimal v = Convert.ToDecimal(0);

                                                if (qtdTotal > 0)
                                                    v = (qtdItemAtual / qtdTotal) * 100;

                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + txtQtdTransitTime.Text + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"))));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                porcAtualAcmulado += v;

                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            }
                                            else
                                            {
                                                if (CalcularMaiorZeros(dt, contador))
                                                {
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                }
                                            }

                                        }
                                        contador += 1;
                                        row += 1;
                                    }
                                }

                                if (contador == Convert.ToInt32(item["PRAZOUTILIZADO"]) && contador <= Convert.ToInt32(txtQtdTransitTime.Text))
                                {
                                    #region "Itens Calculos"

                                    decimal qtdItemAtual = Convert.ToDecimal(0);
                                    decimal qtdTotal = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));


                                    if (contador < Convert.ToInt32(txtQtdTransitTime.Text))
                                    {
                                        qtdItemAtual = Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);
                                    }
                                    else if (contador == Convert.ToInt32(txtQtdTransitTime.Text))
                                    {
                                        qtdItemAtual = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + txtQtdTransitTime.Text + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                    }

                                    decimal porcAtual = Convert.ToDecimal(0);

                                    if (qtdTotal > 0)
                                    {
                                        porcAtual = (qtdItemAtual / qtdTotal) * 100;
                                    }
                                    else
                                    {
                                        porcAtual = Convert.ToDecimal(0);
                                    }

                                    if (contador == Convert.ToInt32(txtQtdTransitTime.Text))
                                    {
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + txtQtdTransitTime.Text + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"))));
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                                        tot[row] += Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);
                                        tot[row] += Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + txtQtdTransitTime.Text + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                    }
                                    else
                                    {
                                        if (CalcularMaiorZeros(dt, contador))
                                        {
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + item["NOTASFISCAIS_ENTREGUE"].ToString()));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                                        }
                                        tot[row] += Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);

                                    }

                                    porcAtualAcmulado += porcAtual;

                                   if (dtRegra.Rows.Count == 0)// se nao tiver regra fica com a regra padrao
                                    {
                                        string cs = "tdpR";
                                        switch (contador)
                                        {

                                            case 1:
                                                if (porcAtualAcmulado >= Convert.ToDecimal(80))
                                                    cs = "tdpRVerdanaVerde";
                                                else if (porcAtualAcmulado >= Convert.ToDecimal(75) && porcAtualAcmulado < Convert.ToDecimal(80))
                                                    cs = "tdpRVerdanaAmarelo";
                                                else
                                                    cs = "tdpRVerdanaVermelho";

                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                break;
                                            case 2:
                                                if (porcAtualAcmulado >= Convert.ToDecimal(90))
                                                    cs = "tdpRVerdanaVerde";
                                                else if (porcAtualAcmulado >= Convert.ToDecimal(85) && porcAtualAcmulado < Convert.ToDecimal(90))
                                                    cs = "tdpRVerdanaAmarelo";
                                                else
                                                    cs = "tdpRVerdanaVermelho";

                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                break;

                                            default:
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                break;
                                        }
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                    }
                                    else if(contador>0)// aplica a regra de cores
                                    {
                                        if (CalcularMaiorZeros(dt, contador) )
                                        {
                                            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0000"));
                                            //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                            DataRow[] oroeCor = dtRegra.Select("PRAZO=" + contador, "");
                                            string cs = "tdpR";

                                            decimal verde = Convert.ToDecimal(0);
                                            decimal amarelo = Convert.ToDecimal(0);
                                            decimal vermelho = Convert.ToDecimal(0);

                                            for (int icor = 0; icor < oroeCor.Length; icor++)
                                            {
                                                if (oroeCor[icor]["Cor"].ToString() == "Verde")
                                                    verde = decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString());

                                                if (oroeCor[icor]["Cor"].ToString() == "Amarelo")
                                                    amarelo = decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString());

                                                if (oroeCor[icor]["Cor"].ToString() == "Vermelho")
                                                    vermelho = decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString());                                            
                                                
                                            }

                                            if (porcAtualAcmulado <= vermelho)
                                            {
                                                cs = "tdpRVerdanaVermelho";
                                            }
                                            else if (porcAtualAcmulado < verde && porcAtualAcmulado <= amarelo)
                                            {
                                                cs = "tdpRVerdanaAmarelo";
                                            }
                                            else  if (porcAtualAcmulado > amarelo && porcAtualAcmulado<= verde)
                                                cs = "tdpRVerdanaVerde";

                                            if (verde == 0 && vermelho == 0 && porcAtualAcmulado <= amarelo)
                                            {
                                                cs = "tdpRVerdanaAmarelo";
                                            }

                                            //if (verde == 0 && amarelo == 0)
                                            //{
                                            //    cs = "tdpRVerdanaVermelho";
                                            //}

                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                        }
                                    }
                                }
                                    #endregion
                                row += 1;
                                contador += 1;
                            }

                            if (contador <= qtdColunasTemp)
                            {
                                for (int iii = contador; iii <= qtdColunasTemp; iii++)
                                {
                                    if (contador == 1 || contador == 2 && dtRegra.Rows.Count==0)
                                    {
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                        string cs = "tdpR";
                                        switch (contador)
                                        {
                                            case 1:
                                                if (porcAtualAcmulado >= Convert.ToDecimal(80))
                                                    cs = "tdpRVerdanaVerde";
                                                else if (porcAtualAcmulado >= Convert.ToDecimal(75) && porcAtualAcmulado < Convert.ToDecimal(80))
                                                    cs = "tdpRVerdanaAmarelo";
                                                else
                                                    cs = "tdpRVerdanaVermelho";

                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                break;
                                            case 2:
                                                if (porcAtualAcmulado >= Convert.ToDecimal(90))
                                                    cs = "tdpRVerdanaVerde";
                                                else if (porcAtualAcmulado >= Convert.ToDecimal(85) && porcAtualAcmulado < Convert.ToDecimal(90))
                                                    cs = "tdpRVerdanaAmarelo";
                                                else
                                                    cs = "tdpRVerdanaVermelho";

                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                break;

                                            default:
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                break;
                                        }
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                    }
                                    else if (contador <= int.Parse(dtRegra.Compute("Max(PRAZO)", "").ToString()) && contador>0 )
                                    {
                                        if (CalcularMaiorZeros(dt, contador))
                                        {
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                            DataRow[] oroeCor = dtRegra.Select("PRAZO=" + contador, "");
                                            string cs = "tdpR";
                                            for (int icor = 0; icor < oroeCor.Length; icor++)
                                            {
                                                if (decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString()) >= porcAtualAcmulado)
                                                {
                                                    cs = "tdpRVerdana" + oroeCor[icor]["Cor"].ToString();
                                                   // break;
                                                }
                                            }
                                            
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                        }
                                    }
                                    else
                                    {

                                        if (contador == int.Parse(txtQtdTransitTime.Text))
                                        {
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            ///
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            //
                                        }
                                        else
                                        {
                                            if (CalcularMaiorZeros(dt, contador))
                                            {
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            }
                                        }
                                    }

                                    contador += 1;
                                }
                            }


                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'")));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'")));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                            decimal perc1 = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                            decimal perc2 = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                            decimal perc4 = ((perc1 / perc2) * 100);

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + perc4.ToString("#0.00") + "%"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'")));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

                            NomeFilial = dt.Rows[ii]["NOMEDAFILIAL"].ToString();
                        }
                    }
                }
            }
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            #endregion

            //#region Rodape

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));


            decimal totporcAcm = Convert.ToDecimal("0.00");
            TT = 0;
            decimal totAtualPorcent = Convert.ToDecimal(0);
            for (int i = 0; i < qtdColunas; i++)
            {
                if (i == 0)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>Total de Notas"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                }
                else
                {

                    if ((CalcularMaiorZeros(dt, TT) && TT <= Convert.ToInt32(txtQtdTransitTime.Text)) || TT == Convert.ToInt32(txtQtdTransitTime.Text))
                    {
                        switch (contDefColun)
                        {
                            case 0:
                                NomeColuna = "&nbsp;" + TT.ToString();
                                contDefColun += 1;
                                break;

                            case 1:

                                int? qtd = 0;
                                if (TT == Convert.ToInt32(txtQtdTransitTime.Text))
                                {
                                    qtd = Convert.ToInt32(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO>=" + TT.ToString()) == DBNull.Value ? 0 : dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO>=" + TT.ToString()));
                                }
                                else
                                {
                                    qtd = Convert.ToInt32(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + TT.ToString()) == DBNull.Value ? 0 : dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + TT.ToString()));
                                }

                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + qtd));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                contDefColun += 1;
                                break;

                            case 2:

                                int? qtds = 0;

                                if (TT == Convert.ToInt32(txtQtdTransitTime.Text))
                                {
                                    qtds = Convert.ToInt32(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO>=" + TT.ToString()) == DBNull.Value ? 0 : dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO>=" + TT.ToString()));

                                }
                                else
                                {
                                    qtds = Convert.ToInt32(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + TT.ToString()) == DBNull.Value ? 0 : dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + TT.ToString()));
                                }

                                decimal AtualPorcent = (Convert.ToDecimal(qtds) / Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "")) * 100);
                                totAtualPorcent += AtualPorcent;

                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + totAtualPorcent.ToString("#0.00") + "%"));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                contDefColun = 0;
                                TT++;
                                break;
                        }
                    }
                    else
                    {
                        TT++;
                    }
                }
            }

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



            decimal perc1tot = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", ""));
            decimal perc2tot = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", ""));
            decimal perc4tot = ((perc1tot / perc2tot) * 100);

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + perc4tot.ToString("#0.00") + "%"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + dt.Compute("SUM(TOTALDENOTAS)", "")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            //#endregion


            PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

            if (dt.Rows.Count > 0 && qtdQtdLinhas < 20)
            {
                //btnGerarReport.Visible = true;
                Session["dt"] = dt;
                GerarGraficos(dt);
                tblLegenda.Visible = true;
            }
            else if (dt.Rows.Count > 0)
            {
                tblLegenda.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void GerarGraficos(DataTable dt)
    {
        if (Convert.ToDouble(dt.Compute("sum(NOTASFISCAIS_ENTREGUE)", "")) > Convert.ToDouble(0))
            pnlEntregues.Controls.Add((WebChartViewer)GraficoEntregues(dt));


        pnlEntregues.Visible = true;
        pnlNaoEntregues.Visible = true;
        tbGraf.Visible = true;
        pnlNaoEntregues.Controls.Add((WebChartViewer)GraficoNaoEntregues(dt));
    }

    protected void btnGerarReport_Click(object sender, EventArgs e)
    {
        MontarTable(false);
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        MontarTable(false);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        MontarTable(false);
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=myexcel.xls");
        Response.ContentType = "application/ms-excel";
        string k = GetGridViewHtml(PlaceHolder1);
        Response.Write(k);
        Response.End();
    }

    public string GetGridViewHtml(Control c)
    {
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        c.RenderControl(hw);
        return sw.ToString();

    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        StringWriter tw = new StringWriter();
        System.Web.UI.Html32TextWriter hw = new Html32TextWriter(tw);
        HtmlForm frm = new HtmlForm();
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=teste.xls");
        Response.Charset = "";
        EnableViewState = false;
        Controls.Add(frm);
        frm.Controls.Add(PlaceHolder1);
        frm.RenderControl(hw);
        Response.Write(tw.ToString());
        Response.End();
    }

    private WebChartViewer GraficoEntregues(DataTable table)
    {

        //definir as quantidades
        int quantidade = 0;
        string nomeFilial = "";

        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOMEDAFILIAL"].ToString())
            {
                quantidade++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
            }
        }


        MultiChart chart = new MultiChart(400, 250);
        double[] numArray = new double[quantidade];
        double[] data = new double[quantidade];
        double[] numArray3 = new double[quantidade];
        string[] labels = new string[quantidade];
        string[] texts = new string[quantidade];
        int[] colors = new int[quantidade];
        int sectorNo = 0;
        int index = 0;
        nomeFilial = "";
        double b = Convert.ToDouble(table.Compute("sum(NOTASFISCAIS_ENTREGUE)", ""));
        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOMEDAFILIAL"].ToString())
            {
                double a = Convert.ToDouble(table.Compute("sum(NOTASFISCAIS_ENTREGUE)", "NOMEDAFILIAL='" + row["NOMEDAFILIAL"].ToString() + "'"));
                double c = a / b;
                numArray3[index] = c;

                string filtemp = row["NOMEDAFILIAL"].ToString();
                labels[index] = filtemp + "<*BR*>";

//                colors[index] = 0x33868e;
                colors[index] = 0x06666e;
                 
                index++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
            }
        }

        //pizza        
        PieChart chart3 = new PieChart(500, 620);
        chart3.setPieSize(180, 70, 70);

        

        chart3.set3D(20);
        chart3.setStartAngle(170.0);
        chart3.setLabelLayout(0);
        chart3.setLabelStyle().setBackground(-65529, -16777216, Chart.glassEffect());
        
        chart3.setLineColor(-65529, 0);
        
        chart3.setData(numArray3, labels);
        chart3.setLabelStyle("verdana.ttf", 6.5);

       chart3.setColors(Chart.transparentPalette);
        
        chart3.setExplode(sectorNo);
        chart.addChart(0, 10, chart3);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);

        return viewer;
    }

    private WebChartViewer GraficoNaoEntregues(DataTable table)
    {
        //definir as quantidades
        int quantidade = 0;
        string nomeFilial = "";

        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOMEDAFILIAL"].ToString())
            {
                quantidade++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
            }
        }

        MultiChart chart = new MultiChart(400, 250);
        double[] numArray = new double[quantidade];
        double[] data = new double[quantidade];
        double[] numArray3 = new double[quantidade];
        string[] labels = new string[quantidade];
        string[] texts = new string[quantidade];
        int[] colors = new int[quantidade];
        int sectorNo = 0;
        int index = 0;
        nomeFilial = "";
        double b = Convert.ToDouble(table.Compute("sum(NOTASFISCAISNAOENTREGUE)", ""));
        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOMEDAFILIAL"].ToString())
            {
                double c=0;
                double a = Convert.ToDouble(Convert.ToDouble(row["NOTASFISCAISNAOENTREGUE"]));

                if (a > 0)
                {
                    c = a / b;
                }


                numArray3[index] = c;
                string filtemp = row["NOMEDAFILIAL"].ToString();

                //if (filtemp.Contains("SAO JOSE DOS CAMPOS"))
                //    filtemp = "S.J.C";

                //if (filtemp.Contains("SAO BERNARDO DO CAMPO"))
                //    filtemp = "S.B.C";


                labels[index] = filtemp + "<*BR*>";

                //labels[index] = row["NOMEDAFILIAL"].ToString() + "<*BR*>";
                colors[index] = 0x33868e;
                index++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
            }
        }

        //pizza        
        PieChart chart3 = new PieChart(500, 620);
        chart3.setPieSize(180, 70, 70);

        chart3.set3D(20);
        chart3.setStartAngle(170.0);
        chart3.setLabelLayout(0);
        chart3.setLabelStyle().setBackground(-65529, -16777216, Chart.glassEffect());
        chart3.setLineColor(-65529, 0);
        chart3.setData(numArray3, labels);
        chart3.setLabelStyle("verdana.ttf", 6.5);
        chart3.setColors(Chart.transparentPalette);
        chart3.setExplode(sectorNo);
        chart.addChart(0, 10, chart3);
        WebChartViewer viewer = new WebChartViewer();

        if (Convert.ToDecimal(b) > Convert.ToDecimal(0))
        {
            viewer.Image = chart.makeWebImage(0);
            tdNaoEntregue.Visible = true;
            tdNaoEntregue2.Visible = true;
        }
        return viewer;
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("ResumoPorFilialPrazoNovo.aspx?opc=teste");
    }


    protected void btnOrigemDados_Click(object sender, EventArgs e)
    {

    }
}