using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace ServicosWEB
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class ReposicaoRogeInterno : System.Web.Services.WebService
    {

        public class vol
        {
            public string CB { get; set; }
            public string Pertence { get; set; }
        }

        [WebMethod]
        public string ReceberVolumes(List<vol> CodigoDeBarras, string IdGaiola, string idUsuario, string Coletor, string emei, string QtdVolumesLidos)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                IdGaiola = int.Parse(IdGaiola).ToString();

                string sql = "SELECT COUNT(*) FROM GAIOLA WHERE IDGAIOLA=" + IdGaiola;
                DataTable tmpx = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                if (tmpx.Rows[0][0].ToString() == "0")
                    throw new Exception("A Gaiola " + IdGaiola + " Foi apagada");

                string SQL = "";


                bool pendencia = false;
                for (int i = 0; i < CodigoDeBarras.Count; i++)
                {
                    string idGaiolaConf = Sistran.Library.GetDataTables.RetornarIdTabela("GAIOLACONFERENCIA", cnx);

                    string s = "SELECT * FROM GAIOLACONFERENCIA WHERE CODIGODEBARRAS='" + CodigoDeBarras[i].CB.ToString() + "' AND PERTENCEAFILIAL='SIM'";
                    DataTable tmp = Sistran.Library.GetDataTables.RetornarDataTableWin(s, cnx);

                    string sit = "OK";
                    if (tmp.Rows.Count > 0)
                    {

                        pendencia = true;

                        DataRow[] o = tmp.Select("Idgaiola <> " + IdGaiola);

                        if (o.Length > 0)
                            sit = "PENDENCIA: VOLUME JA LIDO NA GAIOLA: " + tmp.Rows[0]["IdGaiola"];


                        DataRow[] o2 = tmp.Select("Idgaiola = " + IdGaiola);

                        if (o2.Length > 0)
                            sit = "PENDENCIA: VOLUME LIDO EM DUPLICIDADE";

                    }


                    SQL += " INSERT INTO GAIOLACONFERENCIA (IDGAIOLACONFERENCIA, IDGAIOLA,CODIGODEBARRAS,ROTEIRO, PERTENCEAFILIAL,IDUSUARIO,DATA, NumeroColetor, Emei, Situacao, Ativo) values";
                    SQL += " (" + idGaiolaConf + ", " + IdGaiola + ",'" + CodigoDeBarras[i].CB.ToString() + "','', '" + CodigoDeBarras[i].Pertence.ToString() + "'," + idUsuario + ",GETDATE(), '" + Coletor + "',  '" + emei + "', '" + sit + "', '" + (sit.Contains("DUPLICIDADE") ? "NAO" : "SIM") + "'); ";
                }

                if (CodigoDeBarras.Count == 0)
                    throw new Exception("Nenhum dado recebido");


                string ss = "SELECT COUNT(*) FROM GAIOLACONFERENCIA WHERE SITUACAO LIKE 'PENDENCIA: VOLUME JA LIDO NA GAIOLA:%' AND IDGAIOLA=" + IdGaiola;
                DataTable tmp1 = Sistran.Library.GetDataTables.RetornarDataTableWin(ss, cnx);

                if (tmp1.Rows[0][0].ToString() != "0")
                {
                    pendencia = true;
                }


                // SQL += "UPDATE GAIOLA SET QTDVOLUMESLIDOS=" + QtdVolumesLidos + ", DATAFECHAMENTO=GETDATE(), SITUACAO='FECHADO' WHERE IDGAIOLA=" + IdGaiola;
                SQL += "UPDATE GAIOLA SET QtdVolumesLidos=" + QtdVolumesLidos + ", DataFechamento=GetDate(), SITUACAO='" + (pendencia == false ? "FECHADO" : "PENDENCIA") + "' where IDGAIOLA=" + IdGaiola;
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(SQL, cnx);

                try
                {
                    sql += " INSERT INTO GAIOLAHISTORICO VALUES (" + IdGaiola + ", NULL, GETDATE(), 'ENVIOU OS VOLUMES', " + idUsuario + ") ";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);

                }
                catch (Exception)
                { }

                return "OK";

            }
            catch (Exception ex)
            {
                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - ReceberVolumes", "Gaiola: " + IdGaiola + " - " + ex.Message + ex.InnerException, "mail.sistecno.com.br", "@oncetsis14", "ReceberVolumes");
                }
                catch (Exception)
                { }

                return "Err^" + ex.Message;
            }
        }


        [WebMethod]
        public string ReceberConferenciaEmbarqueGaiola(List<int> gaiolas, int idDt, int idUsuario)
        {
            try
            {
                string sql = "Update Dt set Andamento='CONFERIDO' where IdDt=" + idDt + " ; ";
                for (int i = 0; i < gaiolas.Count; i++)
                {
                    sql += "Insert into DtGaiola (IdDt, IdGaiola, IdUsuario, Data) Values (" + idDt + ", " + gaiolas[i].ToString() + ", " + idUsuario + ", getDate()) ;";
                }
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);
                return "OK";
            }
            catch (Exception ex)
            {
                return "Err^ " + ex.Message;
            }
        }
    


        [WebMethod]
        public string ReceberVolumes2(List<vol> CodigoDeBarras, string IdGaiola, string idUsuario, string Coletor, string emei, string QtdVolumesLidos, string QuantidadeConfirmadaNoColetor)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                IdGaiola = int.Parse(IdGaiola).ToString();

                string sql = "SELECT COUNT(*) FROM GAIOLA WHERE IDGAIOLA=" + IdGaiola;
                DataTable tmpx = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                if (tmpx.Rows[0][0].ToString() == "0")
                    throw new Exception("A Gaiola " + IdGaiola + " Foi apagada");

                string SQL = "";


                bool pendencia = false;
                for (int i = 0; i < CodigoDeBarras.Count; i++)
                {
                    string idGaiolaConf = Sistran.Library.GetDataTables.RetornarIdTabela("GAIOLACONFERENCIA", cnx);

                    string s = "SELECT * FROM GAIOLACONFERENCIA WHERE CODIGODEBARRAS='" + CodigoDeBarras[i].CB.ToString() + "' AND PERTENCEAFILIAL='SIM'";
                    DataTable tmp = Sistran.Library.GetDataTables.RetornarDataTableWin(s, cnx);

                    string sit = "OK";
                    if (tmp.Rows.Count > 0)
                    {

                        pendencia = true;

                        DataRow[] o = tmp.Select("Idgaiola <> " + IdGaiola);

                        if (o.Length > 0)
                            sit = "PENDENCIA: VOLUME JA LIDO NA GAIOLA: " + tmp.Rows[0]["IdGaiola"];


                        DataRow[] o2 = tmp.Select("Idgaiola = " + IdGaiola);

                        if (o2.Length > 0)
                            sit = "PENDENCIA: VOLUME LIDO EM DUPLICIDADE";

                    }


                    SQL += " INSERT INTO GAIOLACONFERENCIA (IDGAIOLACONFERENCIA, IDGAIOLA,CODIGODEBARRAS,ROTEIRO, PERTENCEAFILIAL,IDUSUARIO,DATA, NumeroColetor, Emei, Situacao, Ativo) values";
                    SQL += " (" + idGaiolaConf + ", " + IdGaiola + ",'" + CodigoDeBarras[i].CB.ToString() + "','', '" + CodigoDeBarras[i].Pertence.ToString() + "'," + idUsuario + ",GETDATE(), '" + Coletor + "',  '" + emei + "', '" + sit + "', '" + (sit.Contains("DUPLICIDADE") ? "NAO" : "SIM") + "'); ";
                }

                if (CodigoDeBarras.Count == 0)
                    throw new Exception("Nenhum dado recebido");


                string ss = "SELECT COUNT(*) FROM GAIOLACONFERENCIA WHERE SITUACAO LIKE 'PENDENCIA: VOLUME JA LIDO NA GAIOLA:%' AND IDGAIOLA=" + IdGaiola;
                DataTable tmp1 = Sistran.Library.GetDataTables.RetornarDataTableWin(ss, cnx);

                if (tmp1.Rows[0][0].ToString() != "0")
                {
                    pendencia = true;
                }


                // SQL += "UPDATE GAIOLA SET QTDVOLUMESLIDOS=" + QtdVolumesLidos + ", DATAFECHAMENTO=GETDATE(), SITUACAO='FECHADO' WHERE IDGAIOLA=" + IdGaiola;
                SQL += "UPDATE GAIOLA SET QtdConfirmadaColetor="+QuantidadeConfirmadaNoColetor+",QtdVolumesLidos=" + QtdVolumesLidos + ", DataFechamento=GetDate(), SITUACAO='" + (pendencia == false ? "FECHADO" : "PENDENCIA") + "' where IDGAIOLA=" + IdGaiola;
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(SQL, cnx);

                try
                {
                    sql += " INSERT INTO GAIOLAHISTORICO VALUES (" + IdGaiola + ", NULL, GETDATE(), 'ENVIOU OS VOLUMES', " + idUsuario + ") ";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);

                }
                catch (Exception)
                { }

                return "OK";

            }
            catch (Exception ex)
            {
                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - ReceberVolumes", "Gaiola: " + IdGaiola + " - " + ex.Message + ex.InnerException, "mail.sistecno.com.br", "@oncetsis14", "ReceberVolumes");
                }
                catch (Exception)
                { }

                return "Err^" + ex.Message;
            }
        }


        [WebMethod]
        public string ReceberVolumes2a(List<vol> CodigoDeBarras, string IdGaiola, string idUsuario, string Coletor, string emei, string QtdVolumesLidos, string QuantidadeConfirmadaNoColetor)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                IdGaiola = int.Parse(IdGaiola).ToString();

                string sql = "SELECT COUNT(*) FROM GAIOLA WHERE IDGAIOLA=" + IdGaiola;
                DataTable tmpx = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                if (tmpx.Rows[0][0].ToString() == "0")
                    throw new Exception("A Gaiola " + IdGaiola + " Foi apagada");

                string SQL = "";


                bool pendencia = false;
                string retorno = "";
                for (int i = 0; i < CodigoDeBarras.Count; i++)
                {
                    string idGaiolaConf = Sistran.Library.GetDataTables.RetornarIdTabela("GAIOLACONFERENCIA", cnx);

                    string s = "SELECT * FROM GAIOLACONFERENCIA WHERE CODIGODEBARRAS='" + CodigoDeBarras[i].CB.ToString() + "' AND PERTENCEAFILIAL='SIM'";
                    DataTable tmp = Sistran.Library.GetDataTables.RetornarDataTableWin(s, cnx);

                    string sit = "OK";
                    if (tmp.Rows.Count > 0)
                    {

                        pendencia = true;

                        DataRow[] o = tmp.Select("Idgaiola <> " + IdGaiola);

                        if (o.Length > 0)
                            sit = "PENDENCIA: VOLUME JA LIDO NA GAIOLA: " + tmp.Rows[0]["IdGaiola"];


                        DataRow[] o2 = tmp.Select("Idgaiola = " + IdGaiola);

                        if (o2.Length > 0)
                            sit = "PENDENCIA: VOLUME LIDO EM DUPLICIDADE";

                    }

                    if (!sit.Contains("DUPLICIDADE"))
                    {
                        SQL += " INSERT INTO GAIOLACONFERENCIA (IDGAIOLACONFERENCIA, IDGAIOLA,CODIGODEBARRAS,ROTEIRO, PERTENCEAFILIAL,IDUSUARIO,DATA, NumeroColetor, Emei, Situacao, Ativo) values";
                        SQL += " (" + idGaiolaConf + ", " + IdGaiola + ",'" + CodigoDeBarras[i].CB.ToString() + "','', '" + CodigoDeBarras[i].Pertence.ToString() + "'," + idUsuario + ",GETDATE(), '" + Coletor + "',  '" + emei + "', '" + sit + "', '" + (sit.Contains("DUPLICIDADE") ? "NAO" : "SIM") + "'); ";
                    }
                    else {
                        retorno += "\r\n" + CodigoDeBarras[i].CB.ToString();
                    }
                }

                if (CodigoDeBarras.Count == 0)
                    throw new Exception("Nenhum dado recebido");


                string ss = "SELECT COUNT(*) FROM GAIOLACONFERENCIA WHERE SITUACAO LIKE 'PENDENCIA: VOLUME JA LIDO NA GAIOLA:%' AND IDGAIOLA=" + IdGaiola;
                DataTable tmp1 = Sistran.Library.GetDataTables.RetornarDataTableWin(ss, cnx);

                if (tmp1.Rows[0][0].ToString() != "0")
                {
                    pendencia = true;
                }


                // SQL += "UPDATE GAIOLA SET QTDVOLUMESLIDOS=" + QtdVolumesLidos + ", DATAFECHAMENTO=GETDATE(), SITUACAO='FECHADO' WHERE IDGAIOLA=" + IdGaiola;
                SQL += "UPDATE GAIOLA SET QtdConfirmadaColetor=" + QuantidadeConfirmadaNoColetor + ",QtdVolumesLidos=" + QtdVolumesLidos + ", DataFechamento=GetDate(), SITUACAO='" + (pendencia == false ? "FECHADO" : "PENDENCIA") + "' where IDGAIOLA=" + IdGaiola;
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(SQL, cnx);

                try
                {
                    sql += " INSERT INTO GAIOLAHISTORICO VALUES (" + IdGaiola + ", NULL, GETDATE(), 'ENVIOU OS VOLUMES', " + idUsuario + ") ";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);

                }
                catch (Exception)
                { }


                if (retorno != "")
                    return "OK - Os Volumes lidos já foram lidos em outra Gaiola e não foram gravados: \r\n " + retorno;


                return "OK";

            }
            catch (Exception ex)
            {
                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - ReceberVolumes", "Gaiola: " + IdGaiola + " - " + ex.Message + ex.InnerException, "mail.sistecno.com.br", "@oncetsis14", "ReceberVolumes");
                }
                catch (Exception)
                { }

                return "Err^" + ex.Message;
            }
        }

        [WebMethod]
        public string ChecarBandeira(string CodigoDeBarrasBandira)
        {
            try
            {
                if (CodigoDeBarrasBandira.Length < 15)
                    throw new Exception("Codigo de Barras com comprimento inferior ao tamanho correto.");

                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                string filial = CodigoDeBarrasBandira.Substring(0, 4);
                string sequencial = CodigoDeBarrasBandira.Substring(4, 10);
                string via = CodigoDeBarrasBandira.Substring(14, 1);

                string SQL = "Select * from Gaiola where cast(filial as int) ='" + int.Parse(filial) + "' and Gaiola='" + int.Parse(sequencial) + "'";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(SQL, cnx);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["DATAFECHAMENTO"].ToString() != "")
                    {
                        throw new Exception("BANDEIRA JÁ FINALIZADA.");
                    }

                    if (dt.Rows[0]["IMPRESSO"].ToString().Trim() == "0")
                        throw new Exception("BANDEIRA NÃO IMPRESSA.");
                    else
                    {
                        if (via.Trim() == dt.Rows[0]["IMPRESSO"].ToString().Trim())
                        {
                            Sistran.Library.GetDataTables.ExecutarRetornoIDWin("UPDATE GAIOLA SET SITUACAO='EM CONFERENCIA' WHERE IDGAIOLA="+ CodigoDeBarrasBandira.Substring(4,10) , cnx);

                            Sistran.Library.GetDataTables.ExecutarRetornoIDWin(" INSERT INTO GaiolaHistorico VALUES (" + int.Parse(sequencial) + ", NULL, GETDATE(), 'COLOCOU A BANDEIRA EM CONFERENCIA', 0) ", cnx);

                            return "OK";
                        }
                        else
                        {
                            throw new Exception("Favor faça a impressão novamente da bandeira. Pois a versão da impressão não corresponde a última.");
                        }
                    }
                }
                else
                {
                 
                    throw new Exception("GAIOLA OU FILIAL INCORRETA");
                }
            }
            catch (Exception ex)
            {
                return "Err^" + ex.Message;
            }
        }

        [WebMethod]

        public string ChecarBandeira_v2(string CodigoDeBarrasBandira, string idUsuario)
        {
            try
            {
                if (CodigoDeBarrasBandira.Length < 15)
                    throw new Exception("Codigo de Barras com comprimento inferior ao tamanho correto.");

                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                string filial = CodigoDeBarrasBandira.Substring(0, 4);
                string sequencial = CodigoDeBarrasBandira.Substring(4, 10);
                string via = CodigoDeBarrasBandira.Substring(14, 1);

                string SQL = "Select * from Gaiola where cast(filial as int) ='" + int.Parse(filial) + "' and Gaiola='" + int.Parse(sequencial) + "'";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(SQL, cnx);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["DATAFECHAMENTO"].ToString() != "")
                    {
                        throw new Exception("BANDEIRA JÁ FINALIZADA.");
                    }

                    if (dt.Rows[0]["IMPRESSO"].ToString().Trim() == "0")
                        throw new Exception("BANDEIRA NÃO IMPRESSA.");
                    else
                    {
                        if (via.Trim() == dt.Rows[0]["IMPRESSO"].ToString().Trim())
                        {
                            Sistran.Library.GetDataTables.ExecutarRetornoIDWin("UPDATE GAIOLA SET SITUACAO='EM CONFERENCIA' WHERE IDGAIOLA=" + CodigoDeBarrasBandira.Substring(4, 10), cnx);

                            Sistran.Library.GetDataTables.ExecutarRetornoIDWin(" INSERT INTO GaiolaHistorico VALUES (" + int.Parse(sequencial) + ", NULL, GETDATE(), 'COLOCOU A BANDEIRA EM CONFERENCIA', "+idUsuario+") ", cnx);

                            return "OK";
                        }
                        else
                        {
                            throw new Exception("Favor faça a impressão novamente da bandeira. Pois a versão da impressão não corresponde a última.");
                        }
                    }
                }
                else
                {

                    throw new Exception("GAIOLA OU FILIAL INCORRETA");
                }
            }
            catch (Exception ex)
            {
                return "Err^" + ex.Message;
            }
        }

        [WebMethod]
        public string CriarGaiola(string CodigoBarrasVolume, string idUsuario, string NumeroColetor, string EMEI)
        {
            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();


                //verifica se o volume informado ja nao foi lido anteriormente

                DataTable dtv = Sistran.Library.GetDataTables.RetornarDataTableWin("SELECT COUNT(*) FROM GAIOLA WHERE VolumeInicial='"+CodigoBarrasVolume+"'", cnx);

                if (int.Parse(dtv.Rows[0][0].ToString()) > 0)
                    throw new Exception("Este volume já foi usada para abrir outra gaiola");


                string idGaiola = Sistran.Library.GetDataTables.RetornarIdTabela("GAIOLA", cnx);

         //       string idGaiolaConf = Sistran.Library.GetDataTables.RetornarIdTabela("GAIOLACONFERENCIA", cnx);
                string filial = CodigoBarrasVolume.Substring(26, 2);


                if (Sistran.Library.GetDataTables.RetornarDataTableWin("SELECT COUNT(*) FROM REPOSICAOROGE WHERE CAST(CODIGOREGIAO AS INT)=" + int.Parse(filial), cnx).Rows[0][0].ToString() == "0")
                    throw new Exception("Não foi encontrada a Filial para este Etiqueta: " + CodigoBarrasVolume);

                string SQL = "INSERT INTO GAIOLA(IDGAIOLA, GAIOLA, FILIAL, IDUSUARIO, DATA, IMPRESSO, VolumeInicial, NumeroColetor, EMEI, Situacao) VALUES (" + idGaiola + ", " + idGaiola + ", '" + filial + "', " + idUsuario + ", GETDATE(), '0', '" + CodigoBarrasVolume + "', '" + NumeroColetor + "', '" + EMEI + "', 'ABERTO');  ";
                //SQL += " INSERT INTO GAIOLACONFERENCIA (IDGAIOLACONFERENCIA, IDGAIOLA,CODIGODEBARRAS,ROTEIRO, PERTENCEAFILIAL,IDUSUARIO,DATA) values";
                //SQL += " ("+idGaiolaConf+", "+idGaiola+",'"+CodigoBarrasVolume+"','', 'SIM',"+idUsuario+",GETDATE()) ";
                SQL+= " INSERT INTO GaiolaHistorico VALUES (" + int.Parse(idGaiola) + ", NULL, GETDATE(), 'CRIOU A BANDEIRA', " + idUsuario + ") ";
                

                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(SQL, cnx);
                return idGaiola;
            }
            catch (Exception EX)
            {
                return "Err^" + EX.Message;
            }
        }

        [WebMethod]
        public DataTable RetornarUnidadeDeVenda()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            return Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT CodigoDeBarras, Status FROM ReposicaoRogeEan", cnx);
        }

        [WebMethod]
        public DataTable RetornarUnidadeDeVendaClassificado()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            return Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT CodigoDeBarras, Status FROM ReposicaoRogeEan where status in('I','D')", cnx);
        }


        [WebMethod]
        public String  EnviarConferenciaAprovador(NotaFiscal Nota)
        {
            string sql = "";
            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                List<Vol> v = Nota.Volumes;
                List<It> i = Nota.Itens;

                sql = "Update ReposicaoRoge set UsuarioEnvioAuditoria='" + Nota.Usuario + "', DataEnvioAuditoria=GetDate(),  Status='ENVIADO AUDITORIA', FIM=GETDATE(), INICIO=DataColetor where Chave='" + Nota.Chave + "' ; ";

                for (int ivol = 0; ivol < v.Count; ivol++)
                    sql += " Update reposicaorogeVolume set DataConferido=getDate(), CONFERIDO='SIM' where IdReposicaoRogeVolume= " + v[ivol].IdVolume + "; ";
         

                for (int ivol = 0; ivol < i.Count; ivol++)
                {
                    if (i[ivol].PertenceAnota == "NAO" || i[ivol].PertenceAnota == "0")                    
                    {
                        string id = Sistran.Library.GetDataTables.RetornarIdTabela("ReposicaoRogeItem", cnx);
                        sql += " insert into ReposicaoRogeItem (IdReposicaoRogeItem, IdReposicaoRoge, DataDaInclusao, DataConferido,QuantidadeLido, PerteceANota, CodigoBarrasLido)";
                        sql += " values ('"+id+"', "+ Nota.IdNotaFiscal +", getdate(), getDate(),"+int.Parse(i[ivol].Quantidade)+", 'NAO', '"+ i[ivol].CodigoDeBarras +"'); ";
                    }
                    else
                    {
                        sql += "update reposicaorogeItem set PerteceANota='SIM', CodigoBarrasLido='" + i[ivol].CodigoDeBarras + "', QuantidadeLido="+ int.Parse(i[ivol].Quantidade) + ", DataConferido=getDate() where IdReposicaoRogeItem="+ i[ivol].IdItem + "; ";
                    }
                }

                Sistran.Library.GetDataTables.ExecutarSemRetornoWin(sql.ToUpper(), cnx);
                return "1^ENVIADO PARA O AUDITOR COM SUCESSO.";

            }
            catch (Exception ex)
            {
                return "0^ENVIAR AO AUDITOR FALHOU. MOTIVO: " + ex.Message + "sql: " + sql;

            }
        }


        [WebMethod]
        public String EnviarConferenciaAprovador_Cega(NotaFiscal Nota, List<ConferenciaCega> confCega )
        {
            string sql = "";
            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                List<Vol> v = Nota.Volumes;
                List<It> i = Nota.Itens;

                sql = "Update ReposicaoRoge set UsuarioEnvioAuditoria='" + Nota.Usuario + "', DataEnvioAuditoria=GetDate(),  Status='ENVIADO AUDITORIA', FIM=GETDATE(), INICIO=DataColetor where Chave='" + Nota.Chave + "' ; ";

                for (int ivol = 0; ivol < v.Count; ivol++)
                    sql += " Update reposicaorogeVolume set DataConferido=getDate(), CONFERIDO='SIM' where IdReposicaoRogeVolume= " + v[ivol].IdVolume + "; ";


                for (int ivol = 0; ivol < i.Count; ivol++)
                {
                    if (i[ivol].PertenceAnota == "NAO" || i[ivol].PertenceAnota == "0")
                    {
                        string id = Sistran.Library.GetDataTables.RetornarIdTabela("ReposicaoRogeItem", cnx);
                        sql += " insert into ReposicaoRogeItem (IdReposicaoRogeItem, IdReposicaoRoge, DataDaInclusao, DataConferido,QuantidadeLido, PerteceANota, CodigoBarrasLido)";
                        sql += " values ('" + id + "', " + Nota.IdNotaFiscal + ", getdate(), getDate()," + int.Parse(i[ivol].Quantidade) + ", 'NAO', '" + i[ivol].CodigoDeBarras + "'); ";

                    }
                    else
                    {
                        sql += "update reposicaorogeItem set PerteceANota='SIM', CodigoBarrasLido='" + i[ivol].CodigoDeBarras + "', QuantidadeLido=" + int.Parse(i[ivol].Quantidade) + ", DataConferido=getDate() where IdReposicaoRogeItem=" + i[ivol].IdItem + "; ";
                    }
                }


                for (int ix = 0; ix < confCega.Count; ix++)
                {
                    sql += " INSERT INTO reposicaorogeConferenciaCega (CodigoRoge, IdConferenciaItem, CodigoDeBarrasLido, Quantidade, PertenceANota) ";
                    sql += " VALUES (" + confCega[ix].CodigoRoge + ", " + confCega[ix].IdConferenciaItem + ", '" + confCega[ix].CodigoDeBarrasLido + "', " + confCega[ix].Quantidade + ", '" + confCega[ix].PertenceANota + "') ; ";
                }

                sql += " UPDATE REPOSICAOROGE SET VALOR = (SELECT  SUM(ISNULL(VALOR,0) * (QUANTIDADENOTA-ISNULL(QUANTIDADELIDO, 0))) FROM REPOSICAOROGEITEM RI WHERE IDREPOSICAOROGE =(SELECT IDREPOSICAOROGE FROM REPOSICAOROGE WHERE CHAVE='" + Nota.Chave + "' ) AND ISNULL(PERTECEANOTA, 'SIM') <> 'NAO' ) WHERE CHAVE='"+Nota.Chave+"';";

                Sistran.Library.GetDataTables.ExecutarSemRetornoWin(sql.ToUpper(), cnx);
                return "1^ENVIADO PARA O AUDITOR COM SUCESSO.";

            }
            catch (Exception ex)
            {
                return "0^ENVIAR AO AUDITOR FALHOU. MOTIVO: " + ex.Message + "sql: " + sql;

            }
        }
    }

    public class Vol
    {
        public string IdVolume {get; set;}        
        public string CodigoDeBarras { get; set; }
    }

    public class It
    {
        public string IdItem { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Quantidade { get; set; }
        public string CodigoRoge { get; set; }
        public string PertenceAnota { get; set; }        
    }

    public class NotaFiscal
    {
        public string IdNotaFiscal { get; set; }

        public string Chave { get; set; }
        public string Usuario { get; set; }

        public List<Vol> Volumes {get; set;}
        public List<It> Itens { get; set; }

    }

    public class ConferenciaCega
    {

        public string CodigoRoge { get; set; }
        public string IdConferenciaItem { get; set; }
        public string CodigoDeBarrasLido { get; set; }
        public string Quantidade { get; set; }
        public string PertenceANota { get; set; }

}
}