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

public partial class ResumoPorFilial : System.Web.UI.Page
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
            intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {
                txtQtdTransitTime.Text = "3";
                tdNaoEntregue.Visible = false;
                tdNaoEntregue2.Visible = false;
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                btnGerarReport.Visible = false;
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
            }
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            btnGerarReport.Attributes.Add("onClick", "FullWindow('frmRptResumoFilial.aspx?tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "&DI=" + txtI.Text + "&DF=" + txtF.Text + "', 'NovaJanela2', 'yes')");
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;
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
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

            if (int.Parse(txtQtdTransitTime.Text) < 3)
            {
                throw new Exception("Transit Time até: mínimo 3");
            }

            MontarTable(false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected bool CalcularMaiorZeros(DataTable dts, int tt)
    {
        int? qtd = 0;
        qtd= Convert.ToInt32(dts.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + tt.ToString()) == DBNull.Value ? 0 : dts.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + tt.ToString()));

        int maxTT = Convert.ToInt32(dts.Compute("max(PRAZOUTILIZADO)",""));

        if ((qtd == 0 && maxTT > 3) || tt==0)
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

            if (ts.Days < int.Parse(txtQtdTransitTime.Text))
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas muito curto.')", true);
                return;
            }

            if (ts.Days > intervalo)
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + intervalo.ToString() + " dias.')", true);
                return;
            }

            DataTable dt = NotasFiscais.ListarResumoPorFilial(Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), Sistran.Library.FuncoesUteis.retornarClientes(), Session["Conn"].ToString());

            if (dt.Rows.Count == 0)
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                return;
            }
            PlaceHolder1.Controls.Clear();

            string n = "";
            int QtdFiliais=0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["NOMEDAFILIAL"].ToString() != n)
                {
                    QtdFiliais += 1;
                }

                n = dt.Rows[i]["NOMEDAFILIAL"].ToString();
            }

            //acerta as colunas

            int qtdColunas = 0;

            qtdColunas = ( 3* Convert.ToInt32(txtQtdTransitTime.Text)) + 4;
            
            
            decimal qtdTotalNf = (Convert.ToDecimal(dt.Compute("MAX(TOTALDENOTAS)", "")));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=99% id='teste' runat='server' border=0  >"));
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
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>Filial"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                }
                else
                {
                    if ((CalcularMaiorZeros(dt, TT) && TT<=Convert.ToInt32(txtQtdTransitTime.Text)) || TT==Convert.ToInt32(txtQtdTransitTime.Text))
                    {
                        switch (contDefColun)
                        {
                            case 0:
                                NomeColuna = "&nbsp;&nbsp;&nbsp;TRANSIT TIME" + TT.ToString() ;
                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + NomeColuna));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                contDefColun += 1;
                                break;

                            case 1:
                                NomeColuna = "&nbsp;&nbsp;&nbsp;NF ENTREGUES";
                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + NomeColuna));
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
            

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;&nbsp;TOTAL NF ENTREGUES"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td style='background-color: #FFFFFF;' rowspan=" + (QtdFiliais + 2).ToString() + ">&nbsp;&nbsp;&nbsp"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;&nbsp;NF NÃO ENTREGUES"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;&nbsp;% NF"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;&nbsp;TOTAL DE NOTAS"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            #endregion

            int qtdColunasTemp = Convert.ToInt32(txtQtdTransitTime.Text);
            decimal[] tot = new decimal[qtdColunasTemp + 1];

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
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Rows[ii]["NOMEDAFILIAL"].ToString()));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                            DataRow[] orw = dt.Select("NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'");

                            int contador = 0;
                            int row = 0;
                            decimal porcAtualAcmulado = Convert.ToDecimal("0.00");

                            foreach (DataRow item in orw)
                            {
                                if (contador < Convert.ToInt32(item["PRAZOUTILIZADO"]) && contador <= Convert.ToInt32(txtQtdTransitTime.Text))
                                {
                                    for (int kkkk = contador; kkkk < Convert.ToInt32(item["PRAZOUTILIZADO"]); kkkk++)
                                    {
                                        if ((contador == 1 || contador == 2))
                                        {
                                            
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdanaVermelho' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + contador.ToString()));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                        }
                                        else if (contador <= int.Parse(txtQtdTransitTime.Text))
                                        {
                                            if (int.Parse(txtQtdTransitTime.Text) == contador)
                                            {
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>&nbsp;&nbsp;ACIMA " + contador.ToString()));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            }
                                            else
                                            {
                                                if (CalcularMaiorZeros(dt, contador))
                                                {
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + contador.ToString()));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
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
                                    else if(contador == Convert.ToInt32(txtQtdTransitTime.Text))
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

                                    if (contador == 1 || contador == 2)
                                    {
                                        switch (contador)
                                        {
                                            case 1:
                                                if (porcAtual >= Convert.ToDecimal(80))
                                                {
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdanaVerde' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + item["PRAZOUTILIZADO"].ToString()));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                }

                                                if (porcAtual >= Convert.ToDecimal(75) && porcAtual < Convert.ToDecimal(80))
                                                {
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdanaAmarelo' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + item["PRAZOUTILIZADO"].ToString()));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                }

                                                if (porcAtual < Convert.ToDecimal(75))
                                                {
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdanaVermelho' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + item["PRAZOUTILIZADO"].ToString()));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                }

                                                break;

                                            case 2:
                                                if (porcAtualAcmulado + porcAtual >= Convert.ToDecimal(90))
                                                {
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdanaVerde' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + item["PRAZOUTILIZADO"].ToString()));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                }

                                                if (porcAtualAcmulado + porcAtual >= Convert.ToDecimal(85) && porcAtualAcmulado + porcAtual < Convert.ToDecimal(90))
                                                {
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdanaAmarelo' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + item["PRAZOUTILIZADO"].ToString()));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                }

                                                if (porcAtualAcmulado + porcAtual < Convert.ToDecimal(85))
                                                {
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdanaVermelho' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + item["PRAZOUTILIZADO"].ToString()));
                                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                                }
                                                break;

                                        }
                                    }
                                    else
                                    {                                       
                                        if (contador == Convert.ToInt32(txtQtdTransitTime.Text))
                                        {
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap align='LEFT' style='font-size:7pt;height:10px;font-weight:normal'> &nbsp;&nbsp;ACIMA " + item["PRAZOUTILIZADO"].ToString()));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));                                         
                                        }
                                        else
                                        {
                                            if (CalcularMaiorZeros(dt, contador))
                                            {
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + item["PRAZOUTILIZADO"].ToString()));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            }
                                        }
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

                                    //if (CalcularMaiorZeros(dt, Convert.ToInt32(item["PRAZOUTILIZADO"])))
                                    //{
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                    //}                                    
                                }

                                    #endregion
                                row += 1;
                                contador += 1;
                            }


                            if (contador <= qtdColunasTemp)
                            {
                                for (int iii = contador; iii <= qtdColunasTemp; iii++)
                                {
                                    if (contador == 1 || contador == 2)
                                    {
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdanaVermelho' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + contador.ToString()));
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%"));
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                    }
                                    else
                                    {

                                        if (contador == int.Parse(txtQtdTransitTime.Text))
                                        {
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>&nbsp;&nbsp;ACIMA " + contador.ToString()));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                        }
                                        else
                                        {
                                            if (CalcularMaiorZeros(dt,contador))
                                            {
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + contador.ToString()));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
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
            //int cont2 = 0;

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
                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + NomeColuna));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                contDefColun += 1;
                                break;

                            case 1:
                                // NomeColuna = "&nbsp;&nbsp;&nbsp;";
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

            if (dt.Rows.Count > 0)
            {
                btnGerarReport.Visible = true;                
                Session["dt"] = dt;
                GerarGraficos(dt);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void GerarGraficos(DataTable dt)
    {
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


        MultiChart chart = new MultiChart(500, 300);
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
                //double a = Convert.ToDouble(Convert.ToDouble(row["NOTASFISCAIS_ENTREGUE"]));
                double a = Convert.ToDouble(table.Compute("sum(NOTASFISCAIS_ENTREGUE)", "NOMEDAFILIAL='" + row["NOMEDAFILIAL"].ToString() + "'"));
                double c = a / b;
                numArray3[index] = c;
                labels[index] = row["NOMEDAFILIAL"].ToString() + "<*BR*>";
                colors[index] = 0x33868e;
                index++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
            }
        }

        //pizza        
        PieChart chart3 = new PieChart(750, 790);
        chart3.setPieSize(240, 90, 90);

        chart3.set3D(20);
        chart3.setStartAngle(170.0);
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

        MultiChart chart = new MultiChart(500, 300);
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
                double a = Convert.ToDouble(Convert.ToDouble(row["NOTASFISCAISNAOENTREGUE"]));
                double c = a / b;
                numArray3[index] = c;
                labels[index] = row["NOMEDAFILIAL"].ToString() + "<*BR*>";
                colors[index] = 0x33868e;
                index++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
            }
        }

        //pizza        
        PieChart chart3 = new PieChart(750, 790);
        chart3.setPieSize(240, 90, 90);

        chart3.set3D(20);
        chart3.setStartAngle(170.0);
        chart3.setLabelLayout(0);
        chart3.setLabelStyle().setBackground(-65529, -16777216, Chart.glassEffect());
        chart3.setLineColor(-65529, 0);
        chart3.setData(numArray3, labels);
        chart3.setLabelStyle("verdana.ttf", 6.5);
        chart3.setColors(Chart.transparentPalette);
        chart3.setExplode(sectorNo);
        chart.addChart(5, 30, chart3);
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

    protected void Button6_Click(object sender, EventArgs e)
    {

    }
}
   