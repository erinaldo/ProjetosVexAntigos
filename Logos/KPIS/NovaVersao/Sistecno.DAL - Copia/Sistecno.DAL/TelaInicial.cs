using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL
{
   public class TelaInicial
    {
        public static DataTable Secoes(string par)
        {
            string sql = " SELECT * FROM CANAL C INNER JOIN CANALSECAO CS ON CS.IDCANAL = C.IDCANAL WHERE PARAMETRO='" + par + "'";
            return DAL.BD.cDb.RetornarDataTable(sql, new DAL.BD.ConexaoPrincipal("").CxPrincipal);
        }
    }
}
