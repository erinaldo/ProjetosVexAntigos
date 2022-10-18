using System.Text;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public class Reports
    {
        public DataTable ListaSimplesUsuario()
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DISTINCT U.IDUSUARIO, U.NOME, U.LOGIN, U.SENHA, ");
            strsql.Append(" CD.NOME, ");
            strsql.Append(" CD.IDCLIENTEDIVISAO, PC.CODIGO, PC.DESCRICAO,   ");
            strsql.Append(" (SELECT NOME FROM USUARIO UU WHERE UU.IDUSUARIO = U.IDPERFIL) PERFIL ");
            strsql.Append(" FROM USUARIO U ");
            strsql.Append(" INNER JOIN USUARIOCLIENTE UC ON UC.IDUSUARIO = U.IDUSUARIO ");
            strsql.Append(" INNER JOIN USUARIOCLIENTEDIVISAO UCD ON UCD.IDUSUARIOCLIENTE = UC.IDUSUARIOCLIENTE ");
            strsql.Append(" INNER JOIN CLIENTEDIVISAO CD ON CD.IDCLIENTEDIVISAO = UCD.IDCLIENTEDIVISAO ");
            strsql.Append(" INNER JOIN PRODUTOCLIENTE PC ON PC.IDCLIENTE = UC.IDCLIENTE ");
            strsql.Append(" WHERE UC.IDCLIENTE=" + HttpContext.Current.Session["IDEmpresa"].ToString());
            strsql.Append(" AND U.TIPO='USUARIO' ");
            strsql.Append(" AND U.TIPODESISTEMA='WEB' ");
            strsql.Append(" ORDER BY U.NOME ");

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());

        }

    }
}
