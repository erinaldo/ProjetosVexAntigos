using System;
using System.IO;
using System.Data;
using Sistran.Library.Fatura;



namespace Sistecno.Web.Fatura
{
    public partial class frmGerarConemb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GerarOcoren();
            //criarZIP();
            FaturaHistorico.gravarLog("baixou conenb", Session["idtitulo"].ToString(), Session["cnx"].ToString());
        }

        public string Space(int nx)
        {
            string strret = "";
            for (int i = 1; i <= nx; i++)
                strret = strret + " ";

            return strret;
        }

        string repeticao = "";
        private void GerarOcoren()
        {

            string strsql = "";
            strsql += " SELECT  ";
            strsql += " F.NUMERODAFILIAL NFILIAL, DOC.CLASSECFOP CLASSE,CASE WHEN CFOP.CODIGO IS NULL THEN '357' ELSE CFOP.CODIGO END CFOP,  EMB.CNPJCPF CNPJ_EMBARCADOR, REM.CNPJCPF      REMCNPJ,  ";
            strsql += " REM.RAZAOSOCIALNOME NOMRET, DEST.RAZAOSOCIALNOME NOMDEST, DOC.IDDOCUMENTO,  ";
            strsql += " DEST.CNPJCPF, DEST.RAZAOSOCIALNOME, DOC.IDFILIAL, DOC.SERIE, DOC.NUMERO, DOC.DATADEEMISSAO, DOC.CIFFOB, DOC.PESOBRUTO,FRET.FRETEVALOR, DOC.BASEDOICMS,  ";
            strsql += " FRET.FRETEPERCENTUAL, FRET.ICMSISS, FRET.FRETEPESO, FRET.FRETE, FRET.CAT, FRET.DESPACHO, FRET.PEDAGIO, REM.CNPJCPF, DOC.SERIE, DOC.NUMERO,  ";
            strsql += " TRANS.CNPJCPF CGCTRANS, TRANS.RAZAOSOCIALNOME NOMETRANS, FRET.ALIQUOTA, ";
            strsql += " NF.NUMERO NUMERONF, ";
            strsql += " NF.SERIE SERIENF  ";
            strsql += " FROM  ";
            strsql += " DOCUMENTO DOC  ";
            strsql += " LEFT JOIN CADASTRO DEST ON (DEST.IDCADASTRO = DOC.IDDESTINATARIO)  ";
            strsql += " LEFT JOIN CADASTRO REM ON (REM.IDCADASTRO = DOC.IDREMETENTE)  ";
            strsql += " LEFT JOIN DOCUMENTOFRETE FRET ON (FRET.IDDOCUMENTO = DOC.IDDOCUMENTO)  ";
            strsql += " LEFT JOIN FILIAL F ON (F.IDFILIAL = DOC.IDFILIAL)  ";
            strsql += " LEFT JOIN CADASTRO TRANS ON (TRANS.IDCADASTRO = F.IDCADASTRO)  ";
            strsql += " LEFT JOIN CADASTRO EMB ON (EMB.IDCADASTRO = DOC.IDREMETENTE)  ";
            strsql += " LEFT JOIN DOCUMENTOCFOP DCFOP ON (DCFOP.IDDOCUMENTO = DOC.IDDOCUMENTO)  ";
            strsql += " LEFT JOIN CFOP ON (CFOP.IDCFOP = DCFOP.IDCFOP)  ";
            strsql += " LEFT JOIN TITULODOCUMENTO TD ON (TD.IDDOCUMENTO = DOC.IDDOCUMENTO)  ";
            strsql += " LEFT JOIN TITULO T ON (T.IDTITULO = TD.IDTITULO) ";
            strsql += " INNER JOIN DOCUMENTORELACIONADO DR ON DR.IDDOCUMENTOPAI= DOC.IDDOCUMENTO ";
            strsql += " INNER JOIN DOCUMENTO NF ON NF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO ";
            strsql += " WHERE ";
            strsql += " T.IDTITULO=" + Session["idtitulo"];
            strsql += " /*AND DOC.NOMEDOARQUIVO IS NULL        */ ";
            strsql += " AND DOC.ATIVO='SIM' AND   (DOC.STATUS <> 'CANCELADO' OR DOC.STATUS IS NULL) ";

            DataTable ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString()).Tables[0];

            if (ds.Rows.Count == 0)
                return;

            strsql = "SELECT PROXIMONUMERO FROM NUMERADOR WHERE SERIE='CONEMB'";
            strsql += " UPDATE NUMERADOR SET PROXIMONUMERO=PROXIMONUMERO+1 WHERE SERIE='CONEMB' ";


            string ProximoNumero = Sistran.Library.GetDataTables.ExecutarRetornoIDs(strsql, Session["cnx"].ToString());
            string cam = Server.MapPath("XmlGerados") + "\\" + "CON" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero + ".txt";

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
            linha += "CON" + DateTime.Now.ToString("ddMMhhmm") + "0";
            linha += preenchervariavel("", 585, false);

            wr.WriteLine(linha);
            #endregion

            #region Linha 320
            linha = "320" + preenchervariavel("CONHE" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero, 14, false);
            linha += preenchervariavel("", 663, false);
            wr.WriteLine(linha);
            #endregion


            #region Linha 321
            linha = "321";
            linha += preenchervariavel(ds.Rows[0]["CGCTRANS"].ToString().Replace(".", "").Replace("/", "").Replace("-", ""), 14, false);
            linha += preenchervariavel(ds.Rows[0]["NOMETRANS"].ToString(), 40, false);
            linha += preenchervariavel("", 623, false);

            wr.WriteLine(linha);

            #endregion


            DataTable distinctTable = ds.DefaultView.ToTable(true, "IDDOCUMENTO");

            for (int i = 0; i < distinctTable.Rows.Count; i++)
            {

                DataRow[] dr = ds.Select("IDDOCUMENTO=" + distinctTable.Rows[i]["IDDOCUMENTO"], "numero");

                for (int item = 0; item < dr.Length; item++)
                {
                    if (repeticao != dr[item]["IDDOCUMENTO"].ToString())
                    {
                        repeticao = dr[item]["IDDOCUMENTO"].ToString();
                        #region Nota
                        if (item == 0)
                        {
                            linha = "322";
                            linha += preenchervariavel(dr[0]["NFILIAL"].ToString(), 10, false);
                            linha += preenchervariavel(dr[0]["SERIE"].ToString(), 5, false);
                            linha += preenchervariavel(dr[0]["NUMERO"].ToString(), 12, true);

                            string d = dr[0]["DATADEEMISSAO"].ToString();
                            if (d.Length > 0)
                                d = DateTime.Parse(d).ToString("ddMMyyyy");
                            linha += d;

                            linha += preenchervariavel(dr[0]["CIFFOB"].ToString(), 1, false);

                            decimal x = decimal.Parse(dr[0]["PESOBRUTO"].ToString());
                            string sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 7, true);


                            x = decimal.Parse(dr[0]["FRETE"].ToString());
                            sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 15, true);


                            if (dr[0]["FRETE"].ToString() == "")
                                x = 0;
                            else
                                x = decimal.Parse(dr[0]["FRETE"].ToString());

                            sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 15, true);


                            if (dr[0]["ALIQUOTA"].ToString() == "")
                                x = 0;
                            else
                                x = decimal.Parse(dr[0]["ALIQUOTA"].ToString());

                            sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 4, true);


                            if (dr[0]["ICMSISS"].ToString() == "")
                                x = 0;
                            else
                                x = decimal.Parse(dr[0]["ICMSISS"].ToString());

                            sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 15, true);


                            if (dr[0]["FRETEPESO"].ToString() == "")
                                x = 0;
                            else
                                x = decimal.Parse(dr[0]["FRETEPESO"].ToString());

                            sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 15, true);

                            if (dr[0]["FRETEVALOR"].ToString() == "")
                                x = 0;
                            else
                                x = decimal.Parse(dr[0]["FRETEVALOR"].ToString());

                            sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 15, true);


                            if (dr[0]["CAT"].ToString() == "")
                                x = 0;
                            else
                                x = decimal.Parse(dr[0]["CAT"].ToString());

                            sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 15, true);



                            //--itr

                            linha += preenchervariavel("0", 15, true);


                            if (dr[0]["DESPACHO"].ToString() == "")
                                x = 0;
                            else
                                x = decimal.Parse(dr[0]["DESPACHO"].ToString());

                            sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 15, true);

                            if (dr[0]["PEDAGIO"].ToString() == "")
                                x = 0;
                            else
                                x = decimal.Parse(dr[0]["PEDAGIO"].ToString());

                            sx = x.ToString("#0.00");
                            sx = sx.Replace(".", "").Replace(",", "");
                            linha += preenchervariavel(sx, 15, true);

                            //--ademe

                            linha += preenchervariavel("0", 15, true);

                            //substituicao Triobu
                            linha += preenchervariavel("2", 1, true);
                            linha += preenchervariavel("", 3, false);

                            linha += preenchervariavel(dr[0]["CGCTRANS"].ToString().Replace("/", "").Replace(".", "").Replace("-", ""), 14, false);
                            linha += preenchervariavel(dr[0]["CNPJ_EMBARCADOR"].ToString().Replace("/", "").Replace(".", "").Replace("-", ""), 14, false);

                        #endregion


                            for (int ii = 0; ii <= 39; ii++)
                            {
                                if (dr.Length > ii)
                                {
                                    linha += preenchervariavel(dr[ii]["SERIENF"].ToString(), 3, false);
                                    linha += preenchervariavel(dr[ii]["NUMERONF"].ToString(), 8, true);
                                }
                                else
                                {
                                    linha += preenchervariavel("", 3, false);
                                    linha += preenchervariavel("00000000", 8, false);
                                }

                            }

                            linha += "INU353  ";
                        }
                        
                        wr.WriteLine(linha);
                    }
                }
            }
            #region Linha 323
            linha = "323";
            linha += preenchervariavel(ds.DefaultView.ToTable(true, "IDDOCUMENTO").Rows.Count.ToString(), 4, true);

            DataTable distinctTableVL = ds.DefaultView.ToTable(true, "IDDOCUMENTO", "FRETE");

            decimal v = decimal.Parse((distinctTableVL.Compute("sum(FRETE)", "") == DBNull.Value ? "0" : distinctTableVL.Compute("sum(FRETE)", "").ToString()));
            linha += preenchervariavel(v.ToString("#0.00").Replace(",", "").Replace(".", ""), 15, true);

            linha += preenchervariavel("", 658, false);

            wr.WriteLine(linha);
            #endregion
            wr.Close();

            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "text/txt";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "CON" + DateTime.Now.ToString("ddMMHHmm") + ProximoNumero + ".txt");
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