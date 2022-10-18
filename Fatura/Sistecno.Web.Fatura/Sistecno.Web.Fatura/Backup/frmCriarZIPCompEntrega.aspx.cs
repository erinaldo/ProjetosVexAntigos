using System;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Data;
using Sistran.Library.Fatura;



namespace Sistecno.Web.Fatura
{
    public partial class frmCriarZIPCompEntrega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            criarArquivos();
            FaturaHistorico.gravarLog("baixou comprovantes de entregas", Session["idtitulo"].ToString(), Session["cnx"].ToString());
        }

        private void criarArquivos()
        {

            string strsql = " ";
            strsql += " SELECT   ";
            strsql += " DOA.* ";
            strsql += " FROM  ";
            strsql += " TITULODOCUMENTO TD  ";
            strsql += " INNER JOIN DOCUMENTO CTR ON (CTR.IDDOCUMENTO = TD.IDDOCUMENTO)  ";
            strsql += " LEFT JOIN DOCUMENTORELACIONADO DR ON (DR.IDDOCUMENTOPAI = CTR.IDDOCUMENTO)  ";
            strsql += " LEFT JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL')  ";
            strsql += " LEFT JOIN DOCUMENTOFRETE DF ON (DF.IDDOCUMENTO = CTR.IDDOCUMENTO AND DF.PROPRIETARIO = 'CLIENTE')  ";
            //strsql += " LEFT JOIN DOCUMENTOOCORRENCIA DO ON (DO.IDDOCUMENTOOCORRENCIA = NF.IDDOCUMENTOOCORRENCIA)  ";
            strsql += " LEFT JOIN DOCUMENTOOCORRENCIA DO ON (DO.IDDOCUMENTO = NF.IDDOCUMENTO)  ";
            strsql += " INNER JOIN DOCUMENTOOCORRENCIAARQUIVO DOA ON DOA.IDDOCUMENTOOCORRENCIA = DO.IDDOCUMENTOOCORRENCIA ";
            //strsql += " INNER JOIN DOCUMENTOOCORRENCIAARQUIVO DOA ON DOA.IDDOCUMENTOOCORRENCIA = DO.IDDOCUMENTOOCORRENCIA ";
            strsql += " WHERE  ";
            strsql += " TD.IDTITULO=" + Session["idtitulo"];

            DataTable ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString()).Tables[0];

            for (int i = 0; i < ds.Rows.Count; i++)
            {
                byte[] imagem = (byte[])ds.Rows[0]["Arquivo"];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                string cam = Server.MapPath("XmlGerados") + "\\" + ds.Rows[i]["IDDocumentoOcorrenciaArquivo"].ToString() + ".jpg";

                if (File.Exists(cam))
                    File.Delete(cam);

                
                returnImage.Save(cam);


                
                //StreamWriter wr = new StreamWriter(cam, true);

                //wr.WriteLine(ds.Rows[i]["ULTIMOARQUIVOXML"]);
                //wr.Close();

            }

            if (ds.Rows.Count > 0)
                criarZIP(ds);

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
                string cam = Server.MapPath("XmlGerados") + "\\" + dt.Rows[i]["IDDocumentoOcorrenciaArquivo"].ToString() + ".jpg";
                string nomeZIP = cam;
                zip.NameTransform = new ZipNameTransform(nomeZIP.Substring(0, nomeZIP.LastIndexOf("\\")));
                zip.Add(nomeZIP);

            }

            zip.CommitUpdate();
            zip.Close();

            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename="+ nomeArquivo);

            FileStream inStr = null;
            byte[] buffer = new byte[1024];
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