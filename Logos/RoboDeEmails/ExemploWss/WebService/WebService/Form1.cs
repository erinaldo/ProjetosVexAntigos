using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;

namespace WebService
{
    public partial class fWS : Form
    {
        public fWS()
        {
            InitializeComponent();

            this.cbMethod.SelectedIndex = 0;
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            this.tbAwnser.Text = "";

            try
            {
                bool bFilled = true;
                bFilled &= !String.IsNullOrWhiteSpace(this.tbUrl.Text);
                bFilled &= !String.IsNullOrWhiteSpace(this.tbUser.Text);
                bFilled &= !String.IsNullOrWhiteSpace(this.tbPass.Text);
                bFilled &= !String.IsNullOrWhiteSpace(this.cbMethod.Text);
                bFilled &= !String.IsNullOrWhiteSpace(this.tbValue.Text);

                if (bFilled)
                {
                    string varName = "";
                    string varType = "";
                    string xmlPath = "";
                    string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(this.tbUser.Text + ":" + this.tbPass.Text));

                    switch (this.cbMethod.SelectedIndex)
                    {
                        case 0:
                            varName = "qtdDocumentos";
                            varType = "integer";
                            xmlPath = "Retorno/Documentos/Documento";
                            break;
                        case 1:
                            varName = "key";
                            varType = "string";
                            xmlPath = "/Documento";
                            break;
                    }

                    WebRequest request = (HttpWebRequest)WebRequest.Create(this.tbUrl.Text);
                    string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
                                   "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<SOAP-ENV:Body>" +
                                   "<tns:" + this.cbMethod.Text + " xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<" + varName + " xsi:type=\"xsd:" + varType + "\">" + this.tbValue.Text + "</" + varName + ">" +
                                   "</tns:"+ this.cbMethod.Text + ">"+
                                   "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    byte[] data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "text/xml; charset=ISO-8859-1";
                    request.Headers.Add("SOAPAction", "urn:WebServicePOD#" + this.cbMethod.Text);
                    request.Headers.Add("Authorization", "Basic " + auth);
                    request.ContentLength = data.Length;

                    this.tbAwnser.Text = "Enviando...";
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    this.tbAwnser.Text = "Processando resposta...";
                    Cursor.Current = Cursors.WaitCursor;
                    WebResponse response = (HttpWebResponse)request.GetResponse();

                    XmlDocument xmlAwnser = new XmlDocument();
                    xmlAwnser.LoadXml(new StreamReader(response.GetResponseStream()).ReadToEnd());

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                    nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

                    string documents = "";
                    int count = 1;
                    foreach (XmlNode xmlDocument in xmlAwnser.SelectNodes(xmlPath))
                    {
                        if (xmlDocument["Erro"] == null)
                        {
                            XmlNode xmlPhoto = xmlDocument.LastChild;

                            documents += "--- "+count+"º Documento ---\r\n" +
                                         "Tipo: " + xmlDocument["Tipo"].InnerText + "\r\n" +
                                         "Modelo: " + xmlDocument["Modelo"].InnerText + "\r\n" +
                                         "Numero: " + xmlDocument["Numero"].InnerText + "\r\n" +
                                         "Serie: " + xmlDocument["Serie"].InnerText + "\r\n" +
                                         "Emissao: " + xmlDocument["Emissao"].InnerText + "\r\n" +
                                         "cnpj: " + xmlDocument["cnpj"].InnerText + "\r\n" +
                                         "Chave: " + xmlDocument["Chave"].InnerText + "\r\n";

                            if (xmlPhoto["Dado"] != null)
                            {
                                if (!String.IsNullOrWhiteSpace(xmlPhoto["Dado"].InnerText))
                                {
                                    string fileName = xmlDocument["Numero"].InnerText + "." + xmlPhoto["Extensao"].InnerText;
                                    using (FileStream fs = File.Create(@".\Fotos\\" + fileName))
                                    {

                                        byte[] info = Convert.FromBase64String(xmlPhoto["Dado"].InnerText);
                                        fs.Write(info, 0, info.Length);
                                    }

                                    documents += "Foto: " + fileName + "\r\n";
                                }
                            }

                            documents += "--- Fim do " + count + "º Documento ---\r\n";
                            count++;
                        }
                        else
                        {
                            documents = xmlDocument["Erro"].InnerText;
                        }                        
                    }

                    if (String.IsNullOrWhiteSpace(documents))
                    {                        
                        documents = xmlAwnser.FirstChild["Erro"].InnerText;
                    }

                    request = null;
                    response = null;
                    xmlAwnser = null;

                    this.tbAwnser.Text = documents;
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    this.tbAwnser.Text = "É necessário preencher todos os campos!";
                }                
            }
            catch (Exception ex)
            {
                this.tbAwnser.Text = ex.Message;
            }
        }

        private void cbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tbValue.Text = "";
        }
    }
}
