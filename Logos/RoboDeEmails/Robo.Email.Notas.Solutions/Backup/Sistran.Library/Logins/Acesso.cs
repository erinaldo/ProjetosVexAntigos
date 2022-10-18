using System;
using Sistran.Library;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sistran.Logins
{
    public class Acesso
    {
        public string ConexaoPorNomeBase(string Nome)
        {
            Settings1 x = new Settings1();
            string strConnection = "";
            for (int i = 0; i < x.Properties.Count; i++)
            {
                //pega a conexao
                strConnection = x.Properties["Conn" + (i + 1).ToString()].DefaultValue.ToString().ToLower();

                if (strConnection.ToLower().Contains(("Initial Catalog=" + Nome + ";").ToLower()) == true)
                {
                    HttpContext.Current.Session["ConnLogin"] = strConnection;
                    HttpContext.Current.Session["ConexaoCliete"] = "Conn" + (i + 1).ToString();
                    return strConnection;
                }
            }

            return strConnection;
        }

        public string ConexaoPorUsuario(string usuario, string senha)
        {
            
            Settings1 x = new Settings1();
            bool achou = false;
            string strConnection = "";

            for (int i = 0; i < x.Properties.Count; i++)
            {
                if (achou == true)
                    return strConnection;

                //pega a conexao
                strConnection = x.Properties["Conn" + (i + 1).ToString()].DefaultValue.ToString();

                //verifica se a conexao esta ativa 
                //Caso ocorra erro vai para a proxima select
                try
                {
                    SqlConnection cnn = new SqlConnection(strConnection);
                    cnn.Open();
                    //Seleciona o usuario e verifica se ele tem acesso ao .net
                    string strsql = "Select Login from Usuario where /*TipoDeSistema='WEB' and */ UPPER(Login)='" + usuario.ToUpper() + "' and  UPPER(Senha) ='" + senha.ToUpper() + "' AND SITE='ASP'";

                    SqlCommand cmm = new SqlCommand(strsql, cnn);
                    cmm.CommandType = CommandType.Text;

                    SqlDataReader dr = cmm.ExecuteReader();

                    while (dr.Read())
                    {
                        achou = true;
                        HttpContext.Current.Session["ConexaoCliete"] = "Conn" + (i + 1).ToString();
                    }
                    dr.Close();
                    cnn.Close();
                    dr.Dispose();
                    cnn.Dispose();
                }
                catch (Exception)
                {

                }

            }

            if (achou == true)
            {
                return strConnection;
            }
            else
            {
                return "";
            }
        }
    }
}