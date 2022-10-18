using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace ServicosWEB.Josapar
{
    /// <summary>
    /// Summary description for wsJosapar
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsJosapar : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable Estoque(string login, string senha)
        {
            if(login =="" &&  senha == "")
                return null;

            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            
            try
            {
                string sql = "exec PRC_Saldo_Josapar";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public DataTable EstoqueValidadeDias(string login, string senha, int QuantidadeDias)
        {
            if (login == "" && senha == "")
                return null;

            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                string sql = "exec PRC_Saldo_Josapar_vencimento " + QuantidadeDias;
                return Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
