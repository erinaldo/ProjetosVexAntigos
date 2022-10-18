using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public class Cliente
    {

        public DataTable RetornarTodosClientes(string cnx)
        {
            return new DAL.Cliente().RetornarTodosClientes(cnx);

        }
        public int InserirStarter(string cnx)
        {
            return new DAL.Cliente().InserirStarter( cnx);
        }

        public int InserirDocumentoClienteParametro(DAL.Models.DocumentoEletronicoParametro obj, string cnx)
        {
            return new DAL.Cliente().InserirDocumentoClienteParametro(obj,cnx);
        }

        public int RetornarIdClientePorUsuario(int idUsuario, string cnx)
        {
            return new DAL.Cliente().RetornarIdClientePorUsuario(idUsuario, cnx);
        }

        public string RetornaDivisoesClientes(string idUsuario, string idCliente, string cnx)
        {
            return new DAL.Cliente().RetornaDivisoesClientes(idUsuario, idCliente, cnx);
        }
        public DataTable RetornarArvoreDivisoes(string divs, string cnx)
        {
            return new DAL.Cliente().RetornarArvoreDivisoes(divs, cnx);
        }

        public DataTable DivisoesCompleta(string idCliente, string cnx)
        {
            return new DAL.Cliente().DivisoesCompleta(idCliente, cnx);
        }

        public byte[] RetornarImagemUsuarioCliente(int idcliente, string cnx)
        {
            return new DAL.Cliente().RetornarImagemUsuarioCliente(idcliente, cnx);
        }
    }
}
