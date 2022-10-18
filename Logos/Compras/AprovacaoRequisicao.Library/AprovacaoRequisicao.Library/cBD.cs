using System;
using System.Data.Common;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;
using System.Collections.Generic;

namespace AprovacaoRequisicao.Library
{
    public static class cBD
    {

        public static DataTable RetonarTablePorConexao(string isql, string Conn)
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


        public static void ExecutarSemRetorno(string isql, string Conn)
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


        public static decimal RetornarDecimal(string isql, string Conn)
        {
            decimal ret = Convert.ToDecimal(0);
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
                ret = Convert.ToDecimal(cd.ExecuteScalar());
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }



        /// <summary>
        /// Retorna Novo Id Tabela 
        /// </summary>
        /// <param name="Conn">String de Conexão quando a sessao conn estiver vazia</param>
        /// <param name="Tabela">Nome da tabela</param>
        /// <returns>Retorna o Novo ID de Determinada Tabela</returns>
        public static string RetornarIdTabela(string Tabela, string con)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

            cn.ConnectionString = con;
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

        //public static string RetornarIdTabela(string Tabela, string conexao)
        //{
        //    DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        //    DbConnection cn = factory.CreateConnection();
        //    DbCommand cd = factory.CreateCommand();

        //    cn.ConnectionString = conexao;
        //    cd.Connection = cn;
        //    cn.Open();
        //    DbTransaction oTrans;
        //    oTrans = cn.BeginTransaction();
        //    int nId = 0;
        //    try
        //    {
        //        // 1-Verifia se a tabela existe
        //        string sql = " SELECT COUNT(TABELA) AS QTD FROM IDTABELA WHERE TABELA = '" + Tabela.Trim() + "'";

        //        cd.CommandText = sql;
        //        cd.CommandType = CommandType.Text;
        //        cd.Transaction = oTrans;

        //        //se nao existe insere e já retorna o novo id
        //        if (Convert.ToInt32(cd.ExecuteScalar()) == 0)
        //        {
        //            sql = " INSERT IDTABELA (Tabela, Id) VALUES( ";
        //            sql += " '" + Tabela + "', ";
        //            sql += " (SELECT ISNULL(MAX(" + "ID" + Tabela + "),0)+1 FROM " + Tabela + ")) SELECT ISNULL(MAX(ID),0) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

        //            cd.CommandText = sql;
        //            cd.CommandType = CommandType.Text;
        //            cd.Transaction = oTrans;
        //            nId = Convert.ToInt32(cd.ExecuteScalar());
        //            oTrans.Commit();
        //            return nId.ToString();
        //        }
        //        else
        //        {
        //            //se exite gera um novo e compara os ids, E OS ACERTA para efetivar
        //            sql = " UPDATE IDTABELA SET ID = ";
        //            sql += " CASE WHEN (SELECT MAX(ID)+1 FROM IDTABELA WHERE TABELA='" + Tabela + "' )>=(SELECT coalesce(MAX(ID" + Tabela + "),0) FROM " + Tabela + ") THEN ID+1 ";
        //            sql += " ELSE (SELECT MAX(ID" + Tabela + ")+1 FROM " + Tabela + ")";
        //            sql += " END WHERE TABELA='" + Tabela + "' SELECT MAX(ID) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

        //            cd.CommandText = sql;
        //            cd.CommandType = CommandType.Text;
        //            cd.Transaction = oTrans;
        //            nId = Convert.ToInt32(cd.ExecuteScalar());
        //            oTrans.Commit();
        //            return nId.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        oTrans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        cn.Close();
        //    }
        //}

        public static string RetornarIdTabelaWin(string Tabela, string cnn)
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


        public static void ExecutarComandoSql(string isql, string Conn)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = Conn;

            cd.CommandText = isql;
            cd.Connection = cn;
            cd.CommandTimeout = 999999999;
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

        public static string ExecutarRetornoIDServico(string isql, string Conn)
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
    }


    public class cEmail
    {
        public string para { get; set; }
        public string assunto { get; set; }
        public string mensagem { get; set; }


        public void enviarEmail(string Assunto, string mensagem, string destinatarios, string chaveIdentificadora)
        {
            cEmail msg = new cEmail();
            msg.mensagem = mensagem;
            msg.assunto = Assunto;
            msg.para = destinatarios;

            msg.Enviar(new cEmail.cConfig().CarregarConfiguracoes(chaveIdentificadora), msg);
        }

        public void Enviar(cConfig conf, cEmail msg)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                message.From = new MailAddress(conf.de, conf.apelidoDe.ToUpper());

                message.SubjectEncoding = System.Text.Encoding.GetEncoding(1252);
                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252);

                string[] destinatarios = msg.para.Split(';');

                foreach (string dest in destinatarios)
                {
                    if (dest.Trim() != "")
                        message.To.Add(dest);
                }

                message.Bcc.Add("moises@sistecno.com.br");
                message.Subject = msg.assunto.ToUpper();
                message.Priority = MailPriority.High;
                string MsgTipo = MediaTypeNames.Text.Html;

                AlternateView alternate = AlternateView.CreateAlternateViewFromString(msg.mensagem, System.Text.Encoding.GetEncoding(1252), MsgTipo);
                message.AlternateViews.Add(alternate);

                client.Credentials = new System.Net.NetworkCredential(conf.de, conf.senhaSmtp);
                client.Host = conf.smtp;
                client.Send(message);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                message.Dispose();
                message = null;
                client = null;
            }
        }

        public class cConfig
        {
            public string de { get; set; }
            public string apelidoDe { get; set; }
            public string smtp { get; set; }
            public string senhaSmtp { get; set; }

            public cConfig()
            {

            }

            public cConfig CarregarConfiguracoes(string id)
            {
                cConfig retor = new cConfig();

                DataSet ds = new DataSet();
                ds.ReadXml(@"\\192.168.10.1\Inetpub\wwwroot\BKP\ConfigEmailSite\ConfigEmail.xml");

                DataRow[] orw = ds.Tables[0].Select("id='" + id + "'", "");

                if (orw.Length > 0)
                {
                    retor.apelidoDe = orw[0]["apelido"].ToString();
                    retor.de = orw[0]["de"].ToString();
                    retor.smtp = orw[0]["smtp"].ToString();
                    retor.senhaSmtp = orw[0]["senha"].ToString();

                }
                return retor;
            }

        }

    }

    public static class EnviarEmails
    {
        public static void EnviarEmail(string para, string de, string Assunto, string Mensagem, string smtp, string senhaSmtp, string NomeEmailFrom)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                message.From = new MailAddress(de, NomeEmailFrom);

                message.SubjectEncoding = System.Text.Encoding.GetEncoding(1252);
                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252);

                string[] destinatarios = para.Split(';');

                foreach (string dest in destinatarios)
                {
                    if (dest.Trim() != "")
                        message.To.Add(dest);
                }

                message.Bcc.Add("moises@sistecno.com.br");
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
            }
            finally
            {
                message.Dispose();
                message = null;
                client = null;
            }
        }

        public static void EnviarEmail(string para, string de, string Assunto, string Mensagem, string smtp, string senhaSmtp)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                message.From = new MailAddress(de, de);

                message.SubjectEncoding = System.Text.Encoding.GetEncoding(1252);
                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252);

                string[] destinatarios = para.Split(';');

                foreach (string dest in destinatarios)
                {
                    if (dest.Trim() != "")
                        message.To.Add(dest);
                }

                message.Bcc.Add("moises@sistecno.com.br");
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
                DataTable dt = new DataTable("dterro");
                dt.Columns.Add("erro");
                DataRow r;
                r = dt.NewRow();
                r[0] = ex.Message;
                dt.Rows.Add(r);
                //dt.WriteXml("erro.xml");

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
}

 
