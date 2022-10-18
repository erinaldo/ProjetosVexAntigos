using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL.BD
{    public class cDb
    {
     
        public void CommitTrans(DbTransaction trans)
        {
            trans.Commit();
            // trans.Connection.Close();
        }

        public void RollbackTrans(DbTransaction trans)
        {
            trans.Rollback();
            trans.Connection.Close();

        }

        /// <summary>
        /// Executa Procedure
        /// </summary>
        /// <param name="NomeProcedure">Nome da Procedure</param>
        /// <param name="parametros">Lista com os Parametros</param>
        /// <param name="Conx">String Conexao</param>
        /// <returns></returns>
        public static void ExecutarProcedure(string NomeProcedure, List<ParametrosProcedures> parametros, string Conx)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conx;
            cd.CommandText = NomeProcedure;
            cd.CommandType = CommandType.StoredProcedure;

            DbParameter[] param = new DbParameter[parametros.Count];

            for (int i = 0; i < parametros.Count; i++)
            {
                DbParameter par = factory.CreateParameter();
                par.ParameterName = "@" + parametros[i].nomePar;

                par.Value = parametros[i].valorPar;

                if (parametros[i].direcao != null && parametros[i].direcao != "")
                {
                    par.Direction = ParameterDirection.Output;
                }

                switch (parametros[i].tipoDeDados)
                {
                    case "string":
                        par.DbType = DbType.String;
                        break;
                    case "int":
                        par.DbType = DbType.Int32;
                        break;
                    case "datetime":
                        par.DbType = DbType.DateTime;
                        break;

                    default:
                        par.DbType = DbType.String;
                        break;
                }
                param[i] = par;
            }

            if (parametros.Count > 0)
                cd.Parameters.AddRange(param);

            cd.Connection = cn;
            cn.Open();
            try
            {
                cd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }

        }

        /// <summary>
        /// Retorna o ID Tabela conforme a tabela
        /// </summary>
        /// <param name="Conn">String de Conexao</param>
        /// <param name="sTabela">Nome da Tabela</param>
        /// <returns>Int</returns>
        public static int RetornarIDTabela(string Conn, string sTabela)
        {
            int ret = 0;

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            cd.CommandText = "GERAR_ID_TABELA";
            cd.CommandType = CommandType.StoredProcedure;

            DbParameter p1 = factory.CreateParameter();
            p1.ParameterName = "@NOMEDATABELA";
            p1.DbType = DbType.String;
            p1.Value = sTabela;

            DbParameter p2 = factory.CreateParameter();
            p2.ParameterName = "@RETORNAR_ID_TABELA";
            p2.DbType = DbType.Int32;
            p2.Value = "0";
            p2.Direction = ParameterDirection.Output;

            DbParameter[] parametros = { p1, p2 };

            cd.Parameters.AddRange(parametros);

            cd.Connection = cn;
            cn.Open();
            try
            {
                cd.ExecuteNonQuery();
                ret = int.Parse(cd.Parameters["@RETORNAR_ID_TABELA"].Value.ToString());
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }




            return ret;

        }

        /// <summary>
        /// Retorna um Data Datble
        /// </summary>
        /// <param name="sql">Comando Sql</param>
        /// <param name="Conn">String de Conexao</param>
        /// <returns>DataTable</returns>
        public static DataTable RetornarDataTable(string sql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            cd.CommandText = sql;
            cd.Connection = cn;
            cn.Open();
            da.SelectCommand = cd;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            finally
            {
                // cd.Dispose();
                cn.Close();
                // cn.Dispose();
            }
            return ds.Tables[0];
        }


        /// <summary>
        /// Retorna um DataSet
        /// </summary>
        /// <param name="sql">Comando Sql</param>
        /// <param name="Conn">String de Conexao</param>
        /// <returns>DataSet</returns>
        public static DataSet RetornarDataSet(string sql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            cd.CommandText = sql;
            cd.Connection = cn;
            cn.Open();
            da.SelectCommand = cd;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ds;
        }

        /// <summary>
        /// Executa um comando sql e retorna um id INT/ ou retorna a primeira linha e primeira coluna
        /// </summary>
        /// <param name="sql">Comando Sql</param>
        /// <param name="Conn">String de Conexao</param>
        /// <returns>int</returns>
        public static int ExecutarRetornoID(string isql, string Conn)
        {
            int ret = 0;
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            //Microsoft.Practices.EnterpriseLibrary.Data.Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            //cn.ConnectionString = SistranDb.ConnectionString;

            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();

            try
            {
                ret = Convert.ToInt32(cd.ExecuteScalar());
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }

        /// <summary>
        /// Executa um comando sql e retorna um id String / ou retorna a primeira linha e primeira coluna
        /// </summary>
        /// <param name="sql">Comando Sql</param>
        /// <param name="Conn">String de Conexao</param>
        /// <returns>String</returns>
        public static string ExecutarRetornoIDs(string isql, string Conn)
        {
            string ret = "";
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            /// Microsoft.Practices.EnterpriseLibrary.Data.Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            //cn.ConnectionString = SistranDb.ConnectionString;
            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();
            try
            {
                ret = cd.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }

        //public  static void ExcutarMultiplosSql(string sql, string cnx)
        //{
        //    try
        //    {
        //        SqlConnection con = new SqlConnection();                
        //        SqlConnection conn = new SqlConnection(cnx);
        //        Server server = new Server(new ServerConnection(conn));
        //        server.ConnectionContext.ExecuteNonQuery(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        /// <summary>
        /// Somente executa um comando sql
        /// </summary>
        /// <param name="sql">Comando Sql</param>
        /// <param name="Conn">String de Conexao</param>
        /// <returns></returns>
        public static void ExecutarSemRetorno(string isql, string Conn)
        {

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;


            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();
            try
            {
                cd.ExecuteNonQuery();
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
        }


        public static void Executar(string isql, string Conn)
        {
            SqlConnection cnn = new SqlConnection(Conn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = isql;

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                cnn.Close();
            }
        }

        public static void CriarDatabase(string isql, string Conn, string NomeBd)
        {
            SqlConnection myConn = new SqlConnection(Conn.Replace(NomeBd, "master"));
            SqlCommand myCommand = new SqlCommand(isql, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();

            }
        }
    }

    public class ParametrosProcedures
    {
        public string tipoDeDados { get; set; }
        public string nomePar { get; set; }
        public string valorPar { get; set; }
        public string direcao { get; set; }

    }
}