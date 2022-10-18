using System;
using System.IO;
using System.Data;
using Sistran.Library.Fatura;

namespace Sistecno.Web.Fatura
{
    public partial class frmGerarDocCob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GerarOcoren();
                //criarZIP();
                FaturaHistorico.gravarLog("gerou doccob", Session["idtitulo"].ToString(), Session["cnx"].ToString());
            }
            catch (Exception ex)
            {
                // Response.Write("Indisponível");
            }
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
            strsql += " T.IDTITULO, TDA.IDTITULODUPLICATA,  REM.RAZAOSOCIALNOME RNOME, DEST.RAZAOSOCIALNOME DNOME,  TRANS.CNPJCPF TCNPJ,  TRANS.RAZAOSOCIALNOME TNOME,  F.NUMERODAFILIAL NFILIAL,  T.NUMERO NUMTIT,  T.SERIE TSERIE,  T.DATADEEMISSAO DTEMISSAO,  TDA.DATADEVENCIMENTO DTVENC,  T.VALOR TVALOR,  T.TIPO TTIPO,  DOCF.ICMSISS TICMS,  TDA.JUROSDIARIO JDIARIO,  TDA.DATALIMITEDESCONTO DTDESC,  TDA.DESCONTO TDESCT,   B.NOME NOMBANC,  BC.AGENCIA BAGENC, BC.AGENCIADIGITO AGDIGITO,  BC.CONTA NCONTA,  BC.CONTADIGITO NDIGIT,  DOC.SERIE DSERIE,  DOC.NUMERO DNUMERO,  DOC.DATADEEMISSAO DOCEMISSAO,  DOC.PESOBRUTO PBRUTO,  DOC.VALORDANOTA VMERCNF, REM.CNPJCPF RCNPJ, NF.SERIE SERIENF, NF.NUMERO NUMERONF, ISNULL(NF.DATADEEMISSAO, NF.DATADEENTRADA ) EMISSAONF, NF.PESOBRUTO PESONF,  NF.VALORDANOTA VALORNF,   DEST_NF.CNPJCPF DCNPJ_NF,  CAD.CNPJCPF RCNPJ_NF,  F.NOME NOME_FILIAL,  TRANS.CNPJCPF TR_CNPJ, DOCF.FRETE VALFRETE  ";
            strsql += " FROM DOCUMENTO DOC ";
            strsql += " INNER JOIN CADASTRO DEST ON (DEST.IDCADASTRO = DOC.IDDESTINATARIO)   ";
            strsql += " LEFT JOIN CADASTRO REM ON (REM.IDCADASTRO = DOC.IDREMETENTE)   ";
            strsql += " LEFT JOIN FILIAL F ON (F.IDFILIAL = DOC.IDFILIAL)  LEFT JOIN CADASTRO TRANS ON (TRANS.IDCADASTRO = F.IDCADASTRO)   ";
            strsql += " LEFT JOIN CADASTRO EMB ON (EMB.IDCADASTRO = DOC.IDREMETENTE)  LEFT JOIN TITULODOCUMENTO TD ON (TD.IDDOCUMENTO = DOC.IDDOCUMENTO)   ";
            strsql += " LEFT JOIN TITULO T ON (T.IDTITULO = TD.IDTITULO)  LEFT JOIN TITULODUPLICATA TDA ON (TDA.IDTITULO = T.IDTITULO)   ";
            strsql += " LEFT JOIN BANCOCONTA BC ON (BC.IDBANCOCONTA = TDA.IDBANCOCONTA)  LEFT JOIN BANCO B ON (B.IDBANCO = BC.IDBANCO)   ";
            strsql += " LEFT JOIN DOCUMENTORELACIONADO DOCREL ON (DOC.IDDOCUMENTO = DOCREL.IDDOCUMENTOPAI)   ";
            strsql += " LEFT JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = DOCREL.IDDOCUMENTOFILHO)  AND NF.TIPODEDOCUMENTO='NOTA FISCAL'  ";
            strsql += " LEFT JOIN DOCUMENTOFRETE DOCF ON (DOCF.IDDOCUMENTO = DOC.IDDOCUMENTO and Proprietario='CLIENTE')   ";
            strsql += " LEFT JOIN CADASTRO CAD ON (CAD.IDCADASTRO =DOC.IDREMETENTE)   ";
            strsql += " LEFT JOIN CADASTRO DEST_NF ON (DEST_NF.IDCADASTRO = NF.IDDESTINATARIO)  ";
            strsql += " WHERE ";
            strsql += " T.IDTITULO=" + Session["idtitulo"].ToString(); ;
            strsql += " AND DOC.TIPODEDOCUMENTO ='CONHECIMENTO' ";

            //  strsql += " group by T.IDTITULO, TDA.IDTITULODUPLICATA,  REM.RAZAOSOCIALNOME , DEST.RAZAOSOCIALNOME ,  TRANS.CNPJCPF ,  TRANS.RAZAOSOCIALNOME ,  F.NUMERODAFILIAL ,  T.NUMERO ,  T.SERIE ,  T.DATADEEMISSAO ,  TDA.DATADEVENCIMENTO ,  T.VALOR ,  T.TIPO ,  DOCF.ICMSISS ,  TDA.JUROSDIARIO ,  TDA.DATALIMITEDESCONTO ,  TDA.DESCONTO ,   B.NOME ,  BC.AGENCIA , BC.AGENCIADIGITO ,  BC.CONTA ,  BC.CONTADIGITO ,  DOC.SERIE ,  DOC.NUMERO ,  DOC.DATADEEMISSAO ,  DOC.PESOBRUTO ,  DOC.VALORDANOTA , REM.CNPJCPF , NF.SERIE , NF.NUMERO , ISNULL(NF.DATADEEMISSAO, NF.DATADEENTRADA ) , NF.PESOBRUTO ,  NF.VALORDANOTA ,   DEST_NF.CNPJCPF ,  CAD.CNPJCPF ,  F.NOME ,  TRANS.CNPJCPF ";

            DataTable ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString()).Tables[0];
            if (ds.Rows.Count == 0)
                return;

            strsql = "SELECT PROXIMONUMERO FROM NUMERADOR WHERE SERIE='DOCCOB'";
            strsql += " UPDATE NUMERADOR SET PROXIMONUMERO=PROXIMONUMERO+1 WHERE SERIE='DOCCOB' ";


            string ProximoNumero = Sistran.Library.GetDataTables.ExecutarRetornoIDs(strsql, Session["cnx"].ToString());
            string cam = Server.MapPath("XmlGerados") + "\\" + "DOC" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero + ".txt";

            strsql = "";

            if (File.Exists(cam))
                File.Delete(cam);

            StreamWriter wr = new StreamWriter(cam, true);

            #region Linha 000
            string linha = "";
            linha += "000";
            linha += preenchervariavel(ds.Rows[0]["TNOME"].ToString(), 35, false);
            linha += preenchervariavel(ds.Rows[0]["RNOME"].ToString(), 35, false);

            linha += DateTime.Now.ToString("ddMMyyhhmm");
            linha += "COB" + DateTime.Now.ToString("ddMMhhmm") + "0";
            linha += preenchervariavel("", 75, false);


            wr.WriteLine(linha);
            #endregion

            #region Linha 350
            linha = "350" + preenchervariavel("COBRA" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero, 14, false);
            linha += preenchervariavel("", 153, false);
            wr.WriteLine(linha);
            #endregion


            #region Linha 351
            linha = "351";
            linha += preenchervariavel(ds.Rows[0]["TCNPJ"].ToString().Replace(".", "").Replace("/", "").Replace("-", ""), 14, false);
            linha += preenchervariavel(ds.Rows[0]["TNOME"].ToString(), 40, false);
            linha += preenchervariavel("", 113, false);

            wr.WriteLine(linha);

            #endregion


            #region Linha 352
            linha = "352";
            linha += preenchervariavel(ds.Rows[0]["NFILIAL"].ToString(), 10, true);
            linha += preenchervariavel("0", 1, true);
            linha += preenchervariavel(ds.Rows[0]["TSERIE"].ToString(), 3, false);
            linha += preenchervariavel(ds.Rows[0]["NUMTIT"].ToString(), 10, true);

            DateTime d = DateTime.Parse(ds.Rows[0]["DTEMISSAO"].ToString());
            linha += preenchervariavel(d.ToString("ddMMyyyy"), 8, false);

            d = DateTime.Parse(ds.Rows[0]["DTVENC"].ToString());
            linha += preenchervariavel(d.ToString("ddMMyyyy"), 8, false);

            decimal v = decimal.Parse(ds.Rows[0]["TVALOR"].ToString());
            linha += preenchervariavel(v.ToString("#0.00").Replace(",", "").Replace(".", ""), 15, true);


            linha += preenchervariavel(ds.Rows[0]["TTIPO"].ToString(), 3, false);

            v = decimal.Parse((ds.Rows[0]["TICMS"] == DBNull.Value ? "0" : ds.Rows[0]["TICMS"].ToString()));
            linha += preenchervariavel(v.ToString("#0.00").Replace(",", "").Replace(".", ""), 15, true);

            v = decimal.Parse((ds.Rows[0]["JDIARIO"] == DBNull.Value ? "0" : ds.Rows[0]["JDIARIO"].ToString()));
            linha += preenchervariavel(v.ToString("#0.00").Replace(",", "").Replace(".", ""), 15, true);

            d = DateTime.Parse(ds.Rows[0]["DTVENC"].ToString());
            linha += preenchervariavel(d.ToString("ddMMyyyy"), 8, false);


            v = decimal.Parse((ds.Rows[0]["TDESCT"] == DBNull.Value ? "0" : ds.Rows[0]["TDESCT"].ToString()));
            linha += preenchervariavel(v.ToString("#0.00").Replace(",", "").Replace(".", ""), 15, true);


            linha += preenchervariavel(ds.Rows[0]["NOMBANC"].ToString(), 35, false);
            linha += preenchervariavel(ds.Rows[0]["BAGENC"].ToString(), 4, false);
            linha += preenchervariavel(ds.Rows[0]["AGDIGITO"].ToString(), 1, false);
            linha += preenchervariavel(ds.Rows[0]["NCONTA"].ToString(), 10, false);
            linha += preenchervariavel(ds.Rows[0]["NDIGIT"].ToString(), 2, false);
            linha += preenchervariavel("I", 1, false);
            linha += preenchervariavel("", 3, false);
            wr.WriteLine(linha);

            #endregion


            DataTable distinctTable = ds.DefaultView.ToTable(true, "DNUMERO");

            for (int i = 0; i < distinctTable.Rows.Count; i++)
            {
                DataRow[] dr = ds.Select("DNUMERO='" + distinctTable.Rows[i]["DNUMERO"].ToString() + "'", "DNUMERO");

                linha = preenchervariavel("353", 3, false);
                linha += preenchervariavel(dr[0]["NOME_FILIAL"].ToString(), 10, false);
                linha += preenchervariavel(dr[0]["DSERIE"].ToString(), 5, false);

                linha += preenchervariavel(dr[0]["DNUMERO"].ToString(), 12, false);

                v = decimal.Parse((dr[0]["VALFRETE"] == DBNull.Value ? "0" : ds.Rows[i]["VALFRETE"].ToString()));
                linha += preenchervariavel(v.ToString("#0.00").Replace(",", "").Replace(".", ""), 15, true);

                d = DateTime.Parse(dr[0]["DOCEMISSAO"].ToString());
                linha += preenchervariavel(d.ToString("ddMMyyyy"), 8, false);

                linha += preenchervariavel(dr[0]["RCNPJ_NF"].ToString().Replace(".", "").Replace("/", "").Replace("-", ""), 14, false);
                linha += preenchervariavel(dr[0]["DCNPJ_NF"].ToString().Replace(".", "").Replace("/", "").Replace("-", ""), 14, false);
                linha += preenchervariavel(dr[0]["TR_CNPJ"].ToString().Replace(".", "").Replace("/", "").Replace("-", ""), 14, false);
                linha += preenchervariavel("", 75, false);
                wr.WriteLine(linha);


                for (int ix = 0; ix < dr.Length; ix++)
                {
                    linha = preenchervariavel("354", 3, false);
                    linha += preenchervariavel(dr[ix]["SERIENF"].ToString(), 3, false);
                    linha += preenchervariavel(dr[ix]["NUMERONF"].ToString(), 8, false);

                    d = DateTime.Parse(dr[ix]["EMISSAONF"].ToString());
                    linha += preenchervariavel(d.ToString("ddMMyyyy"), 8, false);

                    v = decimal.Parse((dr[ix]["PESONF"] == DBNull.Value ? "0" : dr[ix]["PESONF"].ToString()));
                    linha += preenchervariavel(v.ToString("#0.00").Replace(",", "").Replace(".", ""), 7, true);

                    v = decimal.Parse((dr[ix]["VALORNF"] == DBNull.Value ? "0" : dr[ix]["VALORNF"].ToString()));
                    linha += preenchervariavel(v.ToString("#0.00").Replace(",", "").Replace(".", ""), 15, true);

                    linha += preenchervariavel(dr[ix]["RCNPJ_NF"].ToString().Replace(".", "").Replace("/", "").Replace("-", ""), 14, false);
                    linha += preenchervariavel("", 112, false);
                    wr.WriteLine(linha);
                }
            }

            #region Linha 355
            linha = "355";
            linha += preenchervariavel(ds.DefaultView.ToTable(true, "NUMERONF").Rows.Count.ToString(), 4, true);

            DataTable distinctTableVL = ds.DefaultView.ToTable(true, "DNUMERO", "TVALOR");

            v = decimal.Parse((distinctTableVL.Compute("sum(TVALOR)", "") == DBNull.Value ? "0" : distinctTableVL.Compute("sum(TVALOR)", "").ToString()));
            linha += preenchervariavel(v.ToString("#0.00").Replace(",", "").Replace(".", ""), 15, true);

            linha += preenchervariavel("", 148, false);

            wr.WriteLine(linha);
            #endregion

            wr.Close();



            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "text/txt";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "DOC" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero + ".txt");
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
    }
}