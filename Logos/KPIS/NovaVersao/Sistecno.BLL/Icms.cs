using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public class Icms
    {
        public decimal SugerirAliquotaICMS(int idestadoOrigem, int idEstadoDestino, string cnx)
        {
            return new Sistecno.DAL.Icms().SugerirAliquotaICMS(idestadoOrigem, idEstadoDestino, cnx);
        }
    }
}
