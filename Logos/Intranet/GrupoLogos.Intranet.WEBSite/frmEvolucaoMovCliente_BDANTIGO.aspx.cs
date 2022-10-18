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

public partial class frmEvolucaoMovCliente_BDANTIGO : System.Web.UI.Page
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


            if (!IsPostBack)
            {
                btnExportar.Visible = false;
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//               SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                txtI.Text = DateTime.Now.Month.ToString()  + "/" + DateTime.Now.Year.ToString();
                txtF.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();

                string stsqlClientes = "SELECT  REM.RECEBEPEDIDO,  REM.CNPJ +' - '+  REM.RAZAOSOCIAL  CLIENTE, REM.CNPJ FROM  CLIENTE REM WHERE  FORNECEDOR IS NOT NULL   AND FORNECEDOR <> 0 ORDER BY 2";
                DataTable dtclentes = Sistran.Library.GetDataTables.RetornarDataTableWS(stsqlClientes, Sistran.Library.Robo.Robo.RetornarStringBaseAntiga());

                cboCliente.DataSource = dtclentes;
                cboCliente.DataTextField = "CLIENTE";
                cboCliente.DataValueField = "CNPJ";
                cboCliente.DataBind();

                cboCliente.Items.Insert(0, "Selecione o Cliente");

            }
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (cboCliente.SelectedIndex == 0)
            return;

        try
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//           SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));


            string[] inicio = txtI.Text.Split('/');
            string[] fim = txtF.Text.Split('/');

            btnExportar.Attributes.Add("onClick", "gerarExcel(); return false;");

            btnExportar.Text = "Gerar Excel";
            btnExportar.Visible = true;




            string stsqlClientes = "SELECT  REM.RECEBEPEDIDO,  REM.RAZAOSOCIAL, REM.CNPJ FROM  CLIENTE REM WHERE  FORNECEDOR IS NOT NULL   AND FORNECEDOR <> 0  AND REM.CNPJ ='"+cboCliente.SelectedValue.Trim()+" ' ORDER BY 2";
            DataTable dtclentes = Sistran.Library.GetDataTables.RetornarDataTableWS(stsqlClientes, Sistran.Library.Robo.Robo.RetornarStringBaseAntiga());


            string strsql = "";
            for (int iCliente = 0; iCliente < dtclentes.Rows.Count; iCliente++)
            {
                //string strsql = "";

                string SeriePermitida = "='UNI'";
                if (dtclentes.Rows[iCliente]["RECEBEPEDIDO"].ToString() == "CNH")
                    SeriePermitida = "='UNI'";
                else
                    SeriePermitida = "<>'UNI'";

                strsql += " SELECT  CNH.CNPJREMETENTE, ";
                strsql += " CONVERT(VARCHAR(7),CNH.DATADEEMISSAO,120) DATA,   ";
                strsql += " SUM(CNH.PESO) AS PESO,     ";
                strsql += " SUM(CNH.PESOCUBADO) AS PESOCUBADO,     ";
                strsql += " SUM(CNH.VOLUMES) AS VOLUMES,     ";
                strsql += " SUM(CNH.VALORDANOTA) AS VALORDANOTA,     ";
                strsql += " SUM(CNH.FRETETOTAL) AS FRETETOTAL,     ";
                strsql += " COUNT(*) AS DESPACHOS ";
                strsql += " FROM CONHECIMENTO CNH ";
                strsql += " WHERE   ";
                strsql += " YEAR(CNH.DATADEEMISSAO) >= " + inicio[1] + " AND MONTH (CNH.DATADEEMISSAO) >= " + inicio[0];
                strsql += " AND YEAR(CNH.DATADEEMISSAO) <=" + fim[1] + " AND MONTH (CNH.DATADEEMISSAO) <= " + fim[0];
                strsql += " AND CNH.SITUACAODAIMPRESSAODOCTRC <> 'CANCELADO'       ";
                strsql += " AND CNH.CNPJREMETENTE='" + dtclentes.Rows[iCliente]["CNPJ"].ToString().Trim() + "' ";
                strsql += " AND CNH.SERIEDOCONHECIMENTO " + SeriePermitida;
                strsql += " GROUP BY  CNH.CNPJREMETENTE,  ";
                strsql += " CONVERT(VARCHAR(7),CNH.DATADEEMISSAO,120) ";
                strsql += " ORDER BY 2 ";

            }


            System.Data.DataSet DS = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseAntiga());
            Session["DS"] = DS;

            PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=99%  runat='server' border='0' >"));

            for (int IITEM = 0; IITEM < DS.Tables.Count; IITEM++)
            {

                DataTable dt = DS.Tables[IITEM];

                if (dt.Rows.Count > 0)
                {

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td COLSPAN='7' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + dtclentes.Select("CNPJ='" + dt.Rows[0]["CNPJREMETENTE"].ToString().Trim() + "'")[0]["RAZAOSOCIAL"] + " (CNPJ: " + dt.Rows[0]["CNPJREMETENTE"].ToString() + ")"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

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


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Rows[i]["DATA"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Rows[i]["DESPACHOS"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Rows[i]["VOLUMES"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + string.Format("{0:N2}", decimal.Parse(dt.Rows[i]["PESO"].ToString()))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + string.Format("{0:N2}", decimal.Parse(dt.Rows[i]["VALORDANOTA"].ToString()))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + string.Format("{0:N2}", decimal.Parse(dt.Rows[i]["FRETETOTAL"].ToString()))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


                        if (i == dt.Rows.Count - 1)
                        {
                            //linha de total
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>SUB TOTAL"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + dt.Compute("SUM(DESPACHOS)", "").ToString()));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + dt.Compute("SUM(VOLUMES)", "")));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + decimal.Parse(dt.Compute("SUM(PESO)", "").ToString()).ToString("N2")));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + decimal.Parse(dt.Compute("SUM(VALORDANOTA)", "").ToString()).ToString("N2")));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;font-weight:bold'>" + decimal.Parse(dt.Compute("SUM(FRETETOTAL)", "").ToString()).ToString("N2")));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr >"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td COLSPAN='7'><HR>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                        }
                    }

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