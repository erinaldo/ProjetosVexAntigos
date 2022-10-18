using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace TesteNovoMetodoComprovei
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try
            {
                Application.DoEvents();
               // enviar(false);
                //enviar(true);
            }
            catch (Exception ee)
            {
                string c = ee.Message;
            }
            finally
            {
                timer1.Enabled = true;
            }

        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enviar(false);
            enviar(false);
        }
        
        private void enviar(bool dts)
        {
            for (int ix = 0; ix < 20; ix++)
            {
                textBox2.Text = "Iniciando....";
                string auth = "";
                string nomeProc = "";

                auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin"));

                if (ix == 11)
                    nomeProc = "PRC_INTEGRAR_COMPROVEI_predilecta";

                if (ix == 0)
                    nomeProc = "EXEC PRC_INTEGRAR_COMPROVEI_SETOR_COLETA";
               

                if (ix == 1)
                    nomeProc = "PRC_INTEGRAR_COMPROVEI_BARBOSA";

                if (ix == 2)
                    nomeProc = "PRC_INTEGRAR_COMPROVEI_Atacadao";

                if (ix == 3)
                    nomeProc = "PRC_INTEGRAR_COMPROVEI_Pandurata";

                if (ix == 4)
                    nomeProc = "PRC_INTEGRAR_COMPROVEI_YANDEH";

                if (ix == 5)
                    nomeProc = "[PRC_INTEGRAR_COMPROVEI_FORCAR] ";


               // if (ix == 6)
              //      nomeProc = "[PRC_INTEGRAR_COMPROVEI_RICOY] ";

                if (ix == 7)
                    nomeProc = "EXEC PRC_INTEGRAR_COMPROVEI_DT_NOVO_WMS";

                if (ix == 8)
                    nomeProc = "EXEC PRC_INTEGRAR_COMPROVEI_DT_NOVO_WMS";
                if (ix == 9)
                    nomeProc = "EXEC PRC_INTEGRAR_COMPROVEI_DT_NOVO_WMS";


                if (ix == 10)
                     nomeProc = "PRC_INTEGRAR_COMPROVEI_SETOR";
                    //nomeProc = "EXEC PRC_INTEGRAR_COMPROVEI_FILIAL_ATUAL";

               
                
                if (ix > 11)
                    // nomeProc = "PRC_INTEGRAR_COMPROVEI_SETOR";
                    nomeProc = "EXEC PRC_INTEGRAR_COMPROVEI_FILIAL_ATUAL";



                string xml = "";

               textBox2.Text = nomeProc + " - " + DateTime.Now;
                Application.DoEvents();

                DataTable dtsel = Sistran.Library.GetDataTables.RetornarDataSetWS(nomeProc, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                DataView view = new DataView(dtsel);
                DataTable dt = view.ToTable(true, "IDDOCUMENTO");

                int x = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Thread.Sleep(5000);

                    x++;

                    DataRow[] dtGeral = dtsel.Select("IdDocumento=" + dt.Rows[i]["IdDocumento"]);
                    string ch = dtGeral[0]["CHAVE"].ToString().Trim().Replace("&", "-");

                    if (ch.Contains("SERNUMERONOT") && dtGeral[0]["idCliente"].ToString() == "3704306")
                        continue;

                    textBox2.Text = "Enviando " + x.ToString() + " de " + (dt.Rows.Count.ToString()) + " Para o comprovei. IdDocumento: " + dt.Rows[i]["IdDocumento"].ToString();                    

                    xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                    xml += "<Documentos>";
                    xml += "<Documento>";
                    xml += "<Tipo>" + dtGeral[0]["TIPO"].ToString().Trim().Replace("&", "-") + "</Tipo>";
                    xml += "<TipoParada>" + dtGeral[0]["tipoParada"].ToString().Trim().Replace("&", "-") + "</TipoParada>"; 
                    xml += "<Modelo>" + dtGeral[0]["Modelo"].ToString().Trim().Replace("&", "-") + "</Modelo>";
                    xml += "<Numero>" + dtGeral[0]["Numero"].ToString().Trim().Replace("&", "-") + "</Numero>";
                    xml += "<Valor>" + decimal.Parse(dtGeral[0]["VALORDANOTA"].ToString().Trim()).ToString().Replace(",", ".") + "</Valor>";
                    xml += "<Serie>" + dtGeral[0]["SERIE"].ToString().Trim().Replace("&", "-").Replace("U","").Replace("u", "") + "</Serie>";
                    xml += "<Emissao>" + DateTime.Parse(dtGeral[0]["DATADEEMISSAO"].ToString()).ToString("yyyyMMdd").Trim().Replace("&", "-") + "</Emissao>";
                    xml += "<Atualizacao>" + DateTime.Parse(dtGeral[0]["ATUALIZACAO"].ToString()).ToString("yyyyMMdd").Trim().Replace("&", "-") + "</Atualizacao>";
                    
                   
                    bool gerouCahve = false;

                    if (ch.Contains("SERNUMERONOT"))
                    {
                        ch = ch.Replace("SER", dtGeral[0]["SERIE"].ToString().Trim().Replace("&", "-").ToString().PadLeft(3, '0'));
                        ch = ch.Replace("NUMERONOT", dtGeral[0]["Numero"].ToString().Trim().Replace("&", "-").PadLeft(9, '0'));

                        gerouCahve = true;
                    }

                    if (gerouCahve)
                        xml += "<Chave>" + ch + "</Chave>";
                    else
                        xml += "<Chave>" + dtGeral[0]["CHAVE"].ToString().Trim().Replace("&", "-") + "</Chave>";

                    xml += "<cnpj>" + dtGeral[0]["CNPJ"].ToString().Trim().Replace("&", "-") + "</cnpj>";
                    xml += "<cnpjEmissor>" + dtGeral[0]["CNPJEMISSOR"].ToString().Trim().Replace("&", "-") + "</cnpjEmissor>";
                    xml += "<cnpjTransportador>" + dtGeral[0]["CNPJTRANSPORTADOR"].ToString().Trim().Replace("&", "-") + "</cnpjTransportador>";
                    //xml += "<TipoFrete>" + (dtGeral[0]["CAPINTER"].ToString() == "CAPITAL" ? "0" : "1") + "</TipoFrete>";

                    if (dtGeral[0]["Pedido"].ToString().Trim().Length > 1)
                    {
                        if (Regex.IsMatch(dtGeral[0]["Pedido"].ToString().Trim(), @"^[0-9]+$"))
                            xml += "<Pedido>" + dtGeral[0]["Pedido"].ToString() + "</Pedido>";
                    }


                    if (dtGeral[0]["Agendamento"].ToString() != "" && dtGeral[0]["CNPJEMISSOR"].ToString().Trim().Replace("&", "-") == "45543915084695")
                    {// Agendamento so para o carrefou, foi solicitado pelo claudio 30/06
                        xml += "<Agendamento>" + DateTime.Parse(dtGeral[0]["Agendamento"].ToString()).ToString("yyyyMMdd").Trim().Replace("&", "-") + "</Agendamento> ";

                        //criar a janela de entrega solicitado em 07/10 pelo Claudio
                        if (dtGeral[0]["HoraInicialEntrega"].ToString() != "")
                        {
                            xml += "<Janela>";
                            xml += "<DataHoraIni>" + DateTime.Parse(dtGeral[0]["Agendamento"].ToString()).ToString("yyyyMMdd").Trim().Replace("&", "-") + " "+ dtGeral[0]["HoraInicialEntrega"].ToString().Replace(":","") + "</DataHoraIni>";
                            xml += "<DataHoraFim>" + DateTime.Parse(dtGeral[0]["Agendamento"].ToString()).ToString("yyyyMMdd").Trim().Replace("&", "-") + " " + dtGeral[0]["HoraFinalEntrega"].ToString().Replace(":", "") + "</DataHoraFim>";
                            xml += "</Janela>";
                        }
                    }

                    xml += "<Cliente>";
                    xml += "<Codigo>" + dtGeral[0]["CODIGOCLIENTE"].ToString().Trim().Replace("&", "-") + "</Codigo>";
                    xml += "<Contato>" + dtGeral[0]["CONTATO"].ToString().Trim().Replace("&", "-") + "</Contato>";
                    xml += "<Telefone>" + dtGeral[0]["TELEFONE"].ToString().Trim().Replace("&", "-") + "</Telefone>";
                    xml += "<Email>" + dtGeral[0]["EMAIL"].ToString().Trim().Replace("&", "-") + "</Email>";
                    xml += "<Razao>" + dtGeral[0]["RAZAO"].ToString().Trim().Replace("&", "-") + "</Razao>";
                    xml += "<Endereco>" + dtGeral[0]["ENDERECO"].ToString().Replace("&", "-") + ", " + dtGeral[0]["NUMERO_END"].ToString() + "</Endereco>";
                    xml += "<Bairro>" + dtGeral[0]["BAIRRO"].ToString().Trim().Replace("&", "-").Replace(">","").Replace("<","") + "</Bairro>";
                    xml += "<Cidade>" + dtGeral[0]["CIDADE"].ToString().Trim().Replace("&", "-") + "</Cidade>";
                    xml += "<Estado>" + dtGeral[0]["ESTADO"].ToString().Trim().Replace("&", "-") + "</Estado>";
                    xml += "<Pais>BRASIL</Pais>";
                    xml += "<CEP>" + dtGeral[0]["CEP"].ToString().Trim().Replace("&", "-") + "</CEP>";
                    xml += "<Regiao>" + dtGeral[0]["REGIAO"].ToString().Trim().Replace("&", "-") + "</Regiao>";
                    xml += "<TipoCliente>0 - Normal</TipoCliente>";

                    if (dtGeral[0]["CodigoDoIBGE"].ToString() != "")
                    {
                        if (Regex.IsMatch(dtGeral[0]["CodigoDoIBGE"].ToString().Trim(), @"^[0-9]+$"))
                            xml += "<CodigoIBGE>" + dtGeral[0]["CodigoDoIBGE"].ToString()+ "</CodigoIBGE>";

                    }

                    //xml += "<TipoCliente>1 - especiais</TipoCliente>";


                    xml += "</Cliente>";
                    


                    xml += "<SKUs>";
                    bool jaInseriu = false;
                    string quote = "\"";
                    for (int ii = 0; ii < dtGeral.Length; ii++)
                    {
                        if (dtGeral[ii]["CODIGOPR"].ToString() != "")
                        {
                            xml += "<SKU codigo=" + quote + dtGeral[ii]["CODIGOPR"].ToString().Trim() + quote + ">";
                            xml += "<PesoBruto>" + dtGeral[ii]["PESOBRUTO"].ToString().Trim() + "</PesoBruto>";
                            xml += "<PesoLiquido>" + dtGeral[ii]["PESOLIQUIDO"].ToString().Trim() + "</PesoLiquido>";
                            xml += "<Volumes>" + dtGeral[ii]["VOLUMES"].ToString().Trim() + "</Volumes>";
                            xml += "<Descricao>" + dtGeral[ii]["DESCRICAO"].ToString().Replace("'", "").Replace("&", "e").Trim() + "</Descricao>";
                            xml += "<Qde>" + dtGeral[ii]["QUANTIDADE"].ToString().Replace(",", ".").Trim() + "</Qde>";
                            xml += "<Uom>" + dtGeral[ii]["UNIDADEDEMEDIDA"].ToString().Trim() + "</Uom>";
                            xml += "<Barcode>" + dtGeral[ii]["BARCODE"].ToString().Trim() + "</Barcode>";
                            xml += "</SKU>";
                        }
                        else
                        {
                            if (jaInseriu == false)
                            {
                                xml += "<SKU codigo=" + quote + dtGeral[0]["NUMERO"].ToString() + quote + ">";
                                xml += "<PesoBruto>0</PesoBruto>";
                                xml += "<PesoLiquido>0</PesoLiquido>";
                                xml += "<Volumes>1</Volumes>";
                                xml += "<Descricao>DANFE " + dtGeral[0]["NUMERO"].ToString().Trim() + "</Descricao>";
                                xml += "<Qde>1</Qde>";
                                xml += "<Uom>FL</Uom>";
                                xml += "<Barcode>" + dtGeral[0]["CHAVE"].ToString().Trim() + "</Barcode>";
                                xml += "</SKU>";
                                jaInseriu = true;
                            }
                        }
                    }
                    xml += "</SKUs>";
                    xml += "</Documento>";
                    xml += "</Documentos>";

                    try
                    {
                        try
                        {
                            


                            if(!Directory.Exists("C:\\tmp\\comprovei"))
                            {
                                Directory.CreateDirectory("C:\\tmp\\comprovei");
                            }



                            StreamWriter sw = new StreamWriter("C:\\tmp\\comprovei\\"+ dt.Rows[i]["IdDocumento"].ToString() + ".xml");
                            
                            sw.WriteLine(xml);
                            
                            
                            
                            sw.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception: " + e.Message);
                        }
                        finally
                        {
                            Console.WriteLine("Executing finally block.");
                        }
                    }
                    catch (Exception xx)
                    {

                    }

                    string auxXML = xml;
                    xml = Base64Encode(xml);

                    WebRequest request = (HttpWebRequest)WebRequest.Create("https://soap.comprovei.com.br/importQueue/index.php?wsdl");
                    string postData = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:WebServiceComprovei\">" +
                                            "<soapenv:Header/>" +
                                                "<soapenv:Body>" +
                                                "<urn:uploadDocuments soapenv:encodingStyle = \"http://schemas.xmlsoap.org/soap/encoding/\">" +
                                                    "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
                                                "</urn:uploadDocuments>" +
                                        "</soapenv:Body>" +
                                        "</soapenv:Envelope>";
                    byte[] data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "text/xml; charset=ISO-8859-1";
                    request.Headers.Add("SOAPAction", "WebServiceComprovei#uploadDocuments");
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
                    /*
                 * 
                    0	Todos os documentos adicionados à fila de importação!
                    1	Arquivo identificado como xml inválido ou corrompido.
                    3	Alguns documentos apresentaram erros. Estes não foram adicionados à fila de importação.
                    4	Todos os documentos apresentam erros e não foram adicionados à fila de importação.
                    999	Usuário não autenticado!
                 * */

                    string sql = "";
                    string oriEnvio = "";

                    string texto = oriEnvio + "-" + (xmlAwnser.GetElementsByTagName("protocolo")[0].InnerText == "" ? xmlAwnser.GetElementsByTagName("Code")[0].InnerText : xmlAwnser.GetElementsByTagName("protocolo")[0].InnerText) + "). " + DateTime.Now.ToString("yyyy-MM-dd hh:mm");


                    textBox2.Text = xmlAwnser.GetElementsByTagName("protocolo")[0].InnerText;
                    Application.DoEvents();

                    if (xmlAwnser.GetElementsByTagName("Code")[0].InnerText == "0")
                        sql = "Update documento set ProcessoEnvioComprovei='"+nomeProc+"',EnviadoComprovei ='Em Prot. (" + texto + "' WHERE IDDOCUMENTO=" + dtGeral[0]["IdDocumento"] + "; ";
                    
                    if (xmlAwnser.GetElementsByTagName("Code")[0].InnerText == "4"
                        && xmlAwnser.GetElementsByTagName("Mensagem")[0].InnerText == "Documento com esta chave já importado")
                        sql = "Update documento set EnviadoComprovei ='Documento Já Enviado Anteriormente: " + DateTime.Now.ToString() + "' WHERE IDDOCUMENTO=" + dtGeral[0]["IdDocumento"] + "; ";

                    sql += " INSERT INTO ProtocoloComproveiEnvio(Protocolo, XmlProtocolo, IdDocumento ) values ('" + (xmlAwnser.GetElementsByTagName("protocolo")[0].InnerText == "" ? xmlAwnser.GetElementsByTagName("Code")[0].InnerText : xmlAwnser.GetElementsByTagName("protocolo")[0].InnerText) + "', @XmlProtocolo, " + dtGeral[0]["IdDocumento"] + ")";

                    if (gerouCahve)
                        sql += " ; update documento set DocumentoDoCliente4='" + ch + "' where IdDocumento=" + dtGeral[0]["IdDocumento"].ToString();


                    SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    SqlCommand cmm = new SqlCommand();
                    cmm.CommandText = sql;
                    cmm.CommandType = CommandType.Text;
                    cmm.Connection = cnn;

                    SqlParameter par = new SqlParameter("@XmlProtocolo", xmlAwnser.InnerXml);
                    cmm.Parameters.Add(par);
                    Application.DoEvents();


                    try
                    {
                        cnn.Open();
                        cmm.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {                        
                        Application.DoEvents();
                    }
                    finally
                    {
                        cnn.Close();
                        textBox2.Text = "Finalizou o Ciclo....";
                    }

                    if (xmlAwnser.GetElementsByTagName("Code")[0].InnerText == "0")
                        textBox1.Text = xmlAwnser.GetElementsByTagName("protocolo")[0].InnerText;
                    else
                        textBox1.Text = "erro";
                }

                if (textBox1.Text != "erro" && textBox1.Text.Length > 10)
                    ConsultarProtocolo(textBox1.Text, true);
            }
        }

        public void ConsultarProtocolo(string protocolo, bool HR)
        {
            string auth = "";
            try
            {
                if (HR)
                    auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin"));
                else
                    auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin"));

                DataTable dtsel = Sistran.Library.GetDataTables.RetornarDataSetWS("Select idProtocoloComproveiEnvio, Protocolo, IdDocumento From ProtocoloComproveiEnvio with (nolock) where Len(Protocolo)>1 and Conclusao is null", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                for (int i = 0; i < dtsel.Rows.Count; i++)
                {
                    WebRequest request = (HttpWebRequest)WebRequest.Create("https://soap.comprovei.com.br/importQueue/index.php?wsdl");
                    string postData = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:WebServiceComprovei\">" +
                    "<soapenv:Header/>" +
                    "<soapenv:Body>" +
                       "<urn:getImportProtocolStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                    "<protocolo xsi:type=\"xsd:string\">" + dtsel.Rows[i]["Protocolo"].ToString() + "</protocolo>" +
                    "</urn:getImportProtocolStatus>" +
                   "</soapenv:Body>" +
                    "</soapenv:Envelope>";

                    byte[] data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "text/xml; charset=ISO-8859-1";
                    request.Headers.Add("SOAPAction", "WebServiceComprovei#uploadDocuments");
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

                    if (xmlAwnser.GetElementsByTagName("status")[0].InnerText == "Protocolo encontrado." && xmlAwnser.GetElementsByTagName("processado")[0].InnerText == "Sim")
                    {
                        string sql = "Update ProtocoloComproveiEnvio set Conclusao = getDate() where idProtocoloComproveiEnvio = " + dtsel.Rows[i]["idProtocoloComproveiEnvio"].ToString();
                        sql += ";Update Documento set EnviadoComprovei = '(Protocolo)Enviado Comprovei - " + DateTime.Now.ToString() + "' where IdDocumento =" + dtsel.Rows[i]["IdDocumento"].ToString();
                        string IdDoEdi = Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        sql += " ;INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + IdDoEdi + ", 'COMPROVEI', " + dtsel.Rows[i]["IdDocumento"].ToString() + ",getdate(), '(Protocolo) Enviado Comprovei - " + dtsel.Rows[i]["IdDocumento"].ToString() + "') ";
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConsultarProtocolo(textBox1.Text, true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                enviar(false);
                Application.DoEvents();
                enviar(true);
                Application.DoEvents();
            }
            catch (Exception ec)
            {
                textBox2.Text = ec.Message;
                Application.DoEvents();
            }
            finally
            {
                timer1.Enabled = true;
            }
        }
    }
}