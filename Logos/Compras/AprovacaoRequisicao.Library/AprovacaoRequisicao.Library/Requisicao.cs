using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace AprovacaoRequisicao.Library
{
    public static class Requisicao
    {
        public static void RequisicoesAguaguardandoAprovacao()
        {
            string strsql = " SELECT DISTINCT RMI.IDCENTRODECUSTO, CCE.ENDERECO, C.RAZAOSOCIALNOME NOME,  UCC.IDUSUARIO ";
            strsql += " FROM REQUISICAODEMATERIAL RM ";
            strsql += " INNER JOIN REQUISICAODEMATERIALITEM RMI ON (RMI.IDREQUISICAODEMATERIAL=RM.IDREQUISICAODEMATERIAL) ";
            strsql += " INNER JOIN USUARIOCENTRODECUSTO UCC ON (UCC.IDCENTRODECUSTO=RMI.IDCENTRODECUSTO) ";
            strsql += " INNER JOIN UsuarioCentroDeCustoOperacao UCCO ON (UCCO.IdUsuarioCentroDeCusto=UCC.IdUsuarioCentroDeCusto) ";
            strsql += " INNER JOIN USUARIO US ON (US.IDUSUARIO=UCC.IDUSUARIO) ";
            strsql += " INNER JOIN CADASTRO C ON (C.IDCADASTRO = US.IDCADASTRO) ";
            strsql += " INNER JOIN CADASTROCONTATOENDERECO CCE ON (CCE.IDCADASTRO = C.IDCADASTRO) ";
            strsql += " WHERE RM.STATUS='AGUARDANDO APROVACAO' ";
            strsql += " AND CCE.IDCADASTROTIPODECONTATO=1 AND UCCO.IDOPERACAO=1";

            DataTable dt = AprovacaoRequisicao.Library.cBD.RetonarTablePorConexao(strsql, Conexao.stringConexao());

            StreamReader objReader = new StreamReader("AprovacaoRequisicao.htm");

            string sLine = "";
            string carta = "";

            sLine = objReader.ReadToEnd();
            objReader.Close();

            string ReportEmail = "RequisicoesAguaguardandoAprovacao: <br>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ReportEmail += dt.Rows[i]["ENDERECO"].ToString() + "<br>";

                carta = sLine;
                carta = carta.Replace("@@DATA@@", DateTime.Now.ToString());
                carta = carta.Replace("@@ID@@", dt.Rows[i]["IDCENTRODECUSTO"].ToString());
                carta = carta.Replace("@@NOME@@", dt.Rows[i]["NOME"].ToString());
                carta = carta.Replace("@@idusuario@@", dt.Rows[i]["IDUSUARIO"].ToString());
                EnviarEmails.EnviarEmail(dt.Rows[i]["ENDERECO"].ToString(), "moises@sistecno.com.br", "Requisição Pendente de Aprovação", carta, "mail.sistecno.com.br", "@oncetsis14", "Aprovação de Requisição");
//                EnviarEmails.EnviarEmail(dt.Rows[i]["ENDERECO"].ToString(), "moises@sistecno.com.br", "Cotação de Compra Pendente", carta, "mail.sistecno.com.br", "@oncetsis14", "Cotação de Compra");
                //
                //new cEmail().enviarEmail("Requisição Pendente de Aprovação", carta, dt.Rows[i]["ENDERECO"].ToString(), "RequisicaoCompra");
            }

            ReportEmail += "<br>" + DateTime.Now.ToString();
            //new cEmail().enviarEmail("REPORT: Requisição Pendente de Aprovação", ReportEmail, "moises@sistecno.com.br;", "RequisicaoCompra");
        }
    }

    public static class Cotacao
    {
        public static void CotacaoAguaguardandoAprovacao()
        {
            string strsql = " SELECT DISTINCT RMI.IDCENTRODECUSTO, CCE.ENDERECO, CAD.RAZAOSOCIALNOME NOME,  UCC.IDUSUARIO ";
            strsql += " FROM COTACAODECOMPRA CC ";
            strsql += " INNER JOIN COTACAOFORNECEDOR CF ON (CF.IDCOTACAODECOMPRA = CC.IDCOTACAODECOMPRA) ";
            strsql += " INNER JOIN COTACAODECOMPRAITEM CCI ON (CCI.IDCOTACAOFORNECEDOR = CF.IDCOTACAOFORNECEDOR) ";
            strsql += " INNER JOIN REQUISICAODEMATERIALDOCUMENTO RMD ON (RMD.IDCOTACAODECOMPRAITEM = CCI.IDCOTACAODECOMPRAITEM) ";
            strsql += " INNER JOIN REQUISICAODEMATERIALITEM RMI ON (RMI.IDREQUISICAODEMATERIALITEM = RMD.IDREQUISICAODEMATERIALITEM) ";
            strsql += " INNER JOIN USUARIOCENTRODECUSTO UCC ON (UCC.IDCENTRODECUSTO = RMI.IDCENTRODECUSTO) ";
            strsql += " INNER JOIN USUARIOCENTRODECUSTOOPERACAO UCCO ON (UCCO.IDUSUARIOCENTRODECUSTO = UCC.IDUSUARIOCENTRODECUSTO) ";
            strsql += " INNER JOIN USUARIO US ON (US.IDUSUARIO = UCC.IDUSUARIO) ";
            strsql += " INNER JOIN CADASTRO CAD ON (CAD.IDCADASTRO = US.IDCADASTRO) ";
            strsql += " INNER JOIN CADASTROCONTATOENDERECO CCE ON (CCE.IDCADASTRO = CAD.IDCADASTRO)   ";
            strsql += " WHERE CC.STATUS='AGUARDANDO APROVACAO COTACAO' ";
            strsql += " AND CF.APROVADA='SIM'  ";
            strsql += " AND UCCO.IDOPERACAO=2 and CCE.IDCADASTROTIPODECONTATO = 15";

            DataTable dt = AprovacaoRequisicao.Library.cBD.RetonarTablePorConexao(strsql, Conexao.stringConexao());

            StreamReader objReader = new StreamReader("AprovacaoCotacao.htm");

            string sLine = "";
            string carta = "";

            sLine = objReader.ReadToEnd();
            objReader.Close();

            string ReportEmail = "CotacaoAguaguardandoAprovacao: <br>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ReportEmail += dt.Rows[i]["ENDERECO"].ToString() + "<br>";
                carta = sLine;
                carta = carta.Replace("@@DATA@@", DateTime.Now.ToString());
                carta = carta.Replace("@@ID@@", dt.Rows[i]["IDCENTRODECUSTO"].ToString());
                carta = carta.Replace("@@NOME@@", dt.Rows[i]["NOME"].ToString());
                carta = carta.Replace("@@idusuario@@", dt.Rows[i]["IdUsuario"].ToString());




                EnviarEmails.EnviarEmail(dt.Rows[i]["ENDERECO"].ToString(), "moises@sistecno.com.br", "Cotação de Compra Pendente", carta, "mail.sistecno.com.br", "@oncetsis14", "Cotação de Compra");
                //new cEmail().enviarEmail("Cotação de Compra Pendente de Aprovação", carta, dt.Rows[i]["ENDERECO"].ToString(), "CotacaoCompra");

            }
            ReportEmail += "<br>" + DateTime.Now.ToString();
           // new cEmail().enviarEmail("REPORT: Cotação de Compra Pendente", ReportEmail, "moises@sistecno.com.br;", "CotacaoCompra");

        }
    }

    public static class PedidoDeCompra
    {
        public static void PedidoDeCompraAguardndoAprovacao()
        {
            string strsql = " SELECT D.IDDOCUMENTO, CCE.ENDERECO, US.IDUSUARIO, CC.IDCENTRODECUSTO , US.NOME   ";
            strsql += " FROM DOCUMENTO D  ";
            strsql += " INNER JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = D.IDDOCUMENTO ";
            strsql += " INNER JOIN LANCAMENTO LAN ON LAN.IDDOCUMENTOORIGEM = D.IDDOCUMENTO ";
            strsql += " INNER JOIN LANCAMENTOCONTABILCC LCC ON LCC.IDLANCAMENTO = LAN.IDLANCAMENTO ";
            strsql += " INNER JOIN CENTRODECUSTOFILIAL CCF ON CCF.IDCENTRODECUSTOFILIAL  = LCC.IDCENTRODECUSTOFILIAL ";
            strsql += " INNER JOIN CENTRODECUSTO CC ON CC.IDCENTRODECUSTO = CCF.IDCENTRODECUSTO ";
            strsql += " INNER JOIN USUARIOCENTRODECUSTO UCC ON (UCC.IDCENTRODECUSTO=CC.IDCENTRODECUSTO)  ";
            strsql += " INNER JOIN USUARIOCENTRODECUSTOOPERACAO UCCO ON (UCCO.IDUSUARIOCENTRODECUSTO=UCC.IDUSUARIOCENTRODECUSTO)   ";
            strsql += "  INNER JOIN USUARIO US ON (US.IDUSUARIO=UCC.IDUSUARIO)   ";
            strsql += " INNER JOIN CADASTRO C ON (C.IDCADASTRO = US.IDCADASTRO)   ";
            strsql += " INNER JOIN CADASTROCONTATOENDERECO CCE ON (CCE.IDCADASTRO = C.IDCADASTRO)   ";
            strsql += " WHERE D.ATIVO='SIM' ";
            strsql += " AND D.TIPODEDOCUMENTO='PEDIDO' ";
            strsql += " AND D.TIPODESERVICO = 'COMPRA' ";
            strsql += " AND DF.SITUACAO = 'AGUARDANDO APROVACAO DO PEDIDO' ";
            strsql += " AND LAN.TABELA='DOCUMENTO' ";
            strsql += " AND UCCO.IDOPERACAO=7 ";
            strsql += " AND CCE.IDCADASTROTIPODECONTATO=15 ";

            DataTable dt = AprovacaoRequisicao.Library.cBD.RetonarTablePorConexao(strsql, Conexao.stringConexao());

            StreamReader objReader = new StreamReader("AprovacaoPedidodeCompra.htm");

            string sLine = "";
            string carta = "";

            sLine = objReader.ReadToEnd();
            objReader.Close();

            string ReportEmail = "PedidoDeCompraAguardndoAprovacao: <br>";


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                carta = sLine;
                carta = carta.Replace("@@DATA@@", DateTime.Now.ToString());
                carta = carta.Replace("@@ID@@", dt.Rows[i]["IDDOCUMENTO"].ToString());
                carta = carta.Replace("@@IDCENTRODECUSTO@@", dt.Rows[i]["IDCENTRODECUSTO"].ToString());
                carta = carta.Replace("@@NOME@@", dt.Rows[i]["NOME"].ToString());
                carta = carta.Replace("@@idusuario@@", dt.Rows[i]["IdUsuario"].ToString());
                ReportEmail += "<br>" + dt.Rows[i]["ENDERECO"].ToString();

                EnviarEmails.EnviarEmail(dt.Rows[i]["ENDERECO"].ToString(), "moises@sistecno.com.br", "Pedido de Compra Pendente", carta, "mail.sistecno.com.br", "@oncetsis14", "Pedido  de Compra");
                //EnviarEmails.EnviarEmail(dt.Rows[i]["ENDERECO"].ToString(), "moises@sistecno.com.br", "Cotação de Compra Pendente", carta, "mail.sistecno.com.br", "@oncetsis14", "Cotação de Compra");

               // new cEmail().enviarEmail("Pedido de Compra Pendente", carta, dt.Rows[i]["ENDERECO"].ToString(), "AprovacaoPedidoCompra");


            }
            ReportEmail += "<br>" + DateTime.Now.ToString();
           // new cEmail().enviarEmail("REPORT: Pedido de Compra Pendente", ReportEmail, "moises@sistecno.com.br;", "AprovacaoPedidoCompra");
        }
    }
}