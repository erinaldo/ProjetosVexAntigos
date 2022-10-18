using System;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Data;
using System.Threading;
using Sistran.Library.Fatura;
using System.Web.UI.WebControls;
using System.Xml;



namespace Sistecno.Web.Fatura
{
    public partial class frmCriarZIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Image imgLogoPrincipal = (Image)Master.FindControl("imgLogoPrincipal");
            imgLogoPrincipal.ImageUrl = Session["img"].ToString();

            criarArquivosXML();
            //criarZIP();
            FaturaHistorico.gravarLog("download dos xmls ", Session["idtitulo"].ToString(), Session["cnx"].ToString());
        }

        DataTable ds;
        private void criarArquivosXML()
        {
            try
            {

            
            string strsql = "";
            //strsql += " SELECT top 10000 IDNOTA,ULTIMOARQUIVOXML FROM DOCUMENTOELETRONICO ";
            //strsql += " WHERE IDNOTA IN(";
            //strsql += " SELECT DISTINCT ";
            //strsql += " ( ";
            //strsql += " SELECT TOP 1 DE.IDNOTA ";
            //strsql += " FROM DOCUMENTOELETRONICO DE ";
            //strsql += " WHERE DE.IDDOCUMENTO = CTR.IDDOCUMENTO AND DE.STATUS IN ('CTE AUTORIZADA PARA USO','NF AUTORIZADA PARA USO', 'AUTORIZADO O USO DO CT-E') ";
            //strsql += " ) IDNOTA ";
            //strsql += " FROM ";
            //strsql += " TITULODOCUMENTO TD ";
            //strsql += " INNER JOIN DOCUMENTO CTR ON (CTR.IDDOCUMENTO = TD.IDDOCUMENTO) ";
            //strsql += " WHERE ";
            //strsql += " TD.IDTITULO =" + Session["idtitulo"];
            //strsql += " )";

            strsql += "Select   ( Select Top 1 De.IdNota From DocumentoEletronico DE      where DE.IdDocumento = Cte.IdDocumento and DE.CStatus = '100' ) IDNOTA ,  ";
            strsql += "( ";
            strsql += "Select Top 1 De.ULTIMOARQUIVOXML ";
            strsql += "From DocumentoEletronico DE WITH (NOLOCK)  ";
            strsql += "    where DE.IdDocumento = Cte.IdDocumento and DE.CStatus = '100' ";
            strsql += ") ULTIMOARQUIVOXML  ";

            strsql += "From Titulo T WITH (NOLOCK) ";
            strsql += "Inner Join TituloDocumento TD WITH (NOLOCK) on TD.IdTitulo = T.IdTitulo ";
            strsql += "Inner Join Documento Cte WITH (NOLOCK)on Cte.IdDocumento = TD.IdDocumento ";
            strsql += "Inner Join DocumentoFrete DFre on DFre.IDDocumento = Cte.IDDocumento ";
            strsql += "where T.IdTitulo = " + Session["idtitulo"];
           



            ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString()).Tables[0];            
            string chavezerada = "";
           

            for (int i = 0; i < ds.Rows.Count; i++)
            {
                string cam = Server.MapPath("XmlGerados") + "\\" + ds.Rows[i]["IdNota"].ToString() + ".xml";

                if (File.Exists(cam))
                    File.Delete(cam);

                StreamWriter wr = new StreamWriter(cam, true);

                wr.WriteLine(ds.Rows[i]["ULTIMOARQUIVOXML"].ToString());
                wr.Close();


                XmlDocument xml = new XmlDocument();
                xml.LoadXml(ds.Rows[i]["ULTIMOARQUIVOXML"].ToString());

                //XmlNodeList elemList = xml.GetElementsByTagName("ICMS");           

                //if(elemList[0].InnerXml.ToString().Contains("<vBC>0.00</vBC>"))
                //{
                //    chavezerada += ds.Rows[i]["IdNota"].ToString() + "<br>";
                //}

                XmlNodeList elemList = xml.GetElementsByTagName("vTPrest");

                if (elemList != null)
                {
                 float   xx = float.Parse( elemList[0].InnerXml);

                    if(xx == float.Parse("0.00"))
                        chavezerada += ds.Rows[i]["IdNota"].ToString() + "<br>";


                }

                if (elemList == null )
                {

                    chavezerada += ds.Rows[i]["IdNota"].ToString() + "<br>";
                }

               
            }

            if (chavezerada.Length > 10 )
            {
                Label1.Text = "XML com Valor de Frete Zerado - Arquivo Zipado";
                string texto = "Direto Arquivo Zipado<br>Data: " + DateTime.Now.ToString() + " <br>Fatura: " + Session["idtitulo"];
                texto += "<br>Chave(s): " + chavezerada;
                texto += "<br>IP: " + Request.UserHostAddress;

                //texto += "<br><br><br>" + ds.Rows[i]["ULTIMOARQUIVOXML"].ToString();
                Sistran.Library.EnviarEmails.EnviarEmailx("cmiguel@timecsc.com.br; tmarquezin@timecsc.com.br", "moises@sistecno.com.br", "XML com Valor de Frete Zerado", texto, "XML com Valor de Frete Zerado");
                throw new Exception("Não foi possivel gerar o arquivo.");
            }


            if (ds.Rows.Count > 0)
                criarZIP(ds);

            }
            catch (Exception ex)
            {
               Label1.Text = ex.Message;
            }
        }

        protected void criarZIP(DataTable dt)
        {
            string nomeArquivo = Guid.NewGuid() + ".zip";
            string caminho = Server.MapPath("ZipsGerados") +"\\" + nomeArquivo;            
            ZipOutputStream zipOutPut = new ZipOutputStream(File.Create(caminho));
            //Compactação level 9
            zipOutPut.SetLevel(9);
            zipOutPut.Finish();
            zipOutPut.Close();          


            ZipFile zip = new ZipFile(caminho);
            //Inicia a criação do ZIP
            zip.BeginUpdate();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Adicionando arquivos previamente criados ao zipFile
                string cam = Server.MapPath("XmlGerados") + "\\" + dt.Rows[i]["IdNota"].ToString() + ".xml";
                string nomeZIP = cam;
                zip.NameTransform = new ZipNameTransform(nomeZIP.Substring(0, nomeZIP.LastIndexOf("\\")));
                zip.Add(nomeZIP);

            }

            zip.CommitUpdate();
            zip.Close();

           // Thread.Sleep(20000); 

            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename="+ nomeArquivo);

            FileStream inStr = null;
            byte[] buffer = new byte[30000];
            long byteCount;

            inStr = File.OpenRead(caminho);
            while ((byteCount = inStr.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (Context.Response.IsClientConnected)
                {
                    Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    Context.Response.Flush();
                }
            }
            Response.End();
        }
    }
}