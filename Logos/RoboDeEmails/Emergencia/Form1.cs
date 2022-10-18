using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Emergencia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();

            try
            {
                timer1.Enabled = false;
                for (int i = 0; i < 200; i++)
                {
                Application.DoEvents();

                    ConsultarProtocolo(false);
                    AcertarNotasNoStistranetDoComprovei();
                    Application.DoEvents();


                }
                timer1.Enabled = true;
            }catch(Exception)
            {
                timer1.Enabled = true;
            }

        }

        public void SolicitarProtocolo()
        {

            try
            {
                Label2.Text = "Solicitando Protocolo: " + DateTime.Now.ToString();
                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                string varName = "";
                string varType = "";
                string xmlPath = "";
                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));



                // tbUrl = "http://soap.comprovei.com.br/exportQueue/index.php?wsdl";
                tbUrl = "http://34.207.154.27/exportQueue/index.php?wsdl";

                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);


                string postData = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:WebServiceComprovei\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                "<urn:downloadDocumentsHistory soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                "<qtdDocumentos xsi:type=\"xsd:string\">" + ConfigurationSettings.AppSettings["QtdDocumentosPorChamada"] + "</qtdDocumentos>" +
                "</urn:downloadDocumentsHistory>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope>";



                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=ISO-8859-1";
                request.Headers.Add("SOAPAction", "urn:WebServicePOD#downloadDocumentsHistory");
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
                // xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

                if (xmlAwnser.GetElementsByTagName("protocolo").Count > 0)
                {
                    //xmlAwnser.GetElementsByTagName("protocolo").Item(0).InnerText

                    string sql = "insert into ProtocoloComprovei (Protocolo,XmlProtocolo,DataSolicitacao) values('" + xmlAwnser.GetElementsByTagName("protocolo").Item(0).InnerText + "',@arquivo,getDate())";
                    SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    SqlCommand cmm = new SqlCommand();

                    cmm.CommandText = sql;
                    cmm.CommandType = CommandType.Text;
                    cmm.Connection = cnn;

                    SqlParameter par = new SqlParameter("@arquivo", xmlAwnser.InnerXml);
                    cmm.Parameters.Add(par);

                    try
                    {
                        cnn.Open();
                        cmm.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Label2.Text = "03. " + ex.Message;
                        Application.DoEvents();

                    }
                    finally
                    {
                        cnn.Close();
                    }

                }


                request = null;
                response = null;
                xmlAwnser = null;

                Cursor.Current = Cursors.Default;

                Label2.Text = DateTime.Now + "- Recebeu  Protocolo";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                Label2.Text = ex.Message;
                Application.DoEvents();
                
            }
        }

        public void ConsultarProtocolo(bool solicitar)
        {
            string ultimoProtocolo = "";
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                if (solicitar)
                {
                    SolicitarProtocolo();
                    SolicitarProtocolo();

                }

                string s = "Select top 1 * from ProtocoloComprovei where ProcessadoSistran is null and DataConclusao is null and Erro is null order by 1  desc";
                //string s = "Select top 1 * from ProtocoloComprovei where IdProtocoloComprovei = 38961";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(s, cnx);

                if (dt.Rows.Count == 0)
                {
                    SolicitarProtocolo();
                    dt = Sistran.Library.GetDataTables.RetornarDataTableWS(s, cnx);

                    if (dt.Rows.Count == 0)
                        return;
                }

                ultimoProtocolo = dt.Rows[0]["Protocolo"].ToString();

                Label2.Text = "Consultando Protocolo Inicio: " + DateTime.Now.ToString();
                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                string varName = "";
                string varType = "";
                string xmlPath = "";
                // string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));



                // tbUrl = "http://soap.comprovei.com.br/exportQueue/index.php?wsdl";
                tbUrl = "http://34.207.154.27/exportQueue/index.php?wsdl";


                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);


                string postData = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:WebServiceComprovei\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                "<urn:getExportProtocolStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                "<protocolo xsi:type=\"xsd:string\">" + dt.Rows[0]["Protocolo"].ToString() + "</protocolo>" +
                "</urn:getExportProtocolStatus>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope>";



                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=ISO-8859-1";
                request.Headers.Add("SOAPAction", "urn:WebServicePOD#getExportProtocolStatus");
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
                //xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);
                bool temOcorrencia = false;
                string sql = "";
                string aux = "";
                XmlDocument doc1 = new XmlDocument();

                if (xmlAwnser.GetElementsByTagName("dataConclusao").Count > 0)
                {
                    if (xmlAwnser.GetElementsByTagName("dataConclusao").Item(0).InnerText != "")
                    {
                        sql = "Update ProtocoloComprovei set XmlProtocolo=@Arquivo, ";
                        sql += "DataConclusao=getdate() ";
                        sql += "where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";


                        if (!xmlAwnser.InnerText.Contains("Sem ocorrências para retornar"))
                        {
                            temOcorrencia = true;
                            if (xmlAwnser.GetElementsByTagName("url").ToString() != "" && xmlAwnser.GetElementsByTagName("url") != null && xmlAwnser.GetElementsByTagName("url").Count > 0)
                            {
                                doc1.Load(xmlAwnser.GetElementsByTagName("url").Item(0).InnerText.Replace("https://s3.amazonaws.com/", "http://s3.amazonaws.com/"));
                                sql = "Update ProtocoloComprovei set XmlProtocolo=@Arquivo/*, XmlDocumento = @ArqDoc */";
                                sql += "where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";
                            }
                        }

                        SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        SqlCommand cmm = new SqlCommand();

                        cmm.CommandText = sql;
                        cmm.CommandType = CommandType.Text;
                        cmm.Connection = cnn;

                        SqlParameter par = new SqlParameter("@arquivo", xmlAwnser.InnerXml);
                        cmm.Parameters.Add(par);
                        /*
                                                if (temOcorrencia)
                                                {
                                                    par = new SqlParameter("@ArqDoc", doc1.InnerXml);
                                                    cmm.Parameters.Add(par);
                                                }
                        */

                        try
                        {
                            cnn.Open();
                            cmm.ExecuteNonQuery();
                            cnn.Close();

                            if (temOcorrencia)
                            {
                                string ss = "Select IdOcorrenciaComprovei from RetornoComprovei where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";
                                DataTable dtRetComp = Sistran.Library.GetDataTables.RetornarDataTableWS(ss, cnx);
                                ProcessarXMLProtocolo(doc1, dt.Rows[0]["Protocolo"].ToString(), dtRetComp);
                            }
                        }
                        catch (Exception ex)
                        {
                            Sistran.Library.GetDataTables.RetornarDataTableWS("Update ProtocoloComprovei set erro='S' where Protocolo='" + ultimoProtocolo + "'; select 1", cnx);
                            Label2.Text = "01. " + ex.Message;
                            Application.DoEvents();

                        }
                    }

                }
                request = null;
                response = null;
                xmlAwnser = null;
                Cursor.Current = Cursors.Default;

                Label2.Text = DateTime.Now + "- Termino da Consulta de Protocolo";
                Application.DoEvents();
                // Thread.Sleep(2000);
            }

            catch (Exception ex)
            {
                Sistran.Library.GetDataTables.RetornarDataTableWS("Update ProtocoloComprovei set erro='S' where Protocolo='" + ultimoProtocolo + "'; select 1", cnx);
               
            }
        }

        private void ProcessarXMLProtocolo(XmlDocument XmlDocumento, string Protocolo, DataTable RetornoComproveiProtocolo)
        {
            bool executouTudo = true;
            Label2.Text = "ProcessarXMLProtocolo";
            Application.DoEvents();
            try
            {
                var docs = XmlDocumento.GetElementsByTagName("Documento");
                int qtdx = 0;
                int qtdxTot = docs.Count;

                for (int i = 0; i < docs.Count; i++)
                {
                    string IdOcorrenciaComprovei = "";
                    string Ocorrencia = "";
                    string DataOco = "";
                    string Chave = docs.Item(i).SelectNodes("Chave")[0].InnerText;
                    string IdDocumento = "";
                    byte[] fotoArray = null;
                    qtdx++;

                    //PEGA A NOTA
                    string sql = "SELECT IDDOCUMENTO FROM DOCUMENTO with (nolock) WHERE TIPODEDOCUMENTO IN('NOTA FISCAL', 'ORDEM DE SERVICO', 'LOGISTICA') AND TIPODESERVICO in ('TRANSPORTE','ENTREGA') AND DOCUMENTODOCLIENTE4='" + Chave + "'";
                    DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    if (dtNota.Rows.Count == 0)
                    {
                        sql = "select IDDOCUMENTO from DocumentoEletronico  with (nolock)  where IdNota = '" + Chave + "'";
                        dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }

                    if (dtNota.Rows.Count > 0)
                        IdDocumento = dtNota.Rows[0]["IdDocumento"].ToString();
                    else
                        continue;

                    var ocos = docs.Item(i).SelectNodes("Ocorrencias/Ocorrencia");
                    for (int iOco = 0; iOco < ocos.Count; iOco++)
                    {
                        IdOcorrenciaComprovei = ocos[iOco]["Numero"].InnerText;

                        if (RetornoComproveiProtocolo != null && RetornoComproveiProtocolo.Rows.Count > 0)
                        {
                            DataRow[] x = RetornoComproveiProtocolo.Select("IdOcorrenciaComprovei='" + IdOcorrenciaComprovei + "'");

                            if (x.Length > 0)
                                continue;
                        }

                        Ocorrencia = ocos[iOco]["Motivo"].InnerText;
                        DataOco = ocos[iOco]["Data"].InnerText;

                        if (DataOco == "0000-00-00 00:00:00")
                        {
                            DataOco = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        var fotos = docs.Item(i).SelectNodes("Ocorrencias/Ocorrencia/Fotos/Foto");
                        if (fotos != null && fotos.Count > 0)
                        {
                            string CaminhoImagem = "";
                            CaminhoImagem = fotos[0].SelectSingleNode("Dado").InnerText;


                            try
                            {
                                var request = WebRequest.Create(CaminhoImagem);
                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                {
                                    Bitmap b = (Bitmap)Bitmap.FromStream(stream);
                                    fotoArray = ConvertBitMapToByteArray(b);
                                }
                            }
                            catch (Exception xx)
                            {
                                string carta = "o IdDocumento: " + IdDocumento + " Não localizou a imagem no endereço: " + CaminhoImagem;
                                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "ProcessarXmlProtocolo ", xx.Message + xx.InnerException + " Protocolo: " + Protocolo + "<BR>" + carta, "mail.grupologos.com.br", "logos0902", "ProcessarXMLDOcumento");
                                //executouTudo = false;

                                //                                sql = "Update ProtocoloComprovei set DataSolicitacao=getDate(), QTDProcessamento = isnull(QTDProcessamento,0)+1  where Protocolo='" + Protocolo + "'";
                                sql = "Insert Into RetornoComproveiImagem (Link,IdOcorrenciaComprovei) values ('" + CaminhoImagem + "','" + IdOcorrenciaComprovei + "')";
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                //continue;
                            }
                        }

                        string k = "";
                        if (fotoArray != null)
                            k = ", @foto ";

                        sql = "Insert into RetornoComprovei (IdDocumento, Protocolo, Chave, DataDaOcorrencia, Ocorrencia, IdOcorrenciaComprovei" + k.Replace("@", "") + ")";
                        sql += "values (" + IdDocumento + ", '" + Protocolo + "' ,'" + Chave + "', '" + DateTime.Parse(DataOco).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Ocorrencia + "', '" + IdOcorrenciaComprovei + "'" + k + ")";
                        //sql += " ; insert Into ProtocoloComproveiChave (IdProtocoloComprovei,Chave) Values (SCOPE_IDENTITY (),'" + Chave + "')";

                        SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        SqlCommand cmm = new SqlCommand();

                        cmm.CommandText = sql;
                        cmm.CommandType = CommandType.Text;
                        cmm.Connection = cnn;

                        SqlParameter par;

                        if (k != "")
                        {
                            par = new SqlParameter("@Foto", fotoArray);
                            cmm.Parameters.Add(par);
                        }

                        try
                        {
                            cnn.Open();
                            cmm.ExecuteNonQuery();

                        }
                        catch (Exception ex1)
                        {
                            Label2.Text = "02." + ex1.Message;
                            Application.DoEvents();
                            //thread.Sleep(2000);
                            executouTudo = false;
                        }
                        finally
                        {
                            cnn.Close();
                        }
                    }
                }

                if (executouTudo)
                {
                    string sql = "Update ProtocoloComprovei set DataConclusao=getDate(), ProcessadoSistran=getdate() ";
                    sql += "where Protocolo='" + Protocolo + "'";
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    //SolicitarProtocolo();
                }

            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "ProcessarXmlProtocolo ", ex.Message + ex.InnerException + " Protocolo: " + Protocolo, "mail.grupologos.com.br", "logos0902", "ProcessarXMLDOcumento");
            }
        }

        public byte[] ConvertBitMapToByteArray(Bitmap bitmap)
        {
            byte[] result = null;
            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, bitmap.RawFormat);
                result = stream.ToArray();
            }
            return result;
        }

        private void AcertarNotasNoStistranetDoComprovei()
        {
            string docAtual = "";
            try
            {
                string sql = "SELECT TOP 1000 COMP.*, D.IDFILIALATUAL  FROM RETORNOCOMPROVEI COMP WITH (NOLOCK) INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = COMP.IDDOCUMENTO WHERE PROCESSADO IS NULL /*AND D.IDDOCUMENTO=13048980 */ and D.IdDocumento not in (Select IdDocumento from Bloquear)  ORDER BY comp.IdRetornoComprovei desc ";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string[] oco = dt.Rows[i]["OCORRENCIA"].ToString().Split('-');
                    oco[0] = oco[0].Trim();
                    oco[1] = oco[1].Trim();


                    Label2.Text = DateTime.Now + "-" + "AcertarNotasNoStistranetDoComprovei. IdDocumento: " + dt.Rows[i]["IDDOCUMENTO"].ToString() + "| - " + i + 1 + " De " + dt.Rows.Count;
                    Application.DoEvents();
                    docAtual = dt.Rows[i]["IDDOCUMENTO"].ToString();


                    string sqlaux = "SELECT * FROM OCORRENCIA WHERE IDOCORRENCIASERIE=3 AND CODIGO='" + oco[0] + "' ";
                    DataTable dtOco = Sistran.Library.GetDataTables.RetornarDataTableWin(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    int idOcorrencia = 0;
                    string finalzadora = "SIM";

                    // se nao existir a ocorrencia insere
                    if (dtOco.Rows.Count == 0)
                    {
                        //GravarLog("Gravando Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

                        idOcorrencia = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("OCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
                        sqlaux = "insert into ocorrencia (IDOcorrencia, IDEmpresa, IDOcorrenciaAcao, Codigo, Nome, Responsabilidade, NomeReduzido, PagaEntrega, Finalizador, Sistema,  Ativo, RestringirCarregamento, AbrirFecharOcorrencia, ApareceSiteCliente, IdOcorrenciaSerie)";
                        sqlaux += "VALUES (" + idOcorrencia + ", NULL, 5, '" + oco[0] + "', '" + (oco[1].Trim().ToUpper().Length >= 60 ? oco[1].Trim().ToUpper().Substring(0, 59) : oco[1].Trim().ToUpper()) + "', 'CLIENTE', '" + (oco[1].Trim().ToUpper().Length >= 30 ? oco[1].Trim().ToUpper().Substring(0, 29) : oco[1].Trim().ToUpper()) + "', 'NAO', 'SIM', NULL,  'NAO', 'NAO', 'AMBOS', NULL, 3)";
                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                    else
                    {
                        idOcorrencia = int.Parse(dtOco.Rows[0]["IDOCORRENCIA"].ToString());
                        finalzadora = dtOco.Rows[0]["FINALIZADOR"].ToString();
                    }


                    //Verifico se ja tem a ocorrencia do comprovei
                    string strsql = "";
                    sql = "SELECT * FROM DOCUMENTOOCORRENCIA with (nolock) WHERE IDOCORRENCIACOMPROVEI= " + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " and IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString();
                    DataTable ret = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    if (ret.Rows.Count == 0)
                    {
                        //insere a ocorrencia
                        AcertarDadosDTRomaneio(int.Parse(dt.Rows[i]["IDDOCUMENTO"].ToString()));

                        //verifico se ja tem alguma ocorrencia feita pelo usuario do sistema
                        sql = "SELECT top 1 * FROM DOCUMENTOOCORRENCIA WITH (NOLOCK)  WHERE IDDOCUMENTO =" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " AND IDOCORRENCIA= (SELECT top 1 IDOCORRENCIA FROM OCORRENCIA WHERE CODIGO='" + oco[0] + "' and IDOCORRENCIASERIE=3) AND IDUSUARIO IS NOT NULL order by IdDocumentoOcorrencia Desc";
                        DataTable aux = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        DataTable existeFoto = new DataTable();
                        if (aux.Rows.Count > 0)
                        {
                            existeFoto = Sistran.Library.GetDataTables.RetornarDataTableWin("select top 1 * from DocumentoOcorrenciaArquivo where IddocumentoOcorrencia= " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                            if (aux.Rows.Count > 0)
                            {

                                DateTime dataDaOcorrencia = DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString());
                                DateTime dataOcorrenciaJaExistente = DateTime.Parse(aux.Rows[0]["DataOcorrencia"].ToString());

                                if (dataDaOcorrencia.ToString("dd/MM/yyyy HH:mm") == dataOcorrenciaJaExistente.ToString("dd/MM/yyyy HH:mm"))
                                {

                                    if (existeFoto.Rows.Count > 0)
                                        strsql = "UPDATE DOCUMENTOOCORRENCIAARQUIVO SET ARQUIVO=(select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ") WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
                                    else
                                    {

                                        if (dt.Rows[i]["foto"].ToString() != "")
                                        {
                                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                            strsql = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                                        }
                                    }

                                    strsql += "UPDATE DOCUMENTOOCORRENCIA SET IDOCORRENCIACOMPROVEI=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                                    continue;
                                }


                                if (dataDaOcorrencia.ToString("dd/MM/yyyy") == dataOcorrenciaJaExistente.ToString("dd/MM/yyyy"))
                                {

                                    if (existeFoto.Rows.Count > 0)
                                        strsql = "UPDATE DOCUMENTOOCORRENCIAARQUIVO SET ARQUIVO=(select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ") WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
                                    else
                                    {
                                        if (dt.Rows[i]["foto"].ToString() != "")
                                        {
                                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                            strsql = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                                        }
                                    }

                                    strsql += "UPDATE DOCUMENTOOCORRENCIA SET DataOcorrencia='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' ,IDOCORRENCIACOMPROVEI=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";

                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                                    continue;
                                }


                            }
                        }


                        int IdDocOco = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));

                        strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
                        strsql += " IDDocumentoOcorrencia, ";
                        strsql += " IdRomaneio, ";
                        strsql += " IDDocumento,";
                        strsql += " IDFilial,";
                        strsql += " IDOcorrencia,";
                        strsql += " DataOcorrencia,";
                        strsql += " Descricao,";
                        strsql += " Sistema,";
                        strsql += "IdOcorrenciaComprovei";
                        strsql += " ) VALUES (";
                        strsql += IdDocOco + " ,";
                        strsql += "ISNULL((SELECT TOP 1 ISNULL(RD.IDROMANEIO,null) FROM ROMANEIODOCUMENTO RD WITH (NOLOCK)  INNER JOIN ROMANEIO R ON R.IDROMANEIO = RD.IDROMANEIO WHERE RD.IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " AND  R.TIPO IN ('ENTREGA', 'COLETA') ORDER BY 1 DESC),null)" + " ,";
                        strsql += dt.Rows[i]["IDDOCUMENTO"].ToString() + " ,";
                        strsql += Convert.ToInt32(dt.Rows[i]["IDFILIALATUAL"].ToString()) + " ,";

                        ////se a data de conclusao for null coloca a ocorrencia se nao apenas uma observação que se caracteriza pelo null no idocorrencia

                        //if (finalzadora == "SIM")
                        strsql += int.Parse(idOcorrencia.ToString()) + " ,";
                        //else
                        //    strsql += " null ,";


                        strsql += "'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' ,";
                        strsql += " '" + dt.Rows[i]["Ocorrencia"].ToString().Trim() + " - Comprovei',";
                        strsql += "'SIM',";
                        strsql += dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " );   ";


                        if (finalzadora == "SIM")
                        {
                            strsql += "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + ", DATADECONCLUSAO= '" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "'  WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                            strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='PROCESSO FINALIZADO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                            //strsql += " UPDATE DOCUMENTO SET DATADECONCLUSAO= convert(datetime,'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "', 103)" + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "; "; 
                        }
                        else
                        {
                            string x = "SELECT COUNT(*) FROM DOCUMENTOFILIAL WHERE SITUACAO='PROCESSO FINALIZADO' AND IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString();
                            DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWin(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                            if (dtx.Rows[0][0].ToString() == "0")
                            {
                                if (dtOco.Rows[0]["RestringirCarregamento"].ToString() == "" || dtOco.Rows[0]["RestringirCarregamento"].ToString() == "NAO")
                                {
                                    strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO EMBARQUE' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                                }
                                else
                                {
                                    strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO SOLUCAO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                                }
                                strsql += " UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                            }
                        }

                        if (dt.Rows[i]["foto"].ToString() != "")
                        {
                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                            strsql += "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + IdDocOco.ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                        }

                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                        //SE INSERIR A DATA DE CONCLUSAO CALCULA O PRAZO UTILIZADO
                        if (finalzadora == "SIM")
                            Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(" EXEC SP_PRAZO_UTILIZADO_ID " + dt.Rows[i]["IDDOCUMENTO"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    }
                    else
                    {
                        strsql = "";
                        //if (finalzadora == "SIM")
                        //{
                        //    strsql += "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + ret.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", DATADECONCLUSAO= '" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "'  WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                        //    strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='PROCESSO FINALIZADO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                        //    //strsql += " UPDATE DOCUMENTO SET DATADECONCLUSAO= convert(datetime,'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "', 103)" + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "; ";
                        //}
                        //else
                        //{
                        //    string x = "SELECT COUNT(*) FROM DOCUMENTOFILIAL WHERE SITUACAO='PROCESSO FINALIZADO' AND IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString();
                        //    DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWin(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        //    if (dtx.Rows[0][0].ToString() == "0")
                        //    {
                        //        if (dtOco.Rows[0]["RestringirCarregamento"].ToString() == "" || dtOco.Rows[0]["RestringirCarregamento"].ToString() == "NAO")
                        //        {
                        //            strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO EMBARQUE' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                        //        }
                        //        else
                        //        {
                        //            strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO SOLUCAO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                        //        }

                        //        strsql += " UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + ret.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                        //    }
                        //}

                        //if (dt.Rows[i]["foto"].ToString() != "")
                        //{
                        //    string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        //    strsql += "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + ret.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                        //}

                        // strsql = "Update RetornoComprovei set Processado=getdate() where IdOcorrenciaComprovei=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString();

                        if (strsql.Length > 10)
                            Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    }
                }
            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "AcertarNotasNoStistranetDoComprovei ", "Documento: " + docAtual + ". Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Erro no Timer");
               
            }
        }
        public void AcertarDadosDTRomaneio(int iddocumento)
        {
            try
            {

                Label2.Text = DateTime.Now + "-" + "AcertarDadosDTRomaneio. IdDocumento: " + iddocumento;
                Application.DoEvents();

                string strsql = " SELECT top 1 DTR.IDDT, DTR.IDROMANEIO, DT.EMISSAO, DT.DATADESAIDA , RS.IDRASTREAMENTO ";
                strsql += "FROM DOCUMENTO D  with(nolock)";
                strsql += "INNER JOIN ROMANEIODOCUMENTO RD  with(nolock) ON RD.IDDOCUMENTO = D.IDDOCUMENTO ";
                strsql += "INNER JOIN DTROMANEIO DTR  with(nolock) ON DTR.IDROMANEIO = RD.IDROMANEIO ";
                strsql += "INNER JOIN DT  with(nolock) ON DT.IDDT = DTR.IDDT ";
                strsql += "LEFT JOIN RASTREAMENTO RS  with(nolock) ON RS.IDDT = DT.IDDT ";
                strsql += "WHERE D.IDDOCUMENTO =  " + iddocumento;
                strsql += " AND DT.IDDTTIPO = 1 ";

                strsql += " Order by 1 desc";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (dt.Rows.Count == 0)
                    return;

                strsql = "";

                //se a data de saida for nulo acerta para aparecer no monitoramento
                if (dt.Rows[0]["DATADESAIDA"].ToString() == "")
                    strsql += "Update dt set dataDeSaida = getdate() where iddt = " + dt.Rows[0]["IDDT"].ToString();

                if (dt.Rows[0]["IDRASTREAMENTO"].ToString() == "")
                {
                    string x = Sistran.Library.GetDataTables.RetornarIdTabela("RASTREAMENTO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    strsql += "; INSERT INTO RASTREAMENTO (IdRastreamento, IdRastreador, IdDt, Latitude,Longitude,Satelites,DataHora, PontodeOcorrencia, LATI , LONGI, DataHoraTransmissao)";
                    strsql += " VALUES (" + x + ", 1, " + dt.Rows[0]["IDDT"].ToString() + ", 0,0,null,getdate(), 'NAO', NULL , NULL, GETDATE())";
                }

                if (strsql.Length > 10)
                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            }
            catch (Exception ex)
            {
                string m = "";
            }
        }
    }
}
