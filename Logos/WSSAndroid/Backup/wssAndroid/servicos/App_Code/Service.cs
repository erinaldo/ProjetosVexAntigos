using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    string conn = "Data Source=192.168.10.5;Initial Catalog=stnnovo;User ID=sa;Password=@logos09022005$;";   
  

    [WebMethod]
    public verificar_documento[] Verificar_Documento(string NUM_DC, string NUM_PL, string NUM_DT)
    {
        string STRSQL = " ";
        STRSQL += " SELECT ";
        STRSQL += " DOC.IDDOCUMENTO, ";
        STRSQL += " DOC.NUMERO AS NUMERODOCUMENTO,  ";
        STRSQL += " DOC.IDFILIALATUAL, ";
        STRSQL += " DOC.VOLUMES, ";
        STRSQL += " DOC.PESOBRUTO, ";
        STRSQL += " VEI.PLACA,  ";
        STRSQL += " SUBSTRING(VEI.PLACA,5,8) AS NUMEROPLACA, ";
        STRSQL += " DT.NUMERO AS NUMERODT, ";
        STRSQL += " DBO.FREMOVE_ACENTOS(CADREM.RAZAOSOCIALNOME) AS REMETENTE, ";
        STRSQL += " DBO.FREMOVE_ACENTOS(CADDES.RAZAOSOCIALNOME) AS DESTINATARIO ";
        STRSQL += " FROM DOCUMENTO DOC ";
        STRSQL += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ";
        STRSQL += "  ON(DOC.IDDOCUMENTO = ROMDOC.IDDOCUMENTO) ";
        STRSQL += " INNER JOIN DTROMANEIO DTROM ";
        STRSQL += " ON(DTROM.IDROMANEIO = ROMDOC.IDROMANEIO) ";
        STRSQL += " INNER JOIN DT ";
        STRSQL += " ON(DT.IDDT = DTROM.IDDT) ";
        STRSQL += " INNER JOIN VEICULO VEI ";
        STRSQL += " ON(VEI.IDVEICULO = DT.IDPRIMEIROVEICULO) ";
        STRSQL += " LEFT JOIN CADASTRO CADREM ";
        STRSQL += " ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ";
        STRSQL += " LEFT JOIN CADASTRO CADDES ";
        STRSQL += " ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO) ";
        STRSQL += " WHERE DOC.NUMERO = '" + NUM_DC + "' ";
        STRSQL += " AND SUBSTRING(VEI.PLACA,5,8) = '" + NUM_PL + "' ";
        STRSQL += " AND DT.NUMERO = '" + NUM_DT + "' ";

        List<verificar_documento> Lista = new List<verificar_documento>();

        DataTable dt = ExecutaBD.RetornarDataTable(STRSQL, conn);

        foreach (DataRow item in dt.Rows)
        {
            verificar_documento itemResult = new verificar_documento();
            itemResult.DESTINATARIO = item["DESTINATARIO"].ToString();
            itemResult.NUMERODOCUMENTO = item["NUMERODOCUMENTO"].ToString();
            itemResult.IDFILIALATUAL = item["IDFILIALATUAL"].ToString();
            itemResult.VOLUMES = item["VOLUMES"].ToString();
            itemResult.PESOBRUTO = item["PESOBRUTO"].ToString();
            itemResult.PLACA = item["PLACA"].ToString();
            itemResult.NUMERODT = item["NUMERODT"].ToString();
            itemResult.REMETENTE = item["REMETENTE"].ToString();
            itemResult.DESTINATARIO = item["DESTINATARIO"].ToString();
            Lista.Add(itemResult);
        }
        return Lista.ToArray();
    }

    [WebMethod]
    public listar_documentos[] Listar_Documentos(string PLACA, string DOCTRANSP)
    {
        /**
         * Lista todos os documentos que estão apontados para a placa e dt que esta sendo enviado.
         * Retorna um Array dividido por linhas (Numericas iniciando do Zero) e Colunas (Com o nome do campo)
         * Ex:
         */

        string SQL = "";
        SQL += " SELECT ";
        SQL += " ISNULL(DOC.IDDOCUMENTOOCORRENCIA,'0') IDDOCUMENTOOCORRENCIA, ";
        SQL += " DOC.NUMERO AS NUMERODOCUMENTO, ";
        SQL += " DOC.IDDOCUMENTO, ";
        SQL += " DOC.IDFILIALATUAL, ";
        SQL += " COALESCE(DOC.VOLUMES,0) AS VOLUMES, ";
        SQL += " COALESCE(DOC.PESOBRUTO,0) AS PESOBRUTO, ";
        SQL += " VEI.PLACA, ";
        SQL += " SUBSTRING(VEI.PLACA,5,8) AS NUMEROPLACA, ";
        SQL += " DT.IDDT, ";
        SQL += " DBO.FREMOVE_ACENTOS(CADREM.RAZAOSOCIALNOME) AS REMETENTE, ";
        SQL += " DBO.FREMOVE_ACENTOS(CADDES.RAZAOSOCIALNOME) AS DESTINATARIO, ";
        SQL += " DBO.FREMOVE_ACENTOS(CADDES.ENDERECO) AS ENDERECO, ";
        SQL += " CADDES.NUMERO, ";
        SQL += " DBO.FREMOVE_ACENTOS(BAI.NOME) AS BAIRRO, ";
        SQL += " DBO.FREMOVE_ACENTOS(CID.NOME) AS CIDADE, ";
        SQL += " DBO.FREMOVE_ACENTOS(EST.NOME) AS ESTADO, ";
        SQL += " DBO.FREMOVE_ACENTOS(PAI.NOME) AS PAIS, ";
        SQL += " OCO.CODIGO AS OCORRENCIA, ";
        SQL += " COALESCE(RAS.TEMPO, 60) AS TEMPO, ";
        SQL += " RAS.ENVIAPOSICAOZERADA, ";
        SQL += " NULL AS CHAVEORIGEM ";
        SQL += " FROM DT ";
        SQL += " INNER JOIN DTROMANEIO DTROM ON(DTROM.IDDT = DT.IDDT) ";
        SQL += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ON (ROMDOC.IDROMANEIO = DTROM.IDROMANEIO) ";
        SQL += " INNER JOIN DOCUMENTO DOC ON (DOC.IDDOCUMENTO = ROMDOC.IDDOCUMENTO) ";
        SQL += " INNER JOIN VEICULO VEI ON(VEI.IDVEICULO = DT.IDPRIMEIROVEICULO) ";
        SQL += " LEFT JOIN CADASTRO CADREM  ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ";
        SQL += " LEFT JOIN CADASTRO CADDES ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO) ";
        SQL += " LEFT JOIN BAIRRO BAI  ON(BAI.IDBAIRRO = CADDES.IDBAIRRO) ";
        SQL += " LEFT JOIN CIDADE CID  ON(CID.IDCIDADE = CADDES.IDCIDADE) ";
        SQL += " LEFT JOIN ESTADO EST ON(EST.IDESTADO = CID.IDESTADO)        ";
        SQL += " LEFT JOIN PAIS PAI  ON(PAI.IDPAIS = EST.IDPAIS) ";
        SQL += " LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO ON (DOCOCO.IDDOCUMENTOOCORRENCIA=DOC.IDDOCUMENTOOCORRENCIA) ";
        SQL += " LEFT JOIN OCORRENCIA OCO ON (OCO.IDOCORRENCIA=DOCOCO.IDOCORRENCIA) ";
        SQL += " LEFT JOIN RASTREADOR RAS ON (RAS.IDRASTREADOR=DT.IDRASTREADOR) ";
        SQL += " WHERE SUBSTRING(VEI.PLACA,5,4) = '" + PLACA + "'";
        SQL += " AND DT.NUMERO ='" + DOCTRANSP + "'";
        SQL += " ORDER BY DOC.NUMERO ASC ";

        List<listar_documentos> Lista = new List<listar_documentos>();

        DataTable dt = ExecutaBD.RetornarDataTable(SQL, conn);

        foreach (DataRow item in dt.Rows)
        {
            listar_documentos itemResult = new listar_documentos();
            itemResult.IDDOCUMENTOOCORRENCIA = item["IdDocumentoOcorrencia"].ToString();
            itemResult.NUMERODOCUMENTO = item["NUMERODOCUMENTO"].ToString();
            itemResult.IDDOCUMENTO = item["IDDocumento"].ToString();
            itemResult.IDFILIALATUAL = item["IDFilialAtual"].ToString();
            itemResult.VOLUMES = item["Volumes"].ToString();
            itemResult.PESOBRUTO = item["PesoBruto"].ToString();
            itemResult.PLACA = item["Placa"].ToString();
            itemResult.PLACANUMERO = item["NumeroPlaca"].ToString();
            itemResult.IDDT = item["IDDT"].ToString();
            itemResult.REMETENTE = item["Remetente"].ToString();
            itemResult.DESTINATARIO = item["Destinatario"].ToString();
            itemResult.ENDERECO = item["Endereco"].ToString();
            itemResult.NUMERO = item["Numero"].ToString();
            

            itemResult.BAIRRO = item["Bairro"].ToString();
            itemResult.CIDADE = item["Cidade"].ToString();
            itemResult.ESTADO = item["Estado"].ToString();
            itemResult.PAIS = item["Pais"].ToString();
            itemResult.OCORRENCIA = item["Ocorrencia"].ToString();
            itemResult.TEMPO = item["Tempo"].ToString();
            itemResult.ENVIAPOSICAOZERADA = item["EnviaPosicaoZerada"].ToString();
            itemResult.CHAVEORIGEM = item["ChaveOrigem"].ToString();
            Lista.Add(itemResult);
        }

        return Lista.ToArray();

    }
    
    [WebMethod]
    public Ocorrencias[] Listar_All_Ocorrencias()
    {
        string SQL = "SELECT IDOCORRENCIA, CODIGO, NOME, RESPONSABILIDADE, FINALIZADOR FROM OCORRENCIA ORDER BY NOME, CODIGO";

        List<Ocorrencias> Lista = new List<Ocorrencias>();

        DataTable dt = ExecutaBD.RetornarDataTable(SQL, conn);

        foreach (DataRow item in dt.Rows)
        {
            Ocorrencias itemResult = new Ocorrencias();
            itemResult.IDOCORRENCIA = item["IDOCORRENCIA"].ToString();
            itemResult.CODIGO = item["CODIGO"].ToString();
            itemResult.FINALIZADOR = item["FINALIZADOR"].ToString();
            itemResult.NOME = item["NOME"].ToString();
            itemResult.RESPONSABILIDADE = item["RESPONSABILIDADE"].ToString();
            Lista.Add(itemResult);
        }

        return Lista.ToArray();

    }

    [WebMethod]
    public string[] gravarDocumentoOcorrencia(string idDocumento, string idOcorrencia, string descricaoOcorrencia, string IDFilial)
    {
        string[] m = new string[1];
        m[0] = "";
        try
        {
            string IdDocOco = RetornarIdTabela("DOCUMENTOOCORRENCIA", conn);
            string strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
            strsql += " IDDocumentoOcorrencia,";
            strsql += " IDDocumento,";
            strsql += " IDFilial,";
            strsql += " IDOcorrencia,";
            strsql += " DataOcorrencia,";
            strsql += " Descricao,";
            strsql += " Sistema";
            strsql += " ) VALUES (";
            strsql += IdDocOco + " ,";
            strsql += idDocumento + " ,";
            strsql += Convert.ToInt32(IDFilial) + " ,";
            strsql += int.Parse(idOcorrencia) + " ,";
            strsql += " convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "', 103) ,";
            strsql += " '" + descricaoOcorrencia.ToUpper().Trim() + "',";
            strsql += "'SIM'";
            strsql += " ) select '1' ";

            ExecutaBD.ExecutaComandoSql(strsql, conn);

            strsql = "SELECT FINALIZADOR FROM OCORRENCIA WHERE IDOCORRENCIA = " + int.Parse(idOcorrencia);
            string finalizadora = ExecutaBD.ExecutarSQLRetornarIDs(strsql, conn);

            strsql = "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " " + (finalizadora == "SIM" ? " , DATADECONCLUSAO= convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "', 103)" : "") + "  WHERE IDDocumento=" + idDocumento + " Select '1'";
            ExecutaBD.ExecutarSQLRetornarIDs(strsql, conn);

            m[0] = "TRUE";
            return m;
        }
        catch (Exception EX)
        {
            m[0] = "FALSE";
            throw EX;
        }
       
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

        public static void ExecutaComandoSql(string isql, string Conn)
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
            try
            {
                cd.ExecuteNonQuery();
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
        }
    }
        
    public class verificar_documento
    {
        public string IDDOCUMENTO  { get; set; }
        public string NUMERODOCUMENTO  { get; set; }
        public string IDFILIALATUAL  { get; set; }
        public string VOLUMES  { get; set; }
        public string PESOBRUTO  { get; set; }
        public string PLACA  { get; set; }
        public string NUMEROPLACA  { get; set; }
        public string NUMERODT  { get; set; }
        public string REMETENTE  { get; set; }
        public string DESTINATARIO  { get; set; }
    }

    public class listar_documentos
    {
        public string IDDOCUMENTOOCORRENCIA  { get; set; }
        public string NUMERODOCUMENTO  { get; set; }
        public string IDDOCUMENTO  { get; set; }
        public string IDFILIALATUAL  { get; set; }
        public string VOLUMES  { get; set; }
        public string PESOBRUTO  { get; set; }
        public string PLACANUMERO  { get; set; }
        public string PLACA  { get; set; }
        public string IDDT  { get; set; }
        public string REMETENTE  { get; set; }
        public string DESTINATARIO  { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }
        public string BAIRRO  { get; set; }
        public string CIDADE  { get; set; }
        public string ESTADO  { get; set; }
        public string PAIS  { get; set; }
        public string OCORRENCIA  { get; set; }
        public string TEMPO  { get; set; }
        public string ENVIAPOSICAOZERADA  { get; set; }
        public string CHAVEORIGEM  { get; set; }
    }

    public class Ocorrencias
    {
        public string IDOCORRENCIA { get; set; }
        public string CODIGO { get; set; }
        public string NOME { get; set; }
        public string RESPONSABILIDADE { get; set; }
        public string FINALIZADOR { get; set; }
    }
  
}