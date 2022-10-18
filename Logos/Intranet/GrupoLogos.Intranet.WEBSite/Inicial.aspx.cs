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

public partial class Inicial : System.Web.UI.Page
{
    int faltamin = Convert.ToInt32(ConfigurationSettings.AppSettings["IntervaloRefresh"]);   

    string clientes = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            Session["DataConf"] = inicio + "|" + DateTime.Now.ToShortDateString();

            ChartDirector.WebChartViewer.OnPageInit(Page);
            if (!IsPostBack)
            {
                clientes = "0";
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];

                if (clientes != "")
                {
                    DataTable DToco = SistranBLL.NotasFiscais.RetornarSituacoes(clientes, inicio.ToShortDateString(), DateTime.Now.ToShortDateString(), Session["Conn"].ToString());
                    cboSituacao.DataSource = DToco;
                    cboSituacao.DataTextField = "NOME";
                    cboSituacao.DataValueField = "IDOCORRENCIA";
                    cboSituacao.DataBind();

                    cboSituacao.Items.Insert(0, new ListItem("", ""));
                    cboSituacao.Items.Insert(0, new ListItem("MERCADORIA EMBARCADA", "MERCADORIA EMBARCADA"));
                    cboSituacao.Items.Insert(0, new ListItem("AGUARDANDO DEVOLUCAO", "AGUARDANDO DEVOLUCAO"));
                    cboSituacao.Items.Insert(0, new ListItem("EM DEVOLUCAO", "EM DEVOLUCAO"));
                    cboSituacao.Items.Insert(0, new ListItem("EM ENTREGA", "EM ENTREGA"));

                }
                CarregarCboFilial();

            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            clientes = cboCliente.SelectedValue;
                 
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }


    private void CarregarCboFilial()
    {
        cboFilial.DataSource = new SistranDAO.Filial().ListarTodasFiliais(Session["Conn"].ToString());
        cboFilial.DataValueField = "VALOR";
        cboFilial.DataTextField = "NOME";
        cboFilial.DataBind();
        cboFilial.Items.Insert(0, new ListItem("SELECIONE"));
    }

    protected void Montar(bool exibir, DateTime? ini, DateTime? fim)
    {
        if (chkSeries.SelectedIndex == -1)
            return;
        
        try
        {
             phAguardandoEmbarqueDataFilial.Controls.Clear();

            #region PorDataDisribuidoPorFilial

            DataTable dtDataFilialSituacao = NotasFiscais.ListarDataFilialSituacao(cboCliente.SelectedValue, Session["Conn"].ToString(), "AGUARDANDO EMBARQUE", ini, fim,
                chkSeries.Items[0].Selected, chkSeries.Items[2].Selected, chkSeries.Items[1].Selected, false);

            phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));


            if (dtDataFilialSituacao.Rows.Count > 0)
            {
                DateTime dtInicio = DateTime.Parse(dtDataFilialSituacao.Compute("min(DATA)", "").ToString());
                DateTime dtFinal = DateTime.Parse(dtDataFilialSituacao.Compute("max(data)", "").ToString());
                int totalGeral = int.Parse(dtDataFilialSituacao.Compute("sum(notas)", "").ToString());


                TimeSpan ts = Convert.ToDateTime(dtInicio) - Convert.ToDateTime(dtFinal);
                int intev = (ts.Days * -1) + 1;

                DataView view = new DataView(dtDataFilialSituacao);
                DataTable dsv = view.ToTable(true, "IDFILIAL", "NOME");


                //cabelchalho
                for (int i = 0; i < dsv.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>Data"));
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));
                    }


                    string par = "os=" + chkSeries.Items[0].Selected + "&d=" + chkSeries.Items[1].Selected + "&r=" + chkSeries.Items[2].Selected; ;

                    string NomeFilial = dsv.Rows[i]["NOME"].ToString().ToString().Replace("PROMOCIONAL", "PROM.").Replace("SAO JOSE DOS CAMPOS", "S. J. CAMPOS").Replace("SAO BERNARDO DO CAMPO", "S.B.C.").Replace("TABOAO", "EMBU"); ;
                    phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'><a href='InicialDetalhe.aspx?esp=0&idfilial=" + dsv.Rows[i]["IDFILIAL"].ToString() + "&data=&tipo=agemb&opc=&"+par+"' class='link' target='_blank' >" + NomeFilial + "</a>"));
                    phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));

                    if (i == dsv.Rows.Count - 1)
                    {
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL"));
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));

                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ACUMULADO %"));
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));
                    }
                }


                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</tr>"));

                //itens
                decimal percAcul = 0;
                for (int ii = 0; ii < intev; ii++)
                {

                    DataRow[] ot = dtDataFilialSituacao.Select("DATA='" + dtInicio.ToString("dd/MM/yyyy") + "'", "");

                    if (ot.Length > 0)
                    {
                        string par = "os=" + chkSeries.Items[0].Selected + "&d=" + chkSeries.Items[1].Selected + "&r=" + chkSeries.Items[2].Selected; 
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<tr>"));
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center'><a href='InicialDetalhe.aspx?esp=0&opc=Aguardando Embarque&idfilial=&data=" + dtInicio.ToShortDateString() + "&tipo=agemb&opc=&" + par + "' class='link' target='_blank'>" + dtInicio.ToShortDateString() + "</a>")); ;
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));

                        for (int i = 0; i < dsv.Rows.Count; i++)
                        {
                            DataRow[] o = dtDataFilialSituacao.Select("IDFILIAL=" + dsv.Rows[i]["IDFILIAL"].ToString() + " AND DATA='" + dtInicio.ToString("dd/MM/yyyy") + "'", "");

                            if (o.Length > 0)
                                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  styLe='color:" + (o[0]["ESPECIAL"].ToString()!="0"?"RED":"BLACK") + "'>" + o[0]["NOTAS"].ToString()));
                            else
                                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap >"));

                            phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));
                            //totaliza por data
                            if (i == dsv.Rows.Count - 1)
                            {
                                if (ot.Length > 0)
                                {
                                    int totalLinha = int.Parse(dtDataFilialSituacao.Compute("sum(notas)", "DATA='" + dtInicio.ToString("dd/MM/yyyy") + "'").ToString());
                                    percAcul += Convert.ToDecimal(totalLinha) / Convert.ToDecimal(totalGeral);

                                    phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap ><b>" + totalLinha + "</b>"));
                                    phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap ><b>" + percAcul.ToString("#0.00%") + "</b>"));

                                }
                                else
                                {
                                    phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap >"));
                                    phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap >"));
                                }

                            }
                        }

                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</tr>"));
                    }
                    dtInicio = dtInicio.AddDays(1);
                }

                //totaliza geral

                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' >"));
                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='center'>TOTAL"));
                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));

                for (int i = 0; i < dsv.Rows.Count; i++)
                {
                    //DataRow[] o = dtDataFilialSituacao.Select("IDFILIAL=" + dsv.Rows[i]["IDFILIAL"].ToString(), "");

                    string result = dtDataFilialSituacao.Compute("SUM(notas)", "IDFILIAL=" + dsv.Rows[i]["IDFILIAL"].ToString()).ToString();

                    if (result != "0")
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='right'>" + result));
                    else
                        phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap >"));

                    phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));

                }

                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='right'>" + dtDataFilialSituacao.Compute("SUM(notas)", "")));
                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));

                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='right'>-"));
                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));

                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</tr>"));


            }
            else
            {
                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<tr>"));
                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</td>"));
                phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</tr>"));

            }
            phAguardandoEmbarqueDataFilial.Controls.Add(new LiteralControl(@"</table>"));



            #endregion

            #region Distribição por situação

            if (cboSituacao.SelectedValue == "")
                cboSituacao.SelectedIndex = 0;

            dtDataFilialSituacao = NotasFiscais.ListarDataFilialSituacao(clientes, Session["Conn"].ToString(), cboSituacao.SelectedValue, ini, fim,
                chkSeries.Items[0].Selected, chkSeries.Items[2].Selected, chkSeries.Items[1].Selected, false);

            phPorSituacao.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width='100%' >"));


            if (dtDataFilialSituacao.Rows.Count > 0 && cboSituacao.Items.Count > 0)
            {
                DateTime dtInicio = DateTime.Parse(dtDataFilialSituacao.Compute("min(data)", "").ToString());
                DateTime dtFinal = DateTime.Parse(dtDataFilialSituacao.Compute("max(data)", "").ToString());

                TimeSpan ts = Convert.ToDateTime(dtInicio) - Convert.ToDateTime(dtFinal);
                int intev = (ts.Days);

                if (intev < 0)
                    intev = (intev * -1);

                intev++;

                DataView view = new DataView(dtDataFilialSituacao);
                DataTable dsv = view.ToTable(true, "IDFILIAL", "NOME");


                //cabelchalho
                for (int i = 0; i < dsv.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        phPorSituacao.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                        phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>Data"));
                        phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));
                    }

                    string par = "os=" + chkSeries.Items[0].Selected + "&d=" + chkSeries.Items[1].Selected + "&r=" + chkSeries.Items[2].Selected; 

                    string NomeFilial = dsv.Rows[i]["NOME"].ToString().ToString().Replace("PROMOCIONAL", "PROM.").Replace("SAO JOSE DOS CAMPOS", "S. J. CAMPOS").Replace("SAO BERNARDO DO CAMPO", "S.B.C.").Replace("TABOAO", "EMBU");
                    phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'><a href='InicialDetalhe.aspx?esp=0&opc=" + cboSituacao.SelectedValue + "&idfilial=" + dsv.Rows[i]["IDFILIAL"].ToString() + "&data=&tipo=" + cboSituacao.SelectedValue + "&opc=&" + par + "' class='link' target='_blank' >" + NomeFilial + "</a>"));
                    phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));

                    if (i == dsv.Rows.Count - 1)
                    {
                        phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL"));
                        phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));

                        phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ACUMULADO %"));
                        phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));
                    }
                }
                phPorSituacao.Controls.Add(new LiteralControl(@"</tr>"));

                decimal total = decimal.Parse(dtDataFilialSituacao.Compute("sum(notas)", "").ToString());
                decimal tPerc = 0;
                //itens
                for (int ii = 0; ii < intev; ii++)
                {

                    DataRow[] ot = dtDataFilialSituacao.Select("DATA='" + dtInicio.ToString("dd/MM/yyyy") + "'", "");

                    if (ot.Length > 0)
                    {
                        string par = "os=" + chkSeries.Items[0].Selected + "&d=" + chkSeries.Items[1].Selected + "&r=" + chkSeries.Items[2].Selected; 

                        phPorSituacao.Controls.Add(new LiteralControl(@"<tr>"));
                        phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center'><a href='InicialDetalhe.aspx?esp=0&idfilial=&data=" + dtInicio.ToShortDateString() + "&tipo=" + cboSituacao.SelectedValue + "&opc=&" + par + "' class='link' target='_blank'>" + dtInicio.ToShortDateString() + "</a>")); ;
                        phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));

                        for (int i = 0; i < dsv.Rows.Count; i++)
                        {
                            DataRow[] o = dtDataFilialSituacao.Select("IDFILIAL=" + dsv.Rows[i]["IDFILIAL"].ToString() + " AND DATA='" + dtInicio.ToString("dd/MM/yyyy") + "'", "");

                            if (o.Length > 0)
                                phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  styLe='color:" + (o[0]["ESPECIAL"].ToString()!="0"?"RED":"BLACK") + "'>" + o[0]["NOTAS"].ToString()));
                            else
                                phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap >"));

                            phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));
                            //totaliza por data
                            if (i == dsv.Rows.Count - 1)
                            {
                                if (ot.Length > 0)
                                {

                                    decimal totLinha = decimal.Parse(dtDataFilialSituacao.Compute("sum(notas)", "DATA='" + dtInicio.ToString("dd/MM/yyyy") + "'").ToString());

                                    tPerc += (totLinha / total);
                                    phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap ><b>" + dtDataFilialSituacao.Compute("sum(notas)", "DATA='" + dtInicio.ToString("dd/MM/yyyy") + "'").ToString() + "</b>"));
                                    phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap ><b>" + tPerc.ToString("#0.00%") + "</b>"));

                                }
                                else
                                {
                                    phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap >"));
                                    phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap >"));
                                }
                            }
                        }

                        phPorSituacao.Controls.Add(new LiteralControl(@"</tr>"));
                    }
                    dtInicio = dtInicio.AddDays(1);
                }

                //totaliza geral

                phPorSituacao.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' >"));
                phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='center'>TOTAL"));
                phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));

                for (int i = 0; i < dsv.Rows.Count; i++)
                {
                    //DataRow[] o = dtDataFilialSituacao.Select("IDFILIAL=" + dsv.Rows[i]["IDFILIAL"].ToString(), "");

                    string result = dtDataFilialSituacao.Compute("SUM(notas)", "IDFILIAL=" + dsv.Rows[i]["IDFILIAL"].ToString()).ToString();

                    if (result != "0")
                        phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='right' >" + result));
                    else
                        phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap >"));

                    phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));

                }

                phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='right' >" + dtDataFilialSituacao.Compute("SUM(notas)", "")));
                phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));

                phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap align='right' >-"));
                phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));

                phPorSituacao.Controls.Add(new LiteralControl(@"</tr>"));


            }
            else
            {
                phPorSituacao.Controls.Add(new LiteralControl(@"<tr>"));
                phPorSituacao.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
                phPorSituacao.Controls.Add(new LiteralControl(@"</td>"));
                phPorSituacao.Controls.Add(new LiteralControl(@"</tr>"));

            }
            phPorSituacao.Controls.Add(new LiteralControl(@"</table>"));



            #endregion
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmBaterDatas.aspx");
    }

    private void MontarTela()
    {
        try
        {
            string[] DataConf = FuncoesGerais.DataConf();

            DateTime? ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime? fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            //phAguardandoDevolucao.Controls.Clear();
            phAguardandoEmbarqueDataFilial.Controls.Clear();
            phPorSituacao.Controls.Clear();
            Montar(true, ini, fim);
            //lblPeriodo.Text = Convert.ToDateTime(DataConf[0]).ToShortDateString() + " à " + DateTime.Parse(DataConf[1]).ToShortDateString();
            //lblqtdEmitidas.Text = NotasFiscais.RetornarTotalNotasFiscaisEmitidas(clientes, Convert.ToDateTime(DataConf[0]).ToShortDateString(), Convert.ToDateTime(DataConf[1]).ToShortDateString()).ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }
    protected void cboSituacao_SelectedIndexChanged(object sender, EventArgs e)
    {
              // MontarTela();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //MontarTela();
        DateTime? ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime? fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        Montar(true, ini, fim);
    }
    protected void cboFilial_SelectedIndexChanged1(object sender, EventArgs e)
    {
        cboCliente.Items.Clear();

        if (cboFilial.SelectedIndex > 0)
        {
            string strsql = "";

            //strsql += " SELECT C.IDCLIENTE, cnpjcpf+ ' - ' +  FANTASIAAPELIDO FANTASIAAPELIDO";
            //strsql += " FROM CLIENTEFILIAL CF ";
            //strsql += " INNER JOIN CLIENTE C ON C.IDCLIENTE = CF.IDCLIENTE ";
            //strsql += " INNER JOIN CADASTRO CD ON CD.IDCADASTRO = C.IDCLIENTE ";
            //strsql += " WHERE IDFILIAL=" + cboFilial.SelectedValue;
            //strsql += "/* AND CF.CLIENTELOGISTICA = 'SIM'*/ order by 2";


            strsql += " SELECT DISTINCT IDCLIENTE, C.CNPJCPF + '-' + C.RAZAOSOCIALNOME FANTASIAAPELIDO";
            strsql += " FROM CADASTRO C ";
            strsql += " INNER JOIN DOCUMENTO D ON D.IDCLIENTE = C.IDCADASTRO ";
            strsql += " AND D.TIPODEDOCUMENTO = 'NOTA FISCAL'  ";
            strsql += " AND D.TIPODESERVICO='TRANSPORTE' ";
            strsql += " AND D.DATADEEMISSAO> '2015-01-01' ";
            strsql += " AND IDFILIALATUAL = " + cboFilial.SelectedValue;
            strsql += " AND C.IDCADASTRO>0 ";
            strsql += " ORDER BY 2 ";

            cboCliente.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
            cboCliente.DataTextField = "FANTASIAAPELIDO";
            cboCliente.DataValueField = "IDCLIENTE";
            cboCliente.DataBind();

            cboCliente.Items.Insert(0, new ListItem("SELECIONE"));

        }
        else
        {
            cboCliente.Items.Add(new ListItem("SELECIONE A FILIAL", ""));

        }
    }


    protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboCliente.SelectedIndex > 0)
        {
            clientes = cboCliente.SelectedValue;

            cboSituacao.Items.Clear();

            DataTable DToco = SistranBLL.NotasFiscais.RetornarSituacoes(clientes, DateTime.Now.AddMonths(12).ToString(), DateTime.Now.ToShortDateString(), Session["Conn"].ToString());
            cboSituacao.DataSource = DToco;
            cboSituacao.DataTextField = "NOME";
            cboSituacao.DataValueField = "IDOCORRENCIA";
            cboSituacao.DataBind();


            cboSituacao.Items.Insert(0, new ListItem("", ""));
            cboSituacao.Items.Insert(0, new ListItem("MERCADORIA EMBARCADA", "MERCADORIA EMBARCADA"));
            cboSituacao.Items.Insert(0, new ListItem("AGUARDANDO DEVOLUCAO", "AGUARDANDO DEVOLUCAO"));
            cboSituacao.Items.Insert(0, new ListItem("EM DEVOLUCAO", "EM DEVOLUCAO"));
            cboSituacao.Items.Insert(0, new ListItem("EM ENTREGA", "EM ENTREGA"));
        }
    }
}
