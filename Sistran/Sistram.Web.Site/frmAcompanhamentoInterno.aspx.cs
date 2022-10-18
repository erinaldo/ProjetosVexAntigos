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
using Artem.Web.UI.Controls;
using Subgurim.Controles;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class frmAcompanhamentoInterno : System.Web.UI.Page
{

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            if (!IsPostBack)
            {
                GMap1.Visible = false;
                lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                CarregarCboFilial();
                txtData.Text = DateTime.Now.ToShortDateString();
                Timer1.Enabled = false;
                Timer1.Interval = Convert.ToInt32(ConfigurationSettings.AppSettings["IntervaloRefresh"]) * 60000;
                esconderTotais();
                HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
                tr0.Style.Add("display", "none");
                lblMapaNota1.Visible = false;

            }

            if (lstFilial.Items.Count == 0)
            {
                lstFilial.Visible = false;
            }
            Pesquisar();
            lstFilial.Width = cboFilial.Width;
            
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");
        }
    }

    protected void btnAdicionarFilial_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstFilial.Items.Count; i++)
        {
            if (lstFilial.Items[i].Value == cboFilial.SelectedValue)
            {
                return;
            }
        }
        lstFilial.Items.Add(new ListItem(cboFilial.SelectedItem.Text, cboFilial.SelectedValue));
        lstFilial.Visible = true;
        lstFilial.Width = cboFilial.Width;
        

        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

        LblListaNf.Visible = false;
        ListBox1.Visible = false;
        GMap2.Visible = false;
        lblMapaNota.Visible = false;
        GridView1.Visible = false;
        Pesquisar();
    }

    protected void btnRemoverFilial_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstFilial.Items.Count; i++)
        {
            if (lstFilial.Items[i].Value == lstFilial.SelectedValue)
            {
                lstFilial.Items.RemoveAt(i);
                if (lstFilial.Items.Count > 0)
                {
                    lstFilial.Visible = true;
                    //                    tb_totais.Visible = true;
                }
                else
                {
                    lstFilial.Visible = false;
                    lstFilial.Width = cboFilial.Width;

                    // Label1.Visible = false;
                    PlaceHolder1.Controls.Clear();
                    esconderTotais();

                    CheckBox1.Text = "Atualizção Automática.";
                }
                return;
            }
        }

    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

        LblListaNf.Visible = false;
        ListBox1.Visible = false;
        GMap2.Visible = false;
        lblMapaNota.Visible = false;
        GridView1.Visible = false;
        Pesquisar();
    }

    private string MontarLinhaCaminhao(string qtdDocs, string realizados, int qtdCels)
    {
        string s = "";
        //realizados = qtdDocs;


        decimal idc = ((Convert.ToDecimal(realizados) / Convert.ToDecimal(qtdDocs)) * 100);
        s = "<table border='0' width='100%' cellspacing='0' cellpading='0' >";
        s += "<tr>";

        for (int i = 0; i <= (qtdCels + 1); i++)
        {
            bool passou = false;


            //caminhao
            if (Convert.ToInt32(realizados) == (i))
            {
                s += "<td style='width:15px; align:rigth'>";
                passou = true;
                s += @"<img src='Imagens/caminhao.png' width='15pt'>";

            }
            if (Convert.ToInt32(qtdDocs) + 1 == (i))
            {
                s += "<td>";
                passou = true;
                string m = "#FFFF99";

                if (realizados == qtdDocs)
                    m = "#99FF99";

                if (qtdDocs.Length < 2)
                    s += "<span style='background-color:" + m + ";'>&nbsp;" + qtdDocs + "</span>";
                else
                    s += "<span style='background-color:" + m + ";'>" + qtdDocs + "</span>";

            }

            if (passou == false)
                s += "<td style='width:15px;color: #E0E0E0'>..";
            //s += "<td style='width:15px;color: #E0E0E0'>x";

            s += "</td>";
        }
        s += "</tr>";
        s += "</table>";
        return s;
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Pesquisar();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            Timer1.Enabled = true;
        }
        else
        {
            Timer1.Enabled = false;
        }
        Pesquisar();
    }
    #endregion

    #region Methods

    private void esconderTotais()
    {
        tbltot.Visible = false;
    }

    private void mostrarTotais()
    {
        tbltot.Visible = true;
    }

    private void CarregarCboFilial()
    {
        cboFilial.DataSource = new SistranDAO.Filial().ListarTodasFiliais(Session["Conn"].ToString());
        cboFilial.DataValueField = "VALOR";
        cboFilial.DataTextField = "NOME";
        cboFilial.DataBind();

        lstFilial.Width = cboFilial.Width;
    }

    private bool ValidarData(string str)
    {
        Regex testa = new Regex(@"^(([0-2]d|[3][0-1])/([0]d|[1][0-2])/[1-2][0-9]d{2})$");
        return testa.IsMatch(str);
    }

    private void Pesquisar()
    {
        string filiais = "";
        string cliente = "";

        if (txtData.Text == "")
        {
            txtData.Text = DateTime.Now.ToShortDateString();
        }

        for (int i = 0; i < lstFilial.Items.Count; i++)
        {
            filiais += lstFilial.Items[i].Value + ",";
        }

        if (filiais.Length > 0)
            filiais = filiais.Substring(0, filiais.Length - 1);
        else
        {
            filiais = "";
            filiais = cboFilial.SelectedValue;
        }
        cliente = Sistran.Library.FuncoesUteis.retornarClientes();

        DataTable dt = new SistranBLL.Veiculo().ListarMonitoramentoInterno(txtData.Text, filiais, cliente, cboOrdem.SelectedValue, Session["Conn"].ToString());

        MontarTable(dt);
        carregarMapaDT();

        if (dt.Rows.Count == 0)
        {
            GMap1.Visible = false;
            lblMapaNota1.Visible = false;
        }

        Label1.Text = "Última Atualização: " + DateTime.Now.ToShortTimeString();
        Label1.Visible = true;
        CheckBox1.Text = "Atualizção Automática.";

        if (dt.Rows.Count > 0 && CheckBox1.Checked == true)
            Timer1.Enabled = true;

        if (dt.Rows.Count > 0)
        {
            mostrarTotais();

        }
        else
        {
            esconderTotais();
            CheckBox1.Text = "Atualizção Automática.";

        }

    }

    protected void MontarTable(DataTable dt)
    {

        PlaceHolder1.Controls.Clear();
        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0>"));


        if (dt.Rows.Count > 0)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' style='font-size:7pt;width:1%;'>Emissão"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' style='font-size:7pt;'>Filial"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;width:4%'>DT"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;width:10%'>Placa"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;width:15%'>Motorista"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;width:15%'>Última Sincronização"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;width:15%'>Início Sincronização"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;width:15%'>Fim Sincronização"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;width:15%'>Tempo Sincronização"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;width:15%'>Último Envio de Dados"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;width:4%'>Entregas"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;width:12%'>Notas Fiscais"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;width:4%'>Realizadas"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;width:4%'>Retorno"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>Ocororrências"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;width:4%'>Pendentes"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;width:50%'> "));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;background-image:url(Images/skins/primeiro/img/menu_3_2.jpg);'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            int qdtcel = Convert.ToInt32(dt.Compute("MAX(Documentos)", ""));

            //calcula totais
            lblVeiculos.Text = dt.Rows.Count.ToString();
            lblServ.Text = Convert.ToInt32(dt.Compute("SUM(TotalDeServicos)", "")).ToString();
            lblDoc.Text = Convert.ToInt32(dt.Compute("SUM(Documentos)", "")).ToString();
            lblOcorrencia.Text = Convert.ToInt32(dt.Compute("SUM(Ocorrencias)", "")).ToString();
            lblRealiz.Text = Convert.ToInt32(dt.Compute("SUM(DocumentosConcluido)", "")).ToString();
            lblNRealiz.Text = Convert.ToInt32(dt.Compute("SUM(DocumentosNaoFinalizado)", "")).ToString();
            lblPend.Text = Convert.ToInt32(dt.Compute("SUM(Pendentes)", "")).ToString();

            lblOcorrencia1.Text = ((Convert.ToDecimal(dt.Compute("SUM(Ocorrencias)", "")) / Convert.ToDecimal(dt.Compute("SUM(Documentos)", ""))) * 100).ToString("#0.00") + "%";
            lblRealiz1.Text = ((Convert.ToDecimal(dt.Compute("SUM(DocumentosConcluido)", "")) / Convert.ToDecimal(dt.Compute("SUM(Documentos)", ""))) * 100).ToString("#0.00") + "%";
            lblNRealiz1.Text = ((Convert.ToDecimal(dt.Compute("SUM(DocumentosNaoFinalizado)", "")) / Convert.ToDecimal(dt.Compute("SUM(Documentos)", ""))) * 100).ToString("#0.00") + "%";
            lblPend1.Text = ((Convert.ToDecimal(dt.Compute("SUM(Pendentes)", "")) / Convert.ToDecimal(dt.Compute("SUM(Documentos)", ""))) * 100).ToString("#0.00") + "%";


            foreach (DataRow item in dt.Rows)
            {
                string mot = item["Motorista"].ToString();

                if (mot.Length > 15)
                    mot = mot.Substring(0, 15) + "...";


                string fil = item["filial"].ToString();
                if (fil.Length > 15)
                    fil = fil.Substring(0, 15) + "...";



                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='center' style='font-size:7pt;height:10px;font-weight:normal;width:1%'>" + Convert.ToDateTime(item["Emissao"]).ToShortDateString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' style='font-size:7pt;height:10px;width:5%'>" + fil));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%' nowrap='nowrap'>"));

                if (item["UltimaSincronizacao"].ToString()=="")
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<a class='link' href='filtro.aspx?acao=dt&opc=Documentos DT: " + item["ndt"].ToString() + " &dt=" + item["iddt"].ToString() + "' target='_blank' alt='Clique para ver os Documentos'>" + item["Ndt"].ToString() + "</a>"));
                else
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<b><a class='link' style='color:green;font; font-weight:bold' href='filtro.aspx?acao=dt&opc=Documentos DT: " + item["ndt"].ToString() + " &dt=" + item["iddt"].ToString() + "' target='_blank' alt='Clique para ver os Documentos'>" + item["Ndt"].ToString() + "</a></b>"));
                //PlaceHolder1.Controls.Add(new LiteralControl(item["Ndt"].ToString() ));             
                
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:7pt;height:10px;width:1%' nowrap='nowrap'>"));
                PlaceHolder1.Controls.Add(criarBotaoMapa(item["iddt"].ToString(), item["Placa"].ToString()));

                


                //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:7pt;height:10px;width:1%' nowrap='nowrap'>" + item["Placa"].ToString()));
                
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                if (item["UltimaSincronizacao"].ToString() == "")
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:7pt;height:10px;width:1%' nowrap='nowrap'>" + mot));
                else
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:7pt;height:10px;width:1%; color:green;font; font-weight:bold' nowrap='nowrap'>" + mot));



                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%'> " + item["UltimaSincronizacao"].ToString()));

                //

                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%'> " + item["InicioSincronizacao"].ToString()));


                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%'> " + item["FinalSincronizacao"].ToString()));


                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%'> " + item["TempoSincronizacao"].ToString()));



                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%'> " + item["UtltimoEnvioDeDados"].ToString()));


                ///


                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%'> " + item["TotalDeServicos"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='center' style='font-size:7pt;height:10px;width:1%'> " + item["Documentos"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right'  style='font-size:7pt;height:10px;width:1%'>" + item["DocumentosConcluido"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%'> " + item["DocumentosNaoFinalizado"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%'> " + item["Ocorrencias"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%'> " + item["Pendentes"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:7pt;height:10pxwidth:99%'> " + MontarLinhaCaminhao(item["Documentos"].ToString(), item["DocumentosConcluido"].ToString(), qdtcel)));

                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' style='font-size:7pt;height:10px'>" + ((Convert.ToDecimal(item["DocumentosConcluido"].ToString()) / Convert.ToDecimal(item["Documentos"].ToString())) * 100).ToString("#0.00") + "%"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            }

        }
        else
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        }


        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
    }

    private void carregarMapaDT(string iddt)
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("exec RETORNAR_POSICAO_VEICULO " + ILusuario[0].EmpresaId);

        GMap1.Controls.Clear();
        //Session["dtPesquisas"] = txtData.Text;
        //DateTime d = DateTime.Parse(Session["dtPesquisas"].ToString());
        DataRow[] dr = dt.Select("IDDT=" + iddt, "");

        if (dr.Length == 0)
        {
            GMap1.Visible = false;
            lblMapaNota1.Visible = false;
            return;
        }
        else
        {
            GMap1.Visible = true;
            lblMapaNota1.Visible = true;
        }

        GMap1.Add(new GControl(GControl.preBuilt.GOverviewMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
        GMap1.setCenter(new GLatLng(double.Parse(dr[0]["lat"].ToString()), double.Parse(dr[0]["long"].ToString())), 15);


        for (int i = 0; i < dr.Length; i++)
        {
            string coment = "DATA HORA: " + DateTime.Parse(dr[i]["dataHra"].ToString()).ToString("dd/MM/yyyy HH:mm:ss") + " <BR>Latitude:" + double.Parse(dr[i]["lat"].ToString()) + " <BR>Longitude:" + double.Parse(dr[i]["long"].ToString()); ;
            coment += "<br>MOTORSTA: " + dr[i]["RazaoSocialNome"].ToString();
            coment += "<br>PLACA: " + dr[i]["Placa"].ToString();
            coment += "<br>N. DT: " + dr[i]["Numero"].ToString();
            coment += "<br><a href='filtro.aspx?opc=Localizar Carga&gps=s&ic=" + ILusuario[0].EmpresaId + "&iddt=" + dr[i]["IDDT"].ToString() + "'>QUANTIDADE DE ENTREGAS: " + dr[i]["Notas"].ToString() + "</a>";


            GLatLng gl = new GLatLng(double.Parse(dr[i]["lat"].ToString()), double.Parse(dr[i]["long"].ToString()));

            GMarker marker = new GMarker(gl);
            GInfoWindow window = new GInfoWindow(marker, coment);
            GMap1.Add(window);
        }
    }

    private void carregarMapaDT()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("exec RETORNAR_POSICAO_VEICULO " + ILusuario[0].EmpresaId);

        GMap1.Controls.Clear();
        Session["dtPesquisas"] = txtData.Text;
        DateTime d = DateTime.Parse(Session["dtPesquisas"].ToString());
        DataRow[] dr = dt.Select("DATANOTA='" + d.ToString("dd/MM/yyyy") + "'", "");
       // DataRow[] dr = dt.Select("datadesaida='" + d.ToString("yyyy-MM-dd") + "'", "");

        if (dr.Length == 0)
        {
            GMap1.Visible = false;
            lblMapaNota1.Visible = false;
            return;
        }
        else
        {
            GMap1.Visible = true;
            lblMapaNota1.Visible = true;
        }

        GMap1.Add(new GControl(GControl.preBuilt.GOverviewMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
        GMap1.setCenter(new GLatLng(double.Parse(dr[0]["lat"].ToString()), double.Parse(dr[0]["long"].ToString())), 9);


        for (int i = 0; i < dr.Length; i++)
        {
            string coment = "DATA HORA: " + DateTime.Parse(dr[i]["dataHra"].ToString()).ToString("dd/MM/yyyy HH:mm:ss") + " <BR>Latitude:" + double.Parse(dr[i]["lat"].ToString()) + " <BR>Longitude:" + double.Parse(dr[i]["long"].ToString()); ;
            coment += "<br>MOTORSTA: " + dr[i]["RazaoSocialNome"].ToString();
            coment += "<br>PLACA: " + dr[i]["Placa"].ToString();
            coment += "<br>N. DT: " + dr[i]["Numero"].ToString();
            coment += "<br><a href='filtro.aspx?opc=Localizar Carga&gps=s&ic=" + ILusuario[0].EmpresaId + "&iddt=" + dr[i]["IDDT"].ToString() + "'>QUANTIDADE DE ENTREGAS: " + dr[i]["Notas"].ToString() + "</a>";
                            
            
            GMarker marker = new GMarker(new GLatLng(double.Parse(dr[i]["lat"].ToString()), double.Parse(dr[i]["long"].ToString())));
            GInfoWindow window = new GInfoWindow(marker, coment);
            GMap1.Add(window);
        }
    }

    private void carregarMapaNF(string idDocumento)
    {
        DataTable dentregues = (DataTable)Session["entregues"];

        GMap1.Controls.Clear();
        GMap2.Controls.Clear();

        if (dentregues.Rows.Count > 0)
        {
            lblMapaNota.Visible = true;
            GMap2.Visible = true;

        }
        else
        {
            lblMapaNota.Visible = false;
            GMap2.Visible = false;
            return;
        }

        DataRow[] dr = dentregues.Select("iddocumento=" + idDocumento, "");
        GMap2.setCenter(new GLatLng(double.Parse(dr[0]["latitude"].ToString()), double.Parse(dr[0]["longitude"].ToString())), 12);
        GMap2.Add(new GControl(GControl.preBuilt.GOverviewMapControl));
        GMap2.Add(new GControl(GControl.preBuilt.LargeMapControl));
        for (int i = 0; i < dr.Length; i++)
        {
            string coment = "";

            if (dr[i]["dataHora"].ToString() != "")
            {
                coment += "<br>DATA HORA: " + DateTime.Parse(dr[i]["dataHora"].ToString()).ToString("dd/MM/yyyy HH:mm:ss") + " <BR>Latitude:" + double.Parse(dr[i]["latitude"].ToString()) + " <BR>Longitude:" + double.Parse(dr[i]["longitude"].ToString());
            }
            else
            {
                coment += "<br>DATA HORA: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " <BR>Latitude:" + double.Parse(dr[i]["latitude"].ToString()) + " <BR>Longitude:" + double.Parse(dr[i]["longitude"].ToString());
            }
            coment += "<br>Nota Fiscal: " + dr[i]["Numero"].ToString();
            coment += "  -  Série: " + dr[i]["SERIE"].ToString();
            coment += "<br>Ocorrência: " + dr[i]["Nome"].ToString();
            coment += "<br><a href='filtro.aspx?gps=s&opc=Detalhe Nota Fical&iddoc=" + dr[i]["IDDOCUMENTO"].ToString() + "' >Clique para ver DETALHE </a>";
            GMarker marker = new GMarker(new GLatLng(double.Parse(dr[i]["latitude"].ToString()), double.Parse(dr[i]["longitude"].ToString())));
            GInfoWindow window = new GInfoWindow(marker, coment);
            GMap2.Add(window);
        }
    }

    #endregion

    private LinkButton criarBotaoMapa(string IdDt, string texto)
    {
        LinkButton bot = new LinkButton();
        bot.BorderStyle = BorderStyle.None;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        // bot.Click += new EventHandler(btnDT_Click);
        bot.Click += btnMapa_Click;
        bot.CssClass = "link";
        bot.ID = IdDt;
        bot.Text = texto;
        bot.CommandArgument = IdDt;
        bot.ToolTip = "Clique para ver a Posição da DT";
        return bot;
    }
    
    private LinkButton criarBotaoNotasFiscais(string IdDt, string texto)
    {
        LinkButton bot = new LinkButton();
        bot.BorderStyle = BorderStyle.None;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Click += btnNfs_Click;
        bot.CssClass = "link";
        bot.ID = IdDt;
        bot.Text = texto;
        bot.CommandArgument = IdDt;
        bot.ToolTip = "Clique para ver os documentos contidos nesta DT";
        return bot;
    }

    private void btnNfs_Click(object sender, System.EventArgs e)
    {
    }

    private void btnMapa_Click(object sender, System.EventArgs e)
    {
        LinkButton x = (LinkButton)sender;
        //   Pesquisar();
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];

        DataTable dNotas = Sistran.Library.GetDataTables.RetornarDataTable("EXEC RETORNARNOTASMAPA " + ILusuario[0].EmpresaId + ", " + x.CommandArgument, Session["Conn"].ToString());
        Session["entregues"] = dNotas;
        ListBox1.DataSource = dNotas;
        ListBox1.DataTextField = "NUMERO";
        ListBox1.DataValueField = "iddocumento";

        LblListaNf.Visible = false;
        GMap2.Visible = false;
        lblMapaNota.Visible = false;

        if (dNotas.Rows.Count == 0)
        {
            LblListaNf.Visible = false;
            ListBox1.Visible = false;
            GMap2.Visible = false;
            lblMapaNota.Visible = false;
            GridView1.Visible = false;

        }
        else
        {
            ListBox1.Items.Clear();
            ListBox1.DataBind();
            LblListaNf.Visible = true;
            ListBox1.Visible = false;
            GridView1.Visible = true;
            GridView1.DataSource = dNotas;
            GridView1.DataBind();
            LblListaNf.Text = "Nota(s) Fiscal(is) DT: " + x.Text;

            // carrega o mapa do local da dt
            carregarMapaDT(x.ID);

        }
    }
    
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //carregarMapaNF();
    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "ver")
        {
            carregarMapaNF(e.CommandName.ToString());
        }

        if (e.CommandArgument.ToString() == "VerFoto")
        {
            dvFoto.Visible = true;
            DataTable dtfotos = buscarFoto(e.CommandName.ToString());
            Session["dtfoto"] = dtfotos;
            lstFoto.DataSource = dtfotos;
            lstFoto.DataBind();

            for (int i = 0; i < dtfotos.Rows.Count; i++)
            {
                byte[] imagem = (byte[])dtfotos.Rows[i]["ARQUIVO"];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(Server.MapPath(@"imgReport/" + dtfotos.Rows[i][0].ToString() + ".jpg"));
                imgFotoGrande.ImageUrl = "imgReport/" + dtfotos.Rows[i]["IDDOCUMENTOOCORRENCIAARQUIVO"] + ".jpg";
            }
            pnlteste.Enabled = false;
        }
    }
    
    protected void btnFecharImagem_Click(object sender, EventArgs e)
    {
        dvFoto.Visible = false;
        pnlteste.Enabled = true;
    }
    int numero = 0;
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //ImageButton imgFotos = (ImageButton)e.Row.FindControl("btnAmpliarImagem");
        //Label lblData = (Label)e.Row.FindControl("lblData");

        //if (imgFotos != null)
        //{
        //    imgFotos.ImageUrl = "imgReport/" + imgFotos.CommandArgument.ToString() + ".jpg";
        //    numero = numero + 1;
        //    lblData.Text = "Foto: " + numero.ToString();
        //       }
    }

    protected void lstFoto_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        ImageButton imgFotos = (ImageButton)e.Item.FindControl("btnAmpliarImagem");
        Label lblData = (Label)e.Item.FindControl("lblData");

        if (imgFotos != null)
        {
            imgFotos.ImageUrl = "imgReport/" + imgFotos.CommandArgument.ToString() + ".jpg";
            numero = numero + 1;
            lblData.Text = "Foto: " + numero.ToString();
        }
    }

    protected void lstFoto_ItemCommand1(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Ampliar")
        {
            imgFotoGrande.ImageUrl = "imgReport/" + e.CommandArgument.ToString() + ".jpg";

        }
    }

    private DataTable buscarFoto(string iddocumento)
    {
        string strsq = "";
        strsq += " SELECT DOA.IDDOCUMENTOOCORRENCIAARQUIVO, ";
        strsq += " DOA.ARQUIVO,  ";
        strsq += " CONVERT(VARCHAR(10),DO.DATAOCORRENCIA, 103) DATAOCORRENCIA ";
        strsq += " FROM DOCUMENTOOCORRENCIAARQUIVO DOA ";
        strsq += " INNER JOIN DOCUMENTOOCORRENCIA DO ON DO.IDDOCUMENTOOCORRENCIA =  DOA.IDDOCUMENTOOCORRENCIA ";
        strsq += " INNER JOIN DOCUMENTO DOC ON DOC.IDDOCUMENTOOCORRENCIA  =  DOA.IDDOCUMENTOOCORRENCIA";
        strsq += " WHERE DOC.IDDOCUMENTO = " + iddocumento;
        strsq += " ORDER BY DO.DATAOCORRENCIA DESC ";
        return new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), strsq).Tables[0];
    }
}