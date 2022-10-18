using System;
using Sistran;
using System.Data;
using System.Threading;
using System.Globalization;
using SistranBLL;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;

public partial class frmaprovarRequisicao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (Request.QueryString["i"] == null)
        {
            dvErro.Visible = true;
            lblMensagem.Text = "NO MOMENTO NÃO EXISTE REQUISIÇÕES PEDNDENTES DE APÇROVAÇÃO PARA ESTE CENTRO DE CUSTO";
            Panel1.Enabled = false;
            Panel1.Visible = false;

            btnVoltarDv.Focus();
            btnSairDv.Visible = true;
            return;

        }
     
        carregar();

        btnsAIR.Attributes.Add("onClick", "javascript:confirmarFechamnento();");

    }

    private void carregar()
    {
        try
        {


            DataTable dt;

            if (Session["dtRequisicao"] == null)
                dt = Retornar();
            else
                dt = (DataTable)Session["dtRequisicao"];


            Session["dtRequisicao"] = dt;            
            if (dt.Rows.Count == 0)
            {
                dvErro.Visible = true;
                lblMensagem.Text = "NO MOMENTO NÃO EXISTE REQUISIÇÕES PEDNDENTES DE APÇROVAÇÃO PARA ESTE CENTRO DE CUSTO";
                Panel1.Visible = false;
                btnVoltarDv.Focus();
                btnSairDv.Visible = true;
                return;

            }   
            
            Label lblUserName = (Label)Master.FindControl("lblUserName");
            lblUserName.Text = "Bem Vindo: " + dt.Rows[0]["USUARIO"].ToString();          

            phCentroCusto.Controls.Clear();
            phDados.Controls.Clear();
            PhUsuario.Controls.Clear();
          
            #region Centro de Custo
            phCentroCusto.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

            phCentroCusto.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phCentroCusto.Controls.Add(new LiteralControl(@"<td colspan='1' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>CENTRO DE CUSTO"));
            phCentroCusto.Controls.Add(new LiteralControl(@"</td>"));
            phCentroCusto.Controls.Add(new LiteralControl(@"</tr>"));

            phCentroCusto.Controls.Add(new LiteralControl(@"<tr>"));
            phCentroCusto.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap style='font-size:8pt;width:1%'><B>" + dt.Rows[0]["CENTRODECUSTO"].ToString()));
            phCentroCusto.Controls.Add(new LiteralControl(@"</B></td>"));
            phCentroCusto.Controls.Add(new LiteralControl(@"</tr>"));

            phCentroCusto.Controls.Add(new LiteralControl(@"<tr>"));
            phCentroCusto.Controls.Add(new LiteralControl(@"<td><HR>"));
            phCentroCusto.Controls.Add(new LiteralControl(@"</td>"));
            phCentroCusto.Controls.Add(new LiteralControl(@"</tr>"));

            phCentroCusto.Controls.Add(new LiteralControl(@"</table>"));
            #endregion
            
            #region USUARIO

            PhUsuario.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

            PhUsuario.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PhUsuario.Controls.Add(new LiteralControl(@"<td colspan='1' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>USUÁRIO"));
            PhUsuario.Controls.Add(new LiteralControl(@"</td>"));
            PhUsuario.Controls.Add(new LiteralControl(@"</tr>"));

            DataRow[] oLinhaUser = dt.Select("IdUsuario=" + Request.QueryString["iu"]);

            PhUsuario.Controls.Add(new LiteralControl(@"<tr>"));
            PhUsuario.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap style='font-size:8pt;width:1%'><B>" + oLinhaUser[0]["USUARIO"].ToString()));
            PhUsuario.Controls.Add(new LiteralControl(@"</B></td>"));
            PhUsuario.Controls.Add(new LiteralControl(@"</tr>"));


            PhUsuario.Controls.Add(new LiteralControl(@"<tr>"));
            PhUsuario.Controls.Add(new LiteralControl(@"<td><HR>"));
            PhUsuario.Controls.Add(new LiteralControl(@"</td>"));
            PhUsuario.Controls.Add(new LiteralControl(@"</tr>"));

            PhUsuario.Controls.Add(new LiteralControl(@"</table>"));
            #endregion
                        
            #region itens

            phDados.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
            phDados.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phDados.Controls.Add(new LiteralControl(@"<td colspan='6' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'><HR>"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));
            phDados.Controls.Add(new LiteralControl(@"</tr>"));


            phDados.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phDados.Controls.Add(new LiteralControl(@"<td colspan='6' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>ITENS"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));
            phDados.Controls.Add(new LiteralControl(@"</tr>"));



            phDados.Controls.Add(new LiteralControl(@"<tr style='background-image:url(../Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phDados.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>CÓDIGO DO PRODUTO</b>"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>DESCRIÇÃO</b>"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpR'  class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>QUANTIDADE</b>"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpR'  class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>NÚMERO REQUISIÇÃO</b>"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));


            phDados.Controls.Add(new LiteralControl(@"<td class='tdp' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>SOLICITANTE</b>"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdp' class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;'><b>STATUS</b>"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));


            phDados.Controls.Add(new LiteralControl(@"</tr>"));          

            foreach (DataRow rw in dt.Rows)
            {
                phDados.Controls.Add(new LiteralControl(@"<tr>"));
                phDados.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + rw["CODIGO"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + rw["DESCRICAO"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:8pt;'>"));

                if (rw["IDUSUARIO"].ToString() == Request.QueryString["iu"])
                       phDados.Controls.Add(CriarTXT(rw["IDREQUISICAODEMATERIALITEM"].ToString(), decimal.Parse(rw["QUANTIDADESOLICITADA"].ToString()), rw["ANDAMENTO"].ToString(), rw["IDUSUARIO"].ToString()));
                else
                    phDados.Controls.Add(new LiteralControl(rw["QUANTIDADESOLICITADA"].ToString()));



                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap style='font-size:8pt;'>" + rw["IDREQUISICAODEMATERIAL"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdp'  nowrap=nowrap style='font-size:8pt;'>" + rw["SOLICITANTE"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap style='font-size:8pt;'>" + rw["ANDAMENTO"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));


                phDados.Controls.Add(new LiteralControl(@"</tr>"));
            }            

            phDados.Controls.Add(new LiteralControl(@"</table>"));

            #endregion

        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable Retornar()
    {
        string strsql = "";
        strsql += " SELECT distinct UCCO.IDUSUARIOCENTRODECUSTOOPERACAO , ucc.IdUsuario,";
        strsql += " RM.IDREQUISICAODEMATERIAL,  ";
        strsql += " RMI.IDREQUISICAODEMATERIALITEM,  ";
        strsql += " RMI.IDCENTRODECUSTO,  ";
        strsql += " RM.STATUS,  ";
        strsql += " RMI.ANDAMENTO,  ";
        strsql += " U.NOME SOLICITANTE, ";
        strsql += " PC.CODIGO, ";
        strsql += " PC.IDPRODUTOCLIENTE, PC.DESCRICAO,";
        strsql += " RMI.QUANTIDADESOLICITADA, ";
        strsql += " RMI.QUANTIDADEATENDIDA, USUCC.NOME  USUARIO,  CC.Nome CENTRODECUSTO";
        strsql += " FROM REQUISICAODEMATERIAL RM ";
        strsql += " INNER JOIN REQUISICAODEMATERIALITEM RMI ";
        strsql += " ON (RM.IDREQUISICAODEMATERIAL = RMI.IDREQUISICAODEMATERIAL) ";
        strsql += " INNER JOIN USUARIOCENTRODECUSTO UCC  ";
        strsql += " ON (UCC.IDCENTRODECUSTO = RMI.IDCENTRODECUSTO) ";
        strsql += " INNER JOIN USUARIOCENTRODECUSTOOPERACAO UCCO  ";
        strsql += " ON (UCCO.IDUSUARIOCENTRODECUSTO = UCC.IDUSUARIOCENTRODECUSTO) ";
        strsql += " INNER JOIN OPERACAO OP ";
        strsql += " ON (OP.IDOPERACAO = UCCO.IDOPERACAO) ";
        strsql += " INNER JOIN PRODUTOCLIENTE PC ";
        strsql += " ON (PC.IDPRODUTOCLIENTE = RMI.IDPRODUTOCLIENTE) ";
        strsql += " INNER JOIN USUARIO U  ";
        strsql += " ON (U.IDUSUARIO = RM.IDUSUARIOCOMPRA) ";
        strsql += " INNER JOIN USUARIO Usucc  ";
        strsql += " ON (USUCC.IDUSUARIO = UCC.IDUSUARIO) INNER JOIN CentroDeCusto CC ON (CC.IDCentroDeCusto = UCC.IdCentroDeCusto)";
        strsql += " WHERE   ucc.IdUsuario = "+Request.QueryString["iu"]+" AND RMI.IDCENTRODECUSTO=" + Request.QueryString["i"];
        strsql += " AND RM.STATUS='AGUARDANDO APROVACAO' ";
        strsql += " AND RM.ATIVO='SIM' ";
        strsql += " AND OP.OPERACAO='REQUISICAO DE MATERIAL'  ORDER BY 1,5";

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
       

    #region T X T  D I N A M I C O

    public TextBox CriarTXT(string idItem, decimal valor, string status, string idusuario)
    {
        TextBox txt = new TextBox();
        txt.ID = "txt" + idItem.ToString();
        txt.Width = 60;
        txt.CssClass = "txtValor";
        txt.Text = valor.ToString("#0.00");
        txt.Attributes.Add("onkeypress", "return SomenteNumeroDecimal(event)");
        txt.Attributes.Add("onFocus", "LimpaValoresZerados(this)");
        txt.Attributes.Add("onBlur", "ColocaValoresZerados(this)");

        if (status != "AGUARDANDO APROVACAO")
            txt.Enabled = false;


        if (idusuario != Request.QueryString["iu"])
            txt.Enabled = false;


        return txt;
    }

    public Label Criarlabel(string idItem, decimal valor, string status, string idusuario)
    {
        Label txt = new Label();
        txt.ID = "lbl" + idItem.ToString();
        txt.Width = 60;
        txt.CssClass = "txtValor";
        txt.Text = valor.ToString("#0.00");
        txt.Attributes.Add("onkeypress", "return SomenteNumeroDecimal(event)");
        txt.Attributes.Add("onFocus", "LimpaValoresZerados(this)");
        txt.Attributes.Add("onBlur", "ColocaValoresZerados(this)");

        if (status != "AGUARDANDO APROVACAO")
            txt.Enabled = false;


        if (idusuario != Request.QueryString["iu"])
            txt.Enabled = false;


        return txt;
    }

     

    #endregion



    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {


            DataTable dt = (DataTable)Session["dtRequisicao"];
            DataTable dtAtualizado = dt.Clone();



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //DataRow oLinha = dt.Rows[i];

                if (Request.QueryString["iu"] == dt.Rows[i]["idusuario"].ToString())
                {
                    DataRow oLinha = dtAtualizado.NewRow();

                    oLinha["IDUSUARIOCENTRODECUSTOOPERACAO"] = dt.Rows[i]["IDUSUARIOCENTRODECUSTOOPERACAO"];
                    oLinha["IdUsuario"] = dt.Rows[i]["IdUsuario"];
                    oLinha["IDREQUISICAODEMATERIAL"] = dt.Rows[i]["IDREQUISICAODEMATERIAL"];
                    oLinha["IDREQUISICAODEMATERIALITEM"] = dt.Rows[i]["IDREQUISICAODEMATERIALITEM"];
                    oLinha["IDCENTRODECUSTO"] = dt.Rows[i]["IDCENTRODECUSTO"];
                    oLinha["STATUS"] = dt.Rows[i]["STATUS"];
                    oLinha["ANDAMENTO"] = dt.Rows[i]["ANDAMENTO"];
                    oLinha["SOLICITANTE"] = dt.Rows[i]["SOLICITANTE"];
                    oLinha["CODIGO"] = dt.Rows[i]["CODIGO"];
                    oLinha["IDPRODUTOCLIENTE"] = dt.Rows[i]["IDPRODUTOCLIENTE"];
                    oLinha["DESCRICAO"] = dt.Rows[i]["DESCRICAO"];
                    oLinha["QUANTIDADESOLICITADA"] = dt.Rows[i]["QUANTIDADESOLICITADA"];
                    oLinha["QUANTIDADEATENDIDA"] = dt.Rows[i]["QUANTIDADEATENDIDA"];
                    oLinha["USUARIO"] = dt.Rows[i]["USUARIO"];
                    oLinha["CENTRODECUSTO"] = dt.Rows[i]["CENTRODECUSTO"];

                    dtAtualizado.Rows.Add(oLinha);
                }

            }



            string strsql = "";
            int contarReprov = 0;
            int contarAprov = 0;
            int qtdItensRequisicao = 0;

            for (int i = 0; i < dtAtualizado.Rows.Count; i++)
            {

                TextBox txt = (TextBox)phDados.FindControl("txt" + dtAtualizado.Rows[i]["IdRequisicaoDeMaterialItem"].ToString());
                if ("txt" + dtAtualizado.Rows[i]["IdRequisicaoDeMaterialItem"].ToString() == txt.ID)
                {
                    dt.Rows[i]["QuantidadeSolicitada"] = txt.Text;

                    if (txt.Text == "" || decimal.Parse(txt.Text) == decimal.Parse("0"))
                    {
                        //contarReprov++;
                        strsql += " UPDATE RequisicaoDeMaterialItem SET QuantidadeSolicitada=" + (txt.Text.Replace(",", ".")==""? "0": txt.Text.Replace(",", ".")) + ", Andamento='ITEM REPROVADO' WHERE IdRequisicaoDeMaterialItem=" + txt.ID.Replace("txt", "");
                        dt.Rows[i]["Andamento"] = "ITEM REPROVADO";
                    }
                    else
                    {
                        //contarAprov++;
                        strsql += " UPDATE RequisicaoDeMaterialItem SET QuantidadeSolicitada=" + txt.Text.Replace(",", ".") + ", Andamento='ITEM APROVADO' WHERE IdRequisicaoDeMaterialItem=" + txt.ID.Replace("txt", "");
                        dt.Rows[i]["Andamento"] = "ITEM APROVADO";

                    }
                }
            }

            string idrequisicaodecompra = "";
            for (int i = 0; i < dtAtualizado.Rows.Count; i++)
            {
                if (idrequisicaodecompra != dtAtualizado.Rows[i]["idrequisicaodematerial"].ToString())
                {
                    qtdItensRequisicao = int.Parse(dt.Compute("count(idrequisicaodematerial)", "idusuario=" + Request.QueryString["iu"] + "   and idrequisicaodematerial=" + dtAtualizado.Rows[i]["idrequisicaodematerial"].ToString()).ToString());
                    contarReprov = int.Parse(dt.Compute("count(idrequisicaodematerial)", "Andamento='ITEM REPROVADO'  AND idusuario=" + Request.QueryString["iu"] + "   and idrequisicaodematerial=" + dtAtualizado.Rows[i]["idrequisicaodematerial"].ToString()).ToString());
                    contarAprov = int.Parse(dt.Compute("count(idrequisicaodematerial)", "Andamento='ITEM APROVADO'  AND idusuario=" + Request.QueryString["iu"] + "   and idrequisicaodematerial=" + dtAtualizado.Rows[i]["idrequisicaodematerial"].ToString()).ToString());

                    string status = "";
                    if (contarReprov == qtdItensRequisicao)
                        status = "REPROVADA E-MAIL";
                    else
                        status = "APROVADA E-MAIL";

                    if (!verificarSeExixtemNaoAprovados(dtAtualizado.Rows[i]["IDCENTRODECUSTO"].ToString(), dtAtualizado.Rows[i]["idrequisicaodematerial"].ToString()))
                        strsql += "  UPDATE RequisicaoDeMaterial SET Status='" + status + "' WHERE IdRequisicaoDeMaterial=" + dtAtualizado.Rows[i]["idrequisicaodematerial"].ToString();

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
                    strsql += " '" + status + " Requisição de Material Número: " + dtAtualizado.Rows[i]["idrequisicaodematerial"].ToString() + "' , ";
                    strsql += " GetDate(), ";
                    strsql += dtAtualizado.Rows[i]["IDUSUARIOCENTRODECUSTOOPERACAO"].ToString() + ") ";


                }

                idrequisicaodecompra = dtAtualizado.Rows[i]["idrequisicaodematerial"].ToString();
            }

           // throw new Exception("Test de erro");
            Sistran.Library.GetDataTables.ExecutarComandoSql(strsql, new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"]).ToString());
            dvErro.Visible = true;
            lblMensagem.Text = "<br>Requisição Aprovada com sucesso! <br><br><br> Clique em OK para finalizar.";
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

    private bool verificarSeExixtemNaoAprovados(string idCentrodeCusto, string idRequisicaoMaterial  )
    {
        //bool m = false;
        string strsql = "SELECT COUNT(*) ITENSNAOAPROVADO  FROM REQUISICAODEMATERIALITEM WHERE IDREQUISICAODEMATERIAL = "+ idRequisicaoMaterial+" AND IDCENTRODECUSTO <> " +idCentrodeCusto + " AND ANDAMENTO='AGUARDANDO APROVACAO'";
        int mx = Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"]).ToString());

        if (mx > 0)
            return true;
        else
            return false;

    }


    protected void Button1_Click1(object sender, EventArgs e)
    {
      
    }
}