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

public partial class frmAcompanhamento : System.Web.UI.Page
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
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()) , System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                CarregarCboFilial();
                txtData.Text = DateTime.Now.ToShortDateString();
                Timer1.Enabled = false;
                Timer1.Interval = Convert.ToInt32(ConfigurationSettings.AppSettings["IntervaloRefresh"]) * 60000;
                esconderTotais();
            }

            if (lstFilial.Items.Count == 0)
            {
                lstFilial.Visible = false;
            }
            //Pesquisar();
            lstFilial.Width = cboFilial.Width;
            //            btnPesquisar.Visible = lstFilial.Visible;

        }
        catch (Exception ex)
        {
           //ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);

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
        //        btnPesquisar.Visible = lstFilial.Visible;
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
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
        Pesquisar();
    }

    private string MontarLinhaCaminhao(string qtdDocs, string realizados, int qtdCels)
    {
        string s = "";
        //realizados = qtdDocs;


        decimal idc = ((Convert.ToDecimal(realizados) / Convert.ToDecimal(qtdDocs)) * 100);
        s = "<table border='0' width='100%' cellspacing='0' cellpading='0' >";
        s += "<tr>";

        for (int i = 0; i <= (qtdCels+1); i++)
        {
            bool passou = false;


            //caminhao
            if (Convert.ToInt32(realizados) == (i))
            {
                s += "<td style='width:15px; align:rigth'>";
                passou = true;
                s += @"<img src='Imagens/caminhao.png' width='15pt'>";

            }
            if (Convert.ToInt32(qtdDocs)+1 == (i))
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
            filiais = "";

        cliente = Sistran.Library.FuncoesUteis.retornarClientes();

        DataTable dt = new SistranBLL.Veiculo().ListarMonitoramento(txtData.Text, filiais, cliente, cboOrdem.SelectedValue, Session["Conn"].ToString());

        MontarTable(dt);
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
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' style='font-size:7pt;height:10px;width:1%' nowrap='nowrap'>" + item["IdDt"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:7pt;height:10px;width:1%' nowrap='nowrap'>" + item["Placa"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:7pt;height:10px;width:1%' nowrap='nowrap'>" + mot));
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
        //Label1.Text = "Última Atualização: " + DateTime.Now.ToShortTimeString();
    }

    #endregion

}