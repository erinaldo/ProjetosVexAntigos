using System;
using Sistran;
using System.Data;
using System.Threading;
using System.Globalization;
using SistranBLL;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;

public partial class frmaprovarPedidoDeCompra : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (Request.QueryString["i"] == null)
        {
            dvErro.Visible = true;
            lblMensagem.Text = "NO MOMENTO NÃO EXISTE PEDIDO DE COMPRA PENDENTE DE APROVAÇÃO.";
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
                lblMensagem.Text = "NO MOMENTO NÃO EXISTE PEDIDO DE COMPRA PENDENTE DE APROVAÇÃO.";
                Panel1.Visible = false;
                btnVoltarDv.Focus();
                btnSairDv.Visible = true;
                return;

            }

            Label lblUserName = (Label)Master.FindControl("lblUserName");
            lblUserName.Text = "Bem Vindo: " + dt.Rows[0]["NOME"].ToString();

            PlaceHolder2.Controls.Clear();


            #region itens

            PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='4' class='tdpCabecalhoMenor' align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>CENTRO DE CUSTO: " + dt.Rows[0]["CENTROCUSTO"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='4' class='tdpCabecalhoMenor' align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>USUÁRIO: " + dt.Rows[0]["NOME"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='4' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'><hr>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));



            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'  class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>NÚMERO DO PEDIDO</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp'  class='tdpCabecalhoMenor' nowrap=nowrap style='font-size:8pt;'><b>FORNECEDOR</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));



            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCenter' align='center' nowrap=nowrap style='font-size:8pt;'><b>DATA</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'  class='tdpCabecalhoMenor' nowrap=nowrap style='font-size:8pt;'><b>VALOR</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));


            foreach (DataRow rw in dt.Rows)
            {


                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));


                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' align='right' nowrap=nowrap style='font-size:8pt;'><a href='http://www.grupologos.com.br/SistranWeb.NET/frmEntregas.aspx?idDoc=" + rw["iddocumento"].ToString() + "' target='_blank' class='link'> " + rw["NUMERO"].ToString() + "</a>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='LEFT' nowrap=nowrap style='font-size:8pt;'>" + rw["RAZAOSOCIALNOME"].ToString()));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap style='font-size:8pt;'>" + DateTime.Parse(rw["DATADEEMISSAO"].ToString()).ToString("dd/MM/yyyy")));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:8pt;'>" + decimal.Parse(rw["VALORDANOTA"].ToString()).ToString("#0.00")));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));

            }


            #endregion

            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>TOTAL:</b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b></b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b></b>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>" + decimal.Parse(dt.Compute("SUM(VALORDANOTA)", "").ToString()).ToString("#0.00") + "</b>"));
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
        strsql += " SELECT DISTINCT D.IDFILIAL,";
        strsql += " D.IDDOCUMENTO, D.NUMERO , CC.NOME CENTROCUSTO, CC.IDCENTRODECUSTO, D.IDREMETENTE , CADFOR.RAZAOSOCIALNOME , US.IDUSUARIO, US.NOME, UCCO.IDUSUARIOCENTRODECUSTOOPERACAO, DATADEEMISSAO, VALORDANOTA ";
        strsql += " FROM DOCUMENTO D  ";
        strsql += " INNER JOIN CADASTRO CADFOR ON CADFOR.IDCADASTRO = D.IDREMETENTE ";
        strsql += " INNER JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = D.IDDOCUMENTO ";
        strsql += " INNER JOIN LANCAMENTO LAN ON LAN.IDDOCUMENTOORIGEM = D.IDDOCUMENTO ";
        strsql += " INNER JOIN LANCAMENTOCONTABILCC LCC ON LCC.IDLANCAMENTO = LAN.IDLANCAMENTO ";
        strsql += " INNER JOIN CENTRODECUSTOFILIAL CCF ON CCF.IDCENTRODECUSTOFILIAL  = LCC.IDCENTRODECUSTOFILIAL ";
        strsql += " INNER JOIN CENTRODECUSTO CC ON CC.IDCENTRODECUSTO = CCF.IDCENTRODECUSTO ";
        strsql += " INNER JOIN USUARIOCENTRODECUSTO UCC ON (UCC.IDCENTRODECUSTO=CC.IDCENTRODECUSTO)   ";
        strsql += " INNER JOIN USUARIOCENTRODECUSTOOPERACAO UCCO ON (UCCO.IDUSUARIOCENTRODECUSTO=UCC.IDUSUARIOCENTRODECUSTO)   ";
        strsql += " INNER JOIN USUARIO US ON (US.IDUSUARIO=UCC.IDUSUARIO)   ";
        strsql += " INNER JOIN CADASTRO C ON (C.IDCADASTRO = US.IDCADASTRO)   ";
        strsql += " INNER JOIN CADASTROCONTATOENDERECO CCE ON (CCE.IDCADASTRO = C.IDCADASTRO)   ";
        strsql += " WHERE  ";
        strsql += " D.IDDOCUMENTO = " + Request.QueryString["I"];
        strsql += " AND UCC.IDUSUARIO =" + Request.QueryString["iu"];
        strsql += " AND CC.IDCENTRODECUSTO=" + Request.QueryString["idc"];
        strsql += " AND DF.SITUACAO = 'AGUARDANDO APROVACAO DO PEDIDO' ";

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
                int centrosDeCustoPendentes = RetornarAprov();

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
                strsql += " ' Aprovação Pedido de  compra : " + dtAtualizado.Rows[i]["NUMERO"].ToString() + "' , ";
                strsql += " GetDate(), ";
                strsql += dtAtualizado.Rows[i]["IDUSUARIOCENTRODECUSTOOPERACAO"].ToString() + ") ";


                if (centrosDeCustoPendentes == 0)
                    strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='PEDIDO DE COMPRA APROVADO' WHERE IDDOCUMENTO=" + Request.QueryString["I"];

                strsql += " insert into documentoocorrencia (IDDOCUMENTOOCORRENCIA, IDFILIAL, IDDOCUMENTO, IDUSUARIO, DATAOCORRENCIA, DATALANCAMENTO, DESCRICAO) ";
                strsql += " values(" + Sistran.Library.GetDataTables.RetornarIdTabela("documentoocorrencia") + ", " + dtAtualizado.Rows[i]["idfilial"].ToString() + ", " + Request.QueryString["i"] + ", " + Request.QueryString["iu"] + ", GETDATE(), GETDATE(), 'PEDIDO DE COMPRA APROVADO')";
            }

            if (strsql.Trim() == "")
            {
                throw new Exception("Não foi possível aprovar a(s) pedido(s) em negrito, pois o valor excede o Limite.");
            }

            Sistran.Library.GetDataTables.ExecutarComandoSql(strsql, new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"]).ToString());

            dvErro.Visible = true;
            lblMensagem.Text = "<br>Pedido de compra aprovado com sucesso! <br><br><br> Clique em OK para finalizar.";
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

    public int RetornarAprov()
    {
        string strsql = "SELECT  ";
        strsql += " count( DISTINCT CC.IDCENTRODECUSTO) ";
        strsql += "  FROM DOCUMENTO D  ";
        strsql += "  INNER JOIN CADASTRO CADFOR ON CADFOR.IDCADASTRO = D.IDREMETENTE ";
        strsql += "  INNER JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = D.IDDOCUMENTO ";
        strsql += "  INNER JOIN LANCAMENTO LAN ON LAN.IDDOCUMENTOORIGEM = D.IDDOCUMENTO ";
        strsql += "  INNER JOIN LANCAMENTOCONTABILCC LCC ON LCC.IDLANCAMENTO = LAN.IDLANCAMENTO ";
        strsql += "  INNER JOIN CENTRODECUSTOFILIAL CCF ON CCF.IDCENTRODECUSTOFILIAL  = LCC.IDCENTRODECUSTOFILIAL ";
        strsql += "  INNER JOIN CENTRODECUSTO CC ON CC.IDCENTRODECUSTO = CCF.IDCENTRODECUSTO ";
        strsql += "  INNER JOIN USUARIOCENTRODECUSTO UCC ON (UCC.IDCENTRODECUSTO=CC.IDCENTRODECUSTO)   ";
        strsql += "  INNER JOIN USUARIOCENTRODECUSTOOPERACAO UCCO ON (UCCO.IDUSUARIOCENTRODECUSTO=UCC.IDUSUARIOCENTRODECUSTO)   ";
        strsql += "  INNER JOIN USUARIO US ON (US.IDUSUARIO=UCC.IDUSUARIO)   ";
        strsql += "  INNER JOIN CADASTRO C ON (C.IDCADASTRO = US.IDCADASTRO)   ";
        strsql += "  INNER JOIN CADASTROCONTATOENDERECO CCE ON (CCE.IDCADASTRO = C.IDCADASTRO)   ";
        strsql += "  WHERE  ";
        strsql += "  D.IDDOCUMENTO = " + Request.QueryString["I"];
        strsql += "  AND DF.SITUACAO = 'AGUARDANDO APROVACAO DO PEDIDO' ";
        strsql += "  and cc.IDCentroDeCusto <> " + Request.QueryString["IDC"];

        return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"]).ToString());
    }
}