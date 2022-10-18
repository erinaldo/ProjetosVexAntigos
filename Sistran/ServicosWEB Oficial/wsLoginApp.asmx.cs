using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Configuration;

namespace ServicosWEB
{
    /// <summary>
    /// Summary description for wsLoginApp
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsLoginApp : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable Logar(string Login, string Senha)
        {
            try
            {
                string sql = "SELECT * FROM USUARIO WHERE LOGIN='" + Login + "' AND SENHA='" + Senha + "' AND ATIVO='SIM' ";
                DataTable ret = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (ret.Rows.Count == 0)
                    ret = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, ConfigurationSettings.AppSettings["cnxHomeRefill"].ToString());

                return ret;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public DataTable ExecSql(string Usuario, string Senha, string Comando)
        {
            try
            {
                if (Usuario == "SISTECNO" && Senha == "@ONCETSIS12122014")
                {
                    string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                    return Sistran.Library.GetDataTables.RetornarDataTableWS(Comando, cnx);
                }
                else
                    throw new Exception("Autenticação Falhou.");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod]
        public DataTable ExecSqlHR(string Usuario, string Senha, string Comando)
        {
            try
            {
                if (Usuario == "SISTECNO" && Senha == "@ONCETSIS12122014")
                {
                    string cnx = ConfigurationSettings.AppSettings["cnxHomeRefill"].ToString();
                    return Sistran.Library.GetDataTables.RetornarDataTableWS(Comando, cnx);
                }
                else
                    throw new Exception("Autenticação Falhou.");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}