using System.Data;

namespace Sistecno.BLL
{
    public class Titulo
    {


        public DataTable retornarPesquisa(string numero, string idFilial, string PagarReceber, string cnx)
        {
            return new Sistecno.DAL.Titulo().retornarPesquisa(numero, idFilial, PagarReceber, cnx);
        }
        public class Duplicata
        {
        }

    }
}
