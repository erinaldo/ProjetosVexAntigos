using System;
using Sistran;
using System.Data;
using System.Threading;
using System.Globalization;
using SistranBLL;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;

public partial class frmaprovarCotacao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (Request.QueryString["i"] == null)
        {
            dvErro.Visible = true;
            lblMensagem.Text = "NO MOMENTO NÃO EXISTE COTAÇÕES DE COMPRA PENDENTES DE APROVAÇÃO PARA SEU CENTRO DE CUSTO.";
            Panel1.Enabled = false;
            Panel1.Visible = false;
            btnVoltarDv.Focus();
            btnSairDv.Visible = true;
            return;
        }

        carregar();
        btnsAIR.Attributes.Add("onClick", "javascript:confirmarFechamnento();");

        lblTotal.Visible = false;
    }

    private void carregar()
    {
        try
        {
            DataTable dt;
            dt = Retornar();
            Session["dtContacaoAprov"] = dt;


            if (dt.Rows.Count == 0)
            {
                dvErro.Visible = true;
                lblMensagem.Text = "NO MOMENTO NÃO EXISTE COTAÇÕES DE COMPRA PENDENTES DE APROVAÇÃO PARA SEU CENTRO DE CUSTO.";
                Panel1.Visible = false;
                btnVoltarDv.Focus();
                btnSairDv.Visible = true;
                return;

            }

            Label lblUserName = (Label)Master.FindControl("lblUserName");
            lblUserName.Text = "Bem Vindo: " + dt.Rows[0]["USUARIO"].ToString();

            PlaceHolder2.Controls.Clear();


            #region itens

            PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='3' class='tdpCabecalhoMenor' align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>CENTRO DE CUSTO: " + dt.Rows[0]["CENTROCUSTO"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='3' class='tdpCabecalhoMenor' align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>FORNECEDOR: " + dt.Rows[0]["CNPJCPF"].ToString() + " - " + dt.Rows[0]["FORNECEDOR"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='3' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'><hr>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));



            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>NÚMERO DA COTAÇÃO</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'  class='tdpCabecalhoMenor' nowrap=nowrap style='font-size:8pt;'><b>VALOR TOTAL</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));



            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'  class='tdpCabecalhoMenor' nowrap=nowrap style='font-size:8pt;'><b>LIMITE DE COMPRA</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow rw in dt.Rows)
            {

                if (decimal.Parse(rw["VALORDECOMPRA"].ToString()) > decimal.Parse(rw["LIMITE"].ToString()))
                {
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));

//                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpVermelho' align='left' nowrap=nowrap style='font-size:8pt;'><a href='http://www.grupologos.com.br/sistranWeb.NET/cotacao/CotacaoFornecedor.aspx?bl=s&i=" + rw["IDCOTACAOFORNECEDOR"].ToString() + "' target='_blank' class='link'> " + rw["NUMEROCOTACAO"].ToString() + "</a>"));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpVermelho' align='left' nowrap=nowrap style='font-size:8pt;'><a href='CotacaoFornecedor.aspx?bl=s&i=" + rw["IDCOTACAOFORNECEDOR"].ToString() + "' target='_blank' class='link'> " + rw["NUMEROCOTACAO"].ToString() + "</a>"));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpVermelho' align='right' nowrap=nowrap style='font-size:8pt;'>" + decimal.Parse(rw["VALORDECOMPRA"].ToString()).ToString("#0.00")));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpVermelho' align='right' nowrap=nowrap style='font-size:8pt;'>" + decimal.Parse(rw["LIMITE"].ToString()).ToString("#0.00")));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
                }
                else
                {
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));

                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'><a href='CotacaoFornecedor.aspx?bl=s&i=" + rw["IDCOTACAOFORNECEDOR"].ToString() + "' target='_blank' class='link'> " + rw["NUMEROCOTACAO"].ToString() + "</a>"));
                   // PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'><a href='http://www.grupologos.com.br/sistranWeb.NET/cotacao/CotacaoFornecedor.aspx?bl=s&i=" + rw["IDCOTACAOFORNECEDOR"].ToString() + "' target='_blank' class='link'> " + rw["NUMEROCOTACAO"].ToString() + "</a>"));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:8pt;'>" + decimal.Parse(rw["VALORDECOMPRA"].ToString()).ToString("#0.00")));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:8pt;'>" + decimal.Parse(rw["LIMITE"].ToString()).ToString("#0.00")));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

                }





            }


            #endregion

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>TOTAL:</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>" + decimal.Parse(dt.Compute("SUM(VALORDECOMPRA)", "").ToString()).ToString("#0.00") + "</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b></b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));


            PlaceHolder2.Controls.Add(new LiteralControl(@"</table>"));


        }
        catch (Exception EX)
        {
            throw EX;
        }
    }

    public DataTable Retornar()
    {
        string strsql = "";
        strsql += " SELECT distinct";
        strsql += " CC.IDCOTACAODECOMPRA NUMEROCOTACAO, ";
        strsql += " CF.IDCOTACAOFORNECEDOR, ";
        strsql += " RMI.IDCENTRODECUSTO, ";
        strsql += " US.IDUSUARIO, ";
        strsql += " UCCO.VALOR LIMITE, ";
        strsql += " CADFOR.RAZAOSOCIALNOME FORNECEDOR, ";
        strsql += " CADFOR.CNPJCPF,   ";
        strsql += " CCUSTO.NOME CENTROCUSTO, US.NOME USUARIO, UCCO.IdUsuarioCentroDeCustoOperacao,";
        strsql += " SUM(CCI.VALORTOTALDOITEM) VALORDECOMPRA FROM COTACAODECOMPRA CC  ";
        strsql += " INNER JOIN COTACAOFORNECEDOR CF ON (CF.IDCOTACAODECOMPRA =CC.IDCOTACAODECOMPRA)  ";
        strsql += " INNER JOIN COTACAODECOMPRAITEM CCI ON (CCI.IDCOTACAOFORNECEDOR =CF.IDCOTACAOFORNECEDOR)  ";
        strsql += " INNER JOIN REQUISICAODEMATERIALDOCUMENTO RMD ON (RMD.IDCOTACAODECOMPRAITEM =CCI.IDCOTACAODECOMPRAITEM)  ";
        strsql += " INNER JOIN REQUISICAODEMATERIALITEM RMI ON (RMI.IDREQUISICAODEMATERIALITEM =RMD.IDREQUISICAODEMATERIALITEM)  ";
        strsql += " INNER JOIN USUARIOCENTRODECUSTO UCC ON (UCC.IDCENTRODECUSTO =RMI.IDCENTRODECUSTO)  ";
        strsql += " INNER JOIN USUARIOCENTRODECUSTOOPERACAO UCCO ON (UCCO.IDUSUARIOCENTRODECUSTO = UCC.IDUSUARIOCENTRODECUSTO)  ";
        strsql += " INNER JOIN USUARIO US ON (US.IDUSUARIO = UCC.IDUSUARIO)  ";
        strsql += " INNER JOIN CADASTRO CAD ON (CAD.IDCADASTRO = US.IDCADASTRO) ";
        strsql += " INNER JOIN CADASTROCONTATOENDERECO CCE ON (CCE.IDCADASTRO = CAD.IDCADASTRO)  ";
        strsql += " INNER JOIN CADASTRO CADFOR ON CADFOR.IDCADASTRO = CF.IDFORNECEDOR ";
        strsql += " INNER JOIN CENTRODECUSTO CCUSTO ON CCUSTO.IDCENTRODECUSTO = UCC.IDCENTRODECUSTO ";
        strsql += " WHERE CC.STATUS='AGUARDANDO APROVACAO COTACAO' ";
        strsql += " and us.idusuario=" + Request.QueryString["iu"];
        strsql += " AND CF.APROVADA='SIM'  ";
        strsql += " AND UCCO.IDOPERACAO=2 ";
        strsql += " AND RMI.IDCENTRODECUSTO IN (" + Request.QueryString["i"] + ") ";
        strsql += " GROUP BY ";
        strsql += " CC.IDCOTACAODECOMPRA, ";
        strsql += " CF.IDCOTACAOFORNECEDOR, ";
        strsql += " RMI.IDCENTRODECUSTO, ";
        strsql += " US.IDUSUARIO, ";
        strsql += " UCCO.VALOR, ";
        strsql += " CADFOR.RAZAOSOCIALNOME, ";
        strsql += " CADFOR.CNPJCPF,  ";
        strsql += " CCUSTO.NOME, US.NOME, UCCO.IdUsuarioCentroDeCustoOperacao";



        return Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"]).ToString()).Tables[0];
    }

    private void gerarErro(string msg)
    {
        dvErro.Visible = true;
        lblMensagem.Text = msg;
        btnVoltarDv.Visible = true;
        btnSairDv.Visible = false;
        Panel1.Enabled = false;
        btnVoltarDv.Focus();
        LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
        lnkLogout.Focus();
    }

    protected void btnVoltarDv_Click(object sender, EventArgs e)
    {
        dvErro.Visible = false;
        btnSairDv.Visible = false;
        Panel1.Enabled = true;
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtAtualizado = (DataTable)Session["dtContacaoAprov"];
            string strsql = "";

            for (int i = 0; i < dtAtualizado.Rows.Count; i++)
            {
                    //verifica se todos os aprovadores de centro de custo aprovaram
                DataTable dtAprov = RetornarAprov(dtAtualizado.Rows[i]["NUMEROCOTACAO"].ToString());

                for (int iapr = 0; iapr < dtAprov.Rows.Count; iapr++)
                {
                    if (dtAprov.Rows[iapr]["IDCENTRODECUSTO"].ToString() == Request.QueryString["i"].ToString())
                    {
                        dtAprov.Rows[iapr]["CENTROCUSTOAPROVADO"] = Request.QueryString["i"].ToString();
                    }
                }
                

                if (decimal.Parse(dtAtualizado.Rows[i]["LIMITE"].ToString()) >= decimal.Parse(dtAtualizado.Rows[i]["VALORDECOMPRA"].ToString()))
                {

                    strsql += " INSERT INTO UsuarioCentroDeCustoOperacaoLog ";
                    strsql += " (";
                    strsql += " idUsuarioCentroDeCustoOperacaolog, ";
                    strsql += " idUsuario, ";
                    strsql += " obs, ";
                    strsql += " DataAprovacao, ";
                    strsql += " idUsuarioCentroDeCustoOperacao ";
                    strsql += " ) VALUES ";
                    strsql += " ( ";
                    strsql += Sistran.Library.GetDataTables.RetornarIdTabela("UsuarioCentroDeCustoOperacaoLog") + ", ";
                    strsql += Request.QueryString["iu"] + " , ";
                    strsql += " ' Aprovação Cotação : " + dtAtualizado.Rows[i]["NUMEROCOTACAO"].ToString() + "' , ";
                    strsql += " GetDate(), ";
                    strsql += dtAtualizado.Rows[i]["IDUSUARIOCENTRODECUSTOOPERACAO"].ToString() + ") ";

                    DataRow[] o = dtAprov.Select("CENTROCUSTOAPROVADO=" + Request.QueryString["i"].ToString());
                    
                    for (int isi = 0; isi < o.Length; isi++)
                    {
                        strsql += " UPDATE COTACAODECOMPRAITEM SET IDCENTRODECUSTO=" + Request.QueryString["i"] + " WHERE IDCOTACAODECOMPRAITEM = " + o[isi]["IDCOTACAODECOMPRAITEM"].ToString();
                    }

                    // se todos os centros de custos aprovaram
                    if (dtAprov.Rows.Count > 0 && int.Parse(dtAprov.Compute("count(CENTROCUSTOAPROVADO)", " CENTROCUSTOAPROVADO =0").ToString()) == 0)
                        strsql += " UPDATE COTACAODECOMPRA SET STATUS='COTACAO APROVADA' WHERE IDCOTACAODECOMPRA=" + dtAtualizado.Rows[i]["NUMEROCOTACAO"].ToString();
                }

            }

            if (strsql.Trim() == "")
            {
                throw new Exception("Não foi possível aprovar a(s) cotação(ões) em negrito, pois o valor excede o Limite.");
            }

            Sistran.Library.GetDataTables.ExecutarComandoSql(strsql, new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"]).ToString());

            dvErro.Visible = true;
            lblMensagem.Text = "<br>Cotação Aprovada com sucesso! <br><br><br> Clique em OK para finalizar.";
            btnVoltarDv.Visible = false;
            btnSairDv.Visible = true;
            Panel1.Enabled = false;
            LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
            lnkLogout.Focus();
        }
        catch (Exception ex)
        {
            gerarErro("Ocorreu algum problema, clique Voltar e Corrigir e verifique os campos. " + ex.Message);
        }
    }

    private DataTable RetornarAprov(string idCotacaoDeCompra)
    {
        string strsql = " SELECT   ";
        strsql += " RMI.IDCENTRODECUSTO, CCI.IDCOTACAODECOMPRAITEM, isnull(CCI.IDCENTRODECUSTO,0) CENTROCUSTOAPROVADO  ";
        strsql += " FROM COTACAODECOMPRA CC     ";
        strsql += " INNER JOIN COTACAOFORNECEDOR CF ON (CF.IDCOTACAODECOMPRA =CC.IDCOTACAODECOMPRA)     ";
        strsql += " INNER JOIN COTACAODECOMPRAITEM CCI ON (CCI.IDCOTACAOFORNECEDOR =CF.IDCOTACAOFORNECEDOR)     ";
        strsql += " INNER JOIN REQUISICAODEMATERIALDOCUMENTO RMD ON (RMD.IDCOTACAODECOMPRAITEM =CCI.IDCOTACAODECOMPRAITEM)     ";
        strsql += " INNER JOIN REQUISICAODEMATERIALITEM RMI ON (RMI.IDREQUISICAODEMATERIALITEM =RMD.IDREQUISICAODEMATERIALITEM)     ";
        strsql += " INNER JOIN USUARIOCENTRODECUSTO UCC ON (UCC.IDCENTRODECUSTO =RMI.IDCENTRODECUSTO)     ";
        strsql += " INNER JOIN USUARIOCENTRODECUSTOOPERACAO UCCO ON (UCCO.IDUSUARIOCENTRODECUSTO = UCC.IDUSUARIOCENTRODECUSTO)     ";
        strsql += " INNER JOIN USUARIO US ON (US.IDUSUARIO = UCC.IDUSUARIO)   INNER JOIN CADASTRO CAD ON (CAD.IDCADASTRO = US.IDCADASTRO)    ";
        strsql += " INNER JOIN CADASTROCONTATOENDERECO CCE ON (CCE.IDCADASTRO = CAD.IDCADASTRO)     ";
        strsql += " INNER JOIN CADASTRO CADFOR ON CADFOR.IDCADASTRO = CF.IDFORNECEDOR    ";
        strsql += " INNER JOIN CENTRODECUSTO CCUSTO ON CCUSTO.IDCENTRODECUSTO = UCC.IDCENTRODECUSTO    ";
        strsql += " WHERE CC.STATUS='AGUARDANDO APROVACAO COTACAO'    ";
        strsql += " AND CF.APROVADA='SIM'     ";
        strsql += " AND UCCO.IDOPERACAO=2     ";
        strsql += " AND CC.IDCOTACAODECOMPRA =  " + idCotacaoDeCompra;
        return Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"]).ToString()).Tables[0];
    }
}