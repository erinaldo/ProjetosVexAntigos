using System.Text;
using System.Data;
using System.Web;
using System;

namespace SistranDAO
{
    public sealed class Sobra
    {

        public DataTable Filtrar(int? idSobra, int? idfilial, bool exibirFinalizados, DateTime Ini, DateTime Fim)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT ");
            strsql.Append(" CADCLI.RAZAOSOCIALNOME CLIENTE, ");
            strsql.Append(" FL.NOME FILIAL, ");
            strsql.Append(" FLD.NOME FILIALDestino, ");
            strsql.Append(" SB.*, CADUSU.LOGIN   NomeUsuario  ");
            strsql.Append(" FROM SOBRA SB ");
            strsql.Append(" INNER JOIN FILIAL FL  ON FL.IDFILIAL = SB.IDFILIAL ");
            strsql.Append(" INNER JOIN FILIAL FLD  ON FLD.IDFILIAL = SB.IDFILIALDestino ");
            strsql.Append(" LEFT JOIN USUARIO CADUSU ON CADUSU.IDUSUARIO = SB.IDUSUARIOBAIZADO ");
            strsql.Append(" INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = SB.IDCLIENTE WHERE 0=0");
            strsql.Append((idSobra == null ? "" : " AND P.IDSOBRA=" + idSobra.ToString()));
            strsql.Append((idfilial == null ? "" : " AND FL.IDFILIAL=" + idfilial.ToString()));
            strsql.Append((exibirFinalizados == false ? " AND FINALIZADO='NAO'" : ""));
            strsql.Append(" AND DATA BETWEEN  CONVERT(DATETIME, '" + Ini + "', 103) AND CONVERT(DATETIME, '" + Fim.ToShortDateString() + " 23:59:59', 103)  ");
            
            strsql.Append(" ORDER BY FL.NOME ,SB.DATA ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), HttpContext.Current.Session["Conn"].ToString());
        }
    }
}