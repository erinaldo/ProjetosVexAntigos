using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Reflection;
using System.Data.SqlServerCe;

namespace SistecnoColetor.Classes
{
    public static class BbColetor
    {
        #region H E L P E R S
        private static string RetornarConexao()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase), "Repositorio.sdf");
            return "Data Source=" + path;
        }


        public static void excSql_trans(string comando)
        {
            SqlCeConnection cnn = new SqlCeConnection(RetornarConexao());

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = CommandType.Text;
            cnn.Open();
           // SqlCeTransaction trans = cnn.BeginTransaction();

            try
            {


                cmd.CommandText = comando;
                cmd.Connection = cnn;
                //cmd.Transaction = trans;

                cmd.ExecuteNonQuery();


                //trans.Commit();
            }
            catch (Exception ex)
            {
                //trans.Rollback();
                throw ex;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cmd.Dispose();
                cnn.Dispose();
            }
        }

        public static void excSql_trans(List<string> comandos)
        {
            SqlCeConnection cnn = new SqlCeConnection(RetornarConexao());

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = CommandType.Text;
            cnn.Open();
            SqlCeTransaction trans = cnn.BeginTransaction();

            try
            {

                for (int i = 0; i < comandos.Count; i++)
                {
                    cmd.CommandText = comandos[i];
                    cmd.Connection = cnn;
                    cmd.Transaction = trans;

                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cmd.Dispose();
                cnn.Dispose();
            }
        }

        public static void excSql(List<string> comandos)
        {
            SqlCeConnection cnn = new SqlCeConnection(RetornarConexao());
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = CommandType.Text;
            try
            {
                cnn.Open();

                for (int i = 0; i < comandos.Count; i++)
                {
                    cmd.CommandText = comandos[i];
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cmd.Dispose();
                cnn.Dispose();
            }
        }

        public static  DataTable RetornarDataTable(string sql)
        {
            SqlCeConnection cnn = new SqlCeConnection(RetornarConexao());
            SqlCeCommand cmd = new SqlCeCommand();
            DataSet ds = new DataSet();

            cmd.CommandType = CommandType.Text;
            try
            {
                cnn.Open();
                cmd.CommandText = sql;
                cmd.Connection = cnn;

                SqlCeDataAdapter oda = new SqlCeDataAdapter(cmd);
                oda.Fill(ds);
                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cmd.Dispose();
                cnn.Dispose();


            }
        }


        public static DataSet RetornarDataSet(string sql)
        {
            SqlCeConnection cnn = new SqlCeConnection(RetornarConexao());
            SqlCeCommand cmd = new SqlCeCommand();
            DataSet ds = new DataSet();

            cmd.CommandType = CommandType.Text;
            try
            {
                cnn.Open();
                cmd.CommandText = sql;
                cmd.Connection = cnn;

                SqlCeDataAdapter oda = new SqlCeDataAdapter(cmd);
                oda.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cmd.Dispose();
                cnn.Dispose();


            }
        }

        #endregion


        #region Select Internas
        public static DataTable RetornarVolumesConferidosPorChave (string chave)
        {
            DataTable d=  RetornarDataTable("select CODIGODEBARRAS [VOLUME], IDReposicaoRogeVolume[CODIGO]  from   ReposicaoRogeVolume where IdReposicaoRoge in(select IdReposicaoRoge from  ReposicaoRoge where Chave = '" + chave + "' AND CONFERIDO='SIM')");

            DataTable dx = new DataTable();
            dx.Columns.Add("VOLUME");
            dx.Columns.Add("CODIGO");

            for (int i = 0; i < d.Rows.Count; i++)
            {
                DataRow x = dx.NewRow();

                x[0] = d.Rows[i][0].ToString();
                x[1] = d.Rows[i][1].ToString();

                dx.Rows.Add(x);
            }

            return dx;
        }

        public static void ApagarDados(string IdResosicaoRoge)
        {
            List<string> sql = new List<string>();



            string x = " delete  from ReposicaoRogeCB where IdReposicaoRogeItem in( ";
            x += " select IdReposicaoRogeItem from reposicaorogeitem where IdReposicaoRogeItem=" + IdResosicaoRoge + ")";
            excSql_trans(x);


            x = " delete  from reposicaorogevolume where IdReposicaoRoge=" + IdResosicaoRoge;
            excSql_trans(x);

            x = " delete from ItensConferenciaCega where IdReposicaoRoge=" + IdResosicaoRoge;
            excSql_trans(x);


            x = " delete  from reposicaorogeitem where IdReposicaoRoge=" + IdResosicaoRoge;
            excSql_trans(x);   

             x = "delete from reposicaoroge where idreposicaoroge=" + IdResosicaoRoge;
            excSql_trans(x);
           
        }

        

        #endregion
        //#region O C O R R E N C I A S

        //public void LimparOcorrencias()
        //{
        //    List<string> comandos = new List<string>();
        //    comandos.Add("delete from Ocorrencia");
        //    excSql(comandos);
        //}

        //public void GravarOcorrencias(RsMobile.webService.Ocorrencias[] oco)
        //{
        //    List<string> comandos = new List<string>();
        //    string sql = "";
        //    for (int i = 0; i < oco.Length; i++)
        //    {
        //        sql = " insert into Ocorrencia (IDOCORRENCIA, CODIGO, NOME, RESPONSABILIDADE, FINALIZADOR) values (" + oco[i].IDOCORRENCIA.Trim() + ", '" + oco[i].CODIGO.Trim().Trim() + "', '" + oco[i].NOME.Trim() + "', '" + oco[i].RESPONSABILIDADE + "', '" + oco[i].FINALIZADOR + "')   ";
        //        comandos.Add(sql);
        //    }

        //    this.excSql(comandos);
        //}


        //public DataTable RetornarOcorrencias()
        //{
        //    return RetornaDataTable("Select CODIGO, NOME, IDOCORRENCIA from Ocorrencia order by CODIGO");
        //}

        //#endregion

        //#region D O C U M E N T O S

        //public DataTable RetornaDocumentosPendentesTransmissao()
        //{
        //    return RetornaDataTable("Select * from Documento where TRANSMITIDO='NAO' AND DATADAOCORRENCIA IS NOT NULL");
        //}

        //public void LimparDocumentos()
        //{
        //    List<string> comandos = new List<string>();
        //    comandos.Add("delete from documento");
        //    excSql(comandos);
        //}

        //public void GravarDocumentos(RsMobile.webService.listar_documentos[] doc)
        //{
        //    List<string> comandos = new List<string>();
        //    string sql = "";
        //    for (int i = 0; i < doc.Length; i++)
        //    {
        //        sql = " insert into Documento (NUMERO, IDDOCUMENTOOCORRENCIA,NUMERODOCUMENTO,IDDOCUMENTO,IDFILIALATUAL,VOLUMES,PESOBRUTO,PLACA,NUMEROPLACA,IDDT,REMETENTE,DESTINATARIO,CIDADE,PENDENTE,TRANSMITIDO,ENDERECO,ESTADO) ";
        //        sql += " values ('" + doc[i].NUMERO.Trim() + "', " + doc[i].IDDOCUMENTOOCORRENCIA.Trim() + ",'" + doc[i].NUMERODOCUMENTO.Trim() + "'," + doc[i].IDDOCUMENTO + "," + doc[i].IDFILIALATUAL + "," + doc[i].VOLUMES.Trim() + ",'" + doc[i].PESOBRUTO.Trim() + "','" + doc[i].PLACA.Trim() + "','" + doc[i].PLACANUMERO.Trim() + "'," + doc[i].IDDT.Trim() + ",'" + doc[i].REMETENTE.Trim().Replace("'", "") + "','" + doc[i].DESTINATARIO.Trim().Replace("'", "") + "','" + doc[i].CIDADE.Trim().Replace("'", "") + "','" + (doc[i].IDDOCUMENTOOCORRENCIA == "0" ? "S" : "N") + "','" + (doc[i].IDDOCUMENTOOCORRENCIA == "0" ? "N" : "S") + "','" + doc[i].ENDERECO.Trim() + "','" + doc[i].ESTADO.Trim() + "')";
        //        comandos.Add(sql);
        //    }

        //    this.excSql(comandos);
        //}

        //public DataTable RetornarAllDocumentos()
        //{
        //    return RetornaDataTable("SELECT * FROM DOCUMENTO ORDER BY NUMERO");
        //}


        //public DataTable RetornarDocumentos(string IdDocumento)
        //{
        //    return RetornaDataTable("SELECT * FROM DOCUMENTO WHERE IDDOCUMENTO = " + IdDocumento);
        //}

        //public void SetarOcorrencia(string Lat, string Longi, string idOcorrencia, string idDocumento)
        //{
        //    string sql = "UPDATE DOCUMENTO SET LATITUDE ='" + Lat + "', LONGITUDE='" + Longi + "', TRANSMITIDO='N',PENDENTE='N', DATADAOCORRENCIA=getDate(), IDOCORRENCIA='" + idOcorrencia + "' WHERE IDDOCUMENTO=" + idDocumento;
        //    List<string> comandos = new List<string>();
        //    comandos.Add(sql);
        //    this.excSql(comandos);
        //}

        //public DataTable RetornarPendentesSincronizacao()
        //{
        //    return RetornaDataTable("Select * from documento where pendente='N' and TRANSMITIDO='N'");
        //}

        //public void AlterarStatusDocumento(String iddocumento)
        //{
        //    String sql = "UPDATE DOCUMENTO SET TRANSMITIDO='S' WHERE IDDOCUMENTO='" + iddocumento + "'";
        //    List<string> comandos = new List<string>();
        //    comandos.Add(sql);
        //    this.excSql(comandos);
        //}

        //public void GravarImagemBD(string iddocumento, string idocorrencia, byte[] imagem)
        //{
        //    string sql = "INSERT INTO DOCUMENTOFOTO (IDDOCUMENTO, IDOCORRENCIA, FOTO, TRANSMITIDO) VALUES (" + iddocumento + ", " + idocorrencia + ", @IMAGEM, 'N')";
        //    SqlCeCommand command = new SqlCeCommand();
        //    SqlCeConnection vv = new SqlCeConnection(RetornarConexao());
        //    command.CommandText = sql;
        //    command.CommandType = CommandType.Text;
        //    command.Connection = vv;
        //    command.Parameters.Add(new SqlCeParameter("@IMAGEM", imagem));
        //    vv.Open();
        //    command.ExecuteNonQuery();
        //    vv.Close();
        //    vv.Dispose();
        //}


        //#endregion

        //#region F O T O

        //public DataTable RetornaFotosPendentesTransmissao()
        //{
        //    return RetornaDataTable("Select * from DocumentoFoto where TRANSMITIDO='N' and IDDOCUMENTO in (select IDDOCUMENTO from documento where Transmitido='S') ");
        //}

        //public void DeletarFotoDispositivo(string iddocumento)
        //{
        //    String sql = "DELETE FROM  DocumentoFoto WHERE IDDOCUMENTO=" + iddocumento;
        //    List<string> comandos = new List<string>();
        //    comandos.Add(sql);
        //    this.excSql(comandos);
        //}


        //#endregion
        
        //#region A P A R E L H O

        //public DataTable RetornarInformacoesAparelho()
        //{
        //    return RetornaDataTable("Select * from Aparelho");            
        //}

        //public void GravarinformacoesAparelho(Aparelho apar)
        //{
        //    string sql = "Insert into Aparelho (Chave, Nome, Tempo, EnviaPosicaozerada, NumeroFone, EnviaFoto) values ('"+apar.Chave+"', '"+apar.Nome+"', '"+apar.Tempo+"', '"+apar.EnviaPosicaozerada+"', '"+apar.NumeroFone+"', '"+apar.EnviaFoto+"')";
        //    List<string> comandos = new List<string>();
        //    comandos.Add(sql);
        //    this.excSql(comandos);
        //}

        //public void LimparTabellaAparelho()
        //{
        //    string sql = "delete from Aparelho";
        //    List<string> comandos = new List<string>();
        //    comandos.Add(sql);
        //    this.excSql(comandos);
        //}
        
        //#endregion


        //#region S I N C R O N I Z A C A  O

        //public void GravarSincronizacao(Sincronizacao sinc)
        //{
        //    List<string> comandos = new List<string>();
        //    comandos.Add("delete from Sincronizacao");
        //    string sql = "Insert into Sincronizacao (Chave, DataInicial, dataFinal, Enviado, DT) values ('" + sinc.Chave + "', '" + DateTime.Parse(sinc.DataInicial).ToString("yyyy-MM-dd HH:mm:ss.fff") + "', '" + DateTime.Parse(sinc.DataFinal).ToString("yyyy-MM-dd HH:mm:ss.fff") + "', 'N', '" + sinc.DT + "')";
        //    comandos.Add(sql);
        //    this.excSql(comandos);
        //}


        //public void AtualizarSincronizacao()
        //{
        //    string sql = " update Sincronizacao set Enviado='S'";
        //    List<string> comandos = new List<string>();
        //    comandos.Add(sql);
        //    this.excSql(comandos);
        //}

        //public DataTable RetornarSincronizacao()
        //{
        //    return RetornaDataTable("Select * from Sincronizacao where Enviado='N'");
        //}


        //#endregion
        //public DataTable RetornarDadosTelaInicial()
        //{
        //    return RetornaDataTable("Select * from Conexao");
        //}


        //public void AterarDadosTelaInicial(string empr, string placa, string dt)
        //{
        //    string sql = "delete from Conexao";
        //    List<string> comandos = new List<string>();
        //    comandos.Add(sql);
        //    sql = "INSERT INTO CONEXAO (Empresa, Placa, DT) values ('"+empr+"', '"+placa+"', '"+dt+"')";
        //    comandos.Add(sql);

        //    this.excSql(comandos);

        //}


        internal static DataTable RetornarConferenciaPorChave(string chave)
        {
            return RetornarDataTable("select * from   ReposicaoRoge where Chave = '" + chave + "'"); 
         }

        public static List<DataTable> RetornarConferenciaCompleta(string chave)
        {
            List<DataTable> l = new List<DataTable>();
            
            string sql = "Select * from ReposicaoRoge where chave='"+chave+"'";
            DataTable dt1 = RetornarDataTable(sql);
            l.Add(dt1);


            sql = "Select * from ReposicaoRogeVolume where CONFERIDO='SIM' and  IdReposicaoRoge in (Select IdReposicaoRoge from ReposicaoRoge where chave='" + chave + "') ";
            DataTable dt2 = RetornarDataTable(sql);
            l.Add(dt2);

            sql = "Select CodigoDeBarrasLido, IdConferenciaItem, PerteceANota, sum(QuantidadeLido)  QuantidadeLido from ItensConferenciaCega where IdReposicaoRoge in (Select IdReposicaoRoge from ReposicaoRoge where chave='" + chave + "') group by CodigoDeBarrasLido, IdConferenciaItem, PerteceANota"; 
            DataTable dt3 = RetornarDataTable(sql);
            l.Add(dt3);

            return l;
        }
    }
}