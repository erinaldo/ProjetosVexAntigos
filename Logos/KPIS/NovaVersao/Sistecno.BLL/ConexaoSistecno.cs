using System;
using System.Data;
using Sistecno.DAL.Models;

namespace Sistecno.BLL
{
    public class ConexaoSistecno
    {
        public DataTable RetornarGrid(int? idConexao, string cnx)
        {

            return new DAL.ConexaoSistecno().RetornarGrid(idConexao, cnx);
        }
        public DataTable Pesquisar(Sistecno.DAL.Models.Conexao obj, string cnx)
        {
            return new DAL.ConexaoSistecno().Pesquisar(obj, cnx);
        }

        public void Gravar(Conexao oC, string cnx)
        {
            new DAL.ConexaoSistecno().Gravar(oC, cnx);
            
        }
    }
}

