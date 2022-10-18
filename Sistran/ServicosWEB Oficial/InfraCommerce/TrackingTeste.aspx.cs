using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ServicosWEB.InfraTracking;
using System.Xml;

namespace ServicosWEB.InfraCommerce
{
    public partial class TrackingTeste : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument xmlSoapRequest = new XmlDocument();

            try
            {
                InfraTracking.Tracking tr = new InfraTracking.Tracking();
                InfraTracking.captureTrackingRequest request = new InfraTracking.captureTrackingRequest();

                tr.orderId = 1913484;
                tr.controlPointId = "MAP"; // pedido Pago 
                //                tr.controlPointId = "MNA"; // pedido Reprovado 

//                tr.controlPointId = "WMS"; // Table  de tipos de Tracking
                tr.controlPointNm = "Limite de Crédito aprovado";
//                tr.controlPointNm = "Separação e embalagem";
                tr.occurrenceDt = DateTime.Now;
                
                
                //InfraTracking.InvoiceInfo inv = new InfraTracking.InvoiceInfo();
                //inv.issuerDocumentNr =123654;
                //inv.invoiceNumber = 123654;
                //inv.invoiceSerialNumber = "13985401888";
                //inv.invoiceEmissionDate = DateTime.Now;
                //inv.invoiceEletronicKey = "0000000000000000000000000000000000000000000";

                var ojData = new List<InfraTracking.ObjectData>();
               
                ojData.Add(new InfraTracking.ObjectData(){ objectId="1"});

                //inv.objectDataList =ojData.ToArray();

              //  tr.invoiceInfo = inv;

                List<ServicosWEB.InfraTracking.SkuDeliveryTracking> dTrac = new List<ServicosWEB.InfraTracking.SkuDeliveryTracking>();       

                tr.skuDeliveryTrackingList = dTrac.ToArray() ;


                var lista = new List<InfraTracking.Tracking>();
                lista.Add(tr);
                request.trackingList = lista.ToArray();
               
                InfraTracking.TrackingServicesClient serv = new InfraTracking.TrackingServicesClient();
                serv.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
                serv.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
                var resp = serv.captureTracking(request);
            }
            catch (Exception ex)
            {
               // throw ex;
                Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "Erro. TrackingTeste", ex.Message, "mail.grupologos.com.br", "logos0902", "Erro. TrackingTeste");

            }
        }
    }
}