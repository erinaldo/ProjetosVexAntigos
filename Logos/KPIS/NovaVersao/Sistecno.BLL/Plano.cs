using System.Data;

namespace Sistecno.BLL
{
    public class Plano
    {
        public DAL.Plano Retornar(int idcliente, string cnx)
        {
            return new DAL.Plano().Retornar(idcliente, cnx);
        }

        public DAL.Plano RetornarById(int idPlano, string cnx)
        {
            return new DAL.Plano().RetornarById(idPlano, cnx);
        }
    }
}
