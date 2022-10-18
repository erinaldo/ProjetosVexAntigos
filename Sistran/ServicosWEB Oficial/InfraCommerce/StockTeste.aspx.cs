using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicosWEB.wsInfraStock;
using System.Configuration;

namespace ServicosWEB.InfraCommerce
{

    public struct logs
    {
        public string datahora { get; set; }
        public string acao { get; set; }
        public string resposta { get; set; }
    }

    public partial class StockTeste : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //eviar quabdo houver baixa de estoque.
            //entrada e saida

            List<logs> l = new List<logs>();

            setStockRequest estR = new setStockRequest();
            var lista = new List<stock>();


            stock ex = new stock()
            {
                skuId = "109318",
                quantity = 20,
              //  stockTypeSourceName = "controlUnitQuantity"
            };
            lista.Add(ex);

            //l.Add(new logs()
            //{
            //    datahora = DateTime.Now.ToString(),
            //    acao = "sku: " + ex.skuId + ", Quantidade: " + ex.quantity + " ,  stockTypeSourceName: " + ex.stockTypeSourceName.ToString()
            //});


            ex = new stock()
                {
                    skuId = "109319",
                    quantity = 5/*,
                    stockTypeSourceName = "controlUnitQuantity"*/
                };
            //l.Add(new logs()
            //{
            //    datahora = DateTime.Now.ToString(),
            //    acao = "sku: " + ex.skuId + ", Quantidade: " + ex.quantity + " ,  stockTypeSourceName: " + ex.stockTypeSourceName.ToString()
            //});
            //lista.Add(ex);


            ex = new stock()
            {
                skuId = "109320",
                quantity = 15,
               // stockTypeSourceName = "controlUnitQuantity"
            };
            //l.Add(new logs()
            //{
            //    datahora = DateTime.Now.ToString(),
            //    acao = "sku: " + ex.skuId + ", Quantidade: " + ex.quantity + " ,  stockTypeSourceName: " + ex.stockTypeSourceName.ToString()
            //});
            //lista.Add(ex);



            ex = new stock()
            {
                skuId = "109321",
                quantity = 0,
                //stockTypeSourceName = "controlUnitQuantity"
            };
            //l.Add(new logs()
            //{
            //    datahora = DateTime.Now.ToString(),
            //    acao = "sku: " + ex.skuId + ", Quantidade: " + ex.quantity + " ,  stockTypeSourceName: " + ex.stockTypeSourceName.ToString()
            //});
            //lista.Add(ex);


            ex = new stock()
            {
                skuId = "109322",
                quantity = 0,
               // stockTypeSourceName = "controlUnitQuantity"
            };
            //l.Add(new logs()
            //{
            //    datahora = DateTime.Now.ToString(),
            //    acao = "sku: " + ex.skuId + ", Quantidade: " + ex.quantity + " ,  stockTypeSourceName: " + ex.stockTypeSourceName.ToString()
            //});
            //lista.Add(ex);

            ex = new stock()
            {
                skuId = "109312",
                quantity = 0,
               // stockTypeSourceName = "controlUnitQuantity"
            };
            //l.Add(new logs()
            //{
            //    datahora = DateTime.Now.ToString(),
            //    acao = "sku: " + ex.skuId + ", Quantidade: " + ex.quantity + " ,  stockTypeSourceName: " + ex.stockTypeSourceName.ToString()
            //});
            //lista.Add(ex);
            



            estR.storeId = "JOSAPAR";
            estR.stockList = lista.ToArray();
            StockServicesClient x = new StockServicesClient();

            x.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
            x.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
            var resposta = x.setStock(estR);

            string sqlLog = "";
            for (int i = 0; i < l.Count; i++)
            {
                sqlLog += "insert into logsInfracomence(DataHora, Acao, Status) values ('" + l[i].datahora + "', '" + l[i].acao + "', '" + (resposta == null ? "-" : resposta.result.ToString()) + "'); ";
                Response.Write("<BR>" + l[i].acao) ;
            }

            if (sqlLog.Length > 5)
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlLog, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


        }
    }
}


//ex = new stock()
//{
//    skuId = "109315",
//    stockType = 0,
//    quantity = 0
//};
//l.Add(new logs()
//{
//    datahora = DateTime.Now.ToString(),
//    acao = "sku: " + ex.skuId + " ,  stockType: " + ex.stockType.ToString()
//});


//lista.Add(ex);
// ex = new stock() { 
//     skuId = "109316", stockType = 0, quantity = 0 
// };
//lista.Add(ex);

//l.Add(new logs()
//{
//    datahora = DateTime.Now.ToString(),
//    acao = "sku: " + ex.skuId + " ,  stockType: " + ex.stockType.ToString()
//});
// ex = new stock() { 
//     skuId = "109317", stockType = 0, quantity = 0 
// };
//lista.Add(ex);

//l.Add(new logs()
//{
//    datahora = DateTime.Now.ToString(),
//    acao = "sku: " + ex.skuId + " ,  stockType: " + ex.stockType.ToString()
//});

// ex = new stock() { 
//     skuId = "109318", stockType = 0, quantity = 0 
// };
//lista.Add(ex);

//l.Add(new logs()
//{
//    datahora = DateTime.Now.ToString(),
//    acao = "sku: " + ex.skuId + " ,  stockType: " + ex.stockType.ToString()
//});

//ex = new stock()
//{
//    skuId = "109319",
//    stockType = 0,
//    quantity = 0
//};
//lista.Add(ex);

//l.Add(new logs()
//{
//    datahora = DateTime.Now.ToString(),
//    acao = "sku: " + ex.skuId + " ,  stockType: " + ex.stockType.ToString()
//});