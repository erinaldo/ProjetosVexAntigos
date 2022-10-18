using Newtonsoft.Json;
using RestSharp;
using RoboEnvioOcorrenciaClientes.Classes;
using Sistecno.Facility.Dbo.Domain.Comum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboEnvioOcorrenciaClientes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // SolicitarTokenRiachuelo();


        }

        private async Task EnviarComprovanteS3()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string cnxFacility = "Data Source=192.168.10.20;Initial Catalog=Facility00002;Persist Security Info=True;User ID=sa;Password=WERasd27;MultipleActiveResultSets=true";

            if (DateTime.Now.Minute % 30 == 0)
            {
                DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWin("Update RetornoComprovei set EnviadoS3=null where EnviadoS3='N' ; Select 1", cnx);
            }

            string sql = "select top 100 rc.IdDocumento, rc.IdRetornoComprovei, rc.IdOcorrenciaComprovei, doco.IDDocumentoOcorrenciaArquivo " +
                        " from RetornoComprovei rc  with (nolock) " +
                        " inner join DocumentoOcorrencia do  with (nolock) on do.IdOcorrenciaComprovei = rc.IdOcorrenciaComprovei " +
                        " inner join DocumentoOcorrenciaArquivo doco  with (nolock) on doco.IDDocumentoOcorrencia = do.IDDocumentoOcorrencia " +
                        " where DATALENGTH(rc.Foto) > 0 " +
                        " and Processado is not null " +
                        " and EnviadoS3 is null " +
                        " Order By rc.IdOcorrenciaComprovei";



            DataTable dss = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

            for (int i = 0; i < dss.Rows.Count; i++)
            {
                sql = "Select * from DocumentoOcorrenciaArquivo where IDDocumentoOcorrenciaArquivo=" + dss.Rows[i]["IDDocumentoOcorrenciaArquivo"].ToString();
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                string sqlFacility = "Select Id From DocumentoTrackingOcorrenciaArquivo where Idold=" + dss.Rows[i]["IDDocumentoOcorrenciaArquivo"].ToString();
                DataTable dtFaci = Sistran.Library.GetDataTables.RetornarDataTableWin(sqlFacility, cnxFacility);

                label1.Text = dss.Rows[i]["IDDocumentoOcorrenciaArquivo"].ToString();
                Application.DoEvents();

                if (dtFaci.Rows.Count > 0)
                {
                    try
                    {
                        RoboEnvioOcorrenciaClientes.S3.S3 s3 = new S3.S3("vex-facility-eletronicos/FACILITY00002/Comprovantes", dtFaci.Rows[0]["id"].ToString() + ".jpg", "");

                        byte[] arquivo = (byte[])dt.Rows[0]["Arquivo"];
                        string ret = await s3.UploadFileSystemAsync(arquivo);
                        sql = "update DocumentoOcorrenciaArquivo  set LinkS3='" + ret + "'   where IDDocumentoOcorrenciaArquivo =" + dss.Rows[i]["IDDocumentoOcorrenciaArquivo"].ToString() + " ;delete from retornoComprovei where IdRetornoComprovei=" + dss.Rows[i]["IdRetornoComprovei"].ToString() + "; select 1";
                        dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                        sql = "update DocumentoTrackingOcorrenciaArquivo set Link='" + ret + "' where Id='" + dtFaci.Rows[0]["id"].ToString() + "'; select 1";
                        dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnxFacility);
                    }
                    catch (Exception ex)
                    {

                        sql = "Update RetornoComprovei set EnviadoS3='Erro: " + ex.Message + "'  where IdRetornoComprovei=" + dss.Rows[i]["IdRetornoComprovei"].ToString();
                        dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql + " ; select 1", cnx);
                    }
                }
                else
                {
                    sql = "Update RetornoComprovei set EnviadoS3='N'  where IdRetornoComprovei=" + dss.Rows[i]["IdRetornoComprovei"].ToString();
                    dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql + " ; select 1", cnx);
                }


            }
        }

        private void SolicitarTokenRiachuelo()
        {
            try
            {
                var client = new RestClient("https://9hyxh9dsj1.execute-api.us-east-1.amazonaws.com/v1/9bf62f11-670c-47b3-b501-5391690c1f40/get-token");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("x-api-key", "qngvjXdsAMVd6oXC0tfYzKUoK0buCG51CJceXvchsLR2qBihguPBmK4h6ZN9pYXv");
                request.AddHeader("Content-Type", "application/json");
                var body = @"{
" + "\n" +
                @"    ""value"": ""KsiUpMnX4wPF+48ijU0IksOEAwvkgRi17UctYDZONKUq9JsWNtZGGxazJ3rVMRhmbNz1teLrspMSP4lMODYdd7lOyVJel852HQswwy8EfhWycnl9Prka2H3fPOduKl+18dOPZYjyfOKcbRnWhTLdwYo5joMaX80c/ZOB1rlNtwzcukMMRmfcd3KAyeDg3xO8fwerokOynvKNMqKf9rLUf+ZWvKgmgg8dp2jNkAPSVJ6TKKsbDVXje7K5MK9WWq21a7AMIH5Ybj0/WCPbHaZ2nS9/2CA3bIQzwCq7+6kV+NV2Tw/59xnHmL4dpeDz8QyFbUEH9D4nxhaX+fhvJWzNRGaHmT45HdgPaJiV55apPq21l7xINd5fCe6mta/TmSig9EjrkVJ3qlkexbO9WHmscP689p1i3JqNKFrjKO5s77YFVGlydM9/nZE0vnuilOL9bXLC2wDXy0oM/cSBGBWCvS+owfqmboAhA0Y7SPadAa+vY9lq6q2I/upHs6JxCINHsUBrXPrjd/akhNlPxo+D7Z/H3Wtyjr8a1NB3+24081KGnSunsNHECc8MozefMqDrBtOsl5hWBLMbmjFb6RwxVvk5rvA4idPv27z9HUtC5StEsEQ/RSW9Mg/MyfjpoY0DTC+LPgXVj+1NLGKGu+rgJ0ATNbmZEsrzsBjdDp4JiBI=""
" + "\n" +
                @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                textBox1.Text = response.Content.ToString();

                var credenciais = JsonConvert.DeserializeObject<CredenciaisRiachuelo>(response.Content.ToString());

                textBox1.Text = credenciais.IdToken;


            }
            catch (Exception)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // EnviarOcorrenciaRiachuelo();
                timer1.Enabled = false;
                EnviarFotosViaVexS3();

            }
            catch (Exception)
            {
            }


            try
            {
                // EnviarOcorrenciaRiachuelo();

                EnviarComprovanteS3();
            }
            catch (Exception)
            {


            }

            timer1.Enabled = true;
        }

        private async Task EnviarFotosViaVexS3()
        {
            string cnxFacility = "Data Source=192.168.10.20;Initial Catalog=Facility00002;Persist Security Info=True;User ID=sa;Password=WERasd27;MultipleActiveResultSets=true";
            string sql = "select top 10 * from Pwa.Transportador  where Processado is not null and JsonClrv is not null and JsonCnh is not null and Isnull(FotosS3, 0) =0";

            DataTable dss = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnxFacility);

            for (int i = 0; i < dss.Rows.Count; i++)
            {

                IdWallParametros.Root p = JsonConvert.DeserializeObject<IdWallParametros.Root>(dss.Rows[i]["JsonCnh"].ToString());
                IdWallVeiculo.Root v = JsonConvert.DeserializeObject<IdWallVeiculo.Root>(dss.Rows[i]["JsonClrv"].ToString());


                if (p.result.parametros.imagens_facematch != null)
                {
                    #region Facematch
                    for (int ii = 0; ii < p.result.parametros.imagens_facematch.Count; ii++)
                    {

                        var image = IdWall.GetImageBytes(p.result.parametros.imagens_facematch[ii].url);
                        if (image.Length > 0)
                        {
                            try
                            {
                                string nome = "";
                                var r = p.result.parametros.imagens_facematch[ii].url.Split('/');
                                nome = r[r.Length - 1];
                                RoboEnvioOcorrenciaClientes.S3.S3 s3 = new S3.S3("vex-facility-eletronicos/FACILITY00002/ViaVex", nome, "");
                                string ret = "";


                                ret = await s3.UploadFileSystemAsync(image);

                                sql = "Update pwa.Transportador set FotosS3=1 where Id='" + dss.Rows[i]["Id"].ToString() + "' ; " +
                                    " Insert into pwa.TransportadorImagemS3 (Id,IdTransportador,TipoImagem,Nome,Url) values ((select NEWID()), '" + dss.Rows[i]["Id"].ToString() + "','facematch','" + nome + "','" + ret + "')";
                                Sistran.Library.GetDataTables.ExecutarSemRetornoWin(sql, cnxFacility);
                            }
                            catch (Exception ex)
                            {
                                string err = ex.Message;

                            }

                        }
                    }
                    #endregion


                    #region Parametros de Midia
                    for (int ii = 0; ii < p.result.parametros.recursos_midia.Count; ii++)
                    {
                        if (p.result.parametros.recursos_midia != null)
                        {
                            var image = IdWall.GetImageBytes(p.result.parametros.recursos_midia[ii].url);
                            if (image.Length > 0)
                            {
                                try
                                {
                                    string nome = "";
                                    var r = p.result.parametros.recursos_midia[ii].url.Split('/');
                                    nome = r[r.Length - 1];
                                    RoboEnvioOcorrenciaClientes.S3.S3 s3 = new S3.S3("vex-facility-eletronicos/FACILITY00002/ViaVex", nome, "");
                                    string ret = await s3.UploadFileSystemAsync(image);

                                    sql = "Update pwa.Transportador set FotosS3=1 where Id='" + dss.Rows[i]["Id"].ToString() + "' ; " +
                                        " Insert into pwa.TransportadorImagemS3 (Id,IdTransportador,TipoImagem,Nome,Url) values ((select NEWID()), '" + dss.Rows[i]["Id"].ToString() + "','facematch','" + nome + "','" + ret + "')";
                                    Sistran.Library.GetDataTables.ExecutarSemRetornoWin(sql, cnxFacility);
                                }
                                catch (Exception ex)
                                {
                                    string err = ex.Message;

                                }

                            }
                        }
                    }
                    #endregion


                    #region Documento Veiculo Full
                    // for (int ii = 0; ii < v.result.documento_ocr.filename_full.; ii++)
                    // {
                    if (p.result.parametros.recursos_midia != null)
                    {
                        var image = IdWall.GetImageBytes(p.result.parametros.recursos_midia.ToString());
                        if (image.Length > 0)
                        {
                            try
                            {
                                string nome = "";
                                var r = p.result.parametros.recursos_midia.ToString().Split('/');
                                nome = r[r.Length - 1];
                                RoboEnvioOcorrenciaClientes.S3.S3 s3 = new S3.S3("vex-facility-eletronicos/FACILITY00002/ViaVex", nome, "");
                                string ret = await s3.UploadFileSystemAsync(image);

                                sql = "Update pwa.Transportador set FotosS3=1 where Id='" + dss.Rows[i]["Id"].ToString() + "' ; " +
                                    " Insert into pwa.TransportadorImagemS3 (Id,IdTransportador,TipoImagem,Nome,Url) values ((select NEWID()), '" + dss.Rows[i]["Id"].ToString() + "','facematch','" + nome + "','" + ret + "')";
                                Sistran.Library.GetDataTables.ExecutarSemRetornoWin(sql, cnxFacility);
                            }
                            catch (Exception ex)
                            {
                                string err = ex.Message;

                            }

                        }
                    }
                    //}
                    #endregion

                    //if (dtFaci.Rows.Count > 0)
                    //{
                    //    try
                    //    {
                    //        RoboEnvioOcorrenciaClientes.S3.S3 s3 = new S3.S3("vex-facility-eletronicos/FACILITY00002/Comprovantes", dtFaci.Rows[0]["id"].ToString() + ".jpg", "");

                    //        byte[] arquivo = (byte[])dt.Rows[0]["Arquivo"];
                    //        string ret = await s3.UploadFileSystemAsync(arquivo);
                    //        sql = "update DocumentoOcorrenciaArquivo  set LinkS3='" + ret + "'   where IDDocumentoOcorrenciaArquivo =" + dss.Rows[i]["IDDocumentoOcorrenciaArquivo"].ToString() + " ;delete from retornoComprovei where IdRetornoComprovei=" + dss.Rows[i]["IdRetornoComprovei"].ToString() + "; select 1";
                    //        dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                    //        sql = "update DocumentoTrackingOcorrenciaArquivo set Link='" + ret + "' where Id='" + dtFaci.Rows[0]["id"].ToString() + "'; select 1";
                    //        dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnxFacility);
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //        sql = "Update RetornoComprovei set EnviadoS3='Erro: " + ex.Message + "'  where IdRetornoComprovei=" + dss.Rows[i]["IdRetornoComprovei"].ToString();
                    //        dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql + " ; select 1", cnx);
                    //    }
                    //}
                    //else
                    //{
                    //    sql = "Update RetornoComprovei set EnviadoS3='N'  where IdRetornoComprovei=" + dss.Rows[i]["IdRetornoComprovei"].ToString();
                    //    dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql + " ; select 1", cnx);
                    //}


                }
            }
        }



        public void processarViaVarejoImagemERecebedor(string idDocOco)
        {

            //for (int i = 0; i < nfs.Rows.Count; i++)
            //{
            try
            {

                ViaVarejoModel m = new ViaVarejoModel();



                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                string sql = "exec Prc_Integrar_Ocorrencia_viaVarejo_Imagem_Entregador " + idDocOco;
                DataTable dss = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                for (int ii = 0; ii < dss.Rows.Count; ii++)
                {
                    m.LogisticsProvider = "JM/Vex";
                    m.Shipper = "Via Varejo";
                    m.LogisticsProviderFederalTaxId = "999999999";
                    m.InvoiceKey = dss.Rows[ii]["chave"].ToString();
                    m.InvoiceSeries = dss.Rows[ii]["Numero"].ToString();
                    m.InvoiceNumber = dss.Rows[ii]["Serie"].ToString();

                    string DadosRecNome = "";
                    string DadosRecDoc = "";

                    if (dss.Rows[ii]["NomeRecebedor"].ToString() != "")
                    {
                        var c = dss.Rows[ii]["NomeRecebedor"].ToString().Split(' ');
                        if (c.Length > 1)
                        {
                            DadosRecNome = c[0];
                            DadosRecDoc = c[c.Length - 1];
                        }
                    }

                    AdditionalInformation sdinfo = new AdditionalInformation()
                    {
                        ReceiverDocument = dss.Rows[ii]["DocRecebedor"].ToString(),
                        ReceiverName = DadosRecNome,
                        Kinship = DadosRecDoc

                    };

                    if (dss.Rows[ii]["content_in_base64"].ToString() != "")
                    {
                        List<Attachment> attachments = new List<Attachment>();

                        attachments.Add(new Attachment()
                        {
                            ContentInBase64 = Convert.ToBase64String((byte[])dss.Rows[ii]["content_in_base64"]),
                            Type = "POD",
                            FileName = dss.Rows[ii]["chave"].ToString() + ".jpg",
                            AdditionalInformation = sdinfo

                        });


                        Event ee = new Event();
                        ee.EventDate = DateTime.Parse(dss.Rows[ii]["DataOcorrencia"].ToString());
                        ee.OriginalCode = dss.Rows[ii]["codigo"].ToString().Trim();
                        ee.OriginalMessage = dss.Rows[ii]["texto"].ToString();
                        ee.Attachments = attachments;

                        m.Events.Add(ee);
                    }
                    else
                    {
                        Event ee = new Event();
                        ee.EventDate = DateTime.Parse(dss.Rows[ii]["DataOcorrencia"].ToString());
                        ee.OriginalCode = dss.Rows[ii]["codigo"].ToString().Trim();
                        ee.OriginalMessage = dss.Rows[ii]["texto"].ToString();

                        m.Events.Add(ee);

                    }
                }

                using (var client = new HttpClient())
                {
                    var serializedProduto = JsonConvert.SerializeObject(m);
                    var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("logistics-provider-api-key", "E46C758B-DD5C-4896-B4E3-5D38F8DDA423");
                    client.DefaultRequestHeaders.Add("platform", "Sistranet");
                    var result = client.PostAsync("https://apivia.sislogica.com.br/api/v1/tracking/add/events", content); // produção

                    var c = result.Result;


                    if (c.StatusCode == HttpStatusCode.OK)
                    {
                        var json = c.Content.ReadAsStringAsync();
                        var cc = JsonConvert.DeserializeObject<ocorrenViaVarejoRet>(json.Result);

                        var mm = cc;
                        Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set Protocolo='" + mm.protocolo + "', EnviadoParaCliente=getdate(),DetalheEnvioParaCliente='" + serializedProduto + "' , DetalheEnvio='OK: " + mm.status + " - err: " + mm.erros + "' where IdDocumentoOcorrencia=" + idDocOco + "; Select 1", cnx);

                        Label2.Text = DateTime.Now + "- Enviou Ocorrencias ViaVarejo: " + serializedProduto;
                        Application.DoEvents();
                    }
                    else
                    {
                        Label2.Text = DateTime.Now + "- Erro Ocorrencias ViaVarejo: " + serializedProduto;
                        Application.DoEvents();
                    }
                }

            }

            catch (Exception)
            {

                throw;
            }
            // }
        }
        
    

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                processarViaVarejoImagemERecebedor("147736282");


            }
        }

      

        
    }

    public class ViaVarejoModel
    {
        public ViaVarejoModel()
        {
            this.Events = new List<Event>();
        }
        [JsonProperty("logistics_provider")]
        public string LogisticsProvider { get; set; }

        [JsonProperty("logistics_provider_id")]
        public int LogisticsProviderId { get; set; }

        [JsonProperty("logistics_provider_federal_tax_id")]
        public string LogisticsProviderFederalTaxId { get; set; }

        [JsonProperty("shipper")]
        public string Shipper { get; set; }

        [JsonProperty("shipper_federal_tax_id")]
        public string ShipperFederalTaxId { get; set; }

        [JsonProperty("invoice_key")]
        public string InvoiceKey { get; set; }

        [JsonProperty("invoice_series")]
        public string InvoiceSeries { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("tracking_code")]
        public string TrackingCode { get; set; }

        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("volume_number")]
        public string VolumeNumber { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }
    }

    public class Extra
    {
        [JsonProperty("any_key")]
        public string AnyKey { get; set; }
    }

    public class Event
    {
        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty("original_code")]
        public string OriginalCode { get; set; }

        [JsonProperty("original_message")]
        public string OriginalMessage { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("extra")]
        public Extra Extra { get; set; }
    }

    public class Location
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("additional")]
        public string Additional { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state_code")]
        public string StateCode { get; set; }

        [JsonProperty("quarter")]
        public string Quarter { get; set; }

        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }

    public class Attachment
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("content_in_base64")]
        public string ContentInBase64 { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

       [JsonProperty("additional_information")]
       public AdditionalInformation AdditionalInformation { get; set; }
    }

    public class ocorrenViaVarejoRet
    {
        public int protocolo { get; set; }
        public int qtd_registros { get; set; }
        public string status { get; set; }
        public object erros { get; set; }
    }
    public class AdditionalInformation
    {
        [JsonProperty("receiver_name")]
        public string ReceiverName { get; set; }

        [JsonProperty("receiver_document")]
        public string ReceiverDocument { get; set; }

        [JsonProperty("kinship")]
        public string Kinship { get; set; }
    }

}

