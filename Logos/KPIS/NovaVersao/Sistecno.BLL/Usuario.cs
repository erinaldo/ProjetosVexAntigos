using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public class Usuario
    {

        public DataTable RetornarUsuarioCliente(int idCLiente, string cnx)
        {
            return new Sistecno.DAL.Usuario().RetornarUsuarioCliente(idCLiente, cnx);

        }
        public Sistecno.DAL.Models.Usuario Logar(Sistecno.DAL.Models.Usuario usuario, string cnx)
        {
            try
            {
                return new Sistecno.DAL.Usuario().Logar(usuario, cnx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RetornarIdConexao(int idcliente, string  cnx)
        {
            return new Sistecno.DAL.Usuario().RetornarIdConexao( idcliente, cnx);
        }

        public Sistecno.DAL.Models.Usuario LogarSistecno(Sistecno.DAL.Models.Usuario usuario, string cnx)
        {
            try
            {
                return new Sistecno.DAL.Usuario().LogarSistecno(usuario, cnx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public void GravaInformacoesEmpresaLogin(Sistecno.DAL.Models.Usuario usuario, string cnx)
        {
           new Sistecno.DAL.Usuario().GravaInformacoesEmpresaLogin(usuario, cnx);
        }

        public int Inserir(Sistecno.DAL.Models.Usuario obj, string cnx)
        {
          return   new Sistecno.DAL.Usuario().Inserir(obj, cnx);
        }

        public DataTable Retornar(string cnx)
        {
            return new Sistecno.DAL.Usuario().Retornar(cnx);
        }

        public Sistecno.DAL.Models.Usuario retornaUltimaInfo(string idUsu, string cnx)
        {
            try
            {
                return new Sistecno.DAL.Usuario().retornaUltimaInfo(idUsu, cnx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RetornarInformacoesUsuarioCliente(int idUsuario, string cnx)
        {
            return new Sistecno.DAL.Usuario().RetornarInformacoesUsuarioCliente(idUsuario, cnx);
        }


        public bool ExisteDivisao(int idUsuario, int idCliente, string cnx)
        {
            return new Sistecno.DAL.Usuario().ExisteDivisao(idUsuario, idCliente, cnx);

        }
    }
}