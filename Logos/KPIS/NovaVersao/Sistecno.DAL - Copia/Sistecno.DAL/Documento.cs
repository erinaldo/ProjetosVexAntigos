using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using Sistecno.DAL.Models;

namespace Sistecno.DAL
{
    public class Documento
    {
        public DataTable RetornarConferencia(bool chave, string numero, int? IdCliente, int? idFilial, string cnx)
        {
            string sqlstr = "SELECT DOC.IDDOCUMENTO, ";
            sqlstr += " DOC.NUMERO, DOC.SERIE, DOC.ValorDaNota, LEFT(RAZAOSOCIALNOME, 30) DESTINATARIO, DOC.VOLUMES ";
            sqlstr += " FROM DOCUMENTO doc WITH (NOLOCK) ";
            sqlstr += " INNER JOIN CADASTRO CADDEST WITH (NOLOCK) ON CADDEST.IDCadastro = DOC.IDDestinatario ";
            sqlstr += " WHERE "  ;

            if (chave)
            {
                sqlstr += " DOC.DOCUMENTODOCLIENTE4 = '" + numero + "'";
            }
            else
            {
                sqlstr += " DOC.IDCLIENTE=" + IdCliente;
                sqlstr += " AND doc.NUMERO = '" + numero + "' ";
            }
            sqlstr += " AND doc.TIPODEDOCUMENTO='NOTA FISCAL' ";
            sqlstr += " AND doc.ATIVO='SIM' ";

            if(idFilial!=null)
                sqlstr += " AND DOC.IDFILIALATUAL=" + idFilial;
            
            sqlstr += " ORDER BY 1 DESC             ";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }
        
        public DataTable RetornarConferenciaChave(string chave, string cnx)
        {
            string sqlstr = "SELECT DOC.IDDOCUMENTO, ";
            sqlstr += " DOC.NUMERO, DOC.SERIE, DOC.ValorDaNota, LEFT(RAZAOSOCIALNOME, 30) DESTINATARIO, DOC.VOLUMES ";
            sqlstr += " FROM DOCUMENTO doc WITH (NOLOCK) ";
            sqlstr += " INNER JOIN CADASTRO CADDEST WITH (NOLOCK) ON CADDEST.IDCadastro = DOC.IDDestinatario ";
            sqlstr += " WHERE ";

            
            sqlstr += " DOC.DOCUMENTODOCLIENTE4 = '" + chave + "'";
            
            sqlstr += " AND doc.ATIVO='SIM' ";

            
            sqlstr += " ORDER BY 1 DESC             ";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }

        public DataTable RetornarConferencia(int iddocumento, string cnx)
        {
            string sqlstr = "SELECT DOC.IDDOCUMENTO, ";
            sqlstr += " DOC.NUMERO, DOC.SERIE, DOC.ValorDaNota, LEFT(RAZAOSOCIALNOME, 60) DESTINATARIO, DOC.VOLUMES ";
            sqlstr += " FROM DOCUMENTO doc WITH (NOLOCK) ";
            sqlstr += " INNER JOIN CADASTRO CADDEST WITH (NOLOCK) ON CADDEST.IDCadastro = DOC.IDDestinatario ";
            sqlstr += " WHERE DOC.IDDOCUMENTO = "+iddocumento;

            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }

        public DAL.Models.Documento Retornar(DAL.Models.Documento doc, string cnx)
        {
            SistecnoContext context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();


            var query = context.Documentoes.FirstOrDefault(p => p.TipoDeDocumento == doc.TipoDeDocumento
                                                  & p.Numero == doc.Numero
                                                  & p.Serie == doc.Serie
                                                  & p.AnoMes == doc.AnoMes
                                                  & p.IDProprietarioDocumento == doc.IDProprietarioDocumento);


            context.Database.Connection.Close();
            return query;
        }

        public DAL.Models.Documento RetornarVolumes(int idDocumento, string cnx)
        {
            SistecnoContext context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var query = context.Documentoes.FirstOrDefault(p => p.IDDocumento == idDocumento);
            context.Database.Connection.Close();
            return query;
        }

        public DAL.Models.Documento RetornarByIdDocumento(int idDocumento, string cnx)
        {
            try
            {
                SistecnoContext context = new SistecnoContext();
                context.Database.Connection.ConnectionString = cnx;
                context.Database.Connection.Open();
                var query = context.Documentoes.FirstOrDefault(p => p.IDDocumento == idDocumento);
                context.Database.Connection.Close();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int GravarDocumentoImportacaoXml(DAL.Models.Documento doc, string cnx)
        {
            SistecnoContext context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            try
            {
                if (doc.IDDocumento == 0)
                {

                    doc.IDDocumento = DAL.BD.cDb.RetornarIDTabela(cnx, "Documento");

                    if (doc.Numero == (int?)null)
                        doc.Numero = doc.IDDocumento;
                    else
                        doc.Numero = doc.Numero;
                    context.Documentoes.Add(doc);
                    context.SaveChanges();
                }
                else
                {
                    var oC = context.Documentoes.First(i => i.IDDocumento == doc.IDDocumento);

                    oC.DataPlanejada = doc.DataPlanejada;
                    oC.Volumes = doc.Volumes;
                    oC.PesoCubado = doc.PesoCubado;
                    oC.PesoBruto = doc.PesoBruto;
                    oC.Natureza = doc.Natureza;
                    oC.DocumentoDoCliente2 = doc.DocumentoDoCliente2;
                    oC.DocumentodoCliente4 = doc.DocumentodoCliente4;                    
                    oC.IDCliente = doc.IDCliente;
                    oC.IDDestinatario = doc.IDDestinatario;
                    oC.IDRemetente = oC.IDRemetente;
                    oC.DataDeEmissao = doc.DataDeEmissao;
                    context.SaveChanges();

                }

                if (context.Database.Connection.State == ConnectionState.Closed)
                         context.Database.Connection.Open();
                

                foreach (var item in doc.DocumentoObservacaos)
                {
                    if (item.IDDocumentoObservacao == 0)
                    {

                        DAL.Models.DocumentoObservacao obs = new DocumentoObservacao();
                        obs.IDDocumentoObservacao = DAL.BD.cDb.RetornarIDTabela(cnx, "DocumentoObservacao");
                        obs.Observacao = item.Observacao;
                        context.DocumentoObservacaos.Add(obs);
                        context.SaveChanges();
                    }
                    else
                    {
                        var o = context.DocumentoObservacaos.First(i => i.IDDocumentoObservacao == item.IDDocumentoObservacao);
                        o.Observacao = item.Observacao;
                        context.SaveChanges();                    
                    }
                }



                if (context.Database.Connection.State == ConnectionState.Closed)
                    context.Database.Connection.Open();


                DAL.Models.DocumentoAguardandoCTRC cctrc = new DAL.Models.DocumentoAguardandoCTRC();
                cctrc.IdDocumentoAguardandoCTRC = DAL.BD.cDb.RetornarIDTabela(cnx, "DocumentoAguardandoCTRC");
                cctrc.IdDocumento = doc.IDDocumento;
                cctrc.IdFilial = doc.IDFilial;
                context.DocumentoAguardandoCTRCs.Add(cctrc);
                context.SaveChanges();


                if (context.Database.Connection.State == ConnectionState.Closed)
                    context.Database.Connection.Open();


                DAL.Models.DocumentoFilial ddf = new DAL.Models.DocumentoFilial();
                
                
                ddf.IDDocumentoFilial = DAL.BD.cDb.RetornarIDTabela(cnx, "DocumentoFilial");
                ddf.IDDocumento = doc.IDDocumento;
                ddf.IDFilial = doc.IDFilial;
                ddf.IDRegiaoItem = 1;
                ddf.IdRegiaoItemCliente = 0;
                ddf.IdRegiaoItemFilial = null;
                ddf.IdRegiaoItemTransportador = null;
                ddf.Situacao = "AGUARDANDO GERAR CTRC";

                context.DocumentoFilials.Add(ddf);
                context.SaveChanges();


                return doc.IDDocumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();

            }

        }

        public int Gravar(DAL.Models.Documento doc, string cnx)
        {
            SistecnoContext context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            try
            {
                if (doc.IDDocumento == 0)
                {                    

                    doc.IDDocumento = DAL.BD.cDb.RetornarIDTabela(cnx, "Documento");
                    
                    if (doc.Numero == (int?)null)
                        doc.Numero = doc.IDDocumento;
                    else
                        doc.Numero = doc.Numero;
                    context.Documentoes.Add(doc);
                    context.SaveChanges();                    
                }
                else
                {
                    var oC = context.Documentoes.First(i => i.IDDocumento == doc.IDDocumento);

                    oC.DataPlanejada = doc.DataPlanejada;
                    oC.Volumes = doc.Volumes;
                    oC.PesoCubado = doc.PesoCubado;
                    oC.PesoBruto = doc.PesoBruto;
                    oC.Natureza = doc.Natureza;
                    oC.DocumentoDoCliente2 = doc.DocumentoDoCliente2;
                    oC.DocumentodoCliente4 = doc.DocumentodoCliente4;
                    //oC.DocumentoObservacao = doc.DocumentoObservacao;
                    oC.IDCliente = doc.IDCliente;
                    oC.IDDestinatario = doc.IDDestinatario;
                    oC.IDRemetente = oC.IDRemetente;
                    oC.DataDeEmissao = doc.DataDeEmissao;
                    context.SaveChanges();                    
                   
                }

                if (context.Database.Connection.State == ConnectionState.Closed)
                {
                    context.Database.Connection.Open();
                }

                foreach (var item in doc.DocumentoObservacaos)
                {
                    if (item.IDDocumentoObservacao == 0)
                    {
                
                        DAL.Models.DocumentoObservacao obs = new DocumentoObservacao();
                        obs.IDDocumentoObservacao = DAL.BD.cDb.RetornarIDTabela(cnx, "DocumentoObservacao");
                        obs.Observacao = item.Observacao;
                        context.DocumentoObservacaos.Add(obs);
                        context.SaveChanges();
                    }
                    else
                    {
                        var o = context.DocumentoObservacaos.First(i => i.IDDocumentoObservacao == item.IDDocumentoObservacao);

                        o.Observacao = item.Observacao;
                        context.SaveChanges();


                    }
                }

                return doc.IDDocumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();

            }

        }
        /// <summary>
        /// Grava e retorna o id Do Conhecimento
        /// </summary>
        /// <param name="documento">Objeto com os dados do Conhecimentp</param>
        /// <param name="IdsDocumentos">Lista de IdDocumentos filhos</param>
        /// <param name="cnx">String de Conexo</param>
        /// <returns>Id do Conhecimento</returns>

        public int Gravar(DAL.Models.Documento documento, List<int> IdsDocumentos, string cnx)
        {
            //Pega os Ids

            int idDocumentoPai = DAL.BD.cDb.RetornarIDTabela(cnx, "Documento");
            int idDocumentoFilial = DAL.BD.cDb.RetornarIDTabela(cnx, "DocumentoFilial");

            int[] idRelacionados = new int[IdsDocumentos.Count];

            for (int i = 0; i < idRelacionados.Length; i++)
            {
                idRelacionados[i] = DAL.BD.cDb.RetornarIDTabela(cnx, "DocumentoRelacionado");
            }

            //novo
            if (documento.IDDocumento == 0)
            {
                SistecnoContext context = new SistecnoContext();
                context.Database.Connection.ConnectionString = cnx;
                context.Database.Connection.Open();
                try
                {
                    //using (TransactionScope transaction = new TransactionScope())
                    //{
                        documento.IDDocumento = idDocumentoPai;
                        documento.Numero = documento.IDDocumento;//Numera somente na hora do envio para o sefaz
                        documento.Serie = "1";// Será alterado depois
                        context.Documentoes.Add(documento);
                        context.SaveChanges();

                        //salvar documentofilial
                        DAL.Models.DocumentoFilial docfil = new DocumentoFilial();
                        docfil.IDDocumentoFilial = idDocumentoFilial;
                        docfil.IDDocumento = documento.IDDocumento;
                        docfil.IDFilial = documento.IDFilial;
                        docfil.Situacao = "EM DIGITACAO";
                        docfil.IDRegiaoItem = 0;
                        context.DocumentoFilials.Add(docfil);
                        context.SaveChanges();

                        if (IdsDocumentos != null)
                        {
                            for (int i = 0; i < IdsDocumentos.Count; i++)
                            {
                                context.Database.ExecuteSqlCommand("INSERT INTO DOCUMENTORELACIONADO VALUES(" + idRelacionados[i] + ", " + documento.IDDocumento + ", " + IdsDocumentos[i] + ", NULL)", "");
                            }
                        }

                    //    transaction.Complete();

                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (context.Database.Connection.State == ConnectionState.Open)
                    {
                        context.Database.Connection.Close();
                    }
                }
            }

            return documento.IDDocumento;

        }
        
        public DataTable RetornarNfsConhecimento(int idDocumento, string cnx)
        {
            string sqlstr = " SELECT  ";
            sqlstr += " ROW_NUMBER() OVER(ORDER BY d.IDDOCUMENTO DESC) SEQUENCIA, ";
            sqlstr += " D.IDDOCUMENTO, ";
            sqlstr += " cast(D.NUMERO as varchar(10)) + ' - ' + cast(D.SERIE as varchar(10)) NOTAFISCAL, SERIE, METRAGEMCUBICA M3, PesoCubado,  ";
            sqlstr += " isnull(D.VOLUMES, 0) VOLUMES, ";
            sqlstr += " isnull(D.PESOBRUTO, 0)  PESO, ";
            sqlstr += " isnull(D.valordanota, 0)  VALORDANOTA, '' CHAVE, ";
            sqlstr += "  isnull(D.IDREMETENTE,0) IDREMETENTE, '' REMETENTE, '' DESTINATARIO, ISNULL(D.IDDESTINATARIO,0) IDDESTINATARIO ";
            sqlstr += " FROM DOCUMENTORELACIONADO DR  ";
            sqlstr += " INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = DR.IDDOCUMENTOFILHO ";
            sqlstr += " WHERE DR.IDDOCUMENTOPAI =  " + idDocumento;            
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }
        
        public DataTable RetornarCteImpressao(int idfilial, string cnx)
        {
            string sqlstr = " Select Doc.IdCliente CODIGOCLIENTE, Cli.FantasiaApelido As Cliente,COUNT(*) QUANTIDADE ";
            sqlstr += " From Documento Doc with (Nolock)  ";
            sqlstr += " Inner Join Cadastro Cli on (Cli.IdCadastro=Doc.IDCliente)  ";
            sqlstr += " Left Join DocumentoEletronico Doc@ on (Doc@.IdDocumento = Doc.IdDocumento)  ";
            sqlstr += " Where Doc.IdFilial =" + idfilial;
            sqlstr += " And   Doc.TipoDeDocumento = 'CONHECIMENTO' ";
            sqlstr += " And   Doc.Ativo='SIM' ";
            sqlstr += " And   Doc.Enviado='SIM' ";
            sqlstr += " AND     Doc@.STATUS='Autorizado o uso do CT-e'";
            sqlstr += " And   Doc.Impresso = 'NAO' ";
            sqlstr += " Group By Cli.FantasiaApelido, Doc.IdCliente Order by 2  ";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }

        public DataTable RetornarCteImpressaoDetalhe(int idCliente, int idFilial, string cnx)
        {
            string sqlstr = " SELECT  ";
            sqlstr += " DOC.IDDOCUMENTO, ";
            sqlstr += " CAST(DOC.NUMERO AS VARCHAR(20)) +'-'+ CAST (DOC.SERIE AS VARCHAR(10)) NUMERO, ";
            sqlstr += " DOC.SERIE, ";
            sqlstr += " DE.STATUS, ";
            sqlstr += " DE.NUMERORECIBO, ";
            sqlstr += " DE.NUMEROPROTOCOLO, ";
            sqlstr += " DE.IDNOTA ";
            sqlstr += " FROM DOCUMENTO DOC WITH (NOLOCK) ";
            sqlstr += " INNER JOIN CADASTRO CLI ON (CLI.IDCADASTRO=DOC.IDCLIENTE)  ";
            sqlstr += " INNER JOIN DOCUMENTOELETRONICO DE ON (DE.IDDOCUMENTO = DOC.IDDOCUMENTO)  ";
            sqlstr += " WHERE DOC.IDFILIAL =  " + idFilial;
            sqlstr += " AND   DOC.TIPODEDOCUMENTO = 'CONHECIMENTO' ";
            sqlstr += " AND   DOC.ATIVO='SIM' ";
            sqlstr += " AND   DOC.ENVIADO='SIM' ";
            sqlstr += " AND   DOC.IMPRESSO = 'NAO' ";
            sqlstr += " AND   DOC.IDCLIENTE =  " + idCliente;
            sqlstr += " AND  LEN(NUMEROPROTOCOLO)>1 ";
            sqlstr += " AND DE.STATUS='Autorizado o uso do CT-e'";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }

        public DataTable RetornarDetalheConhecimento(int idDocumento, string cnx)
        {
            string sqlstr = " SELECT DOC.IDDOCUMENTO,DOC.IDFILIAL IDEMITENTE,DOC.IDREMETENTE,DOC.IDDESTINATARIO,DOC.NUMERO,DOC.SERIE,DOC.IDMODAL,0 TIPODESERVICO,0 FINALIDADEDAEMISSAO,0 FORMADEEMISSAO, doc.TIPODEDOCUMENTO,  de.cstatus  ";
            sqlstr += " FROM DOCUMENTO DOC    ";            
            sqlstr += " LEFT JOIN DOCUMENTOELETRONICO DE ON DE.IDDOCUMENTO = DOC.IDDOCUMENTO   ";
            sqlstr += " where DOC.IDDOCUMENTO=" + idDocumento;
            sqlstr += " ORDER BY DOC.NUMERO DESC ";

            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);

        }

        public void MarcarImpressoByChave(string chave, string cnx)
        {
            string sqlstr = "UPDATE DOCUMENTO SET IMPRESSO='SIM' WHERE IDDOCUMENTO = (SELECT TOP 1 IDDOCUMENTO FROM DOCUMENTOELETRONICO WHERE IDNOTA = '" + chave + "')";
            DAL.BD.cDb.ExecutarSemRetorno(sqlstr, cnx);
        }

        public DataTable RetornarPesquisa(string Numero, string situacao, string tipoDeDocumento, int idfilial, string cStatus, bool abreviarNomes,  string cnx)
        {
            string sqlstr = "";


            if (abreviarNomes)
            {
                sqlstr = "SELECT TOP 100 DOC.IDDOCUMENTO [CODIGO], (CASE DOC.NUMERO WHEN DOC.IDDOCUMENTO THEN NULL ELSE DOC.NUMERO END) NUMERO, SERIE,  DE.STATUS, CADREM.CNPJCPF [CNPJ REM.], LEFT(CADREM.RAZAOSOCIALNOME, 20) [REMETENTE], ";
                sqlstr += " CADDEST.CNPJCPF [CNPJ DEST.], LEFT(CADDEST.RAZAOSOCIALNOME, 20) [DESTINATARIO], LEFT(C.NOME, 10) +' / '+ E.UF [CIDADE]  ";
            }
            else
            {
                sqlstr = "SELECT TOP 50 DOC.IDDOCUMENTO [CODIGO], (CASE DOC.NUMERO WHEN DOC.IDDOCUMENTO THEN NULL ELSE DOC.NUMERO END) NUMERO, SERIE,  DE.STATUS, CADREM.CNPJCPF [CNPJ REM.], LEFT(CADREM.RAZAOSOCIALNOME, 30) [REMETENTE], ";
                sqlstr += " CADDEST.CNPJCPF [CNPJ DEST.], LEFT(CADDEST.RAZAOSOCIALNOME, 30) [DESTINATARIO], C.NOME +' / '+ E.UF [CIDADE]  ";
            }
            
            
            sqlstr += " FROM DOCUMENTO DOC  ";
            sqlstr += " LEFT JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " INNER JOIN CADASTRO CADREM ON CADREM.IDCADASTRO = DOC.IDREMETENTE  ";
            sqlstr += " INNER JOIN CADASTRO CADDEST ON CADDEST.IDCADASTRO = DOC.IDDESTINATARIO  ";
            sqlstr += " LEFT JOIN DOCUMENTOELETRONICO DE ON DE.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " INNER JOIN CIDADE C ON C.IDCIDADE = CADDEST.IDCIDADE ";
            sqlstr += " INNER JOIN ESTADO E ON E.IDESTADO = C.IDESTADO ";
            sqlstr += " WHERE DOC.TIPODEDOCUMENTO ='" + tipoDeDocumento + "'  ";
            sqlstr += " AND DOC.IDFILIAL = " + idfilial;

            if(Numero.Trim() !="")
                sqlstr += " AND DOC.NUMERO LIKE '" + Numero + "%' ";
            
            if (situacao != "")
                sqlstr += " AND SITUACAO='" + situacao + "' ";

            if (cStatus != "")
                sqlstr += " AND DE.CSTATUS = '"+cStatus+"'";

            sqlstr += " ORDER BY 2 DESC";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);

        }


        public DataTable RetornarDocumentosIn(string  idDocumentos, bool abreviarNomes, string cnx)
        {
            string sqlstr = "";


            if (abreviarNomes)
            {
                sqlstr = "SELECT TOP 100 DOC.IDDOCUMENTO [CODIGO], (CASE DOC.NUMERO WHEN DOC.IDDOCUMENTO THEN NULL ELSE DOC.NUMERO END) NUMERO, SERIE,  DE.STATUS, CADREM.CNPJCPF [CNPJ REM.], LEFT(CADREM.RAZAOSOCIALNOME, 20) [REMETENTE], ";
                sqlstr += " CADDEST.CNPJCPF [CNPJ DEST.], LEFT(CADDEST.RAZAOSOCIALNOME, 20) [DESTINATARIO], LEFT(C.NOME, 10) +' / '+ E.UF [CIDADE]  ";
            }
            else
            {
                sqlstr = "SELECT TOP 50 DOC.IDDOCUMENTO [CODIGO], (CASE DOC.NUMERO WHEN DOC.IDDOCUMENTO THEN NULL ELSE DOC.NUMERO END) NUMERO, SERIE,  DE.STATUS, CADREM.CNPJCPF [CNPJ REM.], LEFT(CADREM.RAZAOSOCIALNOME, 30) [REMETENTE], ";
                sqlstr += " CADDEST.CNPJCPF [CNPJ DEST.], LEFT(CADDEST.RAZAOSOCIALNOME, 30) [DESTINATARIO], C.NOME +' / '+ E.UF [CIDADE]  ";
            }


            sqlstr += " FROM DOCUMENTO DOC  ";
            sqlstr += " LEFT JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " INNER JOIN CADASTRO CADREM ON CADREM.IDCADASTRO = DOC.IDREMETENTE  ";
            sqlstr += " INNER JOIN CADASTRO CADDEST ON CADDEST.IDCADASTRO = DOC.IDDESTINATARIO  ";
            sqlstr += " LEFT JOIN DOCUMENTOELETRONICO DE ON DE.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " INNER JOIN CIDADE C ON C.IDCIDADE = CADDEST.IDCIDADE ";
            sqlstr += " INNER JOIN ESTADO E ON E.IDESTADO = C.IDESTADO ";
            sqlstr += " WHERE DOC.IDDOCUMENTO IN(" + idDocumentos + ")";
            sqlstr += " ORDER BY 2 DESC";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);

        }

        public DataTable RetornarPesquisaTelaDocumentos(string Numero, int idCliente, string situacao, int idfilial, string cnx)
        {
            string sqlstr = "SELECT TOP 50 DOC.IDDOCUMENTO [CODIGO], (case DOC.NUMERO when doc.iddocumento then null else doc.numero end) NUMERO, SERIE, DOC.TIPODEDOCUMENTO [TIPO],  DE.STATUS, CADREM.CNPJCPF [CNPJ REM.], LEFT(CADREM.RAZAOSOCIALNOME, 40) [REMETENTE], ";
            sqlstr += " CADDEST.CNPJCPF [CNPJ DEST.], LEFT(CADREM.RAZAOSOCIALNOME, 40) [DESTINATARIO]  ";
            sqlstr += " FROM DOCUMENTO DOC  ";
            sqlstr += " LEFT JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " INNER JOIN CADASTRO CADREM ON CADREM.IDCADASTRO = DOC.IDREMETENTE  ";
            sqlstr += " INNER JOIN CADASTRO CADDEST ON CADDEST.IDCADASTRO = DOC.IDDESTINATARIO  ";
            sqlstr += " LEFT JOIN DOCUMENTOELETRONICO DE ON DE.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " WHERE ((DOC.TIPODEDOCUMENTO = 'CONHECIMENTO') OR (DOC.TIPODEDOCUMENTO ='NOTA FISCAL' AND TIPODESERVICO ='SERVICO')) ";
            sqlstr += " AND DOC.IDFILIAL = " + idfilial;
            sqlstr += " AND DOC.NUMERO LIKE '" + Numero + "%' ";

            if (situacao != "")
                sqlstr += " AND SITUACAO='" + situacao + "' ";

            sqlstr += " ORDER BY 2 DESC";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);

        }


        public XmlDocument retornarXMLPorChave(string chave, string cnx)
        {
            string sqlstr = "SELECT UltimoArquivoXml FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + chave + "'";
            string xml= DAL.BD.cDb.RetornarDataTable(sqlstr, cnx).Rows[0]["UltimoArquivoXml"].ToString();
            XmlDocument xDoc = new XmlDocument();
            
            if(xml!="")
                xDoc.LoadXml(xml);

            return xDoc;
        }

        #region Conhecimento
        public DataTable RetornarNotasDisponivesCte(int idfilial, string agrupamento, string cnx)
        {
            string sqlstr = "";
            string agroup1 = "";
            string agroup2 = "";
            switch (agrupamento)
            {
                case "REM":
                    sqlstr = " SELECT IDREMETENTE CODIGOCLIENTE, CADCLI.RAZAOSOCIALNOME CLIENTE, COUNT(DISTINCT DOC.IDDOCUMENTO) QUANTIDADE ";
                    agroup1 = " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = DOC.IDREMETENTE  ";
                    agroup2 = " GROUP BY IDREMETENTE, CADCLI.RAZAOSOCIALNOME ";
                    break;

                case "DEST":
                    sqlstr = " SELECT IDDESTINATARIO CODIGOCLIENTE, CADCLI.RAZAOSOCIALNOME CLIENTE, COUNT(DISTINCT DOC.IDDOCUMENTO) QUANTIDADE ";
                    agroup1 = " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = DOC.IDDESTINATARIO  ";
                    agroup2 = " GROUP BY IDDESTINATARIO, CADCLI.RAZAOSOCIALNOME ";
                    break;

                case "CLI":
                    sqlstr = " SELECT IDCLIENTE CODIGOCLIENTE, CADCLI.RAZAOSOCIALNOME CLIENTE, COUNT(DISTINCT DOC.IDDOCUMENTO) QUANTIDADE ";
                    agroup1 = " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = DOC.IDCLIENTE  ";
                    agroup2 = " GROUP BY IDCLIENTE, CADCLI.RAZAOSOCIALNOME ";
                    break;
            }
            sqlstr += " FROM DOCUMENTO DOC  ";
            sqlstr += " INNER JOIN DOCUMENTOAGUARDANDOCTRC DAC ON DAC.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " INNER JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += agroup1;
            sqlstr += " WHERE TIPODEDOCUMENTO='NOTA FISCAL'  ";
            sqlstr += " AND DOC.IDFILIAL=" + idfilial;
            sqlstr += " AND SITUACAO='AGUARDANDO GERAR CTRC' ";
            sqlstr += agroup2;
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);

        }

        public DataTable RetornarNotasDisponivesCteDetalhe(int idfilial, string agrupamento, int id, string cnx)
        {
            string sqlstr = "";
            string agroup1 = "";
            switch (agrupamento)
            {
                case "REM":
                    agroup1 = " AND DOC.IDREMETENTE =" + id;

                    break;

                case "DEST":
                    agroup1 = " AND DOC.IDDESTINATARIO =" + id;

                    break;

                case "CLI":
                    agroup1 = " AND DOC.IDCLIENTE =" + id;

                    break;
            }
            sqlstr = " SELECT  ";
            sqlstr += " DOC.IDDOCUMENTO IDDOCUMENTO,   ";
            sqlstr += " CAST(DOC.NUMERO AS VARCHAR(20)) + ' - ' + DOC.SERIE [NÚMERO] ,";
            sqlstr += " ISNULL(DE.IdNota, doc.DocumentoDoCliente4) CHAVE, ";
            sqlstr += " CAD.CNPJCPF + '-' + LEFT(CAD.RAZAOSOCIALNOME, 20) [DESTINATÁRIO], ";
            sqlstr += " E.UF  + ' - ' + LEFT(C.NOME, 10) [UF/CIDADE], ";
            sqlstr += " DOC.PESOBRUTO PESO, ";
            sqlstr += " DOC.VALORDASMERCADORIAS [VALOR MERCADORIAS], DOC.VOLUMES VOLUMES, DOC.VALORDANOTA [VALOR DA NOTA] ";
            sqlstr += " FROM DOCUMENTO DOC  ";
            sqlstr += " INNER JOIN DOCUMENTOAGUARDANDOCTRC DAC ON DAC.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " INNER JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " INNER JOIN CADASTRO CAD ON CAD.IDCADASTRO = DOC.IDDESTINATARIO    ";
            sqlstr += " INNER JOIN CIDADE C ON C.IDCIDADE = CAD.IDCIDADE ";
            sqlstr += " INNER JOIN ESTADO E ON E.IDESTADO = C.IDESTADO ";
            sqlstr += " LEFT JOIN DOCUMENTOELETRONICO DE ON DE.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            sqlstr += " WHERE DOC.TIPODEDOCUMENTO='NOTA FISCAL'  ";
            sqlstr += " AND DOC.IDFILIAL=" + idfilial;
            sqlstr += " AND SITUACAO='AGUARDANDO GERAR CTRC' ";
            sqlstr += agroup1;
            sqlstr += " ORDER BY CAD.CNPJCPF + '-' + LEFT(CAD.RAZAOSOCIALNOME, 20) ";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }

        public DataTable RetornarCteDisponivel(int idfilial, string cnx)
        {
            string ssql = "";

            ssql += " SELECT DOC.IDCLIENTE CODIGOCLIENTE,left(CLI.FANTASIAAPELIDO, 35) AS CLIENTE, DOC.TIPODEDOCUMENTO , COUNT(*) QUANTIDADE ";
            ssql += " FROM DOCUMENTO DOC WITH (NOLOCK) ";
            ssql += " INNER JOIN CADASTRO CLI ON (CLI.IDCADASTRO=DOC.IDCLIENTE)  ";
            ssql += " LEFT JOIN DOCUMENTOELETRONICO DOCE ON (DOCE.IDDOCUMENTO = DOC.IDDOCUMENTO)  ";
            ssql += " WHERE DOC.IDFILIAL =" + idfilial;
            ssql += " AND   (DOC.TIPODEDOCUMENTO = 'CONHECIMENTO'  or (DOC.TIPODEDOCUMENTO = 'NOTA FISCAL' AND TIPODESERVICO='SERVICO')) ";
            ssql += " AND   DOC.ATIVO='SIM' ";
            ssql += " AND   DOC.ENVIADO='NAO' ";
            ssql += " GROUP BY CLI.FANTASIAAPELIDO, DOC.IDCLIENTE, DOC.TIPODEDOCUMENTO  ORDER BY 2 ";
            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);

        }

        public DataTable RetornarCtesEmLote(int idfilial, bool resultadoEnvio, int idLoteEletronico, string cnx)
        {
            string ssql = "";

            //if (resultadoEnvio == false) // pesquisa de lotes com ctes pendentes
            //{
                ssql += " Select top 10 ";
                ssql += " LE.IdLoteEletronico, LE.LoteGerado,LE.LoteEnviadoAoSefaz, LE.Descricao, LE.Recibo, LE.CStatus, LE.Status, ";
                ssql += " COUNT(*) Cte, LE.NomeDaMaquina, LE.EnvLot, ";
                ssql += " (Select EnvLotXML from LoteEletronico where IdLoteEletronico = LE.IdLoteEletronico) EnvLotXML ";
                ssql += " From LoteEletronico LE with (Nolock) ";
                ssql += " left Join DocumentoEletronico DE on (DE.IdLoteEletronico = LE.IdLoteEletronico) ";
                ssql += " left Join Documento Cte on (Cte.IDDocumento = DE.IdDocumento) ";
                ssql += " where LE.IdFilial =  " + idfilial;
                ssql += " and (le.CStatus in ('103','105','678') " + (resultadoEnvio == true ? " OR  LE.IdLoteEletronico =  " + idLoteEletronico : " OR  LE.IdLoteEletronico =0") + " )";
                ssql += " Group by LE.IdLoteEletronico, LE.LoteGerado,LE.LoteEnviadoAoSefaz, LE.Descricao, LE.Recibo, LE.CStatus, LE.Status, LE.NomeDaMaquina, LE.EnvLot ";
                ssql += " Order by  LE.IdLoteEletronico  desc";

            //}
            //else
            //{
            //    ssql += " Select ";
            //    ssql += " LE.IdLoteEletronico, LE.LoteGerado,LE.LoteEnviadoAoSefaz, LE.Descricao, LE.Recibo, LE.CStatus, LE.Status, ";
            //    ssql += " COUNT(*) Cte, LE.NomeDaMaquina, LE.EnvLot, ";
            //    ssql += " (Select EnvLotXML from LoteEletronico where IdLoteEletronico = LE.IdLoteEletronico) EnvLotXML ";
            //    ssql += " From LoteEletronico LE with (Nolock) ";
            //    ssql += " left Join DocumentoEletronico DE on (DE.IdLoteEletronico = LE.IdLoteEletronico) ";
            //    ssql += " left Join Documento Cte on (Cte.IDDocumento = DE.IdDocumento) ";
            //    ssql += " where LE.IdFilial =  " + idfilial;
            //    ssql += " and LE.IdLoteEletronico =  " + idLoteEletronico;
            //    ssql += " Group by LE.IdLoteEletronico, LE.LoteGerado,LE.LoteEnviadoAoSefaz, LE.Descricao, LE.Recibo, LE.CStatus, LE.Status, LE.NomeDaMaquina, LE.EnvLot ";
            //    ssql += " Order by  LE.IdLoteEletronico ";
            //}


            DataTable d = DAL.BD.cDb.RetornarDataTable(ssql, cnx);

            for (int i = 0; i < d.Rows.Count; i++)
            {
                if (d.Rows[i]["Descricao"].ToString().Length >= 15)
                {
                    d.Rows[i]["Descricao"] = d.Rows[i]["Descricao"].ToString().Substring(0, 14);
                }
            }

            return d;
        }

        public DataTable RetornarCtesEmLoteDetalhe(int idLoteEletronico, string cnx)
        {
            string ssql = "";
            ssql += " SELECT DE.STATUS, * ";
            ssql += " FROM LOTEELETRONICO LE WITH (NOLOCK) ";
            ssql += " LEFT JOIN DOCUMENTOELETRONICO DE ON (DE.IDLOTEELETRONICO = LE.IDLOTEELETRONICO) ";
            ssql += " LEFT JOIN DOCUMENTO CTE ON (CTE.IDDOCUMENTO = DE.IDDOCUMENTO) ";
            ssql += " WHERE  ";
            ssql += " le.CSTATUS IN ('103','104','105','678', '100') ";
            ssql += " AND LE.IDLOTEELETRONICO =  " + idLoteEletronico;
            ssql += " ORDER BY  CTE.NUMERO ";
            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);
        }

        public DataTable RetornarCteDisponivelDetalhe(int idfilial, int idcliente, string cnx)
        {
            string ssql = "";
            ssql += " SELECT DISTINCT  ";
            ssql += " CTE.IDDOCUMENTO,  ";
            ssql += " CTE.CIFFOB, Cte.Numero, ";
            ssql += " CTE.SERIE,  ";
            ssql += " CASE WHEN DOCE.IDDOCUMENTO > 0 THEN DOCE.STATUS ELSE 'CTE AGUARDANDO ENVIO' END STATUS,  ";
            ssql += " DOCE.NUMERORECIBO, ";
            ssql += " DOCE.NUMEROPROTOCOLO, ";
            ssql += " CTE.CHAVENFREFERENCIA ";
            ssql += " FROM DOCUMENTO CTE WITH (NOLOCK)  ";
            ssql += " INNER JOIN FILIAL FIL ON (CTE.IDFILIAL = FIL.IDFILIAL)  ";
            ssql += " LEFT JOIN DOCUMENTOELETRONICO DOCE ON (DOCE.IDDOCUMENTO = CTE.IDDOCUMENTO)  ";
            ssql += " WHERE ";
            ssql += " CTE.IDFILIAL = " + idfilial;
            ssql += " AND CTE.IDCLIENTE = " + idcliente;
            ssql += " AND CTE.TIPODEDOCUMENTO = 'CONHECIMENTO' AND CTE.ATIVO='SIM' AND CTE.ENVIADO='NAO'  ";
            ssql += " AND CTE.IDDOCUMENTO NOT IN  ";
            ssql += " (SELECT DOC.IDDOCUMENTO FROM DOCUMENTO DOC  ";
            ssql += " INNER JOIN DOCUMENTOELETRONICO DOCE ON (DOCE.IDDOCUMENTO = DOC.IDDOCUMENTO)  ";
            ssql += " WHERE DOC.IDFILIAL = " + idfilial;
            ssql += " AND DOC.IDCLIENTE = " + idcliente;
            ssql += " AND DOC.TIPODEDOCUMENTO = 'CONHECIMENTO'  ";
            ssql += " AND DOC.ATIVO='SIM'  ";
            ssql += " AND DOC.ENVIADO='NAO'  ";
            ssql += " AND DOCE.CSTATUS IN ('103','104','105','678') )  ";

            ssql += " ORDER BY CTE.NUMERO DESC ";
            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);
        }

        public DataTable RetornarCtesParaEnvio(int idfilial, int idcliente, List<int> idDocumentos, string cnx)
        {
            string ssql = "SELECT DISTINCT   ";
            ssql += " CTE.IDDOCUMENTO, CTE.IDCLIENTE, CTE.NUMERO, CTE.CIFFOB, CTE.SERIE, CTE.DATADEEMISSAO, CTE.VALORDANOTA, CTE.CHAVENFREFERENCIA, CTE.CLASSECFOP, CTE.TIPODESERVICO, CTE.CTETIPODESERVICO, CTE.CTETIPODECTE, CTE.CTEMODAL,   ";
            ssql += " CTE.NUMERODOCUMENTOELETRONICO,   ";
            ssql += " ( SELECT TOP 1 NF.NUMERO FROM DOCUMENTORELACIONADO DR   ";
            ssql += " INNER JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO)  WHERE DR.IDDOCUMENTOPAI = CTE.IDDOCUMENTO AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL'   ";
            ssql += " ) NUMERONF,   ";
            ssql += " ( SELECT TOP 1 NF.IDDOCUMENTO FROM DOCUMENTORELACIONADO DR   ";
            ssql += " INNER JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO)   ";
            ssql += " WHERE DR.IDDOCUMENTOPAI = CTE.IDDOCUMENTO AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL'   ";
            ssql += " ) IDDOCUMENTONF,   ";
            ssql += " ( SELECT TOP 1 NF.DOCUMENTODOCLIENTE4 FROM DOCUMENTORELACIONADO DR   ";
            ssql += " INNER JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO)   ";
            ssql += " WHERE DR.IDDOCUMENTOPAI = CTE.IDDOCUMENTO AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL'   ";
            ssql += " ) CHAVENF,   ";
            ssql += " 'NAO' SELECIONAR, CASE WHEN DOCE.IDDOCUMENTO > 0 THEN DOCE.STATUS ELSE 'CTE AGUARDANDO ENVIO' END STATUS,   ";
            ssql += " DOCE.CSTATUS, DOCE.IDDOCUMENTOELETRONICO, DOCE.IDLOTEELETRONICO,DOCE.IDNOTA,DOCE.NUMERORECIBO,DOCE.NUMEROPROTOCOLO,   ";
            ssql += " CTE.NATUREZA, CTE.ESPECIE, CTE.DATAPLANEJADA, CTE.PESOBRUTO, CTE.PESOLIQUIDO, CTE.PESOCUBADO, CTE.METRAGEMCUBICA, CTE.VOLUMES,   ";
            ssql += " CTE.DOCUMENTODOCLIENTE, CTE.DOCUMENTODOCLIENTE1,   ";
            ssql += " CTE.DATADEEMISSAO,   ";
            ssql += " DOCE.STATUSCOMPLETO,   ";
            ssql += " REM.CNPJCPF AS REMCNPJCPF,   ";
            ssql += " REM.INSCRICAORG REMIE,   ";
            ssql += " REM.FANTASIAAPELIDO AS REMFANTASIAAPELIDO,   ";
            ssql += " REM.RAZAOSOCIALNOME REMRAZAOSOCIAL,   ";
            ssql += " REM.ENDERECO REMENDERECO, REM.NUMERO REMNUMERO, REM.CEP REMCEP, REMBAI.NOME REMBAIRRO,   ";
            ssql += " REMCID.NOME REMCIDADE,REMCID.CODIGODOIBGE REMIBGE, REM.COMPLEMENTO REMCOMPLEMENTO,   ";
            ssql += " REMEST.NOME REMESTADO,REMEST.UF REMUF,REMEST.CODIGODOIBGE REMUFIBGE,   ";
            ssql += " REMPAIS.CODIGODOBACEN REMPAISCODIGO,   ";
            ssql += " REMPAIS.NOME REMPAIS,   ";
            ssql += " (SELECT TOP 1 ENDERECO FROM CADASTROCONTATOENDERECO   ";
            ssql += " WHERE IDCADASTROTIPODECONTATO=2   ";
            ssql += " AND IDCADASTRO = REM.IDCADASTRO) REMFONE,   ";

            ssql += " DES.CNPJCPF AS DESCNPJCPF,   ";
            ssql += " DES.INSCRICAORG DESIE,   ";
            ssql += " DES.FANTASIAAPELIDO AS DESFANTASIAAPELIDO,   ";
            ssql += " DES.RAZAOSOCIALNOME DESRAZAOSOCIAL, DES.COMPLEMENTO DESCOMPLEMENTO,   ";
            ssql += " DES.ENDERECO DESENDERECO, DES.NUMERO DESNUMERO, DES.CEP DESCEP, DESBAI.NOME DESBAIRRO,   ";
            ssql += " DESCID.NOME DESCIDADE,DESCID.CODIGODOIBGE DESIBGE,   ";
            ssql += " DESEST.NOME DESESTADO,DESEST.UF DESUF,DESEST.CODIGODOIBGE DESUFIBGE,   ";
            ssql += " DES.SUFRAMA DESSUFRAMA,   ";
            ssql += " DESPAIS.CODIGODOBACEN DESPAISCODIGO,   ";
            ssql += " DESPAIS.NOME DESPAIS,   ";

            ssql += " EMI.CNPJCPF AS EMICNPJCPF,   ";
            ssql += " EMI.INSCRICAORG EMIIE,   ";
            ssql += " EMI.FANTASIAAPELIDO AS EMIFANTASIAAPELIDO,   ";
            ssql += " EMI.RAZAOSOCIALNOME EMIRAZAOSOCIAL,   ";
            ssql += " EMI.ENDERECO EMIENDERECO, EMI.NUMERO EMINUMERO, EMI.CEP EMICEP, EMIBAI.NOME EMIBAIRRO,   ";
            ssql += " EMICID.NOME EMICIDADE,EMICID.CODIGODOIBGE EMIIBGE, EMI.COMPLEMENTO EMICOMPLEMENTO,   ";
            ssql += " EMIEST.NOME EMIESTADO,EMIEST.UF EMIUF,EMIEST.CODIGODOIBGE EMIUFIBGE,   ";
            ssql += " (SELECT TOP 1 ENDERECO FROM CADASTROCONTATOENDERECO   ";
            ssql += " WHERE IDCADASTROTIPODECONTATO=2   ";
            ssql += " AND IDCADASTRO = EMI.IDCADASTRO) EMIFONE,   ";

            ssql += " CLI.CNPJCPF AS CLICNPJCPF,   ";
            ssql += " CLI.INSCRICAORG CLIIE,   ";
            ssql += " CLI.FANTASIAAPELIDO AS CLIFANTASIAAPELIDO,   ";
            ssql += " CLI.RAZAOSOCIALNOME CLIRAZAOSOCIAL,   ";
            ssql += " CLI.ENDERECO CLIENDERECO, CLI.NUMERO CLINUMERO, CLI.CEP CLICEP, CLIBAI.NOME CLIBAIRRO,   ";
            ssql += " CLICID.NOME CLICIDADE,CLICID.CODIGODOIBGE CLIIBGE, CLI.COMPLEMENTO CLICOMPLEMENTO,   ";
            ssql += " CLIEST.NOME CLIESTADO,CLIEST.UF CLIUF,EMIEST.CODIGODOIBGE CLIUFIBGE,   ";
            ssql += " CLIPAIS.CODIGODOBACEN CLIPAISCODIGO,   ";
            ssql += " CLIPAIS.NOME CLIPAIS,   ";
            ssql += " (SELECT TOP 1 ENDERECO FROM CADASTROCONTATOENDERECO   ";
            ssql += " WHERE IDCADASTROTIPODECONTATO=2   ";
            ssql += " AND IDCADASTRO = CLI.IDCADASTRO) CLIFONE,  ";

            ssql += " ISNULL(RED.IDCADASTRO,0) AS REDIDCADASTRO,   ";
            ssql += " RED.CNPJCPF AS REDCNPJCPF,   ";
            ssql += " RED.INSCRICAORG REDIE,   ";
            ssql += " RED.FANTASIAAPELIDO AS REDFANTASIAAPELIDO,   ";
            ssql += " RED.RAZAOSOCIALNOME REDRAZAOSOCIAL,   ";
            ssql += " RED.ENDERECO REDENDERECO, RED.NUMERO REDNUMERO, RED.CEP REDCEP, REDBAI.NOME REDBAIRRO,   ";
            ssql += " REDCID.NOME REDCIDADE,REDCID.CODIGODOIBGE REDIBGE, RED.COMPLEMENTO REDCOMPLEMENTO,   ";
            ssql += " REDEST.NOME REDESTADO,REDEST.UF REDUF,EMIEST.CODIGODOIBGE REDUFIBGE,   ";
            ssql += " REDPAIS.CODIGODOBACEN REDPAISCODIGO,   ";
            ssql += " REDPAIS.NOME REDPAIS,   ";
            ssql += " (SELECT TOP 1 ENDERECO FROM CADASTROCONTATOENDERECO   ";
            ssql += " WHERE IDCADASTROTIPODECONTATO=2   ";
            ssql += " AND IDCADASTRO = RED.IDCADASTRO) REDFONE,   ";
            ssql += " 'NAO' CANCELAR   ";

            ssql += " FROM DOCUMENTO CTE WITH (NOLOCK)   ";
            ssql += " INNER JOIN FILIAL FIL ON (CTE.IDFILIAL = FIL.IDFILIAL)   ";
            ssql += " INNER JOIN CADASTRO REM ON (REM.IDCADASTRO=CTE.IDREMETENTE)   ";
            ssql += " LEFT  JOIN BAIRRO REMBAI ON (REMBAI.IDBAIRRO = REM.IDBAIRRO)   ";
            ssql += " INNER JOIN CIDADE REMCID ON (REMCID.IDCIDADE = REM.IDCIDADE)   ";
            ssql += " INNER JOIN ESTADO REMEST ON (REMEST.IDESTADO = REMCID.IDESTADO)   ";
            ssql += " INNER JOIN PAIS REMPAIS ON (REMPAIS.IDPAIS = REMEST.IDPAIS)   ";
            ssql += " INNER JOIN CADASTRO DES ON(DES.IDCADASTRO= CTE.IDDESTINATARIO)   ";
            ssql += " LEFT  JOIN BAIRRO DESBAI ON (DESBAI.IDBAIRRO = DES.IDBAIRRO)   ";
            ssql += " INNER JOIN CIDADE DESCID ON (DESCID.IDCIDADE = DES.IDCIDADE)   ";
            ssql += " INNER JOIN ESTADO DESEST ON (DESEST.IDESTADO = DESCID.IDESTADO)   ";
            ssql += " INNER JOIN PAIS DESPAIS ON (DESPAIS.IDPAIS = DESEST.IDPAIS)   ";

            ssql += " INNER JOIN CADASTRO EMI ON(EMI.IDCADASTRO= FIL.IDCADASTRO)   ";
            ssql += " LEFT  JOIN BAIRRO EMIBAI ON (EMIBAI.IDBAIRRO = EMI.IDBAIRRO)   ";
            ssql += " INNER JOIN CIDADE EMICID ON (EMICID.IDCIDADE = EMI.IDCIDADE)   ";
            ssql += " INNER JOIN ESTADO EMIEST ON (EMIEST.IDESTADO = EMICID.IDESTADO)   ";

            ssql += " INNER JOIN CADASTRO CLI ON(CLI.IDCADASTRO= CTE.IDCLIENTE)   ";
            ssql += " LEFT  JOIN BAIRRO CLIBAI ON (CLIBAI.IDBAIRRO = CLI.IDBAIRRO)   ";
            ssql += " INNER JOIN CIDADE CLICID ON (CLICID.IDCIDADE = CLI.IDCIDADE)   ";
            ssql += " INNER JOIN ESTADO CLIEST ON (CLIEST.IDESTADO = CLICID.IDESTADO)   ";
            ssql += " INNER JOIN PAIS CLIPAIS ON (CLIPAIS.IDPAIS = CLIEST.IDPAIS)   ";

            ssql += " LEFT JOIN CADASTRO RED ON(RED.IDCADASTRO= CTE.IDREDESPACHANTE)   ";
            ssql += " LEFT JOIN BAIRRO REDBAI ON (REDBAI.IDBAIRRO = RED.IDBAIRRO)   ";
            ssql += " LEFT JOIN CIDADE REDCID ON (REDCID.IDCIDADE = RED.IDCIDADE)   ";
            ssql += " LEFT JOIN ESTADO REDEST ON (REDEST.IDESTADO = REDCID.IDESTADO)   ";
            ssql += " LEFT JOIN PAIS REDPAIS ON (REDPAIS.IDPAIS = REDEST.IDPAIS)   ";
            ssql += " LEFT JOIN DOCUMENTOELETRONICO DOCE ON (DOCE.IDDOCUMENTO = CTE.IDDOCUMENTO)   ";
            ssql += " WHERE CTE.IDFILIAL =" + idfilial;

            ssql += " AND CTE.TIPODEDOCUMENTO = 'CONHECIMENTO' AND CTE.ATIVO='SIM'  ";

            ssql += " AND CTE.ENVIADO='NAO' ";
            
            /*
            ssql += " AND CTE.IDDOCUMENTO IN   ";            
            ssql += " (SELECT DOC.IDDOCUMENTO FROM DOCUMENTO DOC   ";
            ssql += " INNER JOIN DOCUMENTOELETRONICO DOCE ON (DOCE.IDDOCUMENTO = DOC.IDDOCUMENTO)   ";
            ssql += " WHERE DOC.IDFILIAL =" + idfilial;
            ssql += " AND DOC.TIPODEDOCUMENTO = 'CONHECIMENTO' AND DOC.ATIVO='SIM' AND DOC.ENVIADO='NAO'   ";
            ssql += " AND DOCE.CSTATUS IN ('100','103','104','105','678', '661') )   ";
            */

            string n = "";
            for (int i = 0; i < idDocumentos.Count; i++)
            {
                n += idDocumentos[i].ToString() + ",";
            }

            n = n.Substring(0, n.Length - 1);
            ssql += " AND CTE.IDDOCUMENTO IN(" + n + ")  ";         
            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);

        }

        //public void CriarLote(int idFilial, string Remetente, List<int> idDocumentos, string cnx)
        //{
        //    try
        //    {
        //        System.Text.StringBuilder sql = new StringBuilder();
        //        string id = DAL.BD.cDb.RetornarIDTabela(cnx, "LOTEELETRONICO").ToString();

        //        sql.Append("Insert into LOTEELETRONICO (IdLoteEletronico, Descricao, IdFilial) values (@IdLoteEletronico, '@Descricao', @IdFilial)");
        //        sql.Replace("@IdLoteEletronico", id);
        //        sql.Replace("@Descricao", Remetente);
        //        sql.Replace("@IdFilial", idFilial.ToString());
        //        sql.Append(" ; ");

        //        for (int i = 0; i < idDocumentos.Count; i++)
        //        {
        //            sql.Append("UPDATE DOCUMENTOELETRONICO SET idloteEletronico=" + id + " where iddocumento=" + idDocumentos[i].ToString() + "; ");
        //        }

        //        //DAL.BD.cDbExcutarMultiplosSql(sql.ToString(), cnx);

        //    }
        //    catch (Exception EX)
        //    {
        //        throw EX;
        //    }




        #endregion
        
        public class EletronicoParametro
        {
            public DataTable Retornar(int idFilial, string TipoEletronico, string cnx)
            {
                string sqlstr = "";
                sqlstr += " SELECT DEP.*, CADFILIAL.CNPJCPF, CADFILIAL.RAZAOSOCIALNOME, CADFILIAL.IDCADASTRO, CADFILIAL.IDCIDADE ";
                sqlstr += " FROM DOCUMENTOELETRONICOPARAMETRO DEP";
                sqlstr += " INNER JOIN FILIAL F ON F.IDFILIAL = DEP.IDFILIAL";
                sqlstr += " INNER JOIN CADASTRO CADFILIAL ON  CADFILIAL.IDCADASTRO = F.IDCADASTRO";
                sqlstr += " WHERE F.IDFILIAL =" + idFilial + " AND TIPOELETRONICO='" + TipoEletronico + "'";

                return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
            }
        }

        public class Frete
        {
            public DAL.Models.DocumentoFrete Retornar(int iddocumento, string cnx)
            {
                SistecnoContext context = new SistecnoContext();
                context.Database.Connection.ConnectionString = cnx;
                context.Database.Connection.Open();
                var query = context.DocumentoFretes.FirstOrDefault(p => p.IDDocumento == iddocumento);


                context.Database.Connection.Close();
                return query;
            }
        }
        public static class Numerador
        {
            public static string RetornarNumerador(int idempresa, int idfilial, string ChaveNome, string serie, string cnx)
            {
                string sqlstr = "SELECT * FROM NUMERADOR WHERE IDFILIAL="+idfilial+" AND NOME='"+ChaveNome+"'";
                DataTable dt =  DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);

                string id = "";
                if (dt.Rows.Count > 0)
                {
                    id = dt.Rows[0]["IDNUMERADOR"].ToString();
                }

                if (id == "")
                {
                    id = DAL.BD.cDb.RetornarIDTabela(cnx, "NUMERADOR").ToString();
                    sqlstr = "INSERT INTO NUMERADOR VALUES (" + id + "," + idempresa + "," + idfilial + ",'" + ChaveNome + "', '" + serie + "', 1); SELECT PROXIMONUMERO FROM NUMERADOR WHERE IDNUMERADOR =  " + id;

                }
                else
                {
                    sqlstr = "UPDATE NUMERADOR SET PROXIMONUMERO=PROXIMONUMERO+1 WHERE IDNUMERADOR =  " + id + "; SELECT PROXIMONUMERO FROM NUMERADOR WHERE IDNUMERADOR =  " + id;

                }

                return DAL.BD.cDb.ExecutarRetornoIDs(sqlstr, cnx);

            }

        }
    }
}

