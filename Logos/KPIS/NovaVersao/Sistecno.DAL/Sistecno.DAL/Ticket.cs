using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL
{
    public class Ticket
    {
        /*
         * 1) ABERTURA DO CHAMADO = ABERTO
         * 2) ATRIBUIU O CHAMADO = CHAMADO ATRIBUIDO
         * 3) HOUVE INTERAÇÃO = CHAMADO EM ANDAMENTO
         * 4) FECHOU = FINALIZADO
         * */

        public DataTable Retornar(int IdUsuario, string cnx)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append("SELECT T.IDTICKET, TD.DIVISAO, T.ABERTURA, T.ASSUNTO, T.STATUS FROM TICKET T INNER JOIN TICKETDIVISAO TD ON TD.IDTICKETDIVISAO = T.IDTICKETDIVISAO WHERE STATUS <> 'FECHADO'  AND IDUSUARIO = " + IdUsuario + " ORDER BY ABERTURA DESC");
            return DAL.BD.cDb.RetornarDataTable(txt.ToString(), cnx);
        }

        public DataTable RetornarChamadosNaoAtribuidos(int IdClienteAtribuido, string cnx)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append("SELECT T.IDTICKET, TD.DIVISAO, T.ABERTURA, T.ASSUNTO, T.STATUS FROM TICKET T INNER JOIN TICKETDIVISAO TD ON TD.IDTICKETDIVISAO = T.IDTICKETDIVISAO WHERE IDUSUARIOATRIBUIDO IS NULL   AND IDCLIENTEATRIBUIDO=" + IdClienteAtribuido + " ORDER BY ABERTURA DESC");
            return DAL.BD.cDb.RetornarDataTable(txt.ToString(), cnx);
        }

        public int RetornarQtdChmados(int IdUsuario, string cnx)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT COUNT(*) FROM TICKET T INNER JOIN TICKETDIVISAO TD ON TD.IDTICKETDIVISAO = T.IDTICKETDIVISAO WHERE STATUS <> 'FINALIZADO'  AND (IDUSUARIO = " + IdUsuario + " or IdUsuarioAtribuido=" + IdUsuario + ")");
            return DAL.BD.cDb.ExecutarRetornoID(txt.ToString(), cnx);
        }

        public DataTable RetornarChamadoCompleto(int idEmpresa, int idTicket, int IdUsuario, bool atribuido, string cnx)
        {
            StringBuilder txt = new StringBuilder();

            txt.Append(" SELECT T.*, TM.*, U.NOME USUARIO, UATRIB.NOME USUARIOATRIBUIDO, TMA.*, TD.*, CLI.RAZAOSOCIALNOME EMPRESA, T.IDUSUARIO SOLICITANTE,  TM.IDUSUARIO PARAQUEM, (SELECT NOME FROM USUARIO US WHERE US.IDUSUARIO = T.IDUSUARIOSOLICITANTE) NOMESOLICITANTE ");
            txt.Append(" FROM TICKET T  ");
            txt.Append(" INNER JOIN TICKETMOVIMENTO TM ON TM.IDTICKET = T.IDTICKET ");
            txt.Append(" LEFT JOIN TICKETMOVIMENTOANEXO TMA ON TMA.IDTICKETMOVIMENTO = TM.IDTICKETMOVIMENTO ");
            txt.Append(" LEFT JOIN TICKETDIVISAO TD ON TD.IDTICKETDIVISAO = T.IDTICKETDIVISAO ");
            txt.Append(" LEFT JOIN USUARIO U ON U.IDUSUARIO = TM.IDUSUARIO ");
            txt.Append(" LEFT JOIN USUARIO UATRIB ON UATRIB.IDUSUARIO = T.IDUSUARIOATRIBUIDO ");
            txt.Append(" LEFT JOIN CLIENTE C ON C.IDCLIENTE = T.IDCLIENTEATRIBUIDO ");
            txt.Append(" LEFT JOIN CADASTRO CLI ON CLI.IDCADASTRO = C.IDCLIENTE");

            if (idTicket > 0)
            {
                txt.Append(" WHERE T.IDTICKET =" + idTicket);
                //txt.Append(" AND ISNULL(TM.DESCRICAO, '') <> ''");
            }
            else
            {
                if (atribuido)
                {
                    txt.Append(" WHERE T.IdUsuarioAtribuido =" + IdUsuario);
                    txt.Append(" AND T.IDCLIENTEATRIBUIDO=" + idEmpresa);
                    // txt.Append(" AND ISNULL(TM.DESCRICAO, '') <> ''");

                }
                else
                {
                    txt.Append(" WHERE T.IdUsuario =" + IdUsuario);
                    //  txt.Append(" AND ISNULL(TM.DESCRICAO, '') <> ''");
                }
            }

            txt.Append(" ORDER BY TM.DATA DESC ");
            return DAL.BD.cDb.RetornarDataTable(txt.ToString(), cnx);

        }



        public DataTable RetornarPesquisa(int idEmpresa, string idTicket, string usuario, DateTime? ini, DateTime? fim, string status, string usuarioLogodo, string cnx)
        {
            StringBuilder txt = new StringBuilder();

            txt.Append(" SELECT T.*, TM.*, U.NOME USUARIO, UATRIB.NOME USUARIOATRIBUIDO, TMA.*, TD.*, CLI.RAZAOSOCIALNOME EMPRESA, T.IDUSUARIO SOLICITANTE,  TM.IDUSUARIO PARAQUEM , (SELECT NOME FROM USUARIO US WHERE US.IDUSUARIO = T.IDUSUARIOSOLICITANTE) NOMESOLICITANTE ");
            txt.Append(" FROM TICKET T  ");
            txt.Append(" INNER JOIN TICKETMOVIMENTO TM ON TM.IDTICKET = T.IDTICKET ");
            txt.Append(" LEFT JOIN TICKETMOVIMENTOANEXO TMA ON TMA.IDTICKETMOVIMENTO = TM.IDTICKETMOVIMENTO ");
            txt.Append(" LEFT JOIN TICKETDIVISAO TD ON TD.IDTICKETDIVISAO = T.IDTICKETDIVISAO ");
            txt.Append(" LEFT JOIN USUARIO U ON U.IDUSUARIO = TM.IDUSUARIO ");
            txt.Append(" LEFT JOIN USUARIO UATRIB ON UATRIB.IDUSUARIO = T.IDUSUARIOATRIBUIDO ");
            txt.Append(" LEFT JOIN CLIENTE C ON C.IDCLIENTE = T.IDCLIENTEATRIBUIDO ");
            txt.Append(" LEFT JOIN CADASTRO CLI ON CLI.IDCADASTRO = C.IDCLIENTE");
            txt.Append(" WHERE (IDCLIENTEATRIBUIDO=" + idEmpresa + " or UATRIB.IDUSUARIO=" + usuarioLogodo + " or U.IDUSUARIO=" + usuarioLogodo + ")");

            if (idTicket.Length > 0)
                txt.Append(" AND T.IDTICKET =" + idTicket);

            if (usuario.Length > 0)
                txt.Append(" AND (U.NOME LIKE '%" + usuario + "%' OR UATRIB.NOME LIKE '%" + usuario + "%') ");

            if (ini != null && fim != null)
                txt.Append(" AND CONVERT(DATE, T.ABERTURA) BETWEEn '" + DateTime.Parse(ini.ToString()).ToString("yyyy-MM-dd") + "' AND '" + DateTime.Parse(fim.ToString()).ToString("yyyy-MM-dd") + "'");

            if (status.Length > 0)
            {
                txt.Append(" AND T.STATUS='" + status + "'");
            }

            txt.Append(" ORDER BY TM.DATA DESC ");
            return DAL.BD.cDb.RetornarDataTable(txt.ToString(), cnx);

        }

        public DataTable RetornarChamadoCompletoNaoAtribuido(int idEmpresa, string cnx)
        {
            StringBuilder txt = new StringBuilder();

            txt.Append(" SELECT T.*, TM.*, U.NOME USUARIO, UATRIB.NOME USUARIOATRIBUIDO, TMA.*, TD.*, CLI.RAZAOSOCIALNOME EMPRESA, T.IDUSUARIO SOLICITANTE,  TM.IDUSUARIO PARAQUEM, ");
            txt.Append(" (SELECT NOME FROM USUARIO US WHERE US.IDUSUARIO = T.IDUSUARIOSOLICITANTE) NOMESOLICITANTE  ");
            txt.Append(" FROM TICKET T  ");
            txt.Append(" INNER JOIN TICKETMOVIMENTO TM ON TM.IDTICKET = T.IDTICKET ");
            txt.Append(" LEFT JOIN TICKETMOVIMENTOANEXO TMA ON TMA.IDTICKETMOVIMENTO = TM.IDTICKETMOVIMENTO ");
            txt.Append(" LEFT JOIN TICKETDIVISAO TD ON TD.IDTICKETDIVISAO = T.IDTICKETDIVISAO ");
            txt.Append(" LEFT JOIN USUARIO U ON U.IDUSUARIO = TM.IDUSUARIO ");
            txt.Append(" LEFT JOIN USUARIO UATRIB ON UATRIB.IDUSUARIO = T.IDUSUARIOATRIBUIDO ");
            txt.Append(" LEFT JOIN CLIENTE C ON C.IDCLIENTE = T.IDCLIENTEATRIBUIDO ");
            txt.Append(" LEFT JOIN CADASTRO CLI ON CLI.IDCADASTRO = C.IDCLIENTE");
            txt.Append(" WHERE T.IdUsuarioAtribuido is null");
            txt.Append(" AND T.IDCLIENTEATRIBUIDO=" + idEmpresa);
            txt.Append(" ORDER BY TM.DATA DESC ");
            return DAL.BD.cDb.RetornarDataTable(txt.ToString(), cnx);

        }


        public void AtribuirChamado(int idTicket, int atribuirA, string NomeAtribuidoA, int Idusuario, string cnx)
        {
            string sql = "UPDATE TICKET SET IDUSUARIOATRIBUIDO=" + atribuirA + ", STATUS='CHAMADO ATRIBUIDO' WHERE IDTICKET=" + idTicket;
            sql += "; INSERT INTO TICKETMOVIMENTO (IDTICKET,IDUSUARIO,DATA, DESCRICAO) VALUES ";
            sql += "(" + idTicket + "," + Idusuario + ",GetDate(), 'Chamado Atribuído para: " + NomeAtribuidoA + "')";
            DAL.BD.cDb.ExecutarSemRetorno(sql, cnx);
        }

      
        public string AbrirChamado(
                                    int idClienteAtribuido,
                                    int? idUsuarioAtribuido,
                                    int idusuarioDono,
                                    int idUsuarioSolicitante,
                                    Sistecno.DAL.Ticket.UserTicket objUsuarioSolicitante,
                                    string assunto,
                                    string status, string texto, int IdTicketDivisao,
                                    List<byte[]> Arquivos,
                                    List<string> NomeArquivos, 
                                    List<string > idUsuarioAcompanhamento,
                                    string cnx)
        {
            SqlConnection conn = new SqlConnection(cnx);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            string idMOV = "";

            try
            {
                string sql = "";
                if (objUsuarioSolicitante != null)
                {
                    string IdCad = "";
                    string Id_UsuarioSolicitante = "";
                    if (objUsuarioSolicitante.IdUsuario == -9999)
                    {
                        IdCad = DAL.BD.cDb.RetornarIDTabela(cnx, "CADASTRO").ToString();
                        Id_UsuarioSolicitante = DAL.BD.cDb.RetornarIDTabela(cnx, "USUARIO").ToString();
                        sql = "INSERT INTO CADASTRO (IDCADASTRO, RAZAOSOCIALNOME, FANTASIAAPELIDO, CNPJCPF) VALUES (" + IdCad + ", '" + objUsuarioSolicitante.Nome + "', '" + objUsuarioSolicitante.Nome + "', '" + IdCad + "')";

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.Transaction = trans;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        sql = "INSERT INTO USUARIO (IDUSUARIO, NOME, LOGIN, SENHA, IDCADASTRO, IDCONEXAO) VALUES (" + Id_UsuarioSolicitante + ", '" + objUsuarioSolicitante.Nome + "', '" + objUsuarioSolicitante.Login + "', '" + objUsuarioSolicitante.Senha + "', " + IdCad + ", " + objUsuarioSolicitante.IdConexao + ")";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.Transaction = trans;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        objUsuarioSolicitante.IdUsuario = int.Parse(Id_UsuarioSolicitante);
                    }

                }

                sql = "INSERT INTO  TICKET (IDUSUARIO,  IDCLIENTEATRIBUIDO, IDUSUARIOSOLICITANTE,  IDTICKETDIVISAO, ABERTURA, ASSUNTO, IDUSUARIOATRIBUIDO) VALUES ";
                sql += "(" + idusuarioDono + ",  " + idClienteAtribuido + ", " + objUsuarioSolicitante.IdUsuario + " ," + IdTicketDivisao + ", getDate(), '" + assunto + "', " + (idUsuarioAtribuido == null ? "NULL" : idUsuarioAtribuido.ToString()) + ") ; select SCOPE_IDENTITY()";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sql;
                string idT = cmd.ExecuteScalar().ToString();


                sql = "INSERT INTO TICKETMOVIMENTO (IDTICKET,IDUSUARIO,DATA, DESCRICAO) VALUES ";
                sql += "(" + idT + "," + idusuarioDono + ",GetDate(), '" + texto + "'); select SCOPE_IDENTITY()";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sql;
                idMOV = cmd.ExecuteScalar().ToString();

                for (int i = 0; i < Arquivos.Count; i++)
                {
                    string par = "@arquivo" + i.ToString();

                    sql = "INSERT INTO TICKETMOVIMENTOANEXO (IdTicketMovimento, Arquivo, NomeArquivo) VALUES ";
                    sql += "(" + idMOV + ", " + par + " , '" + NomeArquivos[i] + "')";

                    cmd.Parameters.Add(new SqlParameter(par, Arquivos[i]));
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }



                for (int i = 0; i < idUsuarioAcompanhamento.Count; i++)
                {
                    sql = "INSERT INTO TICKETUSUARIO VALUES (" + idT + ", " + idUsuarioAcompanhamento[i] + ")";                    
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
                return idT;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void AlterarChamado(int idTicket, int IdClienteAtribuido, int Idusuario, string Assunto, string status, string texto, int IdTicketDivisao, List<byte[]> Arquivos, List<string> NomeArquivos, bool AtribuirChamado, int? idUsuarioAAtribuir, string cnx)
        {
            SqlConnection conn = new SqlConnection(cnx);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            string idMOV = "";

            try
            {

                string sql = "INSERT INTO TICKETMOVIMENTO (IDTICKET,IDUSUARIO,DATA, DESCRICAO) VALUES ";
                sql += "(" + idTicket + "," + Idusuario + ",GetDate(), '" + texto + "'); select SCOPE_IDENTITY()";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sql;
                idMOV = cmd.ExecuteScalar().ToString();

                for (int i = 0; i < Arquivos.Count; i++)
                {
                    string par = "@arquivo" + i.ToString();

                    sql = "INSERT INTO TICKETMOVIMENTOANEXO (IdTicketMovimento, Arquivo, NomeArquivo) VALUES ";
                    sql += "(" + idMOV + ", " + par + " , '" + NomeArquivos[i] + "')";

                    cmd.Parameters.Add(new SqlParameter(par, Arquivos[i]));
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                if (AtribuirChamado)
                {
                    sql = "UPDATE TICKET SET IDUSUARIOATRIBUIDO=" + idUsuarioAAtribuir + " WHERE IDTICKET=" + idTicket;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void IniciarTarefa(int idTicket, int Idusuario, List<byte[]> Arquivos, List<string> NomeArquivos, string texto, string cnx)
        {
            SqlConnection conn = new SqlConnection(cnx);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            string idMOV = "";

            try
            {

                string sql = "INSERT INTO TICKETMOVIMENTO (IDTICKET,IDUSUARIO,DATA, DESCRICAO) VALUES ";
                sql += "(" + idTicket + "," + Idusuario + ",GetDate(), 'INICIOU A EXECUÇÃO DO CHAMADO <br>  " + texto + "'); select SCOPE_IDENTITY()";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sql;
                idMOV = cmd.ExecuteScalar().ToString();

                for (int i = 0; i < Arquivos.Count; i++)
                {
                    string par = "@arquivo" + i.ToString();

                    sql = "INSERT INTO TICKETMOVIMENTOANEXO (IdTicketMovimento, Arquivo, NomeArquivo) VALUES ";
                    sql += "(" + idMOV + ", " + par + " , '" + NomeArquivos[i] + "')";

                    cmd.Parameters.Add(new SqlParameter(par, Arquivos[i]));
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                sql = " UPDATE TICKET SET INICIODATAREFA=GETDATE(), STATUS='EM ANDAMENTO' WHERE IDTICKET=" + idTicket;

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void FinalizarTarefa(int idTicket, int Idusuario, List<byte[]> Arquivos, List<string> NomeArquivos, string texto, string cnx)
        {
            SqlConnection conn = new SqlConnection(cnx);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            string idMOV = "";

            try
            {

                string sql = "INSERT INTO TICKETMOVIMENTO (IDTICKET,IDUSUARIO,DATA, DESCRICAO) VALUES ";
                sql += "(" + idTicket + "," + Idusuario + ",GetDate(), 'FINALIZOU A EXECUÇÃO DO CHAMADO <br> " + texto + "'); select SCOPE_IDENTITY()";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sql;
                idMOV = cmd.ExecuteScalar().ToString();

                for (int i = 0; i < Arquivos.Count; i++)
                {
                    string par = "@arquivo" + i.ToString();

                    sql = "INSERT INTO TICKETMOVIMENTOANEXO (IdTicketMovimento, Arquivo, NomeArquivo) VALUES ";
                    sql += "(" + idMOV + ", " + par + " , '" + NomeArquivos[i] + "')";

                    cmd.Parameters.Add(new SqlParameter(par, Arquivos[i]));
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                sql = " UPDATE TICKET SET FINALDATAREFA=GETDATE(), STATUS='EM ANDAMENTO' WHERE IDTICKET=" + idTicket;

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void Finalizar(int idTicket, int Idusuario, List<byte[]> Arquivos, List<string> NomeArquivos, string texto, string cnx)
        {
            SqlConnection conn = new SqlConnection(cnx);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            string idMOV = "";

            try
            {

                string sql = "INSERT INTO TICKETMOVIMENTO (IDTICKET,IDUSUARIO,DATA, DESCRICAO) VALUES ";
                sql += "(" + idTicket + "," + Idusuario + ",GetDate(), 'CHAMADO FINALIZADO <br>  " + texto + "'); select SCOPE_IDENTITY()";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sql;
                idMOV = cmd.ExecuteScalar().ToString();

                for (int i = 0; i < Arquivos.Count; i++)
                {
                    string par = "@arquivo" + i.ToString();

                    sql = "INSERT INTO TICKETMOVIMENTOANEXO (IdTicketMovimento, Arquivo, NomeArquivo) VALUES ";
                    sql += "(" + idMOV + ", " + par + " , '" + NomeArquivos[i] + "')";

                    cmd.Parameters.Add(new SqlParameter(par, Arquivos[i]));
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                sql = " UPDATE TICKET SET Conclusao=GETDATE(), STATUS='FINALIZADO' WHERE IDTICKET=" + idTicket;

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        #region TicketDivisao

        public class Divisao
        {
            public DataTable Retornar(string cnx)
            {
                StringBuilder txt = new StringBuilder();
                txt.Append("SELECT * FROM TICKETDIVISAO ORDER BY  DIVISAO");
                return DAL.BD.cDb.RetornarDataTable(txt.ToString(), cnx);
            }

        }


        #endregion

        public class UserTicket
        {
            public int? IdUsuario { get; set; }
            public string Nome { get; set; }
            public string Login { get; set; }
            public string Senha { get; set; }
            public int IdConexao { get; set; }
        }
    }
}