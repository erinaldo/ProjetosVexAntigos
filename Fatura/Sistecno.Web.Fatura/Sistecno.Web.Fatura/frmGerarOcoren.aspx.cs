using System;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Data;
using Sistran.Library.Fatura;



namespace Sistecno.Web.Fatura
{
    public partial class frmGerarOcoren : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GerarOcoren();
            //criarZIP();
            FaturaHistorico.gravarLog("baixou ocorren", Session["idtitulo"].ToString(), Session["cnx"].ToString());
        }

        public string Space(int nx)
        {
            string strret = "";
            for (int i = 1; i <= nx; i++)
                strret = strret + " ";

            return strret;
        }

        private void GerarOcoren()
        {

            string strsql = "";
            strsql += " SELECT DISTINCT ";
            strsql += "   NF.IDDOCUMENTO, ";
            strsql += " DOCOCORR.IDDOCUMENTOOCORRENCIA, ";
            strsql += " REM.RAZAOSOCIALNOME NOMRET, ";
            strsql += " REM.CNPJCPF CNPJ_REM, ";
            strsql += " DEST.RAZAOSOCIALNOME NOMDEST,  ";
            strsql += " TRANS.CNPJCPF CGCTRANS, ";
            strsql += " TRANS.RAZAOSOCIALNOME NOMETRANS,  ";
            strsql += " EMB.CNPJCPF CNPJ_EMBARCADOR,  ";
            strsql += " NF.SERIE, NF.NUMERO, ";
            strsql += " OCORR.CODIGO,  ";
            strsql += " DOCOCORR.DATAOCORRENCIA,  ";
            strsql += " OCORR.NOME , ";
            strsql += " DOCOCORR.DESCRICAO ";
            strsql += " FROM DOCUMENTO DOC   ";
            strsql += " INNER JOIN DOCUMENTORELACIONADO DOCREL ON DOCREL.IDDOCUMENTOPAI = DOC.IDDOCUMENTO ";
            strsql += " INNER JOIN DOCUMENTO NF  ON NF.IDDOCUMENTO = DOCREL.IDDOCUMENTOFILHO ";
            strsql += " LEFT JOIN TITULODOCUMENTO TDOC ON TDOC.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            strsql += " LEFT JOIN TITULO T ON T.IDTITULO = TDOC.IDTITULO ";
            strsql += " LEFT JOIN CADASTRO DEST ON (DEST.IDCADASTRO = NF.IDDESTINATARIO)  ";
            strsql += " LEFT JOIN CADASTRO REM ON (REM.IDCADASTRO = NF.IDREMETENTE)  ";
            strsql += " LEFT JOIN  FILIAL F ON (F.IDFILIAL = NF.IDFILIAL)  ";
            strsql += " LEFT JOIN  CADASTRO TRANS ON (TRANS.IDCADASTRO = F.IDCADASTRO)  ";
            strsql += " LEFT JOIN CADASTRO EMB ON (EMB.IDCADASTRO = DOC.IDCLIENTE)  ";
            strsql += " LEFT JOIN DOCUMENTOOCORRENCIA DOCOCORR ON (DOCOCORR.IDDocumentoOcorrencia = NF.IDDocumentoOcorrencia)  ";
            strsql += " LEFT JOIN  OCORRENCIA  OCORR ON (OCORR.IDOCORRENCIA = DOCOCORR.IDOCORRENCIA)  ";
            strsql += " LEFT JOIN DOCUMENTOOBSERVACAO OBS ON (OBS.IDDOCUMENTO = DOC.IDDOCUMENTO) ";
            strsql += " WHERE T.IDTITULO=" + Session["idtitulo"];
            strsql += " AND NF.TIPODEDOCUMENTO='NOTA FISCAL' AND OCORR.CODIGO IS NOT NULL  ";

            DataTable ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString()).Tables[0];

            if (ds.Rows.Count == 0)
                return;

            strsql = "SELECT PROXIMONUMERO FROM NUMERADOR WHERE SERIE='OCORREN' ";
            strsql += " UPDATE NUMERADOR SET PROXIMONUMERO=PROXIMONUMERO+1 WHERE SERIE='OCORREN' ";


            string ProximoNumero = Sistran.Library.GetDataTables.ExecutarRetornoIDs(strsql, Session["cnx"].ToString());
            string cam = Server.MapPath("XmlGerados") + "\\" + "LOGOCO" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero + ".txt";

            strsql = "";

            if (File.Exists(cam))
                File.Delete(cam);

            StreamWriter wr = new StreamWriter(cam, true);

            #region Linha 000
            string linha = "";
            linha += "000";
            linha += preenchervariavel(ds.Rows[0]["NOMETRANS"].ToString(), 35, false);
            linha += preenchervariavel(ds.Rows[0]["NOMRET"].ToString(), 35, false);
            linha += DateTime.Now.ToString("ddMMyyhhmm");
            linha += "OCO" + DateTime.Now.ToString("ddMMhhmm") + "0";
            wr.WriteLine(linha);
            #endregion

            #region Linha 340
            linha = "340" + preenchervariavel("LOGOCO" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero, 14, false);
            linha += preenchervariavel("", 18, false);
            wr.WriteLine(linha);
            #endregion

            #region Linha 341
            linha = "341";
            linha += preenchervariavel(ds.Rows[0]["CGCTRANS"].ToString().Replace(".", "").Replace("/", "").Replace("-", ""), 14, false);
            linha += preenchervariavel(ds.Rows[0]["NOMETRANS"].ToString(), 40, false);
            linha += preenchervariavel("", 10, false);
            linha += preenchervariavel("", 53, false);
            wr.WriteLine(linha);

            #endregion

            #region Linha 342

            for (int i = 0; i < ds.Rows.Count; i++)
            {
                linha = "342";
                linha += preenchervariavel(ds.Rows[i]["CNPJ_REM"].ToString().Replace(".", "").Replace("/", "").Replace("-", ""), 14, false);
                linha += preenchervariavel(ds.Rows[i]["SERIE"].ToString(), 3, false);

                linha += preenchervariavel(ds.Rows[i]["NUMERO"].ToString(), 8, true);

                linha += preenchervariavel(ds.Rows[i]["CODIGO"].ToString(), 2, false);

                string d = ds.Rows[i]["DATAOCORRENCIA"].ToString();
                string h = "";
                if (d.Length > 0)
                {
                    h = DateTime.Parse(d).ToString("hhmm");
                    d = DateTime.Parse(d).ToString("ddMMyyyy");
                }

                linha += preenchervariavel(d, 8, false);
                linha += preenchervariavel(h, 4, false);

                linha += preenchervariavel("03", 2, false);
                linha += preenchervariavel(ds.Rows[i]["NOME"].ToString(), 70, false);
                linha += preenchervariavel("", 6, false);

                wr.WriteLine(linha);

                if (ds.Rows[i]["IDDOCUMENTOOCORRENCIA"].ToString() != "")
                    strsql += " UPDATE DOCUMENTOOCORRENCIA SET ARQUIVODEINTEGRACAO ='" + "LOGOCO" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero + ".txt" + "' WHERE IDDOCUMENTOOCORRENCIA =" + ds.Rows[i]["IDDOCUMENTOOCORRENCIA"].ToString() + " AND ARQUIVODEINTEGRACAO IS NULL";

            }
            #endregion


            wr.Close();

            if (strsql.Length > 0)
            {
                strsql += " select 1";
                Sistran.Library.GetDataTables.ExecutarRetornoIDs(strsql, Session["cnx"].ToString());
            }


            //string caminho = Server.MapPath("XmlGerados") + "\\" + ds.Rows[0]["IdNota"].ToString() + ".xml";

            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "text/txt";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "LOGOCO" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero + ".txt");
            Response.TransmitFile(cam);
            Response.End();
            Response.Flush();
            Response.Clear();

        }

        private string preenchervariavel(string valor, int tamanho, bool numerico)
        {
            string texc = valor;
            string texcAux = "";

            if (numerico)
            {
                int ix = valor.Length;

                for (int i = ix; i < tamanho; i++)
                {
                    texcAux += "0";
                }
                texc = texcAux + texc;
            }
            else
            {


                if (texc.Length > tamanho)
                    texc = texc.Substring(0, tamanho);

                if (texc.Length < tamanho)
                    texc = texc + Space(tamanho - texc.Length);
            }
            return texc;
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
                string cam = Server.MapPath("XmlGerados") + "\\" + dt.Rows[i]["IdNota"].ToString() + ".xml";
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