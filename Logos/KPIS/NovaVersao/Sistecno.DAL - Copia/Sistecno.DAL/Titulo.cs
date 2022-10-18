using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL
{
    public class Titulo
    {

        public DataTable retornarPesquisa(string numero, string idFilial, string PagarReceber, string cnx)
        {
            string sql = " SELECT  DISTINCT  TOP 100 T.IDTITULO ID,T.NUMERO,CADFOR.CNPJCPF + ' - ' +  ISNULL(CADFOR.FANTASIAAPELIDO , CADFOR.RAZAOSOCIALNOME) FORNECEDOR,";
            sql += " T.DATADEEMISSAO,TD.DATADEVENCIMENTO,TD.VALOR ";
            sql += " FROM TITULO T WITH (NOLOCK) ";
            sql += " INNER JOIN TITULODUPLICATA TD  WITH (NOLOCK) ON T.IDTITULO = TD.IDTITULO ";
            sql += " INNER JOIN CADASTRO CADFOR WITH (NOLOCK) ON CADFOR.IDCADASTRO = T.IDFORNECEDOR ";
            sql += " WHERE T.PAGARRECEBER= '"+PagarReceber+"' ";
            sql += " AND T.ATIVO='SIM' ";
            sql += " AND T.IDFILIAL="+ idFilial;
            sql += " AND T.NUMERO LIKE '%"+numero+"%' ";
            sql += " ORDER BY 1 DESC ";

            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }

        public class Historico
        {
        }

        public class Duplicata
        {

            public class Historico
            { }
        }

    }
}
