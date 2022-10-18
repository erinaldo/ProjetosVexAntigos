using System;
using System.Web;
using System.Data;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using Sistran.Library.Fatura;
namespace Sistecno.Web.Fatura
{
    public partial class frmBaixarTodosDactes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FaturaHistorico.gravarLog("baixou todos os dactes", Session["idtitulo"].ToString(), Session["cnx"].ToString());


            string strsql = "";
            //strsql += " SELECT TOP 1 IDNOTA,ULTIMOARQUIVOXML FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + Request.Que            ryString["idnota"] + "'";
            strsql += " SELECT IDNOTA,ULTIMOARQUIVOXML FROM DOCUMENTOELETRONICO WHERE IDNOTA in (@@@conteudo@@@) ";

            string strsqlPar = "SELECT DISTINCT    ";
            strsqlPar += " (    ";
            strsqlPar += " 	SELECT TOP 1 DE.IDNOTA    ";
            strsqlPar += " FROM DOCUMENTOELETRONICO DE    ";
            strsqlPar += " WHERE DE.IDDOCUMENTO = CTR.IDDOCUMENTO AND DE.STATUS IN ('CTE AUTORIZADA PARA USO','NF AUTORIZADA PARA USO','AUTORIZADO O USO DO CT-E')    ";
            strsqlPar += " ) IDNOTA ";
            strsqlPar += " FROM    ";
            strsqlPar += " TITULODOCUMENTO TD    ";
            strsqlPar += " INNER JOIN DOCUMENTO CTR ON (CTR.IDDOCUMENTO = TD.IDDOCUMENTO)    ";
            strsqlPar += " LEFT JOIN DOCUMENTORELACIONADO DR ON (DR.IDDOCUMENTOPAI = CTR.IDDOCUMENTO)    ";
            strsqlPar += " LEFT JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL')    ";
            strsqlPar += " LEFT JOIN DOCUMENTOFRETE DF ON (DF.IDDOCUMENTO = CTR.IDDOCUMENTO AND DF.PROPRIETARIO = 'CLIENTE')    ";
            strsqlPar += " LEFT JOIN DOCUMENTOOCORRENCIA DO ON (DO.IDDOCUMENTOOCORRENCIA = NF.IDDOCUMENTOOCORRENCIA)    ";
            strsqlPar += " LEFT JOIN OCORRENCIA O ON (O.IDOCORRENCIA = DO.IDOCORRENCIA)    ";
            strsqlPar += " WHERE   TD.IDTITULO =  " + Session["idtitulo"].ToString();
            strsqlPar += " AND  ";
            strsqlPar += " (    ";
            strsqlPar += " SELECT TOP 1 DE.IDNOTA    ";
            strsqlPar += " FROM DOCUMENTOELETRONICO DE  ";
            strsqlPar += " WHERE DE.IDDOCUMENTO = CTR.IDDOCUMENTO AND DE.STATUS IN ('CTE AUTORIZADA PARA USO','NF AUTORIZADA PARA USO','AUTORIZADO O USO DO CT-E')    ";
            strsqlPar += " ) IS NOT NULL ";


            strsql = strsql.Replace("@@@conteudo@@@", strsqlPar);



            DataTable ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString()).Tables[0];

            for (int i = 0; i < ds.Rows.Count; i++)
            {
                string cam = Server.MapPath("XmlGerados") + "\\" + ds.Rows[i]["IdNota"].ToString() + ".html";

                if (File.Exists(Server.MapPath("XmlGerados") + "\\" + ds.Rows[i]["IdNota"].ToString() + ".html"))
                {
                    File.Delete(Server.MapPath("XmlGerados") + "\\" + ds.Rows[i]["IdNota"].ToString() + ".html");
                }

                string conteudo = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><title>DACTE</title></head>";
                conteudo += "<body>    ";
                conteudo += "<form action='http://www.webdanfe.com.br/danfe/GeraDanfe.php' name='one' enctype='multipart/form-data'    method='post'>    ";                
                conteudo += "<textarea name='arquivoXml' cols='150' rows='50' style='visibility:hidden'>";
                conteudo += " @@@nota@@@   </textarea>    </form>    <script> document.one.submit(); </script></body></html>";

                StreamWriter wr = new StreamWriter(cam, true);
                wr.WriteLine(conteudo.Replace("@@@nota@@@", ds.Rows[i]["ULTIMOARQUIVOXML"].ToString()));
                wr.Close();                

                
            }

            criarZIP(ds);
        }

        protected void criarZIP(DataTable dt)
        {
            string nomeArquivo = Guid.NewGuid() + ".zip";
            string caminho = Server.MapPath("ZipsGerados") + "\\" + nomeArquivo;
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
                string cam = Server.MapPath("XmlGerados") + "\\" + dt.Rows[i]["IdNota"].ToString() + ".html";
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + nomeArquivo);

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