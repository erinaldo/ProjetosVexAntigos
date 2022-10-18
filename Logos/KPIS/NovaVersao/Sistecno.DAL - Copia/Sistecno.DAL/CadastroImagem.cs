using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL.BD;

namespace Sistecno.DAL
{
    public static class CadastroImagem
    {
        public static void GravarImagem(byte[] img, int idCadastro, string cnx)
        {

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();



            cn.ConnectionString = cnx;
            string ID = cDb.RetornarIDTabela(cnx, "CADASTROIMAGEM").ToString();

            try
            {
                cd.Connection = cn;
                cn.Open();
                string m = "";
                m += " INSERT INTO CADASTROIMAGEM(";
                m += " IDCADASTROIMAGEM,";
                m += " IDCADASTRO,";
                m += " IMAGEM,";
                m += " NOME";
                m += " ) VALUES";
                m += " (";


                m += ID + " ,";
                m += idCadastro + " ,";
                m += " @IMAGEM ,";
                m += " 'LOGO TIPO'";
                m += " )";

                cd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IMAGEM", img));
                cd.CommandText = m;
                cd.CommandType = CommandType.Text;
                cd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }

        public static DataTable RetornarImagemSite(int idCadastro, bool todas, string cnx)
        {
            try
            {
                string strsql = "SELECT TOP 1 IDCADASTROIMAGEM, IMAGEM FROM CADASTROIMAGEM WHERE " + (todas == true ? "0=0" : "TIPOIMAGEM='SITE'") + " AND IDCADASTRO=" + idCadastro;
                return cDb.RetornarDataTable(strsql, cnx);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
