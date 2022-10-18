using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL.Models;

namespace Sistecno.DAL
{
    public class Icms
    {
        public decimal SugerirAliquotaICMS(int idestadoOrigem, int idEstadoDestino, string cnx)
        {
            SistecnoContext context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();

            IQueryable<DAL.Models.Icm> icmsAliquota = from p in context.Icms
                                                            where p.IDEstadoOrigem == idestadoOrigem 
                                                                && p.IDEstadoDestino == idEstadoDestino
                                                            select p;

            context.Database.Connection.Close();
            decimal i = (decimal)icmsAliquota.ToList<DAL.Models.Icm>()[0].AliquotaContribuinte;

            return i;
        }
    }
}
