using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;

namespace EnvioDeRota
{
    public partial class frmEnvioDeRota : Form
    {
        public frmEnvioDeRota()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            string iddt = "";

            //if(DateTime.Now.Minute % 5 ==0)
            //    EnviarComprovei();

            try
            {
                textBox1.Text = "Enviar Rotas: " + DateTime.Now.ToString();
                Application.DoEvents();

                //string x = "SELECT TOP 100 DT.IDDT, dt.* FROM DT with (nolock) ";
                //x += " INNER JOIN DTROMANEIO DTR with (nolock) ON DTR.IDDT = DT.IDDT ";
                //x += " INNER JOIN ROMANEIO ROM with (nolock) ON ROM.IDROMANEIO = DTR.IDROMANEIO ";              
                //x += " where ROM.IDFILIAL  NOT IN(48, 15,30,54,49,27, 52) ";
                //x += " AND (DT.ROTAENVIADACOMPROVEI IS NULL or DT.ROTAENVIADACOMPROVEI like '%driver does not exist!%') ";  
                //x += " and  DT.IdFilial in(select idfilial from FILIALENVIAROTASCOMPROVEI)";
                //x += " AND (dt.DataDesaida >= GETDATE()-5) AND Ativo = 'SIM' ";
                //x += " AND ROM.Tipo = 'ENTREGA'";
                string x = "exec PRC_DtsLiberadasParaEnvioComprovei";
                
                
                DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                Application.DoEvents();
                const string quote = "\"";


                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

                for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
                {
                    DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                    iddt = dtDTs.Rows[dts]["IdDt"].ToString();

                    if (dtGeral.Rows.Count == 0)
                        continue;

                    DataView view = new DataView(dtGeral);
                    DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
                    string NomeArquivo = "";
                    int NumeroParada = 1;

                    string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

                    string xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
                    xml += "<Rotas>";
                    // xml += "<Rota numero=" + quote + NomeRota + quote + ">";
                    //  xml += "<Rota numero=" + quote + dtGeral.Rows[0]["NUMEROROTA"].ToString() + quote + " Regiao=" + quote + NomeRota + quote + ">";
                    xml += "<Rota numero=" + quote + dtGeral.Rows[0]["NUMERO"].ToString() + quote + ">";


                    for (int i = 0; i < dtds.Rows.Count; i++)
                    {

                        DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString() + "'" +
                                                        " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString() + "'" +
                                                        " and IDENDERECOBAIRRO=" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString() +
                                                        " and IDENDERECOCIDADE=" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString() +
                                                        " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString() + "'", "");


                        if (i == 0)
                        {                            
                            xml += "<Data>" + DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd") + "</Data>";
                            xml += "<Regiao>" + NomeRota + "</Regiao>";
                            xml += "<Transportadora>";
                            xml += "<Codigo></Codigo>";
                            xml += "<Razao></Razao>";
                            xml += "</Transportadora>";                           
                            xml += "<Motorista>";
                            xml += "<Usuario>" + ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "") + "</Usuario>";
                            xml += "<PlacaVeiculo>" + ret[i]["PLACA"].ToString().Replace("-", "") + "</PlacaVeiculo>";
                            xml += "</Motorista>";
                            xml += "<Paradas>";
                            NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";
                        }


                        for (int ii = 0; ii < ret.Length; ii++)
                        {
                            xml += "<Parada numero=" + quote + NumeroParada + quote + ">";
                            xml += "<Documento>";
                            xml += "<ChaveNota>" + ret[ii]["CHAVE"].ToString() + "</ChaveNota>";
                            xml += "</Documento>";
                            xml += "</Parada>";
                            NumeroParada++;
                        }
                    }
                    xml += "</Paradas>";
                    xml += "</Rota>";
                    xml += "</Rotas>";

                    string auxXML = xml;
                    xml = Base64Encode(xml);

                    //  continue; // retinar na hora de enviar

                    WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
                    string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
                                   "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<SOAP-ENV:Body>" +
                                   "<tns:sendDocsKeysToPOD xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
                                   "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
                                   "</tns:" + "sendDocsKeysToPOD" + ">" +
                                   "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    byte[] data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "text/xml; charset=ISO-8859-1";
                    request.Headers.Add("SOAPAction", "urn:WebServicePOD#sendDocsKeysToPOD");
                    request.Headers.Add("Authorization", "Basic " + auth);
                    request.ContentLength = data.Length;


                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }


                    Cursor.Current = Cursors.WaitCursor;
                    WebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    XmlDocument xmlAwnser = new XmlDocument();
                    xmlAwnser.LoadXml(sr.ReadToEnd());
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                    nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);
                    string retorno = xmlAwnser["status"].InnerText;

                    string sql = "Update Dt Set ROTAENVIADACOMPROVEI='ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno + "' where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
                    sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
                    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    textBox1.Text = "Enviou a Dt: " + dtDTs.Rows[dts]["IDDt"].ToString();
                    

                    //if(retorno.Contains("o existente no sistema"))
                    //{
                    //    PrepararNotasParaReenvio(dtGeral);
                    //    EnviarComprovei();


                    //    #region Reenvia a Rota
                    //    request = (HttpWebRequest)WebRequest.Create(tbUrl);
                    //    postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
                    //                   "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
                    //                   "<SOAP-ENV:Body>" +
                    //                   "<tns:sendDocsKeysToPOD xmlns:tns=\"urn:WebServicePOD\">" +
                    //                   "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
                    //                   "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
                    //                   "</tns:" + "sendDocsKeysToPOD" + ">" +
                    //                   "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    //    byte[] data1 = Encoding.ASCII.GetBytes(postData);
                    //    request.Method = "POST";
                    //    request.ContentType = "text/xml; charset=ISO-8859-1";
                    //    request.Headers.Add("SOAPAction", "urn:WebServicePOD#sendDocsKeysToPOD");
                    //    request.Headers.Add("Authorization", "Basic " + auth);
                    //    request.ContentLength = data1.Length;


                    //    using (var stream = request.GetRequestStream())
                    //    {
                    //        stream.Write(data, 0, data1.Length);
                    //    }


                        
                    //    response = (HttpWebResponse)request.GetResponse();
                    //     sr = new StreamReader(response.GetResponseStream());
                    //     xmlAwnser = new XmlDocument();
                    //    xmlAwnser.LoadXml(sr.ReadToEnd());
                    //    nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                    //    nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    //    xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);
                    //    retorno = xmlAwnser["status"].InnerText;

                    //     sql = "Update Dt Set ROTAENVIADACOMPROVEI='ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno + "' where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
                    //    sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
                    //    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    //    textBox1.Text = "Enviou a Dt: " + dtDTs.Rows[dts]["IDDt"].ToString();
                    //    #endregion
                    //}
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rotas", "iddt:"+ iddt + "- Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
                timer1.Enabled = false;
                timer1.Enabled = true;
                textBox1.Text = "Finalizou as : " + DateTime.Now;
            }
            finally
            {
                timer1.Enabled = false;
                timer1.Enabled = true;

			}
        }

        //private void PrepararNotasParaReenvio(DataTable dtGeral)
        //{
        //    try
        //    {
        //       // var x = dtGeral.Select("Chave='" + chave + "'");
        //        string sql = "";
        //        for (int i = 0; i < dtGeral.Rows.Count; i++)
        //        {
        //            sql += "insert into DocumentoEnviarComprovei values (" + dtGeral.Rows[i]["IdDocumento"] + "); ";
        //        }
        //        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //    }
        //    catch (Exception)
        //    {               
        //    }
        //}

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        


        private void frmEnvioDeRota_Load(object sender, EventArgs e)
        {
			try
			{
				textBox1.Text = "Iniciou as : " + DateTime.Now;
				timer1.Enabled = true;
			}catch(Exception)
			{
				timer1.Enabled = false;
				timer1.Enabled = true;
			}
        }

        //private void EnviarComprovei()
        //{

        //    textBox1.Text = "EnviarComprovei: " + DateTime.Now.ToString();
            
        //    string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
        //    string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

        //    string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin"));

          
        //    DataTable dtGeral = null;           
        //     dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_Reenviar_COMPROVEI", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            


        //    DataView view = new DataView(dtGeral);
        //    DataTable dtds = view.ToTable(true, "IDDOCUMENTO");
        //    const string quote = "\"";
        //    string xml = "";
        //    string ch = "";

        //    // Label2.Text = "Enviando Comprovei";

        //    for (int i = 0; i < dtds.Rows.Count; i++)
        //    {
        //        xml = "";

        //        textBox1.Text = DateTime.Now + "- Enviado " + (i + 1).ToString() + " de " + dtds.Rows.Count + " registros para o comprovei";
        //        Application.DoEvents();

        //        if (i == 1000)
        //            return;

        //        try
        //        {
        //            xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
        //            xml += "<Documentos>";
        //            DataRow[] dr = dtGeral.Select("IDDOCUMENTO=" + dtds.Rows[i]["IDDOCUMENTO"].ToString(), "");

        //            ch = dr[0]["CHAVE"].ToString();

        //            // Label2.Text = "Enviando Comprovei: " + ch;

        //            bool gerouCahve = false;

        //            if (ch.Contains("SERNUMERONOT"))
        //            {
        //                ch = ch.Replace("SER", dr[0]["SERIE"].ToString().PadLeft(3, '0'));
        //                ch = ch.Replace("NUMERONOT", dr[0]["NUMERO"].ToString().PadLeft(9, '0'));

        //                gerouCahve = true;
        //            }



        //            xml += "<Documento>";

        //            if (dr[0]["TIPODEDOCUMENTO"].ToString().ToUpper() == "ORDEM DE SERVICO")
        //            {
        //                xml += "<Tipo>REQ</Tipo>";
        //                xml += "<TipoParada>C</TipoParada>";
        //                xml += "<Modelo>55</Modelo>";
        //            }
        //            else
        //            {
        //                xml += "<Tipo>NFE</Tipo>";
        //                xml += "<TipoParada>E</TipoParada>";
        //                xml += "<Modelo>55</Modelo>";
        //            }

        //            xml += "<Numero>" + dr[0]["NUMERO"].ToString() + "</Numero>";
        //            xml += "<Valor>" + dr[0]["VALORDANOTA"].ToString().Replace(",", ".") + "</Valor>";

        //            if (dr[0]["IDCLIENTE"].ToString() == "65286")
        //            {
        //                //se o cliente for fastshop procura a serie na chave da nota

        //                if (ch.Length == 44)
        //                {
        //                    xml += "<Serie>" + ch.Substring(22, 3) + "</Serie>";
        //                }
        //            }
        //            else
        //                xml += "<Serie>" + (dr[0]["SERIE"].ToString().Trim() == "NFE" ? "001" : dr[0]["SERIE"].ToString().Trim()) + "</Serie>";



        //            xml += "<Emissao>" + DateTime.Parse(dr[0]["DATADEEMISSAO"].ToString()).ToString("yyyyMMdd") + "</Emissao>";
        //            xml += "<Atualizacao>" + DateTime.Parse(dr[0]["ATUALIZACAO"].ToString()).ToString("yyyyMMdd") + "</Atualizacao>";
        //            xml += "<Chave>" + ch + "</Chave>";
        //            xml += "<cnpj>" + dr[0]["CNPJ"].ToString() + "</cnpj>";

        //            xml += "<cnpjEmissor>" + dr[0]["CNPJEMISSOR"].ToString() + "</cnpjEmissor>";
        //            xml += "<cnpjTransportador>" + dr[0]["CNPJTRANSPORTADOR"].ToString() + "</cnpjTransportador>";

        //            xml += "</Documento>";
        //            xml += "<Cliente>";
        //            xml += "<Codigo>" + dr[0]["CODIGOCLIENTE"].ToString() + "</Codigo>";
        //            xml += "<Contato>" + dr[0]["CONTATO"].ToString() + "</Contato>";

        //            //if (dr[0]["TELEFONE"].ToString() == "")
        //            //    xml += "<Telefone>11 9999-9999</Telefone>";
        //            //else
        //            xml += "<Telefone>" + dr[0]["TELEFONE"].ToString() + "</Telefone>";


        //            //if (dr[0]["EMAIL"].ToString() == "")
        //            //    xml += "<Email>moises@sistecno.com.br</Email>";
        //            //else
        //            xml += "<Email>" + dr[0]["EMAIL"].ToString() + "</Email>";


        //            xml += "<Razao>" + dr[0]["RAZAO"].ToString().Replace("&", "") + "</Razao>";
        //            xml += "<Endereco>" + dr[0]["ENDERECO"].ToString() + ", " + dr[0]["NUMERO_END"].ToString() + "</Endereco>";
        //            xml += "<Bairro>" + dr[0]["BAIRRO"].ToString() + "</Bairro>";
        //            xml += "<Cidade>" + dr[0]["CIDADE"].ToString() + "</Cidade>";
        //            xml += "<Estado>" + dr[0]["ESTADO"].ToString() + "</Estado>";
        //            xml += "<Pais>BRASIL</Pais>";
        //            xml += "<CEP>" + dr[0]["CEP"].ToString() + "</CEP>";


        //            xml += "<Regiao>" + dr[0]["REGIAO"].ToString() + "</Regiao>";
        //            xml += "<TipoCliente></TipoCliente>";
        //            xml += "<Mensagem></Mensagem>";
        //            xml += "</Cliente>";

        //            xml += "<SKUs>";
        //            bool jaInseriu = false;

        //            for (int ii = 0; ii < dr.Length; ii++)
        //            {
        //                if (dr[ii]["CODIGOPR"].ToString() != "")
        //                {
        //                    xml += "<SKU codigo=" + quote + dr[ii]["CODIGOPR"].ToString() + quote + ">";
        //                    xml += "<PesoBruto>" + dr[ii]["PESOBRUTO"].ToString() + "</PesoBruto>";
        //                    xml += "<PesoLiquido>" + dr[ii]["PESOLIQUIDO"].ToString() + "</PesoLiquido>";
        //                    xml += "<Volumes>" + dr[ii]["VOLUMES"].ToString() + "</Volumes>";
        //                    xml += "<Descricao>" + dr[ii]["DESCRICAO"].ToString().Replace("'", "").Replace("&", "e") + "</Descricao>";
        //                    xml += "<Qde>" + dr[ii]["QUANTIDADE"].ToString().Replace(",", ".") + "</Qde>";
        //                    xml += "<Uom>" + dr[ii]["UNIDADEDEMEDIDA"].ToString() + "</Uom>";
        //                    xml += "<Barcode>" + dr[ii]["BARCODE"].ToString() + "</Barcode>";
        //                    xml += "</SKU>";
        //                }
        //                else
        //                {
        //                    if (jaInseriu == false)
        //                    {
        //                        xml += "<SKU codigo=" + quote + dr[0]["NUMERO"].ToString() + quote + ">";
        //                        xml += "<PesoBruto>0</PesoBruto>";
        //                        xml += "<PesoLiquido>0</PesoLiquido>";
        //                        xml += "<Volumes>1</Volumes>";
        //                        xml += "<Descricao>DANFE " + dr[0]["NUMERO"].ToString() + "</Descricao>";
        //                        xml += "<Qde>1</Qde>";
        //                        xml += "<Uom>FL</Uom>";
        //                        xml += "<Barcode>" + ch + "</Barcode>";
        //                        xml += "</SKU>";
        //                        jaInseriu = true;
        //                    }
        //                }
        //            }
        //            xml += "</SKUs> ";

        //            xml += " </Documentos> ";

        //            string auxXML = xml;
        //            xml = Base64Encode(xml);

        //            if (ch.Contains("NFE"))
        //            {
        //                ch = ch.Replace("NFE", "000");
        //                gerouCahve = true;
        //            }

        //            //string NomeArquivo = ch + DateTime.Now.ToString("yyyyMMddhhmmssffftt") + ".xml";
        //            string NomeArquivo = dtds.Rows[i]["IDDOCUMENTO"].ToString() + ".xml";

        //            WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
        //            string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
        //                           "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
        //                           "<SOAP-ENV:Body>" +
        //                           "<tns:importDocsToPOD xmlns:tns=\"urn:WebServicePOD\">" +
        //                           "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
        //                           "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
        //                           "</tns:" + "importDocsToPOD" + ">" +
        //                           "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
        //            byte[] data = Encoding.ASCII.GetBytes(postData);
        //            request.Method = "POST";
        //            request.ContentType = "text/xml; charset=ISO-8859-1";
        //            request.Headers.Add("SOAPAction", "urn:WebServicePOD#getDocumentsFromPOD");
        //            request.Headers.Add("Authorization", "Basic " + auth);
        //            request.ContentLength = data.Length;


        //            using (var stream = request.GetRequestStream())
        //            {
        //                stream.Write(data, 0, data.Length);
        //            }


        //            Cursor.Current = Cursors.WaitCursor;
        //            WebResponse response = (HttpWebResponse)request.GetResponse();
        //            StreamReader sr = new StreamReader(response.GetResponseStream());

        //            XmlDocument xmlAwnser = new XmlDocument();
        //            xmlAwnser.LoadXml(sr.ReadToEnd());


        //            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
        //            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        //            xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

        //            textBox1.Text = DateTime.Now + "-" + "Enviando Nota ao Comprovei. IdDocumento: ";
        //            Application.DoEvents();

        //            string retorno = xmlAwnser["status"].InnerText;

        //            retorno = retorno.Replace("'", "");
        //            retorno = retorno.Replace("/", "");
        //            retorno = retorno.Replace("\\", "");
        //            retorno = retorno.Replace("??", "CA");

        //            string sql = "";

        //            if (retorno.ToUpper().Contains("WEVE GOT SOME PROBLEM"))
        //            {
        //                sql = "UPDATE DOCUMENTO SET EnviadoComprovei=null  WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
        //                sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), '" + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
        //                //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";

        //                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei ", "Problema no retorno em: " + DateTime.Now.ToString() + "- IdDocumento:" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + "<BR> " + retorno, "mail.grupologos.com.br", "logos0902", "Envio Comprovei - Problema no retorno do comprovei");

        //            }
        //            else
        //            {

        //                sql = "UPDATE DOCUMENTO SET EnviadoComprovei='ENVIADO COMPROVEI - " + DateTime.Now + "' " + (gerouCahve == true ? ", DocumentoDoCliente4='" + ch + "'" : "") + " WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
        //                sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), 'Filial: " + dr[0]["REGIAO"].ToString() + "  " + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
        //                //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";
        //            }

        //            if (!retorno.Contains("existe."))
        //            {
        //                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //            }
        //            // Label2.Text = "Terminou Envio Comprovei";


        //            Sistran.Library.GetDataTables.ExecutarComandoSql("Delete from DocumentoEnviarComprovei where IdDocumento=" + dtds.Rows[i]["IDDOCUMENTO"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //            textBox1.Text = "EnviarComprovei Termino: " + DateTime.Now.ToString();


        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //}
    }
}

