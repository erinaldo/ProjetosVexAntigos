using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Configuration;
using Sistecno;

namespace ServicosWEB
{
    /// <summary>
    /// Summary description for wsExecutarComado
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsExecutarComado : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable ExecSql(string Usuario, string Senha, string Comando)
        {
            try
            {
                if (Usuario == "SISTECNO" && Senha == "@ONCETSIS12122014")
                {
                    string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                    return Sistran.Library.GetDataTables.RetornarDataTableWS(Comando, cnx);
                }
                else
                    throw new Exception("Autenticação Falhou.");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [WebMethod]
        public void GravarNotasMagalu(string NumeroOriginal, List<string> CodigoDeBarrasLido, int IdUsuario)
        {
            string sqlAux = "";
            string idDoc = "";
            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                Sistran.Library.GetDataTables.ExecutarComandoSql(
                    "insert into LogAppMagaLu  (NumeroOrginal, IdDocumento, Resultado) Values ('" +
                    NumeroOriginal + "', null, 'Inicio')", cnx);

                string sql = "Select IdDocumento, IdFilialAtual from Documento where NumeroOriginal='" + NumeroOriginal +
                             "' and IdCliente=46101 ";
                DataTable dtnf = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dtnf.Rows.Count == 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql(
                        "insert into LogAppMagaLu  (NumeroOrginal, IdDocumento, Resultado) Values ('" + NumeroOriginal +
                        "', null, '## NFe não Encontrada') ",
                        cnx);
                    return;
                }

                idDoc = dtnf.Rows[0][0].ToString();

                sqlAux =
                    "Update DocumentoFilial set Situacao ='AGUARDANDO RECEBIMENTO', Data = GETDATE() WHERE  IDDocumento= " +
                    dtnf.Rows[0][0].ToString();


                sqlAux += "; insert into LogAppMagaLu  (NumeroOrginal, IdDocumento, Resultado) Values ('" +
                          NumeroOriginal + "', " + dtnf.Rows[0][0].ToString() +
                          ", 'Acertou a NFe Para AGUARDANDO RECEBIMENTO') ";


                string iddoc = Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoOcorrencia", cnx);

                sqlAux +=
                    "; insert into DocumentoOcorrencia (IDDocumentoOcorrencia, IDDocumento, IDFilial, IDUsuario, DataOcorrencia, DataLancamento, Descricao)  ";
                sqlAux +=
                    " values  (" + iddoc + ", "+idDoc+", "+ dtnf.Rows[0][1].ToString()+", "+IdUsuario+", getdate(), getdate(), 'Documento Conferido por: ' + (select Login from Usuario where IdUsuario=" +
                    IdUsuario + ") + '' )";


                sql =
                    "Select cte.IdDocumento from Documento cte Inner join DocumentoRelacionado dr on dr.IdDocumentoPai = cte.IdDocumento  where dr.IdDocumentoFilho = " +
                    dtnf.Rows[0][0].ToString() + " and cte.TipoDeDocumento='Conhecimento'  ";

                DataTable dtCtrc = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dtCtrc.Rows.Count == 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql(
                        "insert into LogAppMagaLu  (NumeroOrginal, IdDocumento, Resultado) Values ('" + NumeroOriginal +
                        "', null, '## CTe não Encontrada') ",
                        cnx);
                }

                sqlAux +=
                    "; Update DocumentoFilial set Situacao ='AGUARDANDO GERAR CTRC', Data = GETDATE() WHERE IDDocumento= " +
                    dtCtrc.Rows[0][0].ToString();

                sqlAux += "; insert into LogAppMagaLu  (NumeroOrginal, IdDocumento, Resultado) Values ('" + NumeroOriginal +
                          "', " + dtCtrc.Rows[0][0].ToString() + ", 'Setou o conhecimento: AGUARDANDO GERAR CTR' )  ";

                 

                try
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql(
                        "insert into Chave (chave, DataDeCadastro, Observacao) values ('" + NumeroOriginal +
                        "', getdate(), 'MagaLU') ",
                        cnx);
                }
                catch (Exception)
                {
                }


                for (int i = 0; i < CodigoDeBarrasLido.Count; i++)
                {
                    
                    sqlAux += "; insert into LogAppMagaLu  (NumeroOrginal, IdDocumento, Resultado, CodigoBarras) Values ('" + NumeroOriginal +
                                                                                                            "', " + idDoc + ", 'CB Lido', '" + CodigoDeBarrasLido[i]+ "' )  ";
                }



                if (sqlAux.Length > 0)
                    Sistran.Library.GetDataTables.RetornarDataTableWS(sqlAux  + " ; Select 1", cnx);

                Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br","Sucesso",   sqlAux, "mail.grupologos.com.br", "logos0902", "magalu");


            }

            catch
                (Exception e)
            {
                Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br","***Erro", e.Message + "  " + sqlAux, "mail.grupologos.com.br", "logos0902", "Erro. teste magalu");

            }

        }



        [WebMethod]
        [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\ol014\\Kabum.log")]
        public string GravarNotasKabum(string NumeroOriginal, List<string> CodigoDeBarrasLido, int IdUsuario)
        {
            string sqlAux = "";
            string idDoc = "";
            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                Sistran.Library.GetDataTables.ExecutarComandoSql(
                    "insert into LogAppKabum  (NumeroOrginal, IdDocumento, Resultado) Values ('" +
                    NumeroOriginal + "', null, 'Inicio')", cnx);

                string sql = "Select IdDocumento, IdFilialAtual from Documento where NumeroOriginal='" + NumeroOriginal + "' and IdCliente in (5732433,4031253,3394048) ";
                DataTable dtnf = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dtnf.Rows.Count == 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql(
                        "insert into LogAppKabum  (NumeroOrginal, IdDocumento, Resultado) Values ('" + NumeroOriginal +
                        "', null, '## NFe não Encontrada') ",
                        cnx);
                    return "err:";
                }

                idDoc = dtnf.Rows[0][0].ToString();

                sqlAux =  "Update DocumentoFilial set Situacao ='AGUARDANDO EMBARQUE', Data = GETDATE() WHERE  IDDocumento= " +   dtnf.Rows[0][0].ToString();
                sqlAux += "; Update Documento set EnviadoComprovei ='NAO' WHERE  IDDocumento= " + idDoc;

                sqlAux += "; insert into LogAppKabum  (NumeroOrginal, IdDocumento, Resultado) Values ('" +
                          NumeroOriginal + "', " + dtnf.Rows[0][0].ToString() +
                          ", 'Acertou a NFe Para AGUARDANDO EMBARQUE') ";


                string iddoc = Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoOcorrencia", cnx);

                sqlAux +=
                    "; insert into DocumentoOcorrencia (IDDocumentoOcorrencia, IDDocumento, IDFilial, IDUsuario, DataOcorrencia, DataLancamento, Descricao)  ";
                sqlAux +=
                    " values  (" + iddoc + ", " + idDoc + ", " + dtnf.Rows[0][1].ToString() + ", " + IdUsuario + ", getdate(), getdate(), 'Documento Conferido por: ' + (select Login from Usuario where IdUsuario=" +
                    IdUsuario + ") + '' )";


                sql =
                    "Select cte.IdDocumento from Documento cte Inner join DocumentoRelacionado dr on dr.IdDocumentoPai = cte.IdDocumento  where dr.IdDocumentoFilho = " +
                    dtnf.Rows[0][0].ToString() + " and cte.TipoDeDocumento='Conhecimento'  ";

                DataTable dtCtrc = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dtCtrc.Rows.Count == 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql(
                        "insert into LogAppMagaLu  (NumeroOrginal, IdDocumento, Resultado) Values ('" + NumeroOriginal +
                        "', null, '## CTe não Encontrada') ",
                        cnx);
                }

                sqlAux +=
                    "; Update DocumentoFilial set Situacao ='AGUARDANDO GERAR CTRC', Data = GETDATE() WHERE IDDocumento= " +
                    dtCtrc.Rows[0][0].ToString();

                sqlAux += "; insert into LogAppKabum  (NumeroOrginal, IdDocumento, Resultado) Values ('" + NumeroOriginal +
                          "', " + dtCtrc.Rows[0][0].ToString() + ", 'Setou o conhecimento: AGUARDANDO GERAR CTR' )  ";



                try
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql(
                        "insert into Chave (chave, DataDeCadastro, Observacao) values ('" + NumeroOriginal +
                        "', getdate(), 'Kabum') ",
                        cnx);
                }
                catch (Exception)
                {
                }


                for (int i = 0; i < CodigoDeBarrasLido.Count; i++)
                {
                    sqlAux += "; insert into LogAppKabum  (NumeroOrginal, IdDocumento, Resultado, CodigoBarras) Values ('" + NumeroOriginal +
                                                                                                            "', " + idDoc + ", 'CB Lido', '" + CodigoDeBarrasLido[i] + "' )  ";
                }



                if (sqlAux.Length > 0)
                    Sistran.Library.GetDataTables.RetornarDataTableWS(sqlAux + " ; Select 1", cnx);

                Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "Sucesso", sqlAux, "mail.grupologos.com.br", "logos0902", "magalu");

                return "ok";
            }

            catch
                (Exception e)
            {
                Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "***Erro", e.Message + "  " + sqlAux, "mail.grupologos.com.br", "logos0902", "Erro. teste magalu");
                return "err: " + e.Message;
            }
        }


        [WebMethod]
        public void GravarNotasDafiti(string NumeroOriginal, List<string> CodigoDeBarrasLido, int IdUsuario)
        {
            string sqlAux = "";
            string idDoc = "";
            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                Sistran.Library.GetDataTables.ExecutarComandoSql(
                    "insert into LogAppDafiti  (NumeroOrginal, IdDocumento, Resultado) Values ('" +
                    NumeroOriginal + "', null, 'Inicio')", cnx);

                string sql = "Select IdDocumento, IdFilialAtual from Documento where NumeroOriginal='" + NumeroOriginal +
                             "' and IdCliente=123586 ";
                DataTable dtnf = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dtnf.Rows.Count == 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql(
                        "insert into LogAppDafiti  (NumeroOrginal, IdDocumento, Resultado) Values ('" + NumeroOriginal +
                        "', null, '## NFe não Encontrada') ",
                        cnx);
                    return;
                }

                idDoc = dtnf.Rows[0][0].ToString();

                sqlAux =
                    "Update DocumentoFilial set Situacao ='AGUARDANDO RECEBIMENTO', Data = GETDATE() WHERE  IDDocumento= " +
                    dtnf.Rows[0][0].ToString();


                sqlAux += "; insert into LogAppDafiti  (NumeroOrginal, IdDocumento, Resultado) Values ('" +
                          NumeroOriginal + "', " + dtnf.Rows[0][0].ToString() +
                          ", 'Acertou a NFe Para AGUARDANDO RECEBIMENTO') ";


                string iddoc = Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoOcorrencia", cnx);

                sqlAux +=
                    "; insert into DocumentoOcorrencia (IDDocumentoOcorrencia, IDDocumento, IDFilial, IDUsuario, DataOcorrencia, DataLancamento, Descricao)  ";
                sqlAux +=
                    " values  (" + iddoc + ", " + idDoc + ", " + dtnf.Rows[0][1].ToString() + ", " + IdUsuario + ", getdate(), getdate(), 'Documento Conferido por: ' + (select Login from Usuario where IdUsuario=" +
                    IdUsuario + ") + '' )";


                sql =
                    "Select cte.IdDocumento from Documento cte Inner join DocumentoRelacionado dr on dr.IdDocumentoPai = cte.IdDocumento  where dr.IdDocumentoFilho = " +
                    dtnf.Rows[0][0].ToString() + " and cte.TipoDeDocumento='Conhecimento'  ";

                DataTable dtCtrc = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dtCtrc.Rows.Count == 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql(
                        "insert into LogAppDafiti  (NumeroOrginal, IdDocumento, Resultado) Values ('" + NumeroOriginal +
                        "', null, '## CTe não Encontrada') ",
                        cnx);
                }

                sqlAux +=
                    "; Update DocumentoFilial set Situacao ='AGUARDANDO GERAR CTRC', Data = GETDATE() WHERE IDDocumento= " +
                    dtCtrc.Rows[0][0].ToString();

                sqlAux += "; insert into LogAppDafiti  (NumeroOrginal, IdDocumento, Resultado) Values ('" + NumeroOriginal +
                          "', " + dtCtrc.Rows[0][0].ToString() + ", 'Setou o conhecimento: AGUARDANDO GERAR CTR' )  ";



                try
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql(
                        "insert into Chave (chave, DataDeCadastro, Observacao) values ('" + NumeroOriginal +
                        "', getdate(), 'Dafiti') ",
                        cnx);
                }
                catch (Exception)
                {
                }


                for (int i = 0; i < CodigoDeBarrasLido.Count; i++)
                {

                    sqlAux += "; insert into LogAppDafiti  (NumeroOrginal, IdDocumento, Resultado, CodigoBarras) Values ('" + NumeroOriginal +
                                                                                                            "', " + idDoc + ", 'CB Lido', '" + CodigoDeBarrasLido[i] + "' )  ";
                }



                if (sqlAux.Length > 0)
                    Sistran.Library.GetDataTables.RetornarDataTableWS(sqlAux + " ; Select 1", cnx);

                Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "Sucesso", sqlAux, "mail.grupologos.com.br", "logos0902", "Dafiti");


            }
            catch
                (Exception e)
            {
                Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "***Erro", e.Message + "  " + sqlAux, "mail.grupologos.com.br", "logos0902", "Erro. dafiti");

            }

        }


        [WebMethod]
        public DataTable ExecSqlHR(string Usuario, string Senha, string Comando)
        {
            try
            {
                if (Usuario == "SISTECNO" && Senha == "@ONCETSIS12122014")
                {
                    string cnx = ConfigurationSettings.AppSettings["cnxHomeRefill"].ToString();
                    return Sistran.Library.GetDataTables.RetornarDataTableWS(Comando, cnx);
                }
                else
                    throw new Exception("Autenticação Falhou.");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
