using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL
{
    public class UA
    {

        public DataTable RetornarUa(string IdUa, string CodigodeBarras, string IdFilial, string cnx)
        {
            string sql = "SELECT TOP 50 UA.IDUNIDADEDEARMAZENAGEM CODIGO, UA.DIGITO,ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) CLIENTE, P.CODIGODEBARRAS, PC.DESCRICAO,PC.METODODEMOVIMENTACAO";
            sql += " FROM UNIDADEDEARMAZENAGEM UA ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDProdutoCliente = UA.IdProdutoCliente   ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = ua.IDPRODUTOCLIENTE   ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " INNER JOIN CADASTRO C ON C.IDCADASTRO = PC.IDCLIENTE ";
            sql += " where UA.IDFILIAL = " + IdFilial;
            sql += " AND (P.CODIGODEBARRAS Like  '%" + CodigodeBarras + "%' )";
            sql += " AND UA.DIGITO IS NOT NULL ";
            sql += " AND (CAST(IDUNIDADEDEARMAZENAGEM AS VARCHAR(30)) LIKE '%" + IdUa + "%' ) ";
            sql += " ORDER BY UA.IDUNIDADEDEARMAZENAGEM DESC ";
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }

        public DataTable RetornarUaBYid(string IdUa, string CodigoBarras,  string cnx)
        {
            string sql = "SELECT TOP 50 UA.IDUNIDADEDEARMAZENAGEM CODIGO, UA.IDUNIDADEDEARMAZENAGEM , UA.DIGITO,ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) CLIENTE, P.CODIGODEBARRAS, PC.DESCRICAO,PC.METODODEMOVIMENTACAO, PC.IDCLIENTE, ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) FANTASIAAPELIDO, SolicitarLote, SolicitarDataDeValidade, pc.idprodutocliente, UA.* ";
            sql += " FROM UNIDADEDEARMAZENAGEM UA ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDProdutoCliente = UA.IdProdutoCliente   ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = ua.IDPRODUTOCLIENTE   ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " INNER JOIN CADASTRO C ON C.IDCADASTRO = PC.IDCLIENTE ";
            sql += " AND UA.IDUNIDADEDEARMAZENAGEM =" + IdUa + " ";
            sql += " AND P.CODIGODEBARRAS='"+ CodigoBarras+"' ";
            sql += " AND UA.DIGITO IS NOT NULL ";
            sql += " ORDER BY UA.IDUNIDADEDEARMAZENAGEM DESC ";
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }



        public DataTable Retornar(string Codigo_CB, string cnx)
        {
            string sql = "SELECT DISTINCT PC.DESCRICAO, PC.IDCLIENTE, LEFT(C.FANTASIAAPELIDO, 20) FANTASIAAPELIDO, C.FANTASIAAPELIDO,P.CODIGODEBARRAS,PC.IDPRODUTOCLIENTE, PC.CODIGO, PC.IDCLIENTE, PC.METODODEMOVIMENTACAO, PC.SOLICITARDATADEVALIDADE, PC.SOLICITARLOTE  FROM PRODUTOCLIENTE PC ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO   ";
            sql += " INNER JOIN CADASTRO C ON C.IDCADASTRO = PC.IDCLIENTE   ";
            sql += " WHERE p.CodigoDeBarras ='"+Codigo_CB+"' or pc.Codigo = '"+Codigo_CB+"' ";
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }

        public int Gravar(int idFilial, int IdProdutoCliente, int quantidade, DateTime? Validade, string Lote,  string cnx)
        {
            string ID = DAL.BD.cDb.RetornarIDTabela(cnx, "UnidadeDeArmazenagem").ToString();

            string digito = "";// new frwSistecno.Helpers().Mod10(ID);

            string data = "";

            if(Validade !=null)
                data = ((DateTime)Validade).Year + "-"+ ((DateTime)Validade).Month + "-" + ((DateTime)Validade).Day;

            string sql = "";
            sql += " INSERT INTO UnidadeDeArmazenagem (IDUnidadeDeArmazenagem, IDFilial, IDDepositoPlantaLocalizacao, Impressao, IdProdutoCliente, IdProdutoEmbalagem, Quantidade, Validade, Lote, Digito) ";
            sql += " VALUES (" + ID + ", " + idFilial + ", 230139, null, " + IdProdutoCliente + ", NULL, " + quantidade + ", " + (Validade == null ? "NULL" : "'" + data + "'") + ", " + (Lote == null ? "NULL" : "'" + Lote + "'") + ", '" + digito + "') ";

            DAL.BD.cDb.ExecutarSemRetorno(sql, cnx);
            return int.Parse(ID);


        }


        public DataTable RetornarEtiquetaUA(string IdFilial, string idUa, string CodBarras, string cnx)
        {
            string sql = " UPDATE UNIDADEDEARMAZENAGEM SET VERSAOIMPRESSAO= ISNULL(VersaoImpressao,0) + 1 WHERE IDUNIDADEDEARMAZENAGEM=" + idUa;
            sql += " ; SELECT TOP 1 ";
            sql += " UA.IDUNIDADEDEARMAZENAGEM, ";
            sql += " UA.DIGITO, ";
            sql += " UA.QUANTIDADE, ";
            sql += " UA.VALIDADE, ";
            sql += " PL.DESCRICAO AS LOCALIZACAO, ";
            sql += " UA.LOTE REFERENCIA, ";
            sql += " LT.IDLOTE, ";
            sql += " US.LOGIN, ";
            sql += " UAL.SALDO, ";
            sql += " UAL.SALDO/EMB.UNIDADEDOCLIENTE AS SALDOMAIORUNIDADE, ";
            sql += " DOC.NUMERO, ";
            sql += " LT.VALIDADE, ";
            sql += " LT.DATADEENTRADA, ";
            sql += " LT.VALORUNITARIO, ";
            sql += " LT.REFERENCIA, ";
            sql += " PRO.CODIGODEBARRAS, ";
            sql += " PROCLI.DESCRICAO, ";
            sql += " PROCLI.CODIGO, ";
            sql += " EMB.UNIDADEDOCLIENTE, ";
            sql += " EMB.UNIDADEDEMEDIDA,  isnull(UA.DataDeEmissao, getdate()) EMISSAOETIQUETA,";
            sql += " RO.IDROMANEIO, ";
            sql += " RO.EMISSAO AS EMISSAOROMANEIO, ";
            sql += " REM.RAZAOSOCIALNOME AS REMETENTE, ";
            sql += " DES.RAZAOSOCIALNOME AS DESTINATARIO, ";
            sql += " PROCLI.IDCliente, isNull(ua.VersaoImpressao, 0) VersaoImpressao ";
            sql += " FROM UNIDADEDEARMAZENAGEM UA ";
            sql += " LEFT JOIN DEPOSITOPLANTALOCALIZACAO PL ON (PL.IDDEPOSITOPLANTALOCALIZACAO=UA.IDDEPOSITOPLANTALOCALIZACAO) ";
            sql += " LEFT JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON (UAL.IDUNIDADEDEARMAZENAGEM=UA.IDUNIDADEDEARMAZENAGEM) ";
            sql += " LEFT JOIN LOTE LT ON (LT.IDLOTE=UAL.IDLOTE) ";
            sql += " LEFT JOIN PRODUTOCLIENTE PROCLI ON (PROCLI.IDPRODUTOCLIENTE=UA.IDPRODUTOCLIENTE) ";
            sql += " LEFT JOIN DOCUMENTO DOC ON (DOC.IDDOCUMENTO=LT.IDDOCUMENTO) ";
            sql += " LEFT JOIN CADASTRO REM ON (REM.IDCADASTRO=DOC.IDREMETENTE) ";
            sql += " LEFT JOIN CADASTRO DES ON (DES.IDCADASTRO=DOC.IDDESTINATARIO) ";
            sql += " LEFT JOIN PRODUTOEMBALAGEM EMB ON (EMB.IDPRODUTOCLIENTE=UA.IDPRODUTOCLIENTE) ";
            sql += " LEFT JOIN PRODUTO PRO ON (PRO.IDPRODUTO=EMB.IDPRODUTO) ";
            sql += " LEFT JOIN USUARIO US ON (US.IDUSUARIO=LT.IDUSUARIO)  ";
            sql += " LEFT JOIN (	SELECT RO.IDROMANEIO, RO.EMISSAO, ROD.IDDOCUMENTO ";
            sql += " FROM ROMANEIO RO ";
            sql += " INNER JOIN ROMANEIODOCUMENTO ROD ON (ROD.IDROMANEIO=RO.IDROMANEIO) ";
            sql += " WHERE TIPO='ENTRADA') RO ON (RO.IDDOCUMENTO=LT.IDDOCUMENTO) ";
            sql += " WHERE ";
            sql += " UA.IDUNIDADEDEARMAZENAGEM =  " + idUa;
            sql += " AND UA.IDFILIAL=" + IdFilial;
            if (CodBarras != "")
                sql += " AND PRO.CodigoDeBarras = '" + CodBarras + "' ";
            sql += " ORDER BY ";
            sql += " UA.IDUNIDADEDEARMAZENAGEM, LT.IDLOTE; ";

            return DAL.BD.cDb.RetornarDataTable(sql, cnx);

        }
    }
}
