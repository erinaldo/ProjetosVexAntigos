using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public class Pedido
    {
        #region NovoPedido
        public DataTable RetornarProdutos(int idUsuario, int IdCliente)
        {
            string ssql = "";
            ssql += " SELECT PC.IDGRUPODEPRODUTO, PC.IDPRODUTOCLIENTE, CD.IDCLIENTEDIVISAO, CD.IDPARENTE, GP.DESCRICAO GRUPO, PC.CODIGO,CodigoDoCliente, PC.DESCRICAO PRODUTO, ";
            ssql += " CD.NOME DIVISAO, ED.SALDO, ED.SALDO-DBO.SALDO_EMPENHADO(PC.IDGRUPODEPRODUTO, CD.IDCLIENTEDIVISAO) SALDODISPONIVEL, ED.IDESTOQUEDIVISAO, ";
            ssql += " (SELECT TOP 1 VALORUNITARIO FROM PRODUTOEMBALAGEM WHERE IDPRODUTOCLIENTE=PC.IDPRODUTOCLIENTE ORDER BY DATADECADASTRO DESC)  VLUNITARIO ";
            ssql += " FROM PRODUTOCLIENTE PC ";
            ssql += " LEFT JOIN GRUPODEPRODUTO GP ON GP.IDGRUPODEPRODUTO = PC.IDGRUPODEPRODUTO ";
            ssql += " INNER JOIN ESTOQUE  E ON E.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE	 ";
            ssql += " INNER JOIN ESTOQUEDIVISAO ED ON ED.IDESTOQUE = E.IDESTOQUE ";
            ssql += " INNER JOIN CLIENTEDIVISAO CD ON CD.IDCLIENTEDIVISAO = ED.IDCLIENTEDIVISAO ";
            ssql += " INNER JOIN USUARIOCLIENTEDIVISAO UCD ON UCD.IDCLIENTEDIVISAO = CD.IDCLIENTEDIVISAO  ";
            ssql += " INNER JOIN USUARIOCLIENTE UC ON UC.IDUSUARIOCLIENTE = UCD.IDUSUARIOCLIENTE ";
            ssql += " WHERE	PC.IDCLIENTE= "+ IdCliente;
            ssql += " AND ED.SALDO-DBO.SALDO_EMPENHADO(PC.IDGRUPODEPRODUTO, CD.IDCLIENTEDIVISAO) >0 ";
            ssql += " AND UC.IDUSUARIO= "+ idUsuario;
            ssql += " AND PC.ATIVO='SIM' ";
            ssql += " ORDER BY PC.DESCRICAO ";
            return Sistran.Library.GetDataTables.RetornarDataTable(ssql, "");

        }

        #endregion

        public void AlterarSituacao(string situacaoOrigem, string SituacaoDestino)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" UPDATE DOCUMENTOFILIAL SET SITUACAO='" + SituacaoDestino + "' WHERE SITUACAO='" + situacaoOrigem + "'");
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(),"");
        }
        
        public DataTable PedidoPendentes(string situacao)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>) HttpContext.Current.Session["USUARIO"];
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT ");
            strsql.Append(" CAST(D.DATADEEMISSAO AS DATE) DATA, ");
            strsql.Append(" COUNT(D.IDDocumento) QTD ");
            strsql.Append(" FROM DOCUMENTO D ");
            strsql.Append(" INNER JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = D.IDDOCUMENTO   ");
            strsql.Append(" WHERE SITUACAO='"+situacao+"'  ");
            strsql.Append(" AND D.TIPODEDOCUMENTO='PEDIDO' ");
            strsql.Append(" AND IDUSUARIO IN( SELECT IDUSUARIO FROM USUARIOCLIENTEDIVISAO   UCD INNER JOIN USUARIOCLIENTE UC ON UC.IDUSUARIOCLIENTE = UCD.IDUSUARIOCLIENTE WHERE UCD.IDCLIENTEDIVISAO IN(" + HttpContext.Current.Session["Divisoes"].ToString() + ") ) ");
            strsql.Append(" GROUP BY CAST(D.DATADEEMISSAO AS DATE) ");
            strsql.Append(" ORDER BY CAST(D.DATADEEMISSAO AS DATE) DESC");

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }
        
        public  DataTable ConsultarByDocumento(int DocId)
        {            
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DISTINCT ");
            strsql.Append(" DOC.TIPODEDOCUMENTO,");
            strsql.Append(" TIPODESERVICO,");
            strsql.Append(" ENTRADASAIDA,");
            strsql.Append(" DOC.NUMERO,");
            strsql.Append(" SERIE,");
            strsql.Append(" CLI.IDCLIENTE,");
            strsql.Append(" CADCLI.RAZAOSOCIALNOME RAZAOSOCIALCLIENTE, ");
            strsql.Append(" ESTCLI.UF AS UFCLIENTE,    ");
            strsql.Append(" CIDCLI.NOME AS CIDADECLIENTE, ");
            strsql.Append(" ESTCLI.UF AS UFCLIENTE,");
            strsql.Append(" ISNULL(CADCLI.ENDERECO, '') AS ENDERECOCLIENTE,    ");
            strsql.Append(" ISNULL(CADCLI.NUMERO,'') AS NUMEROCLIENTE,    ");
            strsql.Append(" ISNULL(CADCLI.COMPLEMENTO, '') AS COMPLEMENTOCLIENTE, ");
            strsql.Append(" CADREM.RAZAOSOCIALNOME AS RAZAONOMEREMETENTE, ");
            strsql.Append(" ESTREM.UF AS UFREMETENTE,    ");
            strsql.Append(" CIDDES.NOME AS CIDADEDESTINATARIO,    ");
            strsql.Append(" ESTDES.UF AS UFDESTINATARIO,");
            strsql.Append(" ISNULL(CADREM.ENDERECO, '') AS ENDERECOREMETENTE,    ");
            strsql.Append(" ISNULL(CADREM.NUMERO,'') AS NUMEROREMETENTE,    ");
            strsql.Append(" ISNULL(CADREM.COMPLEMENTO, '') AS COMPLEMENTOREMETENTE, ");
            strsql.Append(" CIDREM.NOME AS CIDADEREMETENTE,");
            strsql.Append(" CADDES.CNPJCPF AS CNPJCPFDESTINATARIO,  CADDES.InscricaoRG  IEDEST,   ");
            strsql.Append(" CADDES.RAZAOSOCIALNOME AS RAZAONOMEDESTINATARIO,    ");
            strsql.Append(" CADDES.FANTASIAAPELIDO AS FANTASIAAPELIDODESTINATARIO, ");
            strsql.Append(" CADREM.CNPJCPF AS CNPJCPFREMETENTE,  ");
            strsql.Append(" ISNULL(CADDES.ENDERECO, '') AS ENDERECODESTINATARIO,    ");
            strsql.Append(" ISNULL(CADDES.NUMERO,'') AS NUMERODESTINATARIO,    ");
            strsql.Append(" ISNULL(CADDES.COMPLEMENTO, '') AS COMPLEMENTODESTINATARIO,    ");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADOMOVIMENTO, '01/01/1900'), 103))AS DATADOMOVIMENTO, ");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADEENTRADA, '01/01/1900'), 103))AS DATADEENTRADA,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADECANCELAMENTO, '01/01/1900'), 103))AS DATADECANCELAMENTO,");

            strsql.Append(" ISNULL(DOC.ATIVO, '') AS ATIVO,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADEEMISSAO, '01/01/1900'), 103))AS DATADEEMISSAO,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATAPLANEJADA, '01/01/1900'), 103))AS DATAPLANEJADA,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADECONCLUSAO, '01/01/1900'), 103))AS DATADECONCLUSAO,");
            strsql.Append(" ISNULL(DOC.CODIGODORECEXP, 0) AS CODIGODORECEXP,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADECONCLUSAO, '01/01/1900'), 103))AS DATADECONCLUSAO,    ");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADORECEXP, '01/01/1900'), 103))AS DATADECONCLUSAORECEBIMENTO,");
            strsql.Append(" DOC.ENDERECO,");
            strsql.Append(" DOC.ENDERECONUMERO,");
            strsql.Append(" DOC.ENDERECOCOMPLEMENTO,");
            strsql.Append(" CID.NOME CIDADE,");
            strsql.Append(" EST.NOME ESTADO,");

            strsql.Append(" CIDDES.NOME CIDADEDEST,");
            strsql.Append(" ESTDES.NOME ESTADODEST,");
            strsql.Append(" CADDES.CEP CEPDEST,");

            strsql.Append(" DOC.IDDOCUMENTO,");
            strsql.Append(" DOC.ORIGEM,    ");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADESAIDA, '01/01/1900'), 103))AS DATADESAIDA,    ");
            strsql.Append(" DOCFIL.SITUACAO,    ");
            strsql.Append(" OCO.NOME AS OCORRENCIA,");
            strsql.Append(" DOCOCO.DATAOCORRENCIA,   ");
            strsql.Append(" OCO.Nome,  ");
            strsql.Append(" VALORDANOTA,");
            strsql.Append(" VALORDEDESCONTO,");
            strsql.Append(" BASEDOIPI,");
            strsql.Append(" BASEDOICMS,");
            strsql.Append(" VALORDOICMS,");
            strsql.Append(" PESOLIQUIDO,");
            strsql.Append(" PESOBRUTO,");
            strsql.Append(" VALORDEDESCONTO,");
            strsql.Append(" FIL.Nome AS NOMEFILIAL,");
            strsql.Append(" FIL.IDFilial,");
            strsql.Append(" DOCOCO.IDDocumentoOcorrencia");
            strsql.Append(" FROM DOCUMENTO DOC    ");
            strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO  ON(DOCOCO.IDDOCUMENTO = DOC.IDDOCUMENTO)  ");
            strsql.Append(" LEFT JOIN OCORRENCIA OCO  ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA)  ");
            strsql.Append(" LEFT JOIN DOCUMENTOFILIAL DOCFIL  ON(DOCFIL.IDDOCUMENTO = DOC.IDDOCUMENTO)  ");
            strsql.Append(" LEFT JOIN CADASTRO CADDES  ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)  ");
            strsql.Append(" LEFT JOIN CIDADE CIDDES  ON(CIDDES.IDCIDADE = CADDES.IDCIDADE)  ");
            strsql.Append(" LEFT JOIN ESTADO ESTDES  ON(ESTDES.IDESTADO = CIDDES.IDESTADO)  ");
            strsql.Append(" LEFT JOIN CADASTRO CADREM  ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ");
            strsql.Append(" LEFT JOIN CIDADE CIDREM  ON(CIDREM.IDCIDADE = CADREM.IDCIDADE)  ");
            strsql.Append(" LEFT JOIN ESTADO ESTREM  ON(ESTREM.IDESTADO = CIDREM.IDESTADO)  ");
            strsql.Append(" LEFT JOIN CLIENTE CLI ON DOC.IDCLIENTE = CLI.IDCLIENTE ");
            strsql.Append(" LEFT JOIN CADASTRO CADCLI  ON(CADCLI.IDCADASTRO = DOC.IDCLIENTE)  ");
            strsql.Append(" LEFT JOIN CIDADE CIDCLI  ON(CIDCLI.IDCIDADE = CLI.IDCLIENTE)  ");
            strsql.Append(" LEFT JOIN ESTADO ESTCLI  ON(ESTCLI.IDESTADO = CLI.IDCLIENTE)");
            strsql.Append(" LEFT JOIN CIDADE CID  ON(CID.IDCIDADE = DOC.IDENDERECOBAIRRO)  ");
            strsql.Append(" LEFT JOIN ESTADO EST  ON(EST.IDESTADO = CID.IDESTADO)");
            strsql.Append(" INNER JOIN Filial FIL ON FIL.IDFilial = DOC.IDFilial");
            strsql.Append(" WHERE DOC.TIPODEDOCUMENTO = 'PEDIDO'  ");
            strsql.Append(" AND DOC.IDDOCUMENTO = @IDDOCUMENTO");

            strsql.Replace("@IDDOCUMENTO", DocId.ToString());

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");

        }

        public void CancelarPedido(string idDocumento)
        {
            string strsql ="  UPDATE Documento SET DataDeCancelamento = getdate(),Ativo = 'NAO' WHERE IDDocumento = " + idDocumento;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(), "");

            strsql = " UPDATE DocumentoItem SET EstoqueProcessado = 'SIM' WHERE IDDocumento = " + idDocumento;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(), "");

        }

        public DataTable AprovarPedidoPrepararDadosEmail(string idDocumento)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT DISTINCT	DOC.NUMERO,  ");
            txt.Append(" DOC.IDUSUARIO,  ");
            txt.Append(" DOC.IDCLIENTE,  ");
            txt.Append(" CAD.RAZAOSOCIALNOME AS NOMEDOCLIENTE,  ");
            txt.Append(" CADCONEND.ENDERECO,  ");
            txt.Append(" MOD.NOME AS MODAL,  ");
            txt.Append(" DOCFIL.SITUACAO, USU.Nome   ");
            txt.Append(" FROM DOCUMENTO DOC  ");
            txt.Append(" LEFT JOIN DOCUMENTOFILIAL DOCFIL  ");
            txt.Append(" ON(DOCFIL.IDDOCUMENTO = DOC.IDDOCUMENTO)  ");
            txt.Append(" LEFT JOIN MODAL MOD  ");
            txt.Append(" ON(MOD.IDMODAL = DOC.IDMODAL)  ");
            txt.Append(" LEFT JOIN USUARIO USU  ");
            txt.Append(" ON(USU.IDUSUARIO = DOC.IDUSUARIO)  ");
            txt.Append(" LEFT JOIN CADASTRO CADCLI  ");
            txt.Append(" ON(CADCLI.IDCADASTRO = DOC.IDCLIENTE)  ");
            txt.Append(" LEFT JOIN CADASTRO CAD  ");
            txt.Append(" ON(CAD.IDCADASTRO = USU.IDCADASTRO)  ");
            txt.Append(" LEFT JOIN CADASTROCONTATOENDERECO CADCONEND  ");
            txt.Append(" ON(CADCONEND.IDCADASTRO = USU.IDCADASTRO)  ");
            txt.Append(" LEFT JOIN CADASTROTIPODECONTATO CADTIPDECON  ");
            txt.Append(" ON(CADTIPDECON.IDCADASTROTIPODECONTATO = CADCONEND.IDCADASTROTIPODECONTATO)      ");
            txt.Append(" WHERE CADTIPDECON.NOME = 'E-MAIL'  ");
            txt.Append(" AND DOC.IDDOCUMENTO=" + idDocumento);
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public void AlterarSituacaoPedido(string situacao, string id_documento)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append(" UPDATE DocumentoFilial SET Data=GetDate(),Situacao = '" + situacao + "' WHERE IDDocumento=" + id_documento);
            Sistran.Library.GetDataTables.ExecutarSemRetorno(txt.ToString(), "");
        }

        public void IncluirOcorrencia(string id_documento, string ocorrencia)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)System.Web.HttpContext.Current.Session["USUARIO"];
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIA");

            string ids = Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoOcorrencia");
            
            string sql_insert = "INSERT INTO ";
            sql_insert += " DocumentoOcorrencia ";
            sql_insert += "( ";
            sql_insert += "    IDDocumentoOcorrencia, ";
            sql_insert += "    IDDocumento, ";
            sql_insert += "    IDFilial, ";
            sql_insert += "    IDUsuario, ";
            sql_insert += "	DataOcorrencia, ";
            sql_insert += "    Descricao, ";
            sql_insert += "    sistema ";
            sql_insert += ")SELECT  ";
            sql_insert += ids + "	, ";
            sql_insert += id_documento + ", ";
            sql_insert += "    1, ";
            sql_insert += ILusuario[0].UsuarioId.ToString() + ", ";
            sql_insert += "	GetDate(), ";
            sql_insert += "    '" + ocorrencia + "', ";
            sql_insert += "    'SIM' ";
            sql_insert += "FROM DocumentoOcorrencia";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(sql_insert.ToString(), "");

        }
        
        public int InserirTabDocumentoPedido(string IDFilial, string IDFilialAtual, string IDCliente, string IDRemetente, string IDDestinatario, string Serie, string Numero, string PesoBruto, string MetragemCubica, string Volumes, string ValorDaNota)
        {
            DataTable dtDest = new SistranBLL.Destinatario().ConsultarDadosDestinatario(IDDestinatario);
            List<SistranMODEL.Usuario> user = (List<SistranMODEL.Usuario>) System.Web.HttpContext.Current.Session["USUARIO"];

            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTO");

            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" INSERT INTO DOCUMENTO (IDDocumento, ");
            s.Append(" IDFilial, ");
            s.Append(" IDFilialAtual, ");
            s.Append(" TipoDeDocumento, ");
            s.Append(" Serie, ");
            s.Append(" Numero, ");
            s.Append(" IDCliente, ");
            s.Append(" IDRemetente, ");
            s.Append(" IDDestinatario, ");
            s.Append(" PesoBruto, ");
            s.Append(" MetragemCubica, ");
            s.Append(" Volumes, ");
            s.Append(" AnoMes, ");
            s.Append(" ValorDaNota,");
            s.Append(" tipodeservico, ");
            s.Append(" numerooriginal, ");
            s.Append(" origem, ");
            s.Append(" entradaSaida, ");
            s.Append(" datadeemissao, ");
            s.Append(" datadeentrada, ");
            s.Append(" valordasmercadorias, ");
            s.Append(" endereco, ");
            s.Append(" endereconumero, ");
            s.Append(" enderecocomplemento, ");
            s.Append(" idenderecobairro, ");
            s.Append(" idenderecocidade, ");
            s.Append(" enderecoCep, IDUSUARIO )");

            s.Append(" VALUES ( ");
            s.Append( ID + ",");
            s.Append(IDFilial + " , ");
            s.Append(IDFilial + ", ");
            s.Append(" 'PEDIDO', ");
            s.Append("'" + Serie.ToString() + "' , ");
            s.Append(Numero.ToString() + " , ");
            s.Append(IDCliente + ", ");
            s.Append(IDRemetente + ", ");
            s.Append(IDDestinatario + ", ");
            s.Append(Convert.ToDecimal(PesoBruto).ToString() + ", ");
            s.Append(Convert.ToDecimal(MetragemCubica.ToString()) + ", ");
            s.Append(Convert.ToInt32(Volumes).ToString() + " , ");
            s.Append("'" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "',");
            s.Append(Convert.ToDouble(ValorDaNota).ToString().Replace(",", ".") + ",");

            s.Append(" 'NORMAL', ");
            s.Append(Numero.ToString() + " , ");
            s.Append("'INTERNET', ");
            s.Append(" 'SAIDA', ");
            s.Append(" GETDATE(), ");
            s.Append(" GETDATE(), ");
            s.Append(Convert.ToDouble(ValorDaNota).ToString().Replace(",", ".") + " , ");

            s.Append("'" + dtDest.Rows[0]["Endereco"].ToString() + "', ");
            s.Append("'" + dtDest.Rows[0]["Numero"].ToString() + "', ");
            s.Append("'" + dtDest.Rows[0]["Complemento"].ToString() + "', ");
            s.Append(" NULL , ");
            s.Append((dtDest.Rows[0]["IDCIDADE"].ToString() == "" ? "0" : dtDest.Rows[0]["IDCIDADE"].ToString()) + ", ");
            s.Append("'" + dtDest.Rows[0]["CEP"].ToString() + "', "+ user[0].UsuarioId.ToString() +" )  ");

            Sistran.Library.GetDataTables.ExecutarRetornoID(s.ToString(), "");
            return Convert.ToInt32(ID);
        }

        public int RetornarIdProdutoEmbalagem(int IdProduto, int IDProdutoCliente)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("SELECT IDPRODUTOEMBALAGEM FROM PRODUTOEMBALAGEM WHERE /*IDPRODUTO =" + IdProduto.ToString() + "  AND*/ IDPRODUTOCLIENTE=" + IDProdutoCliente.ToString());
            return Sistran.Library.GetDataTables.ExecutarRetornoID(s.ToString(), "");
        }

        public int[] RetornarIdProdutoEmbalagemByIdprodutoCliente(string Codigo)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("SELECT TOP 1  IDPRODUTOEMBALAGEM, PC.IDPRODUTOCLIENTE FROM PRODUTOEMBALAGEM PE INNER JOIN ProdutoCliente  PC ON PE.IDProdutoCliente = PC.IDProdutoCliente WHERE PC.Codigo ='" + Codigo + "'");

            DataTable DT = Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), "");

            int[] m = new int[2];

            if (DT.Rows.Count > 0)
            {
                m[0] = Convert.ToInt32(DT.Rows[0]["IDPRODUTOEMBALAGEM"]);
                m[1] = Convert.ToInt32(DT.Rows[0]["IDPRODUTOCLIENTE"]);

            }

            return m;
        }

        public int InserirTabDocumentoItemPedido(int IdDocumento, int IdDocumentoEmbalagem, int IdUsuario, int IdClienteDivisao, int Quantidade, decimal VlUnitario, int IdUnidadeDeArmazenagemLote, string refrrencia)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOITEM");
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" INSERT INTO  DOCUMENTOITEM (IDDOCUMENTOITEM, IDDOCUMENTO, IDPRODUTOEMBALAGEM,IDUSUARIO, IDCLIENTEDIVISAO, QUANTIDADE, VALORUNITARIO,  ");
            s.Append(" QUANTIDADEUNIDADEESTOQUE, SALDO, VALORTOTALDOITEM, IdUnidadeDeArmazenagemLote, REFERENCIA) VALUES(");
            s.Append(ID + " , ");
            s.Append(IdDocumento.ToString() + ",");
            s.Append(IdDocumentoEmbalagem.ToString() + ", ");
            s.Append(IdUsuario.ToString() + ", ");
            s.Append((IdClienteDivisao.ToString()=="0"?"NULL":IdClienteDivisao.ToString() ) + ",  ");
            s.Append(Quantidade.ToString() + ", ");
            s.Append(VlUnitario.ToString().ToString().Replace(",", ".") + ",  ");
            s.Append(Quantidade.ToString() + ", ");
            s.Append(Quantidade.ToString() + ", ");
            s.Append((Quantidade * VlUnitario).ToString().Replace(",", "."));
            s.Append(", " + IdUnidadeDeArmazenagemLote);
            s.Append(", '" + refrrencia + "')");
            Sistran.Library.GetDataTables.ExecutarRetornoID(s.ToString(), "");
            return Convert.ToInt32(ID);
        }

        public int InserirTabDocumentoFilial(int idDocumento, int IdFilial, int IdRegiao, string Situacao)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOFILIAL");

            s.Append(" INSERT INTO DOCUMENTOFILIAL");
            s.Append(" (IDDOCUMENTOFILIAL, ");
            s.Append(" IDDOCUMENTO, ");
            s.Append(" IDFILIAL,");
            s.Append(" IDREGIAOITEM,");
            s.Append(" SITUACAO,");
            s.Append(" DATA)");
            s.Append(" VALUES(");
            s.Append(ID + " , ");
            s.Append(idDocumento.ToString() + ", ");
            s.Append(IdFilial.ToString() + ",");
            s.Append(IdRegiao.ToString() + ",");
            s.Append("'" + Situacao + "',");
            s.Append(" GETDATE())");
            Sistran.Library.GetDataTables.ExecutarSemRetorno(s.ToString(), "");
            return Convert.ToInt32(ID);
        }

        public int Numerador(int IdFilial, string Nome)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("SELECT PROXIMONUMERO FROM NUMERADOR WHERE IDFILIAL=" + IdFilial.ToString() + " AND NOME='" + Nome + "'");
            int ret = Sistran.Library.GetDataTables.ExecutarRetornoID(s.ToString(), "");

            if (ret == 0)
            {
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("numerador");

                string ssql = "INSERT INTO NUMERADOR(IDNUMERADOR, IDFILIAL, NOME, SERIE, PROXIMONUMERO) VALUES ("+ID+", " + IdFilial.ToString() + ", '" + Nome.ToString() + "', 'Ped', (SELECT ISNULL(MAX(PROXIMONUMERO),0)+2 FROM NUMERADOR))";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(ssql, "");
                ret = 1;
            }
            else
            {
                string ssql = "UPDATE NUMERADOR SET PROXIMONUMERO = PROXIMONUMERO+1  WHERE IDFILIAL=" + IdFilial.ToString() + " AND Nome='" + Nome + "'";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(ssql.ToString(), "");
            }

            return ret;

        }

        public void CancelarPedidoByErro(string idDocumento)
        {
            string ssql = "Delete from Documento where IDDocumento=" + idDocumento.ToString();
            Sistran.Library.GetDataTables.ExecutarSemRetorno(ssql, "");

        }

        public DataTable ExportarPedido(string situacao)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT * FROM DOCUMENTOFILIAL DF ");
            txt.Append(" INNER JOIN DOCUMENTO DC ON DF.IDDOCUMENTO = DC.IDDOCUMENTO ");
            txt.Append(" INNER JOIN DOCUMENTOITEM DI ON DC.IDDOCUMENTO = DI.IDDOCUMENTO ");
            txt.Append(" INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM");
            txt.Append(" INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE");
            txt.Append(" WHERE SITUACAO = '" + situacao + "' ");
            txt.Append(" ORDER BY DC.IDDOCUMENTO ");
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public DataTable ItensByDocumento(string idDocumento)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append("  SELECT PC.IDPRODUTOCLIENTE, PC.DESCRICAO , CD.NOME, DI.QUANTIDADE, DI.VALORUNITARIO ");
            txt.Append("  FROM DOCUMENTOITEM  DI ");
            txt.Append("  INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ");
            txt.Append("  INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ");
            txt.Append("  INNER JOIN CLIENTEDIVISAO CD ON CD.IDCLIENTEDIVISAO = DI. IDCLIENTEDIVISAO ");
            txt.Append("  WHERE IDDOCUMENTO="+ idDocumento);
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }
    }
}