using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public class ContaDeEmail
    {

       

        public Sistecno.DAL.Models.ContaDeEmail Retornar(string operacao, string cnx)
        {
            return new Sistecno.DAL.ContaDeEmail().Retornar(operacao, cnx);
        }

        public void Gravar(Sistecno.DAL.Models.ContaDeEmail objemail, string cnx)
        {
             new Sistecno.DAL.ContaDeEmail().Gravar(objemail, cnx);
        }

        public void Alterar(Sistecno.DAL.Models.ContaDeEmail objemail, string cnx)
        {
             new Sistecno.DAL.ContaDeEmail().Alterar(objemail, cnx);
        }
    }
}
