using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using ServicosWEB.Util;
using System.Text;
using Sistecno;

namespace ServicosWEB.InfraCommerce
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "BOOrderIntegrationBinding", Namespace = "http://www.accurate.com/acec/AcecBOSOAIntegration/BOOrderIntegration")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(promotion[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(loyaltyCredit[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(orderLineProperty[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(orderLine[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(trackingProperty[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(tracking[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(skuDeliveryTracking[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(deliveryProperty[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(orderProperty[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(customerLoyalty[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(paymentProperty[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(payment[]))]

    public class OrderServices : System.Web.Services.WebService
    {
        public int idCliente = 3975526; // id da Josapar
        string cnx = "";

        [WebMethod]
        public void teste()
        { }

        [WebMethod]
        [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\ol014\\PedidoJosapar.log")]

        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("integrateOrder", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("integrateOrderResponse", Namespace = "http://www.accurate.com/acec/AcecBOSOAIntegration/BOOrderIntegration")]
        public integrateOrderResponse integrateOrder([System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.accurate.com/acec/AcecBOSOAIntegration/BOOrderIntegration")] integrateOrderRequest integrateOrderRequest)
        {
            string steste = "";
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string ped = "";

            var pedido = integrateOrderRequest.order;
            try
            {


                ped = pedido.orderId;

                if (pedido == null)
                    throw new Exception("Objeto de Pedido Vazio");

                string sql = "Select IdDocumento from Documento  with (nolock) Where Numero='" + pedido.orderId + "' and Serie='PED' and IdCliente=3975526 and Ativo='SIM' And IdFilial=15 ";

                if (Validacao.contemLetras(pedido.orderId))
                    sql = "Select IdDocumento from Documento  with (nolock) Where NumeroOriginal='" + pedido.orderId + "' and Serie='PED' and IdCliente=3975526 and Ativo='SIM'  And IdFilial=15";



                if (Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx).Rows.Count > 0)
                    throw new Exception("Pedido Já Existe na Base de Dados.");

                string iddoc = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTO", cnx);

                if (Validacao.contemLetras(pedido.orderId))
                {
                    sql = " INSERT INTO DOCUMENTO (IDDOCUMENTO, IDCLIENTE, IDREMETENTE, IDFILIAL, IDFILIALATUAL,  NUMERO, SERIE, TIPODEDOCUMENTO, IDDESTINATARIO,  ORIGEM, DATADEEMISSAO, DATADEENTRADA, ENDERECO, ENDERECONUMERO, ENDERECOCOMPLEMENTO, IDENDERECOBAIRRO, IDENDERECOCIDADE, ENDERECOCEP, ANOMES, DATADECANCELAMENTO, ENTRADASAIDA, ATIVO, TIPODESERVICO, ValorDeDesconto, ValorDasMercadorias, NumeroOriginal,AddressId, Payment, IDTes, IDTesCFOP) VALUES";
                    //                    sql = " INSERT INTO DOCUMENTOPEDIDO (IDDOCUMENTO, IDCLIENTE, IDREMETENTE, IDFILIAL, IDFILIALATUAL,  NUMERO, SERIE, TIPODEDOCUMENTO, IDDESTINATARIO,  ORIGEM, DATADEEMISSAO, DATADEENTRADA, ENDERECO, ENDERECONUMERO, ENDERECOCOMPLEMENTO, IDENDERECOBAIRRO, IDENDERECOCIDADE, ENDERECOCEP, ANOMES, DATADECANCELAMENTO, ENTRADASAIDA, ATIVO, TIPODESERVICO, ValorDeDesconto, ValorDasMercadorias, NumeroOriginal) VALUES";
                    sql += " (" + iddoc + ", " + idCliente + ", " + idCliente + ", 15, 15,  '" + iddoc + "', 'PED', 'PEDIDO', @IDDESTINATARIO@,  'WEBSERVICE', '@DATADEEMISSAO@', GETDATE(), '@ENDERECO@', '@ENDERECONUMERO@', '@ENDERECOCOMPLEMENTO@', @IDENDERECOBAIRRO@, @IDENDERECOCIDADE@, '@ENDERECOCEP@', '@ANOMES@', NULL, 'SAIDA', 'SIM', 'NORMAL' , @VALORDEDESCONTO@, @VALORDASMERCADORIAS@, '" + pedido.orderId + "',  '@ADDRESSID@', '@PAYMENT@', 45, 4882 )";
                }
                else
                {
                    sql = " INSERT INTO DOCUMENTO (IDDOCUMENTO, IDCLIENTE, IDREMETENTE, IDFILIAL, IDFILIALATUAL,  NUMERO, SERIE, TIPODEDOCUMENTO, IDDESTINATARIO,  ORIGEM, DATADEEMISSAO, DATADEENTRADA, ENDERECO, ENDERECONUMERO, ENDERECOCOMPLEMENTO, IDENDERECOBAIRRO, IDENDERECOCIDADE, ENDERECOCEP, ANOMES, DATADECANCELAMENTO, ENTRADASAIDA, ATIVO, TIPODESERVICO,  ValorDasMercadorias, ValorDeDesconto, AddressId, Payment, IDTes, IDTesCFOP) VALUES";
                    sql += " (" + iddoc + ", " + idCliente + ", " + idCliente + ", 15, 15,  '@NUMERO@', 'PED', 'PEDIDO', @IDDESTINATARIO@,  'WEBSERVICE', '@DATADEEMISSAO@', GETDATE(), '@ENDERECO@', '@ENDERECONUMERO@', '@ENDERECOCOMPLEMENTO@', @IDENDERECOBAIRRO@, @IDENDERECOCIDADE@, '@ENDERECOCEP@', '@ANOMES@', NULL, 'SAIDA', 'SIM', 'NORMAL' , @VALORDASMERCADORIAS@, @VALORDEDESCONTO@, '@ADDRESSID@', '@PAYMENT@', 45, 4882 )";
                }


                #region Pedido

                sql = sql.Replace("@NUMERO@", pedido.orderId);
                sql = sql.Replace("@DATADEEMISSAO@", pedido.purchaseDate.ToString("yyyy-MM-dd HH:mm:ss"));
                sql = sql.Replace("@ANOMES@", pedido.purchaseDate.ToString("yyyy/MM"));
                sql = sql.Replace("@VALORDEDESCONTO@", "0" /*pedido.totalDiscountAmount.ToString().Replace(",", ".")*/);

                decimal tot = pedido.totalAmount - pedido.totalDiscountAmount;

                sql = sql.Replace("@VALORDASMERCADORIAS@", tot.ToString().Replace(",", "."));
                sql = sql.Replace("@ADDRESSID@", pedido.deliveries[0].deliveryAddressId);
                //sql = sql.Replace("@PAYMENT@", pedido.paymentList[0].Item.paymentType);

                string ac = "";
                for (int ix = 0; ix < pedido.paymentList.Length; ix++)
                {
                    ac += pedido.paymentList[ix].Item.paymentType + "|" + pedido.paymentList[ix].Item.value + "?";
                }

                sql = sql.Replace("@PAYMENT@", ac.Substring(0, ac.Length - 1));

                #endregion

                #region Destinatario
                string saux = "";
                DataTable DtCliente = CadastrarDestinatario(pedido);

                if (pedido.billingAddress.additionalInfo == null)
                    pedido.billingAddress.additionalInfo = "";
                else
                    pedido.billingAddress.additionalInfo = pedido.billingAddress.additionalInfo.ToUpper().Replace("'", " ");


                if (pedido.billingAddress.additionalInfo.Length > 60)
                    pedido.billingAddress.additionalInfo = pedido.billingAddress.additionalInfo.Substring(0, 58);

                sql = sql.Replace("@IDDESTINATARIO@", DtCliente.Rows[0]["IdCadastro"].ToString());
                sql = sql.Replace("@ENDERECO@", pedido.deliveries[0].deliveryAddress.address1.Replace("'", ""));
                sql = sql.Replace("@ENDERECONUMERO@", pedido.deliveries[0].deliveryAddress.addressNr);
                sql = sql.Replace("@ENDERECOCOMPLEMENTO@", (pedido.deliveries[0].deliveryAddress.additionalInfo != null ? pedido.deliveries[0].deliveryAddress.additionalInfo.Replace("'", "") : ""));
                sql = sql.Replace("@ENDERECOCEP@", pedido.deliveries[0].deliveryAddress.postalCd.Replace("-", ""));

                int idCidade = CidadeBairro.RetornarCidade(pedido.deliveries[0].deliveryAddress.postalCd.Replace("-", "").Replace("'", " ").Replace("-", ""), cnx);
                int idBairro = CidadeBairro.RetornarBairro(pedido.deliveries[0].deliveryAddress.quarter.ToUpper().Replace("'", " "), idCidade.ToString(), cnx);

                if (idCidade == 0)
                {
                    try
                    {
                        Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "Erro. integrateOrder", "Cidade do Pedido : " + pedido.orderId + " Nao cadsastrado<br>" + steste, "mail.grupologos.com.br", "logos0902", "Erro. integrateOrder");
                    }
                    catch (Exception)
                    { }

                }

                sql = sql.Replace("@IDENDERECOBAIRRO@", idBairro.ToString());
                sql = sql.Replace("@IDENDERECOCIDADE@", idCidade.ToString());

                #endregion

                #region Itens
                string idDocItem = "";
                string sqlAux = "";
                for (int i = 0; i < pedido.deliveries[0].orderLineList.Length; i++)
                {
                    string IdProdutoEmbalagem = "";
                    string IdprodCli = "";
                    ProcurarProduto(pedido.deliveries[0].orderLineList[i], ref IdProdutoEmbalagem, ref IdprodCli);
                    idDocItem = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOITEM", cnx);
                    decimal vtot = pedido.deliveries[0].orderLineList[i].quantity * pedido.deliveries[0].orderLineList[i].salePrice;
                    string vl = "0";

                    if (pedido.deliveries[0].orderLineList[i].conditionalDiscountAmount == null)
                        vl = "0";
                    else
                        vl = pedido.deliveries[0].orderLineList[i].conditionalDiscountAmount.ToString().Replace(",", ".");

                    if (vl == "")
                        vl = "0";

                    sqlAux += "; INSERT INTO DOCUMENTOITEM (IDDOCUMENTOITEM, IDDOCUMENTO,IDPRODUTOEMBALAGEM, IDUSUARIO, QUANTIDADE,VALORUNITARIO,VALORTOTALDOITEM,IDPRODUTOCLIENTE, QUANTIDADEUNIDADEESTOQUE, SALDO, ValorDoDesconto, SkuType, CatalogListPrice, ListPrice, RoundingDiscountAmount, Service, productBrandName, IDCfop, IDTes, IDTesCfop, EstoqueProcessado) ";
                    sqlAux += " values(" + idDocItem + ", " + iddoc + "," + IdProdutoEmbalagem + ",2, " + pedido.deliveries[0].orderLineList[i].quantity.ToString().Replace(",", ".") + "," + pedido.deliveries[0].orderLineList[i].salePrice.ToString().Replace(",", ".") + ", " + vtot.ToString().Replace(",", ".") + "," + IdprodCli + ", " + pedido.deliveries[0].orderLineList[i].quantity.ToString().Replace(",", ".") + ", 0 , " + vl + ", '" + pedido.deliveries[0].orderLineList[i].skuType + "', " + pedido.deliveries[0].orderLineList[i].catalogListPrice.ToString().Replace(",", ".") + ", " + pedido.deliveries[0].orderLineList[i].listPrice.ToString().Replace(",", ".") + ", " + pedido.deliveries[0].orderLineList[i].roundingDiscountAmount.ToString().Replace(",", ".") + ", '" + pedido.deliveries[0].orderLineList[i].service + "', '" + pedido.deliveries[0].orderLineList[i].productBrandName + "', 218,45,4882, 'SIM')";


                    sqlAux += "; INSERT INTO DocumentoItemFrete(IdDocumentoItem,ChargedAmount,ActualAmount,FreightRoundingAmount,FreightTime,PickupLeadTime,LogisticContract ) Values ";
                    sqlAux += " (" + idDocItem + "," + pedido.deliveries[0].orderLineList[i].freight.chargedAmount.ToString().Replace(",", ".") + "," + pedido.deliveries[0].orderLineList[i].freight.actualAmount.ToString().Replace(",", ".") + ",0," + pedido.deliveries[0].orderLineList[i].freight.freightTime + "," + pedido.deliveries[0].orderLineList[i].freight.pickupLeadTime + ",'NORMAL' )";


                }

                #endregion


                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOFILIAL", cnx);

                sqlAux += " INSERT INTO DOCUMENTOFILIAL (IDDOCUMENTOFILIAL, IDDOCUMENTO, IDFILIAL, SITUACAO, DATA, IDRegiaoItem) VALUES (" + ID + ", " + iddoc + ", 15, 'AGUARDANDO PAGAMENTO', GETDATE(), 0) ";

                steste = sql + sqlAux;

                ////////////////////////////Verifica se ja existe o pedido
                string xsql = "Select IdDocumento from Documento  with (nolock) Where Numero='" + pedido.orderId + "' and Serie='PED' and IdCliente=3975526 and Ativo='SIM' And IdFilial=15 ";

                if (Validacao.contemLetras(pedido.orderId))
                    xsql = "Select IdDocumento from Documento  with (nolock) Where NumeroOriginal='" + pedido.orderId + "' and Serie='PED' and IdCliente=3975526 and Ativo='SIM'  And IdFilial=15";

                if (Sistran.Library.GetDataTables.RetornarDataTableWin(xsql, cnx).Rows.Count > 0)
                {
                    Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "Pedido Duplicado - integrateOrder", "Pedido Recebido Com Sucesso" + steste, "mail.grupologos.com.br", "logos0902", "integrateOrder");
                    throw new Exception("Pedido Já Existe na Base de Dados.");
                }
                /////////////////////////////////////////////////////////////////////////////////

                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql + sqlAux, cnx);

                try
                {         


                    Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "OK - integrateOrder", "Pedido Recebido Com Sucesso" + steste, "mail.grupologos.com.br", "logos0902", "integrateOrder");
                }
                catch (Exception)
                { }


                try
                {
                    string sqlLog = "insert into logsInfracomence(DataHora, Acao, Status) values (getdate(),'Pedido " + pedido.orderId + "', 'Recebido com Sucesso'); ";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlLog, cnx);
                }
                catch (Exception)
                {

                }




                return new integrateOrderResponse() { status = "OK", message = "Pedido Recebido Com Sucesso." };
            }
            catch (Exception ex)
            {
                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "Erro. integrateOrder", ex.Message + "<br>" + steste + " - " + ped, "mail.grupologos.com.br", "logos0902", "Erro. integrateOrder");
                }
                catch (Exception)
                { }

                try
                {
                    string sqlLog = "insert into logsInfracomence(DataHora, Acao, Status) values (getdate(),'Pedido " + pedido.orderId + "', 'Erro ao receber o pedido. " + ex.Message + "'); ";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlLog, cnx);
                }
                catch (Exception)
                {

                }

                if (ex.Message.Contains("Timeout expired"))
                    return new integrateOrderResponse() { status = "", message = "Não foi possivel receber este pedido. Tente novamente" };
                else if (ex.Message.Contains("Pedido Já Existe na Base de Dados."))
                {
                    return new integrateOrderResponse() { status = "OK", message = (ex.Message.Length > 4000 ? ex.Message.Substring(0, 3999) : ex.Message) };
                }
                else
                    return new integrateOrderResponse() { status = "", message = "Não foi possivel receber este pedido. Tente novamente" };
            }
        }

        private DataTable CadastrarDestinatario(order ped)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                if (ped.customer.documentNumber == null || ped.customer.documentNumber == "")
                    throw new Exception("documentNr  obrigatorio");


                if (ped.customer.name == null || ped.customer.name == "")
                    throw new Exception("name  obrigatorio");

                if (ped.customer.email == null || ped.customer.email == "")
                    throw new Exception("email   obrigatorio");

                //4206050000180

                if (ped.customer.documentNumber.Length == 10)
                    ped.customer.documentNumber = "0000" + ped.customer.documentNumber;

                if (ped.customer.documentNumber.Length == 11)
                    ped.customer.documentNumber = "000" + ped.customer.documentNumber;

                if (ped.customer.documentNumber.Length == 12)
                    ped.customer.documentNumber = "00" + ped.customer.documentNumber;

                if (ped.customer.documentNumber.Length == 13)
                    ped.customer.documentNumber = "0" + ped.customer.documentNumber;


                string sql = "Select IdCadastro from Cadastro  with (nolock)  where CNPJCPF = '" + Validacao.FormatarCnpj(ped.customer.documentNumber) + "'";
                int IdDestinatario = Sistran.Library.GetDataTables.ExecutarRetornoID_WIN(sql, cnx);

                if (IdDestinatario == 0)
                {
                    IdDestinatario = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("Cadastro", cnx));

                    string sins = "Insert into Cadastro (IdCadastro, CnpjCpf, RazaoSocialNome, FantasiaApelido,  DataDeCadastro, customerId, addressId";
                    string sval = "Values(" + IdDestinatario + ", '" + Validacao.FormatarCnpj(ped.customer.documentNumber) + "', '" + (ped.customer.name.ToUpper().Replace("'", " ").Length > 60 ? ped.customer.name.ToUpper().Replace("'", " ").Substring(0, 59) : ped.customer.name.ToUpper().Replace("'", " ")) + "', '" + (ped.customer.name.ToUpper().Replace("'", " ").Length > 30 ? ped.customer.name.ToUpper().Replace("'", " ").Substring(0, 29) : ped.customer.name.ToUpper().Replace("'", " ")) + "', getDate(),  '" + ped.customer.customerId + "', '" + ped.billingAddress.addressId + "'";


                    if (ped.customer.state_subscription != null && ped.customer.state_subscription != "")
                    {
                        sins += ", InscricaoRG";
                        sval += ", '" + ped.customer.state_subscription + "'";
                    }
                    else
                    {
                        sins += ", InscricaoRG";
                        sval += ", 'ISENTO'";
                    }


                    //if (ped.billingAddress.address1.Length > 0)
                    //{

                    if(ped.billingAddress.additionalInfo == null)
                        ped.billingAddress.additionalInfo="";
                    else
                         ped.billingAddress.additionalInfo=ped.billingAddress.additionalInfo.ToUpper().Replace("'", " ");


                    if (ped.billingAddress.additionalInfo.Length > 60)
                        ped.billingAddress.additionalInfo = ped.billingAddress.additionalInfo.Substring(0, 58);

                    //(ped.billingAddress.additionalInfo == null ? "" : ped.billingAddress.additionalInfo.ToUpper().Replace("'", " "))

                    sins += ", Endereco, Numero, Complemento, Cep ";
                    sval += ", '" + (ped.billingAddress.address1.ToUpper().Replace("'", " ") == "" ? ped.deliveries[0].deliveryAddress.address1.Replace("'", "") : ped.billingAddress.address1.ToUpper().Replace("'", " ")) + "', '" + ped.billingAddress.addressNr.ToUpper().Replace("'", " ") + "', '" + (ped.billingAddress.additionalInfo == null ? "" : ped.billingAddress.additionalInfo.ToUpper().Replace("'", " ")) + "', '" + ped.billingAddress.postalCd.ToUpper().Replace("'", " ").Replace("-", "") + "' ";


                    int idCidade = CidadeBairro.RetornarCidade(ped.billingAddress.postalCd.ToUpper().Replace("'", " ").Replace("-", ""), cnx);

                    if (idCidade > 0)
                    {
                        sins += ", IdCidade ";
                        sval += "," + idCidade.ToString();
                    }

                    int idBairro = CidadeBairro.RetornarBairro(ped.billingAddress.quarter.ToUpper().Replace("'", " "), idCidade.ToString(), cnx);

                    if (idBairro > 0)
                    {
                        sins += ", IdBairro";
                        sval += "," + idBairro.ToString();
                    }
                    //}
                    sins += ")";
                    sval += ")";

                    //1	E-MAIL
                    //2	TELEFONE COMERCIAL
                    //3	TELEFONE RESIDENCIAL

                    if (ped.customer.email != null && ped.customer.email != "")
                    {
                        int Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CadastroContatoEndereco", cnx));
                        sval += "; INSERT INTO CadastroContatoEndereco (IDCadastroContatoEndereco,IDCadastro,IDCadastroTipoDeContato,Endereco)  ";
                        sval += " Values (" + Id + "," + IdDestinatario + ",1,'" + ped.customer.email + "') ";
                    }


                    if (ped.customer.phoneOffice != null && ped.customer.phoneOffice != "")
                    {
                        int Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CadastroContatoEndereco", cnx));
                        sval += "; INSERT INTO CadastroContatoEndereco (IDCadastroContatoEndereco,IDCadastro,IDCadastroTipoDeContato,Endereco)  ";
                        sval += " Values (" + Id + "," + IdDestinatario + ",2,'" + ped.customer.phoneOffice + "') ";
                    }

                    if (ped.customer.phoneHome != null && ped.customer.phoneHome != "")
                    {
                        int Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CadastroContatoEndereco", cnx));
                        sval += "; INSERT INTO CadastroContatoEndereco (IDCadastroContatoEndereco,IDCadastro,IDCadastroTipoDeContato,Endereco)  ";
                        sval += " Values (" + Id + "," + IdDestinatario + ",3,'" + ped.customer.phoneHome + "') ";
                    }

                    if (ped.customer.phoneMobile != null && ped.customer.phoneMobile != "")
                    {
                        int Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CadastroContatoEndereco", cnx));
                        sval += "; INSERT INTO CadastroContatoEndereco (IDCadastroContatoEndereco,IDCadastro,IDCadastroTipoDeContato,Endereco)  ";
                        sval += " Values (" + Id + "," + IdDestinatario + ",3,'" + ped.customer.phoneMobile + "') ";
                    }


                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sins + sval, cnx);

                }
                else
                {
                    sql = "Update Cadastro set customerId=  '" + ped.customer.customerId + "', AddressId ='" + ped.billingAddress.addressId + "' ";

                    if (ped.billingAddress.additionalInfo == null)
                        ped.billingAddress.additionalInfo = "";
                    else
                        ped.billingAddress.additionalInfo = ped.billingAddress.additionalInfo.ToUpper().Replace("'", " ");


                    if (ped.billingAddress.additionalInfo.Length > 60)
                        ped.billingAddress.additionalInfo = ped.billingAddress.additionalInfo.Substring(0, 58);

                    if (ped.deliveries.Length > 0)
                    {
                        sql += " , Endereco='" + (ped.billingAddress.address1.ToUpper().Replace("'", " ") == "" ? ped.deliveries[0].deliveryAddress.address1 : ped.billingAddress.address1.ToUpper().Replace("'", " ")) + "', Numero= '" + ped.billingAddress.addressNr.ToUpper().Replace("'", " ") + "', Complemento='" + (ped.billingAddress.additionalInfo == null ? "" : ped.billingAddress.additionalInfo.ToUpper().Replace("'", " ")) + "', Cep='" + ped.billingAddress.postalCd.ToUpper().Replace("'", " ").Replace("-", "") + "' ";
                        int idCidade = CidadeBairro.RetornarCidade(ped.billingAddress.postalCd.ToUpper().Replace("'", " ").Replace("-", ""), cnx);

                        if (idCidade > 0)
                            sql += ", IdCidade = " + idCidade.ToString();

                        int idBairro = CidadeBairro.RetornarBairro(ped.billingAddress.quarter.ToUpper().Replace("'", " "), idCidade.ToString(), cnx);

                        if (idBairro > 0)
                            sql += ", IdBairro=" + idBairro.ToString();

                        if (ped.customer.state_subscription != null && ped.customer.state_subscription != "")
                            sql += ", InscricaoRG ='" + ped.customer.state_subscription + "'";
                        else
                            sql += ", InscricaoRG ='ISENTO'";

                    }


                    sql += " Where IdCadastro=" + IdDestinatario;
                    //Sistran.Library.GetDataTables.ExecutarRetornoID_WIN(sql, cnx);

                    sql += "; delete from CadastroContatoEndereco where IdCadastro = " + IdDestinatario;


                    #region "----"
                    if (ped.customer.email != null && ped.customer.email != "")
                    {
                        int Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CadastroContatoEndereco", cnx));
                        sql += "; INSERT INTO CadastroContatoEndereco (IDCadastroContatoEndereco,IDCadastro,IDCadastroTipoDeContato,Endereco)  ";
                        sql += " Values (" + Id + "," + IdDestinatario + ",1,'" + ped.customer.email + "') ";
                    }


                    if (ped.customer.phoneOffice != null && ped.customer.phoneOffice != "")
                    {
                        int Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CadastroContatoEndereco", cnx));
                        sql += "; INSERT INTO CadastroContatoEndereco (IDCadastroContatoEndereco,IDCadastro,IDCadastroTipoDeContato,Endereco)  ";
                        sql += " Values (" + Id + "," + IdDestinatario + ",2,'" + ped.customer.phoneOffice + "') ";
                    }

                    if (ped.customer.phoneHome != null && ped.customer.phoneHome != "")
                    {
                        int Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CadastroContatoEndereco", cnx));
                        sql += "; INSERT INTO CadastroContatoEndereco (IDCadastroContatoEndereco,IDCadastro,IDCadastroTipoDeContato,Endereco)  ";
                        sql += " Values (" + Id + "," + IdDestinatario + ",3,'" + ped.customer.phoneHome + "') ";
                    }

                    if (ped.customer.phoneMobile != null && ped.customer.phoneMobile != "")
                    {
                        int Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CadastroContatoEndereco", cnx));
                        sql += "; INSERT INTO CadastroContatoEndereco (IDCadastroContatoEndereco,IDCadastro,IDCadastroTipoDeContato,Endereco)  ";
                        sql += " Values (" + Id + "," + IdDestinatario + ",3,'" + ped.customer.phoneMobile + "') ";
                    }


                    #endregion

                    Sistran.Library.GetDataTables.ExecutarRetornoID_WIN(sql, cnx);


                }

                return Sistran.Library.GetDataTables.RetornarDataTableWS("Select * from Cadastro  with (nolock) Where IdCadastro=" + IdDestinatario, cnx);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void ProcurarProduto(orderLine orderLine, ref string idProdEmb, ref string IdprodCli)
        {
            try
            {
                string sql = "select pe.IdProdutoEmbalagem, Pc.IdProdutoCliente from ProdutoCliente Pc  with (nolock) ";
                sql += " Inner join ProdutoEmbalagem pe  with (nolock) on pe.idProdutoCliente = Pc.IdprodutoCliente ";
                sql += " where pc.Codigo = '" + orderLine.sku + "' ";
                sql += " and pc.IdCliente = " + idCliente;

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                string nomeprod = "";

                if (orderLine.skuName == null)
                {
                    nomeprod = orderLine.promotionList[0].skuNameTrigger;
                }
                else
                {
                    nomeprod = orderLine.skuName.Trim().ToUpper().Replace("'", "");
                }



                if (nomeprod.Length > 60)
                    nomeprod = nomeprod.Substring(0, 59);

                if (dt.Rows.Count == 0)
                {
                    string m = "Select IdProduto from Produto Where CodigoDeBarras='" + orderLine.sku + "'";
                    DataTable dPro = Sistran.Library.GetDataTables.RetornarDataTableWS(m, cnx);
                    string idProd = "";

                    if (dPro.Rows.Count > 0)
                        idProd = dPro.Rows[0][0].ToString();
                    else
                    {
                        idProd = Sistran.Library.GetDataTables.RetornarIdTabela("Produto", cnx);
                        sql = "INSERT INTO PRODUTO (IDProduto, CodigoDeBarras, PesoLiquido, Especie, DataDeCadastro, PesoBruto) VALUES (" + idProd + ", '" + orderLine.sku + "', 0, 'UNI', Getdate(), 0)";
                    }

                    IdprodCli = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOCLIENTE", cnx);
                    sql += "; INSERT INTO PRODUTOCLIENTE (IDProdutoCliente, IDCliente, IDUnidadeDeMedida,Codigo, Descricao,MetodoDeMovimentacao, DesmembraNaNF, SolicitarDataDeValidade,UnidadeDoFornecedor,Ativo, CodigoNCM, FatorUsoPosicaoPallet) ";
                    sql += " VALUES(" + IdprodCli + ", " + idCliente + ", 1,'" + orderLine.sku + "', '" + nomeprod + "','FEFO', 'NAO', 'SIM',1.00,'SIM', '', 1)";

                    idProdEmb = Sistran.Library.GetDataTables.RetornarIdTabela("ProdutoEmbalagem", cnx);
                    sql += "; INSERT INTO PRODUTOEMBALAGEM(IDProdutoEmbalagem, IDProdutoCliente, IDProduto, Conteudo, UnidadeDoCliente, ValorUnitario, DataDeCadastro, Ativo) ";
                    sql += " VALUES(" + idProdEmb + ", " + IdprodCli + ", " + idProd + ", '" + nomeprod + "', 1, " + orderLine.listPrice.ToString().Replace(",", ".") + ", GETDATE(), 'SIM')";
                }
                else
                {
                    sql = "Update ProdutoCliente set Descricao ='" + nomeprod + "' where IdProdutoCliente = " + dt.Rows[0]["IdProdutoCliente"].ToString();
                    idProdEmb = dt.Rows[0]["IdProdutoEmbalagem"].ToString();
                    IdprodCli = dt.Rows[0]["IdProdutoCliente"].ToString();
                }

                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Boolean Autenticou()
        {
            String TOKEN = HttpContext.Current.Request.Headers["Authorization"];

            var base64 = Convert.FromBase64String(TOKEN.Replace("Basic ", ""));
            var str = Encoding.UTF8.GetString(base64);

            String[] User = str.Split(':');

            Boolean Autenticou = false;

            if (User[0] == "Josapar" && User[1] == "Josapar")
            {
                Autenticou = true;
            }

            return Autenticou;
        }


    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    // //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/AcecBOSOAIntegration/BOOrderIntegration")]
    public partial class integrateOrderRequest
    {

        private order orderField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.accurate.com/acec/order")]
        public order order
        {
            get
            {
                return this.orderField;
            }
            set
            {
                this.orderField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    ////[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class order
    {

        private string orderIdField;

        private string customerIdField;

        private string partnerIdField;

        private decimal totalAmountField;

        private decimal totalDiscountAmountField;

        private decimal totalLoyaltyCreditEarningAmountField;

        private string partnerOrderIdField;

        private System.DateTime purchaseDateField;

        private string visitorIpField;

        private string visitorIdField;

        private string salesOperatorIdField;

        private System.DateTime sessionCreationDateField;

        private bool sessionCreationDateFieldSpecified;

        private string applicationVersionField;

        private string listIdField;

        private string saleChannelField;

        private string shopIdField;

        private string leaderField;

        private orderOrderType orderTypeField;

        private string brandIdField;

        private string storeIdField;

        private string orderMasterIdField;

        private delivery[] deliveriesField;

        private orderProperty[] orderPropertyListField;

        private customer customerField;

        private address billingAddressField;

        private payment[] paymentListField;

        private decimal freightChargedAmountField;

        private bool freightChargedAmountFieldSpecified;

        private decimal freightActualAmountField;

        private bool freightActualAmountFieldSpecified;

        private decimal freightCommercialAmountField;

        private bool freightCommercialAmountFieldSpecified;

        private string countDistinctSkuField;

        private string orderOwnerTpField;

        private shopInfo shopInfoField;

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
        public string customerId
        {
            get
            {
                return this.customerIdField;
            }
            set
            {
                this.customerIdField = value;
            }
        }

        /// <remarks/>
        public string partnerId
        {
            get
            {
                return this.partnerIdField;
            }
            set
            {
                this.partnerIdField = value;
            }
        }

        /// <remarks/>
        public decimal totalAmount
        {
            get
            {
                return this.totalAmountField;
            }
            set
            {
                this.totalAmountField = value;
            }
        }

        /// <remarks/>
        public decimal totalDiscountAmount
        {
            get
            {
                return this.totalDiscountAmountField;
            }
            set
            {
                this.totalDiscountAmountField = value;
            }
        }

        /// <remarks/>
        public decimal totalLoyaltyCreditEarningAmount
        {
            get
            {
                return this.totalLoyaltyCreditEarningAmountField;
            }
            set
            {
                this.totalLoyaltyCreditEarningAmountField = value;
            }
        }

        /// <remarks/>
        public string partnerOrderId
        {
            get
            {
                return this.partnerOrderIdField;
            }
            set
            {
                this.partnerOrderIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime purchaseDate
        {
            get
            {
                return this.purchaseDateField;
            }
            set
            {
                this.purchaseDateField = value;
            }
        }

        /// <remarks/>
        public string visitorIp
        {
            get
            {
                return this.visitorIpField;
            }
            set
            {
                this.visitorIpField = value;
            }
        }

        /// <remarks/>
        public string visitorId
        {
            get
            {
                return this.visitorIdField;
            }
            set
            {
                this.visitorIdField = value;
            }
        }

        /// <remarks/>
        public string salesOperatorId
        {
            get
            {
                return this.salesOperatorIdField;
            }
            set
            {
                this.salesOperatorIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime sessionCreationDate
        {
            get
            {
                return this.sessionCreationDateField;
            }
            set
            {
                this.sessionCreationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sessionCreationDateSpecified
        {
            get
            {
                return this.sessionCreationDateFieldSpecified;
            }
            set
            {
                this.sessionCreationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string applicationVersion
        {
            get
            {
                return this.applicationVersionField;
            }
            set
            {
                this.applicationVersionField = value;
            }
        }

        /// <remarks/>
        public string listId
        {
            get
            {
                return this.listIdField;
            }
            set
            {
                this.listIdField = value;
            }
        }

        /// <remarks/>
        public string saleChannel
        {
            get
            {
                return this.saleChannelField;
            }
            set
            {
                this.saleChannelField = value;
            }
        }

        /// <remarks/>
        public string shopId
        {
            get
            {
                return this.shopIdField;
            }
            set
            {
                this.shopIdField = value;
            }
        }

        /// <remarks/>
        public string leader
        {
            get
            {
                return this.leaderField;
            }
            set
            {
                this.leaderField = value;
            }
        }

        /// <remarks/>
        public orderOrderType orderType
        {
            get
            {
                return this.orderTypeField;
            }
            set
            {
                this.orderTypeField = value;
            }
        }

        /// <remarks/>
        public string brandId
        {
            get
            {
                return this.brandIdField;
            }
            set
            {
                this.brandIdField = value;
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

        /// <remarks/>
        public string orderMasterId
        {
            get
            {
                return this.orderMasterIdField;
            }
            set
            {
                this.orderMasterIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public delivery[] deliveries
        {
            get
            {
                return this.deliveriesField;
            }
            set
            {
                this.deliveriesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public orderProperty[] orderPropertyList
        {
            get
            {
                return this.orderPropertyListField;
            }
            set
            {
                this.orderPropertyListField = value;
            }
        }

        /// <remarks/>
        public customer customer
        {
            get
            {
                return this.customerField;
            }
            set
            {
                this.customerField = value;
            }
        }

        /// <remarks/>
        public address billingAddress
        {
            get
            {
                return this.billingAddressField;
            }
            set
            {
                this.billingAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public payment[] paymentList
        {
            get
            {
                return this.paymentListField;
            }
            set
            {
                this.paymentListField = value;
            }
        }

        /// <remarks/>
        public decimal freightChargedAmount
        {
            get
            {
                return this.freightChargedAmountField;
            }
            set
            {
                this.freightChargedAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool freightChargedAmountSpecified
        {
            get
            {
                return this.freightChargedAmountFieldSpecified;
            }
            set
            {
                this.freightChargedAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal freightActualAmount
        {
            get
            {
                return this.freightActualAmountField;
            }
            set
            {
                this.freightActualAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool freightActualAmountSpecified
        {
            get
            {
                return this.freightActualAmountFieldSpecified;
            }
            set
            {
                this.freightActualAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal freightCommercialAmount
        {
            get
            {
                return this.freightCommercialAmountField;
            }
            set
            {
                this.freightCommercialAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool freightCommercialAmountSpecified
        {
            get
            {
                return this.freightCommercialAmountFieldSpecified;
            }
            set
            {
                this.freightCommercialAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string countDistinctSku
        {
            get
            {
                return this.countDistinctSkuField;
            }
            set
            {
                this.countDistinctSkuField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string orderOwnerTp
        {
            get
            {
                return this.orderOwnerTpField;
            }
            set
            {
                this.orderOwnerTpField = value;
            }
        }

        /// <remarks/>
        public shopInfo shopInfo
        {
            get
            {
                return this.shopInfoField;
            }
            set
            {
                this.shopInfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/order")]
    public enum orderOrderType
    {

        /// <remarks/>
        NORMAL,

        /// <remarks/>
        PRESENT,

        /// <remarks/>
        OTHER,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    ////[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class delivery
    {

        private string deliveryIdField;

        private string orderIdField;

        private string deliveryTypeField;

        private string relatedDeliveryIdField;

        private string statusField;

        private decimal totalAmountField;

        private decimal totalDiscountAmountField;

        private decimal discountRoundingAmountField;

        private decimal totalLoyaltyCreditEarningAmountField;

        private decimal loyaltyCreditEarningRoundingAmountField;

        private string billingAddressIdField;

        private string deliveryAddressIdField;

        private orderLine[] orderLineListField;

        private tracking[] trackingListField;

        private skuDeliveryTracking[] skuDeliveryTrackingListField;

        private freight freightAmountField;

        private string wareHouseIdField;

        private long wareHouseCNPJField;

        private string invoiceNumberField;

        private string invoiceSeriesField;

        private string carrierCodeField;

        private address deliveryAddressField;

        private string scheduledPeriodField;

        private System.DateTime scheduledDateField;

        private bool scheduledDateFieldSpecified;

        private deliveryProperty[] deliveryPropertyListField;

        private slotInfo slotInfoField;

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
        public string deliveryType
        {
            get
            {
                return this.deliveryTypeField;
            }
            set
            {
                this.deliveryTypeField = value;
            }
        }

        /// <remarks/>
        public string relatedDeliveryId
        {
            get
            {
                return this.relatedDeliveryIdField;
            }
            set
            {
                this.relatedDeliveryIdField = value;
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
        public decimal totalAmount
        {
            get
            {
                return this.totalAmountField;
            }
            set
            {
                this.totalAmountField = value;
            }
        }

        /// <remarks/>
        public decimal totalDiscountAmount
        {
            get
            {
                return this.totalDiscountAmountField;
            }
            set
            {
                this.totalDiscountAmountField = value;
            }
        }

        /// <remarks/>
        public decimal discountRoundingAmount
        {
            get
            {
                return this.discountRoundingAmountField;
            }
            set
            {
                this.discountRoundingAmountField = value;
            }
        }

        /// <remarks/>
        public decimal totalLoyaltyCreditEarningAmount
        {
            get
            {
                return this.totalLoyaltyCreditEarningAmountField;
            }
            set
            {
                this.totalLoyaltyCreditEarningAmountField = value;
            }
        }

        /// <remarks/>
        public decimal loyaltyCreditEarningRoundingAmount
        {
            get
            {
                return this.loyaltyCreditEarningRoundingAmountField;
            }
            set
            {
                this.loyaltyCreditEarningRoundingAmountField = value;
            }
        }

        /// <remarks/>
        public string billingAddressId
        {
            get
            {
                return this.billingAddressIdField;
            }
            set
            {
                this.billingAddressIdField = value;
            }
        }

        /// <remarks/>
        public string deliveryAddressId
        {
            get
            {
                return this.deliveryAddressIdField;
            }
            set
            {
                this.deliveryAddressIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public orderLine[] orderLineList
        {
            get
            {
                return this.orderLineListField;
            }
            set
            {
                this.orderLineListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public tracking[] trackingList
        {
            get
            {
                return this.trackingListField;
            }
            set
            {
                this.trackingListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public skuDeliveryTracking[] skuDeliveryTrackingList
        {
            get
            {
                return this.skuDeliveryTrackingListField;
            }
            set
            {
                this.skuDeliveryTrackingListField = value;
            }
        }

        /// <remarks/>
        public freight freightAmount
        {
            get
            {
                return this.freightAmountField;
            }
            set
            {
                this.freightAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string wareHouseId
        {
            get
            {
                return this.wareHouseIdField;
            }
            set
            {
                this.wareHouseIdField = value;
            }
        }

        /// <remarks/>
        public long wareHouseCNPJ
        {
            get
            {
                return this.wareHouseCNPJField;
            }
            set
            {
                this.wareHouseCNPJField = value;
            }
        }

        /// <remarks/>
        public string invoiceNumber
        {
            get
            {
                return this.invoiceNumberField;
            }
            set
            {
                this.invoiceNumberField = value;
            }
        }

        /// <remarks/>
        public string invoiceSeries
        {
            get
            {
                return this.invoiceSeriesField;
            }
            set
            {
                this.invoiceSeriesField = value;
            }
        }

        /// <remarks/>
        public string carrierCode
        {
            get
            {
                return this.carrierCodeField;
            }
            set
            {
                this.carrierCodeField = value;
            }
        }

        /// <remarks/>
        public address deliveryAddress
        {
            get
            {
                return this.deliveryAddressField;
            }
            set
            {
                this.deliveryAddressField = value;
            }
        }

        /// <remarks/>
        public string scheduledPeriod
        {
            get
            {
                return this.scheduledPeriodField;
            }
            set
            {
                this.scheduledPeriodField = value;
            }
        }

        /// <remarks/>
        public System.DateTime scheduledDate
        {
            get
            {
                return this.scheduledDateField;
            }
            set
            {
                this.scheduledDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool scheduledDateSpecified
        {
            get
            {
                return this.scheduledDateFieldSpecified;
            }
            set
            {
                this.scheduledDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public deliveryProperty[] deliveryPropertyList
        {
            get
            {
                return this.deliveryPropertyListField;
            }
            set
            {
                this.deliveryPropertyListField = value;
            }
        }

        /// <remarks/>
        public slotInfo slotInfo
        {
            get
            {
                return this.slotInfoField;
            }
            set
            {
                this.slotInfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    ////[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class orderLine
    {

        private string orderLineIdField;

        private string skuField;

        private string skuIdOriginField;

        private string skuTypeField;

        private int quantityField;

        private string measureUnitField;

        private decimal catalogListPriceField;

        private decimal listPriceField;

        private decimal salePriceField;

        private replenishmentInfo replenishmentInfoField;

        private decimal unconditionalDiscountAmountField;

        private string conditionalDiscountAmountField;
        //        private decimal conditionalDiscountAmountField;

        private decimal roundingDiscountAmountField;

        private decimal loyaltyCreditEarningAmountField;

        private decimal roundingLoyaltyCreditEarningAmountField;

        private promotion[] promotionListField;

        private loyaltyCredit[] loyaltyCreditListField;

        private freight freightField;

        private int leadTimeField;

        private bool leadTimeFieldSpecified;

        private giftCard giftCardField;

        private giftWrap giftWrapField;

        private string kitSkuIdField;

        private string kitSkuNameField;

        private int kitQuantityField;

        private bool kitQuantityFieldSpecified;

        private string[] referenceField;

        private string catalogIdField;

        private string catalogReferenceField;

        private orderLineProperty[] orderLinePropertyListField;

        private string inventoryTypeField;

        private bool serviceField;

        private string skuNameField;

        private string productBrandNameField;

        private decimal itemTotalFinancialExpenseField;

        private bool itemTotalFinancialExpenseFieldSpecified;

        private level[] itemLevelListField;

        private level[] itemLevelErpListField;

        /// <remarks/>
        public string orderLineId
        {
            get
            {
                return this.orderLineIdField;
            }
            set
            {
                this.orderLineIdField = value;
            }
        }

        /// <remarks/>
        public string sku
        {
            get
            {
                return this.skuField;
            }
            set
            {
                this.skuField = value;
            }
        }

        /// <remarks/>
        public string skuIdOrigin
        {
            get
            {
                return this.skuIdOriginField;
            }
            set
            {
                this.skuIdOriginField = value;
            }
        }

        /// <remarks/>
        public string skuType
        {
            get
            {
                return this.skuTypeField;
            }
            set
            {
                this.skuTypeField = value;
            }
        }

        /// <remarks/>
        public int quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        public string measureUnit
        {
            get
            {
                return this.measureUnitField;
            }
            set
            {
                this.measureUnitField = value;
            }
        }

        /// <remarks/>
        public decimal catalogListPrice
        {
            get
            {
                return this.catalogListPriceField;
            }
            set
            {
                this.catalogListPriceField = value;
            }
        }

        /// <remarks/>
        public decimal listPrice
        {
            get
            {
                return this.listPriceField;
            }
            set
            {
                this.listPriceField = value;
            }
        }

        /// <remarks/>
        public decimal salePrice
        {
            get
            {
                return this.salePriceField;
            }
            set
            {
                this.salePriceField = value;
            }
        }

        /// <remarks/>
        public replenishmentInfo replenishmentInfo
        {
            get
            {
                return this.replenishmentInfoField;
            }
            set
            {
                this.replenishmentInfoField = value;
            }
        }

        /// <remarks/>
        public decimal unconditionalDiscountAmount
        {
            get
            {
                return this.unconditionalDiscountAmountField;
            }
            set
            {
                this.unconditionalDiscountAmountField = value;
            }
        }

        /// <remarks/>
        //public decimal conditionalDiscountAmount
        //{
        //    get
        //    {
        //        return this.conditionalDiscountAmountField;
        //    }
        //    set
        //    {
        //        this.conditionalDiscountAmountField = value;
        //    }
        //}


        public string conditionalDiscountAmount
        {
            get
            {
                return this.conditionalDiscountAmountField;
            }
            set
            {
                this.conditionalDiscountAmountField = value;
            }
        }

        /// <remarks/>
        public decimal roundingDiscountAmount
        {
            get
            {
                return this.roundingDiscountAmountField;
            }
            set
            {
                this.roundingDiscountAmountField = value;
            }
        }

        /// <remarks/>
        public decimal loyaltyCreditEarningAmount
        {
            get
            {
                return this.loyaltyCreditEarningAmountField;
            }
            set
            {
                this.loyaltyCreditEarningAmountField = value;
            }
        }

        /// <remarks/>
        public decimal roundingLoyaltyCreditEarningAmount
        {
            get
            {
                return this.roundingLoyaltyCreditEarningAmountField;
            }
            set
            {
                this.roundingLoyaltyCreditEarningAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public promotion[] promotionList
        {
            get
            {
                return this.promotionListField;
            }
            set
            {
                this.promotionListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public loyaltyCredit[] loyaltyCreditList
        {
            get
            {
                return this.loyaltyCreditListField;
            }
            set
            {
                this.loyaltyCreditListField = value;
            }
        }

        /// <remarks/>
        public freight freight
        {
            get
            {
                return this.freightField;
            }
            set
            {
                this.freightField = value;
            }
        }

        /// <remarks/>
        public int leadTime
        {
            get
            {
                return this.leadTimeField;
            }
            set
            {
                this.leadTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool leadTimeSpecified
        {
            get
            {
                return this.leadTimeFieldSpecified;
            }
            set
            {
                this.leadTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public giftCard giftCard
        {
            get
            {
                return this.giftCardField;
            }
            set
            {
                this.giftCardField = value;
            }
        }

        /// <remarks/>
        public giftWrap giftWrap
        {
            get
            {
                return this.giftWrapField;
            }
            set
            {
                this.giftWrapField = value;
            }
        }

        /// <remarks/>
        public string kitSkuId
        {
            get
            {
                return this.kitSkuIdField;
            }
            set
            {
                this.kitSkuIdField = value;
            }
        }

        /// <remarks/>
        public string kitSkuName
        {
            get
            {
                return this.kitSkuNameField;
            }
            set
            {
                this.kitSkuNameField = value;
            }
        }

        /// <remarks/>
        public int kitQuantity
        {
            get
            {
                return this.kitQuantityField;
            }
            set
            {
                this.kitQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool kitQuantitySpecified
        {
            get
            {
                return this.kitQuantityFieldSpecified;
            }
            set
            {
                this.kitQuantityFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("skuId", IsNullable = false)]
        public string[] reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        public string catalogId
        {
            get
            {
                return this.catalogIdField;
            }
            set
            {
                this.catalogIdField = value;
            }
        }

        /// <remarks/>
        public string catalogReference
        {
            get
            {
                return this.catalogReferenceField;
            }
            set
            {
                this.catalogReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public orderLineProperty[] orderLinePropertyList
        {
            get
            {
                return this.orderLinePropertyListField;
            }
            set
            {
                this.orderLinePropertyListField = value;
            }
        }

        /// <remarks/>
        public string inventoryType
        {
            get
            {
                return this.inventoryTypeField;
            }
            set
            {
                this.inventoryTypeField = value;
            }
        }

        /// <remarks/>
        public bool service
        {
            get
            {
                return this.serviceField;
            }
            set
            {
                this.serviceField = value;
            }
        }

        /// <remarks/>
        public string skuName
        {
            get
            {
                return this.skuNameField;
            }
            set
            {
                this.skuNameField = value;
            }
        }

        /// <remarks/>
        public string productBrandName
        {
            get
            {
                return this.productBrandNameField;
            }
            set
            {
                this.productBrandNameField = value;
            }
        }

        /// <remarks/>
        public decimal itemTotalFinancialExpense
        {
            get
            {
                return this.itemTotalFinancialExpenseField;
            }
            set
            {
                this.itemTotalFinancialExpenseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool itemTotalFinancialExpenseSpecified
        {
            get
            {
                return this.itemTotalFinancialExpenseFieldSpecified;
            }
            set
            {
                this.itemTotalFinancialExpenseFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("itemLevel", IsNullable = false)]
        public level[] itemLevelList
        {
            get
            {
                return this.itemLevelListField;
            }
            set
            {
                this.itemLevelListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("itemLevel", IsNullable = false)]
        public level[] itemLevelErpList
        {
            get
            {
                return this.itemLevelErpListField;
            }
            set
            {
                this.itemLevelErpListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    ////[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class replenishmentInfo
    {

        private string quantityField;

        private System.DateTime stockStartDateField;

        /// <remarks/>
        public string quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        public System.DateTime stockStartDate
        {
            get
            {
                return this.stockStartDateField;
            }
            set
            {
                this.stockStartDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    ////[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class shopInfo
    {

        private string shopIdField;

        private string shopCodeField;

        private string legacyShopIdField;

        private string shopNameField;

        private string shopServiceField;

        private string classificationField;

        private string stateField;

        /// <remarks/>
        public string shopId
        {
            get
            {
                return this.shopIdField;
            }
            set
            {
                this.shopIdField = value;
            }
        }

        /// <remarks/>
        public string shopCode
        {
            get
            {
                return this.shopCodeField;
            }
            set
            {
                this.shopCodeField = value;
            }
        }

        /// <remarks/>
        public string legacyShopId
        {
            get
            {
                return this.legacyShopIdField;
            }
            set
            {
                this.legacyShopIdField = value;
            }
        }

        /// <remarks/>
        public string shopName
        {
            get
            {
                return this.shopNameField;
            }
            set
            {
                this.shopNameField = value;
            }
        }

        /// <remarks/>
        public string shopService
        {
            get
            {
                return this.shopServiceField;
            }
            set
            {
                this.shopServiceField = value;
            }
        }

        /// <remarks/>
        public string classification
        {
            get
            {
                return this.classificationField;
            }
            set
            {
                this.classificationField = value;
            }
        }

        /// <remarks/>
        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    ////[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class paymentProperty
    {

        private string paymentPropertyIdField;

        private string paymentPropertyValueField;

        /// <remarks/>
        public string paymentPropertyId
        {
            get
            {
                return this.paymentPropertyIdField;
            }
            set
            {
                this.paymentPropertyIdField = value;
            }
        }

        /// <remarks/>
        public string paymentPropertyValue
        {
            get
            {
                return this.paymentPropertyValueField;
            }
            set
            {
                this.paymentPropertyValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(sipsPayment))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(cieloPayment))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(creditCardPayment))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    ////[System.Diagnostics.DebuggerStepThroughAttribute()]//
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class abstractPayment
    {

        private string paymentIdField;

        private string transactionIdField;

        private string paymentTypeField;

        private string paymentKeyField;

        private string documentNumberField;

        private decimal valueField;

        private decimal interestRateField;

        private decimal interestAmountField;

        private System.DateTime dueDateField;

        private string statusField;

        private string bankCodeField;

        private string agencyCodeField;

        private string accountNumberField;

        private paymentProperty[] paymentPropertyListField;

        /// <remarks/>
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
        public string transactionId
        {
            get
            {
                return this.transactionIdField;
            }
            set
            {
                this.transactionIdField = value;
            }
        }

        /// <remarks/>
        public string paymentType
        {
            get
            {
                return this.paymentTypeField;
            }
            set
            {
                this.paymentTypeField = value;
            }
        }

        /// <remarks/>
        public string paymentKey
        {
            get
            {
                return this.paymentKeyField;
            }
            set
            {
                this.paymentKeyField = value;
            }
        }

        /// <remarks/>
        public string documentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }

        /// <remarks/>
        public decimal value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        public decimal interestRate
        {
            get
            {
                return this.interestRateField;
            }
            set
            {
                this.interestRateField = value;
            }
        }

        /// <remarks/>
        public decimal interestAmount
        {
            get
            {
                return this.interestAmountField;
            }
            set
            {
                this.interestAmountField = value;
            }
        }

        /// <remarks/>
        public System.DateTime dueDate
        {
            get
            {
                return this.dueDateField;
            }
            set
            {
                this.dueDateField = value;
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
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string bankCode
        {
            get
            {
                return this.bankCodeField;
            }
            set
            {
                this.bankCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string agencyCode
        {
            get
            {
                return this.agencyCodeField;
            }
            set
            {
                this.agencyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string accountNumber
        {
            get
            {
                return this.accountNumberField;
            }
            set
            {
                this.accountNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public paymentProperty[] paymentPropertyList
        {
            get
            {
                return this.paymentPropertyListField;
            }
            set
            {
                this.paymentPropertyListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    ////[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class sipsPayment : abstractPayment
    {

        private string authorizationIdField;

        private string bankResponseCodeField;

        private string maskedCreditCardNumberField;

        private string cvvFlagField;

        private string cvvResponseCodeField;

        private string paymentCertificateField;

        private System.DateTime paymentDateTimeField;

        private string billingTypeField;

        private string responseCodeField;

        private string transactionConditionField;

        private string currencyCodeField;

        private System.DateTime transmitionDateTimeField;

        /// <remarks/>
        public string authorizationId
        {
            get
            {
                return this.authorizationIdField;
            }
            set
            {
                this.authorizationIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string bankResponseCode
        {
            get
            {
                return this.bankResponseCodeField;
            }
            set
            {
                this.bankResponseCodeField = value;
            }
        }

        /// <remarks/>
        public string maskedCreditCardNumber
        {
            get
            {
                return this.maskedCreditCardNumberField;
            }
            set
            {
                this.maskedCreditCardNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string cvvFlag
        {
            get
            {
                return this.cvvFlagField;
            }
            set
            {
                this.cvvFlagField = value;
            }
        }

        /// <remarks/>
        public string cvvResponseCode
        {
            get
            {
                return this.cvvResponseCodeField;
            }
            set
            {
                this.cvvResponseCodeField = value;
            }
        }

        /// <remarks/>
        public string paymentCertificate
        {
            get
            {
                return this.paymentCertificateField;
            }
            set
            {
                this.paymentCertificateField = value;
            }
        }

        /// <remarks/>
        public System.DateTime paymentDateTime
        {
            get
            {
                return this.paymentDateTimeField;
            }
            set
            {
                this.paymentDateTimeField = value;
            }
        }

        /// <remarks/>
        public string billingType
        {
            get
            {
                return this.billingTypeField;
            }
            set
            {
                this.billingTypeField = value;
            }
        }

        /// <remarks/>
        public string responseCode
        {
            get
            {
                return this.responseCodeField;
            }
            set
            {
                this.responseCodeField = value;
            }
        }

        /// <remarks/>
        public string transactionCondition
        {
            get
            {
                return this.transactionConditionField;
            }
            set
            {
                this.transactionConditionField = value;
            }
        }

        /// <remarks/>
        public string currencyCode
        {
            get
            {
                return this.currencyCodeField;
            }
            set
            {
                this.currencyCodeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime transmitionDateTime
        {
            get
            {
                return this.transmitionDateTimeField;
            }
            set
            {
                this.transmitionDateTimeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class cieloPayment : abstractPayment
    {

        private string approvalCodeField;

        private System.DateTime approvalDateTimeField;

        private string eciField;

        private string lrField;

        private string arpField;

        private string panField;

        private string nsuField;

        private string cardHoldersNameField;

        private string brandField;

        private string encriptedCardNumberField;

        private string encriptedExpirationDateField;

        private string encriptedCardSecurityCodeField;

        private string maskedCardNumberField;

        private cieloPaymentCardVerificationCode cardVerificationCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string approvalCode
        {
            get
            {
                return this.approvalCodeField;
            }
            set
            {
                this.approvalCodeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime approvalDateTime
        {
            get
            {
                return this.approvalDateTimeField;
            }
            set
            {
                this.approvalDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string eci
        {
            get
            {
                return this.eciField;
            }
            set
            {
                this.eciField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string lr
        {
            get
            {
                return this.lrField;
            }
            set
            {
                this.lrField = value;
            }
        }

        /// <remarks/>
        public string arp
        {
            get
            {
                return this.arpField;
            }
            set
            {
                this.arpField = value;
            }
        }

        /// <remarks/>
        public string pan
        {
            get
            {
                return this.panField;
            }
            set
            {
                this.panField = value;
            }
        }

        /// <remarks/>
        public string nsu
        {
            get
            {
                return this.nsuField;
            }
            set
            {
                this.nsuField = value;
            }
        }

        /// <remarks/>
        public string cardHoldersName
        {
            get
            {
                return this.cardHoldersNameField;
            }
            set
            {
                this.cardHoldersNameField = value;
            }
        }

        /// <remarks/>
        public string brand
        {
            get
            {
                return this.brandField;
            }
            set
            {
                this.brandField = value;
            }
        }

        /// <remarks/>
        public string encriptedCardNumber
        {
            get
            {
                return this.encriptedCardNumberField;
            }
            set
            {
                this.encriptedCardNumberField = value;
            }
        }

        /// <remarks/>
        public string encriptedExpirationDate
        {
            get
            {
                return this.encriptedExpirationDateField;
            }
            set
            {
                this.encriptedExpirationDateField = value;
            }
        }

        /// <remarks/>
        public string encriptedCardSecurityCode
        {
            get
            {
                return this.encriptedCardSecurityCodeField;
            }
            set
            {
                this.encriptedCardSecurityCodeField = value;
            }
        }

        /// <remarks/>
        public string maskedCardNumber
        {
            get
            {
                return this.maskedCardNumberField;
            }
            set
            {
                this.maskedCardNumberField = value;
            }
        }

        /// <remarks/>
        public cieloPaymentCardVerificationCode cardVerificationCode
        {
            get
            {
                return this.cardVerificationCodeField;
            }
            set
            {
                this.cardVerificationCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/order")]
    public enum cieloPaymentCardVerificationCode
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class creditCardPayment : abstractPayment
    {

        private string cardHoldersNameField;

        private string brandField;

        private string numberOfInstallmentsField;

        private string encriptedCreditCardNumberField;

        private string encriptedExpirationDateField;

        private string encriptedCardSecurityCodeField;

        private string maskedCreditCardNumberField;

        private creditCardPaymentCardVerificationCode cardVerificationCodeField;

        /// <remarks/>
        public string cardHoldersName
        {
            get
            {
                return this.cardHoldersNameField;
            }
            set
            {
                this.cardHoldersNameField = value;
            }
        }

        /// <remarks/>
        public string brand
        {
            get
            {
                return this.brandField;
            }
            set
            {
                this.brandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string numberOfInstallments
        {
            get
            {
                return this.numberOfInstallmentsField;
            }
            set
            {
                this.numberOfInstallmentsField = value;
            }
        }

        /// <remarks/>
        public string encriptedCreditCardNumber
        {
            get
            {
                return this.encriptedCreditCardNumberField;
            }
            set
            {
                this.encriptedCreditCardNumberField = value;
            }
        }

        /// <remarks/>
        public string encriptedExpirationDate
        {
            get
            {
                return this.encriptedExpirationDateField;
            }
            set
            {
                this.encriptedExpirationDateField = value;
            }
        }

        /// <remarks/>
        public string encriptedCardSecurityCode
        {
            get
            {
                return this.encriptedCardSecurityCodeField;
            }
            set
            {
                this.encriptedCardSecurityCodeField = value;
            }
        }

        /// <remarks/>
        public string maskedCreditCardNumber
        {
            get
            {
                return this.maskedCreditCardNumberField;
            }
            set
            {
                this.maskedCreditCardNumberField = value;
            }
        }

        /// <remarks/>
        public creditCardPaymentCardVerificationCode cardVerificationCode
        {
            get
            {
                return this.cardVerificationCodeField;
            }
            set
            {
                this.cardVerificationCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/order")]
    public enum creditCardPaymentCardVerificationCode
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class payment
    {

        private abstractPayment itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("abstractPayment", typeof(abstractPayment))]
        [System.Xml.Serialization.XmlElementAttribute("cieloPayment", typeof(cieloPayment))]
        [System.Xml.Serialization.XmlElementAttribute("creditCardPayment", typeof(creditCardPayment))]
        [System.Xml.Serialization.XmlElementAttribute("sipsPayment", typeof(sipsPayment))]
        public abstractPayment Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class customerLoyalty
    {

        private string providerIdField;

        private string affiliationIdField;

        /// <remarks/>
        public string providerId
        {
            get
            {
                return this.providerIdField;
            }
            set
            {
                this.providerIdField = value;
            }
        }

        /// <remarks/>
        public string affiliationId
        {
            get
            {
                return this.affiliationIdField;
            }
            set
            {
                this.affiliationIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class customer
    {

        private string customerIdField;

        private string documentNumberField;

        private string customerClassificationField;

        private customerCustomerType customerTypeField;

        private string nameField;

        private string lastNameField;

        private string emailField;

        private string phoneMobileField;

        private string phoneHomeField;

        private string phoneOfficeField;

        private customerGender genderField;

        private System.DateTime birthDtField;

        private bool birthDtFieldSpecified;

        private string state_subscriptionField;

        private string representativeNmField;

        private System.DateTime createDtField;

        private System.DateTime updateDtField;

        private string titleField;

        private bool optInField;

        private bool optInFieldSpecified;

        private bool partnerOptInField;

        private bool partnerOptInFieldSpecified;

        private bool smsOptInField;

        private bool smsOptInFieldSpecified;

        private bool smsTrackingOptInField;

        private bool smsTrackingOptInFieldSpecified;

        private customerLoyalty[] customerLoyaltyListField;

        private string documentRegisterNumberField;

        private System.DateTime documentRegisterEmissionDateField;

        private bool documentRegisterEmissionDateFieldSpecified;

        private string documentRegisterEmissorField;

        private string occupationField;

        private string salaryRangeField;

        private string maritalStatusField;

        private string nationalityField;

        private string motherNameField;

        private string guestField;

        /// <remarks/>
        public string customerId
        {
            get
            {
                return this.customerIdField;
            }
            set
            {
                this.customerIdField = value;
            }
        }

        /// <remarks/>
        public string documentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }

        /// <remarks/>
        public string customerClassification
        {
            get
            {
                return this.customerClassificationField;
            }
            set
            {
                this.customerClassificationField = value;
            }
        }

        /// <remarks/>
        public customerCustomerType customerType
        {
            get
            {
                return this.customerTypeField;
            }
            set
            {
                this.customerTypeField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string lastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        public string phoneMobile
        {
            get
            {
                return this.phoneMobileField;
            }
            set
            {
                this.phoneMobileField = value;
            }
        }

        /// <remarks/>
        public string phoneHome
        {
            get
            {
                return this.phoneHomeField;
            }
            set
            {
                this.phoneHomeField = value;
            }
        }

        /// <remarks/>
        public string phoneOffice
        {
            get
            {
                return this.phoneOfficeField;
            }
            set
            {
                this.phoneOfficeField = value;
            }
        }

        /// <remarks/>
        public customerGender gender
        {
            get
            {
                return this.genderField;
            }
            set
            {
                this.genderField = value;
            }
        }

        /// <remarks/>
        public System.DateTime birthDt
        {
            get
            {
                return this.birthDtField;
            }
            set
            {
                this.birthDtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool birthDtSpecified
        {
            get
            {
                return this.birthDtFieldSpecified;
            }
            set
            {
                this.birthDtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string state_subscription
        {
            get
            {
                return this.state_subscriptionField;
            }
            set
            {
                this.state_subscriptionField = value;
            }
        }

        /// <remarks/>
        public string representativeNm
        {
            get
            {
                return this.representativeNmField;
            }
            set
            {
                this.representativeNmField = value;
            }
        }

        /// <remarks/>
        public System.DateTime createDt
        {
            get
            {
                return this.createDtField;
            }
            set
            {
                this.createDtField = value;
            }
        }

        /// <remarks/>
        public System.DateTime updateDt
        {
            get
            {
                return this.updateDtField;
            }
            set
            {
                this.updateDtField = value;
            }
        }

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public bool optIn
        {
            get
            {
                return this.optInField;
            }
            set
            {
                this.optInField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool optInSpecified
        {
            get
            {
                return this.optInFieldSpecified;
            }
            set
            {
                this.optInFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool partnerOptIn
        {
            get
            {
                return this.partnerOptInField;
            }
            set
            {
                this.partnerOptInField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool partnerOptInSpecified
        {
            get
            {
                return this.partnerOptInFieldSpecified;
            }
            set
            {
                this.partnerOptInFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool smsOptIn
        {
            get
            {
                return this.smsOptInField;
            }
            set
            {
                this.smsOptInField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool smsOptInSpecified
        {
            get
            {
                return this.smsOptInFieldSpecified;
            }
            set
            {
                this.smsOptInFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool smsTrackingOptIn
        {
            get
            {
                return this.smsTrackingOptInField;
            }
            set
            {
                this.smsTrackingOptInField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool smsTrackingOptInSpecified
        {
            get
            {
                return this.smsTrackingOptInFieldSpecified;
            }
            set
            {
                this.smsTrackingOptInFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public customerLoyalty[] customerLoyaltyList
        {
            get
            {
                return this.customerLoyaltyListField;
            }
            set
            {
                this.customerLoyaltyListField = value;
            }
        }

        /// <remarks/>
        public string documentRegisterNumber
        {
            get
            {
                return this.documentRegisterNumberField;
            }
            set
            {
                this.documentRegisterNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime documentRegisterEmissionDate
        {
            get
            {
                return this.documentRegisterEmissionDateField;
            }
            set
            {
                this.documentRegisterEmissionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool documentRegisterEmissionDateSpecified
        {
            get
            {
                return this.documentRegisterEmissionDateFieldSpecified;
            }
            set
            {
                this.documentRegisterEmissionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string documentRegisterEmissor
        {
            get
            {
                return this.documentRegisterEmissorField;
            }
            set
            {
                this.documentRegisterEmissorField = value;
            }
        }

        /// <remarks/>
        public string occupation
        {
            get
            {
                return this.occupationField;
            }
            set
            {
                this.occupationField = value;
            }
        }

        /// <remarks/>
        public string salaryRange
        {
            get
            {
                return this.salaryRangeField;
            }
            set
            {
                this.salaryRangeField = value;
            }
        }

        /// <remarks/>
        public string maritalStatus
        {
            get
            {
                return this.maritalStatusField;
            }
            set
            {
                this.maritalStatusField = value;
            }
        }

        /// <remarks/>
        public string nationality
        {
            get
            {
                return this.nationalityField;
            }
            set
            {
                this.nationalityField = value;
            }
        }

        /// <remarks/>
        public string motherName
        {
            get
            {
                return this.motherNameField;
            }
            set
            {
                this.motherNameField = value;
            }
        }

        /// <remarks/>
        public string guest
        {
            get
            {
                return this.guestField;
            }
            set
            {
                this.guestField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/order")]
    public enum customerCustomerType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/order")]
    public enum customerGender
    {

        /// <remarks/>
        M,

        /// <remarks/>
        F,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class orderProperty
    {

        private string orderPropertyIdField;

        private string orderPropertyValueField;

        /// <remarks/>
        public string orderPropertyId
        {
            get
            {
                return this.orderPropertyIdField;
            }
            set
            {
                this.orderPropertyIdField = value;
            }
        }

        /// <remarks/>
        public string orderPropertyValue
        {
            get
            {
                return this.orderPropertyValueField;
            }
            set
            {
                this.orderPropertyValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class slotInfo
    {

        private string slotIdField;

        //private System.DateTime pickupStartDateField;
        private string pickupStartDateField;

        private System.DateTime pickupEndDateField;

        private bool pickupEndDateFieldSpecified;

        private double actualAmountField;

        private bool actualAmountFieldSpecified;

        private double chargedAmountField;

        private bool chargedAmountFieldSpecified;

        private string nameField;

        private string sourceNameField;

        private promotion[] promotionListField;

        /// <remarks/>
        public string slotId
        {
            get
            {
                return this.slotIdField;
            }
            set
            {
                this.slotIdField = value;
            }
        }

        /// <remarks/>
        //public System.DateTime pickupStartDate
        //{
        //    get
        //    {
        //        return this.pickupStartDateField;
        //    }
        //    set
        //    {
        //        this.pickupStartDateField = value;
        //    }
        //}

        public string pickupStartDate
        {
            get
            {
                return this.pickupStartDateField;
            }
            set
            {
                this.pickupStartDateField = value;
            }
        }


        /// <remarks/>
        public System.DateTime pickupEndDate
        {
            get
            {
                return this.pickupEndDateField;
            }
            set
            {
                this.pickupEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool pickupEndDateSpecified
        {
            get
            {
                return this.pickupEndDateFieldSpecified;
            }
            set
            {
                this.pickupEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public double actualAmount
        {
            get
            {
                return this.actualAmountField;
            }
            set
            {
                this.actualAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool actualAmountSpecified
        {
            get
            {
                return this.actualAmountFieldSpecified;
            }
            set
            {
                this.actualAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public double chargedAmount
        {
            get
            {
                return this.chargedAmountField;
            }
            set
            {
                this.chargedAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool chargedAmountSpecified
        {
            get
            {
                return this.chargedAmountFieldSpecified;
            }
            set
            {
                this.chargedAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string sourceName
        {
            get
            {
                return this.sourceNameField;
            }
            set
            {
                this.sourceNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public promotion[] promotionList
        {
            get
            {
                return this.promotionListField;
            }
            set
            {
                this.promotionListField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(loyaltyCredit))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class promotion
    {

        private string promotionIdField;

        private string promotionNameField;

        private System.DateTime promotionStartDateField;

        private bool promotionStartDateFieldSpecified;

        private System.DateTime promotionEndDateField;

        private bool promotionEndDateFieldSpecified;

        private string campaignIdField;

        private string labelField;

        private string typeField;

        private decimal amountField;

        private decimal roundingAmountField;

        private int minimumQuantityField;

        private bool minimumQuantityFieldSpecified;

        private bool paidBySupplierField;

        private bool paidBySupplierFieldSpecified;

        private bool couponPromoField;

        private bool couponPromoFieldSpecified;

        private string giftSkuTriggerField;

        private string skuNameField;


        /// <remarks/>
        public string promotionId
        {
            get
            {
                return this.promotionIdField;
            }
            set
            {
                this.promotionIdField = value;
            }
        }

        /// <remarks/>
        public string promotionName
        {
            get
            {
                return this.promotionNameField;
            }
            set
            {
                this.promotionNameField = value;
            }
        }

        /// <remarks/>
        public System.DateTime promotionStartDate
        {
            get
            {
                return this.promotionStartDateField;
            }
            set
            {
                this.promotionStartDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool promotionStartDateSpecified
        {
            get
            {
                return this.promotionStartDateFieldSpecified;
            }
            set
            {
                this.promotionStartDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime promotionEndDate
        {
            get
            {
                return this.promotionEndDateField;
            }
            set
            {
                this.promotionEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool promotionEndDateSpecified
        {
            get
            {
                return this.promotionEndDateFieldSpecified;
            }
            set
            {
                this.promotionEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string campaignId
        {
            get
            {
                return this.campaignIdField;
            }
            set
            {
                this.campaignIdField = value;
            }
        }

        /// <remarks/>
        public string label
        {
            get
            {
                return this.labelField;
            }
            set
            {
                this.labelField = value;
            }
        }

        /// <remarks/>
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public decimal amount
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
        public decimal roundingAmount
        {
            get
            {
                return this.roundingAmountField;
            }
            set
            {
                this.roundingAmountField = value;
            }
        }

        /// <remarks/>
        public int minimumQuantity
        {
            get
            {
                return this.minimumQuantityField;
            }
            set
            {
                this.minimumQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minimumQuantitySpecified
        {
            get
            {
                return this.minimumQuantityFieldSpecified;
            }
            set
            {
                this.minimumQuantityFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool paidBySupplier
        {
            get
            {
                return this.paidBySupplierField;
            }
            set
            {
                this.paidBySupplierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool paidBySupplierSpecified
        {
            get
            {
                return this.paidBySupplierFieldSpecified;
            }
            set
            {
                this.paidBySupplierFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool couponPromo
        {
            get
            {
                return this.couponPromoField;
            }
            set
            {
                this.couponPromoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool couponPromoSpecified
        {
            get
            {
                return this.couponPromoFieldSpecified;
            }
            set
            {
                this.couponPromoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string giftSkuTrigger
        {
            get
            {
                return this.giftSkuTriggerField;
            }
            set
            {
                this.giftSkuTriggerField = value;
            }
        }

        public string skuNameTrigger
        {
            get
            {
                return this.skuNameTrigger;
            }
            set
            {
                this.skuNameTrigger = value;
            }
        }


    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class loyaltyCredit : promotion
    {

        private int daysField;

        private bool daysFieldSpecified;

        /// <remarks/>
        public int days
        {
            get
            {
                return this.daysField;
            }
            set
            {
                this.daysField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool daysSpecified
        {
            get
            {
                return this.daysFieldSpecified;
            }
            set
            {
                this.daysFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class deliveryProperty
    {

        private string deliveryPropertyIdField;

        private string deliveryPropertyValueField;

        /// <remarks/>
        public string deliveryPropertyId
        {
            get
            {
                return this.deliveryPropertyIdField;
            }
            set
            {
                this.deliveryPropertyIdField = value;
            }
        }

        /// <remarks/>
        public string deliveryPropertyValue
        {
            get
            {
                return this.deliveryPropertyValueField;
            }
            set
            {
                this.deliveryPropertyValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class address
    {

        private string addressIdField;

        private addressAddressType addressTypeField;

        private string recipientNmField;

        private string address1Field;

        private string addressNrField;

        private string additionalInfoField;

        private string quarterField;

        private string cityField;

        private string stateField;

        private string countryIdField;

        private string postalCdField;

        private string postalCdIntlField;

        private string referenceField;

        private string friendlyNmField;

        private System.DateTime createDtField;

        private bool createDtFieldSpecified;

        private string addressExtraField1Field;

        private string addressExtraField2Field;

        private string addressExtraField3Field;

        private string buildingNmField;

        private string floorField;

        private string digicodeField;

        private bool liftField;

        private bool liftFieldSpecified;

        private bool intercomField;

        private bool intercomFieldSpecified;

        /// <remarks/>
        public string addressId
        {
            get
            {
                return this.addressIdField;
            }
            set
            {
                this.addressIdField = value;
            }
        }

        /// <remarks/>
        public addressAddressType addressType
        {
            get
            {
                return this.addressTypeField;
            }
            set
            {
                this.addressTypeField = value;
            }
        }

        /// <remarks/>
        public string recipientNm
        {
            get
            {
                return this.recipientNmField;
            }
            set
            {
                this.recipientNmField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("address")]
        public string address1
        {
            get
            {
                return this.address1Field;
            }
            set
            {
                this.address1Field = value;
            }
        }

        /// <remarks/>
        public string addressNr
        {
            get
            {
                return this.addressNrField;
            }
            set
            {
                this.addressNrField = value;
            }
        }

        /// <remarks/>
        public string additionalInfo
        {
            get
            {
                return this.additionalInfoField;
            }
            set
            {
                this.additionalInfoField = value;
            }
        }

        /// <remarks/>
        public string quarter
        {
            get
            {
                return this.quarterField;
            }
            set
            {
                this.quarterField = value;
            }
        }

        /// <remarks/>
        public string city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public string countryId
        {
            get
            {
                return this.countryIdField;
            }
            set
            {
                this.countryIdField = value;
            }
        }

        /// <remarks/>
        public string postalCd
        {
            get
            {
                return this.postalCdField;
            }
            set
            {
                this.postalCdField = value;
            }
        }

        /// <remarks/>
        public string postalCdIntl
        {
            get
            {
                return this.postalCdIntlField;
            }
            set
            {
                this.postalCdIntlField = value;
            }
        }

        /// <remarks/>
        public string reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        public string friendlyNm
        {
            get
            {
                return this.friendlyNmField;
            }
            set
            {
                this.friendlyNmField = value;
            }
        }

        /// <remarks/>
        public System.DateTime createDt
        {
            get
            {
                return this.createDtField;
            }
            set
            {
                this.createDtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool createDtSpecified
        {
            get
            {
                return this.createDtFieldSpecified;
            }
            set
            {
                this.createDtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string addressExtraField1
        {
            get
            {
                return this.addressExtraField1Field;
            }
            set
            {
                this.addressExtraField1Field = value;
            }
        }

        /// <remarks/>
        public string addressExtraField2
        {
            get
            {
                return this.addressExtraField2Field;
            }
            set
            {
                this.addressExtraField2Field = value;
            }
        }

        /// <remarks/>
        public string addressExtraField3
        {
            get
            {
                return this.addressExtraField3Field;
            }
            set
            {
                this.addressExtraField3Field = value;
            }
        }

        /// <remarks/>
        public string buildingNm
        {
            get
            {
                return this.buildingNmField;
            }
            set
            {
                this.buildingNmField = value;
            }
        }

        /// <remarks/>
        public string floor
        {
            get
            {
                return this.floorField;
            }
            set
            {
                this.floorField = value;
            }
        }

        /// <remarks/>
        public string digicode
        {
            get
            {
                return this.digicodeField;
            }
            set
            {
                this.digicodeField = value;
            }
        }

        /// <remarks/>
        public bool lift
        {
            get
            {
                return this.liftField;
            }
            set
            {
                this.liftField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool liftSpecified
        {
            get
            {
                return this.liftFieldSpecified;
            }
            set
            {
                this.liftFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool intercom
        {
            get
            {
                return this.intercomField;
            }
            set
            {
                this.intercomField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool intercomSpecified
        {
            get
            {
                return this.intercomFieldSpecified;
            }
            set
            {
                this.intercomFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/order")]
    public enum addressAddressType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class skuDeliveryTracking
    {

        private string skuIdField;

        private string skuNameField;

        private long preparedQtField;

        private decimal unitPriceField;

        private bool unitPriceFieldSpecified;

        private decimal totalPriceField;

        private bool totalPriceFieldSpecified;

        /// <remarks/>
        public string skuId
        {
            get
            {
                return this.skuIdField;
            }
            set
            {
                this.skuIdField = value;
            }
        }

        /// <remarks/>
        public string skuName
        {
            get
            {
                return this.skuNameField;
            }
            set
            {
                this.skuNameField = value;
            }
        }

        /// <remarks/>
        public long preparedQt
        {
            get
            {
                return this.preparedQtField;
            }
            set
            {
                this.preparedQtField = value;
            }
        }

        /// <remarks/>
        public decimal unitPrice
        {
            get
            {
                return this.unitPriceField;
            }
            set
            {
                this.unitPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool unitPriceSpecified
        {
            get
            {
                return this.unitPriceFieldSpecified;
            }
            set
            {
                this.unitPriceFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal totalPrice
        {
            get
            {
                return this.totalPriceField;
            }
            set
            {
                this.totalPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalPriceSpecified
        {
            get
            {
                return this.totalPriceFieldSpecified;
            }
            set
            {
                this.totalPriceFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class trackingProperty
    {

        private string trackingPropertyIdField;

        private string trackingPropertyValueField;

        /// <remarks/>
        public string trackingPropertyId
        {
            get
            {
                return this.trackingPropertyIdField;
            }
            set
            {
                this.trackingPropertyIdField = value;
            }
        }

        /// <remarks/>
        public string trackingPropertyValue
        {
            get
            {
                return this.trackingPropertyValueField;
            }
            set
            {
                this.trackingPropertyValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/InvoiceInfo")]
    public partial class ObjectData
    {

        private string objectIdField;

        /// <remarks/>
        public string objectId
        {
            get
            {
                return this.objectIdField;
            }
            set
            {
                this.objectIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/InvoiceInfo")]
    public partial class InvoiceInfo
    {

        private long issuerDocumentNrField;

        private bool issuerDocumentNrFieldSpecified;

        private long invoiceNumberField;

        private bool invoiceNumberFieldSpecified;

        private string invoiceSerialNumberField;

        private System.DateTime invoiceEmissionDateField;

        private bool invoiceEmissionDateFieldSpecified;

        private string invoiceEletronicKeyField;

        private ObjectData[] objectDataListField;

        /// <remarks/>
        public long issuerDocumentNr
        {
            get
            {
                return this.issuerDocumentNrField;
            }
            set
            {
                this.issuerDocumentNrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool issuerDocumentNrSpecified
        {
            get
            {
                return this.issuerDocumentNrFieldSpecified;
            }
            set
            {
                this.issuerDocumentNrFieldSpecified = value;
            }
        }

        /// <remarks/>
        public long invoiceNumber
        {
            get
            {
                return this.invoiceNumberField;
            }
            set
            {
                this.invoiceNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool invoiceNumberSpecified
        {
            get
            {
                return this.invoiceNumberFieldSpecified;
            }
            set
            {
                this.invoiceNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string invoiceSerialNumber
        {
            get
            {
                return this.invoiceSerialNumberField;
            }
            set
            {
                this.invoiceSerialNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime invoiceEmissionDate
        {
            get
            {
                return this.invoiceEmissionDateField;
            }
            set
            {
                this.invoiceEmissionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool invoiceEmissionDateSpecified
        {
            get
            {
                return this.invoiceEmissionDateFieldSpecified;
            }
            set
            {
                this.invoiceEmissionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string invoiceEletronicKey
        {
            get
            {
                return this.invoiceEletronicKeyField;
            }
            set
            {
                this.invoiceEletronicKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("objectData", IsNullable = false)]
        public ObjectData[] objectDataList
        {
            get
            {
                return this.objectDataListField;
            }
            set
            {
                this.objectDataListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class tracking
    {

        private string trackingIdField;

        private string trackingCodeField;

        private System.DateTime trackingDateField;

        private string trackingStepField;

        private string trackingTitleField;

        private string trackingMessageField;

        private string trackingStepStatusField;

        private System.DateTime adjustedDeliveryDtField;

        private bool adjustedDeliveryDtFieldSpecified;

        private string carrierURLField;

        private string invoiceURLField;

        private InvoiceInfo invoiceInfoField;

        private trackingProperty[] trackingPropertyListField;

        /// <remarks/>
        public string trackingId
        {
            get
            {
                return this.trackingIdField;
            }
            set
            {
                this.trackingIdField = value;
            }
        }

        /// <remarks/>
        public string trackingCode
        {
            get
            {
                return this.trackingCodeField;
            }
            set
            {
                this.trackingCodeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime trackingDate
        {
            get
            {
                return this.trackingDateField;
            }
            set
            {
                this.trackingDateField = value;
            }
        }

        /// <remarks/>
        public string trackingStep
        {
            get
            {
                return this.trackingStepField;
            }
            set
            {
                this.trackingStepField = value;
            }
        }

        /// <remarks/>
        public string trackingTitle
        {
            get
            {
                return this.trackingTitleField;
            }
            set
            {
                this.trackingTitleField = value;
            }
        }

        /// <remarks/>
        public string trackingMessage
        {
            get
            {
                return this.trackingMessageField;
            }
            set
            {
                this.trackingMessageField = value;
            }
        }

        /// <remarks/>
        public string trackingStepStatus
        {
            get
            {
                return this.trackingStepStatusField;
            }
            set
            {
                this.trackingStepStatusField = value;
            }
        }

        /// <remarks/>
        public System.DateTime adjustedDeliveryDt
        {
            get
            {
                return this.adjustedDeliveryDtField;
            }
            set
            {
                this.adjustedDeliveryDtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool adjustedDeliveryDtSpecified
        {
            get
            {
                return this.adjustedDeliveryDtFieldSpecified;
            }
            set
            {
                this.adjustedDeliveryDtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string carrierURL
        {
            get
            {
                return this.carrierURLField;
            }
            set
            {
                this.carrierURLField = value;
            }
        }

        /// <remarks/>
        public string invoiceURL
        {
            get
            {
                return this.invoiceURLField;
            }
            set
            {
                this.invoiceURLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.accurate.com/acec/InvoiceInfo")]
        public InvoiceInfo invoiceInfo
        {
            get
            {
                return this.invoiceInfoField;
            }
            set
            {
                this.invoiceInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public trackingProperty[] trackingPropertyList
        {
            get
            {
                return this.trackingPropertyListField;
            }
            set
            {
                this.trackingPropertyListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class level
    {

        private string levelIdField;

        private string levelNameField;

        /// <remarks/>
        public string levelId
        {
            get
            {
                return this.levelIdField;
            }
            set
            {
                this.levelIdField = value;
            }
        }

        /// <remarks/>
        public string levelName
        {
            get
            {
                return this.levelNameField;
            }
            set
            {
                this.levelNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class orderLineProperty
    {

        private string orderLinePropertyIdField;

        private string orderLinePropertyValueField;

        /// <remarks/>
        public string orderLinePropertyId
        {
            get
            {
                return this.orderLinePropertyIdField;
            }
            set
            {
                this.orderLinePropertyIdField = value;
            }
        }

        /// <remarks/>
        public string orderLinePropertyValue
        {
            get
            {
                return this.orderLinePropertyValueField;
            }
            set
            {
                this.orderLinePropertyValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class giftWrap
    {

        private string fromField;

        private string toField;

        private string giftWrapTypeField;

        private decimal giftWrapPriceField;

        /// <remarks/>
        public string from
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
            }
        }

        /// <remarks/>
        public string to
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
            }
        }

        /// <remarks/>
        public string giftWrapType
        {
            get
            {
                return this.giftWrapTypeField;
            }
            set
            {
                this.giftWrapTypeField = value;
            }
        }

        /// <remarks/>
        public decimal giftWrapPrice
        {
            get
            {
                return this.giftWrapPriceField;
            }
            set
            {
                this.giftWrapPriceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class giftCard
    {

        private string fromField;

        private string toField;

        private string messageField;

        /// <remarks/>
        public string from
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
            }
        }

        /// <remarks/>
        public string to
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/order")]
    public partial class freight
    {

        private decimal chargedAmountField;

        private bool chargedAmountFieldSpecified;

        private decimal actualAmountField;

        private bool actualAmountFieldSpecified;

        private decimal commercialAmountField;

        private bool commercialAmountFieldSpecified;

        private string freightTimeField;

        private string pickupLeadTimeField;

        private string logisticContractField;

        private string expectedDeliveryDateField;
        //        private System.DateTime expectedDeliveryDateField;

        private System.DateTime adjustedDeliveryDateField;

        private bool adjustedDeliveryDateFieldSpecified;

        /// <remarks/>
        public decimal chargedAmount
        {
            get
            {
                return this.chargedAmountField;
            }
            set
            {
                this.chargedAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool chargedAmountSpecified
        {
            get
            {
                return this.chargedAmountFieldSpecified;
            }
            set
            {
                this.chargedAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal actualAmount
        {
            get
            {
                return this.actualAmountField;
            }
            set
            {
                this.actualAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool actualAmountSpecified
        {
            get
            {
                return this.actualAmountFieldSpecified;
            }
            set
            {
                this.actualAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal commercialAmount
        {
            get
            {
                return this.commercialAmountField;
            }
            set
            {
                this.commercialAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool commercialAmountSpecified
        {
            get
            {
                return this.commercialAmountFieldSpecified;
            }
            set
            {
                this.commercialAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string freightTime
        {
            get
            {
                return this.freightTimeField;
            }
            set
            {
                this.freightTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string pickupLeadTime
        {
            get
            {
                return this.pickupLeadTimeField;
            }
            set
            {
                this.pickupLeadTimeField = value;
            }
        }

        /// <remarks/>
        public string logisticContract
        {
            get
            {
                return this.logisticContractField;
            }
            set
            {
                this.logisticContractField = value;
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        //public System.DateTime expectedDeliveryDate
        //{
        //    get
        //    {
        //        return this.expectedDeliveryDateField;
        //    }
        //    set
        //    {
        //        this.expectedDeliveryDateField = value;
        //    }
        //}

        [System.Xml.Serialization.XmlElementAttribute(DataType = "string")]
        public string expectedDeliveryDate
        {
            get
            {
                return this.expectedDeliveryDateField;
            }
            set
            {
                this.expectedDeliveryDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime adjustedDeliveryDate
        {
            get
            {
                return this.adjustedDeliveryDateField;
            }
            set
            {
                this.adjustedDeliveryDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool adjustedDeliveryDateSpecified
        {
            get
            {
                return this.adjustedDeliveryDateFieldSpecified;
            }
            set
            {
                this.adjustedDeliveryDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/AcecBOSOAIntegration/BOOrderIntegration")]
    public partial class integrateOrderResponse
    {

        private string statusField;

        private string messageField;

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
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void integrateOrderCompletedEventHandler(object sender, integrateOrderCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class integrateOrderCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal integrateOrderCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public integrateOrderResponse Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((integrateOrderResponse)(this.results[0]));
            }
        }
    }





    /*
        public class Order
        {
            public string orderId { get; set; }
            public decimal totalAmount { get; set; }
            public decimal totalDiscountAmount { get; set; }
            public DateTime purchaseDate { get; set; }
            public DateTime? sessionCreationDate { get; set; }
            public string applicationVersion { get; set; }
            public string listId { get; set; }
            public string saleChannel { get; set; }
           public  List<Delivery> deliveries { get; set; }
            public Customer customer { get; set; }
            public Address billingAddress { get; set; }
            public List<Payment> paymentList { get; set; }
            public Decimal freightChargedAmount { get; set; }
            public Decimal freightActualAmount { get; set; }
            public int countDistinctSku { get; set; }
        }

        public class Customer
        {
            public string documentNr { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string stateSubscription { get; set; }
            public string representativeNm { get; set; }
            public DateTime? createDate { get; set; }
            public List<Address> addressList { get; set; }
            public List<Phone> phoneList { get; set; }
        }

        public class Address
        {
            public string recipientNm { get; set; }
            public string address { get; set; }
            public string addressNr { get; set; }
            public string additionalInfo { get; set; }
            public string quarter { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postalCd { get; set; }
        }

        public class Phone
        {
            public int phoneTp { get; set; }
            public string areaCd { get; set; }
            public long phoneNr { get; set; }

        }

        public class Delivery
        {
            public string orderId { get; set; }
            public string deliveryId { get; set; }
            public Decimal totalAmount { get; set; }
            public Decimal totalDiscountAmount { get; set; }
            public List<OrderLine> orderLineList { get; set; }
            public Freight freightAmount { get; set; }
            public Address deliveryAddress { get; set; }
        }

        public class OrderLine
        {
            public string sku { get; set; }
            public string skuType { get; set; }
            public int quantity { get; set; }
            public decimal catalogListPrice { get; set; }
            public decimal listPrice { get; set; }
            public decimal salePrice { get; set; }
            public decimal unconditionalDiscountAmount { get; set; }
            public decimal conditionalDiscountAmount { get; set; }
            public decimal roundingDiscountAmount { get; set; }
            public List<Promotion> promotionList { get; set; }
            Freight freight { get; set; }
            public string kitSkuId { get; set; }
            public string kitSkuName { get; set; }
            public int kitQuantity { get; set; }
            List<string> reference { get; set; }
            public bool service { get; set; }
            public string skuName { get; set; }

        }

        public class Freight
        {
            public decimal chargedAmount { get; set; }
            public decimal actualAmount { get; set; }
            public decimal commercialAmount { get; set; }
            public int freightTime { get; set; }
            public int pickupLeadTime { get; set; }
            public string logisticContract { get; set; }
            public DateTime expectedDeliveryDate { get; set; }
            public DateTime adjustedDeliveryDate { get; set; }
        }

        public class Payment
        {
            public string paymentType { get; set; }
            public decimal value { get; set; }
        }

        public class Promotion
        {
            public string promotionId { get; set; }
            public string promotionName { get; set; }
            public DateTime? promotionStartDate { get; set; }
            public DateTime? promotionEndDate { get; set; }
            public string campaignId { get; set; }
            public string label { get; set; }
            public string type { get; set; }
            public decimal amount { get; set; }
            public decimal roundingAmount { get; set; }
            public int minimumQuantity { get; set; }
            public bool couponPromo { get; set; }
            public string giftSkuTrigger { get; set; }
        }

        public struct notifyCustomerCreationResponse
        {
            public string status { get; set; }
        }

        public struct integrateOrderResponse
        {
            public string status { get; set; }
            public string message { get; set; }
        }

        public struct confirmPaymentResponse
        {
            public bool success  { get; set; }
        }

        public class confirmPayment
        {
          public int orderId { get; set; }
          public string  status { get; set; }  
             
        }

        //public class Stock
        //{
        //    public string skuId { get; set; }
        //    public int stockType { get; set; }         
 
        //}

        //public struct setStockResponse
        //{
        //    public string  result { get; set; }         

        //}

        //public class Tracking
        //{
        //    public string orderId { get; set; }
        //    public string controlPointId { get; set; }
        //    public string controlPointNm { get; set; }
        //    public DateTime occurrenceDt { get; set; }
        //    public string invoiceUrl { get; set; }
        //    public InvoiceInfo invoiceInfo { get; set; }
        //    public List<SkuDeliveryTracking> skuDeliveryTrackingList { get; set; }
        //}

        //public class InvoiceInfo
        //{
        //    public string issuerDocumentNr { get; set; }
        //    public string invoiceNumber { get; set; }
        //    public string invoiceSerialNumber { get; set; }
        //    public DateTime invoiceEmissionDate { get; set; }
        //    public string  invoiceEletronicKey { get; set; }
        //    public List<ObjectData> objectDataList  { get; set; }
        //}

        //public class ObjectData
        //{
        //    public string objectId { get; set; }
        //}

        //public class SkuDeliveryTracking
        //{
        //    public string skuId { get; set; }
        //    public decimal unitPrice { get; set; }
        //    public int preparedQt { get; set; }
        //    public string kitSkuId { get; set; }
        //}

        //public class SkuDeliveryTrackingList
        //{
        //    public List<SkuDeliveryTracking> skuDeliveryTracking  { get; set; }
        //}

        //public struct captureTrackingResponse
        //{
        //    public bool success  { get; set; }
        //}
    //}*/

}