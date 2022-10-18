using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ImpressaoEtiquetaUA
{
    public static class cDB
    {
        public static DataTable RetornarUa(string IdUa, string CodigodeBarras, string IdFilial, string cnx)
        {
            string sql = "SELECT TOP 50 UA.IDUNIDADEDEARMAZENAGEM CODIGO, UA.DIGITO,ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) CLIENTE, P.CODIGODEBARRAS, PC.DESCRICAO,PC.METODODEMOVIMENTACAO";
            sql += " FROM UNIDADEDEARMAZENAGEM UA ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDProdutoCliente = UA.IdProdutoCliente   ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDProdutoEmbalagem = ua.IDProdutoEmbalagem   ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " INNER JOIN CADASTRO C ON C.IDCADASTRO = PC.IDCLIENTE ";
            sql += " where UA.IDFILIAL = " + IdFilial;
            //sql += " AND (P.CODIGODEBARRAS Like  '%" + CodigodeBarras + "%' )";
            sql += " AND (CAST(IDUNIDADEDEARMAZENAGEM AS VARCHAR(30)) LIKE '%" + IdUa + "%' ) ";
            sql += " AND UA.DIGITO IS NOT NULL ";

            sql += " ORDER BY UA.IDUNIDADEDEARMAZENAGEM DESC ";
            return RetornarDataTable(sql, cnx);
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


    }
}
