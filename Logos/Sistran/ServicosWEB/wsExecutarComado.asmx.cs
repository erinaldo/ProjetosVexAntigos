using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace ServicosWEB
{
    /// <summary>
    /// Summary description for wsExecutarComado
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsExecutarComado : System.Web.Services.WebService
    {

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
    }
}
