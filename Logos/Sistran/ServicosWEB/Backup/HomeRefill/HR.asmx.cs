using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ComponentModel;

namespace ServicosWEB.HomeRefill
{
    /// <summary>
    /// Summary description for HR
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class HR : System.Web.Services.WebService
    {
        [WebMethod]
        public DataTable Entradas(string Login, string Senha, string DataInicial, string DataFinal)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            int idLog = 0;
            string Err = "";

            try
            {
                idLog = Sistran.Library.GetDataTables.LogMetodo(null, "Entradas", null, null, "", cnx);

                string sql = "";
                sql = "SELECT * FROM USUARIO WHERE LOGIN='" + Login + "' AND SENHA='" + Senha + "' AND ATIVO='SIM'";
                DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);


                //if (d.Rows.Count == 0)
                //    throw new Exception("USUARIO OU SENHA INVALIDOS");

                sql = " SELECT    MI.IDMOVIMENTACAOITEM, DOC.TIPODEDOCUMENTO, DOC.NUMERO, DOC.DATADEEMISSAO, F.CNPJCPF CNPJ, F.RAZAOSOCIALNOME FORNECEDOR, PC.CODIGO, P.CODIGODEBARRAS,  LT.VALIDADE, LT.VALORUNITARIO, MI.QUANTIDADE ";
                sql += " FROM MOVIMENTACAO M WITH (NOLOCK) ";
                sql += "   INNER JOIN MOVIMENTACAOITEM MI ON MI.IDMOVIMENTACAO = M.IDMOVIMENTACAO ";
                sql += " INNER JOIN DOCUMENTO DOC ON DOC.IDDOCUMENTO = MI.IDDOCUMENTO ";
                sql += " INNER JOIN CADASTRO F ON F.IDCADASTRO = DOC.IDREMETENTE ";
                sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDUNIDADEDEARMAZENAGEMLOTE=MI.IDUNIDADEDEARMAZENAGEMLOTE ";
                sql += " INNER JOIN LOTE LT ON LT.IDLOTE = UAL.IDLOTE ";
                sql += " INNER JOIN MAPA MP ON MP.IDMAPA = MI.IDMAPA ";
                sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = MI.IDPRODUTOEMBALAGEM ";
                sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
                sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO=PE.IDPRODUTO ";
                sql += " WHERE    M.IDFILIAL= 43  ";
                sql += " AND M.TIPO ='ENTRADA'  ";
                sql += " AND M.MOTIVO='ARMAZENAGEM'  ";
                sql += " AND M.ATIVO='SIM'  ";

                if (DataInicial == "" || DataFinal == "")
                    sql += " AND  (MI.OBS  NOT LIKE '%ENVIADO%'  OR MI.OBS IS NULL)  AND  M.DATADECADASTRO >= '2015-03-10'  ";
                else
                    sql += " AND DOC.DATADEENTRADA>=  CONVERT(DATETIME, '" + DataInicial + "',120)  AND  DOC.DATADEENTRADA <=  CONVERT(DATETIME, '" + DataFinal + "',120) ";
                    //sql += " AND CONVERT(DATETIME, M.DATADECADASTRO, 103) >=  CONVERT(DATETIME, '" + DataInicial + "',120)  AND M.DATADECADASTRO <=  CONVERT(DATETIME, '" + DataFinal + "',120) ";


                sql += " AND DOC.IDCLIENTE = 150000 ";
                sql += " AND NOT MI.DATADEEXECUCAO IS NULL  ";

                d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                sql = "";
                if (DataInicial == "" || DataFinal == "")
                {

                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        sql += " update movimentacaoItem set obs='ENVIADO HOMEREFEILL " + DateTime.Now.ToShortDateString() + "' WHERE IDMOVIMENTACAOITEM=" + d.Rows[i][0].ToString() + " ;";
                    }

                    if (sql != "")
                        Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, cnx);
                }
                d.Columns.Remove("IDMOVIMENTACAOITEM");

                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - Entradas ", "SQL:" + sql , "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception)
                { }


                return d;
            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - Entradas ", "Ex:" + ex.Message, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                throw ex;
            }
            finally
            {
                idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "Entradas", null, null, "sql - Finalizou -" + Err, cnx);
            }
        }

        [WebMethod]
        public DataTable SaldoAtual(string Login, string Senha)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            int idLog = 0;
            string Err = "";

            try
            {

                idLog = Sistran.Library.GetDataTables.LogMetodo(null, "SaldoAtual", null, null, "EXEC PRC_SALDO_ATUAL", cnx);

                string sql = "";
                sql = "SELECT * FROM USUARIO WHERE LOGIN='" + Login + "' AND SENHA='" + Senha + "' AND ATIVO='SIM'";
                DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                //if (d.Rows.Count == 0)
                //    throw new Exception("USUARIO OU SENHA INVALIDOS");

                sql = "EXEC PRC_SALDO_ATUAL";
                d =  Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);


                for (int i = 0; i < d.Rows.Count; i++)
                {
                    d.Rows[i]["SALDOATUAL"] = Math.Abs(float.Parse( d.Rows[i]["SALDOESTOQUE"].ToString()) - float.Parse( d.Rows[i]["EMPENHADOCOMSEPARACAO"].ToString()) -  float.Parse(d.Rows[i]["EMPENHADOCOMPEDIDOS"].ToString()));
                }

                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - SaldoAtual ", "SQL:" + sql, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception)
                { }


                return d;
            }
            catch (Exception ex)
            {

                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - SaldoAtual ", "Ex:" + ex.Message, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                throw ex;
            }
            finally
            {
                idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "SaldoAtual", null, null, "EXEC PRC_SALDO_ATUAL - Finalizou -" + Err, cnx);
            }
        }

        [WebMethod]
        public string CancelarPedidos(string Login, string Senha, PedidoCancelado pedidoCancelar, string prod_hom)
        {
            string sqlFinal = "";
            string sql = "";
            string obj = "";
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            int idLog = 0;
            string Err = "";

            idLog = Sistran.Library.GetDataTables.LogMetodo(null, "CancelarPedidos", null, null, "Iniciou", cnx);



            obj += "Cliente_CNPJ>" + pedidoCancelar.Cliente_CNPJ + "</Cliente_CNPJ><BR>";
            obj += "NumeroDocumento>" + pedidoCancelar.NumeroDocumento + "</NumeroDocumento><BR>";
            obj += "Serie>" + pedidoCancelar.Serie + "</Serie><BR>";
            obj += "Dest_CNPJCPF>" + pedidoCancelar.Dest_CNPJCPF + "</Dest_CNPJCPF><BR>";
            obj += "CompraVenda>" + pedidoCancelar.CompraVenda + "</CompraVenda><BR>";
            obj += "prod_hom>" + prod_hom + "</prod_hom><BR>";

            try
            {
                
                sql = "SELECT CL.IDCLIENTE, CL.IDFILIALPADRAOINTERNET FROM CADASTRO C INNER JOIN CLIENTE CL ON CL.IDCLIENTE = C.IDCADASTRO WHERE CNPJCPF='" + FormatarCnpj(pedidoCancelar.Cliente_CNPJ) + "' ";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                string entradadsaida = "";
                if (pedidoCancelar.CompraVenda.ToUpper() == "VENDA")
                    entradadsaida = "SAIDA";
                else
                    entradadsaida = "ENTRADA";

                sql = "SELECT CNPJCPF,*  FROM DOCUMENTO D ";

                if (entradadsaida == "SAIDA")
                    sql += "INNER JOIN Cadastro CD ON CD.IDCADASTRO = D.IDDESTINATARIO ";
                else
                    sql += "INNER JOIN Cadastro CD ON CD.IDCADASTRO = D.IDREMETENTE ";


                sql += " WHERE IDCLIENTE=  " + dt.Rows[0]["IdCliente"].ToString();
                sql += " AND D.NUMERO ='" + pedidoCancelar.NumeroDocumento + "' ";
                sql += " AND TIPODEDOCUMENTO='PEDIDO' ";
                sql += " AND SERIE = '" + pedidoCancelar.Serie + "' ";
                sql += " AND ENTRADASAIDA = '" + entradadsaida + "'";
                sql += " AND ORIGEM='WEBSERVICE'";
                //sql += " AND ATIVO='SIM'";
                //sql += " AND DATADECANCELAMENTO IS NULL ";
                sql += " AND CNPJCPF = '" + FormatarCnpj(pedidoCancelar.Dest_CNPJCPF) + "'";
                sql += " ORDER BY D.ATIVO DESC";

                DataTable dtPed = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);


                string sqlt = sql.Replace("*  FROM DOCUMENTO D", "*  FROM DOCUMENTOPEDIDO D");                
                DataTable dtPedTemp = Sistran.Library.GetDataTables.RetornarDataTableWS(sqlt, cnx);

                DataRow[] o = dtPedTemp.Select("DataDeCancelamento is null", "");
                for (int ii = 0; ii <o.Length; ii++)
                {
                    sql = "UPDATE DOCUMENTOPEDIDO SET DATADECANCELAMENTO=GETDATE(), DATADOPROCESSAMENTO=GETDATE() WHERE IDDOCUMENTO=" + o[ii]["IdDocumento"].ToString();
                    DataTable dtPedTemp2 = Sistran.Library.GetDataTables.RetornarDataTableWS(sql + "; select 1", cnx);
                }

                if (dtPed.Rows.Count == 0)
                {

                    if (dtPedTemp.Rows.Count > 0)
                    {
                       
                        throw new Exception("PEDIDO CANCELADO T");
                    }

                    
                    throw new Exception("PEDIDO NÃO ENCONTRADO");
                }


                if (dtPed.Rows[0]["DATADECANCELAMENTO"].ToString() != "")
                    throw new Exception("PEDIDO JÁ CANCELADO NA BASE DE DADOS");

                //verifica se o pedido esta em romaneio
                sql = "SELECT COUNT(*) QTD FROM ROMANEIODOCUMENTO WHERE IDDOCUMENTO=" + dtPed.Rows[0]["IDDOCUMENTO"].ToString();
                DataTable dtAux = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (int.Parse(dtAux.Rows[0][0].ToString()) > 0)
                    throw new Exception("NÃO FOI POSSIVEL CANCELAR O PEDIDO. PEDIDO EM PROCESSAMENTO");


                sqlFinal += "UPDATE DOCUMENTOITEM SET SALDO=0, ESTOQUEPROCESSADO='SIM' WHERE IDDOCUMENTO=" + dtPed.Rows[0]["IDDOCUMENTO"].ToString() + "; ";
                sqlFinal += "UPDATE DOCUMENTOFILIAL SET SITUACAO='PEDIDO CANCELADO', DATA=GETDATE() WHERE IDDOCUMENTO = " + dtPed.Rows[0]["IDDOCUMENTO"].ToString() + "; ";
                sqlFinal += "UPDATE DOCUMENTO SET DATADECANCELAMENTO=GETDATE(), ATIVO='NAO' WHERE IDDOCUMENTO=" + dtPed.Rows[0]["IDDOCUMENTO"].ToString() + "; ";

                string iddoc = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIA", cnx);

                sqlFinal += "INSERT INTO DOCUMENTOOCORRENCIA (IDDOCUMENTOOCORRENCIA, IDDOCUMENTO, IDFILIAL, DATAOCORRENCIA, DATALANCAMENTO, DESCRICAO)";
                sqlFinal += "VALUES (" + iddoc + ", " + dtPed.Rows[0]["IDDOCUMENTO"].ToString() + ", " + dtPed.Rows[0]["IDFILIALATUAL"].ToString() + ", GETDATE(), GETDATE(), 'PEDIDO CANCELADO PELO WEB SERVICE')";

                sqlFinal += "; SELECT IDDOCUMENTO FROM  DOCUMENTO WHERE IDDOCUMENTO=" + dtPed.Rows[0]["IDDOCUMENTO"].ToString() + "; ";

                dtPed = Sistran.Library.GetDataTables.RetornarDataTableWS(sqlFinal, cnx);

                obj += "<br>IDDOCUMENTO=" + dtPed.Rows[0]["IDDOCUMENTO"].ToString();

                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - PEDIDOS HOME REFILL - Cancelar Pedidos ", "obj:" + obj + "<br> " + "SQL:" + sql + " ||| SQLFINAL: " + sqlFinal, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception)
                { }

                return "Pedido Cancelado com Sucesso";
            }
            catch (Exception ex)
            {
                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "PEDIDOS HOME REFILL - Cancelamento", "obj:" + obj + "<br> " + ex.Message + " |||| SQL:" + sql + " ||| SQLFINAL: " + sqlFinal, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception)
                { }

                return "Não foi possivel cancelar o pedido. - " + ex.Message;
            }

            finally
            {
                idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "CancelarPedidos", null, null, "Finalizou -" + Err, cnx);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="prod_hom"></param>
        [WebMethod]
        public string ReceberPedidos(string Login, string Senha, List<Pedido> pedido, string prod_hom)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            int idLog = 0;
            string Err = "";

            idLog = Sistran.Library.GetDataTables.LogMetodo(null, "ReceberPedidos", null, null, "Iniciou", cnx);

            string ssqlFinal = "";
            string sql = "";

            string obj = "";

            foreach (var p in pedido)
            {

                obj += " Cliente_CNPJ:" + p.Cliente_CNPJ + "</Cliente_CNPJ<BR>";
                obj += " TipoDeDocumento:" + p.TipoDeDocumento + "</TipoDeDocumento<BR>";
                obj += " DataDeEmissao:" + p.DataDeEmissao.ToString() + "</DataDeEmissao<BR>";
                obj += " NumeroDocumento:" + p.NumeroDocumento + "</NumeroDocumento<BR>";
                obj += " Serie:" + p.Serie + "</Serie<BR>";
                obj += " Dest_CNPJCPF:" + p.Dest_CNPJCPF + "</Dest_CNPJCPF<BR>";
                obj += " Dest_IERG:" + p.Dest_IERG + "</Dest_IERG<BR>";
                obj += " Dest_RAZAOSOCIAL:" + p.Dest_RAZAOSOCIAL + "</Dest_RAZAOSOCIAL<BR>";
                obj += " Dest_FANTASIA:" + p.Dest_FANTASIA + "</Dest_FANTASIA<BR>";
                obj += " Dest_ENDERECO:" + p.Dest_ENDERECO + "</Dest_ENDERECO<BR>";
                obj += " Dest_NUMERO:" + p.Dest_NUMERO + "</Dest_NUMERO<BR>";
                obj += " Dest_COMPLEMENTO:" + p.Dest_COMPLEMENTO + "</Dest_COMPLEMENTO<BR>";
                obj += " Dest_BAIRRO:" + p.Dest_BAIRRO + "</Dest_BAIRRO<BR>";
                obj += " Dest_COD_IBGE_CIDADE:" + p.Dest_COD_IBGE_CIDADE + "</Dest_COD_IBGE_CIDADE<BR>";
                obj += " Dest_CIDADE_NOME:" + p.Dest_CIDADE_NOME + "</Dest_CIDADE_NOME<BR>";
                obj += " Dest_CEP:" + p.Dest_CEP + "</Dest_CEP<BR>";
                obj += " Dest_ENTREGA_CEP:" + p.Dest_ENTREGA_CEP + "</Dest_ENTREGA_CEP<BR>";
                obj += " Dest_ENTREGA_ENDERECO:" + p.Dest_ENTREGA_ENDERECO + "</Dest_ENTREGA_ENDERECO<BR>";
                obj += " Dest_ENTREGA_NUMERO:" + p.Dest_ENTREGA_NUMERO + "</Dest_ENTREGA_NUMERO<BR>";
                obj += " Dest_ENTREGA_COMPLEMENTO:" + p.Dest_ENTREGA_COMPLEMENTO + "</Dest_ENTREGA_COMPLEMENTO<BR>";
                obj += " Dest_ENTREGA_BAIRRO:" + p.Dest_ENTREGA_BAIRRO + "</Dest_ENTREGA_BAIRRO<BR>";
                obj += " Dest_ENTREGA_COD_IBGE_CIDADE:" + p.Dest_ENTREGA_COD_IBGE_CIDADE + "</Dest_ENTREGA_COD_IBGE_CIDADE<BR>";
                obj += "Ambiente: " + prod_hom;
            }

            try
            {

                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                for (int i = 0; i < pedido.Count; i++)
                {
                    string tipo = "";

                    if (pedido[i].CompraVenda.ToUpper() == "VENDA")
                        tipo = "'SAIDA'";
                    else
                        tipo = "'ENTRADA'";


                    sql = "SELECT CL.IDCLIENTE, CL.IDFILIALPADRAOINTERNET FROM CADASTRO C INNER JOIN CLIENTE CL ON CL.IDCLIENTE = C.IDCADASTRO WHERE CNPJCPF='" + FormatarCnpj(pedido[i].Cliente_CNPJ) + "' ";
                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                    if (dt.Rows.Count == 0)
                        throw new Exception("Cliente Não Localizado");

                    sql = " SELECT count(*)  FROM DOCUMENTO WHERE IDCLIENTE=" + dt.Rows[0]["idcliente"].ToString() + " AND NUMERO ='" + pedido[i].NumeroDocumento + "' AND TIPODEDOCUMENTO='PEDIDO' AND SERIE = '" + pedido[i].Serie + "' AND ENTRADASAIDA = " + tipo + " AND ATIVO='SIM' AND DATADECANCELAMENTO IS NULL ";

                    if (Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx).Rows[0][0].ToString() != "0")
                        continue;

                    pedido[i].Dest_CNPJCPF = FormatarCnpj(pedido[i].Dest_CNPJCPF);


                    string iddoc = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOPEDIDO", cnx);

                    obj += "<br>IDDOCUMENTOPEDIDO" + iddoc;

                    if (dt.Rows.Count == 0)
                        throw new Exception("Cliente não cadastrado.");

                    if (i > 0)
                        ssqlFinal += ";";

                    ssqlFinal += " INSERT INTO DOCUMENTOPEDIDO (IDDOCUMENTO, IDCLIENTE, IDREMETENTE, IDFILIAL, IDFILIALATUAL,  NUMERO, SERIE, TIPODEDOCUMENTO, IDDESTINATARIO,  ORIGEM, DATADEEMISSAO, DATADEENTRADA, ENDERECO, ENDERECONUMERO, ENDERECOCOMPLEMENTO, IDENDERECOBAIRRO, IDENDERECOCIDADE, ENDERECOCEP, ANOMES, DATADECANCELAMENTO, ENTRADASAIDA, ATIVO, TIPODESERVICO) VALUES";
                    ssqlFinal += " (@IDDOCUMENTO, @IDCLIENTE, @IDREMETENTE, @IDFILIAL@, @IDFILIALATUAL@,  '@NUMERO', '@SERIE', '@TIPODEDOCUMENTO', @IDDESTINATARIO,  'WEBSERVICE', '@DATADEEMISSAO', GETDATE(),  '@ENDERECO_ENTREGA@', '@ENDERECONUMERO_ENTREGA@', '@ENDERECOCOMPLEMENTO_ENTREGA@', @IDENDERECOBAIRRO_ENTREGA@, @IDENDERECOCIDADE_ENTREGA@, '@ENDERECOCEP_ENTREGA@', '@ANOMES', @DATADECANCELAMENTO@, @ENTRADASAIDA@, '" + (prod_hom.ToUpper() == "PRODUCAO" ? "SIM" : "NAO") + "', 'NORMAL') ";
                    ssqlFinal = ssqlFinal.Replace("@IDDOCUMENTO", iddoc);

                    ssqlFinal = ssqlFinal.Replace("@IDFILIAL@", dt.Rows[0]["IDFILIALPADRAOINTERNET"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@IDFILIALATUAL@", dt.Rows[0]["IDFILIALPADRAOINTERNET"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@NUMERO", pedido[i].NumeroDocumento);
                    ssqlFinal = ssqlFinal.Replace("@SERIE", pedido[i].Serie);
                    ssqlFinal = ssqlFinal.Replace("@TIPODEDOCUMENTO", pedido[i].TipoDeDocumento);
                    ssqlFinal = ssqlFinal.Replace("@DATADEEMISSAO", DateTime.Parse(pedido[i].DataDeEmissao.ToString()).ToString("yyyy-MM-dd"));
                    ssqlFinal = ssqlFinal.Replace("@DATADECANCELAMENTO@", (prod_hom.ToUpper() == "PRODUCAO" ? "NULL" : "GetDate()"));
                    ssqlFinal = ssqlFinal.Replace("@ANOMES", DateTime.Now.Year.ToString() + "/" + DateTime.Now.ToString("MM"));
                    ssqlFinal = ssqlFinal.Replace("@IDCLIENTE", dt.Rows[0]["IDCLIENTE"].ToString());

                    ssqlFinal = ssqlFinal.Replace("@ENDERECO_ENTREGA@", pedido[i].Dest_ENTREGA_ENDERECO);
                    ssqlFinal = ssqlFinal.Replace("@ENDERECONUMERO_ENTREGA@", pedido[i].Dest_ENTREGA_NUMERO);
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCOMPLEMENTO_ENTREGA@", pedido[i].Dest_ENTREGA_COMPLEMENTO);


                    sql = "select idcidade from cidade where CodigoDoIBGE=" + pedido[i].Dest_ENTREGA_COD_IBGE_CIDADE;
                    DataTable Cid = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                    string cid = "NULL";

                    if (Cid.Rows.Count > 0)
                        cid = Cid.Rows[0][0].ToString();


                    ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE_ENTREGA@", cid);
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP_ENTREGA@", pedido[i].Dest_ENTREGA_CEP);


                    sql = "SELECT * FROM BAIRRO WHERE NOME='" + pedido[i].Dest_ENTREGA_BAIRRO + "' AND IDCIDADE = " + "(select idcidade from cidade where CodigoDoIBGE=" + pedido[i].Dest_ENTREGA_COD_IBGE_CIDADE + ")";
                    DataTable Bai = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                    if (Bai.Rows.Count == 0)
                    {
                        string idb = Sistran.Library.GetDataTables.RetornarIdTabela("BAIRRO", cnx);
                        sql = "INSERT INTO BAIRRO (IDBairro, IDCidade, Nome) VALUES (" + idb + "," + cid + ", '" + pedido[i].Dest_ENTREGA_BAIRRO.ToUpper().Replace("'", "") + "' ); select 1";
                        Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", idb);
                    }
                    else
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", Bai.Rows[0]["IdBairro"].ToString());


                    if (pedido[i].CompraVenda.ToUpper() == "VENDA")
                    {
                        ssqlFinal = ssqlFinal.Replace("@ENTRADASAIDA@", "'SAIDA'");
                        ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", dt.Rows[0]["IDCLIENTE"].ToString());
                    }
                    else
                        ssqlFinal = ssqlFinal.Replace("@ENTRADASAIDA@", "'ENTRADA'");



                    #region Dados do Destinatario
                    sql = "SELECT C.IDCADASTRO, IDCIDADE, IDBAIRRO FROM CADASTRO C WHERE C.CNPJCPF='" + FormatarCnpj(pedido[i].Dest_CNPJCPF) + "' ";
                    DataTable dtDest = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                    if (dtDest.Rows.Count > 0)
                    {
                        if (dtDest.Rows[0]["IDCIDADE"].ToString() == "")
                        {
                            dtDest.Rows[0]["IDCIDADE"] = int.Parse(Sistran.Library.GetDataTables.ExecutarRetornoIDs("SELECT IDCIDADE FROM CIDADE WHERE CODIGODOIBGE=" + pedido[i].Dest_COD_IBGE_CIDADE, cnx));
                        }
                    }

                    //cadastra o destinario

                    //verifica se pega o IdCdastro Zerado
                    if (dtDest.Rows.Count > 0)
                    {
                        if (dtDest.Rows[0]["IDCADASTRO"].ToString() == "0")
                        {
                            dtDest.Rows.Clear();
                        }
                    }



                    string idDest = "";
                    if (dtDest.Rows.Count == 0)
                    {
                        idDest = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO", cnx);
                        sql = "INSERT INTO CADASTRO (IDCADASTRO,CNPJCPF, INSCRICAORG, RAZAOSOCIALNOME,FANTASIAAPELIDO,ENDERECO,NUMERO,COMPLEMENTO,IDCIDADE,IDBAIRRO,CEP) ";
                        sql += " VALUES(@IDCADASTRO,'@CNPJCPF', '@INSCRICAORG', '@RAZAOSOCIALNOME','@FANTASIAAPELIDO','@ENDERECO','@NUMERO', '@COMPLEMENTO',@IDCIDADE,@IDBAIRRO,'@CEP')";

                        sql = sql.Replace("@IDCADASTRO", idDest);
                        sql = sql.Replace("@CNPJCPF", FormatarCnpj(pedido[i].Dest_CNPJCPF));
                        sql = sql.Replace("@INSCRICAORG", pedido[i].Dest_IERG);
                        sql = sql.Replace("@RAZAOSOCIALNOME", pedido[i].Dest_RAZAOSOCIAL);

                        if (pedido[i].Dest_FANTASIA.Length > 30)
                            pedido[i].Dest_FANTASIA = pedido[i].Dest_FANTASIA.Substring(0, 29);

                        sql = sql.Replace("@FANTASIAAPELIDO", pedido[i].Dest_FANTASIA);
                        sql = sql.Replace("@ENDERECO", pedido[i].Dest_ENDERECO);
                        sql = sql.Replace("@NUMERO", pedido[i].Dest_NUMERO);
                        sql = sql.Replace("@COMPLEMENTO", pedido[i].Dest_COMPLEMENTO);

                        string idCidade = Sistran.Library.GetDataTables.ExecutarRetornoIDs("SELECT IDCIDADE FROM CIDADE WHERE CODIGODOIBGE=" + pedido[i].Dest_COD_IBGE_CIDADE, cnx);
                        DataTable Bairro = Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT * FROM BAIRRO WHERE NOME='" + pedido[i].Dest_BAIRRO + "' AND IDCIDADE = " + idCidade, cnx);



                        if (Bairro.Rows.Count == 0)
                        {

                            string idb2 = Sistran.Library.GetDataTables.RetornarIdTabela("BAIRRO", cnx);
                            string sqlAUX = "INSERT INTO BAIRRO (IDBairro, IDCidade, Nome) VALUES (" + idb2 + "," + idCidade + ", '" + pedido[i].Dest_BAIRRO.ToUpper().Replace("'", "") + "' ); select 1";
                            Sistran.Library.GetDataTables.RetornarDataTableWS(sqlAUX, cnx);

                            sql = sql.Replace("@IDBAIRRO", idb2);
                            ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO@", idb2);

                        }
                        else
                        {
                            sql = sql.Replace("@IDBAIRRO", Bairro.Rows[0]["IDBAIRRO"].ToString());
                            ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO@", Bairro.Rows[0]["IDBAIRRO"].ToString());
                        }

                        /////////////////////////////////////////////////////////////////////////////
                        sql = sql.Replace("@IDCIDADE", idCidade);
                        sql = sql.Replace("@CEP", pedido[i].Dest_CEP.Replace("-", ""));
                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, cnx);
                        /////////////////////////////////////////////////////////////////////////////

                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE@", idCidade);
                        //ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", idDest);


                        if (pedido[i].CompraVenda.ToUpper() == "VENDA")
                        {
                            ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", idDest);
                            ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", dt.Rows[0]["IDCLIENTE"].ToString());
                        }
                        else
                        {
                            ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", dt.Rows[0]["IDCLIENTE"].ToString());
                            ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", idDest);
                        }

                    }
                    else
                    {
                        idDest = dtDest.Rows[0]["IdCadastro"].ToString();
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO@", (dtDest.Rows[0]["IDBAIRRO"].ToString() == "" ? "NULL" : dtDest.Rows[0]["IDBAIRRO"].ToString()));
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE@", dtDest.Rows[0]["IDCIDADE"].ToString());

                        if (pedido[i].CompraVenda.ToUpper() == "VENDA")
                        {
                            ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", idDest);
                            ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", dt.Rows[0]["IDCLIENTE"].ToString());
                        }
                        else
                        {
                            ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", dt.Rows[0]["IDCLIENTE"].ToString());
                            ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", idDest);
                        }


                    }

                    #endregion

                    ssqlFinal = ssqlFinal.Replace("@ENDERECO@", pedido[i].Dest_ENDERECO);
                    ssqlFinal = ssqlFinal.Replace("@ENDERECONUMERO@", pedido[i].Dest_NUMERO);
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCOMPLEMENTO@", pedido[i].Dest_COMPLEMENTO);
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP@", pedido[i].Dest_CEP);

                    List<Pedido.Itens> item = pedido[i].itens;

                    if (item == null || item.Count == 0)
                        throw new Exception("Não foi possível receber o pedido. Motivo: Falta de itens.");


                    obj += "<br><b>ITENS</b><br>";
                    for (int iped = 0; iped < item.Count; iped++)
                    {
                        #region Verificações
                        sql = "SELECT * FROM PRODUTOCLIENTE PC WHERE IDCLIENTE= " + dt.Rows[0]["IDCLIENTE"].ToString() + " AND CODIGO = '" + item[iped].SKU.Trim() + "'";
                        DataTable dProdCli = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                        string IdprodCli = "";
                        if (dProdCli.Rows.Count == 0)
                        {

                            try
                            {
                                obj += "INSERE <BR> COD. PRODUTO: " + item[iped].SKU.ToUpper().Trim() + "<BR>";
                                obj += "DESC. PRODUTO: " + item[iped].Descricao.ToUpper().Trim() + "<BR>";
                                obj += "NCM. PRODUTO: " + item[iped].CodigoNCM.ToUpper().Trim() + "<BR>";
                                obj += "CB. PRODUTO: " + item[iped].EAN.ToUpper().Trim() + "<BR>";
                                obj += "PESO LIQ. PRODUTO: " + item[iped].PesoLiquido.ToString().ToUpper().Trim() + "<BR>";
                                obj += "QTD. PRODUTO: " + item[iped].Quantidade.ToString().ToUpper().Trim() + "<BR>";
                                obj += "VL. UNIT. PRODUTO: " + item[iped].ValorUnitario.ToString().ToUpper().Trim() + "<BR>";
                            }
                            catch (Exception ex)
                            {
                                obj += "Erro na montagem do email. " + ex.Message;
                            }

                            IdprodCli = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOCLIENTE", cnx);

                            if (item[iped].Descricao.Trim().ToUpper().Length > 60)
                                item[iped].Descricao = item[iped].Descricao.Substring(0, 59);

                            ssqlFinal += "; INSERT INTO PRODUTOCLIENTE (IDProdutoCliente, IDCliente, IDUnidadeDeMedida,Codigo, Descricao,MetodoDeMovimentacao, DesmembraNaNF, SolicitarDataDeValidade,UnidadeDoFornecedor,Ativo, CodigoNCM, FatorUsoPosicaoPallet) ";
                            ssqlFinal += " VALUES(" + IdprodCli + ", " + dt.Rows[0]["IDCLIENTE"].ToString() + ", 1,'" + item[iped].SKU.ToUpper().Trim() + "', '" + item[iped].Descricao.Trim().ToUpper().Replace("'", "") + "','FEFO', 'NAO', 'SIM',1.00,'SIM', '" + item[iped].CodigoNCM + "', 1)";
                        }
                        else
                        {

                            try
                            {
                                obj += "ALTERA <BR> COD. PRODUTO: " + item[iped].SKU.ToUpper().Trim() + "<BR>";
                                obj += "DESC. PRODUTO: " + item[iped].Descricao.ToUpper().Trim() + "<BR>";
                                obj += "NCM. PRODUTO: " + item[iped].CodigoNCM.ToUpper().Trim() + "<BR>";
                                obj += "CB. PRODUTO: " + item[iped].EAN.ToUpper().Trim() + "<BR>";
                                obj += "PESO LIQ. PRODUTO: " + item[iped].PesoLiquido.ToString().ToUpper().Trim() + "<BR>";
                                obj += "QTD. PRODUTO: " + item[iped].Quantidade.ToString().ToUpper().Trim() + "<BR>";
                                obj += "VL. UNIT. PRODUTO: " + item[iped].ValorUnitario.ToString().ToUpper().Trim() + "<BR>";
                            }
                            catch (Exception ex)
                            {
                                obj += "Erro na montagem do email. " + ex.Message;
                            }


                            IdprodCli = dProdCli.Rows[0]["IdProdutoCliente"].ToString();

                            if (item[iped].Descricao.Trim().ToUpper().Replace("'", "") != dProdCli.Rows[0]["DESCRICAO"].ToString().ToUpper().Replace("'", ""))
                            {
                                string nomeprod = item[iped].Descricao.Trim().ToUpper().Replace("'", "");

                                if (nomeprod.Length > 60)
                                    nomeprod = nomeprod.Substring(0, 59);

                                ssqlFinal += "; UPDATE PRODUTOCLIENTE SET DESCRICAO='" + nomeprod + "' WHERE IDPRODUTOCLIENTE=" + IdprodCli + " ";
                            }
                        }


                        string idProd = "";
                        sql = "SELECT * FROM PRODUTO WHERE CodigoDeBarras='" + item[iped].EAN.Trim() + "'";
                        DataTable dProd = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                        if (dProd.Rows.Count == 0)
                        {
                            idProd = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTO", cnx);
                            ssqlFinal += "; INSERT INTO PRODUTO (IDProduto, CodigoDeBarras, PesoLiquido, Especie, DataDeCadastro) VALUES (" + idProd + ", '" + item[iped].EAN.Trim() + "', 0, 'UNI', Getdate())";
                        }
                        else
                            idProd = dProd.Rows[0]["IDPRODUTO"].ToString();


                        string idProdEmb = "";
                        sql = "SELECT * FROM PRODUTOEMBALAGEM WHERE IDPRODUTOCLIENTE= " + IdprodCli + " AND IDPRODUTO=" + idProd;
                        DataTable dProdEmb = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                        if (dProdEmb.Rows.Count == 0)
                        {
                            string nomeprod = item[iped].Descricao.Trim().ToUpper().Replace("'", "");

                            if (nomeprod.Length > 60)
                                nomeprod = nomeprod.Substring(0, 59);

                            idProdEmb = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOEMBALAGEM", cnx);
                            ssqlFinal += "; INSERT INTO PRODUTOEMBALAGEM(IDProdutoEmbalagem, IDProdutoCliente, IDProduto, Conteudo, UnidadeDoCliente, ValorUnitario, DataDeCadastro, Ativo) ";
                            ssqlFinal += " VALUES(" + idProdEmb + ", " + IdprodCli + ", " + idProd + ", '" + nomeprod + "', 1, " + item[iped].ValorUnitario.ToString().Replace(",", ".") + ", GETDATE(), 'SIM')";
                        }
                        else
                            idProdEmb = dProdEmb.Rows[0]["IDProdutoEmbalagem"].ToString();

                        #endregion

                        #region Documento Item

                        decimal vltot = item[iped].Quantidade * item[iped].ValorUnitario;

                        string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOPEDIDOITEM", cnx);
                        ssqlFinal += "; INSERT INTO DOCUMENTOPEDIDOITEM (IDDocumentoItem, IDDocumento,IDProdutoEmbalagem, IDUsuario, Quantidade,ValorUnitario,ValorTotalDoItem,IdProdutoCliente, QuantidadeUnidadeEstoque, Saldo) ";
                        ssqlFinal += " values(" + id + ", " + iddoc + "," + idProdEmb + ",2, " + item[iped].Quantidade.ToString().Replace(",", ".") + "," + item[iped].ValorUnitario.ToString().Replace(",", ".") + ", " + vltot.ToString().Replace(",", ".") + "," + IdprodCli + ", " + item[iped].Quantidade.ToString().Replace(",", ".") + ", " + item[iped].Quantidade.ToString().Replace(",", ".") + ")";

                        #endregion
                    }

                    /*
                    #region Documento Filial
                    string iddocfil = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOFILIAL", cnx);

                    ssqlFinal += "; INSERT INTO DOCUMENTOFILIAL (IDDocumentoFilial,IDDocumento,IDFilial,Situacao, Data, IDRegiaoItem) VALUES(" + iddocfil + "," + iddoc + "," + dt.Rows[0]["IDFILIALPADRAOINTERNET"].ToString() + ",'" + (prod_hom.ToUpper() == "PRODUCAO" ? "AGUARDANDO LIBERACAO" : "PEDIDO CANCELADO") + "', GETDATE(), 1)";
                    #endregion
                     */
                }

                if (ssqlFinal == "")
                    throw new Exception("Verifique o Documento. Já existe na base de dados.");

                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(ssqlFinal.ToUpper(), cnx);

                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - PEDIDOS HOME REFILL - Receber Pedidos ", "obj:" + obj + "<br>SQL:" + sql + " ||| SQLFINAL: " + ssqlFinal, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception)
                { }

                return "Documento(s) Gravados com Sucesso ";
            }
            catch (Exception ex)
            {
                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "PEDIDOS HOME REFILL - Receber Pedidos", "obj: " + obj + "<br> " + ex.Message + " |||| SQL:" + sql + " ||| SQLFINAL: " + ssqlFinal, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception)
                { }

                Err = ex.Message;
                return ex.Message;

            }
            finally
            {
                idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "ReceberPedidos", null, null, "Finalizou -" + Err, cnx);
            }
        }

        [WebMethod]
        public DataTable PosicaoEstoque(string Login, string Senha)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            int idLog = 0;
            string Err="";
            try
            {


                idLog = Sistran.Library.GetDataTables.LogMetodo(null, "PosicaoEstoque", null, null, "EXEC PRC_ESTOQUE_HOMEREFIILL", cnx);

                string sql = "";
                sql = "SELECT * FROM USUARIO WHERE LOGIN='" + Login + "' AND SENHA='" + Senha + "' AND ATIVO='SIM'";
                DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                //if (d.Rows.Count == 0)
                //    throw new Exception("USUARIO OU SENHA INVALIDOS");


                sql = "EXEC PRC_ESTOQUE_HOMEREFIILL";
                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - PosicaoEstoque ", "SQL:" + sql, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception)
                { }



                return Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

            }
            catch (Exception EX)
            {

                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - PosicaoEstoque ", "Ex:" + EX.Message, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                Err = EX.Message;
                throw EX;
            }
            finally
            {
                idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "PosicaoEstoque", null, null, "EXEC PRC_ESTOQUE_HOMEREFIILL - Finalizou -" + Err, cnx);
            }
        }

        [WebMethod]
        public List<Produto> ProdutosAtivos(string Login, string Senha)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            int idLog = 0;
            string Err = "";
            try
            {
                idLog = Sistran.Library.GetDataTables.LogMetodo(null, "ProdutosAtivos", null, null, "EXEC PRC_RETORNAR_PRODUTOS_HOMEREFILL", cnx);
                
                string sql = "";
                sql = "SELECT * FROM USUARIO WHERE LOGIN='" + Login + "' AND SENHA='" + Senha + "' AND ATIVO='SIM'";

                DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                //if (d.Rows.Count == 0)
                //    throw new Exception("USUARIO OU SENHA INVALIDOS");


                sql = "EXEC PRC_RETORNAR_PRODUTOS_HOMEREFILL";

                DataTable dprod = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                DataView view = new DataView(dprod);
                DataTable dd = view.ToTable(true, "IDPRODUTOCLIENTE", "CODIGO", "DESCRICAO", "CODIGONCM", "ATIVO");


                List<Produto> prd = new List<Produto>();
                for (int i = 0; i < dd.Rows.Count; i++)
                {
                    Produto p = new Produto();
                    p.IDPRODUTOCLIENTE = dd.Rows[i]["IDPRODUTOCLIENTE"].ToString();
                    p.CODIGO = dd.Rows[i]["CODIGO"].ToString();
                    p.CODIGONCM = dd.Rows[i]["CODIGONCM"].ToString();
                    p.DESCRICAO = dd.Rows[i]["DESCRICAO"].ToString();

                    DataRow[] it = dprod.Select("IDPRODUTOCLIENTE='" + p.IDPRODUTOCLIENTE + "'", "");
                    List<CodigoDeBarras> lbar = new List<CodigoDeBarras>(); ;

                    for (int j = 0; j < it.Length; j++)
                    {
                        CodigoDeBarras cb = new CodigoDeBarras();
                        cb.CODIGODEBARRAS = it[j]["CODIGODEBARRAS"].ToString();
                        lbar.Add(cb);
                    }


                    if (it.Length > 0)
                        p.CODIGODEBARRA = lbar;

                    prd.Add(p);

                }

                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - ProdutosAtivos ", "SQL:" + sql, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception)
                { }

                return prd;
            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - ProdutosAtivos ", "Ex:" + ex.Message, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                Err = ex.Message;
                throw ex;
            }
            finally
            {
                idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "ProdutosAtivos", null, null, "EXEC PRC_RETORNAR_PRODUTOS_HOMEREFILL - Finalizou -" + Err, cnx);
            }
        }

        #region CNPJ / CPF
        public string FormatarCnpj(string s)
        {
            s = s.Replace(".", "");
            s = s.Replace("-", "");
            s = s.Replace("/", "");
            s = s.Replace(@"\", "");

            string resultCNPJ = "SIM";
            string resultCPF = "SIM";

            if (!IsCnpj(s))
                resultCNPJ = "NAO";

            if(!IsCpf(s))
                resultCPF = "NAO";

            if (resultCNPJ == "NAO" && resultCPF == "NAO")                 
                throw new Exception("CNPJ/CPF Inválidos");



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

        public string ZerosEsquerda(string strString, int intTamanho)
        {

            string strResult = "";

            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {

                strResult += "0";

            }

            return strResult + strString;

        }
        
        public static bool IsCnpj(string cnpj)
		{
			int[] multiplicador1 = new int[12] {5,4,3,2,9,8,7,6,5,4,3,2};
			int[] multiplicador2 = new int[13] {6,5,4,3,2,9,8,7,6,5,4,3,2};
			int soma;
			int resto;
			string digito;
			string tempCnpj;

			cnpj = cnpj.Trim();
			cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

			if (cnpj.Length != 14)
			   return false;

			tempCnpj = cnpj.Substring(0, 12);

			soma = 0;
			for(int i=0; i<12; i++)
			   soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

			resto = (soma % 11);
			if ( resto < 2)
			   resto = 0;
			else
			   resto = 11 - resto;

			digito = resto.ToString();

			tempCnpj = tempCnpj + digito;
			soma = 0;
			for (int i = 0; i < 13; i++)
			   soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

			resto = (soma % 11);
			if (resto < 2)
			    resto = 0;
			else
			   resto = 11 - resto;

			digito = digito + resto.ToString();

			return cnpj.EndsWith(digito);
		}

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        #endregion
    }

    public class Produto
    {
        public string IDPRODUTOCLIENTE { get; set; }
        public string CODIGO { get; set; }
        public string DESCRICAO { get; set; }
        public string CODIGONCM { get; set; }
        public List<CodigoDeBarras> CODIGODEBARRA { get; set; }
        
    }

    public class CodigoDeBarras
    {
        public string CODIGODEBARRAS { get; set; }
    }


    public class PedidoCancelado
    {
        public string Cliente_CNPJ { get; set; }        
        public string NumeroDocumento { get; set; }
        public string Serie { get; set; }        
        public string Dest_CNPJCPF { get; set; }
        public string CompraVenda { get; set; }

    }

    public class Pedido
    {
        public string Cliente_CNPJ { get; set; }
        public string TipoDeDocumento { get; set; }
        public DateTime DataDeEmissao { get; set; }
        public string NumeroDocumento { get; set; }
        public string Serie { get; set; }
        public string Dest_CNPJCPF { get; set; }
        public string Dest_IERG { get; set; }
        public string Dest_RAZAOSOCIAL { get; set; }
        public string Dest_FANTASIA { get; set; }
        public string Dest_ENDERECO { get; set; }
        public string Dest_NUMERO { get; set; }
        public string Dest_COMPLEMENTO { get; set; }
        public string Dest_BAIRRO { get; set; }
        public string Dest_COD_IBGE_CIDADE { get; set; }
        public string Dest_CIDADE_NOME { get; set; }
        public string Dest_CEP { get; set; }


        public string Dest_ENTREGA_CEP { get; set; }
        public string Dest_ENTREGA_ENDERECO { get; set; }
        public string Dest_ENTREGA_NUMERO { get; set; }
        public string Dest_ENTREGA_COMPLEMENTO { get; set; }
        public string Dest_ENTREGA_BAIRRO { get; set; }
        public string Dest_ENTREGA_COD_IBGE_CIDADE { get; set; }



        public string CompraVenda { get; set; }
        public List<Itens> itens { get; set; }



        public class Itens
        {
            public string SKU { get; set; }
            public string Descricao { get; set; }
            public string EAN { get; set; }
            public string CodigoNCM { get; set; }
            public int Quantidade { get; set; }
            public decimal ValorUnitario { get; set; }
            public decimal PesoLiquido { get; set; }

        }
    }
}