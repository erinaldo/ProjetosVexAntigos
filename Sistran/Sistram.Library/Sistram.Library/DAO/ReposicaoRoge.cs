using System.Text;
using System.Data;
using System.Web;
using System;
using Sistran.Library;

using System.Collections.Generic;
using Sistran.Library.DTO;
using System.IO;

namespace SistranDAO
{
    public class Estrutura
    {
        public string VolumeCodigoBarras { get; set; }
        public string ItemCodigoBarras { get; set; }
        public string CB_LIDO { get; set; }
    }

    public sealed class ReposicaoRoge
    {

        public void gravarConferencia(List<Estrutura> CbLidos)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            for (int i = 0; i < CbLidos.Count; i++)
            {
                if (CbLidos[i].VolumeCodigoBarras != "")
                {
                    string sql = "UPDATE ReposicaoRoge SET CodigoDeBarrasLido='" + CbLidos[i].CB_LIDO + "' where CodigoDeBarras='" + CbLidos[i].VolumeCodigoBarras + "'";
                    Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(sql, cnx);
                }
                else
                {
                    string sql = "UPDATE ReposicaoRoge SET CodigoDeBarrasLido='" + CbLidos[i].CB_LIDO + "' where CodigoDeBarras='" + CbLidos[i].ItemCodigoBarras + "'";
                    Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(sql, cnx);
                }
            }
        }

        public void CancelarConferencia(string chave, string usuario)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            string sql = "";

            sql = "SELECT * FROM ReposicaoRoge WHERE CHAVE='"+chave+"'";

            if (Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx).Rows.Count > 0)
            {

                sql = " UPDATE ReposicaoRoge SET STATUS='AGUARDANDO CONFERENCIA', INICIO=NULL, fim=null, UsuarioColetor=NULL,  DescricaoEnvioRoge='', DATACOLETOR=NULL/*, CHAVE=CHAVE+ '_" + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + "'*/ WHERE CHAVE ='" + chave + "' ";
                sql += " UPDATE REPOSICAOROGEVOLUME SET DATACONFERIDO=NULL, CONFERIDO='NAO' WHERE IdResposicaoRoge = (select IdReposicaoRoge from  ReposicaoRoge where Chave= '" + chave + "') ";
               
                sql += " delete from reposicaorogeConferenciaCega WHERE IdConferenciaItem in(select idREPOSICAOROGEITEM from REPOSICAOROGEITEM where IdReposicaoRoge= (select IdReposicaoRoge from  ReposicaoRoge where Chave= '" + chave + "')) ";

                sql += " delete from reposicaorogeConferenciaCega WHERE IdConferenciaItem=0; ";
                sql += " UPDATE ReposicaoRogeItem  set CodigoBarrasLido=NULL, DataConferido=null, QuantidadeLido =null, PerteceANota=null   WHERE IdReposicaoRoge = (select IdReposicaoRoge from  ReposicaoRoge where Chave= '" + chave + "') ";
                sql += " DELETE FROM REPOSICAOROGEITEM WHERE IDREPOSICAOROGE= (select IdReposicaoRoge from  ReposicaoRoge where Chave= '" + chave + "') AND (PERTECEANOTA='NAO' or codigoroge is null) ";

                Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(sql, cnx);

                string x = Sistran.Library.GetDataTables.RetornarIdTabela("ReposicaoRogeLog", cnx);
                sql =" INSERT INTO REPOSICAOROGELOG (IdReposicaoRogeLog, Login, Acao) VALUES (" + x + ", '" + usuario + "', 'Cancelar Conferencia da Nota:  " + chave + "') ";
                Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(sql, cnx);
            }
            else
            {
                throw new Exception("DOCUMENTO NAO ENCONTRADO");
            }
        }

        public void Gravar(string Chave, string IdNota, string CodigoRegiao, string NomeRegiao, string ClienteEspecial, List<Volumes> Volume, List<Itens> Item)
        {

            Log.GravarLog(Chave, "================="+Chave+"===================", "Gravar");    

            try
            {
                string sql = "";

                Log.GravarLog(Chave, "Inicio" + Chave, "Gravar");
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                
                string IdReposicaoRoge = Sistran.Library.GetDataTables.RetornarIdTabela("ReposicaoRoge", cnx);
                Log.GravarLog(Chave, "Pegou o Id: " + IdReposicaoRoge, "Gravar");

                Log.GravarLog(Chave, cnx, "Gravar");


                if (Volume == null)
                {
                    Log.GravarLog(Chave, "Volumes Vazios", "Gravar");
                    throw new Exception("Volumes nao Definidos pela Roge");

                }
                Log.GravarLog(Chave, "Vol.: " + Volume.Count, "Gravar");

                if (Item == null)
                {
                    Log.GravarLog(Chave, "Itens Vazios", "Gravar");
                    throw new Exception("Itens nao Definidos pela Roge");
                }

                Log.GravarLog(Chave, "Itens: " + Item.Count, "Gravar");    


                sql += " INSERT INTO ReposicaoRoge (IdReposicaoRoge, Chave, IdNota, CodigoRegiao, NomeRegiao, ClienteEspecial, DataDaInclusao) ";
                sql += " values (" + IdReposicaoRoge + ", '" + Chave + "', '" + IdNota + "', '" + CodigoRegiao + "', '" + NomeRegiao + "', '" + ClienteEspecial + "', getDate()) ;";


                //Log.GravarLog("", "SQL ReposicaoRoge : " + sql , "Gravar");    


                for (int i = 0; i < Volume.Count; i++)
                {
                    if (i == 0)
                        Log.GravarLog(Chave, "Varrendo Volumes", "Gravar");    

                    string IdReposicaoRogeVolume = Sistran.Library.GetDataTables.RetornarIdTabela("ReposicaoRogeVolume", cnx);
                    sql += " INSERT INTO REPOSICAOROGEVOLUME(IdReposicaoRogeVolume, IdResposicaoRoge, CodigoDeBarras, DataDaInclusao)";
                    sql += " VALUES(" + IdReposicaoRogeVolume + ", " + IdReposicaoRoge + ", '" + Volume[i].CodigoBarras + "', getDate())";

                    //Log.GravarLog(Chave, "Varreu o volume: " + Volume[i].CodigoBarras, "Gravar");
                    //Log.GravarLog("", "SQL volume : " + sql, "Gravar");    


                }
                

                for (int i = 0; i < Item.Count; i++)
                {
                    if(i==0)
                        Log.GravarLog(Chave, "Varrendo itens", "Gravar");    


                    string IdReposicaoRogeItem = Sistran.Library.GetDataTables.RetornarIdTabela("ReposicaoRogeItem", cnx);
                    sql += " INSERT INTO REPOSICAOROGEITEM (IdReposicaoRogeItem, IdReposicaoRoge, CodigoRoge, DataDaInclusao, Descricao, QuantidadeNota, ValorTotal, Valor) ";
                    sql += "VALUES (" + IdReposicaoRogeItem + ", " + IdReposicaoRoge + ", '" + Item[i].CodigoRoge + "', GETDATE(), '" + Item[i].Descricao + "', " + Item[i].Quantidade + ",  " + (Item[i].Valor == null ? "0" : (double.Parse(Item[i].Valor) / 100.00).ToString().Replace(",", ".")) + " ,  " + (Item[i].Valor == null ? "0" : (double.Parse(Item[i].Valor) / 100.00).ToString().Replace(",", ".")) + ")";
                 
                    for (int ii = 0; ii < Item[i].ItensCodigoBarras.Count; ii++)
                    {
                        string IdReposicaoRogeCB = Sistran.Library.GetDataTables.RetornarIdTabela("ReposicaoRogeCB", cnx);
                        sql += " INSERT INTO ReposicaoRogeCB(IdReposicaoRogeCB,IdReposicaoRogeItem,CodigoDeBarras,Embalagem,Quantidade) ";
                        sql += " VALUES(" + IdReposicaoRogeCB + "," + IdReposicaoRogeItem + ",'" + Item[i].ItensCodigoBarras[ii].CodigoDeBarras + "','" + Item[i].ItensCodigoBarras[ii].Embalagem + "'," + Item[i].ItensCodigoBarras[ii].EmbalagemQuantidade + ")";

                        //Log.GravarLog(Chave, "Varreu o item: " + Item[i].ItensCodigoBarras[ii].CodigoDeBarras, "Gravar");    

                    }
                }

                Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(sql, cnx);
               // Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Gravação de Notas", Chave + " - " + sql, "mail.sistecno.com.br", "@oncetsis14", "(sql)Gravacao De Nota Roge");
                Log.GravarLog(Chave, "Gravou com sucesso: " + sql, "Gravar");
                Log.GravarLog(Chave, "====================================", "Gravar");    
                
            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("rovani@oai.com.br", "moises@sistecno.com.br", "Erro: Gravação de Notas", Chave + " - " + ex.Message + ex.InnerException, "mail.sistecno.com.br", "@oncetsis14", "Gravacao De Nota Roge");
                Log.GravarLog(Chave, "erro: " + ex.Message + ex.InnerException, "Gravar");

                throw ex;
            }
        }

        private string MapPath(string p)
        {
            throw new NotImplementedException();
        }

        public DataSet ResgatarDocumentoColetor(string chave, string LoginUsuario)
        {
            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                string sql = "exec PRC_RESGATARDOCUMENTO '"+ chave +"'";
                DataSet Ds = Sistran.Library.GetDataTables.RetornarDataSet(sql, cnx);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    sql = "UPDATE REPOSICAOROGE SET USUARIOCOLETOR='" + LoginUsuario + "', DATACOLETOR=GETDATE(), Status='EM CONFERENCIA'  WHERE CHAVE='" + Ds.Tables[0].Rows[0][1].ToString() + "'";
                    Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(sql, cnx);
                }

               

                return Ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    public static class Log
    {
        public static void GravarLog(string NomeArquivo, string MenssagemLog, string NomeFuncao)
        {
            //try
            //{
            //    StreamWriter valor = new StreamWriter(@"\\192.168.10.1\Inetpub\wwwroot\www.grupologos.com.br\wss\log" + "\\" + NomeArquivo + ".txt", true, Encoding.Unicode);
            //    valor.Write(DateTime.Now.ToString() + " | " + NomeFuncao + " | " + MenssagemLog + "\r\n");
            //    valor.Close();
            //}
            //catch (Exception)
            //{
            //}
        }
    }
}