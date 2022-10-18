using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using ServicosWEB.Util;
using ServicosWEB.InfraTracking;
using System.Configuration;

namespace ServicosWEB.InfraCommerce
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "PaymentServicesBinding", Namespace = "http://www.accurate.com/acec/PaymentServices")]
    public class PaymentServices : System.Web.Services.WebService
    {
        List<logs> l = new List<logs>();

        public int idCliente = 3703114; // id da Josapar
        [WebMethod]

        //public confirmPaymentResponse confirmPayment(confirmPayment ConfPay)
        //{
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("confirmPayment", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("confirmPaymentResponse", Namespace = "http://www.accurate.com/acec/PaymentServices")]
        public confirmPaymentResponse confirmPayment([System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.accurate.com/acec/PaymentServices")] confirmPaymentRequest confirmPaymentRequest)
        {
           l =new List<logs>();

            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            try
            {
                //if (!Autenticou())
                //{
                //    throw new Exception("Erro: Usuário não autenticado.");
                //}


              

                string sql = "Select IdDocumento from Documento Where Numero='" + confirmPaymentRequest.orderId + "' and IdCliente=" + idCliente + " and IdFilial=15 and Ativo <> 'nao'";

                if (Validacao.contemLetras(confirmPaymentRequest.orderId))
                    sql = "Select IdDocumento from Documento Where NumeroOriginal='" + confirmPaymentRequest.orderId + "' and IdCliente=" + idCliente + " and IdFilial=15";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                if (dt.Rows.Count == 0)
                    throw new Exception("Pedido Não Encontrado");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (confirmPaymentRequest.status.ToUpper() == "AP")
                    {
                        sql = "Update Documento set Ativo='SIM' Where IdDocumento=" + dt.Rows[i][0].ToString() + "; Update DocumentoFilial set Situacao='LIBERADO PARA SEPARACAO' WHERE IDDOCUMENTO = " + dt.Rows[i][0].ToString() + "; select 1";

                        EnviarTrackingPagamento(true, confirmPaymentRequest.orderId.ToString());
                        EnviarTrackingSeparacao(true, confirmPaymentRequest.orderId.ToString());

                    }
                    else
                    {
                        sql = "Update Documento set Ativo='NAO' Where IdDocumento=" + dt.Rows[i][0].ToString() + "; Update DocumentoFilial set Situacao='NAO APROVADO' WHERE IDDOCUMENTO = " + dt.Rows[i][0].ToString() + "; select 1";
                        EnviarTrackingPagamento(false, confirmPaymentRequest.orderId);
                    }

                    Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                    l.Add(new logs()
                    {
                        datahora = DateTime.Now.ToString(),  acao = "Pedido: " + confirmPaymentRequest.orderId.ToString() + ". Aprovacao de Pagamento: " + confirmPaymentRequest.status.ToUpper()
                    });



                    string sqlLog = "";
                    for (int ix = 0; ix < l.Count; ix++)
                    {
                        sqlLog += "insert into logsInfracomence(DataHora, Acao, Status) values ('" + l[ix].datahora + "', '" + l[i].acao + "', ''); ";
                   }

                    if (sqlLog.Length > 5)
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlLog, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }

                return new confirmPaymentResponse() { success = true };
            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "Erro. confirmPaymentResponse", ex.Message, "mail.grupologos.com.br", "logos0902", "Erro. confirmPaymentResponse");
                return new confirmPaymentResponse() { success = false };
            }
        }

        private void EnviarTrackingPagamento(bool Pago, string Numero)
        {

            ServicosWEB.InfraTracking.Tracking tr = new InfraTracking.Tracking();
            ServicosWEB.InfraTracking.captureTrackingRequest request = new InfraTracking.captureTrackingRequest();

            tr.orderId = int.Parse(Numero);

            if (Pago)
            {
                tr.controlPointId = "MAP"; // pedido Pago 
                tr.controlPointNm = "Limite de Crédito aprovado";

            }
            else
            {
                tr.controlPointId = "MNA"; // pedido Reprovado 
                tr.controlPointNm = "Limite de Crédito Não aprovado";

            }
            //                tr.controlPointId = "WMS"; // Table  de tipos de Tracking
            //                tr.controlPointNm = "Separação e embalagem";
            tr.occurrenceDt = DateTime.Now;



            var ojData = new List<InfraTracking.ObjectData>();
            ojData.Add(new InfraTracking.ObjectData() { objectId = "1" });

            List<ServicosWEB.InfraTracking.SkuDeliveryTracking> dTrac = new List<ServicosWEB.InfraTracking.SkuDeliveryTracking>();

            tr.skuDeliveryTrackingList = dTrac.ToArray();


            var lista = new List<InfraTracking.Tracking>();
            lista.Add(tr);
            request.trackingList = lista.ToArray();

            InfraTracking.TrackingServicesClient serv = new InfraTracking.TrackingServicesClient();
            serv.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
            serv.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
            var resp = serv.captureTracking(request);

            l.Add(new logs()
            {
                datahora = DateTime.Now.ToString(),
                acao = "Pedido: " + Numero + ". Trackin Pagamento : " + (Pago==true?" SIM": " NAO")
            });
        }




        private void EnviarTrackingSeparacao(bool Pago, string Numero)
        {



            InfraTracking.Tracking tr = new InfraTracking.Tracking();
            InfraTracking.captureTrackingRequest request = new InfraTracking.captureTrackingRequest();

            tr.orderId = int.Parse(Numero);


            tr.controlPointId = "WMS"; // Table  de tipos de Tracking
            tr.controlPointNm = "Separação e embalagem";
            tr.occurrenceDt = DateTime.Now;



            var ojData = new List<InfraTracking.ObjectData>();
            ojData.Add(new InfraTracking.ObjectData() { objectId = "1" });

            List<ServicosWEB.InfraTracking.SkuDeliveryTracking> dTrac = new List<ServicosWEB.InfraTracking.SkuDeliveryTracking>();

            tr.skuDeliveryTrackingList = dTrac.ToArray();


            var lista = new List<InfraTracking.Tracking>();
            lista.Add(tr);
            request.trackingList = lista.ToArray();

            InfraTracking.TrackingServicesClient serv = new InfraTracking.TrackingServicesClient();
            serv.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
            serv.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
            var resp = serv.captureTracking(request);

            l.Add(new logs()
            {
                datahora = DateTime.Now.ToString(),
                acao = "Pedido: " + Numero + ". Trackin Pedido em separação"
            });
        }






        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
        [System.SerializableAttribute()]
        //[System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/PaymentServices")]
        public partial class confirmPaymentRequest
        {

            private string orderIdField;

            private string deliveryIdField;

            private string paymentIdField;

            private double amountField;

            private string statusField;

            private string updateRiskAnalysisField;

            private string descField;

            private string authCodeField;

            private string returnCodeField;

            private string partnerTransactionIdField;

            private string storeIdField;

            /// <remarks/>
            public string orderId
            {
                get
                {
                    return this.orderIdField;
                }
                set
                {
                    this.orderIdField = value;
                }
            }

            /// <remarks/>
            public string deliveryId
            {
                get
                {
                    return this.deliveryIdField;
                }
                set
                {
                    this.deliveryIdField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
            public string paymentId
            {
                get
                {
                    return this.paymentIdField;
                }
                set
                {
                    this.paymentIdField = value;
                }
            }

            /// <remarks/>
            public double amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }

            /// <remarks/>
            public string status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            public string updateRiskAnalysis
            {
                get
                {
                    return this.updateRiskAnalysisField;
                }
                set
                {
                    this.updateRiskAnalysisField = value;
                }
            }

            /// <remarks/>
            public string desc
            {
                get
                {
                    return this.descField;
                }
                set
                {
                    this.descField = value;
                }
            }

            /// <remarks/>
            public string authCode
            {
                get
                {
                    return this.authCodeField;
                }
                set
                {
                    this.authCodeField = value;
                }
            }

            /// <remarks/>
            public string returnCode
            {
                get
                {
                    return this.returnCodeField;
                }
                set
                {
                    this.returnCodeField = value;
                }
            }

            /// <remarks/>
            public string partnerTransactionId
            {
                get
                {
                    return this.partnerTransactionIdField;
                }
                set
                {
                    this.partnerTransactionIdField = value;
                }
            }

            /// <remarks/>
            public string storeId
            {
                get
                {
                    return this.storeIdField;
                }
                set
                {
                    this.storeIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
        [System.SerializableAttribute()]
        //[System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/PaymentServices")]
        public partial class confirmPaymentResponse
        {

            private bool successField;

            private string statusMessageField;

            /// <remarks/>
            public bool success
            {
                get
                {
                    return this.successField;
                }
                set
                {
                    this.successField = value;
                }
            }

            /// <remarks/>
            public string statusMessage
            {
                get
                {
                    return this.statusMessageField;
                }
                set
                {
                    this.statusMessageField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
        public delegate void confirmPaymentCompletedEventHandler(object sender, confirmPaymentCompletedEventArgs e);

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
        //[System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        public partial class confirmPaymentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
        {

            private object[] results;

            internal confirmPaymentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
            {
                this.results = results;
            }

            /// <remarks/>
            public confirmPaymentResponse Result
            {
                get
                {
                    this.RaiseExceptionIfNecessary();
                    return ((confirmPaymentResponse)(this.results[0]));
                }
            }
        }
    }
}