using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControls_ctrCargasDispDet : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strsql = "";
        strsql += " SELECT DISTINCT ";
        strsql += " NR.*, ";
        strsql += " NF.NUMERO NF, ";
        strsql += " NF.DATADEEMISSAO, ";
        strsql += " NF.DATADEENTRADA, ";
        strsql += " CADREM.FANTASIAAPELIDO REMETENTE, ";
        strsql += " CADDEST.FANTASIAAPELIDO DESTINATARIO, ";
        strsql += " AGR.NOME AGRUPAMENTO, ";
        strsql += " REG.NOME REGIAO, ";
        strsql += " CASE ISNULL(IDSETOR,0) WHEN 0 THEN NF.IDENDERECOCIDADE ELSE IDSETOR END IDSETORCERTO, ";
        strsql += " NR.SETOR ";
        strsql += " FROM NOTAS_REGIAO(" + Session["Chave"] + ") NR ";
        strsql += " LEFT JOIN AGRUPAMENTO AGR ON AGR.IDAGRUPAMENTO = NR.IDAGRUPAMENTO ";
        strsql += " LEFT JOIN REGIAO REG ON REG.IDREGIAO = NR.IDREGIAO  ";
        strsql += " INNER JOIN DOCUMENTO NF ON NF.IDDOCUMENTO = NR.IDDOCUMENTO  ";
        strsql += " INNER JOIN CADASTRO CADREM ON CADREM.IDCADASTRO = NF.IDREMETENTE  ";
        strsql += " INNER JOIN CADASTRO CADDEST ON CADDEST.IDCADASTRO = NF.IDDESTINATARIO  ";
        //        strsql += "WHERE NF.TIPODEDOCUMENTO = 'PEDIDO'";

        strsql += " ORDER by AGR.NOME, REG.NOME";

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseSTNNovo());

        string[] x = new string[2];
        x[0] = "IDAGRUPAMENTO";
        x[1] = "AGRUPAMENTO";
        DataTable dtDistFilial = dt.DefaultView.ToTable(true, x);

        string[] par = new string[4];
        par[0] = "IDAGRUPAMENTO";
        par[1] = "AGRUPAMENTO";
        par[2] = "IDREGIAO";
        par[3] = "REGIAO";

        DataTable dtDistreg = dt.DefaultView.ToTable(true, par);

        string[] parSet = new string[3];
        parSet[0] = "IDREGIAO";
        parSet[1] = "IDSETORCERTO";
        parSet[2] = "SETOR";

        DataTable dtSetor = dt.DefaultView.ToTable(true, parSet);


        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table'  cellspacing='1' celpanding='1' border='1' >"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='letf'>GRUPO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='left'>REGIÃO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='left'>SETOR"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='letf'>DESTINATÁRIO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='letf'>NOTA FISCAL"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='letf'>EMISSÃO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='letf'>DATA DE ENTRADA"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='RIGHT'>DOCUMENTOS"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='RIGHT'>VOLUMES"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='RIGHT'>PESO BRUTO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='RIGHT'>M3"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='RIGHT'>PESO CUBADO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='RIGHT'>VALOR DOCUMENTOS"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td align='RIGHT'>FRETE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));



        for (int i = 0; i < dtDistFilial.Rows.Count; i++)
        {
            #region Grupo
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            DataRow[] or = dtDistreg.Select("IDAGRUPAMENTO =" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString());
            //coluna expandir
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='width:20px'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandirFilial(dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString(), or.Length, dtDistFilial.Rows[i]["AGRUPAMENTO"].ToString())));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'>&nbsp;"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'>&nbsp;"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'>&nbsp;"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'>&nbsp;"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            int docs = int.Parse(dt.Compute("COUNT(iddocumento)", "IDAGRUPAMENTO=" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString()).ToString());

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='RIGHT' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + docs.ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            decimal val = decimal.Parse(dt.Compute("sum(VOLUMES)", "IDAGRUPAMENTO=" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString()).ToString());

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='RIGHT' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + val.ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            val = decimal.Parse(dt.Compute("sum(PESOBRUTO)", "IDAGRUPAMENTO=" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString()).ToString());
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='RIGHT' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + val.ToString("##,0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            val = decimal.Parse(dt.Compute("sum(PESOCUBADO)", "IDAGRUPAMENTO=" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString()).ToString());
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='RIGHT' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + val.ToString("##,0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            val = decimal.Parse(dt.Compute("sum(PESOCUBADO)", "IDAGRUPAMENTO=" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString()).ToString());
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='RIGHT' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + val.ToString("##,0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            val = decimal.Parse(dt.Compute("sum(VALORDANOTA)", "IDAGRUPAMENTO=" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString()).ToString());
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='RIGHT' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + val.ToString("##,0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            val = decimal.Parse(dt.Compute("sum(FRETE)", "IDAGRUPAMENTO=" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString()).ToString());
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='RIGHT' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + val.ToString("##,0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


            #endregion

            #region Regiao
            for (int iiReg = 0; iiReg < or.Length; iiReg++)
            {
                DataRow[] ors = dtSetor.Select("idregiao=" + or[iiReg]["idregiao"].ToString(), "Setor");
                
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr id='" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString() + iiReg.ToString() + "' style='color:BLUE; display:none' >"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'> &nbsp;"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>"));

                PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandirRegiao(dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString() + or[iiReg]["idregiao"].ToString(), ors.Length, or[iiReg]["REGIAO"].ToString())));

                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                decimal v = decimal.Parse(dt.Compute("COUNT(iddocumento)", "idregiao=" + or[iiReg]["idregiao"].ToString()).ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v.ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                v = decimal.Parse(dt.Compute("sum(volumes)", "idregiao=" + or[iiReg]["idregiao"].ToString()).ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v.ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                v = decimal.Parse(dt.Compute("sum(PESOBRUTO)", "idregiao=" + or[iiReg]["idregiao"].ToString()).ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v.ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                v = decimal.Parse(dt.Compute("sum(METRAGEMCUBICA)", "idregiao=" + or[iiReg]["idregiao"].ToString()).ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v.ToString("##,0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                v = decimal.Parse(dt.Compute("sum(PESOCUBADO)", "idregiao=" + or[iiReg]["idregiao"].ToString()).ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v.ToString("##,0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                v = decimal.Parse(dt.Compute("sum(VALORDANOTA)", "idregiao=" + or[iiReg]["idregiao"].ToString()).ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v.ToString("##,0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                v = decimal.Parse(dt.Compute("sum(frete)", "idregiao=" + or[iiReg]["idregiao"].ToString()).ToString());
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v.ToString("##,0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                


                #region Setor

                for (int iset = 0; iset < ors.Length; iset++)
                {
                    DataRow[] ornota = dt.Select("IDSETORCERTO=" + ors[iset]["IDSETORCERTO"].ToString(), "NF");

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr id='Lreg_" + dtDistFilial.Rows[i]["IDAGRUPAMENTO"].ToString() + or[iiReg]["idregiao"].ToString() + iset.ToString() + "' style='display:none; ; color:green' >"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandirSetor(ors[iset]["IDSETORCERTO"].ToString(), ornota.Length, ors[iset]["SETOR"].ToString())));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



                    decimal v2 = decimal.Parse(dt.Compute("count(IDDOCUMENTO)", "IDREGIAO=" + or[iiReg]["IDREGIAO"].ToString() + " AND IDSETORCERTO=" + ors[iset]["IDSETORCERTO"].ToString()).ToString());
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v2.ToString("#0")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    v2 = decimal.Parse(dt.Compute("SUM(VOLUMES)", "IDREGIAO=" + or[iiReg]["IDREGIAO"].ToString() + " AND IDSETORCERTO=" + ors[iset]["IDSETORCERTO"].ToString()).ToString());
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v2.ToString("#0")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    v2 = decimal.Parse(dt.Compute("SUM(PESOBRUTO)", "IDREGIAO=" + or[iiReg]["IDREGIAO"].ToString() + " AND IDSETORCERTO=" + ors[iset]["IDSETORCERTO"].ToString()).ToString());
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v2.ToString("#0")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                    v2 = decimal.Parse(dt.Compute("SUM(PESOCUBADO)", "IDREGIAO=" + or[iiReg]["IDREGIAO"].ToString() + " AND IDSETORCERTO=" + ors[iset]["IDSETORCERTO"].ToString()).ToString());
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v2.ToString("##,0.00")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                    v2 = decimal.Parse(dt.Compute("SUM(METRAGEMCUBICA)", "IDREGIAO=" + or[iiReg]["IDREGIAO"].ToString() + " AND IDSETORCERTO=" + ors[iset]["IDSETORCERTO"].ToString()).ToString());
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v2.ToString("##,0.00")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    v2 = decimal.Parse(dt.Compute("SUM(VALORDANOTA)", "IDREGIAO=" + or[iiReg]["IDREGIAO"].ToString() + " AND IDSETORCERTO=" + ors[iset]["IDSETORCERTO"].ToString()).ToString());
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v2.ToString("##,0.00")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    v2 = decimal.Parse(dt.Compute("SUM(frete)", "IDREGIAO=" + or[iiReg]["IDREGIAO"].ToString() + " AND IDSETORCERTO=" + ors[iset]["IDSETORCERTO"].ToString()).ToString());
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + v2.ToString("##,0.00")));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


                    for (int inf = 0; inf < ornota.Length; inf++)
                    {
                        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr id='nf_" + ors[iset]["IDSETORCERTO"].ToString() + inf.ToString() + "' style='display:none; ; color:red' >"));



                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>&nbsp;"));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>"));
                        //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>" + ornota[inf]["REMETENTE"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>" + ornota[inf]["DESTINATARIO"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>" + ornota[inf]["NF"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'>" + DateTime.Parse(ornota[inf]["DATADEEMISSAO"].ToString()).ToString("dd/MM/yyyy")));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'>" + DateTime.Parse(ornota[inf]["DATADEENTRADA"].ToString()).ToString("dd/MM/yyyy")));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>"));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + decimal.Parse(ornota[inf]["VOLUMES"].ToString()).ToString("#0")));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + decimal.Parse(ornota[inf]["PESOBRUTO"].ToString()).ToString("#0")));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + decimal.Parse(ornota[inf]["METRAGEMCUBICA"].ToString()).ToString(("##,0.00"))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + decimal.Parse(ornota[inf]["PESOCUBADO"].ToString()).ToString(("##,0.00"))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + decimal.Parse(ornota[inf]["VALORDANOTA"].ToString()).ToString(("##,0.00"))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + decimal.Parse(ornota[inf]["FRETE"].ToString()).ToString(("##,0.00"))));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

                    }
                }
                #endregion
            }


            #endregion
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

    }

    private string CriarBotaoExpandirFilial(string p, int qtdLinha, string palavra)
    {
        string m = "";
        m = "<b><div id='dv_" + p + "'  style='cursor:Pointer; background: url(plus.gif)  no-repeat; heigth:10px' OnClick=ExpandirGrupo('" + qtdLinha + "','dv_" + p + "','" + p + "');>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        m += palavra + "</div></b>";

        return m;
    }

    private string CriarBotaoExpandirRegiao(string p, int qtdLinha, string palavra)
    {
        string m = "";
        m = "<b><div id='dv_r" + p + "'  style='cursor:Pointer; background: url(plus.gif)  no-repeat; heigth:10px' OnClick=ExpandirRegiao('" + qtdLinha + "','dv_r" + p + "','Lreg_" + p + "');>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        m += palavra + "</div></b>";
        return m;
    }

    private string CriarBotaoExpandirSetor(string p, int qtdLinha, string palavra)
    {
        string m = "";
        m = "<b><div id='dv_set" + p + "'  style='cursor:Pointer; background: url(plus.gif)  no-repeat; heigth:10px' OnClick=ExpandirSetor('" + qtdLinha + "','dv_set" + p + "','nf_" + p + "');>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        m += palavra + "</div></b>";
        return m;
    }
}