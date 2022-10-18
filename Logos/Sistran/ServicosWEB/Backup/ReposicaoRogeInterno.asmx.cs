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
        public string ReceberVolumes(List<vol> CodigoDeBarras, string IdGaiola, string idUsuario)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                string SQL = "";

                for (int i = 0; i < CodigoDeBarras.Count; i++)
                {
                    string idGaiolaConf = Sistran.Library.GetDataTables.RetornarIdTabela("GAIOLACONFERENCIA", cnx);

                    SQL += " INSERT INTO GAIOLACONFERENCIA (IDGAIOLACONFERENCIA, IDGAIOLA,CODIGODEBARRAS,ROTEIRO, PERTENCEAFILIAL,IDUSUARIO,DATA) values";
                    SQL += " (" + idGaiolaConf + ", " + IdGaiola + ",'" + CodigoDeBarras[i].CB.ToString() + "','', '" + CodigoDeBarras[i].Pertence.ToString() + "'," + idUsuario + ",GETDATE()); ";
                }

                if (CodigoDeBarras.Count == 0)
                    throw new Exception("Nenhum dado recebido");

                SQL += "UPDATE GAIOLA SET DataFechamento=GetDate() where IDGAIOLA=" + IdGaiola;
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(SQL, cnx);
                return "OK";

            }
            catch (Exception ex)
            {
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
        public string CriarGaiola(string CodigoBarrasVolume, string idUsuario)
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

                string SQL = "INSERT INTO GAIOLA(IDGAIOLA, GAIOLA, FILIAL, IDUSUARIO, DATA, IMPRESSO, VolumeInicial) VALUES (" + idGaiola + ", " + idGaiola + ", '" + filial + "', " + idUsuario + ", GETDATE(), '0', '"+CodigoBarrasVolume+"');  ";
                //SQL += " INSERT INTO GAIOLACONFERENCIA (IDGAIOLACONFERENCIA, IDGAIOLA,CODIGODEBARRAS,ROTEIRO, PERTENCEAFILIAL,IDUSUARIO,DATA) values";
                //SQL += " ("+idGaiolaConf+", "+idGaiola+",'"+CodigoBarrasVolume+"','', 'SIM',"+idUsuario+",GETDATE()) ";
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