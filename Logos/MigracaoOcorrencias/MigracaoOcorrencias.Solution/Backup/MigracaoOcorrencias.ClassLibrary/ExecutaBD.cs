using System;
using System.Data.Common;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MigracaoOcorrenciasClassLibrary
{
    public static class ExecutaBD
    {
        public static DataTable RetornarDataTable(string isql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();
            da.SelectCommand = cd;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ds.Tables[0];
        }

        public static DataSet RetornarDataSet(string isql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();

            da.SelectCommand = cd;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ds;
        }

        public static void ExecutarSQL(string isql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();

            cn.ConnectionString = Conn;
            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();

            try
            {
                cd.ExecuteNonQuery();
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
        }

        public static int ExecutarSQLRetornarID(string isql, string Conn)
        {
            int ret = 0;
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();

            try
            {
                ret = Convert.ToInt32(cd.ExecuteScalar());
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }

        public static string ExecutarSQLRetornarIDs(string isql, string Conn)
        {
            string ret = "";
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;
            cd.CommandText = isql;
            cd.Connection = cn;
            cn.Open();


            try
            {
                ret = cd.ExecuteScalar().ToString();
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }

        public static string RetornarIdTabela(string Tabela, string cnn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

            cn.ConnectionString = cnn;
            cd.Connection = cn;
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();
            int nId = 0;
            try
            {
                // 1-Verifia se a tabela existe
                string sql = " SELECT COUNT(TABELA) AS QTD FROM IDTABELA WHERE TABELA = '" + Tabela.Trim() + "'";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;

                //se nao existe insere e já retorna o novo id
                if (Convert.ToInt32(cd.ExecuteScalar()) == 0)
                {
                    sql = " INSERT IDTABELA (Tabela, Id) VALUES( ";
                    sql += " '" + Tabela + "', ";
                    sql += " (SELECT ISNULL(MAX(" + "ID" + Tabela + "),0)+1 FROM " + Tabela + ")) SELECT ISNULL(MAX(ID),0) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    nId = Convert.ToInt32(cd.ExecuteScalar());
                    oTrans.Commit();
                    return nId.ToString();
                }
                else
                {
                    //se exite gera um novo e compara os ids, E OS ACERTA para efetivar
                    sql = " UPDATE IDTABELA SET ID = ";
                    sql += " CASE WHEN (SELECT MAX(ID)+1 FROM IDTABELA WHERE TABELA='" + Tabela + "' )>=(SELECT coalesce(MAX(ID" + Tabela + "),0) FROM " + Tabela + ") THEN ID+1 ";
                    sql += " ELSE (SELECT MAX(ID" + Tabela + ")+1 FROM " + Tabela + ")";
                    sql += " END WHERE TABELA='" + Tabela + "' SELECT MAX(ID) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    nId = Convert.ToInt32(cd.ExecuteScalar());
                    oTrans.Commit();
                    return nId.ToString();
                }
            }
            catch (Exception ex)
            {
                oTrans.Rollback();
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }

        public static class EnviarEmails
        {
            public static void EnviarEmail(string para, string de, string Assunto, string Mensagem, string smtp, string senhaSmtp)
            {
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient();
                try
                {
                    message.From = new MailAddress(de, de, System.Text.Encoding.GetEncoding(1252));
                    message.SubjectEncoding = System.Text.Encoding.GetEncoding(1252);
                    message.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
                    string[] destinatarios = para.Split(';');

                    foreach (string dest in destinatarios)
                    {
                        if (dest.Trim() != "")
                            message.CC.Add(dest);
                    }
                    message.Subject = Assunto;
                    string MsgTipo = MediaTypeNames.Text.Html;
                    AlternateView alternate = AlternateView.CreateAlternateViewFromString(Mensagem, System.Text.Encoding.GetEncoding(1252), MsgTipo);
                    message.AlternateViews.Add(alternate);
                    client.Credentials = new System.Net.NetworkCredential(de, senhaSmtp);
                    client.Host = smtp;
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("erro");
                    DataRow r;
                    r = dt.NewRow();
                    r[0] = ex.Message;
                    dt.Rows.Add(r);
                    dt.WriteXml("erro.xml");

                }
                finally
                {
                    message.Dispose();
                    message = null;
                    client = null;
                }
            }
        }

        public static class Helpers
        {
            public static string FormatarCnpj(string s)
            {
                s = s.Replace(".", "");
                s = s.Replace("-", "");
                s = s.Replace("/", "");
                s = s.Replace(@"\", "");

                if (s.Length == 0)
                {
                    return "";
                }

                if (s.Length <= 11)
                {
                    MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                    mtpCpf.Set(ZerosEsquerda(s, 11));
                    return mtpCpf.ToString();
                }
                else
                {
                    MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                    mtpCnpj.Set(ZerosEsquerda(s, 11));
                    return mtpCnpj.ToString();
                }
            }

            private static string ZerosEsquerda(string strString, int intTamanho)
            {
                string strResult = "";

                for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
                {
                    strResult += "0";
                }

                return strResult + strString;
            }

        }

        public static DataTable RetonarNaoMigrados(string connOrigem)
        {
            string strsql = " SELECT TR.IDTRANSFERENCIA, TR.OPERACAO,";
            strsql += " CNH.IDNOTAFISCAL, ";
            strsql += " CNH.CONHECIMENTO NOTAFISCAL, ";
            strsql += " CNH.SERIEDOCONHECIMENTO SERIE, ";
            strsql += " /*CAST(O.CODIGODAOCORRENCIA AS INT) CODIGODAOCORRENCIA*/ O.CODIGODAOCORRENCIA, ";
            strsql += " CNH.FILIALLANCAMENTO + CNH.CONTROLE + CNH.CONHECIMENTO AS ID ,   CNH.FILIALLANCAMENTO,  O.DESCRICAO,  CO.DATADAOCORRENCIA";
            strsql += " FROM TRANSFERENCIA TR ";
            strsql += " INNER JOIN CONHECIMENTOOCORRENCIA CO ON (CO.FILIAL = SUBSTRING(TR.CHAVE,1,2) AND CO.LANCAMENTO = SUBSTRING(TR.CHAVE,3,12)) ";
            strsql += " INNER JOIN CONHECIMENTO CNH ON (CNH.FILIALLANCAMENTO = CO.FILIALLANCAMENTO AND CNH.CONTROLE = CO.CONTROLE) ";
            strsql += " INNER JOIN OCORRENCIA O ON (O.SERIEDAOCORRENCIA = CO.SERIEDAOCORRENCIA AND O.CODIGODAOCORRENCIA = CO.CODIGODAOCORRENCIA) WHERE TR.TABELA = 'CONHECIMENTOOCORRENCIA'   ";
            strsql += " AND CNH.IDNOTAFISCAL IS NOT NULL  AND  TR.TRANSFERIDO='NAO'";
            strsql += " AND CNH.DATADEEMISSAO >= '2011-09-01'";
            return RetornarDataTable(strsql, connOrigem);
        }

        public static void AlterarDocumentoOcorrencia(string connDestino, string ConnOrigem, string IDDocumento, string IDFilial, string IDOcorrencia, DateTime DataOcorrencia, string Descricao, int IdTransferencia)
        {
            try
            {
                string strsql;
                strsql = "UPDATE TRANSFERENCIA SET Transferido ='SIM' WHERE IDTRANSFERENCIA =" + IdTransferencia + " select '1'";
                //ExecutarSQLRetornarIDs(strsql, ConnOrigem);
                EscreverLog("TRANFERENCIA OK: " + IdTransferencia);
            }
            catch (Exception EX)
            {
                EscreverLog(EX.Message + "IDDocumento: " + IDDocumento);
            }
        }

        public static void InserirDocumentoOcorrencia(string connDestino, string ConnOrigem, string IDDocumento, string IDFilial, string IDOcorrencia, DateTime DataOcorrencia, string Descricao, int IdTransferencia)
        {
            try
            {
               string IdDocOco = RetornarIdTabela("DOCUMENTOOCORRENCIA", connDestino);
                string strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
                strsql += " IDDocumentoOcorrencia,";
                strsql += " IDDocumento,";
                strsql += " IDFilial,";
                strsql += " IDOcorrencia,";
                strsql += " DataOcorrencia,";
                strsql += " Descricao,";
                strsql += " Sistema";
                strsql += " ) VALUES (";
                strsql +=  IdDocOco + " ,";
                strsql += IDDocumento + " ,";
                strsql += Convert.ToInt32(IDFilial) + " ,";
                strsql +=int.Parse( IDOcorrencia) + " ,";
                strsql += " convert(datetime,'" + DataOcorrencia + "', 103) ,";
                strsql += " '" + Descricao.ToUpper().Trim() + "',";
                strsql += "'SIM'";
                strsql += " ) select '1' ";

               ExecutarSQLRetornarIDs(strsql, connDestino);



               strsql = "SELECT FINALIZADOR FROM OCORRENCIA WHERE IDOCORRENCIA = " + int.Parse(IDOcorrencia);
               string finalizadora = ExecutarSQLRetornarIDs(strsql, connDestino);

               strsql = "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " " + (finalizadora=="SIM"?" , DATADECONCLUSAO= convert(datetime,'" + DataOcorrencia + "', 103)": "") + "  WHERE IDDocumento=" + IDDocumento + " Select '1'";
               ExecutarSQLRetornarIDs(strsql, connDestino);


                strsql = "UPDATE TRANSFERENCIA SET Transferido ='SIM' WHERE IDTRANSFERENCIA =" + IdTransferencia + " select'1'";
                ExecutarSQLRetornarIDs(strsql, ConnOrigem);
                EscreverLog("TRANFERENCIA OK: " + IdTransferencia);

                

            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        public static void EscreverLog(string menssagem)
        {
            //string nomeArquivo = ConfigurationSettings.AppSettings["ArquivoLog"].Replace("DDMMYYYY", DateTime.Now.ToShortDateString());
            //StreamWriter writer = new StreamWriter(nomeArquivo, true);
            //writer.WriteLine("DATA: " + DateTime.Now + " =>>" + menssagem.ToUpper());
            //writer.Close();
        }

        public static DataTable RetonarNaoEntregue(string BASE_DEST)
        {
            string strsql = " SELECT NF.IDDOCUMENTO, NF.DOCUMENTODOCLIENTE1 , NF.NUMERO";
            strsql += " FROM DOCUMENTO NF     ";
            strsql += " INNER JOIN CADASTRO REME ON (REME.IDCADASTRO = NF.IDREMETENTE)     ";
            strsql += " INNER JOIN CADASTRO DEST ON (DEST.IDCADASTRO = NF.IDDESTINATARIO)     ";
            strsql += " INNER JOIN CIDADE DESTCID ON (DESTCID.IDCIDADE = DEST.IDCIDADE)     ";
            strsql += " INNER JOIN ESTADO DESTEST ON (DESTEST.IDESTADO = DESTCID.IDESTADO)     ";
            strsql += " INNER JOIN FILIAL FL ON (FL.IDFILIAL = NF.IDFILIAL)     ";
            strsql += " WHERE   NF.TIPODEDOCUMENTO = 'NOTA FISCAL'    ";
            strsql += " AND NF.ATIVO='SIM'    ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN   CONVERT(DATETIME, '01/08/2011', 103)  and   CONVERT(DATETIME, '29/09/2020', 103)  ";
            strsql += " AND (NF.IDREMETENTE IN (444,446,445,62694)  OR NF.IDDESTINATARIO IN (444,446,445,62694)  OR NF.IDCLIENTE IN (444,446,445,62694) )  ";
            strsql += " AND DATADECONCLUSAO IS NULL ";
            strsql += " ORDER BY FL.NOME , ISNULL( NF.PRAZOUTILIZADO,1) , NF.DATADECONCLUSAO    ";
            //strsql += " AND CNH.DATADEEMISSAO >= '2011-06-01'";
            return RetornarDataTable(strsql, BASE_DEST);
        }


       

    }
    
}


    