using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.InfraCommerce
{
    /// <summary>
    /// Summary description for StockServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StockServices : System.Web.Services.WebService
    {
        public int idCliente = 3703114; // id da Josapar

        [WebMethod]
        public setStockResponse setStock(List<Stock> stockList)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                return new setStockResponse(){result="Sucesso"};
            }
            catch (Exception ex)
            {
                return new setStockResponse() { result=(ex.Message.Length>4000? ex.Message.Substring(0,4000): ex.Message)};
            }
        }
    }
}
