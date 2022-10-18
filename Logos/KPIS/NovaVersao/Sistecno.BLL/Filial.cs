using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public class Filial
    {

        public DataSet RetornarTodosCampos(int idfilial, string cnx)
        {
            return new Sistecno.DAL.Filial().RetornarTodosCampos(idfilial, cnx);
        }

        public int Inserir(Sistecno.DAL.Models.Filial obj, string cnx)
        {
            return new Sistecno.DAL.Filial().Inserir(obj, cnx);
        }

        public void Alterar(Sistecno.DAL.Models.Filial ff, string cnx)
        {
            new Sistecno.DAL.Filial().Alterar(ff, cnx);
        }
    }
}