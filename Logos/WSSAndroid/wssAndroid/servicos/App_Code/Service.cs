using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public string RetornarStringConexao(string cd_cliente, string senhaSeguranca)
    {
        try
        {
            if (DeixaLogar(senhaSeguranca))
            {
                DataSet dConections = new DataSet();
                dConections.ReadXml(Server.MapPath("Conexoes") + "\\conexoes.xml");

                DataRow[] linha = dConections.Tables[0].Select("cd_cliente='" + cd_cliente + "'", "");

                if (linha[0] == null)
                    throw new Exception("Conexao nao Encontrada....");

                return linha[0]["conexao"].ToString();
            }
            else
                return "";
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    private bool DeixaLogar(string senhaSeguranca)
    {
        try
        {
            DataSet dConections = new DataSet();
            dConections.ReadXml(Server.MapPath("Seguranca") + "\\seguranca.xml");

            DataRow[] linha = dConections.Tables[0].Select("cd_senha='" + senhaSeguranca + "" + "'", ""); //sistecno

            if (linha[0] == null)
                throw new Exception("Nao Autenticado....");
            else
                return true;

        }
        catch (Exception ex)
        {
            return false;
            throw ex;
        }
    }

    [WebMethod]
    public bool estaAtivo()
    {
        return true;
    }

    [WebMethod]
    public void GravarAparelho(string Chave, string Nome, string Tempo, string EnviaPosicaoZerada, string NumeroChip, string EnviaFoto, string cd_cliente, string senhaSeguranca)
    {

        DataTable dt = ExecutaBD.RetornarDataTable("SELECT * FROM RASTREADOR WHERE CHAVE='" + Chave.ToUpper().Trim().Replace(" ", "") + "'", RetornarStringConexao(cd_cliente, senhaSeguranca));

        string ssql = "";
        if (dt.Rows.Count == 0)
        {
            string id = RetornarIdTabela("Rastreador", RetornarStringConexao(cd_cliente, senhaSeguranca));
            ssql += "INSERT INTO RASTREADOR (IdRastreador,Chave,Nome,Tempo,EnviaPosicaoZerada,NumeroChip,EnviaFoto) VALUES (" + id + ",'" + Chave.ToUpper() + "','" + Nome + "'," + Tempo + ",'" + EnviaPosicaoZerada + "','" + NumeroChip + "','" + EnviaFoto + "')";
            ExecutaBD.ExecutaComandoSql(ssql, RetornarStringConexao(cd_cliente, senhaSeguranca));
        }

    }
     

    [WebMethod]
    public verificar_documento[] Verificar_Documento(string NUM_DC, string NUM_PL, string NUM_DT, string cd_cliente, string senhaSeguranca)
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

        DataTable dt = ExecutaBD.RetornarDataTable(STRSQL, RetornarStringConexao(cd_cliente, senhaSeguranca));

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
    public aparelho[] Verificar_Aparelho(string chave, string senhaSeguranca, string cd_cliente)
    {
        string STRSQL = "SELECT * FROM RASTREADOR WHERE CHAVE='" + chave + "'";
        List<aparelho> Lista = new List<aparelho>();

        DataTable dt = ExecutaBD.RetornarDataTable(STRSQL, RetornarStringConexao(cd_cliente, senhaSeguranca));

        foreach (DataRow item in dt.Rows)
        {
            aparelho itemResult = new aparelho();
            itemResult.IdRastreador = item["IdRastreador"].ToString();
            itemResult.Chave = item["Chave"].ToString();
            itemResult.Nome = item["Nome"].ToString();
            itemResult.Tempo = item["Tempo"].ToString();
            itemResult.NumeroFone = item["NumeroChip"].ToString();
            itemResult.EnviaFoto = item["EnviaFoto"].ToString();
            itemResult.EnviaPosicaoZerada = item["EnviaPosicaoZerada"].ToString();
            Lista.Add(itemResult);
        }
        return Lista.ToArray();
    }

    [WebMethod]
    public void Limpar(string PLACA, string DOCTRANSP, string cd_cliente, string senhaSeguranca)
    {

        string strsql = "";

        strsql += "  UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA =NULL, DATADECONCLUSAO=NULL ";
        strsql += "  WHERE IDDOCUMENTO IN(";
        strsql += "  SELECT DOC.IDDOCUMENTO FROM DT INNER JOIN DTROMANEIO DTROM ON(DTROM.IDDT = DT.IDDT) ";
        strsql += "  INNER JOIN ROMANEIODOCUMENTO ROMDOC ON (ROMDOC.IDROMANEIO = DTROM.IDROMANEIO) ";
        strsql += "  INNER JOIN DOCUMENTO DOC ON (DOC.IDDOCUMENTO = ROMDOC.IDDOCUMENTO) ";
        strsql += "  INNER JOIN VEICULO VEI ON(VEI.IDVEICULO = DT.IDPRIMEIROVEICULO) ";
        strsql += "  LEFT JOIN CADASTRO CADREM ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ";
        strsql += "  LEFT JOIN CADASTRO CADDES ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)";
        strsql += "  LEFT JOIN BAIRRO BAI ON(BAI.IDBAIRRO = CADDES.IDBAIRRO) ";
        strsql += "  LEFT JOIN CIDADE CID ON(CID.IDCIDADE = CADDES.IDCIDADE) ";
        strsql += "  LEFT JOIN ESTADO EST ON(EST.IDESTADO = CID.IDESTADO) ";
        strsql += "  LEFT JOIN PAIS PAI ON(PAI.IDPAIS = EST.IDPAIS) ";
        strsql += "  LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO ON (DOCOCO.IDDOCUMENTOOCORRENCIA=DOC.IDDOCUMENTOOCORRENCIA) ";
        strsql += "  LEFT JOIN OCORRENCIA OCO ON (OCO.IDOCORRENCIA=DOCOCO.IDOCORRENCIA) ";
        strsql += "  LEFT JOIN RASTREADOR RAS ON (RAS.IDRASTREADOR=DT.IDRASTREADOR) ";
        strsql += "  WHERE SUBSTRING(VEI.PLACA,5,4) = '" + PLACA + "' AND TIPODEDOCUMENTO='NOTA FISCAL' AND DOC.ATIVO='SIM' AND DT.NUMERO ='" + DOCTRANSP + "'  ";
        strsql += "  )";


        strsql += "  DELETE FROM DOCUMENTOOCORRENCIA WHERE IDDOCUMENTO IN( ";
        strsql += "  SELECT DOC.IDDOCUMENTO FROM DT INNER JOIN DTROMANEIO DTROM ON(DTROM.IDDT = DT.IDDT)  ";
        strsql += "  INNER JOIN ROMANEIODOCUMENTO ROMDOC ON (ROMDOC.IDROMANEIO = DTROM.IDROMANEIO)  ";
        strsql += "  INNER JOIN DOCUMENTO DOC ON (DOC.IDDOCUMENTO = ROMDOC.IDDOCUMENTO)  ";
        strsql += "  INNER JOIN VEICULO VEI ON(VEI.IDVEICULO = DT.IDPRIMEIROVEICULO)  ";
        strsql += "  LEFT JOIN CADASTRO CADREM ON(CADREM.IDCADASTRO = DOC.IDREMETENTE)  ";
        strsql += "  LEFT JOIN CADASTRO CADDES ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)  ";
        strsql += "  LEFT JOIN BAIRRO BAI ON(BAI.IDBAIRRO = CADDES.IDBAIRRO)  ";
        strsql += "  LEFT JOIN CIDADE CID ON(CID.IDCIDADE = CADDES.IDCIDADE)  ";
        strsql += "  LEFT JOIN ESTADO EST ON(EST.IDESTADO = CID.IDESTADO)  ";
        strsql += "  LEFT JOIN PAIS PAI ON(PAI.IDPAIS = EST.IDPAIS)  ";
        strsql += "  LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO ON (DOCOCO.IDDOCUMENTOOCORRENCIA=DOC.IDDOCUMENTOOCORRENCIA)  ";
        strsql += "  LEFT JOIN OCORRENCIA OCO ON (OCO.IDOCORRENCIA=DOCOCO.IDOCORRENCIA)  ";
        strsql += "  LEFT JOIN RASTREADOR RAS ON (RAS.IDRASTREADOR=DT.IDRASTREADOR)  ";
        strsql += "  WHERE SUBSTRING(VEI.PLACA,5,4) = '" + PLACA + "' AND TIPODEDOCUMENTO='NOTA FISCAL' AND DOC.ATIVO='SIM' AND DT.NUMERO ='" + DOCTRANSP + "'  ";
        strsql += "  )";


        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        DbConnection cn = factory.CreateConnection();
        DbCommand cd = factory.CreateCommand();

        cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
        cd.Connection = cn;
        cn.Open();
        DbTransaction oTrans;
        oTrans = cn.BeginTransaction();
        try
        {
            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;
            cd.ExecuteNonQuery();

            oTrans.Commit();
            cn.Close();
            cn.Dispose();
        }
        catch (Exception EX)
        {
            oTrans.Rollback();
            cn.Close();
            cn.Dispose();
            enviarEmailGrupoLogos("ERRO NO WSANDROID", EX.Message + "-" + EX.StackTrace + "-" + EX.InnerException + "- DATA: " + DateTime.Now);
            throw EX;
        }

    }

    [WebMethod]
    public listar_documentos[] Listar_Documentos(string PLACA, string DOCTRANSP, string EQUIPAMENTO, string cd_cliente, string senhaSeguranca)
    {
        /**
         * Lista todos os documentos que estão apontados para a placa e dt que esta sendo enviado.
         * Retorna um Array dividido por linhas (Numericas iniciando do Zero) e Colunas (Com o nome do campo)
         * Ex:
         * 
         * 
         */


        try
        {
            GravarLog("Listar_Documentos", "Iniciou", "Auditoria");

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
            SQL += " OCO.CODIGO AS OCORRENCIA, OCO.IDOCORRENCIA, ";
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
            SQL += " WHERE SUBSTRING(VEI.PLACA,5,4) = '" + PLACA + "'  AND DOC.ATIVO='SIM'";
            SQL += " AND TIPODEDOCUMENTO in ('NOTA FISCAL' , 'ORDEM DE SERVICO', 'GUIA DE REMESSA', 'ORDEM DE COLETA')";
            SQL += " AND DT.NUMERO ='" + DOCTRANSP + "'";
            SQL += " ORDER BY DOC.NUMERO ASC ";

            enviarEmailGrupoLogos("Select Executado-Android", "IDEquipamento: " + EQUIPAMENTO + " <br>Conexao: " + RetornarStringConexao(cd_cliente, senhaSeguranca) + " <br>DT: " + DOCTRANSP + "  <br>PLACA: " + PLACA + "   <br><br><br>" + SQL);

            List<listar_documentos> Lista = new List<listar_documentos>();

            DataTable dt = ExecutaBD.RetornarDataTable(SQL, RetornarStringConexao(cd_cliente, senhaSeguranca));
            //dt.WriteXml(@"c:\teste\Documentos.xml");

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
                itemResult.REMETENTE = item["Remetente"].ToString().Replace("'","");
                itemResult.DESTINATARIO = item["Destinatario"].ToString().Replace("'", "");
                itemResult.ENDERECO = item["Endereco"].ToString().Replace("'", "");
                itemResult.NUMERO = item["Numero"].ToString().Replace("'", "");
                itemResult.BAIRRO = item["Bairro"].ToString().Replace("'", "");
                itemResult.CIDADE = item["Cidade"].ToString().Replace("'", "");
                itemResult.ESTADO = item["Estado"].ToString().Replace("'", "");
                itemResult.PAIS = item["Pais"].ToString();
                itemResult.OCORRENCIA = item["Ocorrencia"].ToString();
                itemResult.TEMPO = item["Tempo"].ToString();
                itemResult.ENVIAPOSICAOZERADA = item["EnviaPosicaoZerada"].ToString();
                itemResult.CHAVEORIGEM = item["ChaveOrigem"].ToString();
                Lista.Add(itemResult);
            }

            GravarLog("Listar_Documentos", "Finalizou", "Auditoria");

            SQL = "UPDATE RASTREADOR SET UltimaSincronizacao=GETDATE(), UltimaDT=null,UltimaPlaca=null, UtltimoEnvioDeDados=GETDATE()  WHERE UltimaDT='" + DOCTRANSP + "'";
            ExecutaBD.ExecutaComandoSql(SQL, RetornarStringConexao(cd_cliente, senhaSeguranca));


            SQL = "UPDATE RASTREADOR SET UltimaSincronizacao=GETDATE(), UltimaDT=" + DOCTRANSP + ",UltimaPlaca='" + PLACA + "', UtltimoEnvioDeDados=GETDATE()  WHERE CHAVE='" + EQUIPAMENTO + "'";
            ExecutaBD.ExecutaComandoSql(SQL, RetornarStringConexao(cd_cliente, senhaSeguranca));


            GrarvarTempoSincronizacao("", DOCTRANSP, DateTime.Now.AddSeconds(-3).ToString(), DateTime.Now.ToString(), cd_cliente, senhaSeguranca);
            return Lista.ToArray();
        }
        catch (Exception EX)
        {
            enviarEmailGrupoLogos("ERRO NO WSANDROID", EX.Message + "-" + EX.StackTrace + "-" + EX.InnerException);
            throw new Exception("Não foi possivel Sincronizar." + EX.Message);
        }
    }

    [WebMethod]
    public Ocorrencias[] Listar_All_Ocorrencias(string cd_cliente, string senhaSeguranca)
    {
        GravarLog("Listar_All_Ocorrencias", "Iniciou", "Auditoria");
        try
        {
            string SQL = "SELECT IDOCORRENCIA, CODIGO, NOME, RESPONSABILIDADE, FINALIZADOR FROM OCORRENCIA ORDER BY NOME, CODIGO";

            List<Ocorrencias> Lista = new List<Ocorrencias>();

            DataTable dt = ExecutaBD.RetornarDataTable(SQL, RetornarStringConexao(cd_cliente, senhaSeguranca));

            //dt.WriteXml(@"c:\teste\Ocorrencias.xml");

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

            GravarLog("Listar_All_Ocorrencias", "Finalizou", "Auditoria");
            return Lista.ToArray();
        }
        catch (Exception EX)
        {
            enviarEmailGrupoLogos("ERRO NO WSANDROID", EX.Message + "-" + EX.StackTrace + "-" + EX.InnerException);
            throw new Exception("Não foi possivel Sincronizar." + EX.Message);
        }
    }

    [WebMethod]
    public string[] gravarDocumentoOcorrencia(
        string idDocumento,
        string idOcorrencia,
        string descricaoOcorrencia,
        string IDFilial,
        byte[] image,
        string longitude,
        string latitude,
        string idDt,
        string DataHoraPosicao,
        string dataHoraOcorencia,
        string cd_cliente,
        string senhaSeguranca)
    {
        string[] m = new string[1];
        m[0] = "";
        string IdDocOco = RetornarIdTabela("DOCUMENTOOCORRENCIA", RetornarStringConexao(cd_cliente, senhaSeguranca));
        string id = RetornarIdTabela("DocumentoOcorrenciaArquivo", RetornarStringConexao(cd_cliente, senhaSeguranca));

        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        DbConnection cn = factory.CreateConnection();
        DbCommand cd = factory.CreateCommand();

        cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
        cd.Connection = cn;
        cn.Open();
        DbTransaction oTrans;
        oTrans = cn.BeginTransaction();
        try
        {

            cd.CommandText = "SELECT ISNULL(IDDOCUMENTOOCORRENCIA, 0) FROM DOCUMENTO WHERE IDDOCUMENTO = " + idDocumento;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;
            string x = cd.ExecuteScalar().ToString();

            if (int.Parse(x) > 0)
            {
                m[0] = "TRUE";
                oTrans.Commit();
                cn.Close();
                cn.Dispose();
                return m;
            }

            string strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
            strsql += " IDDocumentoOcorrencia,";
            strsql += " IDDocumento,";
            strsql += " IDFilial,";
            strsql += " IDOcorrencia,";
            strsql += " DataOcorrencia,";
            strsql += " Descricao,";
            strsql += " Sistema, ";
            strsql += " Latitude, ";
            strsql += " Longitude ";
            strsql += " ) VALUES (";
            strsql += IdDocOco + " ,";
            strsql += idDocumento + " ,";
            strsql += Convert.ToInt32(IDFilial) + " ,";
            strsql += int.Parse(idOcorrencia) + " ,";
            strsql += " getDate() ,";
            //strsql += " convert(datetime,'" + DateTime.Parse(dataHoraOcorencia).ToString("dd/MM/yyyy HH:mm:ss") + "', 103) ,";
            strsql += " '" + descricaoOcorrencia.ToUpper().Trim() + "',";
            strsql += "'SIM', ";
            strsql += latitude + ", ";
            strsql += longitude;
            strsql += " ) select '1' ";

            //ExecutaBD.ExecutaComandoSql(strsql, RetornarStringConexao(cd_cliente));
            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;
            cd.ExecuteScalar();

            strsql = "SELECT FINALIZADOR FROM OCORRENCIA WHERE IDOCORRENCIA = " + int.Parse(idOcorrencia);
            //string finalizadora = ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente));

            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;
            string finalizadora = cd.ExecuteScalar().ToString();

            strsql = "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " " + (finalizadora == "SIM" ? " , DATADECONCLUSAO= convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "', 103)" : "") + "  WHERE IDDocumento=" + idDocumento + " Select '1'";
            //ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente));
            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;
            cd.ExecuteNonQuery();

            oTrans.Commit();
            cn.Close();
            cn.Dispose();

            if (image != null)
            {
                strsql = "INSERT INTO DocumentoOcorrenciaArquivo (IDDocumentoOcorrenciaArquivo, IDDocumentoOcorrencia, Arquivo) VALUES (" + id + ", " + IdDocOco + ", @IMAGEM)";
                SqlCommand command = new SqlCommand();
                SqlConnection vv = new SqlConnection(RetornarStringConexao(cd_cliente, senhaSeguranca));
                command.CommandText = strsql.ToString();
                command.CommandType = CommandType.Text;
                command.Connection = vv;
                command.Parameters.Add(new SqlParameter("@IMAGEM", image));
                vv.Open();
                command.ExecuteNonQuery();
                vv.Close();
                vv.Dispose();
            }

            m[0] = "TRUE";
            gravarPosicaoOcorrenciaInterno(idDt, latitude, longitude, DataHoraPosicao, cd_cliente, senhaSeguranca, "SIM");

            enviarEmailGrupoLogos("Gravando Entrega - Android", "IDDocumento: " + idDocumento + "  IDDocumentoOcorrencia: " + IdDocOco + "   cd_cliente" + cd_cliente);

            GrarvarUltimoEnvioDeDadosAndroid(IdDocOco, cd_cliente, senhaSeguranca);
            return m;
        }
        catch (Exception EX)
        {
            oTrans.Rollback();
            cn.Close();
            cn.Dispose();

            m[0] = "FALSE";
            enviarEmailGrupoLogos("ERRO NO WSANDROID", EX.Message + "-" + EX.StackTrace + "-" + EX.InnerException + "- DATA: " + descricaoOcorrencia);
            throw EX;
        }

    }

    [WebMethod]
    public string RetornarQtdNotasNoDt(string placa, string ndt, string cd_cliente, string senhaSeguranca)
    {
        string sql = " SELECT  count(distinct ROMDOC.iddocumento) qtd FROM DT with (nolock) INNER JOIN DTROMANEIO DTROM ON(DTROM.IDDT = DT.IDDT)  INNER JOIN ROMANEIODOCUMENTO ROMDOC ON (ROMDOC.IDROMANEIO = DTROM.IDROMANEIO)  INNER JOIN VEICULO VEI ON(VEI.IDVEICULO = DT.IDPRIMEIROVEICULO)  WHERE SUBSTRING(VEI.PLACA,5,4) = '"+placa+"' AND DT.NUMERO ='"+ndt+"' ";
        return ExecutaBD.ExecutarSQLRetornarIDs(sql, RetornarStringConexao(cd_cliente, senhaSeguranca));        
    }

    [WebMethod]
    public void GrarvarTempoSincronizacao(string chave, string dt, string DataInicial, string DataFinal, string cd_cliente, string senhaSeguranca)
    {
        string sql = "";
        if(chave=="")
            sql = "UPDATE RASTREADOR SET InicioSincronizacao='" + DateTime.Parse(DataInicial).ToString("yyyy-MM-dd HH:mm:ss.fff") + "', FINALSincronizacao='" + DateTime.Parse(DataFinal).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' WHERE ULTIMADT = '" + dt + "'";
        else
        sql = "UPDATE RASTREADOR SET InicioSincronizacao='" + DateTime.Parse(DataInicial).ToString("yyyy-MM-dd HH:mm:ss.fff") + "', FINALSincronizacao='" + DateTime.Parse(DataFinal).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' WHERE CHAVE = '" + chave + "'";

        ExecutaBD.ExecutaComandoSql(sql, RetornarStringConexao(cd_cliente, senhaSeguranca));
        ExecutaBD.ExecutaComandoSql("UPDATE RASTREADOR SET  TempoSincronizacao=DATEDIFF(ss, InicioSincronizacao, FinalSincronizacao) WHERE CHAVE = '" + chave + "'", RetornarStringConexao(cd_cliente, senhaSeguranca));  
        
    }

    [WebMethod]
    public void GrarvarUltimoEnvioDeDados(string chave, string cd_cliente, string senhaSeguranca)
    {
        string sql = "UPDATE RASTREADOR SET UtltimoEnvioDeDados=GetDate() WHERE CHAVE = '" + chave + "'";
        ExecutaBD.ExecutaComandoSql(sql, RetornarStringConexao(cd_cliente, senhaSeguranca));    
    }

    [WebMethod]
    public void GrarvarUltimoEnvioDeDadosAndroid(string ndt, string cd_cliente, string senhaSeguranca)
    {
        string sql = "UPDATE RASTREADOR SET UtltimoEnvioDeDados=GetDate() WHERE ULTIMADT = '" + ndt + "'";
        ExecutaBD.ExecutaComandoSql(sql, RetornarStringConexao(cd_cliente, senhaSeguranca));
    }

    [WebMethod]
    public string[] gravarDocumentoOcorrenciaSemImagem(string idDocumento, string idOcorrencia, string descricaoOcorrencia, string IDFilial, string longitude, string latitude, string idDt, string DataHoraPosicao, string DataHoraOcorrencia, string cd_cliente, string senhaSeguranca)
    {
        GravarLog("gravarDocumentoOcorrenciaSemImagem", "Iniciou", "Auditoria");
        string IdDocOco = RetornarIdTabela("DOCUMENTOOCORRENCIA", RetornarStringConexao(cd_cliente, senhaSeguranca));

        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        DbConnection cn = factory.CreateConnection();
        DbCommand cd = factory.CreateCommand();

        cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
        cd.Connection = cn;
        cn.Open();
        DbTransaction oTrans;
        oTrans = cn.BeginTransaction();

        string[] m = new string[1];
        m[0] = "";
        try
        {

            cd.CommandText = "SELECT ISNULL(IDDOCUMENTOOCORRENCIA, 0) FROM DOCUMENTO WHERE IDDOCUMENTO = " + idDocumento;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;
            string x = cd.ExecuteScalar().ToString();

            if (int.Parse(x) > 0)
            {
                m[0] = "TRUE";
                oTrans.Commit();
                cn.Close();
                cn.Dispose();
                return m;
            }

            string strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
            strsql += " IDDocumentoOcorrencia,";
            strsql += " IDDocumento,";
            strsql += " IDFilial,";
            strsql += " IDOcorrencia,";
            strsql += " DataOcorrencia,";
            strsql += " Descricao,";
            strsql += " Sistema, ";
            strsql += " Latitude, ";
            strsql += " Longitude ";
            strsql += " ) VALUES (";
            strsql += IdDocOco + " ,";
            strsql += idDocumento + " ,";
            strsql += Convert.ToInt32(IDFilial) + " ,";
            strsql += int.Parse(idOcorrencia) + " ,";
//            strsql += " convert(datetime,'" + DateTime.Parse(DataHoraOcorrencia).ToString("dd/MM/yyyy HH:mm:ss") + "', 103) ,";
            strsql += " convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "', 103) ,";
            strsql += " '" + descricaoOcorrencia.ToUpper().Trim() + "',";
            strsql += "'SIM', ";
            strsql += latitude + ", ";
            strsql += longitude;
            strsql += " ) select '1' ";

            //ExecutaBD.ExecutaComandoSql(strsql, RetornarStringConexao(cd_cliente));

            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;
            cd.ExecuteScalar();

            GravarLog("gravarDocumentoOcorrencia_wce", strsql, "Auditoria");


            strsql = "SELECT FINALIZADOR FROM OCORRENCIA WHERE IDOCORRENCIA = " + int.Parse(idOcorrencia);
            //string finalizadora = ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente));

            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;
            string finalizadora = cd.ExecuteScalar().ToString();
            GravarLog("gravarDocumentoOcorrencia_wce", strsql, "Auditoria");


            strsql = "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " " + (finalizadora == "SIM" ? " , DATADECONCLUSAO= convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "', 103)" : "") + "  WHERE IDDocumento=" + idDocumento + " Select '1'";
            //--            ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente));

            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;
            cd.ExecuteNonQuery();
            GravarLog("gravarDocumentoOcorrencia_wce", strsql, "Auditoria");


            m[0] = "TRUE";

            oTrans.Commit();
            cn.Close();
            cn.Dispose();

            gravarPosicaoOcorrenciaInterno(idDt, latitude, longitude, DataHoraPosicao, cd_cliente, senhaSeguranca, "SIM");
                       
            enviarEmailGrupoLogos("Gravando Entrega - Android", "IDDocumento: " + idDocumento + "  IDDocumentoOcorrencia: " + IdDocOco + "   cd_cliente: " + cd_cliente + " IDDT: " + idDt);

            GrarvarUltimoEnvioDeDadosAndroid(idDt, cd_cliente, senhaSeguranca);
            return m;
        }
        catch (Exception EX)
        {
            m[0] = "FALSE";
            oTrans.Rollback();
            cn.Close();
            cn.Dispose();
            GravarLog("gravarDocumentoOcorrenciaSemImagem", EX.Message, "Erro");
            enviarEmailGrupoLogos("ERRO NO WSANDROID", EX.Message + "-" + EX.StackTrace + "-" + EX.InnerException);
            throw EX;
        }
    }


    [WebMethod]
    public void gravarDocumentoOcorrenciaSemImagem_wce(string idDocumento, string idOcorrencia, string descricaoOcorrencia, string IDFilial, string longitude, string latitude, string idDt, string DataHoraPosicao, string DataHoraOcorrencia, string cd_cliente, string senhaSeguranca)
    {
        GravarLog("gravarDocumentoOcorrenciaSemImagem_wce", "Iniciou", "Auditoria");

        string IdDocOco = RetornarIdTabela("DOCUMENTOOCORRENCIA", RetornarStringConexao(cd_cliente, senhaSeguranca));

        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        DbConnection cn = factory.CreateConnection();
        DbCommand cd = factory.CreateCommand();

        cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
        cd.Connection = cn;
        cn.Open();
        //DbTransaction oTrans;
        //oTrans = cn.BeginTransaction();

        string[] m = new string[1];
        m[0] = "";
        try
        {

            cd.CommandText = "SELECT ISNULL(IDDOCUMENTOOCORRENCIA, 0) FROM DOCUMENTO WHERE IDDOCUMENTO = " + idDocumento;
            cd.CommandType = CommandType.Text;
            //cd.Transaction = oTrans;

            if (cn.State == ConnectionState.Closed)
            {
                cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
                cn.Open();
            }

            string x = cd.ExecuteScalar().ToString();
            GravarLog("gravarDocumentoOcorrenciaSemImagem_wce", cd.CommandText, "Auditoria");


            if (int.Parse(x) > 0)
            {
                m[0] = "TRUE";                
                cn.Close();
                cn.Dispose();

                GravarLog("gravarDocumentoOcorrenciaSemImagem_wce", "IDDocumento:"+ idDocumento+ "  - Ja existe IdDocumentoOcorrencia - "+ x.ToString(), "Auditoria");

                return ;
            }

            string strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
            strsql += " IDDocumentoOcorrencia,";
            strsql += " IDDocumento,";
            strsql += " IDFilial,";
            strsql += " IDOcorrencia,";
            strsql += " DataOcorrencia,";
            strsql += " Descricao,";
            strsql += " Sistema, ";
            strsql += " Latitude, ";
            strsql += " Longitude ";
            strsql += " ) VALUES (";
            strsql += IdDocOco + " ,";
            strsql += idDocumento + " ,";
            strsql += Convert.ToInt32(IDFilial) + " ,";
            strsql += int.Parse(idOcorrencia) + " ,";
            strsql += " convert(datetime,'" + DateTime.Parse(DataHoraOcorrencia).ToString("dd/MM/yyyy HH:mm:ss") + "', 103) ,";
            strsql += " '" + descricaoOcorrencia.ToUpper().Trim() + "',";
            strsql += "'SIM', ";
            strsql += latitude + ", ";
            strsql += longitude;
            strsql += " ) select '1' ";

            //ExecutaBD.ExecutaComandoSql(strsql, RetornarStringConexao(cd_cliente));
            GravarLog("gravarDocumentoOcorrenciaSemImagem_wce", strsql, "Auditoria");

            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            //cd.Transaction = oTrans;

            if (cn.State == ConnectionState.Closed)
            {
                cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
                cn.Open();
            }

            cd.ExecuteScalar();

            strsql = "SELECT FINALIZADOR FROM OCORRENCIA WHERE IDOCORRENCIA = " + int.Parse(idOcorrencia);
            //string finalizadora = ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente));

            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            //cd.Transaction = oTrans;
            if (cn.State == ConnectionState.Closed)
            {
                cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
                cn.Open();
            }
            string finalizadora = cd.ExecuteScalar().ToString();
            GravarLog("gravarDocumentoOcorrenciaSemImagem_wce", strsql, "Auditoria");


            strsql = "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " " + (finalizadora == "SIM" ? " , DATADECONCLUSAO= convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "', 103)" : "") + "  WHERE IDDocumento=" + idDocumento + " Select '1'";
            //--            ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente));

            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            //cd.Transaction = oTrans;
            if (cn.State == ConnectionState.Closed)
            {
                cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
                cn.Open();
            }
            cd.ExecuteNonQuery();
            GravarLog("gravarDocumentoOcorrenciaSemImagem_wce", strsql, "Auditoria");

            ////grava a ultimo Envio de Dados
            //cd.CommandText = "update rastreador set UtltimoEnvioDeDados=getdate() where ultimaDT = '"+idDt+"'";
            //cd.CommandType = CommandType.Text;
            ////cd.Transaction = oTrans;
            //if (cn.State == ConnectionState.Closed)
            //{
            //    cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
            //    cn.Open();
            //}
            //cd.ExecuteNonQuery();


            m[0] = "TRUE";

            //oTrans.Commit();
            cn.Close();
            cn.Dispose();

            GravarLog("gravarDocumentoOcorrenciaSemImagem_wce", "Finalizou", "Auditoria");
            enviarEmailGrupoLogos("Gravando Entrega - Android", "IDDocumento: " + idDocumento + "  IDDocumentoOcorrencia: " + IdDocOco + "   cd_cliente: " + cd_cliente + " IDDT: " + idDt);


        }
        catch (Exception EX)
        {
            m[0] = "FALSE";
            //oTrans.Rollback();
            GravarLog("gravarDocumentoOcorrenciaSemImagem_wce", EX.Message, "erro");

            cn.Close();
            cn.Dispose();
            enviarEmailGrupoLogos("ERRO NO WSANDROID _ Id: " + idDocumento, EX.Message + "-" + EX.StackTrace + "-" + EX.InnerException);
            throw EX;
        }
    }

    [WebMethod]
    public void gravarImagens(string iddocumento, string idocorrencia, byte[] image, string cd_cliente, string senhaSeguranca)
    {
        try
        {

            GravarLog("gravrImagens", "Iniciou", "Auditoria");

            string ssql = "SELECT TOP 1  DO.IDDOCUMENTOOCORRENCIA FROM DOCUMENTO D INNER JOIN DOCUMENTOOCORRENCIA DO ON DO.IDDOCUMENTO = D.IDDOCUMENTO INNER JOIN OCORRENCIA O ON O.IDOCORRENCIA = DO.IDOCORRENCIA WHERE D.IDDOCUMENTO =" + iddocumento + "  AND O.IDOCORRENCIA = " + idocorrencia + " ORDER BY 1 DESC";
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

            cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
            cd.Connection = cn;
            cn.Open();

            cd.CommandText = ssql;
            GravarLog("gravarImagens", cd.CommandText, "Auditoria");

            cd.CommandType = CommandType.Text;
            string x = cd.ExecuteScalar().ToString();

            if (x == "")
                throw new Exception("Não encontrou documentoocorrencia");

            string id = RetornarIdTabela("DocumentoOcorrenciaArquivo", RetornarStringConexao(cd_cliente, senhaSeguranca));

            ssql = "INSERT INTO DOCUMENTOOCORRENCIAARQUIVO (IDDOCUMENTOOCORRENCIAARQUIVO, IDDOCUMENTOOCORRENCIA, ARQUIVO) VALUES (" + id + ", " + x + ", @IMAGEM)";
            cd = new SqlCommand();

            cd.CommandText = ssql.ToString();
            GravarLog("gravarImagens", cd.CommandText, "Auditoria");

            cd.CommandType = CommandType.Text;
            cd.Connection = cn;
            cd.Parameters.Add(new SqlParameter("@IMAGEM", image));

            cd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
            GravarLog("gravrImagens", "Finalizou", "Auditoria");
            enviarEmailGrupoLogos("gravarImagens", "IDDocumento: " + iddocumento + "   cd_cliente" + cd_cliente);


        }
        catch (Exception ex)
        {
            GravarLog("gravrImagens", ex.Message, "Erro");
            enviarEmailGrupoLogos("Erro gravarImagens", ex.Message+" - IDDocumento: " + iddocumento + "   cd_cliente" + cd_cliente);

        }
    }


    [WebMethod]
    public void gravarDocumentoOcorrencia_wce(
        string idDocumento,
        string idOcorrencia,
        string descricaoOcorrencia,
        string IDFilial,
        byte[] image,
        string longitude,
        string latitude,
        string idDt,
        string DataHoraPosicao,
        string dataHoraOcorencia,
        string cd_cliente,
        string senhaSeguranca)
    {
        GravarLog("gravarDocumentoOcorrencia_wce", "Iniciou", "Auditoria");
        string[] m = new string[1];
        m[0] = "";
        string IdDocOco = RetornarIdTabela("DOCUMENTOOCORRENCIA", RetornarStringConexao(cd_cliente, senhaSeguranca));
        string id = RetornarIdTabela("DocumentoOcorrenciaArquivo", RetornarStringConexao(cd_cliente, senhaSeguranca));

        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        DbConnection cn = factory.CreateConnection();
        DbCommand cd = factory.CreateCommand();

        cn.ConnectionString = RetornarStringConexao(cd_cliente, senhaSeguranca);
        cd.Connection = cn;
        cn.Open();
        //DbTransaction oTrans;
        //oTrans = cn.BeginTransaction();
        try
        {

            cd.CommandText = "SELECT ISNULL(IDDOCUMENTOOCORRENCIA, 0) FROM DOCUMENTO WHERE IDDOCUMENTO = " + idDocumento;
            GravarLog("gravarDocumentoOcorrencia_wce", cd.CommandText, "Auditoria");

            cd.CommandType = CommandType.Text;
            // cd.Transaction = oTrans;
            string x = cd.ExecuteScalar().ToString();

            if (int.Parse(x) > 0)
            {
                m[0] = "TRUE";
                //  oTrans.Commit();
                cn.Close();
                cn.Dispose();
                //return m;
            }

            string strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
            strsql += " IDDocumentoOcorrencia,";
            strsql += " IDDocumento,";
            strsql += " IDFilial,";
            strsql += " IDOcorrencia,";
            strsql += " DataOcorrencia,";
            strsql += " Descricao,";
            strsql += " Sistema, ";
            strsql += " Latitude, ";
            strsql += " Longitude ";
            strsql += " ) VALUES (";
            strsql += IdDocOco + " ,";
            strsql += idDocumento + " ,";
            strsql += Convert.ToInt32(IDFilial) + " ,";
            strsql += int.Parse(idOcorrencia) + " ,";
            //strsql += " getDate() ,";
            strsql += " convert(datetime,'" + DateTime.Parse(dataHoraOcorencia).ToString("dd/MM/yyyy HH:mm:ss") + "', 103) ,";
            strsql += " '" + descricaoOcorrencia.ToUpper().Trim() + "',";
            strsql += "'SIM', ";
            strsql += latitude + ", ";
            strsql += longitude;
            strsql += " ) select '1' ";

            //ExecutaBD.ExecutaComandoSql(strsql, RetornarStringConexao(cd_cliente));
            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            //cd.Transaction = oTrans;
            cd.ExecuteScalar();
            GravarLog("gravarDocumentoOcorrencia_wce", strsql, "Auditoria");

            strsql = "SELECT FINALIZADOR FROM OCORRENCIA WHERE IDOCORRENCIA = " + int.Parse(idOcorrencia);
            //string finalizadora = ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente));

            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            //cd.Transaction = oTrans;
            string finalizadora = cd.ExecuteScalar().ToString();

            strsql = "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " " + (finalizadora == "SIM" ? " , DATADECONCLUSAO= convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "', 103)" : "") + "  WHERE IDDocumento=" + idDocumento + " Select '1'";
            //ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente));
            cd.CommandText = strsql;
            cd.CommandType = CommandType.Text;
            //cd.Transaction = oTrans;
            cd.ExecuteNonQuery();

            //oTrans.Commit();
            cn.Close();
            cn.Dispose();

            if (image != null)
            {
                strsql = "INSERT INTO DocumentoOcorrenciaArquivo (IDDocumentoOcorrenciaArquivo, IDDocumentoOcorrencia, Arquivo) VALUES (" + id + ", " + IdDocOco + ", @IMAGEM)";
                SqlCommand command = new SqlCommand();
                SqlConnection vv = new SqlConnection(RetornarStringConexao(cd_cliente, senhaSeguranca));
                command.CommandText = strsql.ToString();
                command.CommandType = CommandType.Text;
                command.Connection = vv;
                command.Parameters.Add(new SqlParameter("@IMAGEM", image));
                vv.Open();
                command.ExecuteNonQuery();
                vv.Close();
                vv.Dispose();

                GravarLog("gravarDocumentoOcorrencia_wce", strsql, "Auditoria");
            }

            m[0] = "TRUE";
            //gravarPosicaoOcorrenciaInterno(idDt, latitude, longitude, DataHoraPosicao, cd_cliente, senhaSeguranca, "SIM");

            enviarEmailGrupoLogos("Gravando Entrega - Android", "IDDocumento: " + idDocumento + "  IDDocumentoOcorrencia: " + IdDocOco + "   cd_cliente" + cd_cliente);

            //return m;
            GravarLog("gravarDocumentoOcorrencia_wce", "Finalizou", "Auditoria");

        }
        catch (Exception EX)
        {
            //oTrans.Rollback();
            cn.Close();
            cn.Dispose();
            GravarLog("gravarDocumentoOcorrencia_wce", EX.Message, "Erro");
            m[0] = "FALSE";
            enviarEmailGrupoLogos("ERRO NO WSANDROID", EX.Message + "-" + EX.StackTrace + "-" + EX.InnerException + "- DATA: " + descricaoOcorrencia);
            throw EX;
        }

    }

    [WebMethod]
    public void gravarPosicaoOcorrencia(string IDDT, string LATITUDE, string LONGITUDE, string DATAHORA, string cd_cliente, string senhaSeguranca, string ptoOcoc)
    {
        string strsql = "Select IDRASTREAMENTO from RASTREAMENTO where DATAHORA=convert(datetime,'" + DATAHORA + "', 103) and IDDT=" + IDDT;
        DataTable dt = ExecutaBD.RetornarDataTable(strsql, RetornarStringConexao(cd_cliente, senhaSeguranca));

        if (dt.Rows.Count == 0)
        {
            strsql = "INSERT INTO RASTREAMENTO (IDRASTREAMENTO, IDRASTREADOR, IDDT, LATITUDE, LONGITUDE, DATAHORA, PONTODEOCORRENCIA) VALUES (" + RetornarIdTabela("RASTREAMENTO", RetornarStringConexao(cd_cliente, senhaSeguranca)) + ", 1, " + IDDT + ", " + LATITUDE + ", " + LONGITUDE + ", convert(datetime,'" + DATAHORA + "', 103), '" + ptoOcoc + "') select '1'";
            ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente, senhaSeguranca));
        }

        GrarvarUltimoEnvioDeDadosAndroid(IDDT, cd_cliente, senhaSeguranca);
        
    }

    [WebMethod]
    public void gravarPosicaoOcorrenciaInterno(string IDDT, string LATITUDE, string LONGITUDE, string DATAHORA, string cd_cliente, string senhaSeguranca, string ptoOcoc)
    {
        string strsql = "Select IDRASTREAMENTO from RASTREAMENTO where DATAHORA=convert(datetime,'" + DATAHORA + "', 103) and IDDT=" + IDDT;
        DataTable dt = ExecutaBD.RetornarDataTable(strsql, RetornarStringConexao(cd_cliente, senhaSeguranca));

        if (dt.Rows.Count == 0)
        {
            strsql = "INSERT INTO RASTREAMENTO (IDRASTREAMENTO, IDRASTREADOR, IDDT, LATITUDE, LONGITUDE, DATAHORA, PONTODEOCORRENCIA) VALUES (" + RetornarIdTabela("RASTREAMENTO", RetornarStringConexao(cd_cliente, senhaSeguranca)) + ", 1, " + IDDT + ", " + LATITUDE + ", " + LONGITUDE + ", convert(datetime,'" + DATAHORA + "', 103), '" + ptoOcoc + "') select '1'";
            ExecutaBD.ExecutarSQLRetornarIDs(strsql, RetornarStringConexao(cd_cliente, senhaSeguranca));
        }

        GrarvarUltimoEnvioDeDadosAndroid(IDDT, cd_cliente, senhaSeguranca);
    }

    //public static string RetornarIdTabela(string Tabela, string cnn)
    //{
    //    DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
    //    DbConnection cn = factory.CreateConnection();
    //    DbCommand cd = factory.CreateCommand();

    //    cn.ConnectionString = cnn;
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

            string sql = " UPDATE ZID_" + Tabela + " SET ID=ID+1 ; SELECT ID FROM ZID_" + Tabela ;

            cd.CommandText = sql;
            cd.CommandType = CommandType.Text;
            cd.Transaction = oTrans;

            ////se nao existe insere e já retorna o novo id
            //if (Convert.ToInt32(cd.ExecuteScalar()) == 0)
            //{
            //    sql = " INSERT IDTABELA (Tabela, Id) VALUES( ";
            //    sql += " '" + Tabela + "', ";
            //    sql += " (SELECT ISNULL(MAX(" + "ID" + Tabela + "),0)+1 FROM " + Tabela + ")) SELECT ISNULL(MAX(ID),0) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

            //    cd.CommandText = sql;
            //    cd.CommandType = CommandType.Text;
            //    cd.Transaction = oTrans;
            //    nId = Convert.ToInt32(cd.ExecuteScalar());
            //    oTrans.Commit();
            //    return nId.ToString();
            //}
            //else
            //{
                //se exite gera um novo e compara os ids, E OS ACERTA para efetivar
                //sql = " UPDATE IDTABELA SET ID = ";
                //sql += " CASE WHEN (SELECT MAX(ID)+1 FROM IDTABELA WHERE TABELA='" + Tabela + "' )>=(SELECT coalesce(MAX(ID" + Tabela + "),0) FROM " + Tabela + ") THEN ID+1 ";
                //sql += " ELSE (SELECT MAX(ID" + Tabela + ")+1 FROM " + Tabela + ")";
                //sql += " END WHERE TABELA='" + Tabela + "' SELECT MAX(ID) FROM IDTABELA WHERE TABELA='" + Tabela + "'";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;
                nId = Convert.ToInt32(cd.ExecuteScalar());
                oTrans.Commit();
                return nId.ToString();
           // }
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
        public string IDDOCUMENTO { get; set; }
        public string NUMERODOCUMENTO { get; set; }
        public string IDFILIALATUAL { get; set; }
        public string VOLUMES { get; set; }
        public string PESOBRUTO { get; set; }
        public string PLACA { get; set; }
        public string NUMEROPLACA { get; set; }
        public string NUMERODT { get; set; }
        public string REMETENTE { get; set; }
        public string DESTINATARIO { get; set; }
    }

    public class aparelho
    {
        public string IdRastreador { get; set; }
        public string Chave { get; set; }
        public string Nome { get; set; }
        public string Tempo { get; set; }
        public string EnviaPosicaoZerada { get; set; }
        public string NumeroFone { get; set; }
        public string EnviaFoto { get; set; }

    }

    public class listar_documentos
    {
        public string IDDOCUMENTOOCORRENCIA { get; set; }
        public string NUMERODOCUMENTO { get; set; }
        public string IDDOCUMENTO { get; set; }
        public string IDFILIALATUAL { get; set; }
        public string VOLUMES { get; set; }
        public string PESOBRUTO { get; set; }
        public string PLACANUMERO { get; set; }
        public string PLACA { get; set; }
        public string IDDT { get; set; }
        public string REMETENTE { get; set; }
        public string DESTINATARIO { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string ESTADO { get; set; }
        public string PAIS { get; set; }
        public string OCORRENCIA { get; set; }
        public string TEMPO { get; set; }
        public string ENVIAPOSICAOZERADA { get; set; }
        public string CHAVEORIGEM { get; set; }
    }

    public class Ocorrencias
    {
        public string IDOCORRENCIA { get; set; }
        public string CODIGO { get; set; }
        public string NOME { get; set; }
        public string RESPONSABILIDADE { get; set; }
        public string FINALIZADOR { get; set; }
    }

    public static void enviarEmailGrupoLogos(string assunto, string corpo)
    {
        //create the mail message
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

        //set the addresses
        mail.From = new System.Net.Mail.MailAddress("moises@sistecno.com.br");
        mail.Bcc.Add("moises@sistecno.com.br");
        mail.To.Add("moises@sistecno.com.br");


        //set the content
        mail.Subject = assunto;
        mail.Body = corpo;
        mail.Priority = System.Net.Mail.MailPriority.High;
        mail.IsBodyHtml = true;


        //send the message
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.sistecno.com.br");
        smtp.EnableSsl = false;

        System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential("moises@sistecno.com.br", "mo2404");
        smtp.Credentials = credenciais;
        smtp.Send(mail);
    }

    private void GravarLog(string metodo, string Mensagem, string tipo)
    {
        //StreamWriter w = File.AppendText(@"c:\Inetpub\wwwroot\BKP\LogWs\log" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".txt");
        //w.WriteLine(tipo.Substring(0, 3) + "| Data: " + DateTime.Now + " |Metodo: " + metodo + ", Mensagem: " + Mensagem, w);
        //w.Close();

    }

    public class RastramentoDto
    {
        public string _Longitude { get; set; }
        public string _Latitude { get; set; }
        public string _DataHora { get; set; }
        public string _PontoDeOcorrencia { get; set; }
        public string _IdDt { get; set; }
        public string _Sinc { get; set; }
    }

}