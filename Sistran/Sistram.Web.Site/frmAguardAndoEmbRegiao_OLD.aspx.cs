﻿using System;
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


public partial class frmAguardAndoEmbRegiao_OLD : System.Web.UI.Page
{



    protected void Page_Load(object sender, EventArgs e)
    {

    }


    private void CarregarGrid()
    {

        string clientes = Sistran.Library.FuncoesUteis.retornarClientes();
        System.Text.StringBuilder ssql = new System.Text.StringBuilder();


        ssql.Append(" SELECT   ");
        ssql.Append(" AG.NOME AS GRUPO,  ");
        ssql.Append(" REG.NOME AS REGIAO,  ");
        ssql.Append(" CASE WHEN NOT RICAD.RAZAOSOCIALNOME IS NULL THEN 'CLIENTE ESPECIAL:'  + RICAD.RAZAOSOCIALNOME  ");
        ssql.Append(" WHEN NOT RIST.NOME IS NULL THEN 'SETOR:' +  CAST(RIST.CODIGO AS VARCHAR(5)) +' '+ RIST.NOME  ");
        ssql.Append(" WHEN NOT RICID.NOME IS NULL THEN RICID.NOME  ");
        ssql.Append(" WHEN NOT RIEST.UF IS NULL THEN RIEST.UF  ");
        ssql.Append(" WHEN NOT RIPAI.NOME IS NULL THEN RIPAI.NOME  ");
        ssql.Append(" END SETOR,  ");
        ssql.Append(" CAST(COALESCE(SUM(NF.VOLUMES),0) AS NUMERIC (10,0)) VOLUMES,  ");
        ssql.Append(" COALESCE(SUM(NF.PESOBRUTO),0) PESOBRUTO,  ");
        ssql.Append(" COALESCE(SUM(NF.METRAGEMCUBICA),0) METRAGEMCUBICA,  ");
        ssql.Append(" COALESCE(SUM(NF.PESOCUBADO),0) PESOCUBADO,  ");
        ssql.Append(" COALESCE(SUM(NF.VALORDANOTA),0) VALORDANOTA,  ");
        ssql.Append(" COALESCE(SUM(DF.FRETE),0) FRETE,  ");
        ssql.Append(" COALESCE(SUM(DF.ICMSISS),0) ICMSISS,  ");
        ssql.Append(" COUNT(*) NOTAS  ");
        ssql.Append(" FROM DOCUMENTO NF  ");
        ssql.Append(" INNER JOIN DOCUMENTOFILIAL FL ON (FL.IDDOCUMENTO = NF.IDDOCUMENTO)  ");
        ssql.Append(" LEFT JOIN DOCUMENTORELACIONADO DOCREL ON (DOCREL.IDDOCUMENTOFILHO = NF.IDDOCUMENTO)  ");
        ssql.Append(" LEFT JOIN DOCUMENTO CT ON (CT.IDDOCUMENTO = DOCREL.IDDOCUMENTOPAI AND CT.TIPODEDOCUMENTO='CONHECIMENTO')  ");
        ssql.Append(" LEFT JOIN REGIAOITEM RI ON (RI.IDREGIAOITEM = FL.IDREGIAOITEMFILIAL)  ");
        ssql.Append(" LEFT JOIN REGIAO REG ON (REG.IDREGIAO = RI.IDREGIAO)  ");
        ssql.Append(" LEFT JOIN AGRUPAMENTOREGIAO AGR ON (AGR.IDREGIAO = REG.IDREGIAO)  ");
        ssql.Append(" LEFT JOIN AGRUPAMENTO AG ON (AG.IDAGRUPAMENTO = AGR.IDAGRUPAMENTO)  ");
        ssql.Append(" LEFT JOIN DOCUMENTOFRETE DF ON (DF.IDDOCUMENTO = NF.IDDOCUMENTO AND DF.PROPRIETARIO = 'CLIENTE' )  ");
        ssql.Append(" LEFT JOIN CADASTRO RICAD ON (RICAD.IDCADASTRO = RI.IDCADASTRO)  ");
        ssql.Append(" LEFT JOIN SETOR RIST ON (RIST.IDSETOR = RI.IDSETOR)  ");
        ssql.Append(" LEFT JOIN CIDADE RICID ON (RICID.IDCIDADE = RI.IDCIDADE) ");
        ssql.Append(" LEFT JOIN ESTADO RIEST ON (RIEST.IDESTADO = RI.IDESTADO)  ");
        ssql.Append(" LEFT JOIN PAIS RIPAI ON (RIPAI.IDPAIS = RI.IDPAIS)  ");
        ssql.Append(" WHERE  ");
        ssql.Append(" FL.SITUACAO ='AGUARDANDO EMBARQUE' ");
        ssql.Append(" AND  ");
        ssql.Append(" ( ");
        ssql.Append(" NF.IDREMETENTE IN(" + clientes + ")  ");
        ssql.Append(" OR NF.IDCLIENTE IN(" + clientes + ")  ");
        ssql.Append(" )  ");
        ssql.Append(" GROUP BY AG.NOME, REG.NOME, RICAD.RAZAOSOCIALNOME,RIST.NOME,RICID.NOME, RIEST.UF,RIPAI.NOME, RIST.CODIGO  ");
        ssql.Append(" ORDER BY 1,2,3 ");


        DataTable dt = new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), ssql.ToString()).Tables[0];


        DataTable distinctTable = dt.DefaultView.ToTable(true, "GRUPO");


        for (int i = 0; i < distinctTable.Rows.Count; i++)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' >"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;' rowspan='2' valign='top' >GRUPO"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;' rowspan='2' valign='top' >REGIÃO"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;' rowspan='2' valign='top' >SETOR"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top' >NOTAS FISCAIS"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top' >VOLUMES"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top' >PESO BRUTO"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top' >M3"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top' >PESO CUBADO"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top' >VALOR DA NOTA"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top' >FRETE"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));


        }
    }
}