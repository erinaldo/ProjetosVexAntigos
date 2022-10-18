using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data;
using MigracaoOcorrenciasClassLibrary;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;

namespace MigracaoOcorrencias.ClassLibrary
{
    public class Iniciar
    {
       
        public void iniciarProcessoImportacao(string BaseOrigems, string BaseDestinos)
        {
            EscreverLog("******************");
            EscreverLog("*");
            EscreverLog("INÍCIO DO PROCESSO");
            
            //EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Iniciou o Servico de Importação de Ocorrencias data: " + DateTime.Now.ToString(),"Serviço de importação de ocorrencias", "mail.sistecno.com.br", "mo2404");

            DataTable dtNaoImport = ExecutaBD.RetonarNaoMigrados(BaseOrigems);
            EscreverLog("PESQUISOU NOTAS NAO MINGRADAS. QTD: " + dtNaoImport.Rows.Count.ToString());
            int qtd = 0;

            for (int i = 0; i < dtNaoImport.Rows.Count; i++)
            {

                try
                {
                    string ID = dtNaoImport.Rows[i]["ID"].ToString().Trim();
                    string codOco = dtNaoImport.Rows[i]["CODIGODAOCORRENCIA"].ToString();
                    string idDocumento = "";

                    try
                    {
                        idDocumento = ExecutaBD.ExecutarSQLRetornarIDs("SELECT IDDOCUMENTO FROM DOCUMENTO WHERE DOCUMENTODOCLIENTE1='" + ID + "' ", BaseDestinos);
                    }
                    catch (Exception ex)
                    {
                        EscreverLog("NOTA NAO ENCONTRADA = INSERT IDNOTA: " + ID + " IDOCORRENCIA: " + codOco);
                
                    }

                    if (idDocumento != "0" && idDocumento != "")
                    {
                        int idFilial = DeParaFilial(dtNaoImport.Rows[i]["FILIALLANCAMENTO"].ToString());
                        string DescricaoOcorrencia = dtNaoImport.Rows[i]["DESCRICAO"].ToString();
                        string idOcorrencia = dtNaoImport.Rows[i]["CODIGODAOCORRENCIA"].ToString();
                        DateTime dtOcorrencia = Convert.ToDateTime(dtNaoImport.Rows[i]["DATADAOCORRENCIA"].ToString());
                        int idTransferencia = Convert.ToInt32(dtNaoImport.Rows[i]["IDTRANSFERENCIA"]);

                        if (idDocumento == "" || idDocumento == "0")
                        {
                            EscreverLog("NOTA NAO ENCONTRADA = INSERT IDNOTA: " + ID + " IDOCORRENCIA: " + codOco);
                        }
                        else
                        {
                            qtd++;
                            EscreverLog("IDNOTA: " + ID + " IDOCORRENCIA: " + codOco);
                            if (dtNaoImport.Rows[i]["OPERACAO"].ToString().ToUpper() == "INSERT")
                            {
                                ExecutaBD.InserirDocumentoOcorrencia(BaseDestinos, BaseOrigems, idDocumento, idFilial.ToString(), idOcorrencia, dtOcorrencia, DescricaoOcorrencia, idTransferencia);
                                EscreverLog("INSERT IDNOTA: " + ID + " IDOCORRENCIA: " + codOco);
                            }

                            else if (dtNaoImport.Rows[i]["OPERACAO"].ToString().ToUpper() == "UPDATE")
                            {
                                EscreverLog("UPDATE IDNOTA: " + ID + " IDOCORRENCIA: " + codOco);
                                ExecutaBD.InserirDocumentoOcorrencia(BaseDestinos, BaseOrigems, idDocumento, idFilial.ToString(), idOcorrencia, dtOcorrencia, DescricaoOcorrencia, idTransferencia);

                            }
                        }
                    }
                }
                catch (Exception EX)
                {
                    EscreverLog(EX.Message);
                    EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro no Servico de Importação de Ocorrencias data: " + DateTime.Now.ToString(), EX.Message, "mail.sistecno.com.br", "mo2404");
                }
            }
            
            EscreverLog("FIM DO PROCESSO");
            EscreverLog("******************");
            EscreverLog("*");
            //EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Encontrou: " + dtNaoImport.Rows.Count + " notas, Importou: "+ qtd +" notas .Finalizou o Servico de Importação de Ocorrencias data: " + DateTime.Now.ToString(), "Serviço de importação de ocorrencias", "mail.sistecno.com.br", "mo2404");
        }

        public int DeParaFilial(string Unidade)
        {            

            if (Unidade == "00") return 3;   // Campinas
            if (Unidade == "01") return 12;  // Campinas
            if (Unidade == "02") return 9;   // Taboao
            if (Unidade == "03") return 2;   // Ribeirao
            if (Unidade == "04") return 28;  // Marilia I
            if (Unidade == "05") return 4;   // Rio Preto
            if (Unidade == "07") return 6;   // Itapetininga
            if (Unidade == "08") return 7;   // Ricoy
            if (Unidade == "09") return 7;   // Sul
            if (Unidade == "10") return 16;  // Sao Jose Campos
            if (Unidade == "11") return 22;  // Frota
            if (Unidade == "12") return 6;   // Itapetininga
            if (Unidade == "13") return 17;  // Pres Prudente
            if (Unidade == "14") return 3;   // Matriz
            if (Unidade == "15") return 13;  // Guarulhos
            if (Unidade == "16") return 18;  // Registro
            if (Unidade == "17") return 19;  // Sao Bernardo
            if (Unidade == "23") return 5;   // Marilia II
            if (Unidade == "20") return 20;  // Logos Promocional Belval
            if (Unidade == "26") return 27;  // Embu Maxi
            if (Unidade == "27") return 10;  // Jadira
            if (Unidade == "22") return 33;  // JARINU
            if (Unidade == "FR") return 3;   // Campinas
            if (Unidade == "") return 3;   // Campinas

            return 3;
        }

        public static void EscreverLog(string menssagem)
        {
            //string nomeArquivo = ConfigurationSettings.AppSettings["ArquivoLog"].Replace("DDMMYYYY", DateTime.Now.ToShortDateString());
            //StreamWriter writer = new StreamWriter(nomeArquivo, true);
            ////writer.WriteLine("DATA: " + DateTime.Now + " =>>" + menssagem.ToUpper());
            //writer.WriteLine(menssagem.ToUpper());
            //writer.Close();
        }

        public List<UPDATESS> IniciarProcessoNotasFicaisNaoEntregue(string BaseOrigems, string BaseDestinos)
        {
            DataTable dtNaoImport = ExecutaBD.RetonarNaoEntregue(BaseDestinos);
            List<UPDATESS> list = new List<UPDATESS>();

            SqlConnection con = new SqlConnection(BaseOrigems);
            con.Open();

            for (int i = 0; i < dtNaoImport.Rows.Count; i++)
            {
                string filial = "", controle = "", conhecimento = "";

                filial = dtNaoImport.Rows[i]["DOCUMENTODOCLIENTE1"].ToString().Substring(0, 2);
                controle = dtNaoImport.Rows[i]["DOCUMENTODOCLIENTE1"].ToString().Substring(2, 12);
                conhecimento = dtNaoImport.Rows[i]["NUMERO"].ToString();

                SqlCommand comando = new SqlCommand("SELECT TOP 1 isnull(DATADEENTREGA, '') FROM  CONHECIMENTO C WHERE  IDNOTAFISCAL IS NOT NULL  AND CONHECIMENTO='" + conhecimento + "' AND IDNOTAFISCAL IS NOT NULL and Controle='" + controle + "' AND FilialLancamento = '"+ filial +"'", con);
                comando.CommandType = CommandType.Text;

                string data = "";
                try
                {
                    data = comando.ExecuteScalar().ToString();                    
                }
                catch (Exception)
                {   
                }


                if (data != "" && Convert.ToDateTime(data).Year>=2011)
                {
                    UPDATESS ilista = new UPDATESS();
                    ilista.UPD = "UPDATE DOCUMENTO SET DATADECONCLUSAO= CONVERT(DATETIME, '" + data + "', 103)    WHERE IDDOCUMENTO=" + dtNaoImport.Rows[i]["IDDOCUMENTO"].ToString();
                    list.Add(ilista);
                    ClassLibrary.Iniciar.EscreverLog(ilista.UPD);
                }

            }
            con.Close();
            con.Dispose();

            executarUPDATS(list, BaseDestinos);
            return list;
        }

        public void executarUPDATS( List<UPDATESS> ups, string BASE_DEST)
        {
            SqlConnection con = new SqlConnection(BASE_DEST);
            return;

            try
            {
                con.Open();
                for (int i = 0; i < ups.Count; i++)
                {
                    SqlCommand comando = new SqlCommand(ups[0].UPD.ToString(), con);
                    comando.CommandType = CommandType.Text;
                    comando.ExecuteNonQuery();

                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            
        }

        public void EnviarEmail(string para, string de, string Assunto, string Mensagem, string smtp, string senhaSmtp)
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

    public class UPDATESS
    {
        private string _UPD;

        public string UPD
        {
            get { return _UPD; }
            set { _UPD = value; }
        }


    }
}

