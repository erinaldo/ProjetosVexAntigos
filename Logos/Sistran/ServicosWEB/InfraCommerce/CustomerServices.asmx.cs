using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ServicosWEB.Util;


namespace ServicosWEB.InfraCommerce
{
  
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class CustomerServices : System.Web.Services.WebService
    {
            // CustomerServices.CustomerServicesSoapClient ws = new CustomerServices.CustomerServicesSoapClient();
 
            //using (new OperationContextScope(ws.InnerChannel))
            //{
            //    HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
            //    requestMessage.Headers["Authorization"] = "Basic VE9LRU46dGVzdGU=";
            //    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
 
            //    CustomerServices.customer CLI = new CustomerServices.customer();
            //    CLI.name = "tstestestete";
 
            //    CustomerServices.notifyCustomerCreationResponse r = ws.notifyCustomerCreation(CLI);
            //}

        //[WebMethod]
        //public notifyCustomerCreationResponse notifyCustomerCreation(Customer custumer)
        //{
        //    string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
        //    notifyCustomerCreationResponse notifyCustomerCreationResponse = new notifyCustomerCreationResponse();

        //    try
        //    {
        //        if (custumer.documentNr == null || custumer.documentNr == "")
        //            throw new Exception("documentNr  obrigatorio");


        //        if (custumer.name == null || custumer.name == "")
        //            throw new Exception("name  obrigatorio");

        //        if (custumer.email == null || custumer.email == "")
        //            throw new Exception("email   obrigatorio");


        //        string sql = "Select IdCadastro from Cadastro where CNPJCPF = '" + Validacao.FormatarCnpj(custumer.documentNr) + "'";
        //        int IdDestinatario = Sistran.Library.GetDataTables.ExecutarRetornoID_WIN(sql, cnx);

        //        if (IdDestinatario == 0)
        //        {
        //            IdDestinatario = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("Cadastro", cnx));

        //            string sins = "Insert into Cadastro (IdCadastro, CnpjCpf, RazaoSocialNome, DataDeCadastro";
        //            string sval = "Values(" + IdDestinatario + ", '" + custumer.documentNr + "', '" + custumer.name.ToUpper().Replace("'", " ") + "', getDate() ";


        //            if (custumer.stateSubscription != null && custumer.stateSubscription != "")
        //            {
        //                sins += ", InscricaoRG";
        //                sval += ", '" + custumer.stateSubscription + "'";
        //            }

        //            if (custumer.addressList.Count > 0)
        //            {
        //                sins += ", Endereco, Numero, Complemento, Cep ";
        //                sval += ", '" + custumer.addressList[0].address.ToUpper().Replace("'", " ") + "', '" + custumer.addressList[0].addressNr.ToUpper().Replace("'", " ") + "', '" + custumer.addressList[0].additionalInfo.ToUpper().Replace("'", " ") + "', '" + custumer.addressList[0].postalCd.ToUpper().Replace("'", " ").Replace("-", "") + "' ";


        //                int idCidade = CidadeBairro.RetornarCidade(custumer.addressList[0].postalCd.ToUpper().Replace("'", " ").Replace("-", ""), cnx);

        //                if (idCidade > 0)
        //                {
        //                    sins += ", IdCidade = ";
        //                    sval += "," + idCidade.ToString();
        //                }

        //                int idBairro = CidadeBairro.RetornarBairro(custumer.addressList[0].quarter.ToUpper().Replace("'", " "), idCidade.ToString(), cnx);

        //                if (idBairro > 0)
        //                {
        //                    sins += ", IdBairro";
        //                    sval += "," + idBairro.ToString();
        //                }
        //            }
        //            sins += ")";
        //            sval += ")";

        //            Sistran.Library.GetDataTables.ExecutarRetornoID_WIN(sins + sval, cnx);

        //        }
        //        else
        //        {
        //            sql = "Update Documento set ";

        //            if (custumer.addressList.Count > 0)
        //            {
        //                sql += ", Endereco='" + custumer.addressList[0].address.ToUpper().Replace("'", " ") + "', Numero= '" + custumer.addressList[0].addressNr.ToUpper().Replace("'", " ") + "', Complemento='" + custumer.addressList[0].additionalInfo.ToUpper().Replace("'", " ") + "', Cep='" + custumer.addressList[0].postalCd.ToUpper().Replace("'", " ").Replace("-", "") + "' ";
        //                int idCidade = CidadeBairro.RetornarCidade(custumer.addressList[0].postalCd.ToUpper().Replace("'", " ").Replace("-", ""), cnx);

        //                if (idCidade > 0)
        //                    sql += ", IdCidade = " + idCidade.ToString();
                     
        //                int idBairro = CidadeBairro.RetornarBairro(custumer.addressList[0].quarter.ToUpper().Replace("'", " "), idCidade.ToString(), cnx);

        //                if (idBairro > 0)
        //                    sql += ", IdBairro=" + idBairro.ToString();
                      
        //            }


        //            sql += " Where IdCadastro=" + IdDestinatario;
        //            Sistran.Library.GetDataTables.ExecutarRetornoID_WIN(sql, cnx);

        //        }


        //        notifyCustomerCreationResponse.status = "PEN";
        //        return notifyCustomerCreationResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        notifyCustomerCreationResponse.status = ex.Message;
        //        return notifyCustomerCreationResponse;

        //    }
        //}

        //[WebMethod]
        //public string updateCustomer()
        //{
        //    return "Hello World";
        //}
    }
}
