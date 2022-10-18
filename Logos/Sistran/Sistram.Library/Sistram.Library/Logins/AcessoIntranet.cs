using System;
using Sistran.Library;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sistran.Logins
{
    public class AcessoIntranet
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


        public string GetStringNoAccents(string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };


            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }


            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°", "'" };


            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }


            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por  " " **/
            str = str.Replace("\\s+", " ");
            return str;
        }



        public string ConexaoPorUsuario(string usuario, string senha)
        {

            IntranetSettings x = new IntranetSettings();
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
                    string strsql = "SELECT LOGIN FROM USUARIO WHERE UPPER(LOGIN)='" + GetStringNoAccents(usuario.ToUpper()) + "' AND  UPPER(SENHA) ='" + GetStringNoAccents(senha.ToUpper()) + "' AND (TipoDeSistema='WIN') AND ATIVO='SIM'";

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




