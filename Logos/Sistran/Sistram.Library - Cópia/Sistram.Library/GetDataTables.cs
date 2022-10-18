using System;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;
using System.Collections.Generic;

namespace Sistran.Library
{
    public static class ExecutarVarios
    {
        public static DataTable RetornarDataTableUsandoMesmaConexao(string isql, DbConnection cn)
        {

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();

            cd.CommandText = isql;
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
            }
            return ds.Tables[0];
        }
        
        public static DbConnection AbrirConexao(DbConnection conn)
        {
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                conn = factory.CreateConnection();
                conn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception)
            {
            }
            return conn;
        }     

        public static void FecharConexao(DbConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

    }

    public static class GetDataTables
    {

        public static void InserirCotacao(string Conn, string sql)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

            if (HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "")
            {
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }

            cd.Connection = cn;
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();

            try
            {
                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;
                cd.ExecuteNonQuery();
                oTrans.Commit();

            }
            catch (Exception)
            {
                oTrans.Rollback();
                throw;
            }
            finally 
            {
                cn.Close();
            }
        }

        public static DataTable RetornarDataTableWS(string isql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            cd.CommandText = isql;
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
            return ds.Tables[0];
        }

        public static DataSet RetornarDataSetWS(string isql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            //cn.ConnectionTimeout = 120;
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            cd.CommandTimeout = 3000;
            cd.CommandText = isql;
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
                       
        public static void InserirImagens(string idProd, byte[] im)
        {

            SqlCommand command = new SqlCommand();
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOFOTO");

            string strsql = " INSERT INTO PRODUTOFOTO (";
            strsql += " IDProdutoFoto, ";
            strsql += " IDProduto,";
            strsql += " Foto";
            strsql += " ) VALUES";
            strsql += " (";
            strsql += ID + ", ";
            strsql += "@IDPRODUTO ,";
            strsql += "@IMAGEM )";           

            SqlConnection vv = new SqlConnection(HttpContext.Current.Session["ConnLogin"].ToString());
            command.CommandText = strsql.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = vv;

            command.Parameters.Add(new SqlParameter("@IDPRODUTO", idProd));
            command.Parameters.Add(new SqlParameter("@IMAGEM", im));

            vv.Open();
            command.ExecuteNonQuery();
            vv.Close();
            vv.Dispose();
        }


        public static void InserirArquivosProjeto(string idProjeto, byte[] arq, string descr, string nome, string tipo, string idProjetoArquivo)
        {

            SqlCommand command = new SqlCommand();
            //string ID = Sistran.Library.GetDataTables.RetornarIdTabela("PROJETOARQUIVO");

            string strsql = " INSERT INTO PROJETOARQUIVO (";
            strsql += "IdProjetoArquivo,";
            strsql += "IdProjeto,";
            strsql += "Descricao,";
            strsql += "Data,";
            strsql += "Arquivo,";
            strsql += "Nome,";
            strsql += "Tipo";
            strsql += " ) VALUES (";
            strsql += idProjetoArquivo + ",";
            strsql += idProjeto +",";
            strsql += "'"+descr+"',";
            strsql += "getdate(),";
            strsql += "@Arquivo,";
            strsql += "'"+nome+"',";
            strsql += "'"+tipo+"')";           

            SqlConnection vv = new SqlConnection(HttpContext.Current.Session["ConnLogin"].ToString());

            command.CommandText = strsql.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = vv;
            command.Parameters.Add(new SqlParameter("@Arquivo", arq));         
            vv.Open();
            command.ExecuteNonQuery();
            vv.Close();
            vv.Dispose();
        }


        public static void InserirArquivos(string idArquivo, byte[] arq, string nome)
        {

            SqlCommand command = new SqlCommand();
            //string ID = Sistran.Library.GetDataTables.RetornarIdTabela("PROJETOARQUIVO");

            string strsql = " INSERT INTO ARQUIVOITEM ( ";
            strsql += " IDARQUIVOITEM,";
            strsql += " IDARQUIVO,";
            strsql += " NOMEDOARQUIVO,";
            strsql += " CONTEUDOARQUIVO";
            strsql += " ) ";
            strsql += " VALUES";
            strsql += " (";
            strsql += Sistran.Library.GetDataTables.RetornarIdTabela("ArquivoItem") + ",";
            strsql += idArquivo + " ,";
            strsql += " '" + nome + "',";
            strsql += " @Arquivo";
            strsql += " )";

            SqlConnection vv = new SqlConnection(HttpContext.Current.Session["ConnLogin"].ToString());

            command.CommandText = strsql.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = vv;
            command.Parameters.Add(new SqlParameter("@Arquivo", arq));
            vv.Open();
            command.ExecuteNonQuery();
            vv.Close();
            vv.Dispose();
        }

        public static void AlterarImagens(string idProd, byte[] im)
        {

            SqlCommand command = new SqlCommand();


            string strsql = " UPDATE PRODUTOFOTO SET Foto=@IMAGEM WHERE  IDProduto =@IDPRODUTO ";

            SqlConnection vv = new SqlConnection(HttpContext.Current.Session["ConnLogin"].ToString());

            command.CommandText = strsql.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = vv;

            command.Parameters.Add(new SqlParameter("@IDPRODUTO", idProd));
            command.Parameters.Add(new SqlParameter("@IMAGEM", im));

            vv.Open();

            command.ExecuteNonQuery();

            vv.Close();
            vv.Dispose();
        }

        public static DataTable FotoMotorista(string idMotorista)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            string strsql = " select cast(IDCadastroImagem as nvarchar(200)) idTemp, IDCadastroImagem id, Imagem conteudo, '' excluido, Nome texto from CadastroImagem where IDCadastro = " + idMotorista;

            SqlConnection sqlConnection = new SqlConnection(HttpContext.Current.Session["ConnLogin"].ToString());

            command.CommandText = strsql.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = sqlConnection;

            sqlConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            sqlConnection.Close();
            sqlConnection.Dispose();
            return dt;

        }

        public static DataTable PlanilhaEstoque()
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            string strsql = " SELECT * FROM PLANILHAESTOQUE WHERE PROCESSADO IS NULL AND [TRADE]+NORDESTE+KA+[MG / NO]  +[RIO DE JANEIRO]+[SP CAPITAL]+[SP INTERIOR]+SUL >0";

            SqlConnection sqlConnection = new SqlConnection(HttpContext.Current.Session["ConnLogin"].ToString());

            command.CommandText = strsql.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = sqlConnection;

            sqlConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            sqlConnection.Close();
            sqlConnection.Dispose();
            return dt;

        }

        public static DataTable PesquisarImagensDocumentoOcorrencia(string idDocumento, string Conn)
        {
            string strsql = " SELECT ARQUIVO FROM DOCUMENTOOCORRENCIAARQUIVO WHERE IDDOCUMENTOOCORRENCIA IN(SELECT IDDOCUMENTOOCORRENCIA FROM DOCUMENTOOCORRENCIA WHERE IDDOCUMENTO = " + idDocumento + ") AND ARQUIVO IS NOT NULL  ";

            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();

            SqlConnection sqlConnection = new SqlConnection(Conn);

            command.CommandText = strsql.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = sqlConnection;

            sqlConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            sqlConnection.Close();
            sqlConnection.Dispose();
            return dt;

        }

        public static DataTable PesquisarDocImagem(string Conn, string IDDOCUMENTO)
        {
            return RetornarDataTable("SELECT IDDOCUMENTOIMAGEM, TITULO  FROM DOCUMENTOIMAGEM  WHERE IDDOCUMENTO=" + IDDOCUMENTO, Conn);

        }

        public static DataTable RetornarDataTable(string isql, string Conn)
        {
            if (Conn == "")
                Conn = HttpContext.Current.Session["Conn"].ToString();

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();

            if ((HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "") && HttpContext.Current.Session["ConnLogin"] != null)
            {
                if(HttpContext.Current.Session["ConnLogin"].ToString()!="")
                                  cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
                else
                    cn.ConnectionString = HttpContext.Current.Session["Conn"].ToString();
            }
            else if (Conn != "")
            {
                cn.ConnectionString = Conn;
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }


            cd.CommandText = isql;
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
            return ds.Tables[0];
        }

        public static DataTable RetornarDataTable(string isql)
        {

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();

            if (HttpContext.Current.Session["ConnLogin"] != null)
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            else
                cn.ConnectionString = "";

            cd.CommandText = isql;
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
            return ds.Tables[0];
        }

        public static DataTable RetornarDataTableBoleto(string isql)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();


            cn.ConnectionString = "Data Source=192.168.10.4;Initial Catalog=grupologos;User ID=site_ASP;Password=asp7998;";

            cd.CommandText = isql;
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
            return ds.Tables[0];
        }

        public static DataSet RetornarDataSet(string isql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");

            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();

            //if (HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "")
            //{
            //    cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            //}
            //else
            //{
            //    Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            //    cn.ConnectionString = SistranDb.ConnectionString;
            //}

            if (Conn != "")
            {
                cn.ConnectionString = Conn;

            }
            else if (HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "" )
            {
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();

            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }

            cd.CommandText = isql;
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

        public static void ExecutarSemRetorno(string isql, string Conn)
        {
            if (Conn == "")
                Conn = HttpContext.Current.Session["Conn"].ToString();

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();


            if (HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "")
            {
                if(HttpContext.Current.Session["ConnLogin"]!=null)
                    cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
                else
                    cn.ConnectionString = Conn;
            }
            else if (Conn != "")
            {
                cn.ConnectionString = Conn;
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }

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

        public static void ExecutarSemRetornoWin(string isql, string Conn)
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

        public static decimal RetornarDecimal(string isql, string Conn)
        {
            decimal ret = Convert.ToDecimal(0);
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();

            if ((HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "") && HttpContext.Current.Session["ConnLogin"] != null)
            {
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            }
            else if (Conn != "")
            {
                cn.ConnectionString = Conn;
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }



            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();


            try
            {
                ret = Convert.ToDecimal(cd.ExecuteScalar());
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }

        public static int ExecutarRetornoID(string isql, string Conn)
        {

            if (Conn == "" || Conn == null)
                Conn = HttpContext.Current.Session["Conn"].ToString();

            if (Conn == "" || Conn == null)
                Conn = HttpContext.Current.Session["ConnLogin"].ToString();

            int ret = 0;
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();

            

            if ((HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "") && HttpContext.Current.Session["ConnLogin"] != null)
            {
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            }
            else if (Conn != "")
            {
                cn.ConnectionString = Conn;
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }



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
        
        public static int ExecutarRetornoIDWin(string isql, string Conn)
        {
            int ret = 0;
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();


            cn.ConnectionString = Conn;
            //Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
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

        public static string ExecutarRetornoIDs(string isql, string Conn)
        {
            string ret = "";
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();

            if (Conn == "")
            {
                if (HttpContext.Current.Session["Conn"] != null || HttpContext.Current.Session["Conn"].ToString() != "")
                {
                    cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
                }
                else if (Conn != "")
                {
                    cn.ConnectionString = Conn;
                }
                else
                {
                    Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                    cn.ConnectionString = SistranDb.ConnectionString;
                }
            }
            else
            {
                cn.ConnectionString = Conn;

            }

            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();


            try
            {
                ret = cd.ExecuteScalar().ToString();
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }

        public static int TransacaoInserirCadastroTrasnportadora(string Conn, SistranMODEL.Cadastro oCad, SistranMODEL.Cadastro.Tranportadora oTransp)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

            if (HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "")
            {
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }

            cd.Connection = cn;
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();

            try
            {
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO");

                string sql = " INSERT INTO CADASTRO ";
                sql += " ( ";
                sql += " IDCADASTRO, ";
                sql += " CNPJCPF, ";
                sql += " INSCRICAORG, ";
                sql += " RAZAOSOCIALNOME, ";
                sql += " FANTASIAAPELIDO, ";
                sql += " ENDERECO, ";
                sql += " NUMERO, ";
                sql += " COMPLEMENTO, ";
                sql += " IDCIDADE, ";
                sql += " IDBAIRRO, ";
                sql += " CEP, ";
                sql += " DATADECADASTRO ";
                sql += " ) ";
                sql += " VALUES ";
                sql += " ( ";
                sql += ID + " , ";
                sql += " '" + oCad.CnpjCpf + "', ";
                sql += " '" + oCad.InscricaoRG + "', ";
                sql += " '" + oCad.RazaoSocialNome.ToUpper() + "', ";
                sql += " '" + oCad.RazaoSocialNome.ToUpper() + "', ";
                sql += " '" + oCad.Endereco.ToUpper() + "', ";
                sql += " '" + oCad.Numero.ToUpper() + "', ";
                sql += " '" + oCad.Complemento.ToUpper() + "', ";
                sql += oCad.IDCidade.ToString() + " , ";
                sql += oCad.IDBairro.ToString() + ", ";
                sql += " '" + oCad.Cep + "', ";
                sql += " GETDATE() ";
                sql += " )  ";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;
                cd.ExecuteNonQuery();
                int idCadastro = Convert.ToInt32(ID);

                sql = "INSERT INTO TRANSPORTADORA (IDTRANSPORTADORA, IDCONTACONTABIL) VALUES (" + idCadastro.ToString() + ", " + oTransp.IDContaContabil.ToString() + ") ";
                cd.CommandText = sql;
                cd.Transaction = oTrans;

                cd.ExecuteNonQuery();
                oTrans.Commit();
                return idCadastro;
            }
            catch (Exception)
            {
                oTrans.Rollback();
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        /// <summary>
        /// Retorna Novo Id Tabela 
        /// </summary>
        /// <param name="Conn">String de Conexão quando a sessao conn estiver vazia</param>
        /// <param name="Tabela">Nome da tabela</param>
        /// <returns>Retorna o Novo ID de Determinada Tabela</returns>
        public static string RetornarIdTabela(string Tabela)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

            cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            cd.Connection = cn;
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();
            int nId = 0;
            try
            {
                // 1-Verifia se a tabela existe
                string sql = " SELECT COUNT(TABELA) AS QTD FROM IDTABELA WHERE TABELA = '" + Tabela.Trim() + "'";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;

                //se nao existe insere e já retorna o novo id
                if (Convert.ToInt32(cd.ExecuteScalar()) == 0)
                {
                    sql = " INSERT IDTABELA (Tabela, Id) VALUES( ";
                    sql += " '" + Tabela + "', ";
                    sql += " (SELECT ISNULL(MAX(" + "ID" + Tabela + "),0)+1 FROM " + Tabela + ")) SELECT ISNULL(MAX(ID),0) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    nId = Convert.ToInt32(cd.ExecuteScalar());
                    oTrans.Commit();
                    return nId.ToString();
                }
                else
                {
                    //se exite gera um novo e compara os ids, E OS ACERTA para efetivar
                    sql = " UPDATE IDTABELA SET ID = ";
                    sql += " CASE WHEN (SELECT MAX(ID)+1 FROM IDTABELA WHERE TABELA='" + Tabela + "' )>=(SELECT coalesce(MAX(ID" + Tabela + "),0) FROM " + Tabela + ") THEN ID+1 ";
                    sql += " ELSE (SELECT MAX(ID" + Tabela + ")+1 FROM " + Tabela + ")";
                    sql += " END WHERE TABELA='" + Tabela + "' SELECT MAX(ID) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    nId = Convert.ToInt32(cd.ExecuteScalar());
                    oTrans.Commit();
                    return nId.ToString();
                }
            }
            catch (Exception ex)
            {
                oTrans.Rollback();
                SistranBLL.Usuario.LogBDBLL.GravarLog(0, "RetornarIdTabela", "ERRO", "erro");
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }

        public static string RetornarIdTabela(string Tabela , string conexao)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

            cn.ConnectionString = conexao;
            cd.Connection = cn;
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();
            int nId = 0;
            try
            {
                // 1-Verifia se a tabela existe
                string sql = " SELECT COUNT(TABELA) AS QTD FROM IDTABELA WHERE TABELA = '" + Tabela.Trim() + "'";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;

                //se nao existe insere e já retorna o novo id
                if (Convert.ToInt32(cd.ExecuteScalar()) == 0)
                {
                    sql = " INSERT IDTABELA (Tabela, Id) VALUES( ";
                    sql += " '" + Tabela + "', ";
                    sql += " (SELECT ISNULL(MAX(" + "ID" + Tabela + "),0)+1 FROM " + Tabela + ")) SELECT ISNULL(MAX(ID),0) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    nId = Convert.ToInt32(cd.ExecuteScalar());
                    oTrans.Commit();
                    return nId.ToString();
                }
                else
                {
                    //se exite gera um novo e compara os ids, E OS ACERTA para efetivar
                    sql = " UPDATE IDTABELA SET ID = ";
                    sql += " CASE WHEN (SELECT MAX(ID)+1 FROM IDTABELA WHERE TABELA='" + Tabela + "' )>=(SELECT coalesce(MAX(ID" + Tabela + "),0) FROM " + Tabela + ") THEN ID+1 ";
                    sql += " ELSE (SELECT MAX(ID" + Tabela + ")+1 FROM " + Tabela + ")";
                    sql += " END WHERE TABELA='" + Tabela + "' SELECT MAX(ID) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    nId = Convert.ToInt32(cd.ExecuteScalar());
                    oTrans.Commit();
                    return nId.ToString();
                }
            }
            catch (Exception ex)
            {
                oTrans.Rollback();
                SistranBLL.Usuario.LogBDBLL.GravarLog(0, "RetornarIdTabela", "ERRO", "erro");
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }

        public static string RetornarIdTabelaWin(string Tabela, string cnn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

            cn.ConnectionString = cnn;
            
            cd.Connection = cn;
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();
            int nId = 0;
            try
            {
                // 1-Verifia se a tabela existe
                string sql = " SELECT COUNT(TABELA) AS QTD FROM IDTABELA WHERE TABELA = '" + Tabela.Trim() + "'";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;

                //se nao existe insere e já retorna o novo id
                if (Convert.ToInt32(cd.ExecuteScalar()) == 0)
                {
                    sql = " INSERT IDTABELA (Tabela, Id) VALUES( ";
                    sql += " '" + Tabela + "', ";
                    sql += " (SELECT ISNULL(MAX(" + "ID" + Tabela + "),0)+1 FROM " + Tabela + ")) SELECT ISNULL(MAX(ID),0) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    nId = Convert.ToInt32(cd.ExecuteScalar());
                    oTrans.Commit();
                    return nId.ToString();
                }
                else
                {
                    //se exite gera um novo e compara os ids, E OS ACERTA para efetivar
                    sql = " UPDATE IDTABELA SET ID = ";
                    sql += " CASE WHEN (SELECT MAX(ID)+1 FROM IDTABELA WHERE TABELA='" + Tabela + "' )>=(SELECT coalesce(MAX(ID" + Tabela + "),0) FROM " + Tabela + ") THEN ID+1 ";
                    sql += " ELSE (SELECT MAX(ID" + Tabela + ")+1 FROM " + Tabela + ")";
                    sql += " END WHERE TABELA='" + Tabela + "' SELECT MAX(ID) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    nId = Convert.ToInt32(cd.ExecuteScalar());
                    oTrans.Commit();
                    return nId.ToString();
                }
            }
            catch (Exception ex)
            {
                oTrans.Rollback();
                SistranBLL.Usuario.LogBDBLL.GravarLog(0, "RetornarIdTabela", "ERRO", "erro");
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }



        #region "BASE ANTIGA"

        public static void TransacaoInserirVeiculoBaseAntiga(string Conn, int IDVEICULO, int IDVEICULOMODELO, int IDVEICULOTIPO, int IDVEICULORASTREADOR, int IDCIDADE, int IDPROPRIETARIO, int IDMOTORISTA, string PLACA, string RENAVAN, string CHASSI, int ANO, string COR, decimal CAPACIDADEDECARGAKG, decimal CAPACIDADEDECARGAM3, decimal QUATIDADEDEEIXOS, string CATEGORIASDECNHPERMITIDAS, string ANTT, string NUMEROSERIEEQUIPAMENTO, DateTime VenctoAntt, DateTime DataLiecenc, SistranMODEL.Cadastro.Motorista oMot, SistranMODEL.Cadastro.Proprietario oProp, string MarcaDescricao)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

            if (HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "")
            {
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            }
            else if (Conn != "")
            {
                cn.ConnectionString = Conn;
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }


            cd.Connection = cn;
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();

            try
            {
                string sql = "SELECT COUNT(CNPJCPF) FROM MOTORISTAPROPRIETARIO WHERE CNPJCPF='" + oMot.Cadastro.CnpjCpf + "' AND (PROPRIETARIOMOTORISTAAMBOS='MOTORISTA' OR PROPRIETARIOMOTORISTAAMBOS='AMBOS' )";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;

                if (Convert.ToInt32(cd.ExecuteScalar()) == 0)
                {
                    //Cadastrar o motorista
                    sql = " INSERT INTO MOTORISTAPROPRIETARIO ";
                    sql += " ( ";
                    sql += " CNPJCPF, ";
                    sql += " PROPRIETARIOMOTORISTAAMBOS, ";
                    sql += " CADASTRO, ";
                    sql += " NOME, ";
                    sql += " ENDERECO, ";
                    sql += " NUMERO, ";
                    sql += " BAIRRO, ";
                    sql += " CIDADE, ";
                    sql += " UF, ";
                    sql += " CEP, ";
                    sql += " CNH, ";
                    sql += " VALIDADEDACNH, ";
                    sql += " ATIVO, ";
                    sql += " RG, ";
                    sql += " CNHCATEGORIA, ";
                    sql += " PIS, ";
                    sql += " DEPENDENTES, ";
                    sql += " ACIDENTEQUANTIDADE, ";
                    sql += " ASSALTOQUANTIDADE, ";
                    sql += " INSS, ";
                    sql += " CONTABANCO, ";
                    sql += " CONTAAGENCIA, ";
                    sql += " CONTANUMERO, ";
                    sql += " CONTATIPO, ";
                    sql += " DATANASCIMENTO, ";
                    sql += " SESTSENATALIQUOTA, ";
                    sql += " CONTATITULAR, ";
                    sql += " ESTADOCIVIL, ";
                    sql += " FILIACAOPAI, ";
                    sql += " FILIACAOMAE, ";
                    sql += " LIBERADO ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += " '" + oMot.Cadastro.CnpjCpf + "', ";
                    sql += " 'AMBOS', ";
                    sql += " GETDATE(), ";
                    sql += " '" + oMot.Cadastro.RazaoSocialNome + "', ";
                    sql += " '" + oMot.Cadastro.Endereco + "', ";
                    sql += " '" + oMot.Cadastro.Numero + "', ";
                    sql += " NULL, ";
                    sql += " '" + oMot.Cidade.Nome + "', ";
                    sql += " '" + oMot.Estado.Uf + "', ";
                    sql += " '" + oMot.Cadastro.Cep + "', ";
                    sql += " '" + oMot.CarteiraDeHabilitacao + "', ";
                    sql += " CONVERT(DATETIME,'" + oMot.ValidadeDaHabilitacao + "', 103), ";
                    sql += " '" + oMot.Ativo + "', ";
                    sql += " '" + oMot.Cadastro.InscricaoRG + "', ";
                    sql += " '" + oMot.Categoria + "', ";
                    sql += " '" + oMot.CadastroComplemento.InscricaoPIS + "', ";
                    sql += oMot.CadastroComplemento.Dependentes.ToString() + ", ";
                    sql += oMot.SofreuAcidadeQuantidade.ToString() + ", ";
                    sql += oMot.VitimaDeRouboQuantidade.ToString() + " , ";
                    sql += " '" + oMot.CadastroComplemento.InscricaoNoInss + "', ";
                    sql += "'" + oMot.CadastroComplemento.Banco + "', ";
                    sql += "'" + oMot.CadastroComplemento.Agencia + "', ";
                    sql += " '" + oMot.CadastroComplemento.Conta + oMot.CadastroComplemento.ContaDigito + "', ";
                    sql += " '" + oMot.CadastroComplemento.TipoConta + "', ";
                    sql += " CONVERT(DATETIME,'" + oMot.DataDeNascimento + "', 103), ";
                    sql += oMot.AliquotaSestSenat.ToString().Replace(",", ".") + ", ";
                    sql += " '" + oMot.CadastroComplemento.NomeFavorecido + "', ";
                    sql += " '" + oMot.EstadoCivil + "', ";
                    sql += " '" + oMot.NomeDoPai + "', ";
                    sql += " '" + oMot.NomeDaMae + "', ";
                    sql += " '" + oMot.Liberado + "' ";
                    sql += " ) ";

                    sql = sql.ToUpper();
                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();

                }

                sql = "SELECT COUNT(CNPJCPF) FROM MOTORISTAPROPRIETARIO WHERE CNPJCPF='" + oProp.Cadastro.CnpjCpf + "' AND (PROPRIETARIOMOTORISTAAMBOS='PROPRIETARIO' OR PROPRIETARIOMOTORISTAAMBOS='AMBOS' )";
                cd.CommandText = sql;
                cd.Transaction = oTrans;

                if (Convert.ToInt32(cd.ExecuteScalar()) == 0)
                {
                    //Cadastrar o proprietario
                    sql = " INSERT INTO MOTORISTAPROPRIETARIO ";
                    sql += " ( ";
                    sql += " CNPJCPF, ";
                    sql += " PROPRIETARIOMOTORISTAAMBOS, ";
                    sql += " CADASTRO, ";
                    sql += " NOME, ";
                    sql += " ENDERECO, ";
                    sql += " NUMERO, ";
                    sql += " BAIRRO, ";
                    sql += " CIDADE, ";
                    sql += " UF, ";
                    sql += " CEP, ";
                    sql += " RG, ";
                    sql += " PIS, ";
                    sql += " DEPENDENTES, ";
                    sql += " INSS, ";
                    sql += " CONTABANCO, ";
                    sql += " CONTAAGENCIA, ";
                    sql += " CONTANUMERO, ";
                    sql += " CONTATIPO, ";
                    sql += " CONTATITULAR ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += " '" + oProp.Cadastro.CnpjCpf + "', ";
                    sql += " 'AMBOS', ";
                    sql += " GETDATE(), ";
                    sql += " '" + oProp.Cadastro.RazaoSocialNome + "', ";
                    sql += " '" + oProp.Cadastro.Endereco + "', ";
                    sql += " '" + oProp.Cadastro.Numero + "', ";
                    sql += " NULL, ";
                    sql += " '" + oProp.Cidade.Nome + "', ";
                    sql += " '" + oProp.Estado.Uf + "', ";
                    sql += " '" + oProp.Cadastro.Cep + "', ";
                    sql += " '" + oProp.Cadastro.InscricaoRG + "', ";
                    sql += " '" + oProp.CadastroComplemento.InscricaoPIS + "', ";
                    sql += oProp.CadastroComplemento.Dependentes.ToString() + ", ";
                    sql += " '" + oProp.CadastroComplemento.InscricaoNoInss + "', ";
                    sql += "'" + oProp.CadastroComplemento.Banco + "', ";
                    sql += "'" + oProp.CadastroComplemento.Agencia + "', ";
                    sql += " '" + oProp.CadastroComplemento.Conta + oProp.CadastroComplemento.ContaDigito + "', ";
                    sql += " '" + oProp.CadastroComplemento.TipoConta + "', ";
                    sql += " '" + oProp.CadastroComplemento.NomeFavorecido + "' ";
                    sql += " ) ";
                    sql = sql.ToUpper();

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }
                sql = " SELECT TOP 1 MARCA FROM VEICULOMARCA WHERE DESCRICAO LIKE '%" + MarcaDescricao + "%' UNION SELECT '0' ORDER BY 1 DESC ";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;
                string CodigoMarca = cd.ExecuteScalar().ToString();

                if (CodigoMarca == "0")
                {
                    sql = "  INSERT INTO VEICULOMARCA ";
                    sql += " VALUES ((SELECT  ";
                    sql += " CASE LEN(MAX(ISNULL(MARCA,0))+1)  ";
                    sql += " WHEN 1 THEN '00' +  CAST(MAX(ISNULL(MARCA,0))+1 AS CHAR(3)) ";
                    sql += " WHEN 2 THEN '0'  +  CAST(MAX(ISNULL(MARCA,0))+1 AS CHAR(3)) ";
                    sql += " END  ";
                    sql += " FROM VEICULOMARCA ), '" + MarcaDescricao.ToUpper() + "') SELECT MAX(MARCA) FROM VEICULOMARCA";
                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    CodigoMarca = cd.ExecuteScalar().ToString();
                }

                sql = "SELECT COUNT(PLACA) FROM VEICULO WHERE PLACA='" + PLACA + "'";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;

                if (Convert.ToInt32(cd.ExecuteScalar()) == 0)
                {
                    sql = " INSERT INTO VEICULO ";
                    sql += " ( ";
                    sql += " PLACA, TIPO, MARCA,";
                    sql += " PROPRIETARIO, ";
                    sql += " MOTORISTA, ";
                    sql += " CADASTRO, ";
                    sql += " ANO, ";
                    sql += " COR, ";
                    sql += " CIDADE, ";
                    sql += " UF, ";
                    sql += " CAPACIDADECUBICA, ";
                    sql += " VENCIMENTODOCUMENTO, ";
                    sql += " VEICULOATIVO, ";
                    sql += " CHASSI, ";
                    sql += " RENAVAM, ";
                    sql += " CATEGORIASDECNHPERMITIDAS, ";
                    sql += " CAPACIDADEEMKG, ";
                    sql += " QUATIDADEDEEIXOS, ";
                    sql += " ANTT, ";
                    sql += " ANTTVENCIMENTO, ";
                    sql += " RASTREADOR ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += " '" + PLACA + "',  '000', '" + CodigoMarca + "',";
                    sql += " '" + oProp.Cadastro.CnpjCpf + "', ";
                    sql += " '" + oMot.Cadastro.CnpjCpf + "', ";
                    sql += " getDate(), ";
                    sql += ANO.ToString() + " , ";
                    sql += " '" + COR + "', ";
                    sql += " '" + oMot.Cidade.Nome + "', ";
                    sql += " '" + oMot.Estado.Uf + "', ";
                    sql += " 0, ";
                    sql += " CONVERT(DATETIME,'" + DataLiecenc + "', 103), ";
                    sql += " 'SIM', ";
                    sql += " '" + CHASSI + "', ";
                    sql += " '" + RENAVAN + "', ";
                    sql += " '" + CATEGORIASDECNHPERMITIDAS + "', ";
                    sql += CAPACIDADEDECARGAKG.ToString().Replace(",", ".") + " , ";
                    sql += QUATIDADEDEEIXOS + " , ";
                    sql += " '" + ANTT + "', ";
                    sql += " CONVERT(DATETIME,'" + VenctoAntt + "', 103),";
                    sql += " NULL ";
                    sql += " ) ";
                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();

                }
                else
                {
                    sql = "UPDATE VEICULO SET ";
                    sql += " MARCA='" + CodigoMarca + "',";
                    sql += " PROPRIETARIO='" + oProp.Cadastro.CnpjCpf + "', ";
                    sql += " MOTORISTA='" + oMot.Cadastro.CnpjCpf + "', ";
                    sql += " ANO=" + ANO.ToString() + " , ";
                    sql += " COR='" + COR + "', ";
                    sql += " CIDADE='" + oMot.Cidade.Nome + "', ";
                    sql += " UF='" + oMot.Estado.Uf + "', ";
                    sql += " CHASSI = '" + CHASSI + "', ";
                    sql += " RENAVAM ='" + RENAVAN + "'";
                    sql += " WHERE PLACA='" + PLACA + "' ";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }
                oTrans.Commit();
            }
            catch (Exception ex)
            {
                oTrans.Rollback();
                throw ex;
            }
            finally
            {
                cn.Close();
                HttpContext.Current.Session["Conn"] = null;
            }
        }

        #endregion


        public static void ExecutarComandoSql(string isql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;

            cd.CommandText = isql;
            cd.Connection = cn;
            cd.CommandTimeout = 999999999;
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

        public static string ExecutarRetornoIDServico(string isql, string Conn)
        {
            string ret = "";
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
                ret = cd.ExecuteScalar().ToString();
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }
    }

    public enum TipoHistoricos
    {
        CADASTRO_MOTORISTAS,
        CADASTRO_VEICULOS,
        CADASTROS_PROPRIETARIOS
    }

    public static class FuncoesUteis
    {

        public static string retornarClientes()
        {
            string m = "0";

            if (HttpContext.Current.Session["ListClientes"] == null)
                throw new Exception("Selecione ao menos um cliente.");

            for (int i = 0; i < ((ListBox)HttpContext.Current.Session["ListClientes"]).Items.Count; i++)
            {
                if (m == "0")
                    m = "";


                bool jaExiste = false;
                string[] mm= m.Split(',');


                for (int ii = 0; ii < mm.Length; ii++)
                {
                    if(mm[ii]== ((ListBox) HttpContext.Current.Session["ListClientes"]).Items[i].Value)
                    jaExiste = true;
                }

                if(jaExiste==false)
                    m += int.Parse( ((ListBox)HttpContext.Current.Session["ListClientes"]).Items[i].Value).ToString() + ",";
            }

            if (m.Length > 0)
                m = m.Substring(0, m.Length - 1);
            else
                m = "0";

            return m;
        }


       public static string retornarClientesResumoFilial(bool intranet)
       {

           if (HttpContext.Current.Session["ListClientes"] == null)
               return "";


           string m = "";
           for (int i = 0; i < ((ListBox)HttpContext.Current.Session["ListClientes"]).Items.Count; i++)
           {
               if(intranet)
                     m += ((ListBox)HttpContext.Current.Session["ListClientes"]).Items[i].Text + ",";
               else
                   m += ((ListBox)HttpContext.Current.Session["ListClientes"]).Items[i].Value + ",";

           }

           if (m.Length > 0)
               m = m.Substring(0, m.Length - 1);
           else
               m = "";

           return m;
       }

      

        public static string retornarClientes(string Clientes)
        {
            string[] m = Clientes.Split('*');
            string result = "";

            for (int i = 0; i < m.Length; i++)
            {
                result += m[i]+ ",";
            }

            if (result.Contains(",,"))
            {
                result = result.Replace(",,", ",");
            }

            if (result.Length > 0)
                result = result.Substring(0, result.Length - 1);
            
            
            return result;
        }


        /*public static void InserirHistoricos(string Descricao, TipoHistoricos chave, int id)
        {
            try
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)System.Web.HttpContext.Current.Session["USUARIO"];

                SistranMODEL.Historico hst = new SistranMODEL.Historico();
                hst.Descricao = Descricao.ToUpper();
                hst.Chave = chave.ToString();
                hst.IDUsuario = ILusuario[0].UsuarioId;
                hst.ID = id;
                new SistranBLL.Historico().InserirHistorico(hst);
            }
            catch (Exception)
            {   
            }            
        }*/
    }

    public static class EnviarEmails
    {
        public static void EnviarEmail(string para, string de, string Assunto, string Mensagem, string smtp, string senhaSmtp, string NomeEmailFrom)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                message.From = new MailAddress(de, NomeEmailFrom);

                message.SubjectEncoding = System.Text.Encoding.GetEncoding(1252);
                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
                
                string[] destinatarios = para.Split(';');

                foreach (string dest in destinatarios)
                {
                    if (dest.Trim() != "")
                        message.To.Add(dest);
                }

                message.Bcc.Add("moises@sistecno.com.br");
                message.Subject = Assunto;
                string MsgTipo = MediaTypeNames.Text.Html;
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(Mensagem,System.Text.Encoding.GetEncoding(1252), MsgTipo);
                message.AlternateViews.Add(alternate);
                
                client.Credentials = new System.Net.NetworkCredential(de, senhaSmtp);
                client.Host = smtp;
                client.Send(message);
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable("dterro");
                dt.Columns.Add("erro");
                DataRow r;
                r = dt.NewRow();
                r[0] = ex.Message;
                dt.Rows.Add(r);
                //dt.WriteXml("erro.xml");

            }
            finally
            {
                message.Dispose();
                message = null;
                client = null;
            }
        }

        public static void EnviarEmail(string para, string de, string Assunto, string Mensagem, string smtp, string senhaSmtp)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                message.From = new MailAddress(de, de);

                message.SubjectEncoding = System.Text.Encoding.GetEncoding(1252);
                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252);

                string[] destinatarios = para.Split(';');

                foreach (string dest in destinatarios)
                {
                    if (dest.Trim() != "")
                        message.To.Add(dest);
                }

                message.Bcc.Add("moises@sistecno.com.br");
                message.Subject = Assunto;
                string MsgTipo = MediaTypeNames.Text.Html;
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(Mensagem, System.Text.Encoding.GetEncoding(1252), MsgTipo);
                message.AlternateViews.Add(alternate);

                client.Credentials = new System.Net.NetworkCredential(de, senhaSmtp);
                client.Host = smtp;
                client.Send(message);
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable("dterro");
                dt.Columns.Add("erro");
                DataRow r;
                r = dt.NewRow();
                r[0] = ex.Message;
                dt.Rows.Add(r);
                //dt.WriteXml("erro.xml");

            }
            finally
            {
                message.Dispose();
                message = null;
                client = null;
            }
        }

        public static void enviarEmailGrupoLogos(string assunto, string corpo, List<listaEmailIrwin> emails)
        {
            //create the mail message
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            //set the addresses
            mail.From = new System.Net.Mail.MailAddress("sistema@grupologos.com.br");
            mail.Bcc.Add("moises@sistecno.com.br");

            for (int i = 0; i < emails.Count; i++)
            {
                mail.To.Add(emails[i].email);
            }


            //set the content
            mail.Subject = assunto;
            mail.Body = corpo;
            mail.Priority = System.Net.Mail.MailPriority.High;
            mail.IsBodyHtml = true;

         
            //send the message
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.grupologos.com.br");
            smtp.EnableSsl = false;

            System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential("sistema@grupologos.com.br", "logos0902");
            smtp.Credentials = credenciais;
            smtp.Send(mail);
        }
    }

    public static class Helpers
    {
        public static string FormatarCnpj(string s)
        {
            s = s.Replace(".", "");
            s = s.Replace("-", "");
            s = s.Replace("/", "");
            s = s.Replace(@"\", "");

            if (s.Length == 0)
            {
                return "";
            }

            if (s.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(s, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(s, 11));
                return mtpCnpj.ToString();
            }
        }

        private static string ZerosEsquerda(string strString, int intTamanho)
        {
            string strResult = "";

            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }

            return strResult + strString;
        }

        public static string  RetornarCnnIntranet()
        {
            return IntranetSettings.Default.Conn1;
        }

    }

    public static class ImportTemp
    {
        public static DataTable LeTabelaTemp(string tabela)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            string strsql = "SELECT * FROM " + tabela + " WHERE IMPORTADO IS NULL OR IMPORTADO ='N'";

            SqlConnection sqlConnection = new SqlConnection(HttpContext.Current.Session["ConnLogin"].ToString());

            command.CommandText = strsql.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = sqlConnection;

            sqlConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);


            command.CommandText = "update " + tabela + " set CodigoDoCliente=Codigo where CodigoDoCliente is null or CodigoDoCliente=''" ;
            command.CommandType = CommandType.Text;
            command.Connection = sqlConnection;
            command.ExecuteNonQuery();

            //command.CommandText = "update " + tabela + " set Codigo=CodigoDoCliente where Codigo is null or Codigo=''";
            //command.CommandType = CommandType.Text;
            //command.Connection = sqlConnection;
            //command.ExecuteNonQuery();

            sqlConnection.Close();
            sqlConnection.Dispose();
            return dt;

        }


        public static DataTable LeDivisao(string NomeDivisao)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            string strsql = "SELECT * FROM ClienteDivisao where Nome like '%"+NomeDivisao+"%'";

            SqlConnection sqlConnection = new SqlConnection(HttpContext.Current.Session["ConnLogin"].ToString());

            command.CommandText = strsql.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = sqlConnection;

            sqlConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            sqlConnection.Close();
            sqlConnection.Dispose();
            return dt;

        }

    }

    public static class ArquivoTranportadora
    {
        public static DataTable RetornarArquivos(string OriDest, int IdTransportadora)
        {
            string strsql = "";
            if (OriDest == "R")
            {
                strsql = "SELECT IDEDITROCADEARQUIVO, IDORIGEM,IDDESTINO,TIPODEARQUIVO,ENTRADADATA, ENTRADAIDUSUARIO, SAIDADATA, SAIDAIDUSUARIO, NOMEDOARQUIVO FROM EDITROCADEARQUIVO WHERE IDDESTINO =" + IdTransportadora + " AND SAIDADATA IS NULL ORDER BY SAIDADATA DESC ";// -- RECEBIDOS"
            }
            else
            {
                strsql = "SELECT IdEdiTrocaDeArquivo, IdOrigem,IdDestino,TipoDeArquivo,EntradaData, EntradaIdUsuario, SaidaData, SaidaIdUsuario, NomeDoArquivo FROM EDITROCADEARQUIVO WHERE IDORIGEM=" + IdTransportadora + " ORDER BY ENTRADADATA DESC ";// -- ENVIADOS
            }

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }

        public static byte[] RetornarArquivo(int IdEdiTrocaDeArquivo)
        {
            string strsql = "";
            strsql = "SELECT Arquivo FROM EDITROCADEARQUIVO WHERE IdEdiTrocaDeArquivo=" + IdEdiTrocaDeArquivo; 
            DataTable d =Sistran.Library.GetDataTables.RetornarDataTable(strsql);

            byte[] arquivo = null;
            if (d.Rows.Count > 0)
            {
                arquivo = (byte[])d.Rows[0][0];
            }
            return arquivo;
        }

        public static string RetornarNomeArquivo(int IdEdiTrocaDeArquivo)
        {
            string strsql = "";
            strsql = "  SELECT NOMEDOARQUIVO FROM EDITROCADEARQUIVO WHERE IDEDITROCADEARQUIVO=" + IdEdiTrocaDeArquivo;
            DataTable d = Sistran.Library.GetDataTables.RetornarDataTable(strsql);

            string arquivo ="";
            if (d.Rows.Count > 0)
            {
                arquivo = d.Rows[0]["NomeDoArquivo"].ToString();
            }
            return arquivo;
        }

        public static void BaixarArquivo( int idUsuario, int IDEDITROCADEARQUIVO)
        {
            string strsql = "UPDATE EDITROCADEARQUIVO SET  SAIDADATA=GETDATE(), SaidaIdUsuario=" + idUsuario + " WHERE IDEDITROCADEARQUIVO=" + IDEDITROCADEARQUIVO;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
        }
        public static void Incluir(byte[] arquivo, int IdOrigem, int idDestino, string nomeArquivo, string TipoDeArquivo, int idUsuario)
        {
            string strsql = "INSERT INTO EDITROCADEARQUIVO (IDEDITROCADEARQUIVO,";
            strsql += " IdOrigem, ";
            strsql += " IdDestino, ";
            strsql += " TipoDeArquivo, ";
            strsql += " EntradaData, ";
            strsql += " EntradaIdUsuario, ";
            strsql += " Arquivo, ";
            strsql += " NomeDoArquivo ";
            
            strsql += " ) VALUES (";

            string id = Sistran.Library.GetDataTables.RetornarIdTabela("EDITROCADEARQUIVO");
            strsql += id + " , ";
            strsql += IdOrigem + " , ";
            strsql += idDestino + " , ";
            strsql += " '"+TipoDeArquivo.ToUpper()+"', ";
            strsql += " getDate(), ";
            strsql += idUsuario + " , ";
            strsql += " @arquivo, ";
            strsql += " '" + nomeArquivo + "'";
            strsql += " )";

            SqlConnection cnn = new SqlConnection(HttpContext.Current.Session["ConnLogin"].ToString());
            SqlCommand cmm = new SqlCommand();

            cmm.CommandText = strsql;
            cmm.CommandType = CommandType.Text;
            cmm.Connection = cnn;


            SqlParameter par = new SqlParameter("@arquivo", arquivo);
            cmm.Parameters.Add(par);

            try
            {
                cnn.Open();
                cmm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

        }


       
    }
}


    