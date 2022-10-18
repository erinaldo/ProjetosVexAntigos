using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.IO;

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

        /// <summary>
        /// Metodo para receber um XML
        /// </summary>
        /// <param name="Chave">Chave da Nota Fiscal</param>
        /// <param name="xml">Binário Xml</param>
        /// <param name="EntradaSaida">Tipo de Nota (Entrada ou Saída)</param>
        /// <returns>Resultado do Processamento (OK ou err^Descrição do Erro)</returns>
        /// 
        [WebMethod]
        public string ReceberXml(string Chave, byte[] xml, string EntradaSaida)
        {
                SqlConnection cnx = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - ReceberXml ", "Chave: "+ Chave, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");


            try
            {
                if (Chave == "" || Chave.Trim().Length != 44)  
                    throw new Exception("Chave Inválida");

                if(xml==null)
                    throw new Exception("XML Inválido");

                if (xml.Length == 0)
                    throw new Exception("XML Vazio");


                if (EntradaSaida == "")
                    EntradaSaida = "Entrada";


                string sql = "INSERT INTO XmlHomeRefill (Chave, Arquivo,EntradaSaida, DataRecebimento,Processado) VALUES ('"+Chave+"', @Arquivo,'"+EntradaSaida+"', GETDATE(),NULL)";                 


                SqlCommand comm = new SqlCommand();

                cnx.Open();
                comm.Connection = cnx;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sql;
                SqlParameter parArq = new SqlParameter("@Arquivo", xml);
                comm.Parameters.Add(parArq);
                comm.ExecuteNonQuery();
                return "OK";
  
            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "erro - ReceberXml ", "Chave: " + Chave + "-" + ex.Message, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");

                return "err^" + ex.Message;
            }
            finally
            {
                if(cnx.State == ConnectionState.Open)
                    cnx.Close();
            }
        }


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
                    //Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - Entradas ", "SQL:" + sql, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
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
                d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);


                for (int i = 0; i < d.Rows.Count; i++)
                {
                    d.Rows[i]["SALDOATUAL"] = float.Parse(d.Rows[i]["SALDOESTOQUE"].ToString()) - float.Parse(d.Rows[i]["EMPENHADOCOMSEPARACAO"].ToString()) - float.Parse(d.Rows[i]["EMPENHADOCOMPEDIDOS"].ToString());

                    if (int.Parse(d.Rows[i]["SALDOATUAL"].ToString()) < 0)
                        d.Rows[i]["SALDOATUAL"] = 0;

                }
                //http://www.grupologos.com.br/wss/homerefill/hr.asmx

                try
                {
                    string s = "insert into LogSaldoAtual(erro) values ('CONSULTA CORRETA')";
                    Sistran.Library.GetDataTables.ExecutarSemRetornoWin(s, cnx);
                }
                catch (Exception)
                { }
               
                return d;
            }
            catch (Exception exx)
            {
                //Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - SaldoAtual ", "Ex:" + ex.Message, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                //DataTable s = new DataTable();
                //s.Columns.Add("Erro");

                //DataRow r = s.NewRow();
                //r[0] = "Não foi possível retornar os dados, tente novamente.";
                //s.Rows.Add(r);              

                try
                {
                    string s = "insert into LogSaldoAtual(erro) values ('" + exx.Message.Replace("'", "").Replace("  ", " ") + "')";
                    Sistran.Library.GetDataTables.ExecutarSemRetornoWin(s, cnx);
                    throw new Exception("Não foi possível retornar os dados, tente novamente.");

                }
                catch (Exception)
                { }

                throw new Exception("Não foi possível retornar os dados, tente novamente.");
                

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
                for (int ii = 0; ii < o.Length; ii++)
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
                   // Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - PEDIDOS HOME REFILL - Cancelar Pedidos ", "obj:" + obj + "<br> " + "SQL:" + sql + " ||| SQLFINAL: " + sqlFinal, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception)
                { }

                return "Pedido Cancelado com Sucesso";
            }
            catch (Exception ex)
            {
                //try
                //{
                //    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "PEDIDOS HOME REFILL - Cancelamento", "obj:" + obj + "<br> " + ex.Message + " |||| SQL:" + sql + " ||| SQLFINAL: " + sqlFinal, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                //}
                //catch (Exception)
                //{ }

                return "Não foi possivel cancelar o pedido. - " + ex.Message;
            }

            finally
            {
                idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "CancelarPedidos", null, null, "Finalizou -" + Err, cnx);
            }
        }

      

        public string gCep="";
        public string gidBairro="";
        public string gIdCidade="";
        public string gEndereco = "";


        public struct NovoEndereco
        {
            public int IdCidade { get; set; }
            public int IdBairro { get; set; }
            public string NomeCidade { get; set; }
            public string NomeBairro { get; set; }
            public string NomeEndereco { get; set; }
            public string Numero { get; set; }
            public string CEP { get; set; }
            public string Complemento { get; set; }
        }

        [WebMethod]
        public string ReceberPedidosStatus(string Login, string Senha, List<Pedido> pedido, string prod_hom, string TipoDePedido)
        {
            if(Login != "321500" && Senha !="321500")
                throw new Exception("Credenciais Inválidas");

             
            if (TipoDePedido == "")
                TipoDePedido = "EFETIVO";

            NovoEndereco ne = new NovoEndereco();

            string iddoc = "0";
            string cid = "NULL";

            gCep = "";
            gidBairro = "";
            gIdCidade = "";
            gEndereco = "";

            string comp = "";
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            int idLog = 0;
            string Err = "";

            string ssqlFinal = "";
            string sql = "";
        //    string obj = "";
            string idDest = "";
            try
            {


                if (pedido == null)
                    throw new Exception("Objeto de Pedido Vazio. Favor Verificar");


                foreach (var p in pedido)
                {

                    if (p.Dest_ENTREGA_ENDERECO == null)
                        throw new Exception("Pedido Recusado. Favor Informar o Nome da Rua.");


                    p.Cliente_CNPJ = (p.Cliente_CNPJ == null ? "" : p.Cliente_CNPJ);
                    p.DataDeEmissao = (p.DataDeEmissao == null ? DateTime.Now : p.DataDeEmissao);
                    p.NumeroDocumento = (p.NumeroDocumento == null ? "" : p.NumeroDocumento);
                    p.Serie = (p.Serie == null ? "" : p.Serie);
                    p.Dest_IERG = (p.Dest_IERG == null ? "" : p.Dest_IERG);
                    p.Dest_RAZAOSOCIAL = (p.Dest_RAZAOSOCIAL == null ? "" : p.Dest_RAZAOSOCIAL).Replace("'", "");
                    p.Dest_FANTASIA = (p.Dest_FANTASIA == null ? "" : p.Dest_FANTASIA).Replace("'", "");
                    p.Dest_ENDERECO = (p.Dest_ENDERECO == null ? "" : p.Dest_ENDERECO).Replace("'", "");
                    p.Dest_NUMERO = (p.Dest_NUMERO == null ? "" : p.Dest_NUMERO).Replace("'", "");
                    p.Dest_COMPLEMENTO = (p.Dest_COMPLEMENTO == null ? "" : p.Dest_COMPLEMENTO).Replace("'", "");
                    p.Dest_BAIRRO = (p.Dest_BAIRRO == null ? "" : p.Dest_BAIRRO).Replace("'", "");
                    p.Dest_CEP = (p.Dest_CEP == null ? "" : p.Dest_CEP).Replace("'", "");
                    p.Dest_ENTREGA_CEP = (p.Dest_ENTREGA_CEP == null ? "" : p.Dest_ENTREGA_CEP).Replace("'", "");
                    p.Dest_ENTREGA_ENDERECO = (p.Dest_ENTREGA_ENDERECO == null ? "" : p.Dest_ENTREGA_ENDERECO).Replace("'", "");
                    p.Dest_ENTREGA_NUMERO = (p.Dest_ENTREGA_NUMERO == null ? "" : p.Dest_ENTREGA_NUMERO).Replace("'", "");
                    p.Dest_ENTREGA_COMPLEMENTO = (p.Dest_ENTREGA_COMPLEMENTO == null ? "" : p.Dest_ENTREGA_COMPLEMENTO).Replace("'", "");
                    p.Dest_ENTREGA_BAIRRO = (p.Dest_ENTREGA_BAIRRO == null ? "" : p.Dest_ENTREGA_BAIRRO).Replace("'", "");
                    p.Dest_ENTREGA_COD_IBGE_CIDADE = (p.Dest_ENTREGA_COD_IBGE_CIDADE == null ? "" : p.Dest_ENTREGA_COD_IBGE_CIDADE);
                    p.PeriodoDeEntregaInicio = (p.PeriodoDeEntregaInicio == null ? "" : p.PeriodoDeEntregaInicio);
                    p.PeriodoDeEntregaFim = (p.PeriodoDeEntregaFim == null ? "" : p.PeriodoDeEntregaFim);
                    p.Longitude = (p.Longitude == null ? "" : p.Longitude);
                    p.Latitude = (p.Latitude == null ? "" : p.Latitude);
                    p.TipoDeDocumento = "PEDIDO";
                                      

                    if (p.Dest_COD_IBGE_CIDADE == null)
                        p.Dest_COD_IBGE_CIDADE = "0";
                   
                    if (p.Dest_ENTREGA_COD_IBGE_CIDADE == null)
                        p.Dest_ENTREGA_COD_IBGE_CIDADE = "0";

                    if (p.TipoDeDocumento == null || p.TipoDeDocumento == "")
                        throw new Exception("Pedido Recusado falta Tipo de Documento. Informe Pedido ou NOTA FISCAL");

                    if (prod_hom == null || prod_hom == "")
                        throw new Exception("Pedido Recusado. Defina Homologação ou Produção");


                    if (p.Cliente_CNPJ.Length > 20)
                        p.Cliente_CNPJ = p.Cliente_CNPJ.Substring(0, 20);
                   

                    if (p.Serie.Length > 3)
                        p.Serie.Substring(0, 3);

                    if (p.Dest_IERG.Length > 20)
                        p.Dest_IERG.Substring(0, 20);

                    if (p.Dest_RAZAOSOCIAL.Length > 60)
                        p.Dest_RAZAOSOCIAL = p.Dest_RAZAOSOCIAL.Substring(0, 60);


                    if (p.Dest_FANTASIA.Length > 30)
                        p.Dest_FANTASIA = p.Dest_FANTASIA.Substring(0, 30);

                    if (p.Dest_ENDERECO.Length > 60)
                        p.Dest_ENDERECO = p.Dest_ENDERECO.Substring(0, 60);

                    if (p.Dest_NUMERO.Length > 10)
                        p.Dest_NUMERO = p.Dest_NUMERO.Substring(0, 10);


                    if (p.Dest_COMPLEMENTO.Length > 50)
                        p.Dest_COMPLEMENTO = p.Dest_COMPLEMENTO.Substring(0, 50);

                    if (p.Dest_CEP.Length > 8)
                        p.Dest_CEP = p.Dest_CEP.Substring(0, 8);


                    if (p.Dest_ENTREGA_CEP.Length > 8)
                        p.Dest_ENTREGA_CEP = p.Dest_ENTREGA_CEP.Substring(0, 8);

                    if (p.Dest_ENTREGA_ENDERECO.Length > 60)
                        p.Dest_ENTREGA_ENDERECO = p.Dest_ENTREGA_ENDERECO.Substring(0, 60);

                    if (p.Dest_ENTREGA_NUMERO.Length > 10)
                        p.Dest_ENTREGA_NUMERO = p.Dest_ENTREGA_NUMERO.Substring(0, 10);

                    if (p.Dest_ENTREGA_COMPLEMENTO.Length > 50)
                        p.Dest_ENTREGA_COMPLEMENTO = p.Dest_ENTREGA_COMPLEMENTO.Substring(0, 50);
                    if (p.DataParaEntrega == null)
                        p.DataParaEntrega = "";

                    if (p.Longitude != "" && p.Latitude != "")
                    {
                        p.Longitude = p.Longitude.Replace(",", ".");
                        p.Latitude = p.Latitude.Replace(",", ".");

                        Pe pe = ProcurarEndereco.ProcurarPeloGoogle(p.Longitude, p.Latitude, cnx, p.Dest_ENTREGA_ENDERECO);

                        if (pe.CEPP == null || pe.CEPP == "")
                            pe = ProcurarEndereco.ProcurarPeloBing(p.Longitude, p.Latitude, cnx, p.Dest_ENTREGA_ENDERECO);


                        ne.CEP = pe.CEPP;
                        ne.Complemento = p.Dest_ENTREGA_COMPLEMENTO.Replace("'", "");

                        if (p.Dest_CIDADE_NOME !=null && p.Dest_CIDADE_NOME.Length > 1)
                            ne.IdCidade = ProcurarEndereco.RetornarCidade(p.Dest_CIDADE_NOME, "", cnx);
                        else
                            ne.IdCidade = int.Parse(pe.IdCidadeP);

                        if (p.Dest_BAIRRO != null && p.Dest_BAIRRO == "")
                            p.Dest_BAIRRO = (p.Dest_ENTREGA_BAIRRO == null?"": p.Dest_ENTREGA_BAIRRO);
                        

                        if (p.Dest_BAIRRO != null &&p.Dest_BAIRRO.Length > 1)
                            ne.IdBairro = ProcurarEndereco.RetornarBairro(p.Dest_BAIRRO, ne.IdCidade.ToString(), cnx);
                        else
                            ne.IdBairro = int.Parse(pe.IdBairroP);
                        
                     
                        ne.Numero = p.Dest_ENTREGA_NUMERO;
                        ne.NomeEndereco = p.Dest_ENTREGA_ENDERECO.Replace("'", "");                        
                    }


                    try
                    {
                        List<Pedido.Itens> item = pedido[0].itens;
                        for (int iped = 0; iped < item.Count; iped++)
                        {
                            item[iped].SKU = (item[iped].SKU == null ? "" : item[iped].SKU);
                            item[iped].Descricao = (item[iped].Descricao == null ? "" : item[iped].Descricao);
                            item[iped].CodigoNCM = (item[iped].CodigoNCM == null ? "" : item[iped].CodigoNCM);
                            item[iped].PesoLiquido = (item[iped].PesoLiquido == null ? 0 : item[iped].PesoLiquido);
                            item[iped].Quantidade = (item[iped].Quantidade == null ? 0 : item[iped].Quantidade);
                            item[iped].ValorUnitario = (item[iped].ValorUnitario == null ? 0 : item[iped].ValorUnitario);
                        }
                    }
                    catch (Exception)
                    { }
                }



                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                iddoc = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOPEDIDO", cnx);

                string dataPlajenadaAlterada = "";
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

                    
                    if (dt.Rows.Count == 0)
                        throw new Exception("Cliente não cadastrado.");

                    if (i > 0)
                        ssqlFinal += ";";

                    string trataData = "";
                    if (pedido[i].DataParaEntrega == null)
                        pedido[i].DataParaEntrega = "";

                    if (pedido[i].DataParaEntrega != "")
                    {
                        try
                        {
                            trataData = DateTime.Parse(pedido[i].DataParaEntrega).ToString("yyyy-MM-dd");
                        }
                        catch (Exception)
                        {
                            if (tipo == "SAIDA")
                            {
                                trataData = DateTime.Now.ToString("yyyy-MM-dd");
                                dataPlajenadaAlterada = "Pedido: " + pedido[i].NumeroDocumento + " foi alterada a Data Planejada para a Data Atual";

                            }
                            else
                                trataData = "";
                        }
                    }
                    else
                    {
                        if (tipo == "SAIDA")
                        {
                            trataData = DateTime.Now.ToString("yyyy-MM-dd");
                            dataPlajenadaAlterada = "Pedido: " + pedido[i].NumeroDocumento + " foi alterada a Data Planejada para a Data Atual";
                        }
                    }


                    string pei = pedido[i].PeriodoDeEntregaInicio;
                    if (pei == "")
                        pei = "";
                    else
                    {
                        try
                        {
                            if (pei.Split(' ').Length > 1) // vem hora no formato certro
                            {
                                pei = pei;
                            }
                            else
                            {
                                string[] h = pei.Split('-');
                                string[] mm = h[3].Split(':');
                                DateTime dd = new DateTime(int.Parse(h[0]), int.Parse(h[1]), int.Parse(h[2]), int.Parse(mm[0]), int.Parse(mm[1]), 0);
                                pei = dd.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }


                    string pef = pedido[i].PeriodoDeEntregaFim;
                    if (pef == "")
                        pef = "";
                    else
                    {
                        try
                        {
                            if (pef.Split(' ').Length > 1) // vem hora no formato certro
                            {
                                pef = pef;
                            }
                            else
                            {
                                string[] h = pef.Split('-');
                                string[] mm = h[3].Split(':');
                                DateTime dd = new DateTime(int.Parse(h[0]), int.Parse(h[1]), int.Parse(h[2]), int.Parse(mm[0]), int.Parse(mm[1]), 0);
                                pef = dd.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }        

                    string primeiraEntrega = "0";
                    if (pedido[i].PrimeiraEntrega != null)
                    {
                        primeiraEntrega = pedido[i].PrimeiraEntrega.ToString();
                    }

                    ssqlFinal += " INSERT INTO DOCUMENTOPEDIDO (IDDOCUMENTO, IDCLIENTE, IDREMETENTE, IDFILIAL, IDFILIALATUAL,  NUMERO, SERIE, TIPODEDOCUMENTO, IDDESTINATARIO,  ORIGEM, DATADEEMISSAO, DATADEENTRADA, ENDERECO, ENDERECONUMERO, ENDERECOCOMPLEMENTO, IDENDERECOBAIRRO, IDENDERECOCIDADE, ENDERECOCEP, ANOMES, DATADECANCELAMENTO, ENTRADASAIDA, ATIVO, TIPODESERVICO, DATAPLANEJADA, PERIODODEENTREGAINICIO, PERIODODEENTREGAFIM, TipoDePedido, Latitude, Longitude, EnderecoCoord, CepCoord, PrimeiroPedido) VALUES";
                    ssqlFinal += " (@IDDOCUMENTO, @IDCLIENTE, @IDREMETENTE, @IDFILIAL@, @IDFILIALATUAL@,  '@NUMERO', '@SERIE', '@TIPODEDOCUMENTO', @IDDESTINATARIO,  'WEBSERVICE', '@DATADEEMISSAO', GETDATE(),  '@ENDERECO_ENTREGA@', '@ENDERECONUMERO_ENTREGA@', '@ENDERECOCOMPLEMENTO_ENTREGA@', @IDENDERECOBAIRRO_ENTREGA@, @IDENDERECOCIDADE_ENTREGA@, '@ENDERECOCEP_ENTREGA@', '@ANOMES', @DATADECANCELAMENTO@, @ENTRADASAIDA@, '" + (prod_hom.ToUpper() == "PRODUCAO" ? "SIM" : "NAO") + "', 'NORMAL', " + (trataData == "" ? "NULL" : "'" + trataData + "'") + ", " + (pei == "" ? "NULL" : "'" + pei + "'") + ", " + (pei == "" ? "NULL" : "'" + pef + "'") + ", '" + TipoDePedido + "', '" + pedido[i].Latitude + "', '" + pedido[i].Longitude + "', '@@EnderecoCoord@@','@CepCoord@', '"+ primeiraEntrega +"'  ) ";
                    ssqlFinal = ssqlFinal.Replace("@IDDOCUMENTO", iddoc);

                    ssqlFinal = ssqlFinal.Replace("@IDFILIAL@", dt.Rows[0]["IDFILIALPADRAOINTERNET"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@IDFILIALATUAL@", dt.Rows[0]["IDFILIALPADRAOINTERNET"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@NUMERO", pedido[i].NumeroDocumento);

                    if (TipoDePedido == "EFETIVO" || tipo=="ENTRADA")
                        ssqlFinal = ssqlFinal.Replace("@SERIE", pedido[i].Serie);
                    else
                        ssqlFinal = ssqlFinal.Replace("@SERIE", "ESP");

                    
                    ssqlFinal = ssqlFinal.Replace("@TIPODEDOCUMENTO", pedido[i].TipoDeDocumento);
                    ssqlFinal = ssqlFinal.Replace("@DATADEEMISSAO", DateTime.Parse(pedido[i].DataDeEmissao.ToString()).ToString("yyyy-MM-dd"));
                    ssqlFinal = ssqlFinal.Replace("@DATADECANCELAMENTO@", (prod_hom.ToUpper() == "PRODUCAO" ? "NULL" : "GetDate()"));
                    ssqlFinal = ssqlFinal.Replace("@ANOMES", DateTime.Now.Year.ToString() + "/" + DateTime.Now.ToString("MM"));
                    ssqlFinal = ssqlFinal.Replace("@IDCLIENTE", dt.Rows[0]["IDCLIENTE"].ToString());


                    if (pedido[i].Longitude != "" && pedido[i].Latitude != "")
                    {
                        ssqlFinal = ssqlFinal.Replace("@ENDERECO_ENTREGA@", ne.NomeEndereco.Replace("'", ""));
                        ssqlFinal = ssqlFinal.Replace("@ENDERECONUMERO_ENTREGA@", ne.Numero.Replace("'", ""));
                        ssqlFinal = ssqlFinal.Replace("@ENDERECOCOMPLEMENTO_ENTREGA@", ne.Complemento.Replace("'", ""));
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", ne.IdBairro.ToString());
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE_ENTREGA@", ne.IdCidade.ToString());
                        ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP_ENTREGA@", ne.CEP);
                    }

                    comp = "";
                    comp = pedido[i].Dest_ENTREGA_COMPLEMENTO;
                    if (comp != null && comp.Length > 60)
                        comp = comp.Substring(0, 58);

                    //se vier os dados do endereço de entrega
                    if (pedido[i].Dest_ENTREGA_ENDERECO != "" && pedido[i].Dest_ENTREGA_CEP != "")
                    {
                        if (pedido[i].Dest_ENTREGA_NUMERO.Length > 9)
                            pedido[i].Dest_ENTREGA_NUMERO = pedido[i].Dest_ENTREGA_NUMERO.Substring(0, 9);
                        
                        ssqlFinal = ssqlFinal.Replace("@ENDERECO_ENTREGA@", pedido[i].Dest_ENTREGA_ENDERECO.Replace("'", ""));
                        ssqlFinal = ssqlFinal.Replace("@ENDERECONUMERO_ENTREGA@", pedido[i].Dest_ENTREGA_NUMERO.Replace("'", ""));
                        ssqlFinal = ssqlFinal.Replace("@ENDERECOCOMPLEMENTO_ENTREGA@", comp.Replace("'", ""));

                        string idCidade = ProcurarEndereco.RetornarCidade(pedido[i].Dest_CIDADE_NOME, pedido[i].Dest_ENTREGA_CEP, cnx).ToString();
                        string idBairro = ProcurarEndereco.RetornarBairro(pedido[i].Dest_ENTREGA_BAIRRO, idCidade, cnx).ToString();


                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE_ENTREGA@", idCidade);
                        ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP_ENTREGA@", pedido[i].Dest_ENTREGA_CEP);                        
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", idBairro);
                    }  
                    else // Se nao vier o endereço e vier as coordenadas  
                    {

                        if (pedido[i].Longitude == "" || pedido[i].Latitude == "")
                            throw new Exception("Pedido Recusado. As coordenadas não foram informadas");
                        
                        ssqlFinal = ssqlFinal.Replace("@ENDERECONUMERO_ENTREGA@", pedido[i].Dest_ENTREGA_NUMERO);
                        ssqlFinal = ssqlFinal.Replace("@ENDERECOCOMPLEMENTO_ENTREGA@", comp);

                        sql = sql.Replace("@CEP", ne.CEP.Trim());

                        if(pedido[i].Dest_ENTREGA_ENDERECO !="" &&  pedido[i].Dest_ENTREGA_ENDERECO.Length>10)
                            sql = sql.Replace("@ENDERECO",pedido[i].Dest_ENTREGA_ENDERECO.Replace("'", "´"));
                        else
                            sql = sql.Replace("@ENDERECO", ne.NomeEndereco.Replace("'", "´"));
                        
                        sql = sql.Replace("@IDCIDADE", ne.IdCidade.ToString());
                        sql = sql.Replace("@IDBAIRRO", ne.IdBairro.ToString());

                        ssqlFinal = ssqlFinal.Replace("@ENDERECO_ENTREGA@", ne.NomeEndereco);
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", ne.IdBairro.ToString());
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE_ENTREGA@", ne.IdCidade.ToString());
                        ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP_ENTREGA@", ne.CEP);                                        
                    }


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

                   
                    //cadastra o destinario

                    //verifica se pega o IdCdastro Zerado
                    if (dtDest.Rows.Count > 0)
                    {
                        if (dtDest.Rows[0]["IDCADASTRO"].ToString() == "0")
                        {
                            dtDest.Rows.Clear();
                        }
                    }

                    if (dtDest.Rows.Count == 0)
                    {
                        idDest = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO", cnx);
                        sql = "INSERT INTO CADASTRO (IDCADASTRO,CNPJCPF, INSCRICAORG, RAZAOSOCIALNOME,FANTASIAAPELIDO,ENDERECO,NUMERO,COMPLEMENTO,IDCIDADE,IDBAIRRO,CEP,Latitude, Longitude) ";
                        sql += " VALUES(@IDCADASTRO,'@CNPJCPF', '@INSCRICAORG', '@RAZAOSOCIALNOME','@FANTASIAAPELIDO','@ENDERECO','@NUMERO', '@COMPLEMENTO',@IDCIDADE,@IDBAIRRO,'@CEP', '" + pedido[i].Latitude + "', '" + pedido[i].Longitude+ "')";

                        if (pedido[i].Longitude != "" && pedido[i].Latitude != "")
                        {
                            sql = sql.Replace("@CEP", ne.CEP.Trim());

                            if(pedido[i].Dest_ENTREGA_ENDERECO.Trim()!="")
                                sql = sql.Replace("@ENDERECO", pedido[i].Dest_ENTREGA_ENDERECO.Trim().Replace("'", ""));
                            else
                                sql = sql.Replace("@ENDERECO", ne.NomeEndereco.Replace("'", "").Trim().Replace("'", ""));


                            sql = sql.Replace("@IDCIDADE", ne.IdCidade.ToString());
                            sql = sql.Replace("@NUMERO", ne.Numero.Replace("'", ""));
                            sql = sql.Replace("@COMPLEMENTO", ne.Complemento.Replace("'", ""));
                            ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO@", ne.IdBairro.ToString());
                            sql = sql.Replace("@IDBAIRRO", ne.IdBairro.ToString());                          

                        }


                        sql = sql.Replace("@IDCADASTRO", idDest);
                        sql = sql.Replace("@CNPJCPF", FormatarCnpj(pedido[i].Dest_CNPJCPF));
                        sql = sql.Replace("@INSCRICAORG", pedido[i].Dest_IERG);
                        sql = sql.Replace("@RAZAOSOCIALNOME", pedido[i].Dest_RAZAOSOCIAL.Replace("'", ""));

                        if (pedido[i].Dest_FANTASIA.Length > 30)
                            pedido[i].Dest_FANTASIA = pedido[i].Dest_FANTASIA.Substring(0, 30).Replace("'", "");

                        sql = sql.Replace("@FANTASIAAPELIDO", pedido[i].Dest_FANTASIA.Replace("'", ""));

                        comp = "";
                        comp = pedido[i].Dest_ENTREGA_COMPLEMENTO; ;
                        if (comp != null && comp.Length > 50)
                            comp = comp.Substring(0, 50);

                        sql = sql.Replace("@NUMERO", pedido[i].Dest_ENTREGA_NUMERO);
                        sql = sql.Replace("@COMPLEMENTO", comp.Replace("'", ""));

                        //if (pedido[i].Dest_ENTREGA_COD_IBGE_CIDADE != "" && pedido[i].Dest_ENTREGA_COD_IBGE_CIDADE != null && pedido[i].Dest_ENTREGA_ENDERECO !="")
                        //{
                        //    sql = sql.Replace("@ENDERECO", pedido[i].Dest_ENTREGA_ENDERECO);

                        //    string idCidade = Sistran.Library.GetDataTables.ExecutarRetornoIDs("SELECT IDCIDADE FROM CIDADE WHERE CODIGODOIBGE=" + (pedido[i].Dest_ENTREGA_COD_IBGE_CIDADE == "" ? "0" : pedido[i].Dest_ENTREGA_COD_IBGE_CIDADE), cnx);
                        //    DataTable Bairro = Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT * FROM BAIRRO WHERE NOME='" + pedido[i].Dest_ENTREGA_BAIRRO + "' AND IDCIDADE = " + idCidade, cnx);

                        //    if (Bairro.Rows.Count == 0)
                        //    {

                        //        string idb2 = Sistran.Library.GetDataTables.RetornarIdTabela("BAIRRO", cnx);
                        //        string sqlAUX = "INSERT INTO BAIRRO (IDBairro, IDCidade, Nome, Origem) VALUES (" + idb2 + "," + idCidade + ", '" + pedido[i].Dest_ENTREGA_BAIRRO.ToUpper().Replace("'", "") + "','WEBSERVICE' ); select 1";
                        //        Sistran.Library.GetDataTables.RetornarDataTableWS(sqlAUX, cnx);

                        //        sql = sql.Replace("@IDBAIRRO", idb2);
                        //        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO@", idb2);

                        //    }
                        //    else
                        //    {
                        //        sql = sql.Replace("@IDBAIRRO", Bairro.Rows[0]["IDBAIRRO"].ToString());
                        //        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO@", Bairro.Rows[0]["IDBAIRRO"].ToString());
                        //    }

                        //    /////////////////////////////////////////////////////////////////////////////
                        //    sql = sql.Replace("@IDCIDADE", idCidade);
                        //    sql = sql.Replace("@CEP", pedido[i].Dest_ENTREGA_CEP.Replace("-", ""));
                        //    /////////////////////////////////////////////////////////////////////////////

                        //    ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE@", idCidade);
                        //    //ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", idDest);
                           
                        //}
                        //else // Caso nao venha o Codigo IBGE
                        //{
                            sql = sql.Replace("@CEP", ne.CEP.Trim());

                            if(pedido[i].Dest_ENTREGA_ENDERECO.Trim()!="")
                                sql = sql.Replace("@ENDERECO", pedido[i].Dest_ENTREGA_ENDERECO.Trim().Replace("'",""));                            
                            else
                             sql = sql.Replace("@ENDERECO", ne.NomeEndereco.Replace("'",""));
                            
                            sql = sql.Replace("@IDCIDADE", ne.IdCidade.ToString());                            
                            sql = sql.Replace("@IDBAIRRO", ne.IdBairro.ToString());

                            ssqlFinal = ssqlFinal.Replace("@ENDERECO_ENTREGA@", ne.NomeEndereco);
                            ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", ne.IdBairro.ToString());
                            ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE_ENTREGA@",ne.IdCidade.ToString());
                            ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP_ENTREGA@",ne.CEP);

                            ssqlFinal = ssqlFinal.Replace("@@EnderecoCoord@@", ne.NomeEndereco);
                            ssqlFinal = ssqlFinal.Replace("@CepCoord@", ne.CEP);

                        
                        //}

                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, cnx);


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

                        //////////////////////////////////////////////////////////////////////////
                        string idCadEndereco = "";
                        if (pedido[i].Dest_Email == null)
                            pedido[i].Dest_Email = "";

                        if (pedido[i].Dest_Telefone == null)
                            pedido[i].Dest_Telefone = "";


                        if (pedido[i].Dest_Email.ToString() != "" && pedido[i].Dest_Email.ToString().Contains("@"))
                        {
                            idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                            ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                            ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 1, '" + pedido[i].Dest_Email.ToLower() + "'); ";
                        }

                        if (pedido[i].Dest_Telefone.ToString() != "")
                        {
                            idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                            ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                            ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 2, '" + pedido[i].Dest_Telefone + "'); ";
                        }

                        //////////////////////////////////////////////////////////////////////////

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

                        //Iguala o endereço do Cadastro = endereço do Pedido//
                        string ss = "Update Cadastro set IdCidade=" + ne.IdCidade + ", IdBairro=" + ne.IdBairro + ", CEP='" + ne.CEP + "', Complemento='" + ne.Complemento + "', Endereco='" + ne.NomeEndereco + "', Numero='" + ne.Numero + "', Latitude ='" + pedido[i].Latitude + "', Longitude='" + pedido[i].Longitude + "' ";
                        ss += " Where IdCadastro =" + idDest + " And IdCadastro not in(Select IdCadastro from Filial)";
                        Sistran.Library.GetDataTables.ExecutarComandoSql(ss, cnx);
                        //

                        /////////////////////////////////////////////////////////////////////////////////////////
                        if (pedido[i].Dest_Email != "" || pedido[i].Dest_Telefone != "")
                        {
                            string xsql = "SELECT * FROM CadastroContatoEndereco where idCadastro=" + idDest + " and IDCadastroTipoDeContato in (1,2)";
                            DataTable dx = Sistran.Library.GetDataTables.RetornarDataTableWS(xsql, cnx);


                            if (dx.Rows.Count == 0)
                            {
                                if (pedido[i].Dest_Email == null)
                                    pedido[i].Dest_Email = "";

                                if (pedido[i].Dest_Telefone == null)
                                    pedido[i].Dest_Telefone = "";


                                string idCadEndereco = "";
                                if (pedido[i].Dest_Email.ToString() != "" && pedido[i].Dest_Email.ToString().Contains("@"))
                                {
                                    idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                                    ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                                    ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 1, '" + pedido[i].Dest_Email.ToLower().Replace("'", "") + "'); ";
                                }

                                if (pedido[i].Dest_Telefone.ToString() != "")
                                {
                                    idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                                    ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                                    ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 2, '" + pedido[i].Dest_Telefone + "'); ";
                                }

                            }
                            else
                            {
                                if (pedido[i].Dest_Email == null)
                                    pedido[i].Dest_Email = "";

                                if (pedido[i].Dest_Telefone == null)
                                    pedido[i].Dest_Telefone = "";


                                if (pedido[i].Dest_Email.ToString() != "" && pedido[i].Dest_Email.Contains("@"))
                                {
                                    DataRow[] rx = dx.Select("IDCadastroTipoDeContato=1", "");

                                    if (rx != null && rx.Length > 0)
                                    {
                                        ssqlFinal += "UPDATE CadastroContatoEndereco SET ENDERECO='" + pedido[i].Dest_Email.ToString() + "' WHERE IDCadastroContatoEndereco=" + rx[0]["IDCadastroContatoEndereco"].ToString() + "; ";
                                    }
                                    else
                                    {
                                        string idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                                        ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                                        ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 1, '" + pedido[i].Dest_Email.ToString().ToLower() + "'); ";
                                    }
                                }


                                if (pedido[i].Dest_Telefone.ToString() != "")
                                {
                                    DataRow[] rx = dx.Select("IDCadastroTipoDeContato=2", "");

                                    if (rx != null && rx.Length > 0)
                                    {
                                        ssqlFinal += "UPDATE CadastroContatoEndereco SET ENDERECO='" + pedido[i].Dest_Telefone.ToString() + "' WHERE IDCadastroContatoEndereco=" + rx[0]["IDCadastroContatoEndereco"].ToString() + "; ";
                                    }
                                    else
                                    {
                                        string idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                                        ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                                        ssqlFinal += "(" + idCadEndereco + ", " + idDest + ",2, '" + pedido[i].Dest_Telefone.ToLower() + "'); ";
                                    }
                                }
                            }
                        }
                        /////////////////////////////////////////////////////////////////////////////////////////


                    }

                    #endregion

                    ssqlFinal = ssqlFinal.Replace("@ENDERECO@", pedido[i].Dest_ENDERECO);

                    if (pedido[i].Dest_NUMERO.Length > 9)
                        pedido[i].Dest_NUMERO = pedido[i].Dest_ENTREGA_NUMERO.Substring(0, 9);

                    ssqlFinal = ssqlFinal.Replace("@ENDERECONUMERO@", pedido[i].Dest_NUMERO);

                    comp = "";
                    comp = pedido[i].Dest_COMPLEMENTO;

                    if (comp != null && comp.Length > 50)
                        comp = comp.Substring(0, 50);
                    else
                        comp = comp;



                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCOMPLEMENTO@", comp);
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP@", pedido[i].Dest_CEP);

                    List<Pedido.Itens> item = pedido[i].itens;

                    if (item == null || item.Count == 0)
                        throw new Exception("Não foi possível receber o pedido. Motivo: Falta de itens.");

                    for (int iped = 0; iped < item.Count; iped++)
                    {
                        #region Verificações

                        if (item[iped].SKU.Length > 20)
                            item[iped].SKU = item[iped].SKU.Substring(0, 20);

                        if (item[iped].Descricao.Length > 60)
                            item[iped].Descricao = item[iped].Descricao.Substring(0, 60);

                        if (item[iped].CodigoNCM.Length > 10)
                            item[iped].CodigoNCM = item[iped].CodigoNCM.Substring(0, 10);

                        if (item[iped].EAN.Length > 20)
                            item[iped].EAN = item[iped].EAN.Substring(0, 20);

                        sql = "SELECT * FROM PRODUTOCLIENTE PC WHERE IDCLIENTE= " + dt.Rows[0]["IDCLIENTE"].ToString() + " AND CODIGO = '" + item[iped].SKU.Trim() + "'";
                        DataTable dProdCli = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                        string IdprodCli = "";
                        if (dProdCli.Rows.Count == 0)
                        {
                            IdprodCli = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOCLIENTE", cnx);

                            if (item[iped].Descricao.Trim().ToUpper().Length > 60)
                                item[iped].Descricao = item[iped].Descricao.Substring(0, 59);

                            ssqlFinal += "; INSERT INTO PRODUTOCLIENTE (IDProdutoCliente, IDCliente, IDUnidadeDeMedida,Codigo, Descricao,MetodoDeMovimentacao, DesmembraNaNF, SolicitarDataDeValidade,UnidadeDoFornecedor,Ativo, CodigoNCM, FatorUsoPosicaoPallet) ";
                            ssqlFinal += " VALUES(" + IdprodCli + ", " + dt.Rows[0]["IDCLIENTE"].ToString() + ", 1,'" + item[iped].SKU.ToUpper().Trim() + "', '" + item[iped].Descricao.Trim().ToUpper().Replace("'", "") + "','FEFO', 'NAO', 'SIM',1.00,'SIM', '" + item[iped].CodigoNCM + "', 1)";
                        }
                        else
                        {
                         
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

                            if (item[iped].PesoLiquido.ToString() == "")
                                item[iped].PesoLiquido = 0;

                            ssqlFinal += "; INSERT INTO PRODUTO (IDProduto, CodigoDeBarras, PesoLiquido, Especie, DataDeCadastro, PesoBruto) VALUES (" + idProd + ", '" + item[iped].EAN.Trim() + "', " + item[iped].PesoLiquido.ToString().Trim().Replace(",", ".") + ", 'UNI', Getdate(), " + item[iped].PesoLiquido.ToString().Trim().Replace(",", ".") + ")";
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
                        ssqlFinal += "; INSERT INTO DOCUMENTOPEDIDOITEM (IDDOCUMENTOITEM, IDDOCUMENTO,IDPRODUTOEMBALAGEM, IDUSUARIO, QUANTIDADE,VALORUNITARIO,VALORTOTALDOITEM,IDPRODUTOCLIENTE, QUANTIDADEUNIDADEESTOQUE, SALDO) ";
                        ssqlFinal += " values(" + id + ", " + iddoc + "," + idProdEmb + ",2, " + item[iped].Quantidade.ToString().Replace(",", ".") + "," + item[iped].ValorUnitario.ToString().Replace(",", ".") + ", " + vltot.ToString().Replace(",", ".") + "," + IdprodCli + ", " + item[iped].Quantidade.ToString().Replace(",", ".") + ", " + item[iped].Quantidade.ToString().Replace(",", ".") + ")";

                        #endregion
                    }

                   
                    if (pedido[i].OcorrenciasPedidosAnteriores != null)
                    {
                        for (int ih = 0; ih < pedido[i].OcorrenciasPedidosAnteriores.Count; ih++)
                        {
                            ssqlFinal += "; Insert into HistoricoPedRH (IdDocumento, Ocorrencia) values (" + iddoc + ", '" + pedido[i].OcorrenciasPedidosAnteriores[ih].ToString() + "') ";
                        }
                    }
                }

                if (ssqlFinal == "")
                    throw new Exception("Verifique o Documento. Já existe na base de dados.");

                ssqlFinal = ssqlFinal.Replace("@@EnderecoCoord@@", "");
                ssqlFinal = ssqlFinal.Replace("@CepCoord@", "");      

                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(ssqlFinal.ToUpper(), cnx);
                

                return "Documento(s) Gravados com Sucesso ";
            }
            catch (Exception ex)
            {               
                Err = ex.Message;
                return ex.Message;
            }
            //finally
            //{
            //    idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "ReceberPedidos", null, null, "Finalizou -" + Err, cnx);
            //    //AlterarCadastroDestinatario(pedido[0].Dest_CNPJCPF, pedido[0].Dest_BAIRRO, pedido[0].Latitude, pedido[0].Longitude, iddoc, cnx, cid, pedido[0].Dest_ENTREGA_ENDERECO);

            //}
        }

    

        private int RetornarCidade(string NomeCidade, string CEP, string cnx)
        {
            NomeCidade = removerAcentos(NomeCidade);

            string sql = "select IdCidade from Cidade where Nome = '" + NomeCidade + "' and IdEstado=26";
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdCidade"]);

            sql = "Select * from CidadeFaixaDeCep cfc  ";
            sql += " inner join Cidade c on c.IdCidade = cfc.IdCidade ";
            sql += " where "+CEP+" between CepInicial and CepFinal ";

            dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdCidade"]);

            return 0;
        }

        private int RetornarBairro(string Nome, string IdClidade, string cnx)
        {
            Nome = removerAcentos(Nome);
            int Id = 0;

            string sql = "select IdBairro from Bairro where Nome = '" + Nome + "' and IdCidade="+ IdClidade;
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdBairro"]);
            else
            {
                Id = int.Parse( Sistran.Library.GetDataTables.RetornarIdTabela("Bairro", cnx));
                sql = "Insert into Bairro (IdBairro, Nome, IdCidade) values (" + Id + ", '" + Nome + "', " + IdClidade + ")";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, cnx);
                return Id;
            }
        }


        public static string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        [WebMethod]
        public DataTable PosicaoEstoque(string Login, string Senha)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            int idLog = 0;
            string Err = "";
            try
            {


                idLog = Sistran.Library.GetDataTables.LogMetodo(null, "PosicaoEstoque", null, null, "EXEC PRC_ESTOQUE_HOMEREFIILL", cnx);

                string sql = "";
                sql = "SELECT * FROM USUARIO WHERE LOGIN='" + Login + "' AND SENHA='" + Senha + "' AND ATIVO='SIM'";
                DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                //if (d.Rows.Count == 0)
                //    throw new Exception("USUARIO OU SENHA INVALIDOS");
                //PREVISAO DE PEDIDO

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

            if (!IsCpf(s))
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
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
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
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
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
        public string Dest_Email { get; set; }
        public string Dest_Telefone { get; set; }
        public string DataParaEntrega { get; set; }
        public string CompraVenda { get; set; }
        public string PeriodoDeEntregaInicio { get; set; }
        public string PeriodoDeEntregaFim { get; set; }
        public List<Itens> itens { get; set; }
        public List<string> OcorrenciasPedidosAnteriores { get; set; }
        public bool? PrimeiraEntrega { get; set; }
        
        //public class HistoricoPedido
        //{
        //    public bool PrimeiraEntrega { get; set; }
        //    public List<string> Ocorrencias { get; set; }

        //}


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

        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    
}

public struct Pe
{
    public string IdCidadeP { get; set; }
    public string IdBairroP { get; set; }
    public string CEPP { get; set; }
    public string EnderecoP { get; set; }
    public string nBairroP { get; set; }
}

public static class ProcurarEndereco
{

    public static string removerAcentos(string texto)
    {
        string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
        string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

        for (int i = 0; i < comAcentos.Length; i++)
        {
            texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
        }
        return texto;
    }

    public  static int RetornarCidade(string NomeCidade, string CEP, string cnx)
    {
        NomeCidade = removerAcentos(NomeCidade).Trim();

        string sql = "select IdCidade from Cidade where Nome = '" + NomeCidade + "' and IdEstado=26 ";
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

        if (dt.Rows.Count > 0)
            return Convert.ToInt32(dt.Rows[0]["IdCidade"]);

        sql = "Select * from CidadeFaixaDeCep cfc  ";
        sql += " inner join Cidade c on c.IdCidade = cfc.IdCidade ";
        sql += " where " + CEP + " between CepInicial and CepFinal ";

        dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

        if (dt.Rows.Count > 0)
            return Convert.ToInt32(dt.Rows[0]["IdCidade"]);

        

        return 0;
    }

    public static int RetornarBairro(string Nome, string IdClidade, string cnx)
    {
        Nome = removerAcentos(Nome);

        string sql = "select IdBairro from Bairro where Nome = '" + Nome + "' and IdCidade=" + IdClidade;
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

        if (dt.Rows.Count > 0)
            return Convert.ToInt32(dt.Rows[0]["IdBairro"]);
        else
        {
                string Id = Sistran.Library.GetDataTables.RetornarIdTabela("Bairro", cnx);
                sql = "Insert into Bairro (IdBairro, Nome, IdCidade) values ("+Id+", '"+ Nome +"', "+IdClidade+")";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, cnx);
                return int.Parse(Id);
        }



       
    }
    public static Pe p;

    public static Pe ProcurarPeloGoogle(string Log, string Lat, string cnx, string NomeRua)
    {
        string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
        string requestUri = string.Format(baseUri, Lat, Log);
        p = new Pe();
        string url = requestUri;
        WebRequest request = WebRequest.Create(url);
        using (WebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                DataSet dsResult = new DataSet();
                dsResult.ReadXml(reader);
                if (dsResult.Tables["GeocodeResponse"].Rows[0]["status"].ToString().ToUpper() == "OK")
                {
                    string[] ret = dsResult.Tables["result"].Rows[0][1].ToString().Split(',');

                    if (ret.Length >= 5)
                    {
                        
                        p.EnderecoP = ret[0];
                        p.CEPP = ret[3].Replace("-", "").Trim();
                        p.nBairroP = ret[1].Replace("'", "");

                        try
                        {
                            int c = int.Parse(ret[3].Replace("-", ""));
                            p.IdCidadeP = RetornarCidade(ret[2].Replace(" - SP", ""), p.CEPP, cnx).ToString();
                            p.IdBairroP = RetornarBairro(p.nBairroP, p.IdCidadeP, cnx).ToString();
                            return p;
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                }
            }
        }
        return p;
    }

    public static Pe ProcurarPeloBing(string Log, string Lat, string cnx, string nomeRua)
    {
        string baseUri = "http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&key=HTyoRgOqBO4gN9SL8ceN~h2Uqv9KKr4_F1Lqe2cVBbw~AsHsYOFxZiU8Jv2aDKhDohShli1JAiPvcwFBHZzBC-IOB8cA6VDQkIoxFfh1Zug6";
        string requestUri = string.Format(baseUri, Lat, Log);
       

        string url = requestUri;
        WebRequest request = WebRequest.Create(url);
        using (WebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                DataSet dsResult = new DataSet();
                dsResult.ReadXml(reader);
                p = new Pe();

                if (dsResult.Tables["Response"].Rows[0]["statusDescription"].ToString().ToUpper() == "OK")
                {
                    string[] ret = dsResult.Tables["Location"].Rows[0]["Name"].ToString().Split(',');
                   

                    if (ret.Length >= 3)
                    {
                        if (ret.Length == 3)
                        {
                            p.CEPP = ret[1].Replace("-", "").Trim();
                        }
                        else if (ret.Length == 4)
                        {
                            p.EnderecoP = nomeRua.Replace("'", "");
                            p.CEPP = ret[2].Replace("-", "").Trim();
                            p.nBairroP = ret[0].Replace("'", "");
                        }
                        else
                        {
                            p.EnderecoP = ret[0];

                            if (ret[4].Replace("-", "").Trim().ToUpper() == "BRAZIL" || ret[4].Replace("-", "").Trim().ToUpper() == "BRASIL")
                                p.CEPP = ret[3].Replace("-", "").Trim();
                            else
                                p.CEPP = ret[4].Replace("-", "").Trim();

                            p.nBairroP = ret[1];

                        }

                        try
                        {
                            int c = int.Parse(p.CEPP);
                        }
                        catch (Exception)
                        {
                            for (int i = 1; i < dsResult.Tables["Location"].Rows.Count; i++)
                            {
                                
                               string[]  ret1 = dsResult.Tables["Location"].Rows[i]["Name"].ToString().Split(',');

                                if (ret1.Length == 6)
                                {
                                    p.EnderecoP = ret[0];

                                    if (ret1[4].Replace("-", "").Trim().ToUpper() == "BRAZIL" || ret1[4].Replace("-", "").Trim().ToUpper() == "BRASIL" || ret1[4].Replace("-", "").Trim().ToUpper().Contains("SP"))
                                        p.CEPP = ret1[3].Replace("-", "").Trim();
                                    else
                                        p.CEPP = ret1[4].Replace("-", "").Trim();

                                    p.nBairroP = ret1[1];

                                    try
                                    {
                                        int d = int.Parse(p.CEPP);
                                        p.nBairroP = ret1[2];

                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Não foi Possivel Localizzar o Endereco 04. Lat:" + Lat + ", long: " + Log);

                                    }
                                }
                            }

                            //throw new Exception("Não foi Possivel Localizzar o Endereco 03. Lat:"+ Lat + ", long: "+ Log);
                        }

                        try
                        {
                            if (ret[0].Contains("SP"))
                            {
                                p.IdCidadeP = RetornarCidade(ret[0].Trim().Replace("-", "").Replace("SP", ""), p.CEPP, cnx).ToString();
                                p.IdBairroP = "0";
                            }
                            else
                            {
                                p.IdCidadeP = RetornarCidade(ret[3].Trim().Replace("-", "").Replace("SP", ""), p.CEPP, cnx).ToString();
                                p.IdBairroP = RetornarBairro(ret[2].Trim(), p.IdCidadeP, cnx).ToString();
                            }
                        }
                        catch (Exception) { }

                    }                    
                }
                else
                    throw new Exception("Não foi Possivel Localizzar o Endereco 04. Lat:"+ Lat + ", long: "+ Log);

            }
        }
        return p;
    }   
}