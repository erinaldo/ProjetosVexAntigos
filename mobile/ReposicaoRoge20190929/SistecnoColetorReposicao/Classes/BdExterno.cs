using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SistecnoColetor.Classes
{
    public class ParametrosProcedures
    {
        public string tipoDeDados { get; set; }
        public string nomePar { get; set; }
        public string valorPar { get; set; }
        public string direcao { get; set; }

    }

    public static class BdExterno
    {
        public static DataTable RetornarDataTableProcedure(string NomeProcedure, List<ParametrosProcedures> parametros, string Conx)
        {
            DataSet ds = new DataSet();

            System.Data.SqlClient.SqlConnection cn = new SqlConnection(VarGlobal.Conexao);
            System.Data.SqlClient.SqlCommand cd = new SqlCommand();

            cn.ConnectionString = VarGlobal.Conexao;
            cd.CommandText = NomeProcedure;
            cd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[parametros.Count];

            for (int i = 0; i < parametros.Count; i++)
            {
                System.Data.SqlClient.SqlParameter par = new System.Data.SqlClient.SqlParameter();
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
                cd.Parameters.Add(par);
            }


            cd.Connection = cn;
            cn.Open();
            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cd);
                da.Fill(ds);
                return ds.Tables[0];
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }          

        }

        public static int RetornarIDTabela(string sTabela)
        {
            int ret = 0;


            System.Data.SqlClient.SqlConnection cn = new SqlConnection(VarGlobal.Conexao);
            System.Data.SqlClient.SqlCommand cd = new SqlCommand();
            
            cn.ConnectionString = VarGlobal.Conexao;
            cd.CommandText = "GERAR_ID_TABELA";
            cd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter();
            p1.ParameterName = "@NOMEDATABELA";
            p1.DbType = DbType.String;
            p1.Value = sTabela;

            cd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter(); ;
            p2.ParameterName = "@RETORNAR_ID_TABELA";
            p2.DbType = DbType.Int32;
            p2.Value = "0";
            p2.Direction = ParameterDirection.Output;

            cd.Parameters.Add(p2);


            cd.Connection = cn;
            cn.Open();
            try
            {
                cd.ExecuteNonQuery();
                ret = int.Parse(cd.Parameters["@RETORNAR_ID_TABELA"].Value.ToString());
            }
            catch(Exception)
            {               
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
        /// Retona um dataTable correnspondente a um sql
        /// </summary>
        /// <param name="isql"></param>
        /// <param name="cnx"></param>
        /// <returns></returns>
        public static DataTable RetornarDT(string isql, string cnx)
        {
            //cnx = cnx.Replace("Initial Catalog", "Database").Replace(",1433", "") ;

            System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection();
            System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand();

            myConn.ConnectionString = cnx;
            myCommand.Connection = myConn;
            myCommand.CommandText =isql;
            myCommand.CommandType = CommandType.Text;



            DataTable dt = new DataTable();
            
            try
            {
                myConn.Open();
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(myCommand);
                da.Fill(dt);
                return dt;
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
        

        public static void Executar(string isql, string cnx)
        {
            System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection(cnx);
            System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(isql, myConn);
            myCommand.CommandType = CommandType.Text;            
            try
            {

                myConn.Open();
                myCommand.ExecuteNonQuery();

            
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();

            }
        }


        public static string ExecutarComRetorno(string isql, string cnx)
        {
            System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection(cnx);
            System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(isql, myConn);
            myCommand.CommandType = CommandType.Text;
            string ret = "";
            try
            {

                myConn.Open();
                ret = myCommand.ExecuteScalar().ToString();       

                return ret;

            }
            catch (System.Exception)
            {
                //throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
                return "0";
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();

            }
        }
    }
    public static class Combo
    {
        public static void CarregarCombo(DataTable fonteDeDados, ref ComboBox dp, Boolean InserirSelecione, string CampoValue, string CampoText)
        {
            dp.Items.Clear();
            

            for (int i = 0; i < fonteDeDados.Rows.Count; i++)
            {
                dp.Items.Add(fonteDeDados.Rows[i][CampoValue] + "-" + fonteDeDados.Rows[i][CampoText]);
            }
        }
    }
}    
