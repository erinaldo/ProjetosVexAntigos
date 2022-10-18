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
using System.Web.UI.WebControls;

public partial class frmEvolucaoMovCliente : System.Web.UI.Page
{
    public int intervalo;
    string clientesSelecionados = "";

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
                btnExportar.Visible = false;
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                txtI.Text = "01/" + DateTime.Now.Year.ToString();
                txtF.Text = (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year.ToString();

                //carregarCboCliente();



            }
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

            if (Session["ListClientes"] == null || Session["ListClientes"] == "" || ((ListBox)Session["ListClientes"]).Items.Count == 0)
            {
                dvSelecionarCliente.Visible = true;
                return;
            }


            ListBox listClie = (ListBox)Session["ListClientes"];
            for (int i = 0; i < listClie.Items.Count; i++)
            {
                clientesSelecionados += listClie.Items[i].Text + ",";
            }

            clientesSelecionados = clientesSelecionados.Substring(0, clientesSelecionados.Length - 1);

            btnExportar.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    //    private void carregarCboCliente()
    //    {
    //        string stsqlClientes = "SELECT DISTINCT C.IDCADASTRO,  ";
    //stsqlClientes += " C.CNPJCPF + ' - ' + ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) + ' - ' + CID.NOME + ' / ' + EST.NOME  NOME,  ";
    //stsqlClientes += " ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) ";
    //stsqlClientes += " FROM CADASTRO C ";
    //stsqlClientes += " INNER JOIN CLIENTE CLI ON C.IDCADASTRO = CLI.IDCLIENTE ";
    //stsqlClientes += " INNER JOIN DOCUMENTO DOC ON DOC.IDCLIENTE = CLI.IDCLIENTE ";
    //stsqlClientes += " INNER JOIN CIDADE CID  ON CID.IDCIDADE = C.IDCIDADE ";
    //stsqlClientes += " INNER JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO ";
    //stsqlClientes += " WHERE CLI.ATIVO='SIM' ";
    //stsqlClientes += " AND DOC.TIPODEDOCUMENTO='NOTA FISCAL' ";
    //stsqlClientes += " AND DOC.TIPODESERVICO='TRANSPORTE' ";
    //stsqlClientes += " AND DOC.DATADEEMISSAO>='2012-01-01' ";
    //stsqlClientes += " ORDER BY 3 ";



    //        cboCliente.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWS(stsqlClientes, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
    //        cboCliente.DataTextField = "NOME";
    //        cboCliente.DataValueField = "IDCADASTRO";
    //        cboCliente.DataBind();

    //        cboCliente.Items.Insert(0, "SELECIONE O CLIENTE");
    //    }

    public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
    {
        DataTable dtUniqRecords = new DataTable();
        dtUniqRecords = dt.DefaultView.ToTable(true, Columns);
        return dtUniqRecords;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (cboCliente.SelectedIndex == 0)
        //    return;

        try
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//           SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));


            string[] inicio = txtI.Text.Split('/');
            string[] fim = txtF.Text.Split('/');

            Session["ini"] =txtI.Text;
            Session["fim"] = txtF.Text;


            btnExportar.Attributes.Add("onClick", "gerarExcel(); return false;");

            btnExportar.Text = "Gerar Excel";
            btnExportar.Visible = false;

            string[] dinicio = txtI.Text.Split('/');
            string[] dfim = txtF.Text.Split('/');


            DateTime di = Convert.ToDateTime(dinicio[1] + "/" + dinicio[0] + "/01");
            DateTime df = Convert.ToDateTime(dfim[1] + "/" + dfim[0] + "/" + DateTime.DaysInMonth(int.Parse(dfim[1]), int.Parse(dfim[0])));

            string stsqlClientes = " SELECT  C.CNPJCPF, isnull(c.FantasiaApelido,  C.RAZAOSOCIALNOME) RAZAOSOCIALNOME, ";
            stsqlClientes += " CAST(MONTH(DOC.DATADEEMISSAO) AS NVARCHAR(2)) + ' - ' + CAST(YEAR(DOC.DATADEEMISSAO) AS NVARCHAR(4)) PERIODO,  ";
            stsqlClientes += " COUNT(DISTINCT DOC.IDDOCUMENTO) NOTAS , ";
            stsqlClientes += " SUM(ISNULL(DOC.VOLUMES,0)) VOLUMES, ";
            stsqlClientes += " SUM(ISNULL(DOC.PesoBruto,0)) PESO, ";
            stsqlClientes += " SUM(ISNULL(DOC.ValorDaNota,0)) VALORNOTA, ";
            stsqlClientes += " SUM(ISNULL(DF.FRETE,0)) FRETE ";
            stsqlClientes += " FROM  DOCUMENTO DOC ";
            stsqlClientes += " left JOIN DOCUMENTOFRETE DF ON DF.IDDOCUMENTO = DOC.IDDOCUMENTO  AND DF.PROPRIETARIO='CLIENTE'";
            stsqlClientes += " left JOIN CADASTRO C ON C.IDCADASTRO = DOC.IDCLIENTE ";
            stsqlClientes += " WHERE  ";
            stsqlClientes += " DOC.ATIVO='SIM' ";
            stsqlClientes += " AND DOC.TIPODESERVICO='TRANSPORTE' ";
            stsqlClientes += " AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'ORDEM DE SERVICO', 'GUIA DE REMESSA') ";
            stsqlClientes += " AND DOC.IDCLIENTE  in( " + clientesSelecionados + ")";
            stsqlClientes += " AND DOC.DATADEEMISSAO BETWEEN '" + di.ToString("yyyy-MM-dd") + "' AND '" + df.ToString("yyyy-MM-dd") + "' ";
            stsqlClientes += " GROUP BY C.CnpjCpf,  isnull(c.FantasiaApelido,  C.RAZAOSOCIALNOME), YEAR(DOC.DATADEEMISSAO), MONTH(DOC.DATADEEMISSAO) ORDER BY C.CnpjCpf,  isnull(c.FantasiaApelido,  C.RAZAOSOCIALNOME),YEAR(DOC.DATADEEMISSAO) , MONTH(DOC.DATADEEMISSAO)";
            DataTable dtclentes = Sistran.Library.GetDataTables.RetornarDataTableWS(stsqlClientes, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            //Session["DS"] = dtclentes;

            PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=99%  runat='server' border='0' >"));

            if (dtclentes.Rows.Count == 0)
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td COLSPAN='1' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>NENHUM ITEM ENCONTRADO"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            }
            else
            {

                string[] TobeDistinct = { "CNPJCPF", "RAZAOSOCIALNOME" };
                DataTable dtDistinct = GetDistinctRecords(dtclentes, TobeDistinct);

                for (int i = 0; i < dtDistinct.Rows.Count; i++)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td COLSPAN='7' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + dtDistinct.Rows[i][0].ToString() + " - " + dtDistinct.Rows[i][1].ToString()));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;font-weight:bold'>PERÍODO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>NOTA FISCAL"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>VOLUMES"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>PESO"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>VALOR NOTA"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>FRETE TOTAL"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


                    DataRow[] l = dtclentes.Select("CNPJCPF='" + dtDistinct.Rows[i][0].ToString() + "'", "");
                    for (int il = 0; il < l.Length; il++)
                    {
                        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:7pt;height:10px;font-weight:normal'>" + l[il]["PERIODO"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + l[il]["NOTAS"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + l[il]["VOLUMES"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + string.Format("{0:N2}", decimal.Parse(l[il]["PESO"].ToString()))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + string.Format("{0:N2}", decimal.Parse(l[il]["VALORNOTA"].ToString()))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + string.Format("{0:N2}", decimal.Parse(l[il]["FRETE"].ToString()))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                    }

                    //linha de total
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>SUB-TOTAL"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + dtclentes.Compute("SUM(NOTAS)", "CNPJCPF='" + dtDistinct.Rows[i][0].ToString() + "'").ToString()));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + dtclentes.Compute("SUM(VOLUMES)", "CNPJCPF='" + dtDistinct.Rows[i][0].ToString() + "'").ToString()));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + decimal.Parse(dtclentes.Compute("SUM(PESO)", "CNPJCPF='" + dtDistinct.Rows[i][0].ToString() + "'").ToString()).ToString("N2")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + decimal.Parse(dtclentes.Compute("SUM(VALORNOTA)", "CNPJCPF='" + dtDistinct.Rows[i][0].ToString() + "'").ToString()).ToString("N2")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + decimal.Parse(dtclentes.Compute("SUM(FRETE)", "CNPJCPF='" + dtDistinct.Rows[i][0].ToString() + "'").ToString()).ToString("N2")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr >"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td COLSPAN='7'><HR>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

                }

            }
            PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }
}