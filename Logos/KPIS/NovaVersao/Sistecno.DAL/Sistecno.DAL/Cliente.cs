using System;
using System.Data;
using System.Text;
using Sistecno.DAL.Models;

namespace Sistecno.DAL
{
    public class Cliente
    {
        public DataTable RetornarTodosClientes(string cnx)
        {
            string sql = "SELECT C.IDCLIENTE, LEFT(FANTASIAAPELIDO, 30) RAZAOSOCIALNOME FROM CLIENTE C";
            sql += " INNER JOIN CADASTRO ON CADASTRO.IDCADASTRO = C.IDCLIENTE ";
            sql += " INNER JOIN CONEXAO X ON X.IDCLIENTE = C.IDCLIENTE ";
            sql += " WHERE ATIVO ='SIM'";
            sql+= "  ORDER BY RAZAOSOCIALNOME";
           return  DAL.BD.cDb.RetornarDataTable(sql , cnx);
        }

        public int InserirStarter(string cnx)
        {
            DAL.Models.Cliente obj = new DAL.Models.Cliente();
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            try
            {
                context.Database.Connection.Open();
                obj.IDCliente = DAL.BD.cDb.RetornarIDTabela(cnx, "CLIENTE");
                obj.CodigoDoCliente = obj.IDCliente;
                obj.CodigoDoClienteFilial = obj.IDCliente;

                obj.SeguroProprio = "NAO";
                obj.Ativo = "SIM";
                obj.DataDeCadastro = DateTime.Now;
                obj.Bloqueado = "NAO";
                obj.EmiteNotaFiscalServicoDeTransporte = "SIM";
                context.Cliente.Add(obj);
                context.SaveChanges();
                return obj.IDCliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }
        }

        public int RetornarIdClientePorUsuario(int idUsuario, string cnx)
        {
            string sql = "SELECT C.IDCLIENTE FROM USUARIO U  INNER JOIN CONEXAO C ON C.IDCONEXAO = U.IDCONEXAO WHERE U.IDUSUARIO = "+idUsuario;
            return DAL.BD.cDb.ExecutarRetornoID(sql, cnx);

        }

        public int InserirDocumentoClienteParametro(DAL.Models.DocumentoEletronicoParametro obj , string cnx)
        {

            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            try
            {
                context.Database.Connection.Open();
                obj.IdParametroCte = 1;
                context.DocumentoEletronicoParametro.Add(obj);
                context.SaveChanges();
                return obj.IdParametroCte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }
        }


        public string RetornaDivisoesClientes(string idUsuario, string idCliente, string cnx)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT CliDiv.IDClienteDivisao     ");
            txt.Append(" FROM   UsuarioCliente UsuCli    ");
            txt.Append(" INNER JOIN UsuarioClienteDivisao UsuCliDiv on(UsuCliDiv.IDUsuarioCliente = UsuCli.IDUsuarioCliente)");
            txt.Append(" INNER JOIN ClienteDivisao CliDiv on(CliDiv.IDClienteDivisao = UsuCliDiv.IDClienteDivisao)");
            
            txt.Append(" WHERE CliDiv.IDCliente = " + idCliente);
            txt.Append(" AND UsuCli.IDUsuario =" + idUsuario);

            string m = "";
            DataTable t = DAL.BD.cDb.RetornarDataTable(txt.ToString(), cnx);

            foreach (DataRow item in t.Rows)
            {
                m += item["IDClienteDivisao"].ToString() + ",";
                m += RetornaDivisoesFilhos(item["IDClienteDivisao"].ToString(), cnx);
            }

            if (m.Length > 0)
                m = m.Substring(0, m.Length - 1);
            return m;
        }

        public static string RetornaDivisoesFilhos(string IDClienteDivisao, string cnx)
        {
            string mm = "";
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT  IDClienteDivisao, isnull(IdParente,0) IdParente FROM  ClienteDivisao WHERE  IDParente = " + IDClienteDivisao);

            DataTable t = DAL.BD.cDb.RetornarDataTable(txt.ToString(),  cnx);

            foreach (DataRow item in t.Rows)
            {
                mm += item["IDClienteDivisao"].ToString() + ",";
                if (item["IDClienteDivisao"].ToString() != "" && item["IDClienteDivisao"].ToString() != "0")
                    mm += RetornaDivisoesFilhos(item["IDClienteDivisao"].ToString(), cnx);
            }

            return mm;
        }

        public DataTable RetornarArvoreDivisoes(string divs, string cnx)
        {
            string strsql = " SELECT IDCLIENTEDIVISAO, ISNULL(IDPARENTE, 0) IDPARENTE, NOME FROM CLIENTEDIVISAO WHERE IDCLIENTEDIVISAO IN (" + divs + ") AND ATIVO='SIM'";
            return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
        }

        public  DataTable DivisoesCompleta(string idCliente, string cnx)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT * FROM CLIENTEDIVISAO WHERE IDCLIENTE='" + idCliente + "' AND ATIVO='SIM'");
            return DAL.BD.cDb.RetornarDataTable(strsql.ToString(), cnx);
        }


        public byte[] RetornarImagemUsuarioCliente(int idcliente, string cnx)
        {
            string strsql = "SELECT * FROM CADASTROIMAGEM WHERE IDCADASTRO = "+idcliente.ToString()+" AND (TIPOIMAGEM='LOGOTIPO' or nome like '%Logo%' )";
            DataTable r = DAL.BD.cDb.RetornarDataTable(strsql.ToString(), cnx);

            if (r.Rows.Count == 0)
                return null;

            if (r.Rows[0]["IMAGEM"].ToString() == "")
                return null;
            else
                return (byte[])r.Rows[0]["IMAGEM"];

        }
    }
}
