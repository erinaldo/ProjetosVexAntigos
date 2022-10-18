using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public class CFOP
    {
        public DAL.Models.Cfop Retornar(string codigo, string cnx)
        {
            return new DAL.CFOP().Retornar(codigo, cnx);
        }


        public List<DAL.Models.Cfop> RetornarCFOPTransporte(string cnx)
        {
            return new DAL.CFOP().RetornarCFOPTransporte(cnx);
        }

        /// <summary>
        /// Retorna a classse do Cfop Conforme o estado de origem e destino
        /// </summary>
        /// <param name="idEstadoOrigem"></param>
        /// <param name="idEstadoDestino"></param>
        /// <returns></returns>
        public string RetornarClasseCFOP(string UfOrigem, string UfDestino)
        {
            if (UfDestino == "EX")
                return "7";

            if (UfDestino == UfOrigem)
                return "6";
            else
                return "5";
        }
    }
}
