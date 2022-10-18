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
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet DS = (DataSet)Session["DS"];
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




        gerarExcel();
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