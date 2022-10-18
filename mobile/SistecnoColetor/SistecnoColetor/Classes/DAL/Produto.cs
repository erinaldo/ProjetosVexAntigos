using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SistecnoColetor.Classes.DAL
{
    public class Documento
    {
        public DataTable retornarConferenciaByChave(string chave, string IdDocumento)
        {
            string sql = "SELECT DISTINCT D.NUMERO, D.DATADEEMISSAO,  D.IDDOCUMENTO, D.DOCUMENTODOCLIENTE4 CHAVE, P.IDPRODUTO, P.CODIGODEBARRAS,DI.IDPRODUTOEMBALAGEM,DI.QUANTIDADE,PE.UNIDADEDOCLIENTE  FATOR, PC.CODIGO, PC.IDPRODUTOCLIENTE,PC.DESCRICAO, ISNULL(DI.VALORUNITARIO, 0) VALORUNITARIO  , CADEST.CNPJCPF + '-' + CADEST.RAZAOSOCIALNOME DESTINATARIO,  DI.IDDOCUMENTOITEM ";
            sql += " FROM DOCUMENTO D ";
            sql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTO = D.IDDOCUMENTO ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE INNER JOIN CADASTRO CADEST ON CADEST.IDCADASTRO = D.IDDESTINATARIO ";
            sql += " WHERE TIPODEDOCUMENTO  ='NOTA FISCAL' AND IDFILIAl = " + VarGlobal.Usuario.UltimaFilial;

            if(IdDocumento!="")
                sql += " AND D.IDDOCUMENTO="+ IdDocumento;
            else
                sql += " AND (DOCUMENTODOCLIENTE4='" + chave + "' or D.NUMERO='" + chave + "' )";
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }
    }

    public class Ocorrencia
    {
        public void GravarDevolucao(string IdDocumento, DataTable dtFalta)
        {
            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
            Conn.Open();
            System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();

            try
            {
                System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();
                Comm.CommandType = CommandType.Text;
                Comm.Connection = Conn;
                Comm.Transaction = trans;

                string IdDocOco = BdExterno.RetornarIDTabela("DOCUMENTOOCORRENCIA").ToString();

                string sql = "INSERT INTO DOCUMENTOOCORRENCIA (IDDOCUMENTOOCORRENCIA, IDDOCUMENTO, IDFILIAL, IDUSUARIO, DATAOCORRENCIA, DESCRICAO) ";
                sql += " VALUES (" + IdDocOco + ", " + IdDocumento + ", " + VarGlobal.Usuario.UltimaFilial + ", " + VarGlobal.Usuario.IDUsuario + ", getDate(), 'DEVOLUÇÃO')";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();

                for (int i = 0; i < dtFalta.Rows.Count; i++)
                {
                    string IdOcoItem = BdExterno.RetornarIDTabela("DocumentoOcorrenciaItem").ToString();
                    sql = "INSERT INTO DocumentoOcorrenciaItem (IdDocumentoOcorrenciaItem, IdDocumentoOcorrencia, IdDocumentoItem, Codigo, Descricao, Quantidade, ValorUnitario, TotalDoItem) ";
                    sql += " VALUES(" + IdOcoItem + ", " + IdDocOco + ", " + dtFalta.Rows[i]["IDDOCUMENTOITEM"].ToString() + ", '" + dtFalta.Rows[i]["CODIGO"].ToString() + "', '" + dtFalta.Rows[i]["DESCRICAO"].ToString() + "', " + dtFalta.Rows[i]["FALTA"].ToString().Replace(",", ".") + ", " + dtFalta.Rows[i]["VALORUNITARIO"].ToString().Replace(",", ".") + ", " + dtFalta.Rows[i]["TOTALITEM"].ToString().Replace(",", ".") + ")";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + ex.InnerException);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
    }

    public class Produto
    {
        public DataTable RetornarDadosLogistico(string CodigoDeBarras)
        {
            string sql = "";
            sql += " SELECT DISTINCT P.IDPRODUTO, PC.DESCRICAO, PC.CODIGO,   PC.IDPRODUTOCLIENTE, CNPJCPF + '-'+ LEFT(CADCLI.RAZAOSOCIALNOME , 20) CLIENTE, PC.IDCLIENTE, ISNULL(PC.LASTRO, 0.00) LASTRO, ISNULL(PC.ALTURA, 0.00) LASTROALTURA, ISNULL(P.ALTURA, 0.00) ALTURA, ISNULL(P.LARGURA, 0.00) LARGURA, ISNULL(P.COMPRIMENTO, 0.00)COMPRIMENTO, ISNULL(P.PESOLIQUIDO, 0.00) PESOLIQUIDO, ISNULL(P.PESOBRUTO, 0.00) PESOBRUTO ";
            sql += " FROM PRODUTO P  ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTO = P.IDPRODUTO ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = PC.IDCLIENTE ";
            sql += " INNER JOIN ESTOQUE E ON E.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
            sql += " WHERE CODIGODEBARRAS ='" + CodigoDeBarras + "' ";
            sql += " AND E.IDFILIAL =" + VarGlobal.Usuario.UltimaFilial;
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);

        }

        public void Alterar(SistecnoColetor.Classes.DTO.Produto pro)
        {
            string sql = "UPDATE PRODUTOCLIENTE SET LASTRO=" + pro.Lastro.Replace(",", ".") + ", ALTURA=" + pro.LastroAltura.Replace(",", ".") + " WHERE IDPRODUTOCLIENTE = " + pro.IdProdutoCliente;
            sql += ";UPDATE PRODUTO SET ALTURA=" + pro.Altura.Replace(",", ".") + ", LARGURA=" + pro.Largura.Replace(",", ".") + ", Comprimento=" + pro.Comprimento.Replace(",", ".") + ", PESOLIQUIDO=" + pro.PesoLiquido.Replace(",", ".") + ", PESOBRUTO=" + pro.PesoBruto.Replace(",", ".") + " WHERE IDPRODUTO=" + pro.IdProduto;
            BdExterno.Executar(sql, VarGlobal.Conexao);

        }

        public DataTable RetornarProdutoUA(string IdUa, string CodigoBarras)
        {
            string sql = "SELECT TOP 1 pe.IDProdutoEmbalagem, UA.IDUNIDADEDEARMAZENAGEM CODIGO, UA.IDUNIDADEDEARMAZENAGEM , UA.DIGITO,ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) CLIENTE, P.CODIGODEBARRAS, PC.CODIGO CODIGOPRODUTO, PC.DESCRICAO,PC.METODODEMOVIMENTACAO, PC.IDCLIENTE, ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) FANTASIAAPELIDO, SolicitarLote, SolicitarDataDeValidade, pc.idprodutocliente, UA.*, PE.* ";
            sql += " FROM UNIDADEDEARMAZENAGEM UA ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDProdutoCliente = UA.IdProdutoCliente   ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = ua.IDPRODUTOCLIENTE   ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " INNER JOIN CADASTRO C ON C.IDCADASTRO = PC.IDCLIENTE ";
            sql += " AND UA.IDUNIDADEDEARMAZENAGEM =" + IdUa + " ";
            sql += " AND P.CODIGODEBARRAS='" + CodigoBarras + "' ";
            sql += " AND UA.DIGITO IS NOT NULL ";
            sql += " ORDER BY UA.IDUNIDADEDEARMAZENAGEM DESC ";
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }


        public DataTable RetornarProduto(string CodigoBarras)
        {
            string sql = "SELECT TOP 1 pe.IDProdutoEmbalagem, UA.IDUNIDADEDEARMAZENAGEM CODIGO, UA.IDUNIDADEDEARMAZENAGEM , UA.DIGITO,ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) CLIENTE, P.CODIGODEBARRAS, PC.CODIGO CODIGOPRODUTO, PC.DESCRICAO,PC.METODODEMOVIMENTACAO, PC.IDCLIENTE, ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) FANTASIAAPELIDO, SolicitarLote, SolicitarDataDeValidade, pc.idprodutocliente, UA.*, PE.* ";
            sql += " FROM UNIDADEDEARMAZENAGEM UA ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDProdutoCliente = UA.IdProdutoCliente   ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = ua.IDPRODUTOCLIENTE   ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " INNER JOIN CADASTRO C ON C.IDCADASTRO = PC.IDCLIENTE ";
            //sql += " AND UA.IDUNIDADEDEARMAZENAGEM =" + IdUa + " ";
            sql += " AND P.CODIGODEBARRAS='" + CodigoBarras + "' ";
            sql += " AND UA.DIGITO IS NOT NULL ";
            sql += " ORDER BY UA.IDUNIDADEDEARMAZENAGEM DESC ";
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public DataTable RetornarProdutoByCBandCodigo(string Valor)
        {
            string sql = "";

            sql += " SELECT DISTINCT PC.IDPRODUTOCLIENTE, PC.CODIGO, P.CODIGODEBARRAS, LEFT((CAD.CNPJCPF + '-' + CAD.RAZAOSOCIALNOME), 35) EMPRESA ";
            sql += " FROM PRODUTOEMBALAGEM PE  ";
            sql += " INNER JOIN PRODUTO P ON PE.IDPRODUTO = P.IDPRODUTO ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN CLIENTE C ON C.IDCLIENTE = PC.IDCLIENTE ";
            sql += " INNER JOIN CADASTRO CAD ON CAD.IDCADASTRO = C.IDCLIENTE ";
            sql += " WHERE  ";
            sql += " PC.CODIGO = '" + Valor + "' OR P.CODIGODEBARRAS ='" + Valor + "' ";

            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public void AlterarEnderecoDePicking(string idProdutoCliente, string IdDepositoPlantaLocalizacao)
        {
            string sql = "";
            sql += " UPDATE PRODUTOCLIENTE SET IDDEPOSITOPLANTALOCALIZACAO=" + IdDepositoPlantaLocalizacao + " WHERE IDPRODUTOCLIENTE = " + idProdutoCliente;
            BdExterno.Executar(sql, VarGlobal.Conexao);
        }

        public DataTable RetornarProdutosDoEnderecoPicking(string IdDepositoPlantaLocalizacaoPicking)
        {
            string sql = "";
            sql += " SELECT DISTINCT PC.IDPRODUTOCLIENTE, PC.DESCRICAO, PC.METODODEMOVIMENTACAO,  DPL.DESCRICAO ENDERECO, PCR.IDDEPOSITOPLANTALOCALIZACAO, PC.CODIGO ";
            sql += " FROM PRODUTOCLIENTEREGRA PCR ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PCR.IDPRODUTOCLIENTE  ";
            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = PCR.IDDEPOSITOPLANTALOCALIZACAO ";
            sql += " INNER JOIN DEPOSITOPLANTA DP ON DP.IDDEPOSITOPLANTA  = DPL.IDDEPOSITOPLANTA ";
            sql += " INNER JOIN DEPOSITO D ON D.IDDEPOSITO = DP.IDDEPOSITO ";
            sql += " WHERE PCR.TIPODEREGRA='PICKING' ";
            sql += " AND PCR.IDDEPOSITOPLANTALOCALIZACAO =" + IdDepositoPlantaLocalizacaoPicking;
            sql += " AND D.IDFILIAL =" + VarGlobal.Usuario.UltimaFilial;
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public DataTable RetornarProdutosDoEndereco(string IdDepositoPlantaLocalizacao)
        {
            string sql = "";

            sql += " Select Distinct ";
            sql += " PC.IDPRODUTOCLIENTE, PC.DESCRICAO, PC.METODODEMOVIMENTACAO,  DPL.Descricao ENDERECO, DPL.IDDepositoPlantaLocalizacao ";
            sql += " FROM DepositoPlantaLocalizacao DPL  ";
            sql += " Inner Join UnidadeDeArmazenagem UA on (UA.IDDepositoPlantaLocalizacao = DPL.IDDepositoPlantaLocalizacao) ";
            sql += " Inner Join UnidadeDeArmazenagemLote UAL on (UAL.IDUnidadeDeArmazenagem = UA.IDUnidadeDeArmazenagem) ";
            sql += " Inner Join Lote LT on (LT.IDLote = UAL.IDLote) ";
            sql += " Inner Join ProdutoCliente PC on (PC.IDProdutoCliente = LT.IDProdutoCliente) ";
            sql += " Inner Join Estoque ES on (ES.IDProdutoCliente = PC.IDProdutoCliente and ES.IDEstoque = LT.IDEstoque)  ";
            sql += " inner join ProdutoEmbalagem pe on pe.IDProdutoCliente = pc.IDProdutoCliente ";
            sql += " inner join Produto p on p.IDProduto = pe.IDProduto ";
            sql += " where  ";
            sql += " UA.IDFilial =  " + VarGlobal.Usuario.UltimaFilial;
            sql += " and PC.IDDepositoPlantaLocalizacao = DPL.IDDepositoPlantaLocalizacao ";
            sql += " and  DPL.IDDepositoPlantaLocalizacao = '" + IdDepositoPlantaLocalizacao + "'";
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public DataTable RetornarProdutosCodigoDeBarras(string CodigoDeBarras)
        {
            string sql = "";

            sql += " Select Distinct ";
            sql += " PC.IDPRODUTOCLIENTE, PC.DESCRICAO, PC.METODODEMOVIMENTACAO,  DPL.Descricao ENDERECO , DPL.IDDepositoPlantaLocalizacao";
            sql += " FROM DepositoPlantaLocalizacao DPL  ";
            sql += " Inner Join UnidadeDeArmazenagem UA on (UA.IDDepositoPlantaLocalizacao = DPL.IDDepositoPlantaLocalizacao) ";
            sql += " Inner Join UnidadeDeArmazenagemLote UAL on (UAL.IDUnidadeDeArmazenagem = UA.IDUnidadeDeArmazenagem) ";
            sql += " Inner Join Lote LT on (LT.IDLote = UAL.IDLote) ";
            sql += " Inner Join ProdutoCliente PC on (PC.IDProdutoCliente = LT.IDProdutoCliente) ";
            sql += " Inner Join Estoque ES on (ES.IDProdutoCliente = PC.IDProdutoCliente and ES.IDEstoque = LT.IDEstoque)  ";
            sql += " inner join ProdutoEmbalagem pe on pe.IDProdutoCliente = pc.IDProdutoCliente ";
            sql += " inner join Produto p on p.IDProduto = pe.IDProduto ";
            sql += " where  ";
            sql += " UA.IDFilial =  " + VarGlobal.Usuario.UltimaFilial;
            sql += " and PC.IDDepositoPlantaLocalizacao = DPL.IDDepositoPlantaLocalizacao ";
            sql += " and  P.CODIGODEBARRAS = '" + CodigoDeBarras + "'";
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }
    }

    public class DepositoPlantaLocalizacao
    {

        public DataTable RetornarEnderecosPicking(string IdprodutoCliente)
        {
            string sql = "";
            sql += " SELECT * FROM PRODUTOCLIENTEREGRA PR INNER JOIN  DEPOSITOPLANTALOCALIZACAO DPL  ON  DPL.IDDEPOSITOPLANTALOCALIZACAO = PR.IDDEPOSITOPLANTALOCALIZACAO WHERE PR.IDPRODUTOCLIENTE = "+IdprodutoCliente+"  AND TipoDeRegra='PICKING'";
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public void GravarPicking(string sql, bool insert)
        {
            sql = sql.ToUpper();
            if (insert)
            {
                int id = BdExterno.RetornarIDTabela("PRODUTOCLIENTEREGRA");
                sql = sql.Replace("@IDPRODUTOCLIENTEREGRA@", id.ToString());
            }
            BdExterno.Executar(sql, VarGlobal.Conexao);
        }

        public DataTable RetornarDepositoPlantaLocalizacao(string IdDepositoPlantaLocalizacao)
        {
            string sql = "";
            sql += " SELECT DPL.*, PC.IDPRODUTOCLIENTE FROM DEPOSITOPLANTALOCALIZACAO DPL ";
            sql += " INNER JOIN DEPOSITOPLANTA DP ON DP.IDDEPOSITOPLANTA = DPL.IDDEPOSITOPLANTA ";
            sql += " INNER JOIN DEPOSITO D ON D.IDDEPOSITO = DP.IDDEPOSITO ";
            sql += " LEFT JOIN PRODUTOCLIENTE PC ON PC.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO ";
            sql += " WHERE D.IDFILIAL =  " + VarGlobal.Usuario.UltimaFilial.ToString();
            sql += " AND DPL.IDDEPOSITOPLANTALOCALIZACAO =" + IdDepositoPlantaLocalizacao;
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public DataTable RetornarPorDPL_Prod(string valor)
        {
            string sql = "";
            sql += " SELECT EST.IDFILIAL,DPL.IDDEPOSITOPLANTALOCALIZACAO,PC.IDDEPOSITOPLANTALOCALIZACAO DEPOSITOPROD,PC.IDPRODUTOCLIENTE,";
            sql += " PC.CODIGO,PC.METODODEMOVIMENTACAO, PC.DATALIMITEDEUSO,DPL.DESCRICAO ENDERECO, P.CODIGODEBARRAS, PC.DESCRICAO, UA.*, ";
            sql += " PE.IDPRODUTOEMBALAGEM, LOTE.VALIDADE,  CADCLI.CNPJCPF +'-'+ CADCLI.RAZAOSOCIALNOME CLIENTE, PC.IDCLIENTE";
            sql += " FROM UNIDADEDEARMAZENAGEM UA   ";
            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO ";
            sql += " INNER  JOIN UNIDADEDEARMAZENAGEMLOTE UALOTE ON UALOTE.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM ";
            sql += " INNER JOIN LOTE ON LOTE.IDLOTE = UALOTE.IDLOTE ";
            sql += " INNER JOIN ESTOQUE EST ON EST.IDESTOQUE = LOTE.IDESTOQUE ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = EST.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = PC.IDCLIENTE ";
            sql += " WHERE ";
            sql += " UA.IDFILIAL = " + VarGlobal.Usuario.UltimaFilial;
            sql += " AND (CAST(DPL.IDDEPOSITOPLANTALOCALIZACAO AS VARCHAR(50)) ='" + valor + "' OR P.CodigoDeBarras ='" + valor + "' ) ";
            sql += " AND PC.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO ";
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public DataTable RetornarEndereco(string idProdutoCliente)
        {
            string sql = "";
            sql += " Select ";
            sql += " DPL.* , convert(varchar(10), LT.VALIDADE, 120) VALIDADE, convert(varchar(10), LT.DATADEENTRADA, 120) DATADEENTRADA  ,  LT.DATADEENTRADA, UA.IDUNIDADEDEARMAZENAGEM ";
            sql += " From UnidadeDeArmazenagemLote UAL ";
            sql += " Inner Join Lote LT on (LT.IDLote = UAL.IDLote) ";
            sql += " Inner Join ProdutoCliente PC on (PC.IDProdutoCliente = LT.IDProdutoCliente) ";
            sql += " Inner Join Estoque ES on (/*ES.IDProdutoCliente = PC.IDProdutoCliente and*/ ES.IDEstoque = LT.IDEstoque)  ";
            sql += " Inner Join UnidadeDeArmazenagem UA on (UA.IDUnidadeDeArmazenagem = UAL.IDUnidadeDeArmazenagem) ";
            sql += " Inner Join DepositoPlantaLocalizacao DPL on (DPL.IDDepositoPlantaLocalizacao = UA.IDDepositoPlantaLocalizacao) ";
            sql += " where UA.IDFilial = " + VarGlobal.Usuario.UltimaFilial + " and  PC.IDProdutoCliente = " + idProdutoCliente + " and UAL.Saldo > 0 ";
            sql += " and  DPL.IDDepositoPlantaLocalizacao not in (select IDDepositoPlantaLocalizacao from ProdutoClienteRegra where IDProdutoCliente="+idProdutoCliente+" and TipoDeRegra='PICKING') ";
            sql += " and UAL.Divisao = 'ARMAZENAGEM' ";
            sql += " and dpl.Picking='NAO' ";
            sql += " and ual.IDUnidadeDeArmazenagem not in (select IDUnidadeDeArmazenagem from MovimentacaoItem mi where mi.IDUnidadeDeArmazenagem = ual.IDUnidadeDeArmazenagem and mi.DataDeExecucao is null)";
            sql += " order by DPL.ORDEMARMAZENAGEM, DPL.CODIGO, CASE PC.MetodoDeMovimentacao WHEN 'FIFO' THEN LT.DataDeEntrada   ELSE LT.Validade END  ";
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }
    }

    public class UnidadeDeArmazenagem
    {

        public DataTable RetornarGuardarPallet(string idUa, string status)
        {
            string sql = "SELECT ";
            sql += " UA.IDUNIDADEDEARMAZENAGEM, "; 
            sql += " DPL.DESCRICAO ENDRECO_DESTINO, ";
            sql += " UA.IDPRODUTOCLIENTE, UA.IdProdutoEmbalagem, ";
            sql += " MI.QUANTIDADE, ";
            sql += " DPL.IDDEPOSITOPLANTALOCALIZACAO, ";
            sql += " UA.IDFILIAL,  ";
            sql += " UA.VALIDADE, UA.LOTE REFERENCIA,";
            sql += " 'ARMAZENAGEM' DIVISAO, (SELECT TOP 1 PCRI.IDDEPOSITOPLANTALOCALIZACAO FROM PRODUTOCLIENTEREGRA  PCRI WHERE PCRI.IDPRODUTOCLIENTE = UA.IDPRODUTOCLIENTE AND PCRI.TIPODEREGRA='PICKING') PICKING ";
            sql += " FROM MOVIMENTACAOITEM MI ";
            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = MI.IDDEPOSITOPLANTALOCALIZACAODESTINO ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM = MI.IDUNIDADEDEARMAZENAGEM ";
            sql += " WHERE (CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(20))+ UA.DIGITO) = " + idUa;

            if (status != "")
                sql += " AND UA.STATUS = '" + status + "'";

            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public DataTable RetornarSaida(string idUa, string IdDeposiotoPlantaloc)
        {
            string sql = "";

            sql += " SELECT PC.DESCRICAO, PC.CODIGO CODIGOPRODUTO, PC.IDPRODUTOCLIENTE, DPL.CODIGO ENDERECO, UAL.IDUNIDADEDEARMAZENAGEM, DPL.IDDEPOSITOPLANTALOCALIZACAO, UA.DIGITO, UAL.IDUNIDADEDEARMAZENAGEMLOTE, UAL.SALDO, PC.IDPRODUTOCLIENTE, PE.IDPRODUTOEMBALAGEM ";
            sql += " FROM DEPOSITOPLANTALOCALIZACAO DPL  ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = UA.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = UA.IDPRODUTOEMBALAGEM ";

            sql += " WHERE DPL.IDDEPOSITOPLANTALOCALIZACAO NOT IN (SELECT PCR.IDDEPOSITOPLANTALOCALIZACAO FROM PRODUTOCLIENTEREGRA PCR WHERE PCR.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO AND TIPODEREGRA = 'PICKING') ";
            sql += " AND UAL.SALDO>0 ";
            sql += " AND DPL.IDDEPOSITOPLANTALOCALIZACAO = " + IdDeposiotoPlantaloc;
            sql += " AND (CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(20))+ UA.DIGITO) = " + idUa;

            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);

        }


        public DataTable RetornarSaidaLivre(string idUa, string IdDeposiotoPlantaloc)
        {
            string sql = "";

            sql += " SELECT PC.DESCRICAO, PC.CODIGO CODIGOPRODUTO, PC.IDPRODUTOCLIENTE, DPL.CODIGO ENDERECO, UAL.IDUNIDADEDEARMAZENAGEM, DPL.IDDEPOSITOPLANTALOCALIZACAO, UA.DIGITO, UAL.IDUNIDADEDEARMAZENAGEMLOTE, UAL.SALDO, PC.IDPRODUTOCLIENTE, PE.IDPRODUTOEMBALAGEM ";
            sql += " FROM DEPOSITOPLANTALOCALIZACAO DPL  ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = UA.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = UA.IDPRODUTOEMBALAGEM ";

            sql += " WHERE /*DPL.IDDEPOSITOPLANTALOCALIZACAO NOT IN (SELECT PCR.IDDEPOSITOPLANTALOCALIZACAO FROM PRODUTOCLIENTEREGRA PCR WHERE PCR.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO AND TIPODEREGRA = 'PICKING') ";
            sql += " AND */ UAL.SALDO>0 ";
            sql += " AND DPL.IDDEPOSITOPLANTALOCALIZACAO = " + IdDeposiotoPlantaloc;
            sql += " AND (CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(20))+ UA.DIGITO) = " + idUa;

            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);

        }


        public DataTable RetornarSaida(string idUa, string codigoDeBarras, string IdDeposiotoPlantaloc)
        {
            string sql = "";

            sql += " SELECT ";            
            sql += " UAL.SALDO , DPL.DESCRICAO ENDERECO, P.CODIGODEBARRAS, PC.DESCRICAO, PC.IDPRODUTOCLIENTE,  UA.*, pe.IDPRODUTOEMBALAGEM";

            sql += " FROM PRODUTOCLIENTE PC ";
            sql += " INNER JOIN ESTOQUE ES ON ES.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " INNER JOIN LOTE LT ON LT.IDESTOQUE = ES.IDESTOQUE ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDLOTE = LT.IDLOTE ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM = UAL.IDUNIDADEDEARMAZENAGEM ";
            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON (DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO)  ";
            sql += " WHERE  ";
            sql += " P.CODIGODEBARRAS = '" + codigoDeBarras + "' ";
            sql += " AND UA.IDDEPOSITOPLANTALOCALIZACAO =  " + IdDeposiotoPlantaloc;
            sql += " AND (CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(20))+ isnull(UA.DIGITO,'')) =" + idUa;
            sql += " AND UAL.SALDO > 0 ";

           
         //  sql += "AND DPL.IDDEPOSITOPLANTALOCALIZACAO IN (SELECT IDDEPOSITOPLANTALOCALIZACAO FROM PRODUTOCLIENTEREGRA PR WHERE PR.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE)";

            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);

        }

        public DataTable LerUnidadeDeArmazenagem(string idUnidadedeArmazenagem)
        {
            string sql = "";
            sql += " SELECT top 1 DPL.DESCRICAO ENDERECO, P.CODIGODEBARRAS, PC.DESCRICAO, PC.IDPRODUTOCLIENTE,  UA.*, UALOTE.SALDO ";
            sql += " FROM UNIDADEDEARMAZENAGEM UA   ";
            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UALOTE ON UALOTE.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM ";
            sql += " INNER JOIN LOTE ON LOTE.IDLOTE = UALOTE.IDLOTE ";
            sql += " INNER JOIN ESTOQUE EST ON EST.IDESTOQUE = LOTE.IDESTOQUE ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = EST.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " WHERE UA.IDFILIAL = " + VarGlobal.Usuario.UltimaFilial;
            sql += " AND (CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(20))+ UA.DIGITO) =" + idUnidadedeArmazenagem;
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public DataTable Retornar(string idUnidadedeArmazenagem, string  status)
        {
            string sql = "";
            sql += " SELECT  UA.* ";
            sql += " FROM UNIDADEDEARMAZENAGEM UA   ";
            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO ";
            sql += " WHERE UA.IDFILIAL = " + VarGlobal.Usuario.UltimaFilial;
            sql += " AND (CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(20))+ UA.DIGITO) =" + idUnidadedeArmazenagem;

            if (status != "")
                sql += " AND STATUS = '"+status+"'";
            
            return BdExterno.RetornarDT(sql, VarGlobal.Conexao);
        }

        public void AlterarEnderecoUa(string idUa, string IdDepositoPlantaLocalizacao)
        {
            string sql = "";
            sql += " UPDATE UNIDADEDEARMAZENAGEM SET IDDEPOSITOPLANTALOCALIZACAO=" + IdDepositoPlantaLocalizacao + " WHERE IDUnidadeDeArmazenagem = " + idUa;
            BdExterno.Executar(sql, VarGlobal.Conexao);
        }

        public void AlterarEnderecoUa(int idproducliente, string IdDepositoPlantaLocalizacaoDestino, string IdUa)
        {
            try
            {            
                string sql = " update UnidadeDeArmazenagem set IDDepositoPlantaLocalizacao=" + IdDepositoPlantaLocalizacaoDestino + " where IDUnidadeDeArmazenagem =" + IdUa;
                BdExterno.Executar(sql, VarGlobal.Conexao);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    public class Movimentacao
    {
        public int RetonarEndereco(string EntradaSaida, string IdProdutoCliente, string IdUa )
        {
            int ret = 0;


            System.Data.SqlClient.SqlConnection cn = new SqlConnection(VarGlobal.Conexao);
            System.Data.SqlClient.SqlCommand cd = new SqlCommand();

            cn.ConnectionString = VarGlobal.Conexao;
            cd.CommandText = "PRC_LOCALIZA_ENDERECO";
            cd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter();
            p1.ParameterName = "@OPERACAO";
            p1.DbType = DbType.String;
            p1.Value = EntradaSaida;
            cd.Parameters.Add(p1);

            p1 = new System.Data.SqlClient.SqlParameter();
            p1.ParameterName = "@IDPRODUTOCLIENTE";
            p1.DbType = DbType.Int32;
            p1.Value = IdProdutoCliente;
            cd.Parameters.Add(p1);


            p1 = new System.Data.SqlClient.SqlParameter();
            p1.ParameterName = "@IDUA";
            p1.DbType = DbType.Int32;
            p1.Value = IdUa;
            cd.Parameters.Add(p1);
            
            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter(); ;
            p2.ParameterName = "@RETORNO";
            p2.DbType = DbType.Int32;
            p2.Value = "0";
            p2.Direction = ParameterDirection.Output;
            cd.Parameters.Add(p2);
                        
            cd.Connection = cn;
            cn.Open();
            try
            {
                cd.ExecuteNonQuery();
                ret = int.Parse(cd.Parameters["@RETORNO"].Value.ToString());
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("Endereco não encontrado. "+ee.Message, "Atenção");
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }

        public void GravarMovimentacao(
                                        string IdUa, 
                                        string IdDepositoPlantaLocalizacaoOrigem, 
                                        string IdProdutoEmbalagem, 
                                        string Quantidade, 
                                        string Fator,   
                                        string IdOperacaoColetor, 
                                        string EntradaSaida,
                                        string IdProdutoCliente, string Origem)
        {
            
            string endercoDestino = RetonarEndereco(EntradaSaida, IdProdutoCliente, IdUa).ToString();

            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
            Conn.Open();
            System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();
           
            try
            {
                System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();

                
                Comm.CommandType = CommandType.Text;
                Comm.Connection = Conn;
                Comm.Transaction = trans;

                string sql = "";

                #region Movimentação
                
                int idMov = BdExterno.RetornarIDTabela("Movimentacao");
                sql = "INSERT INTO MOVIMENTACAO (IDMovimentacao ,IDFilial, IDUsuario, Numero, DataDeCadastro, Modo, EstoqueProcessado, Tipo, Motivo, Observacao, Ativo, MapaGerado, PedidoNotaFiscal) ";
                sql += " VALUES (" + idMov + " ," + VarGlobal.Usuario.UltimaFilial + ", " + VarGlobal.Usuario.IDUsuario + ", (SELECT ISNULL(MAX(CAST(ISNULL(NUMERO, 0) AS INT)),0)+ 1 FROM MOVIMENTACAO), GETDATE(), 'ATIVO', 'NAO', 'ENTRADA' , 'ENTRADA', 'ENTRADA RECEBIMENTO DE PALLET', 'SIM', 'NAO', NULL)";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                
                #endregion

                #region MovimentaçãoItem

                int quan = int.Parse(float.Parse(Fator).ToString()) * int.Parse(Quantidade);

                if (quan == 0)
                    quan =int.Parse( Quantidade);

                int idMovItem = BdExterno.RetornarIDTabela("MovimentacaoItem");
                sql = "INSERT INTO MovimentacaoItem( IDMovimentacaoItem, IDMovimentacao, IDUnidadeDeArmazenagem, IDDepositoPlantaLocalizacaoOrigem, IDDepositoPlantaLocalizacaoDestino, IDProdutoEmbalagem, IDUsuario, Quantidade, QuantidadeUnidadeEstoque, IdOperacaoColetor, IdUnidadeDeArmazenagemLote) ";
                sql += " VALUES ( " + idMovItem.ToString() + ", " + idMov + ", " + IdUa + ", " + IdDepositoPlantaLocalizacaoOrigem + " , " + endercoDestino + ", " + IdProdutoEmbalagem + ", " + VarGlobal.Usuario.IDUsuario + ", " + quan + ", " + quan + ", " + IdOperacaoColetor + ", (SELECT IDUNIDADEDEARMAZENAGEMLOTE FROM UNIDADEDEARMAZENAGEMLOTE WHERE IDUNIDADEDEARMAZENAGEM = "+IdUa+")) ";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion
                                
                
                #region Atualiza o Status do Pallet
                sql = "UPDATE UNIDADEDEARMAZENAGEM SET STATUS='RECEBIDO', OrigemDaUA='" + Origem + "' , IdProdutoEmbalagem="+IdProdutoEmbalagem+", IdProdutoCliente="+IdProdutoCliente+", QUANTIDADE="+int.Parse(float.Parse(Quantidade.ToString()).ToString())+" WHERE IDUnidadeDeArmazenagem= " + IdUa;
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion
                trans.Commit();


            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();

            }
        }


        public void GravarMovimentacao(
                                       string IdUa,
                                       string IdDepositoPlantaLocalizacaoOrigem,
                                       string IdProdutoEmbalagem,
                                       string Quantidade,
                                       string Fator,
                                       string IdOperacaoColetor,
                                       string EntradaSaida,
                                       string IdProdutoCliente, string  GravarUA, string Origem)
        {





            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
            Conn.Open();
            System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();

            try
            {
                System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();
                
                Comm.CommandType = CommandType.Text;
                Comm.Connection = Conn;
                Comm.Transaction = trans;

                string sql = "";
                               
                //variavel ja vem com o Insert montado
                Comm.CommandText = GravarUA;
                Comm.ExecuteNonQuery();


                #region Movimentação

                int idMov = BdExterno.RetornarIDTabela("Movimentacao");
                sql = "INSERT INTO MOVIMENTACAO (IDMovimentacao ,IDFilial, IDUsuario, Numero, DataDeCadastro, Modo, EstoqueProcessado, Tipo, Motivo, Observacao, Ativo, MapaGerado, PedidoNotaFiscal) ";
                sql += " VALUES (" + idMov + " ," + VarGlobal.Usuario.UltimaFilial + ", " + VarGlobal.Usuario.IDUsuario + ", (SELECT ISNULL(MAX(CAST(ISNULL(NUMERO, 0) AS INT)),0)+ 1 FROM MOVIMENTACAO), GETDATE(), 'ATIVO', 'NAO', 'ENTRADA' , 'ENTRADA', 'ENTRADA RECEBIMENTO DE PALLET', 'SIM', 'NAO', NULL)";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();

                #endregion

                #region MovimentaçãoItem

                int quan = int.Parse(float.Parse(Fator).ToString()) * int.Parse(Quantidade);

                int idMovItem = BdExterno.RetornarIDTabela("MovimentacaoItem");
                sql = "INSERT INTO MovimentacaoItem( IDMovimentacaoItem, IDMovimentacao, IDUnidadeDeArmazenagem, IDDepositoPlantaLocalizacaoOrigem, IDDepositoPlantaLocalizacaoDestino, IDProdutoEmbalagem, IDUsuario, Quantidade, QuantidadeUnidadeEstoque, IdOperacaoColetor, IdUnidadeDeArmazenagemLote) ";
                sql += " VALUES ( " + idMovItem.ToString() + ", " + idMov + ", " + IdUa + ", " + IdDepositoPlantaLocalizacaoOrigem + " , " + "1" + ", " + IdProdutoEmbalagem + ", " + VarGlobal.Usuario.IDUsuario + ", " + quan + ", " + quan + ", " + IdOperacaoColetor + ", (SELECT IDUNIDADEDEARMAZENAGEMLOTE FROM UNIDADEDEARMAZENAGEMLOTE WHERE IDUNIDADEDEARMAZENAGEM = " + IdUa + ")) ";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion

                sql = "update MovimentacaoItem set IDDepositoPlantaLocalizacaoDestino=@destino where idMovimentacaoItem=" + idMovItem;
                trans.Commit();


                string endercoDestino = RetonarEndereco(EntradaSaida, IdProdutoCliente, IdUa).ToString();
                sql = sql.Replace("@destino", endercoDestino);
                Classes.BdExterno.Executar(sql , VarGlobal.Conexao);


            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();

            }
        }

       
    }
}
