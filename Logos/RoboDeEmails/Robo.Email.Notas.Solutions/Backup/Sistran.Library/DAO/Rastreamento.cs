using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public sealed class Rastreamento
    {

        public static DataTable Rastrear(string NumeroNotas, string CNPJ, string Conn)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT ");
            strsql.Append(" DOC.IDDOCUMENTO, DOC.TIPODEDOCUMENTO, ");
            strsql.Append(" CADEST.RAZAOSOCIALNOME DESTINATARIO, ");
            strsql.Append(" CADREM.RAZAOSOCIALNOME REMETENTE, ");
            strsql.Append(" ISNULL(DOC.DOCUMENTODOCLIENTE1,'-') REMESSA, ");
            strsql.Append(" DOC.DATADEEMISSAO DATAREMESSA, ");
            strsql.Append(" ISNULL(DOCREL.IDDOCUMENTOFILHO, '') PEDIDO, ");
            strsql.Append(" DOC.NUMERO, ");
            strsql.Append(" ISNULL(( ");
            strsql.Append(" SELECT TOP 1 NOME FROM DOCUMENTOOCORRENCIA DO ");
            strsql.Append(" INNER JOIN OCORRENCIA O ON O.IDOCORRENCIA = DO.IDOCORRENCIA ");
            strsql.Append(" WHERE IDDOCUMENTO = DOC.IDDOCUMENTO ");
            strsql.Append(" ORDER BY DO.DATAOCORRENCIA DESC ");
            strsql.Append(" ),'') STATUS ");

            strsql.Append(" FROM CADASTRO CAD ");
            strsql.Append(" INNER JOIN DOCUMENTO DOC ON DOC.IDCLIENTE = CAD.IDCADASTRO  ");
            strsql.Append(" INNER JOIN CADASTRO CADEST ON CADEST.IDCADASTRO  = DOC.IDDESTINATARIO ");
            strsql.Append(" INNER JOIN CADASTRO CADREM ON CADREM.IDCADASTRO  = DOC.IDREMETENTE ");
            strsql.Append(" LEFT JOIN DOCUMENTORELACIONADO DOCREL ON DOCREL.IDDOCUMENTOFILHO= DOC.IDDOCUMENTO ");
            //strsql.Append(" WHERE CAD.CNPJCPF = '" + CNPJ + "' ");
            //strsql.Append(" AND (DOC.NUMERO  IN(" + NumeroNotas + ")  OR DOC.DOCUMENTODOCLIENTE1 IN (" + NumeroNotas + ")) ");
            //strsql.Append(" AND (DOC.TIPODEDOCUMENTO='NOTA FISCAL' OR DOC.TIPODEDOCUMENTO='PEDIDO' ) AND DOC.ATIVO='SIM' ");
            strsql.Append(" WHERE CAD.CNPJCPF = '" + CNPJ + "' ");
            strsql.Append(" AND (cast(DOC.NUMERO as nvarchar(50))  IN(" + NumeroNotas + ")  OR DOC.DOCUMENTODOCLIENTE1 IN (" + NumeroNotas + ")) ");
            strsql.Append(" AND (DOC.TIPODEDOCUMENTO='NOTA FISCAL' OR DOC.TIPODEDOCUMENTO='PEDIDO' ) AND DOC.ATIVO='SIM' ");

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static int Contar(string NumeroNotas, string CNPJ, string Conn)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT  COUNT(DOC.IDDOCUMENTO)");
            strsql.Append(" FROM CADASTRO CAD ");
            strsql.Append(" INNER JOIN DOCUMENTO DOC ON DOC.IDCLIENTE = CAD.IDCADASTRO  ");
            strsql.Append(" INNER JOIN CADASTRO CADEST ON CADEST.IDCADASTRO  = DOC.IDDESTINATARIO ");
            strsql.Append(" INNER JOIN CADASTRO CADREM ON CADREM.IDCADASTRO  = DOC.IDREMETENTE ");
            strsql.Append(" LEFT JOIN DOCUMENTORELACIONADO DOCREL ON DOCREL.IDDOCUMENTOFILHO= DOC.IDDOCUMENTO ");
            strsql.Append(" WHERE CAD.CNPJCPF = '" + CNPJ + "' ");
            strsql.Append(" AND (cast(DOC.NUMERO as nvarchar(50))  IN(" + NumeroNotas + ")  OR DOC.DOCUMENTODOCLIENTE1 IN (" + NumeroNotas + ")) ");
            strsql.Append(" AND (DOC.TIPODEDOCUMENTO='NOTA FISCAL' OR DOC.TIPODEDOCUMENTO='PEDIDO' ) AND DOC.ATIVO='SIM' ");

            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), Conn);
        }

        public static DataTable RetornarTracking(string IdDocumento, string Conn)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT  ");
            strsql.Append(" ISNULL(DO.IDDocumentoOcorrencia, '')IDDocumentoOcorrencia, ");
            strsql.Append(" DATAOCORRENCIA,  ");
            strsql.Append(" OCO.NOME, ");
            strsql.Append(" OCO.NOMEREDUZIDO,  ");
            strsql.Append(" FL.Nome FILIAL, ");
            strsql.Append(" DOC.*, DO.* ");
            strsql.Append(" FROM DOCUMENTO DOC ");
            strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DO ON DO.IDDocumento = DOC.IDDocumento ");
            strsql.Append(" LEFT JOIN OCORRENCIA OCO ON OCO.IDOCORRENCIA = DO.IDOCORRENCIA ");
            strsql.Append(" INNER JOIN Cadastro CADDEST ON CADDEST.IDCadastro = Doc.IDDestinatario ");
            strsql.Append(" LEFT JOIN FilialCidadeSetor FLS ON FLS.IdCidade = CADDEST.IDCidade ");
            strsql.Append(" LEFT JOIN Filial FL ON FL.IDFilial = FLS.IdFilial ");
            strsql.Append(" WHERE DOC.IDDOCUMENTO="+ IdDocumento);
            strsql.Append(" ORDER BY DO.DATAOCORRENCIA DESC ");

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }
    }
}