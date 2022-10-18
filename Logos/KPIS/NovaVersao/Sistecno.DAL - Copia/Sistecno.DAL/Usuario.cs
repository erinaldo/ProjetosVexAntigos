using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL.Models;


namespace Sistecno.DAL
{
    public class Usuario
    {

        public DataTable RetornarUsuarioCliente(int idCLiente, string cnx)
        {
            string sql = " Select U.IDUsuario, U.Nome ";
            sql += " From Usuario U      ";
            sql += " Inner Join Conexao Cx on (Cx.IdConexao = U.IdConexao)    Inner Join Cliente Cl on (Cl.IdCliente = Cx.IdCliente)     ";
            sql += " Inner Join Cadastro Cd on Cd.IdCadastro = CL.IdCliente  ";
            sql += " WHERE CX.IdCliente = " + idCLiente;
            sql += " ORDER BY Nome ";

            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }


        public int RetornarIdConexao(int idcliente, string cnx)
        {
            string sql = "SELECT IDCONEXAO FROM CONEXAO WHERE IDCLIENTE=" + idcliente;
            return int.Parse(DAL.BD.cDb.ExecutarRetornoIDs(sql, cnx));
        }

        public DAL.Models.Usuario Logar(DAL.Models.Usuario usuario, string cnx)
        {
            try
            {
                string sql = "Select * from Usuario where login='" + usuario.Login + "' and senha='" + usuario.Senha + "'";
                DataTable dt = DAL.BD.cDb.RetornarDataTable(sql, cnx);

                DAL.Models.Usuario ret = new DAL.Models.Usuario();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ATIVO"].ToString() == "NAO")
                        throw new Exception("Usuário Inativo");

                    ret.IDUsuario = int.Parse(dt.Rows[0]["IDUsuario"].ToString());
                    ret.IDCadastro = int.Parse(dt.Rows[0]["IDCadastro"].ToString());
                    ret.IDGrupo = (dt.Rows[0]["IDGrupo"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDGrupo"]);
                    ret.IDPerfil = (dt.Rows[0]["IDPerfil"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDPerfil"]);
                    ret.UltimaEmpresa = (dt.Rows[0]["UltimaEmpresa"] == DBNull.Value ? null : (int?)dt.Rows[0]["UltimaEmpresa"]);
                    ret.UltimaFilial = (dt.Rows[0]["UltimaFilial"] == DBNull.Value ? null : (int?)dt.Rows[0]["UltimaFilial"]);
                    ret.Nome = dt.Rows[0]["nome"].ToString().ToUpper();
                    ret.Login = usuario.Login.ToUpper();
                    ret.Senha = usuario.Senha;
                    ret.TipoDeSistema = dt.Rows[0]["TipoDeSistema"].ToString();
                    return ret;
                }
                else
                    throw new Exception("Usuário não encontrado!!!");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DAL.Models.Usuario LogarSistecno(DAL.Models.Usuario usuario, string cnx)
        {
            try
            {
                string sql = "Select * from Usuario where login='" + usuario.Login + "'";
                DataTable dt = DAL.BD.cDb.RetornarDataTable(sql, cnx);

                DAL.Models.Usuario ret = new DAL.Models.Usuario();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ATIVO"].ToString() == "NAO")
                        throw new Exception("Usuário Inativo");

                    ret.IDUsuario = int.Parse(dt.Rows[0]["IDUsuario"].ToString());
                    ret.IDCadastro = int.Parse(dt.Rows[0]["IDCadastro"].ToString());
                    ret.IDGrupo = (dt.Rows[0]["IDGrupo"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDGrupo"]);
                    ret.IDPerfil = (dt.Rows[0]["IDPerfil"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDPerfil"]);
                    ret.UltimaEmpresa = (dt.Rows[0]["UltimaEmpresa"] == DBNull.Value ? null : (int?)dt.Rows[0]["UltimaEmpresa"]);
                    ret.UltimaFilial = (dt.Rows[0]["UltimaFilial"] == DBNull.Value ? null : (int?)dt.Rows[0]["UltimaFilial"]);
                    ret.Nome = dt.Rows[0]["nome"].ToString().ToUpper();
                    ret.Login = usuario.Login.ToUpper();
                    ret.Senha = usuario.Senha;
                    ret.TipoDeSistema = dt.Rows[0]["TipoDeSistema"].ToString();
                    return ret;
                }
                else
                    throw new Exception("Usuário não encontrado!!!");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DAL.Models.Usuario retornaUltimaInfo(string idUsu , string cnx)
        {
            try
            {

                string sql = "Select * from Usuario where idusuario=" + idUsu;

                if(idUsu=="-1")
                    sql = "Select * from Usuario order by Nome";

                DataTable dt = DAL.BD.cDb.RetornarDataTable(sql, cnx);

                DAL.Models.Usuario ret = new DAL.Models.Usuario();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ATIVO"].ToString() == "NAO")
                        throw new Exception("Usuário Inativo");

                    ret.IDUsuario = int.Parse(dt.Rows[0]["IDUsuario"].ToString());
                    ret.IDCadastro = int.Parse(dt.Rows[0]["IDCadastro"].ToString());
                    ret.IDGrupo = (dt.Rows[0]["IDGrupo"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDGrupo"]);
                    ret.IDPerfil = (dt.Rows[0]["IDPerfil"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDPerfil"]);
                    ret.UltimaEmpresa = (dt.Rows[0]["UltimaEmpresa"] == DBNull.Value ? null : (int?)dt.Rows[0]["UltimaEmpresa"]);
                    ret.UltimaFilial = (dt.Rows[0]["UltimaFilial"] == DBNull.Value ? null : (int?)dt.Rows[0]["UltimaFilial"]);
                    ret.Nome = dt.Rows[0]["nome"].ToString().ToUpper();
                    ret.Login = dt.Rows[0]["LOGIN"].ToString().ToUpper();  
                    return ret;
                }
                else
                    throw new Exception("Usuário não encontrado!!!");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable Retornar(string cnx)
        {
            try
            {

                string sql = "Select * from Usuario order by Nome";

                return DAL.BD.cDb.RetornarDataTable(sql, cnx);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GravaInformacoesEmpresaLogin(DAL.Models.Usuario usuario, string cnx)
        {
           /* var context = new SistecnoContext();
            //
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var obj = context.Usuario.FirstOrDefault(i => i.IDUsuario == usuario.IDUsuario);

            obj.UltimoAcesso = usuario.UltimoAcesso;
            obj.UltimaFilial = usuario.UltimaFilial;
            obj.UltimaEmpresa = usuario.UltimaEmpresa;
            context.SaveChanges();
            context.Database.Connection.Close();

            //return this.Logar(usuario, cnx);*/

            string sql = "Update usuario set UltimaFilial="+usuario.UltimaFilial+" ,UltimaEmpresa="+usuario.UltimaEmpresa+"  where idusuario= "+ usuario.IDUsuario + " Select 1";
             DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }

        public int Inserir(DAL.Models.Usuario obj, string cnx)
        {
            var context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            try
            {
                context.Database.Connection.Open();
                obj.IDUsuario = DAL.BD.cDb.RetornarIDTabela(cnx, "USUARIO");
                context.Usuarios.Add(obj);
                context.SaveChanges();
                return obj.IDUsuario;
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

        public  DataTable RetornarInformacoesUsuarioCliente(int idUsuario, string cnx)
        {
            string strsql =  "SELECT TOP 1 * FROM USUARIOCLIENTE  UC INNER JOIN CLIENTE C ON C.IDCLIENTE = UC.IDCLIENTE INNER JOIN USUARIO U ON U.IDUSUARIO = UC.IDUSUARIO WHERE u.IDUSUARIO = "+ idUsuario;
            return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
        }

        public bool ExisteDivisao (int idUsuario,  int idCliente, string cnx)
        {
            string strsql =  "SELECT COUNT(*) FROM USUARIO U   INNER JOIN USUARIOCLIENTE UC ON UC.IDUSUARIO = U.IDUSUARIO   INNER JOIN CLIENTEDIVISAO CD ON CD.IDCLIENTE = UC.IDCLIENTE   INNER JOIN ESTOQUEDIVISAO ED ON ED.IDCLIENTEDIVISAO = CD.IDCLIENTEDIVISAO   WHERE U.IDUSUARIO  = "+idUsuario+" AND UC.IDCLIENTE = "+ idCliente +"   AND CD.ATIVO = 'SIM'";
            DataTable d = DAL.BD.cDb.RetornarDataTable(strsql, cnx);

            if (d == null || d.Rows[0][0].ToString() == "0")
                return false;
            else
                return true;
        }        
    }
}
