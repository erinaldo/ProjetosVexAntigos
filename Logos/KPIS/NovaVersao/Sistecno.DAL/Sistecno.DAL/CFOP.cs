using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistecno.DAL.Models;

namespace Sistecno.DAL
{
    public class CFOP
    {
        public DAL.Models.Cfop Retornar(string codigo, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var query = context.Cfop.FirstOrDefault(p => p.Codigo == codigo);
            context.Database.Connection.Close();
            return query;
        }

        public List<DAL.Models.Cfop> RetornarCFOPTransporte(string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();

            IQueryable<DAL.Models.Cfop> cfo = from p in context.Cfop where p.Tipo == "SAIDA" && p.Codigo.Substring(0,2)=="35" && p.Codigo != "350"
                                                   select p;
                       
            context.Database.Connection.Close();

            return cfo.ToList<DAL.Models.Cfop>();
        }
    }

   
}
