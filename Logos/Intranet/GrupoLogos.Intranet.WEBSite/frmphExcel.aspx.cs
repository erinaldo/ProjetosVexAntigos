using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class frmphExcel : System.Web.UI.Page
{
    string clientesSelecionados = "";

    public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
    {
        DataTable dtUniqRecords = new DataTable();
        dtUniqRecords = dt.DefaultView.ToTable(true, Columns);
        return dtUniqRecords;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBox listClie = (ListBox)Session["ListClientes"];
            for (int i = 0; i < listClie.Items.Count; i++)
            {
                clientesSelecionados += listClie.Items[i].Text + ",";
            }

            Session["ListClientes"] = listClie;
            clientesSelecionados = clientesSelecionados.Substring(0, clientesSelecionados.Length - 1);

            /*DataSet DS = (DataSet)Session["DS"];
            string stsqlClientes = "SELECT  REM.RECEBEPEDIDO,  REM.RAZAOSOCIAL, REM.CNPJ FROM  CLIENTE REM WHERE  FORNECEDOR IS NOT NULL   AND FORNECEDOR <> 0 ORDER BY 2";
            DataTable dtclentes = Sistran.Library.GetDataTables.RetornarDataTableWS(stsqlClientes, Sistran.Library.Robo.Robo.RetornarStringBaseAntiga());



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

            */

            string[] dinicio = Session["ini"].ToString().Split('/');
            string[] dfim = Session["fim"].ToString().Split('/');


            DateTime di = Convert.ToDateTime(dinicio[1] + "/" + dinicio[0] + "/01");
            DateTime df = Convert.ToDateTime(dfim[1] + "/" + dfim[0] + "/" + DateTime.DaysInMonth(int.Parse(dfim[1]), int.Parse(dfim[0])));

            string stsqlClientes = " SELECT  C.CNPJCPF, C.RAZAOSOCIALNOME,";
            stsqlClientes += " CAST(MONTH(DOC.DATADEEMISSAO) AS NVARCHAR(2)) + ' - ' + CAST(YEAR(DOC.DATADEEMISSAO) AS NVARCHAR(4)) PERIODO,  ";
            stsqlClientes += " COUNT(DISTINCT DOC.IDDOCUMENTO) NOTAS , ";
            stsqlClientes += " SUM(ISNULL(DOC.VOLUMES,0)) VOLUMES, ";
            stsqlClientes += " SUM(ISNULL(DOC.PesoBruto,0)) PESO, ";
            stsqlClientes += " SUM(ISNULL(DOC.ValorDaNota,0)) VALORNOTA, ";
            stsqlClientes += " SUM(ISNULL(DF.FRETE,0)) FRETE ";
            stsqlClientes += " FROM  DOCUMENTO DOC ";
            stsqlClientes += " INNER JOIN DOCUMENTOFRETE DF ON DF.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            stsqlClientes += " INNER JOIN CADASTRO C ON C.IDCADASTRO = DOC.IDCLIENTE ";
            stsqlClientes += " WHERE DF.PROPRIETARIO='CLIENTE' ";
            stsqlClientes += " AND DOC.ATIVO='SIM' ";
            stsqlClientes += " AND DOC.TIPODESERVICO='TRANSPORTE' ";
            stsqlClientes += " AND DOC.TIPODEDOCUMENTO='NOTA FISCAL' ";
            stsqlClientes += " AND DOC.IDCLIENTE  in( " + clientesSelecionados + ")";
            stsqlClientes += " AND DOC.DATADEEMISSAO BETWEEN '" + di.ToString("yyyy-MM-dd") + "' AND '" + df.ToString("yyyy-MM-dd") + "' ";
            stsqlClientes += " GROUP BY C.CnpjCpf, C.RazaoSocialNome, YEAR(DOC.DATADEEMISSAO), MONTH(DOC.DATADEEMISSAO) ORDER BY C.CnpjCpf, C.RazaoSocialNome,YEAR(DOC.DATADEEMISSAO) , MONTH(DOC.DATADEEMISSAO)";
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




            gerarExcel();
        }
    }

    private void gerarExcel()
    {
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=evoCli.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        form.Controls.Add(PlaceHolder1);
        this.Controls.Add(form);
        form.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }
}