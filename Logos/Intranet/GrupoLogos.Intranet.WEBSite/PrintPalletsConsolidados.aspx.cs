using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PrintPalletsConsolidados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MontarTableDados();
    }


    protected void MontarTableDados()
    {
        
        TextBox txtI = new TextBox();
        TextBox txtF = new TextBox();

        string[] DataConf = FuncoesGerais.DataConf();
        txtI.Text = DataConf[0];
        txtF.Text = DataConf[1];

        phDados.Controls.Clear();

        string strsql = "";
        strsql += " SELECT distinct ";
        strsql += " MVCC.*, ";
        strsql += " CDCLI.CNPJCPF, ";
        strsql += " CDCLI.RAZAOSOCIALNOME ";
        strsql += " FROM MOVIMENTACAOCLIENTECONSOLIDADO  MVCC ";
        strsql += " INNER JOIN CADASTRO CDCLI ON CDCLI.IDCADASTRO = MVCC.IDCLIENTE ";
        strsql += " WHERE CONVERT(DATETIME, DATA, 103) BETWEEN CONVERT(DATE,'" + Convert.ToDateTime(txtI.Text) + "', 103) AND CONVERT(DATE,'" + Convert.ToDateTime(txtF.Text) + "', 103) ";
        strsql += " AND MVCC.IDCLIENTE IN (" + Session["idCliente_pallets"].ToString() + ")";
        strsql += " ORDER BY DATA";

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];

        phDados.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 width='200px'>"));
        if (dt.Rows.Count > 0)
        {

            phDados.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>Data"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>POSIÇÕES PALLETS ARMAZENAGEM"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'> UA´S ENTRADA **"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'> UA´s SAÍDAS **"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>M3 ARMAZENAGEM"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>M3 ENTRADA"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>M3 SAÍDAS"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in dt.Rows)
            {

                phDados.Controls.Add(new LiteralControl(@"<tr>"));
                lblDivCliente.Text = item["CNPJCPF"].ToString().Trim() + " - " + item["RAZAOSOCIALNOME"].ToString().Trim();

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap style='font-size:7pt;height:10px'><a href='frmhistoricopalletsDetalhe.aspx?data=" + Convert.ToDateTime(item["Data"]).ToString("dd/MM/yyyy") + "&idcliente=" + (item["idcliente"]).ToString() + "' target='_blank' class='link'>" + Convert.ToDateTime(item["Data"]).ToString("dd/MM/yyyy") + "</a>"));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + item["PalletsArmazenagem"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + item["PalletsEntrada"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + item["PalletsSaida"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + decimal.Parse(item["M3Armazenagem"].ToString()).ToString("#,0.000")));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + decimal.Parse(item["M3Entrada"].ToString()).ToString("#,0.000")));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + decimal.Parse(item["M3Saida"].ToString()).ToString("#,0.000")));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

            }

            phDados.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>(Média) " + dt.Compute("AVG(PalletsArmazenagem)", "")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + dt.Compute("SUM(PalletsEntrada)", "")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>" + dt.Compute("SUM(PalletsSaida)", "")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>(Média) " + decimal.Parse(dt.Compute("AVG(M3Armazenagem)", "").ToString()).ToString("#,0.000")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + decimal.Parse(dt.Compute("SUM(M3Entrada)", "").ToString()).ToString("#,0.000")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>" + decimal.Parse(dt.Compute("SUM(M3Saida)", "").ToString()).ToString("#,0.000")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));
            phDados.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phDados.Controls.Add(new LiteralControl(@"<tr>"));
            phDados.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phDados.Controls.Add(new LiteralControl(@"</td>"));
            phDados.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phDados.Controls.Add(new LiteralControl(@"</table>"));

    }
}