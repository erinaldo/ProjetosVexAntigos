using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class frmDetalheDT : System.Web.UI.Page
{
    int idDt = 0, idfilial = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["IDDT"] == null || Request.QueryString["IDFILIAL"] == null)
                {
                    throw new Exception("");
                }

                idDt = int.Parse(Request.QueryString["IDDT"]);
                idfilial = int.Parse(Request.QueryString["IDFILIAL"]);

                DataTable dtCabec = new SistranBLL.DocumentoDeTransporte().RetornarRomaneios(idDt, idfilial);

                if (dtCabec.Rows.Count > 0)
                {
                    //--------------------------------------------------------------------------------------------------------------
                    ph_dt.Controls.Add(new LiteralControl(@"<div style='width:100%;'>"));
                    ph_dt.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=2 celpanding=2 border='0' >"));

                    ph_dt.Controls.Add(new LiteralControl(@"<tr>"));

                    ph_dt.Controls.Add(new LiteralControl(@"<td  class='tdp' nowrap=nowrap style='font-size:7pt;width:1%'>Número DT:"));
                    ph_dt.Controls.Add(new LiteralControl(@"</td>"));

                    ph_dt.Controls.Add(new LiteralControl(@"<td  class='tdpCenter' nowrap=nowrap style='font-size:7pt;'>" + dtCabec.Rows[0]["IDDT"].ToString()));
                    ph_dt.Controls.Add(new LiteralControl(@"</td>"));

                    ph_dt.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;width:1%'>Data de Emissão: "));
                    ph_dt.Controls.Add(new LiteralControl(@"</td>"));

                    ph_dt.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap style='font-size:7pt;'>" + Convert.ToDateTime(dtCabec.Rows[0]["EMISSAO"]).ToShortDateString()));
                    ph_dt.Controls.Add(new LiteralControl(@"</td>"));
                    ph_dt.Controls.Add(new LiteralControl(@"</tr>"));
                    ph_dt.Controls.Add(new LiteralControl(@"</table>"));
                    ph_dt.Controls.Add(new LiteralControl(@"</div>"));
                    //--------------------------------------------------------------------------------------------------------------
                    
                  

                    //--------------------------------------------------------------------------------------------------------------

                    ph_Veiculo.Controls.Add(new LiteralControl(@"<div style='width:100%'>"));

                    ph_Veiculo.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=2 celpanding=2 border='0' >"));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"<tr>"));

                    ph_Veiculo.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;;width:1%'>Veículo Placa:"));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Veiculo.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;'>" + (dtCabec.Rows[0]["Placa"].ToString() == "" ? "-" : dtCabec.Rows[0]["Placa"].ToString())));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Veiculo.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;;width:1%'>Cidade / UF: "));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Veiculo.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;'>" + dtCabec.Rows[0]["CIDADEVEICULO"].ToString() + "-" + dtCabec.Rows[0]["UFVEICULO"].ToString()));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Veiculo.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;;width:1%'>Ano Fabricação: "));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"</td>"));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;'>" + (dtCabec.Rows[0]["Ano"].ToString() == "" ? "-" : dtCabec.Rows[0]["Ano"].ToString())));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Veiculo.Controls.Add(new LiteralControl(@"</tr>"));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"</table>"));
                    ph_Veiculo.Controls.Add(new LiteralControl(@"</div>"));
                    //--------------------------------------------------------------------------------------------------------------

                    //--------------------------------------------------------------------------------------------------------------

                    ph_Motorista.Controls.Add(new LiteralControl(@"<div style='width:100%'>"));

                    ph_Motorista.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=2 celpanding=2 border='0' >"));
                    ph_Motorista.Controls.Add(new LiteralControl(@"<tr>"));

                    ph_Motorista.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;width:1%'>Nome Motorista:"));
                    ph_Motorista.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Motorista.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;width:30%'>" + (dtCabec.Rows[0]["NOMEMOTORISTA"].ToString() == "" ? "-" : dtCabec.Rows[0]["NOMEMOTORISTA"].ToString())));
                    ph_Motorista.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Motorista.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;width:1%'>CPF:"));
                    ph_Motorista.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Motorista.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;width:30%'>" + dtCabec.Rows[0]["CPFMOTORISTA"].ToString() + "-" + dtCabec.Rows[0]["CPFMOTORISTA"].ToString()));
                    ph_Motorista.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Motorista.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;width:1%'>RG: "));
                    ph_Motorista.Controls.Add(new LiteralControl(@"</td>"));
                    ph_Motorista.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;width:30%'>" + (dtCabec.Rows[0]["RGMOTORISTA"].ToString() == "" ? "-" : dtCabec.Rows[0]["RGMOTORISTA"].ToString())));
                    ph_Motorista.Controls.Add(new LiteralControl(@"</td>"));

                    ph_Motorista.Controls.Add(new LiteralControl(@"</tr>"));
                    ph_Motorista.Controls.Add(new LiteralControl(@"</table>"));
                    ph_Motorista.Controls.Add(new LiteralControl(@"</div>"));
                    //--------------------------------------------------------------------------------------------------------------
                    MontarTabela(dtCabec);
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
                return;
            }
        }
    }

    private void MontarTabela(DataTable dt)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<hr/>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<div style='font-size:7pt;text-align:left' ><b><left>ROMANEIO: " + dt.Rows[i]["NUMEROROMANEIO"].ToString() + " </left></B></div>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<hr/>"));
            
            PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 width='99%'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>Remetente"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>Destinatário"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' style='font-size:7pt;width:2%' nowrap=nowrap>Data do Movimento"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Número Documento"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Volumes"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Peso"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Peso Cubado"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));            
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Valor do Documento"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap style='font-size:7pt;'>Ocorrência"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


            DataTable dtDetalhe = new SistranBLL.DocumentoDeTransporte().RetornarDt(Convert.ToInt32(dt.Rows[i]["IDDT"]), idfilial);
            for (int ii = 0; ii < dtDetalhe.Rows.Count; ii++)
            {

                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>" + dtDetalhe.Rows[ii]["REMETENTE"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>" + dtDetalhe.Rows[ii]["DESTINATARIO"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:7pt;height:10px'>" + (dtDetalhe.Rows[ii]["DATADOMOVIMENTO"].ToString()==""?"-":Convert.ToDateTime(dtDetalhe.Rows[ii]["DATADOMOVIMENTO"]).ToShortDateString())));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + dtDetalhe.Rows[ii]["NUMERO"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + dtDetalhe.Rows[ii]["VOLUMES"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + (dtDetalhe.Rows[ii]["PESOBRUTO"].ToString()==""?"-":Convert.ToDecimal(dtDetalhe.Rows[ii]["PESOBRUTO"]).ToString("#0.00"))));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + (dtDetalhe.Rows[ii]["PESOCUBADO"].ToString()==""?"-": Convert.ToDecimal(dtDetalhe.Rows[ii]["PESOCUBADO"]).ToString("#0.00"))));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + (dtDetalhe.Rows[ii]["VALORDANOTA"].ToString() == "" ? "-" : Convert.ToDecimal(dtDetalhe.Rows[ii]["VALORDANOTA"]).ToString("#0.00"))));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>" ));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>" + dtDetalhe.Rows[ii]["OCORRENCIA"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            }


            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>total de Notas:"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + dtDetalhe.Rows.Count));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' style='font-size:7pt;width:2%' nowrap=nowrap>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Total do Romaneio:"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + dtDetalhe.Compute("SUM(VOLUMES)", "")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + Convert.ToDecimal(dtDetalhe.Compute("SUM(PESOBRUTO)", "")).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + Convert.ToDecimal(dtDetalhe.Compute("SUM(PESOBRUTO)", "")).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + Convert.ToDecimal(dtDetalhe.Compute("SUM(VALORDANOTA)", "")).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap style='font-size:7pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
        }
    }
}