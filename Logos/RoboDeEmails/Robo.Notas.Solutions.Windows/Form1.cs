using ServicosWEB;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AprovacaoRequisicao.Library;
using System.Xml;
using System.Net;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Drawing;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Collections.Generic;
//using Robo.Email.Notas.Solutions.Windows.Testes.InfraTracking;
using Robo.Email.Notas.Solutions.Windows.Testes.Josapar.OrderService;
using ServicosWEB.wsInfraStock;
using Robo.Email.Notas.Solutions.Windows.Testes.InfraTracking;

namespace Robo.Email.Notas.Solutions.Windows.Testes
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        #region E V E N T S
        private void Form1_Load(object sender, EventArgs e)
        {

            reiniciarTimers();


        }

        private void reiniciarTimers()
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
            Application.DoEvents();
        }

        DataTable dt;
        DateTime dataAtualizacao;
        int minRod = 1;
		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Enabled = false;

			try
			{

				DateTime horaAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
				DateTime horaean6 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
				DateTime horaean8 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
				DateTime horaean10 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
				DateTime horaean12 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
				DateTime horaean14 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0);
				DateTime horaean16 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0);
				DateTime horaean18 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);


				#region Açoes


				Label2.Text = DateTime.Now + "- Enviando Notas Do Sistranet para o Comprovei -  Conta Home Refill";
				Application.DoEvents();
				EnviarComprovei_HOMEREFILL();


				Label2.Text = DateTime.Now + "- Enviando Notas Do Sistranet para o Comprovei";
				Application.DoEvents();
				EnviarComprovei(true, false);


				Label2.Text = "Consultando Protocolo Comprovei";
				Application.DoEvents();
				ConsultarProtocolo(false);


				Label2.Text = DateTime.Now + "- Processando notas que o comprovei deu baixa";
				Application.DoEvents();
				AcertarNotasNoStistranetDoComprovei();

				if (checkBox2.Checked)
				{
					ProcessarComprovei();
					Label2.Text = DateTime.Now + "- Recebeu Baixa do Comprovei";
					Application.DoEvents();
				}

				//EnviarComprovei(false, false);
				//Label2.Text = DateTime.Now + "- Enviou Notas Do Sistranet para o Comprovei";
				//Application.DoEvents();

				if (checkBox1.Checked)
				{
					Label2.Text = DateTime.Now + "- Enviou Notas Do Sistranet para o Comprovei -  Conta Home Refill";
					Application.DoEvents();
					EnviarComprovei_HOMEREFILL();
				}


				//AcertarNotasNoStistranetDoComprovei();
				//Label2.Text = DateTime.Now + "- Processou notas que o comprovei deu baixa";
				//Application.DoEvents();

				Label2.Text = DateTime.Now + "- Enviou Notas Do Sistranet para o Comprovei";
				Application.DoEvents();
				EnviarComprovei(false, false);



				Label2.Text = DateTime.Now + "- Gravando pedidos Home Refill";
				Application.DoEvents();
				GravarPedidosHomeRefill();


				if (DateTime.Now.Minute % 5 == 0)
				{
					ConsultarProtocoloNovaTentativa();

				}

				if (chkHabilitaJosapar.Checked)
				{
					try
					{
						Label2.Text = "Enviando Pedido Josapar";
						Application.DoEvents();
						EnviarPedidoJojapar();
					}
					catch (Exception)
					{ }


					try
					{
						Label2.Text = "Enviando Tracking";
						Application.DoEvents();
						EnviarTrackinBD();
					}
					catch (Exception)
					{ }


					try
					{
						Label2.Text = "Enviando Estoque";
						Application.DoEvents();
						EnviarEstoqueJosapar();
					}
					catch (Exception)
					{ }


					try
					{
						Label2.Text = DateTime.Now + "- Processando Imagens Comprovei";
						Application.DoEvents();
						ProcessarImagensComprovei();
					}
					catch (Exception)
					{ }
				}

				#endregion

				Label2.Text = DateTime.Now + "- Último Processamento";
				Application.DoEvents();

			}
			catch (Exception ex)
			{
				reiniciarTimers();
				Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Timer ", "erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Erro no Timer");


			}
			finally
			{
				timer1.Enabled = true;
			}
		}

        string log = "";
        string ssqlFinal = "";

        private void GravarPedidosHomeRefill()
        {
            string ultimos = "";

            //limpa Pedidos espelho menores que a data de hoje
            Sistran.Library.GetDataTables.RetornarDataTableWin("UPDATE DOCUMENTO SET ATIVO='NAO' WHERE IDCLIENTE = 150000 AND TIPODEDOCUMENTO='PEDIDO' AND SERIE = 'ESP' AND DATAPLANEJADA < GETDATE() AND ATIVO='SIM' ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            string ItemZerado = "";
            try
            {
                // string sql = " SELECT * FROM DOCUMENTOPEDIDO DP INNER JOIN DOCUMENTOPEDIDOITEM DPI ON DPI.IDDOCUMENTO = DP.IDDOCUMENTO WHERE DP.IDDOCUMENTO=52776";
                string sql = " SELECT distinct * FROM DOCUMENTOPEDIDO DP INNER JOIN DOCUMENTOPEDIDOITEM DPI ON DPI.IDDOCUMENTO = DP.IDDOCUMENTO WHERE DATADOPROCESSAMENTO IS NULL AND  DP.IDCLIENTE=150000 AND ATIVO='SIM' and IDEnderecoCidade>0  and IDEnderecoBairro>0   order by 1 desc";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "IdDocumento");
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();


                for (int i = 0; i < distinctValues.Rows.Count; i++)
                //for (int i = 0; i < (distinctValues.Rows.Count>5 ? 5 : distinctValues.Rows.Count); i++)
                    {
                    try
                    {

                   
                    
                    ssqlFinal = "";

                    DataRow[] item = dt.Select("IdDocumento=" + distinctValues.Rows[i][0].ToString(), "");
                    log = distinctValues.Rows[i][0].ToString();

                    sql = " SELECT count(*)  FROM DOCUMENTO WHERE IDCLIENTE=" + item[0]["idcliente"].ToString() + " AND NUMERO ='" + item[0]["Numero"].ToString() + "' AND TIPODEDOCUMENTO='PEDIDO' AND SERIE = '" + item[0]["Serie"].ToString() + "' AND ENTRADASAIDA = '" + item[0]["ENTRADASAIDA"].ToString() + "' AND ATIVO='SIM' AND DATADECANCELAMENTO IS NULL ";

                    if (Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx).Rows[0][0].ToString() != "0")
                    {
                        ssqlFinal += "; UPDATE DOCUMENTOPEDIDO SET DATADOPROCESSAMENTO=GETDATE() WHERE IDDOCUMENTO = " + distinctValues.Rows[i][0].ToString();
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(ssqlFinal.ToUpper(), cnx);
                        continue;
                    }


                    ssqlFinal += " INSERT INTO DOCUMENTO (IDDOCUMENTO, IDCLIENTE, IDREMETENTE, IDFILIAL, IDFILIALATUAL,  NUMERO, SERIE, TIPODEDOCUMENTO, IDDESTINATARIO,  ORIGEM, DATADEEMISSAO, DATADEENTRADA, ENDERECO, ENDERECONUMERO, ENDERECOCOMPLEMENTO, IDENDERECOBAIRRO, IDENDERECOCIDADE, ENDERECOCEP, ANOMES, DATADECANCELAMENTO, ENTRADASAIDA, ATIVO, TIPODESERVICO, IDTES, IDTESCFOP, DATAPLANEJADA, PERIODODEENTREGAINICIO, PERIODODEENTREGAFIM, PrimeiraEntrega) VALUES";

                    if (item[0]["ENTRADASAIDA"].ToString() == "ENTRADA")
                        ssqlFinal += " (@IDDOCUMENTO, @IDCLIENTE, @IDREMETENTE, @IDFILIAL@, @IDFILIALATUAL@,  '@NUMERO', '@SERIE', '@TIPODEDOCUMENTO', @IDDESTINATARIO,  'WEBSERVICE', '@DATADEEMISSAO', GETDATE(),  '@ENDERECO_ENTREGA@', '@ENDERECONUMERO_ENTREGA@', '@ENDERECOCOMPLEMENTO_ENTREGA@', @IDENDERECOBAIRRO_ENTREGA@, @IDENDERECOCIDADE_ENTREGA@, '@ENDERECOCEP_ENTREGA@', '@ANOMES', @DATADECANCELAMENTO@, '@ENTRADASAIDA@', '@ATIVO@' , 'NORMAL', 4867, 4979, @DATAPLANEJADA@, @PERIODODEENTREGAINICIO@, @PERIODODEENTREGAFIM@,0) ";
                    else
                        ssqlFinal += " (@IDDOCUMENTO, @IDCLIENTE, @IDREMETENTE, @IDFILIAL@, @IDFILIALATUAL@,  '@NUMERO', '@SERIE', '@TIPODEDOCUMENTO', @IDDESTINATARIO,  'WEBSERVICE', '@DATADEEMISSAO', GETDATE(),  '@ENDERECO_ENTREGA@', '@ENDERECONUMERO_ENTREGA@', '@ENDERECOCOMPLEMENTO_ENTREGA@', @IDENDERECOBAIRRO_ENTREGA@, @IDENDERECOCIDADE_ENTREGA@, '@ENDERECOCEP_ENTREGA@', '@ANOMES', @DATADECANCELAMENTO@, '@ENTRADASAIDA@', '@ATIVO@' , 'NORMAL', 4, 37, @DATAPLANEJADA@, @PERIODODEENTREGAINICIO@, @PERIODODEENTREGAFIM@, '@PRIMEIRAENTREGA@') ";

                    string iddoc = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTO", cnx);


                    ssqlFinal = ssqlFinal.Replace("@IDDOCUMENTO", iddoc);
                    ssqlFinal = ssqlFinal.Replace("@IDCLIENTE", item[0]["IDCliente"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", item[0]["IDRemetente"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@IDFILIAL@", item[0]["IDFilial"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@IDFILIALATUAL@", item[0]["IDFilial"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@NUMERO", item[0]["Numero"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@SERIE", item[0]["Serie"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@TIPODEDOCUMENTO", item[0]["TipoDeDocumento"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", item[0]["IDDestinatario"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@DATADEEMISSAO", DateTime.Parse(item[0]["DataDeEmissao"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                    ssqlFinal = ssqlFinal.Replace("@ENDERECO_ENTREGA@", item[0]["Endereco"].ToString().ToUpper().Replace("-", "").Replace("CONDOMÍNIO", "").Replace("EDIFÍCIO", ""));
                    ssqlFinal = ssqlFinal.Replace("@ENDERECONUMERO_ENTREGA@", item[0]["EnderecoNumero"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCOMPLEMENTO_ENTREGA@", item[0]["EnderecoComplemento"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", item[0]["IDEnderecoBairro"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE_ENTREGA@", item[0]["IDEnderecoCidade"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP_ENTREGA@", item[0]["EnderecoCep"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@ANOMES", item[0]["AnoMes"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@DATADECANCELAMENTO@", (item[0]["DataDeCancelamento"].ToString() == "" ? "NULL" : "'" + DateTime.Parse(item[0]["DataDeCancelamento"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'"));
                    ssqlFinal = ssqlFinal.Replace("@ENTRADASAIDA@", item[0]["EntradaSaida"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@ATIVO@", item[0]["ATIVO"].ToString());

                    ssqlFinal = ssqlFinal.Replace("@PERIODODEENTREGAINICIO@", (item[0]["PERIODODEENTREGAINICIO"].ToString() == "" ? "null" : "'" + DateTime.Parse(item[0]["PERIODODEENTREGAINICIO"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'"));
                    ssqlFinal = ssqlFinal.Replace("@PERIODODEENTREGAFIM@", (item[0]["PERIODODEENTREGAFIM"].ToString() == "" ? "null" : "'" + DateTime.Parse(item[0]["PERIODODEENTREGAFIM"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'"));

                    string dp = item[0]["DATAPLANEJADA"].ToString();
                    ssqlFinal = ssqlFinal.Replace("@DATAPLANEJADA@", (dp == "" ? "null" : "'" + DateTime.Parse(dp).ToString("yyyy-MM-dd") + "'"));


                    string pp = item[0]["PrimeiroPedido"].ToString();
                    ssqlFinal = ssqlFinal.Replace("@PRIMEIRAENTREGA@", (pp == "1" ? "SIM" : "NAO"));


                    for (int iped = 0; iped < item.Length; iped++)
                    {
                        if (float.Parse(item[iped]["Quantidade"].ToString().Replace(",", ".")) > 0)
                        {

                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOITEM", cnx);
                            ssqlFinal += "; INSERT INTO DOCUMENTOITEM (IDDocumentoItem, IDDocumento,IDProdutoEmbalagem, IDUsuario, Quantidade,ValorUnitario,ValorTotalDoItem,IdProdutoCliente, QuantidadeUnidadeEstoque, Saldo, IDTes, IDTesCFOP, IDCFOP) ";

                            if (item[0]["ENTRADASAIDA"].ToString() == "ENTRADA")
                                ssqlFinal += " values(" + id + ", " + iddoc + "," + item[iped]["IDProdutoEmbalagem"].ToString() + ",2, " + item[iped]["Quantidade"].ToString().Replace(",", ".") + "," + item[iped]["ValorUnitario"].ToString().Replace(",", ".") + ", " + item[iped]["ValorTotalDoItem"].ToString().Replace(",", ".") + "," + item[iped]["IdProdutoCliente"].ToString() + ", " + item[iped]["Quantidade"].ToString().Replace(",", ".") + ", " + item[iped]["Quantidade"].ToString().Replace(",", ".") + ", 4867, 4979, null)";
                            else
                                ssqlFinal += " values(" + id + ", " + iddoc + "," + item[iped]["IDProdutoEmbalagem"].ToString() + ",2, " + item[iped]["Quantidade"].ToString().Replace(",", ".") + "," + item[iped]["ValorUnitario"].ToString().Replace(",", ".") + ", " + item[iped]["ValorTotalDoItem"].ToString().Replace(",", ".") + "," + item[iped]["IdProdutoCliente"].ToString() + ", " + item[iped]["Quantidade"].ToString().Replace(",", ".") + ", " + item[iped]["Quantidade"].ToString().Replace(",", ".") + ", 4, 37, 117)";
                        }
                        else
                        {
                            ItemZerado = "Pedido: " + item[0]["Numero"].ToString() + " com Produto Qantidade Zerada.  IdDocumento: " + iddoc + " - IdProdutoEmbalagem: " + item[iped]["IDProdutoEmbalagem"].ToString() + " - IdProdutoCliente: " + item[iped]["IDProdutoCliente"].ToString();
                        }
                    }

                    string iddocfil = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOFILIAL", cnx);
                    if (item[0]["ENTRADASAIDA"].ToString() == "ENTRADA")
                    {
                        ssqlFinal += "; INSERT INTO DOCUMENTOFILIAL (IDDocumentoFilial,IDDocumento,IDFilial,Situacao, Data, IDRegiaoItem) VALUES(" + iddocfil + "," + iddoc + "," + item[0]["IDFilial"].ToString() + ",'PEDIDO DE ENTRADA', GETDATE(), 1)";

                    }
                    else
                    {
                        if (item[0]["TIPODEPEDIDO"].ToString().ToUpper().ToUpper() == "EFETIVO" || item[0]["TIPODEPEDIDO"].ToString().ToUpper() == "")
                        {
                            ssqlFinal += "; INSERT INTO DOCUMENTOFILIAL (IDDocumentoFilial,IDDocumento,IDFilial,Situacao, Data, IDRegiaoItem) VALUES(" + iddocfil + "," + iddoc + "," + item[0]["IDFilial"].ToString() + ",'AGUARDANDO LIBERACAO', GETDATE(), 1)";

                        }
                        else
                        {
                            ssqlFinal += "; INSERT INTO DOCUMENTOFILIAL (IDDocumentoFilial,IDDocumento,IDFilial,Situacao, Data, IDRegiaoItem) VALUES(" + iddocfil + "," + iddoc + "," + item[0]["IDFilial"].ToString() + ",'PREVISAO DE PEDIDO', GETDATE(), 1)";
                            ssqlFinal += ";UPDATE DOCUMENTOITEM SET SALDO=0, ESTOQUEPROCESSADO='SIM' WHERE IDDOCUMENTO=" + iddoc + "; ";
                        }

                    }
                    ssqlFinal += "; UPDATE DOCUMENTOPEDIDO SET DATADOPROCESSAMENTO=GETDATE() WHERE IDDOCUMENTO = " + distinctValues.Rows[i][0].ToString();


                    if (item[0]["ATIVO"].ToString() == "NAO")
                    {
                        ssqlFinal += ";UPDATE DOCUMENTOITEM SET SALDO=0, ESTOQUEPROCESSADO='SIM' WHERE IDDOCUMENTO=" + iddoc + "; ";
                        ssqlFinal += "UPDATE DOCUMENTOFILIAL SET SITUACAO='DOCUMENTO CANCELADO', DATA=GETDATE() WHERE IDDOCUMENTO = " + iddoc + "; ";
                        ssqlFinal += "UPDATE DOCUMENTO SET DATADECANCELAMENTO=GETDATE(), ATIVO='NAO' WHERE IDDOCUMENTO=" + iddoc + "; ";
                    }

                    ultimos = ssqlFinal.ToUpper();
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(ssqlFinal.ToUpper(), cnx);
                  

                  
                    if (item[0]["ENTRADASAIDA"].ToString() == "SAIDA")
                    {
                        //Atualiza o Peso na nota
                        
                        sql = "SELECT PC.IDPRODUTOCLIENTE, DI.QUANTIDADE, MAX(P.PESOBRUTO) PESO, ISNULL(DI.VALORUNITARIO,0) VALORUNITARIO, MAX(P.PESOBRUTO) * DI.QUANTIDADE PesoCalculado";
                        sql += " FROM DOCUMENTOITEM  DI ";
                        sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = DI.IDPRODUTOCLIENTE ";
                        sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
                        sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                        sql += " WHERE IDDOCUMENTO=" + iddoc;
                        sql += " GROUP BY PC.IDPRODUTOCLIENTE , DI.QUANTIDADE, ISNULL(DI.VALORUNITARIO,0)";

                        DataTable dtPeso = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                        decimal peso = 0;//decimal.Parse(dtPeso.Compute("SUM(PESO*QUANTIDADE)", "").ToString());
                        decimal vlTotal = 0;// decimal.Parse(dtPeso.Compute("SUM(QUANTIDADE*VALORUNITARIO)", "").ToString());

                        for (int ipe = 0; ipe < dtPeso.Rows.Count; ipe++)
                        {
                            peso += decimal.Parse(dtPeso.Rows[ipe]["PesoCalculado"].ToString());
                            vlTotal += decimal.Parse(dtPeso.Rows[ipe]["VALORUNITARIO"].ToString()) * decimal.Parse(dtPeso.Rows[ipe]["Quantidade"].ToString());
                        }



                        ssqlFinal = "";
                        ssqlFinal += "Update Documento set PesoBruto=" + peso.ToString().Replace(",", ".") + ", ValorDaNota=" + vlTotal.ToString().Replace(",", ".") + ", DataDeEntrada=GetDate(), PeriodoEntregaInicio='" + DateTime.Parse(item[0]["PERIODODEENTREGAINICIO"].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "',  PeriodoEntregaFim ='" + DateTime.Parse(item[0]["PERIODODEENTREGAFIM"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'  where IdDocumento = " + iddoc;

                        //Calcula o Frete
                        decimal vlFrete = (decimal)(3.5 / 100) * vlTotal;

                        string iddocfrete = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOFRETE", cnx);
                        ssqlFinal += "; INSERT INTO DOCUMENTOFRETE (IDDOCUMENTOFRETE, IDDOCUMENTO, IDFILIAL,IDPAGADORDOFRETE, IDSERVICO,FRETE, FRETEPESO) VALUES (" + iddocfrete + ", " + iddoc + ", " + item[0]["IDFilial"].ToString() + ",150000, 1 ," + vlFrete.ToString().Replace(",", ".") + ", " + vlFrete.ToString().Replace(",", ".") + ") ";

                        if (item[0]["PERIODODEENTREGAINICIO"].ToString() != "")
                        {
                            string idobs = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOBSERVACAO", cnx);
                            ssqlFinal += "; INSERT INTO DOCUMENTOOBSERVACAO (IDDOCUMENTOOBSERVACAO,IDDOCUMENTO,OBSERVACAO) VALUES (" + idobs + ", " + iddoc + ", 'PERÍODO DE ENTREGA: " + DateTime.Parse(item[0]["PERIODODEENTREGAINICIO"].ToString()).ToString("dd/MM/yyyy HH:mm:ss") + " até " + DateTime.Parse(item[0]["PERIODODEENTREGAFIM"].ToString()).ToString("dd/MM/yyyy HH:mm:ss") + " ') ";
                        }

                        ultimos = ssqlFinal.ToUpper();

                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(ssqlFinal.ToUpper(), cnx);

                        //ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", item[0]["IDEnderecoBairro"].ToString());
                        //ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE_ENTREGA@", item[0]["IDEnderecoCidade"].ToString());

                        if (item[0]["ENTRADASAIDA"].ToString() == "SAIDA")
                        {

                            DataTable dtDest = Sistran.Library.GetDataTables.RetornarDataTableWS("Select IdCadastro, isnull(IdBairro, 0) IdBairro , isnull(IdCidade,0) IdCidade from Cadastro where IdCadastro = " + item[0]["IDDestinatario"].ToString(), cnx);

                            if (dtDest.Rows.Count > 0)
                            {
                                if (dtDest.Rows[0]["IdCidade"].ToString() == "0" || dtDest.Rows[0]["IdBairro"].ToString() == "0")
                                {
                                    sql = "UPDATE CADASTRO SET IDBAIRRO=" + item[0]["IDEnderecoBairro"].ToString() + " , IDCIDADE= " + item[0]["IDEnderecoCidade"].ToString() + "  WHERE IDCADASTRO=" + dtDest.Rows[0]["IdCadastro"].ToString();
                                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql.ToUpper(), cnx);
                                    dtDest = Sistran.Library.GetDataTables.RetornarDataTableWS("Select IdCadastro, isnull(IdBairro, 0) IdBairro , isnull(IdCidade,0) IdCidade from Cadastro where IdCadastro = " + item[0]["IDDestinatario"].ToString(), cnx);
                                }
                            }

                            try
                            {
                                sql = "Update HistoricoPedRH set IdDocumento = " + iddoc + " where IdDocumento=" + item[0]["IdDocumento"].ToString();
                                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql.ToUpper(), cnx);
                            }
                            catch (Exception)
                            {
                                log = log;

                            }
                            //Coloca o IdDocumento Correto na tabela de Historico


                        }

                    }


                    if (ItemZerado.Length > 0)
                    {
                        try
                        {
                            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "ItemZerado", "erro em: " + DateTime.Now.ToString() + "-" + ItemZerado, "mail.grupologos.com.br", "logos0902", "Item Zerado Home Refill");
                        }
                        catch (Exception)
                        {
                        }
                    }
                    }
                    catch (Exception xx)
                    {
                        //Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Timer ", "erro em: " + DateTime.Now.ToString() + "-" + xx.Message + ssqlFinal, "mail.grupologos.com.br", "logos0902", "Erro no Timer");

                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                reiniciarTimers();
                log = log;
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Timer ", "erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException + ssqlFinal, "mail.grupologos.com.br", "logos0902", "Erro no Timer");
            }
            finally
            {
                timer1.Enabled = true;
            }
        }

        private void AcertarNotasNoStistranetDoComprovei()
        {
            string docAtual = "";
            try
            {
                string sql = "SELECT TOP 1000 COMP.*, D.IDFILIALATUAL  FROM RETORNOCOMPROVEI COMP WITH (NOLOCK) INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = COMP.IDDOCUMENTO WHERE PROCESSADO IS NULL /*AND D.IDDOCUMENTO=13048980 */ and D.IdDocumento not in (Select IdDocumento from Bloquear)  ORDER BY comp.IdRetornoComprovei desc ";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string[] oco = dt.Rows[i]["OCORRENCIA"].ToString().Split('-');
                    oco[0] = oco[0].Trim();
                    oco[1] = oco[1].Trim();


                    Label2.Text = DateTime.Now + "-" + "AcertarNotasNoStistranetDoComprovei. IdDocumento: " + dt.Rows[i]["IDDOCUMENTO"].ToString() + "| - " + i + 1 + " De " + dt.Rows.Count;
                    Application.DoEvents();
                    docAtual = dt.Rows[i]["IDDOCUMENTO"].ToString();


                    string sqlaux = "SELECT * FROM OCORRENCIA WHERE IDOCORRENCIASERIE=3 AND CODIGO='" + oco[0] + "' ";
                    DataTable dtOco = Sistran.Library.GetDataTables.RetornarDataTableWin(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    int idOcorrencia = 0;
                    string finalzadora = "SIM";

                    // se nao existir a ocorrencia insere
                    if (dtOco.Rows.Count == 0)
                    {
                        GravarLog("Gravando Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

                        idOcorrencia = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("OCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
                        sqlaux = "insert into ocorrencia (IDOcorrencia, IDEmpresa, IDOcorrenciaAcao, Codigo, Nome, Responsabilidade, NomeReduzido, PagaEntrega, Finalizador, Sistema,  Ativo, RestringirCarregamento, AbrirFecharOcorrencia, ApareceSiteCliente, IdOcorrenciaSerie)";
                        sqlaux += "VALUES (" + idOcorrencia + ", NULL, 5, '" + oco[0] + "', '" + (oco[1].Trim().ToUpper().Length >= 60 ? oco[1].Trim().ToUpper().Substring(0, 59) : oco[1].Trim().ToUpper()) + "', 'CLIENTE', '" + (oco[1].Trim().ToUpper().Length >= 30 ? oco[1].Trim().ToUpper().Substring(0, 29) : oco[1].Trim().ToUpper()) + "', 'NAO', 'SIM', NULL,  'NAO', 'NAO', 'AMBOS', NULL, 3)";
                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                    else
                    {
                        idOcorrencia = int.Parse(dtOco.Rows[0]["IDOCORRENCIA"].ToString());
                        finalzadora = dtOco.Rows[0]["FINALIZADOR"].ToString();
                    }


                    //Verifico se ja tem a ocorrencia do comprovei
                    string strsql = "";
                    sql = "SELECT * FROM DOCUMENTOOCORRENCIA with (nolock) WHERE IDOCORRENCIACOMPROVEI= " + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " and IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString();
                    DataTable ret = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    if (ret.Rows.Count == 0)
                    {
                        //insere a ocorrencia
                        AcertarDadosDTRomaneio(int.Parse(dt.Rows[i]["IDDOCUMENTO"].ToString()));

                        //verifico se ja tem alguma ocorrencia feita pelo usuario do sistema
                        sql = "SELECT top 1 * FROM DOCUMENTOOCORRENCIA WITH (NOLOCK)  WHERE IDDOCUMENTO =" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " AND IDOCORRENCIA= (SELECT top 1 IDOCORRENCIA FROM OCORRENCIA WHERE CODIGO='" + oco[0] + "' and IDOCORRENCIASERIE=3) AND IDUSUARIO IS NOT NULL order by IdDocumentoOcorrencia Desc";
                        DataTable aux = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        DataTable existeFoto = new DataTable();
                        if (aux.Rows.Count > 0)
                        {
                            existeFoto = Sistran.Library.GetDataTables.RetornarDataTableWin("select top 1 * from DocumentoOcorrenciaArquivo where IddocumentoOcorrencia= " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                            if (aux.Rows.Count > 0)
                            {

                                DateTime dataDaOcorrencia = DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString());
                                DateTime dataOcorrenciaJaExistente = DateTime.Parse(aux.Rows[0]["DataOcorrencia"].ToString());

                                if (dataDaOcorrencia.ToString("dd/MM/yyyy HH:mm") == dataOcorrenciaJaExistente.ToString("dd/MM/yyyy HH:mm"))
                                {

                                    if (existeFoto.Rows.Count > 0)
                                        strsql = "UPDATE DOCUMENTOOCORRENCIAARQUIVO SET ARQUIVO=(select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ") WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
                                    else
                                    {

                                        if (dt.Rows[i]["foto"].ToString() != "")
                                        {
                                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                            strsql = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                                        }
                                    }

                                    strsql += "UPDATE DOCUMENTOOCORRENCIA SET IDOCORRENCIACOMPROVEI=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                                    continue;
                                }


                                if (dataDaOcorrencia.ToString("dd/MM/yyyy") == dataOcorrenciaJaExistente.ToString("dd/MM/yyyy"))
                                {

                                    if (existeFoto.Rows.Count > 0)
                                        strsql = "UPDATE DOCUMENTOOCORRENCIAARQUIVO SET ARQUIVO=(select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ") WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
                                    else
                                    {
                                        if (dt.Rows[i]["foto"].ToString() != "")
                                        {
                                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                            strsql = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                                        }
                                    }

                                    strsql += "UPDATE DOCUMENTOOCORRENCIA SET DataOcorrencia='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' ,IDOCORRENCIACOMPROVEI=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";

                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                                    continue;
                                }


                            }
                        }


                        int IdDocOco = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));

                        strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
                        strsql += " IDDocumentoOcorrencia, ";
                        strsql += " IdRomaneio, ";
                        strsql += " IDDocumento,";
                        strsql += " IDFilial,";
                        strsql += " IDOcorrencia,";
                        strsql += " DataOcorrencia,";
                        strsql += " Descricao,";
                        strsql += " Sistema,";
                        strsql += "IdOcorrenciaComprovei";
                        strsql += " ) VALUES (";
                        strsql += IdDocOco + " ,";
                        strsql += "ISNULL((SELECT TOP 1 ISNULL(RD.IDROMANEIO,null) FROM ROMANEIODOCUMENTO RD WITH (NOLOCK)  INNER JOIN ROMANEIO R ON R.IDROMANEIO = RD.IDROMANEIO WHERE RD.IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " AND  R.TIPO IN ('ENTREGA', 'COLETA') ORDER BY 1 DESC),null)" + " ,";
                        strsql += dt.Rows[i]["IDDOCUMENTO"].ToString() + " ,";
                        strsql += Convert.ToInt32(dt.Rows[i]["IDFILIALATUAL"].ToString()) + " ,";

                        ////se a data de conclusao for null coloca a ocorrencia se nao apenas uma observação que se caracteriza pelo null no idocorrencia

                        //if (finalzadora == "SIM")
                        strsql += int.Parse(idOcorrencia.ToString()) + " ,";
                        //else
                        //    strsql += " null ,";


                        strsql += "'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' ,";
                        strsql += " '" + dt.Rows[i]["Ocorrencia"].ToString().Trim() + " - Comprovei',";
                        strsql += "'SIM',";
                        strsql += dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " );   ";


                        if (finalzadora == "SIM")
                        {
                            strsql += "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + ", DATADECONCLUSAO= '" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "'  WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                            strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='PROCESSO FINALIZADO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                            //strsql += " UPDATE DOCUMENTO SET DATADECONCLUSAO= convert(datetime,'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "', 103)" + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "; "; 
                        }
                        else
                        {
                            string x = "SELECT COUNT(*) FROM DOCUMENTOFILIAL WHERE SITUACAO='PROCESSO FINALIZADO' AND IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString();
                            DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWin(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                            if (dtx.Rows[0][0].ToString() == "0")
                            {
                                if (dtOco.Rows[0]["RestringirCarregamento"].ToString() == "" || dtOco.Rows[0]["RestringirCarregamento"].ToString() == "NAO")
                                {
                                    strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO EMBARQUE' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                                }
                                else
                                {
                                    strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO SOLUCAO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                                }
                                strsql += " UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                            }
                        }

                        if (dt.Rows[i]["foto"].ToString() != "")
                        {
                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                            strsql += "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + IdDocOco.ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                        }

                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                        //SE INSERIR A DATA DE CONCLUSAO CALCULA O PRAZO UTILIZADO
                        if (finalzadora == "SIM")
                            Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(" EXEC SP_PRAZO_UTILIZADO_ID " + dt.Rows[i]["IDDOCUMENTO"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    }
                    else
                    {
                        strsql = "";
                        //if (finalzadora == "SIM")
                        //{
                        //    strsql += "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + ret.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", DATADECONCLUSAO= '" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "'  WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                        //    strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='PROCESSO FINALIZADO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                        //    //strsql += " UPDATE DOCUMENTO SET DATADECONCLUSAO= convert(datetime,'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "', 103)" + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "; ";
                        //}
                        //else
                        //{
                        //    string x = "SELECT COUNT(*) FROM DOCUMENTOFILIAL WHERE SITUACAO='PROCESSO FINALIZADO' AND IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString();
                        //    DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWin(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        //    if (dtx.Rows[0][0].ToString() == "0")
                        //    {
                        //        if (dtOco.Rows[0]["RestringirCarregamento"].ToString() == "" || dtOco.Rows[0]["RestringirCarregamento"].ToString() == "NAO")
                        //        {
                        //            strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO EMBARQUE' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                        //        }
                        //        else
                        //        {
                        //            strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO SOLUCAO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                        //        }

                        //        strsql += " UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + ret.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                        //    }
                        //}

                        //if (dt.Rows[i]["foto"].ToString() != "")
                        //{
                        //    string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        //    strsql += "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + ret.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                        //}

                        // strsql = "Update RetornoComprovei set Processado=getdate() where IdOcorrenciaComprovei=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString();

                        if (strsql.Length > 10)
                            Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    }
                }
            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "AcertarNotasNoStistranetDoComprovei ", "Documento: " + docAtual + ". Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Erro no Timer");
                reiniciarTimers();
            }
        }

        string sss = "";
        public void ProcessarComprovei()
        {

            try
            {
                //  return;

                //GravarLog("Iniciou", "ProcessarTwx");
                Label2.Text = "ProcessarTwx Inicio: " + DateTime.Now.ToString();


                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                //GravarLog("Ambiente: " + ambiente, "ProcessarTwx");
                //GravarLog("URL: " + tbUrl, "ProcessarTwx");


                string varName = "";
                string varType = "";
                string xmlPath = "";
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));

                varName = "qtdDocumentos";
                varType = "integer";

                xmlPath = "Retorno/Documentos/Documento";
                xmlPath = "Retorno/Documentos/item";

                //tbUrl = "http://ws.comprovei.com.br/server.php?wsdl";
                tbUrl = "http://soap.comprovei.com.br/WebServicePOD/server.php?wsdl";

                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
                string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
                               "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
                               "<SOAP-ENV:Body>" +
                               "<tns:getDocumentsStatus xmlns:tns=\"urn:WebServicePOD\">" +
                               "<" + varName + " xsi:type=\"xsd:" + varType + "\">" + ConfigurationSettings.AppSettings["QtdDocumentosPorChamada"] + "</" + varName + ">" +
                               "</tns:" + "getDocumentsStatus" + ">" +
                               "</SOAP-ENV:Body></SOAP-ENV:Envelope>";





                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=ISO-8859-1";
                request.Headers.Add("SOAPAction", "urn:WebServicePOD#getDocumentsFromPOD");
                request.Headers.Add("Authorization", "Basic " + auth);
                request.ContentLength = data.Length;


                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }


                Cursor.Current = Cursors.WaitCursor;
                WebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());

                XmlDocument xmlAwnser = new XmlDocument();
                xmlAwnser.LoadXml(sr.ReadToEnd());

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

                string documents = "";
                int countDocument = 1;

                // GravarXML(xmlAwnser.InnerXml, DateTime.Now);

                if (xmlAwnser.InnerText.Contains("No documents done yet!"))
                {
                    request = null;
                    response = null;
                    xmlAwnser = null;

                    Cursor.Current = Cursors.Default;
                    Label2.Text = "ProcessarTwx FIM: " + DateTime.Now.ToString();
                    Thread.Sleep(30000);
                    return;
                }


                //int idXML = GravarXml(xmlAwnser);                

                int xx = 0;
                foreach (XmlNode xmlDocument in xmlAwnser.SelectNodes(xmlPath))
                {
                    xx += 1;
                    //GravarLog("", "");
                    //GravarLog("---------------------------------------------------------------------------------------------------", "");
                    GravarLog("##: " + countDocument, "");


                    if (xmlDocument["Erro"] == null)
                    {
                        string sKey = (xmlDocument["Chave"] != null) ? xmlDocument["Chave"].InnerText : "";
                        GravarLog("Chave da Nota: " + sKey, "ProcessarTwx");
                        sss = sKey;

                        //PEGA A NOTA
                        string sql = "SELECT IDDOCUMENTO FROM DOCUMENTO with (nolock) WHERE /*TIPODEDOCUMENTO IN('NOTA FISCAL', 'ORDEM DE SERVICO', 'LOGISTICA') AND TIPODESERVICO in ('TRANSPORTE','ENTREGA') AND*/ DOCUMENTODOCLIENTE4='" + sKey + "'";
                        DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        if (dtNota.Rows.Count == 0)
                        {
                            sql = "select IDDOCUMENTO from DocumentoEletronico where IdNota = '" + sKey + "'";
                            dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        }

                        //se nao achar a nota vai para a proxima
                        if (dtNota.Rows.Count == 0)
                        {
                            GravarLog("Não Encontro o documento:" + sKey, "ProcessarTwx");
                            continue;
                        }

                        gravarTabelasAuxiliares(xmlDocument, int.Parse(dtNota.Rows[0]["IDDOCUMENTO"].ToString()));


                        //Antes pegava direto do webservice

                        //GravarLog("IDDOCUMENTO: " + dtNota.Rows[0]["IDDOCUMENTO"].ToString(), "ProcessarTwx");
                        //GravarLog("NUMERO: " + dtNota.Rows[0]["Numero"].ToString(), "ProcessarTwx");

                        //int qtdOcorrencia = 0;
                        //if (xmlDocument.SelectNodes("Ocorrencias/Ocorrencia") != null)
                        //{
                        //    qtdOcorrencia = xmlDocument.SelectNodes("Ocorrencias/Ocorrencia").Count;
                        //}

                        //for (int i = qtdOcorrencia; i >= 1; i--)
                        //{
                        //    XmlNode xmlOccurrence = xmlDocument.SelectNodes("Ocorrencias/Ocorrencia")[i - 1];

                        //    }


                        //    foreach (XmlNode xmlOccurrence in xmlDocument.SelectNodes("Ocorrencias/Ocorrencia"))
                        //    {
                        //    string occurenceNumber = xmlOccurrence["Numero"].InnerText;
                        //    string[] oco = xmlOccurrence["Motivo"].InnerText.Split('-');


                        //    oco[0] = oco[0].Trim();
                        //    oco[1] = oco[1].Trim();

                        //    GravarLog("NumeroOcorrencia: " + occurenceNumber, "ProcessarTwx");
                        //    GravarLog("CodOcorrencia: " + oco[0], "ProcessarTwx");
                        //    GravarLog("DescOcorrencia: " + oco[1], "ProcessarTwx");

                        //    string sqlaux = "SELECT * FROM OCORRENCIA WHERE IDOCORRENCIASERIE=3 AND CODIGO='" + oco[0] + "' ";
                        //    DataTable dtOco = Sistran.Library.GetDataTables.RetornarDataTableWin(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        //    int idOcorrencia = 0;
                        //    string finalzadora = "SIM";

                        //     se nao existir a ocorrencia insere
                        //    if (dtOco.Rows.Count == 0)
                        //    {
                        //        GravarLog("Gravando Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

                        //        idOcorrencia = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("OCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
                        //        sqlaux = "insert into ocorrencia (IDOcorrencia, IDEmpresa, IDOcorrenciaAcao, Codigo, Nome, Responsabilidade, NomeReduzido, PagaEntrega, Finalizador, Sistema,  Ativo, RestringirCarregamento, AbrirFecharOcorrencia, ApareceSiteCliente, IdOcorrenciaSerie)";
                        //        sqlaux += "VALUES (" + idOcorrencia + ", NULL, 5, '" + oco[0] + "', '" + (oco[1].Trim().ToUpper().Length >= 60 ? oco[1].Trim().ToUpper().Substring(0, 59) : oco[1].Trim().ToUpper()) + "', 'CLIENTE', '" + (oco[1].Trim().ToUpper().Length >= 30 ? oco[1].Trim().ToUpper().Substring(0, 29) : oco[1].Trim().ToUpper()) + "', 'NAO', 'SIM', NULL,  'SIM', NULL, 'AMBOS', NULL, 3)";

                        //        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        //        GravarLog("Gravou Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

                        //    }
                        //    else
                        //    {
                        //        GravarLog("Já existe a Ocorrencia apenas seta a Variavel: " + oco[1], "ProcessarTwx");
                        //        idOcorrencia = int.Parse(dtOco.Rows[0]["IDOCORRENCIA"].ToString());
                        //        finalzadora = dtOco.Rows[0]["Finalizador"].ToString();
                        //    }


                        //    DateTime dataOco = DateTime.Now;

                        //    if (xmlOccurrence["Data"].InnerText != "" && ! xmlOccurrence["Data"].InnerText.Contains("0000-00-00"))
                        //    {
                        //        try
                        //        {
                        //            dataOco = DateTime.Parse(xmlOccurrence["Data"].InnerText);
                        //            GravarLog("DataDaOcorrencia: " + dataOco, "ProcessarTwx");
                        //        }
                        //        catch (Exception)
                        //        {
                        //            MessageBox.Show("Test");
                        //        }


                        //    }

                        //    DateTime? DataDeConclusaoDocumento = null;
                        //    int IdDocOco = 0;
                        //    if (dtNota.Rows[0]["DataDeConclusao"].ToString().Length > 0)
                        //    {

                        //        DataDeConclusaoDocumento = DateTime.Parse(dtNota.Rows[0]["DataDeConclusao"].ToString());
                        //        GravarLog("DataDoDocumento já Finalizado: " + dtNota.Rows[0]["DataDeConclusao"].ToString(), "ProcessarTwx");

                        //    }

                        //    try
                        //    {
                        //        IdDocOco = InserirDocumentoOcorrencia(
                        //                                     Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos(),
                        //                                     dtNota.Rows[0]["IDDOCUMENTO"].ToString(),
                        //                                     dtNota.Rows[0]["IDFILIALATUAL"].ToString(),
                        //                                     idOcorrencia.ToString(),
                        //                                     dataOco,
                        //                                     oco[1],
                        //                                     finalzadora,
                        //                                     DataDeConclusaoDocumento);
                        //    }
                        //    catch (Exception)
                        //    {
                        //    }

                        //    try
                        //    {
                        //        if (IdDocOco > 0)
                        //        {
                        //            XmlNode xmlPhoto = xmlOccurrence.SelectSingleNode("Foto");

                        //            if (xmlPhoto != null)
                        //            {
                        //                if (!String.IsNullOrEmpty(xmlPhoto.InnerText))
                        //                {
                        //                    string dado = xmlPhoto["Dado"].InnerText;
                        //                    if (!String.IsNullOrEmpty(dado))
                        //                    {
                        //                        if (IdDocOco > 0)
                        //                        {
                        //                            byte[] info = Convert.FromBase64String(dado);

                        //                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        //                            string strsql = "INSERT INTO DocumentoOcorrenciaArquivo (IDDocumentoOcorrenciaArquivo, IDDocumentoOcorrencia, Arquivo) VALUES (" + id + ", " + IdDocOco + ", @IMAGEM)";
                        //                            SqlCommand command = new SqlCommand();

                        //                            SqlConnection vv = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        //                            command.CommandText = strsql.ToString();
                        //                            command.CommandType = CommandType.Text;
                        //                            command.Connection = vv;
                        //                            command.Parameters.Add(new SqlParameter("@IMAGEM", info));
                        //                            vv.Open();
                        //                            command.ExecuteNonQuery();
                        //                            vv.Close();
                        //                            vv.Dispose();
                        //                            GravarLog("Gravou a foto - IdOcorrenciaFoto: " + id, "ProcessarTwx");
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //    catch (Exception)
                        //    {
                        //    }
                        //}

                        countDocument++;
                    }
                    else
                    {
                        documents = xmlDocument["Erro"].InnerText;
                    }

                }



                if (String.IsNullOrEmpty(documents))
                {
                    documents = xmlAwnser.FirstChild["Erro"].InnerText;
                    GravarLog(documents, "Erro");

                }

                request = null;
                response = null;
                xmlAwnser = null;

                Cursor.Current = Cursors.Default;

                Label2.Text = DateTime.Now + "- Recebeu " + xx + " Baixa do Comprovei";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //txtchave.Text  +=  sss + "\r\n";

                GravarLog(sss, "ProcessarComprovei" + ex.Message);
                reiniciarTimers();
            }
        }

        public void AcertarDadosDTRomaneio(int iddocumento)
        {
            try
            {

                Label2.Text = DateTime.Now + "-" + "AcertarDadosDTRomaneio. IdDocumento: " + iddocumento;
                Application.DoEvents();

                string strsql = " SELECT top 1 DTR.IDDT, DTR.IDROMANEIO, DT.EMISSAO, DT.DATADESAIDA , RS.IDRASTREAMENTO ";
                strsql += "FROM DOCUMENTO D  with(nolock)";
                strsql += "INNER JOIN ROMANEIODOCUMENTO RD  with(nolock) ON RD.IDDOCUMENTO = D.IDDOCUMENTO ";
                strsql += "INNER JOIN DTROMANEIO DTR  with(nolock) ON DTR.IDROMANEIO = RD.IDROMANEIO ";
                strsql += "INNER JOIN DT  with(nolock) ON DT.IDDT = DTR.IDDT ";
                strsql += "LEFT JOIN RASTREAMENTO RS  with(nolock) ON RS.IDDT = DT.IDDT ";
                strsql += "WHERE D.IDDOCUMENTO =  " + iddocumento;
                strsql += " AND DT.IDDTTIPO = 1 ";

                strsql += " Order by 1 desc";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (dt.Rows.Count == 0)
                    return;

                strsql = "";

                //se a data de saida for nulo acerta para aparecer no monitoramento
                if (dt.Rows[0]["DATADESAIDA"].ToString() == "")
                    strsql += "Update dt set dataDeSaida = getdate() where iddt = " + dt.Rows[0]["IDDT"].ToString();

                if (dt.Rows[0]["IDRASTREAMENTO"].ToString() == "")
                {
                    string x = Sistran.Library.GetDataTables.RetornarIdTabela("RASTREAMENTO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    strsql += "; INSERT INTO RASTREAMENTO (IdRastreamento, IdRastreador, IdDt, Latitude,Longitude,Satelites,DataHora, PontodeOcorrencia, LATI , LONGI, DataHoraTransmissao)";
                    strsql += " VALUES (" + x + ", 1, " + dt.Rows[0]["IDDT"].ToString() + ", 0,0,null,getdate(), 'NAO', NULL , NULL, GETDATE())";
                }

                if (strsql.Length > 10)
                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            }
            catch (Exception ex)
            {
                string m = "";
            }
        }

        private void gravarTabelasAuxiliares(XmlNode xmlDocument, int IdDocuemnto)
        {
            string docAtual = "";
            try
            {
                string chave = xmlDocument["Chave"].InnerText;

                int qtdOcorrencia = 0;
                if (xmlDocument.SelectNodes("Ocorrencias/Ocorrencia") != null)
                {
                    qtdOcorrencia = xmlDocument.SelectNodes("Ocorrencias/Ocorrencia").Count;
                }

                for (int i = qtdOcorrencia; i >= 1; i--)
                {
                    XmlNode xmlOccurrence = xmlDocument.SelectNodes("Ocorrencias/Ocorrencia")[i - 1];
                    string[] oco = xmlOccurrence["Motivo"].InnerText.Split('-');


                    string dataOcorrencia = "";

                    if (xmlOccurrence["Data"] == null)
                        dataOcorrencia = DateTime.Now.ToString();
                    else
                        dataOcorrencia = xmlOccurrence["Data"].InnerText;


                    string codigoOcorrenciaComprovei = xmlOccurrence["Numero"].InnerText;
                    byte[] foto = null;

                    Label2.Text = DateTime.Now + "-" + "gravarTabelasAuxiliares. IdDocumento: " + IdDocuemnto;
                    Application.DoEvents();
                    docAtual = IdDocuemnto.ToString();

                    DateTime dtCovert = new DateTime();
                    try
                    {
                        dtCovert = DateTime.Parse(dataOcorrencia);
                    }
                    catch (Exception)
                    {
                        dtCovert = DateTime.Parse(DateTime.Now.ToShortDateString());
                    }


                    oco[0] = oco[0].Trim();
                    oco[1] = oco[1].Trim();

                    XmlNode xmlPhoto = xmlOccurrence.SelectSingleNode("Foto");

                    if (xmlPhoto != null)
                    {
                        if (!String.IsNullOrEmpty(xmlPhoto.InnerText))
                        {
                            string dado = xmlPhoto["Dado"].InnerText;
                            if (!String.IsNullOrEmpty(dado))
                            {

                                foto = Convert.FromBase64String(dado);

                            }
                        }
                    }

                    if (foto != null)
                    {
                        string sql = "INSERT INTO RetornoComprovei (IdDocumento,Chave, DataDaOcorrencia,Ocorrencia,IdOcorrenciaComprovei,Foto,Processado,HorarioProcessamento, HorarioRecebimento)";
                        sql += "values (" + IdDocuemnto + ",'" + chave + "', @DataDaOcorrencia, '" + (xmlOccurrence["Motivo"].InnerText.ToUpper().Length > 50 ? xmlOccurrence["Motivo"].InnerText.ToUpper().Substring(0, 49) : xmlOccurrence["Motivo"].InnerText.ToUpper()) + "'," + codigoOcorrenciaComprovei + ",@Foto,null,null, getdate())";

                        SqlConnection vv = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        SqlCommand command = new SqlCommand();

                        command.CommandText = sql;
                        command.CommandType = CommandType.Text;
                        command.Connection = vv;
                        command.Parameters.Add(new SqlParameter("@Foto", foto));
                        command.Parameters.Add(new SqlParameter("@DataDaOcorrencia", dtCovert));
                        vv.Open();
                        command.ExecuteNonQuery();
                        vv.Close();
                        vv.Dispose();
                    }
                    else
                    {
                        string sql = "INSERT INTO RetornoComprovei (IdDocumento,Chave, DataDaOcorrencia,Ocorrencia,IdOcorrenciaComprovei,Processado,HorarioProcessamento, HorarioRecebimento)";
                        sql += "values (" + IdDocuemnto + ",'" + chave + "','" + dtCovert.ToString("yyyy-MM-dd HH:mm") + "', '" + (xmlOccurrence["Motivo"].InnerText.ToUpper().Length > 50 ? xmlOccurrence["Motivo"].InnerText.ToUpper().Substring(0, 49) : xmlOccurrence["Motivo"].InnerText.ToUpper()) + "'," + codigoOcorrenciaComprovei + ",null,null, getdate())";

                        SqlConnection vv = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        SqlCommand command = new SqlCommand();

                        command.CommandText = sql;
                        command.CommandType = CommandType.Text;
                        command.Connection = vv;
                        //command.Parameters.Add(new SqlParameter("@Foto", foto));
                        //command.Parameters.Add(new SqlParameter("@DataDaOcorrencia", dtCovert));
                        vv.Open();
                        command.ExecuteNonQuery();
                        vv.Close();
                        vv.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "gravarTabelasAuxiliaresXXX ", "Dcocumento: " + docAtual + " erro em: " + DateTime.Now.ToString() + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "gravarTabelasAuxiliares");

            }
        }

        private void CdEanReposicaoRoge()
        {
            Label2.Text = "CdEanReposicaoRoge Inicio: " + DateTime.Now.ToString();

            XmlDocument xdoc = new XmlDocument();
            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "EAN ", "Em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");

            try
            {
                try
                {
                    xdoc.Load("http://wslogos01.roge.com.br:8086/csp/roge/WSRoge.WSLogos.cls?soap_method=RetornarDadosCDEAN");
                }
                catch (Exception)
                {
                    xdoc.Load("http://wslogos02.roge.com.br:8087/csp/roge/WSRoge.WSLogos.cls?soap_method=RetornarDadosCDEAN");

                }

                XmlNodeList xnList = xdoc.GetElementsByTagName("SQL");

                if (xnList.Count == 0)
                {
                    new cEmail().enviarEmail("Roge cdEan", "Nao Encontrou itens para atualizar em: " + DateTime.Now.ToString() + " ITENS PROCESSADOS: " + xnList.Count.ToString(), "moises@sistecno.com.br", "AlertaDoSite");

                    throw new Exception("Nao Encontrou itens para atualizar" + DateTime.Now.ToString());
                }

                string sql = "";
                int erros = 0;
                //string cbErros = "";

                if (xnList.Count > 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql("update ReposicaoRogeEan set Status='D' ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }


                for (int i = 0; i < xnList.Count; i++)
                {
                    try
                    {
                        string cb = xnList[i]["eancodigo"] == null ? "" : xnList[i]["eancodigo"].InnerText;
                        string desc = xnList[i]["nome"] == null ? "" : xnList[i]["nome"].InnerText;

                        sql += "  IF EXISTS (SELECT CodigoDeBarras FROM ReposicaoRogeEan WHERE CodigoDeBarras='" + cb + "') ";
                        sql += " UPDATE ReposicaoRogeEan SET STATUS='U', DataInclusao=GETDATE(), Descricao='" + desc.Trim().ToUpper() + "' WHERE CodigoDeBarras='" + cb.Trim() + "'";
                        sql += "  ELSE ";
                        sql += " INSERT INTO ReposicaoRogeEan (CodigoDeBarras, Status, Descricao) VALUES ('" + cb.Trim() + "', 'I', '" + desc.Trim().ToUpper() + "') ";
                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        sql = "";
                    }
                    catch (Exception ex1)
                    {
                        erros++;
                        MessageBox.Show(ex1.Message);
                    }
                }
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "Finalizou em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");
                Label2.Text = "CdEanReposicaoRoge FIM: " + DateTime.Now.ToString();

            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "erro em: " + DateTime.Now.ToString() + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "CDEAN");
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Text = this.Text;
            notifyIcon1.BalloonTipTitle = this.Text;
            notifyIcon1.BalloonTipText = "Clique duas vezes no ícone para retornar à aplicação!";
            this.ShowInTaskbar = false;
            notifyIcon1.ShowBalloonTip(0);
        }


        #endregion


        #region   R O G E
        private void btnRoge_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            GravarPedidosHomeRefill();
            reiniciarTimers();
        }



        private void Cdean()
        {
            XmlDocument xdoc = new XmlDocument();
            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "INICIOU em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");

            try
            {
                xdoc.Load("http://wslogos01.roge.com.br:8086/csp/roge/WSRoge.WSLogos.cls?soap_method=RetornarDadosCDEAN");
                XmlNodeList xnList = xdoc.GetElementsByTagName("SQL");

                if (xnList.Count == 0)
                {
                    new cEmail().enviarEmail("Roge cdEan", "Nao Encontrou itens para atualizar em: " + DateTime.Now.ToString() + " ITENS PROCESSADOS: " + xnList.Count.ToString(), "moises@sistecno.com.br", "AlertaDoSite");

                    throw new Exception("Nao Encontrou itens para atualizar" + DateTime.Now.ToString());
                }

                string sql = "";
                int erros = 0;
                string cbErros = "";

                if (xnList.Count > 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql("DELETE FROM COLETORCONFERENCIACDEAN ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }


                for (int i = 0; i < xnList.Count; i++)
                {
                    try
                    {
                        string cb = xnList[i]["CodigoBarras"] == null ? "" : xnList[i]["CodigoBarras"].InnerText;
                        string uv = xnList[i]["UnidadeVenda"] == null ? "" : xnList[i]["UnidadeVenda"].InnerText;
                        string duv = xnList[i]["DescricaoUnidadeVenda"] == null ? "" : xnList[i]["DescricaoUnidadeVenda"].InnerText;
                        string qtd = xnList[i]["QuantidadeUnidadeVenda"] == null ? "0" : xnList[i]["QuantidadeUnidadeVenda"].InnerText;

                        sql += "  IF EXISTS (SELECT CodigoBarras FROM COLETORCONFERENCIACDEAN WHERE CODIGOBARRAS='" + cb + "') ";
                        sql += " UPDATE COLETORCONFERENCIACDEAN SET UnidadeVenda='" + (uv == "" ? "UN" : uv) + "', DescricaoUnidadeVenda='" + (duv == "" ? "UNIDADE" : duv) + "', QuantidadeUnidadeVenda=" + qtd + ", UltimaAtualizacao=getDate() WHERE CodigoBarras='" + cb + "'";
                        sql += "  ELSE ";
                        sql += " INSERT INTO COLETORCONFERENCIACDEAN (CODIGOBARRAS, UNIDADEVENDA, DESCRICAOUNIDADEVENDA, QUANTIDADEUNIDADEVENDA, UltimaAtualizacao) VALUES ('" + cb + "', '" + (uv == "" ? "UN" : uv) + "', '" + (duv == "" ? "UNIDADE" : duv) + "', " + qtd.ToString() + ", getdate()) ";
                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        sql = "";
                    }
                    catch (Exception)
                    {
                        erros++;
                        cbErros += xnList[i]["CodigoBarras"].ToString() + "<br>";
                    }
                }
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "Finalizou em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");
            }
            catch (Exception ex)
            {
                new cEmail().enviarEmail("Roge cdEan", ex.Message, "moises@sistecno.com.br", "AlertaDoSite");
            }
        }

        #endregion

        private void btnTWX_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                // AcertarNotasNoStistranetDoComprovei();
                for (int i = 0; i < 10; i++)
                {
                    //SolicitarProtocolo();
                    //ConsultarProtocoloNovaTentativa();
                    AcertarNotasNoStistranetDoComprovei();
                }

                //ConsultarProtocolo();




                Application.Exit();
            }
            catch (Exception ex)
            {
                GravarLog("Erro: " + ex.Message, "btnTWX_Click");
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "btnTWX_Click ", "erro em: " + DateTime.Now.ToString() + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "CDEAN");

            }
        }

        //string sss = "";
        public void SolicitarProtocolo()
        {

            try
            {
                Label2.Text = "Solicitando Protocolo: " + DateTime.Now.ToString();
                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                string varName = "";
                string varType = "";
                string xmlPath = "";
                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));



                // tbUrl = "http://soap.comprovei.com.br/exportQueue/index.php?wsdl";
                tbUrl = "http://34.207.154.27/exportQueue/index.php?wsdl";

                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);


                string postData = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:WebServiceComprovei\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                "<urn:downloadDocumentsHistory soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                "<qtdDocumentos xsi:type=\"xsd:string\">" + ConfigurationSettings.AppSettings["QtdDocumentosPorChamada"] + "</qtdDocumentos>" +
                "</urn:downloadDocumentsHistory>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope>";



                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=ISO-8859-1";
                request.Headers.Add("SOAPAction", "urn:WebServicePOD#downloadDocumentsHistory");
                request.Headers.Add("Authorization", "Basic " + auth);
                request.ContentLength = data.Length;


                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }


                Cursor.Current = Cursors.WaitCursor;
                WebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());

                XmlDocument xmlAwnser = new XmlDocument();
                xmlAwnser.LoadXml(sr.ReadToEnd());

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                // xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

                if (xmlAwnser.GetElementsByTagName("protocolo").Count > 0)
                {
                    //xmlAwnser.GetElementsByTagName("protocolo").Item(0).InnerText

                    string sql = "insert into ProtocoloComprovei (Protocolo,XmlProtocolo,DataSolicitacao) values('" + xmlAwnser.GetElementsByTagName("protocolo").Item(0).InnerText + "',@arquivo,getDate())";
                    SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    SqlCommand cmm = new SqlCommand();

                    cmm.CommandText = sql;
                    cmm.CommandType = CommandType.Text;
                    cmm.Connection = cnn;

                    SqlParameter par = new SqlParameter("@arquivo", xmlAwnser.InnerXml);
                    cmm.Parameters.Add(par);

                    try
                    {
                        cnn.Open();
                        cmm.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Label2.Text = "03. " + ex.Message;
                        Application.DoEvents();

                    }
                    finally
                    {
                        cnn.Close();
                    }

                }


                request = null;
                response = null;
                xmlAwnser = null;

                Cursor.Current = Cursors.Default;

                Label2.Text = DateTime.Now + "- Recebeu  Protocolo";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                Label2.Text = ex.Message;
                Application.DoEvents();
                Thread.Sleep(2000);
                GravarLog(sss, "Solicitar Protocolo" + ex.Message);
                reiniciarTimers();
            }
        }

        public void ConsultarProtocoloNovaTentativa()
        {
            try
            {

                string s = "Select top 1 * from ProtocoloComprovei with (nolock)  where ProcessadoSistran is null and DataConclusao is null and QTDProcessamento  is not null /*and DataSolicitacao<= DATEADD(MI, -3, GETDATE())*/  order by isnull(QTDProcessamento,0), 5 ";
                //string s = "Select top 1 * from ProtocoloComprovei where IdProtocoloComprovei = 38961";
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(s, cnx);

                if (dt.Rows.Count == 0)
                {
                    SolicitarProtocolo();
                    dt = Sistran.Library.GetDataTables.RetornarDataTableWS(s, cnx);

                    if (dt.Rows.Count == 0)
                        return;
                }


                Label2.Text = "Consultando Protocolo Inicio: " + DateTime.Now.ToString();
                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                string varName = "";
                string varType = "";
                string xmlPath = "";
                // string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));



                // tbUrl = "http://soap.comprovei.com.br/exportQueue/index.php?wsdl";
                tbUrl = "http://34.207.154.27/exportQueue/index.php?wsdl";


                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);


                string postData = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:WebServiceComprovei\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                "<urn:getExportProtocolStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                "<protocolo xsi:type=\"xsd:string\">" + dt.Rows[0]["Protocolo"].ToString() + "</protocolo>" +
                "</urn:getExportProtocolStatus>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope>";



                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=ISO-8859-1";
                request.Headers.Add("SOAPAction", "urn:WebServicePOD#getExportProtocolStatus");
                request.Headers.Add("Authorization", "Basic " + auth);
                request.ContentLength = data.Length;


                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }


                Cursor.Current = Cursors.WaitCursor;
                WebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());

                XmlDocument xmlAwnser = new XmlDocument();
                xmlAwnser.LoadXml(sr.ReadToEnd());

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                //xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);
                bool temOcorrencia = false;
                string sql = "";
                string aux = "";
                XmlDocument doc1 = new XmlDocument();

                if (xmlAwnser.GetElementsByTagName("dataConclusao").Count > 0)
                {
                    if (xmlAwnser.GetElementsByTagName("dataConclusao").Item(0).InnerText != "")
                    {
                        sql = "Update ProtocoloComprovei set XmlProtocolo=@Arquivo, ";
                        sql += "DataConclusao=getdate() ";
                        sql += "where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";


                        if (!xmlAwnser.InnerText.Contains("Sem ocorrências para retornar"))
                        {
                            temOcorrencia = true;
                            if (xmlAwnser.GetElementsByTagName("url").Count > 0)
                            {
                                doc1.Load(xmlAwnser.GetElementsByTagName("url").Item(0).InnerText.Replace("https://s3.amazonaws.com/", "http://s3.amazonaws.com/"));
                                sql = "Update ProtocoloComprovei set XmlProtocolo=@Arquivo/*, XmlDocumento = @ArqDoc */";
                                sql += "where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";
                            }
                        }

                        SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        SqlCommand cmm = new SqlCommand();

                        cmm.CommandText = sql;
                        cmm.CommandType = CommandType.Text;
                        cmm.Connection = cnn;

                        SqlParameter par = new SqlParameter("@arquivo", xmlAwnser.InnerXml);
                        cmm.Parameters.Add(par);
                        /*
                                                if (temOcorrencia)
                                                {
                                                    par = new SqlParameter("@ArqDoc", doc1.InnerXml);
                                                    cmm.Parameters.Add(par);
                                                }
                        */

                        try
                        {
                            cnn.Open();
                            cmm.ExecuteNonQuery();
                            cnn.Close();

                            if (temOcorrencia)
                            {
                                string ss = "Select IdOcorrenciaComprovei from RetornoComprovei  with (nolock)  where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";
                                DataTable dtRetComp = Sistran.Library.GetDataTables.RetornarDataTableWS(ss, cnx);
                                ProcessarXMLProtocolo(doc1, dt.Rows[0]["Protocolo"].ToString(), dtRetComp);
                            }
                        }
                        catch (Exception ex)
                        {
                            Label2.Text = "01. " + ex.Message;
                            Application.DoEvents();

                        }
                    }

                }
                request = null;
                response = null;
                xmlAwnser = null;
                Cursor.Current = Cursors.Default;

                Label2.Text = DateTime.Now + "- Termino da Consulta de Protocolo";
                Application.DoEvents();
                // Thread.Sleep(2000);
            }

            catch (Exception ex)
            {
                GravarLog(sss, "Solicitar Protocolo" + ex.Message);
                reiniciarTimers();


            }
        }


        public void ConsultarProtocolo(bool solicitar)
        {
            string ultimoProtocolo = "";
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                if (solicitar)
                {
                    SolicitarProtocolo();
                    SolicitarProtocolo();

                }

                string s = "Select top 1 * from ProtocoloComprovei where ProcessadoSistran is null and DataConclusao is null and Erro is null order by 1  desc";
                //string s = "Select top 1 * from ProtocoloComprovei where IdProtocoloComprovei = 38961";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(s, cnx);

                if (dt.Rows.Count == 0)
                {
                    SolicitarProtocolo();
                    dt = Sistran.Library.GetDataTables.RetornarDataTableWS(s, cnx);

                    if (dt.Rows.Count == 0)
                        return;
                }

                ultimoProtocolo = dt.Rows[0]["Protocolo"].ToString();

                Label2.Text = "Consultando Protocolo Inicio: " + DateTime.Now.ToString();
                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                string varName = "";
                string varType = "";
                string xmlPath = "";
                // string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));



                // tbUrl = "http://soap.comprovei.com.br/exportQueue/index.php?wsdl";
                tbUrl = "http://34.207.154.27/exportQueue/index.php?wsdl";


                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);


                string postData = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:WebServiceComprovei\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                "<urn:getExportProtocolStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                "<protocolo xsi:type=\"xsd:string\">" + dt.Rows[0]["Protocolo"].ToString() + "</protocolo>" +
                "</urn:getExportProtocolStatus>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope>";



                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=ISO-8859-1";
                request.Headers.Add("SOAPAction", "urn:WebServicePOD#getExportProtocolStatus");
                request.Headers.Add("Authorization", "Basic " + auth);
                request.ContentLength = data.Length;


                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }


                Cursor.Current = Cursors.WaitCursor;
                WebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());

                XmlDocument xmlAwnser = new XmlDocument();
                xmlAwnser.LoadXml(sr.ReadToEnd());

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                //xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);
                bool temOcorrencia = false;
                string sql = "";
                string aux = "";
                XmlDocument doc1 = new XmlDocument();

                if (xmlAwnser.GetElementsByTagName("dataConclusao").Count > 0)
                {
                    if (xmlAwnser.GetElementsByTagName("dataConclusao").Item(0).InnerText != "")
                    {
                        sql = "Update ProtocoloComprovei set XmlProtocolo=@Arquivo, ";
                        sql += "DataConclusao=getdate() ";
                        sql += "where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";


                        if (!xmlAwnser.InnerText.Contains("Sem ocorrências para retornar"))
                        {
                            temOcorrencia = true;
                            if (xmlAwnser.GetElementsByTagName("url").ToString() != "" && xmlAwnser.GetElementsByTagName("url") != null && xmlAwnser.GetElementsByTagName("url").Count > 0)
                            {
                                doc1.Load(xmlAwnser.GetElementsByTagName("url").Item(0).InnerText.Replace("https://s3.amazonaws.com/", "http://s3.amazonaws.com/"));
                                sql = "Update ProtocoloComprovei set XmlProtocolo=@Arquivo/*, XmlDocumento = @ArqDoc */";
                                sql += "where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";
                            }
                        }

                        SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        SqlCommand cmm = new SqlCommand();

                        cmm.CommandText = sql;
                        cmm.CommandType = CommandType.Text;
                        cmm.Connection = cnn;

                        SqlParameter par = new SqlParameter("@arquivo", xmlAwnser.InnerXml);
                        cmm.Parameters.Add(par);
                        /*
                                                if (temOcorrencia)
                                                {
                                                    par = new SqlParameter("@ArqDoc", doc1.InnerXml);
                                                    cmm.Parameters.Add(par);
                                                }
                        */

                        try
                        {
                            cnn.Open();
                            cmm.ExecuteNonQuery();
                            cnn.Close();

                            if (temOcorrencia)
                            {
                                string ss = "Select IdOcorrenciaComprovei from RetornoComprovei where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";
                                DataTable dtRetComp = Sistran.Library.GetDataTables.RetornarDataTableWS(ss, cnx);
                                ProcessarXMLProtocolo(doc1, dt.Rows[0]["Protocolo"].ToString(), dtRetComp);
                            }
                        }
                        catch (Exception ex)
                        {
                            Sistran.Library.GetDataTables.RetornarDataTableWS("Update ProtocoloComprovei set erro='S' where Protocolo='" + ultimoProtocolo + "'; select 1", cnx);
                            Label2.Text = "01. " + ex.Message;
                            Application.DoEvents();

                        }
                    }

                }
                request = null;
                response = null;
                xmlAwnser = null;
                Cursor.Current = Cursors.Default;

                Label2.Text = DateTime.Now + "- Termino da Consulta de Protocolo";
                Application.DoEvents();
                // Thread.Sleep(2000);
            }

            catch (Exception ex)
            {
                Sistran.Library.GetDataTables.RetornarDataTableWS("Update ProtocoloComprovei set erro='S' where Protocolo='" + ultimoProtocolo + "'; select 1", cnx);
                GravarLog(sss, "Solicitar Protocolo" + ex.Message);
                reiniciarTimers();


            }
        }

        private void ProcessarXMLProtocolo(XmlDocument XmlDocumento, string Protocolo, DataTable RetornoComproveiProtocolo)
        {
            bool executouTudo = true;
            Label2.Text = "ProcessarXMLProtocolo";
            Application.DoEvents();
            try
            {
                var docs = XmlDocumento.GetElementsByTagName("Documento");
                int qtdx = 0;
                int qtdxTot = docs.Count;

                for (int i = 0; i < docs.Count; i++)
                {
                    string IdOcorrenciaComprovei = "";
                    string Ocorrencia = "";
                    string DataOco = "";
                    string Chave = docs.Item(i).SelectNodes("Chave")[0].InnerText;
                    string IdDocumento = "";
                    byte[] fotoArray = null;
                    qtdx++;

                    //PEGA A NOTA
                    string sql = "SELECT IDDOCUMENTO FROM DOCUMENTO with (nolock) WHERE TIPODEDOCUMENTO IN('NOTA FISCAL', 'ORDEM DE SERVICO', 'LOGISTICA') AND TIPODESERVICO in ('TRANSPORTE','ENTREGA') AND DOCUMENTODOCLIENTE4='" + Chave + "'";
                    DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    if (dtNota.Rows.Count == 0)
                    {
                        sql = "select IDDOCUMENTO from DocumentoEletronico  with (nolock)  where IdNota = '" + Chave + "'";
                        dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }

                    if (dtNota.Rows.Count > 0)
                        IdDocumento = dtNota.Rows[0]["IdDocumento"].ToString();
                    else
                        continue;

                    var ocos = docs.Item(i).SelectNodes("Ocorrencias/Ocorrencia");
                    for (int iOco = 0; iOco < ocos.Count; iOco++)
                    {
                        IdOcorrenciaComprovei = ocos[iOco]["Numero"].InnerText;

                        if (RetornoComproveiProtocolo != null && RetornoComproveiProtocolo.Rows.Count > 0)
                        {
                            DataRow[] x = RetornoComproveiProtocolo.Select("IdOcorrenciaComprovei='" + IdOcorrenciaComprovei + "'");

                            if (x.Length > 0)
                                continue;
                        }

                        Ocorrencia = ocos[iOco]["Motivo"].InnerText;
                        DataOco = ocos[iOco]["Data"].InnerText;

                        if (DataOco == "0000-00-00 00:00:00")
                        {
                            DataOco = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        var fotos = docs.Item(i).SelectNodes("Ocorrencias/Ocorrencia/Fotos/Foto");
                        if (fotos != null && fotos.Count > 0)
                        {
                            string CaminhoImagem = "";
                            CaminhoImagem = fotos[0].SelectSingleNode("Dado").InnerText;


                            try
                            {
                                var request = WebRequest.Create(CaminhoImagem);
                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                {
                                    Bitmap b = (Bitmap)Bitmap.FromStream(stream);
                                    fotoArray = ConvertBitMapToByteArray(b);
                                }
                            }
                            catch (Exception xx)
                            {
                                string carta = "o IdDocumento: " + IdDocumento + " Não localizou a imagem no endereço: " + CaminhoImagem;
                                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "ProcessarXmlProtocolo ", xx.Message + xx.InnerException + " Protocolo: " + Protocolo + "<BR>" + carta, "mail.grupologos.com.br", "logos0902", "ProcessarXMLDOcumento");
                                //executouTudo = false;

                                //                                sql = "Update ProtocoloComprovei set DataSolicitacao=getDate(), QTDProcessamento = isnull(QTDProcessamento,0)+1  where Protocolo='" + Protocolo + "'";
                                sql = "Insert Into RetornoComproveiImagem (Link,IdOcorrenciaComprovei) values ('" + CaminhoImagem + "','" + IdOcorrenciaComprovei + "')";
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                //continue;
                            }
                        }

                        string k = "";
                        if (fotoArray != null)
                            k = ", @foto ";

                        sql = "Insert into RetornoComprovei (IdDocumento, Protocolo, Chave, DataDaOcorrencia, Ocorrencia, IdOcorrenciaComprovei" + k.Replace("@", "") + ")";
                        sql += "values (" + IdDocumento + ", '" + Protocolo + "' ,'" + Chave + "', '" + DateTime.Parse(DataOco).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Ocorrencia + "', '" + IdOcorrenciaComprovei + "'" + k + ")";
                        //sql += " ; insert Into ProtocoloComproveiChave (IdProtocoloComprovei,Chave) Values (SCOPE_IDENTITY (),'" + Chave + "')";

                        SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        SqlCommand cmm = new SqlCommand();

                        cmm.CommandText = sql;
                        cmm.CommandType = CommandType.Text;
                        cmm.Connection = cnn;

                        SqlParameter par;

                        if (k != "")
                        {
                            par = new SqlParameter("@Foto", fotoArray);
                            cmm.Parameters.Add(par);
                        }

                        try
                        {
                            cnn.Open();
                            cmm.ExecuteNonQuery();

                        }
                        catch (Exception ex1)
                        {
                            Label2.Text = "02." + ex1.Message;
                            Application.DoEvents();
                            //thread.Sleep(2000);
                            executouTudo = false;
                        }
                        finally
                        {
                            cnn.Close();
                        }
                    }
                }

                if (executouTudo)
                {
                    string sql = "Update ProtocoloComprovei set DataConclusao=getDate(), ProcessadoSistran=getdate() ";
                    sql += "where Protocolo='" + Protocolo + "'";
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    //SolicitarProtocolo();
                }

            }
            catch (Exception ex)
            {
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "ProcessarXmlProtocolo ", ex.Message + ex.InnerException + " Protocolo: " + Protocolo, "mail.grupologos.com.br", "logos0902", "ProcessarXMLDOcumento");
            }
        }

        public byte[] ConvertBitMapToByteArray(Bitmap bitmap)
        {
            byte[] result = null;
            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, bitmap.RawFormat);
                result = stream.ToArray();
            }
            return result;
        }

        //public int InserirDocumentoOcorrencia(
        //                                    string connDestino,
        //                                    string IDDocumento, string IDFilial,
        //                                    string IDOcorrencia, DateTime DataOcorrencia,
        //                                    string Descricao, string finalizadora,
        //                                    DateTime? DataDeConclusao)
        //{
        //    int IdDocOco = 0;
        //    try
        //    {

        //        // se ja tem uma finalizadora igual
        //        string sql = " SELECT TOP 1 IDDOCUMENTOOCORRENCIA, O.IDOCORRENCIA  FROM DOCUMENTOOCORRENCIA D with (nolock) INNER JOIN OCORRENCIA O with (nolock) ON  O.IDOCORRENCIA = D.IDOCORRENCIA WHERE IDDOCUMENTO=" + IDDocumento + " AND FINALIZADOR = 'SIM' and o.IdOcorrencia=" + IDOcorrencia + " ORDER BY IDDOCUMENTOOCORRENCIA DESC";
        //        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //        if (dt.Rows.Count > 0)
        //        {
        //            return int.Parse(dt.Rows[0][0].ToString());
        //        }


        //        sql = " SELECT TOP 1 IDDOCUMENTOOCORRENCIA, O.IDOCORRENCIA  FROM DOCUMENTOOCORRENCIA D with (nolock) INNER JOIN OCORRENCIA  O with (nolock) ON  O.IDOCORRENCIA = D.IDOCORRENCIA WHERE IDDOCUMENTO=" + IDDocumento + " AND FINALIZADOR = 'SIM'  ORDER BY IDDOCUMENTOOCORRENCIA DESC";
        //        dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //        if (dt.Rows.Count > 0)
        //        {
        //            return 0;
        //        }


        //        AcertarDadosDTRomaneio(int.Parse(IDDocumento));

        //        IdDocOco = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
        //        string strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
        //        strsql += " IDDocumentoOcorrencia, ";
        //        strsql += " IdRomaneio, ";
        //        strsql += " IDDocumento,";
        //        strsql += " IDFilial,";
        //        strsql += " IDOcorrencia,";
        //        strsql += " DataOcorrencia,";
        //        strsql += " Descricao,";
        //        strsql += " Sistema";
        //        strsql += " ) VALUES (";
        //        strsql += IdDocOco + " ,";
        //        strsql += "(SELECT TOP 1 RD.IDROMANEIO FROM ROMANEIODOCUMENTO RD with (nolock) WHERE RD.IDDOCUMENTO = " + IDDocumento + " ORDER BY 1 DESC)" + " ,";
        //        strsql += IDDocumento + " ,";
        //        strsql += Convert.ToInt32(IDFilial) + " ,";

        //        //se a data de conclusao for null coloca a ocorrencia se nao apenas uma observação que se caracteriza pelo null no idocorrencia

        //        if (DataDeConclusao == null)
        //            strsql += int.Parse(IDOcorrencia) + " ,";
        //        else
        //            strsql += " null ,";


        //        //se as datas sao diferentes pega a menor data de ocorrencia

        //        if (DataDeConclusao != null && DataDeConclusao < DataOcorrencia)
        //            DataOcorrencia = (DateTime)DataDeConclusao;

        //        strsql += " convert(datetime,'" + DataOcorrencia + "', 103) ,";
        //        strsql += " '" + Descricao.ToUpper().Trim() + " - TWX',";
        //        strsql += "'SIM'";
        //        strsql += " );   ";


        //        if (DataDeConclusao == null && finalizadora == "SIM")
        //            strsql += "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " " + (finalizadora == "SIM" ? " , DATADECONCLUSAO= convert(datetime,'" + DataOcorrencia + "', 103)" : "") + "  WHERE IDDocumento=" + IDDocumento + " ;";


        //        if (finalizadora == "SIM")
        //        {
        //            strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='PROCESSO FINALIZADO' WHERE IDDOCUMENTO = " + IDDocumento + " ; ";
        //            strsql += "UPDATE DOCUMENTO SET DATADECONCLUSAO= convert(datetime,'" + (DataOcorrencia == null ? DateTime.Now : DataOcorrencia) + "', 103)" + " WHERE IDDocumento=" + IDDocumento ; // + "  AND DATADECONCLUSAO IS NULL;";

        //        }
        //        else
        //        {
        //            if (DataDeConclusao == null)
        //            {
        //                strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO SOLUCAO' WHERE IDDOCUMENTO = " + IDDocumento + " ; ";
        //                strsql += " UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " WHERE IDDocumento=" + IDDocumento + " ;";
        //            }
        //        }

        //        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //        GravarLog("Inseriu a DocumentoOcorrencia: " + IdDocOco, "InserirDocumentoOcorrencia");


        //        //SE INSERIR A DATA DE CONCLUSAO CALCULA O PRAZO UTILIZADO
        //        if (DataDeConclusao == null && finalizadora == "SIM")
        //            Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(" EXEC SP_PRAZO_UTILIZADO_ID " + IDDocumento.ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


        //        return IdDocOco;
        //    }
        //    catch (Exception EX)
        //    {
        //        GravarLog("Erro: " + EX.Message, "InserirDocumentoOcorrencia");
        //        return IdDocOco;
        //    }
        //}


        //public void GravarXml(XmlDocument xml)
        //{
        //    /*
        //    string id = Sistran.Library.GetDataTables.RetornarIdTabela("XmlComprovei", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //    string sql = "Insert Into XmlComprovei Values(" + id + ", @Arquivo) ";
        //    SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //    SqlCommand cmm = new SqlCommand();

        //    cmm.CommandText = sql;
        //    cmm.CommandType = CommandType.Text;
        //    cmm.Connection = cnn;


        //    SqlParameter par = new SqlParameter("@arquivo", xml.InnerXml);
        //    cmm.Parameters.Add(par);

        //    try
        //    {
        //        cnn.Open();
        //        cmm.ExecuteNonQuery();
        //        return int.Parse(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //     * */
        //}

        ////recebe nota a nota
        //public void ProcessarTwx(string chave)
        //{

        //    try
        //    {
        //        //  return;

        //        GravarLog("Iniciou", "ProcessarTwx");
        //        Label2.Text = "ProcessarTwx Inicio: " + DateTime.Now.ToString();

        //        string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
        //        string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

        //        GravarLog("Ambiente: " + ambiente, "ProcessarTwx");
        //        GravarLog("URL: " + tbUrl, "ProcessarTwx");


        //        string varName = "";
        //        string varType = "";
        //        string xmlPath = "";
        //        string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));

        //        varName = "key";
        //        varType = "string";

        //        xmlPath = "Retorno/Documentos/Documento";
        //        xmlPath = "Retorno/Documentos/item";
        //        WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
        //        string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
        //                       "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
        //                       "<SOAP-ENV:Body>" +
        //                       "<tns:getADocumentFromPOD xmlns:tns=\"urn:WebServicePOD\">" +
        //                       "<" + varName + " xsi:type=\"xsd:" + varType + "\">" + chave + "</" + varName + ">" +
        //                       "</tns:" + "getADocumentFromPOD" + ">" +
        //                       "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
        //        byte[] data = Encoding.ASCII.GetBytes(postData);
        //        request.Method = "POST";
        //        request.ContentType = "text/xml; charset=ISO-8859-1";
        //        request.Headers.Add("SOAPAction", "urn:WebServicePOD#getADocumentFromPOD");
        //        request.Headers.Add("Authorization", "Basic " + auth);
        //        request.ContentLength = data.Length;


        //        using (var stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }


        //        Cursor.Current = Cursors.WaitCursor;
        //        WebResponse response = (HttpWebResponse)request.GetResponse();
        //        StreamReader sr = new StreamReader(response.GetResponseStream());

        //        XmlDocument xmlAwnser = new XmlDocument();
        //        xmlAwnser.LoadXml(sr.ReadToEnd());

        //        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
        //        nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        //        xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

        //        string documents = "";
        //        int countDocument = 1;

        //        // GravarXML(xmlAwnser.InnerXml, DateTime.Now);

        //        //int idXML = GravarXml(xmlAwnser);

        //        foreach (XmlNode xmlDocument in xmlAwnser.SelectNodes(xmlPath))
        //        {
        //            GravarLog("", "");
        //            GravarLog("---------------------------------------------------------------------------------------------------", "");
        //            GravarLog("##: " + countDocument, "");


        //            if (xmlDocument["Erro"] == null)
        //            {
        //                string sKey = (xmlDocument["Chave"] != null) ? xmlDocument["Chave"].InnerText : "";
        //                GravarLog("Chave da Nota: " + sKey, "ProcessarTwx");

        //                //PEGA A NOTA
        //                string sql = "SELECT * FROM DOCUMENTO WHERE DOCUMENTODOCLIENTE4='" + sKey + "'";
        //                DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                //se nao achar a nota vai para a proxima
        //                if (dtNota.Rows.Count == 0)
        //                {
        //                    GravarLog("Não Encontro o documento:" + sKey, "ProcessarTwx");
        //                    continue;

        //                }

        //                GravarLog("IDDOCUMENTO: " + dtNota.Rows[0]["IDDOCUMENTO"].ToString(), "ProcessarTwx");
        //                GravarLog("NUMERO: " + dtNota.Rows[0]["Numero"].ToString(), "ProcessarTwx");

        //                int qtdOcorrencia = 0;
        //                if (xmlDocument.SelectNodes("Ocorrencias/Ocorrencia") != null)
        //                {
        //                    qtdOcorrencia = xmlDocument.SelectNodes("Ocorrencias/Ocorrencia").Count;
        //                }

        //                for (int i = qtdOcorrencia; i >= 1; i--)
        //                {
        //                    XmlNode xmlOccurrence = xmlDocument.SelectNodes("Ocorrencias/Ocorrencia")[i - 1];

        //                    //}


        //                    //foreach (XmlNode xmlOccurrence in xmlDocument.SelectNodes("Ocorrencias/Ocorrencia"))
        //                    //{
        //                    string occurenceNumber = xmlOccurrence["Numero"].InnerText;
        //                    string[] oco = xmlOccurrence["Motivo"].InnerText.Split('-');


        //                    oco[0] = oco[0].Trim();
        //                    oco[1] = oco[1].Trim();

        //                    GravarLog("NumeroOcorrencia: " + occurenceNumber, "ProcessarTwx");
        //                    GravarLog("CodOcorrencia: " + oco[0], "ProcessarTwx");
        //                    GravarLog("DescOcorrencia: " + oco[1], "ProcessarTwx");

        //                    string sqlaux = "SELECT * FROM OCORRENCIA WHERE IDOCORRENCIASERIE=3 AND CODIGO='" + oco[0] + "' ";
        //                    DataTable dtOco = Sistran.Library.GetDataTables.RetornarDataTableWin(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                    int idOcorrencia = 0;
        //                    string finalzadora = "SIM";

        //                    // se nao existir a ocorrencia insere
        //                    if (dtOco.Rows.Count == 0)
        //                    {
        //                        GravarLog("Gravando Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

        //                        idOcorrencia = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("OCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
        //                        sqlaux = "insert into ocorrencia (IDOcorrencia, IDEmpresa, IDOcorrenciaAcao, Codigo, Nome, Responsabilidade, NomeReduzido, PagaEntrega, Finalizador, Sistema,  Ativo, RestringirCarregamento, AbrirFecharOcorrencia, ApareceSiteCliente, IdOcorrenciaSerie)";
        //                        sqlaux += "VALUES (" + idOcorrencia + ", NULL, 5, '" + oco[0] + "', '" + (oco[1].Trim().ToUpper().Length >= 60 ? oco[1].Trim().ToUpper().Substring(0, 59) : oco[1].Trim().ToUpper()) + "', 'CLIENTE', '" + (oco[1].Trim().ToUpper().Length >= 30 ? oco[1].Trim().ToUpper().Substring(0, 29) : oco[1].Trim().ToUpper()) + "', 'NAO', 'SIM', NULL,  'SIM', NULL, 'AMBOS', NULL, 3)";

        //                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                        GravarLog("Gravou Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

        //                    }
        //                    else
        //                    {
        //                        GravarLog("Já existe a Ocorrencia apenas seta a Variavel: " + oco[1], "ProcessarTwx");
        //                        idOcorrencia = int.Parse(dtOco.Rows[0]["IDOCORRENCIA"].ToString());
        //                        finalzadora = dtOco.Rows[0]["Finalizador"].ToString();
        //                    }


        //                    DateTime dataOco = DateTime.Now;

        //                    if (xmlOccurrence["Data"].InnerText != "")
        //                    {
        //                        dataOco = DateTime.Parse(xmlOccurrence["Data"].InnerText);
        //                        GravarLog("DataDaOcorrencia: " + dataOco, "ProcessarTwx");

        //                    }

        //                    DateTime? DataDeConclusaoDocumento = null;
        //                    int IdDocOco = 0;
        //                    if (dtNota.Rows[0]["DataDeConclusao"].ToString().Length > 0)
        //                    {

        //                        DataDeConclusaoDocumento = DateTime.Parse(dtNota.Rows[0]["DataDeConclusao"].ToString());
        //                        GravarLog("DataDoDocumento já Finalizado: " + dtNota.Rows[0]["DataDeConclusao"].ToString(), "ProcessarTwx");

        //                    }

        //                    try
        //                    {
        //                        IdDocOco = InserirDocumentoOcorrencia(
        //                                                     Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos(),
        //                                                     dtNota.Rows[0]["IDDOCUMENTO"].ToString(),
        //                                                     dtNota.Rows[0]["IDFILIALATUAL"].ToString(),
        //                                                     idOcorrencia.ToString(),
        //                                                     dataOco,
        //                                                     oco[1],
        //                                                     finalzadora,
        //                                                     DataDeConclusaoDocumento);
        //                    }
        //                    catch (Exception)
        //                    {
        //                    }

        //                    try
        //                    {
        //                        if (IdDocOco > 0)
        //                        {
        //                            XmlNode xmlPhoto = xmlOccurrence.SelectSingleNode("Foto");

        //                            if (xmlPhoto != null)
        //                            {
        //                                if (!String.IsNullOrEmpty(xmlPhoto.InnerText))
        //                                {
        //                                    string dado = xmlPhoto["Dado"].InnerText;
        //                                    if (!String.IsNullOrEmpty(dado))
        //                                    {
        //                                        if (IdDocOco > 0)
        //                                        {
        //                                            byte[] info = Convert.FromBase64String(dado);

        //                                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                                            string strsql = "INSERT INTO DocumentoOcorrenciaArquivo (IDDocumentoOcorrenciaArquivo, IDDocumentoOcorrencia, Arquivo) VALUES (" + id + ", " + IdDocOco + ", @IMAGEM)";
        //                                            SqlCommand command = new SqlCommand();

        //                                            SqlConnection vv = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                                            command.CommandText = strsql.ToString();
        //                                            command.CommandType = CommandType.Text;
        //                                            command.Connection = vv;
        //                                            command.Parameters.Add(new SqlParameter("@IMAGEM", info));
        //                                            vv.Open();
        //                                            command.ExecuteNonQuery();
        //                                            vv.Close();
        //                                            vv.Dispose();
        //                                            GravarLog("Gravou a foto - IdOcorrenciaFoto: " + id, "ProcessarTwx");
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    catch (Exception)
        //                    {
        //                    }
        //                }

        //                countDocument++;
        //            }
        //            else
        //            {
        //                documents = xmlDocument["Erro"].InnerText;
        //            }
        //        }



        //        if (String.IsNullOrEmpty(documents))
        //        {
        //            documents = xmlAwnser.FirstChild["Erro"].InnerText;
        //            GravarLog(documents, "Erro");

        //        }

        //        request = null;
        //        response = null;
        //        xmlAwnser = null;

        //        Cursor.Current = Cursors.Default;
        //        Label2.Text = "ProcessarTwx FIM: " + DateTime.Now.ToString();

        //    }
        //    catch (Exception)
        //    {
        //        reiniciarTimers();
        //    }
        //}


        public void GravarLog(string MenssagemLog, string NomeFuncao)
        {
            //try
            //{
            //    StreamWriter valor = new StreamWriter(@".\log\\Log_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".txt", true, Encoding.Unicode);
            //    valor.Write(DateTime.Now.ToString() + " | " + NomeFuncao + " | " + MenssagemLog + "\r\n");
            //    valor.Close();
            //}
            //catch (Exception)
            //{
            //}
        }

        public void GravarXML(string conteudo, DateTime d)
        {
            try
            {
                //string no = d.ToString().Replace(".", "_").Replace(":", "_").Replace("/", "_") + ".xml";
                //StreamWriter valor = new StreamWriter(@".\xml\\" + no, true, Encoding.Unicode);
                //valor.Write(conteudo);
                //valor.Close();

                //GravarLog("NomeXml: " + no, "ProcessarTwx");

            }
            catch (Exception)
            {
            }
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private void EnviarComprovei(bool dt, bool BaseTeste)
        {

            Label2.Text = "EnviarComprovei: " + DateTime.Now.ToString();
            //Sistran.Library.GetDataTables.ExecutarComandoSql("UPDATE DOCUMENTO SET NOMEDOARQUIVO1=NULL, DOCUMENTODOCLIENTE4=NULL WHERE  LTRIM(RTRIM(DOCUMENTODOCLIENTE4))='' AND  convert(datetime, DATADEEMISSAO, 103)  >=  convert(datetime,'01/12/2015',103) AND TIPODEDOCUMENTO='NOTA FISCAL'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
            string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

            string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin"));

            if (BaseTeste)
                auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));

            DataTable dtGeral = null;

            if (BaseTeste)
            {
                dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI_TESTE " + txtIdDt.Text, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            }
            else
            {
                if (dt)
                    dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI_DT ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                else
                    dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            }


            DataView view = new DataView(dtGeral);
            DataTable dtds = view.ToTable(true, "IDDOCUMENTO");
            const string quote = "\"";
            string xml = "";
            string ch = "";

            // Label2.Text = "Enviando Comprovei";

            for (int i = 0; i < dtds.Rows.Count; i++)
            {
                xml = "";

                Label2.Text = DateTime.Now + "- Enviado " + (i + 1).ToString() + " de " + dtds.Rows.Count + " registros para o comprovei";
                Application.DoEvents();

                if (i == 1000)
                    return;

                try
                {
                    xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
                    xml += "<Documentos>";
                    DataRow[] dr = dtGeral.Select("IDDOCUMENTO=" + dtds.Rows[i]["IDDOCUMENTO"].ToString(), "");

                    ch = dr[0]["CHAVE"].ToString();

                    // Label2.Text = "Enviando Comprovei: " + ch;

                    bool gerouCahve = false;

                    if (ch.Contains("SERNUMERONOT"))
                    {
                        ch = ch.Replace("SER", dr[0]["SERIE"].ToString().PadLeft(3, '0'));
                        ch = ch.Replace("NUMERONOT", dr[0]["NUMERO"].ToString().PadLeft(9, '0'));

                        gerouCahve = true;
                    }



                    xml += "<Documento>";

                    if (dr[0]["TIPODEDOCUMENTO"].ToString().ToUpper() == "ORDEM DE SERVICO")
                    {
                        xml += "<Tipo>REQ</Tipo>";
                        xml += "<TipoParada>C</TipoParada>";
                        xml += "<Modelo>55</Modelo>";
                    }
                    else
                    {
                        xml += "<Tipo>NFE</Tipo>";
                        xml += "<TipoParada>E</TipoParada>";
                        xml += "<Modelo>55</Modelo>";
                    }

                    xml += "<Numero>" + dr[0]["NUMERO"].ToString() + "</Numero>";
                    xml += "<Valor>" + dr[0]["VALORDANOTA"].ToString().Replace(",", ".") + "</Valor>";

                    if (dr[0]["IDCLIENTE"].ToString() == "65286")
                    {
                        //se o cliente for fastshop procura a serie na chave da nota

                        if (ch.Length == 44)
                        {
                            xml += "<Serie>" + ch.Substring(22, 3) + "</Serie>";
                        }
                    }
                    else
                        xml += "<Serie>" + (dr[0]["SERIE"].ToString().Trim() == "NFE" ? "001" : dr[0]["SERIE"].ToString().Trim()) + "</Serie>";



                    xml += "<Emissao>" + DateTime.Parse(dr[0]["DATADEEMISSAO"].ToString()).ToString("yyyyMMdd") + "</Emissao>";
                    xml += "<Atualizacao>" + DateTime.Parse(dr[0]["ATUALIZACAO"].ToString()).ToString("yyyyMMdd") + "</Atualizacao>";
                    xml += "<Chave>" + ch + "</Chave>";
                    xml += "<cnpj>" + dr[0]["CNPJ"].ToString() + "</cnpj>";

                    xml += "<cnpjEmissor>" + dr[0]["CNPJEMISSOR"].ToString() + "</cnpjEmissor>";
                    xml += "<cnpjTransportador>" + dr[0]["CNPJTRANSPORTADOR"].ToString() + "</cnpjTransportador>";

                    xml += "</Documento>";
                    xml += "<Cliente>";
                    xml += "<Codigo>" + dr[0]["CODIGOCLIENTE"].ToString() + "</Codigo>";
                    xml += "<Contato>" + dr[0]["CONTATO"].ToString() + "</Contato>";

                    //if (dr[0]["TELEFONE"].ToString() == "")
                    //    xml += "<Telefone>11 9999-9999</Telefone>";
                    //else
                    xml += "<Telefone>" + dr[0]["TELEFONE"].ToString() + "</Telefone>";


                    //if (dr[0]["EMAIL"].ToString() == "")
                    //    xml += "<Email>moises@sistecno.com.br</Email>";
                    //else
                    xml += "<Email>" + dr[0]["EMAIL"].ToString() + "</Email>";


                    xml += "<Razao>" + dr[0]["RAZAO"].ToString().Replace("&", "") + "</Razao>";
                    xml += "<Endereco>" + dr[0]["ENDERECO"].ToString() + ", " + dr[0]["NUMERO_END"].ToString() + "</Endereco>";
                    xml += "<Bairro>" + dr[0]["BAIRRO"].ToString() + "</Bairro>";
                    xml += "<Cidade>" + dr[0]["CIDADE"].ToString() + "</Cidade>";
                    xml += "<Estado>" + dr[0]["ESTADO"].ToString() + "</Estado>";
                    xml += "<Pais>BRASIL</Pais>";
                    xml += "<CEP>" + dr[0]["CEP"].ToString() + "</CEP>";


                    xml += "<Regiao>" + dr[0]["REGIAO"].ToString() + "</Regiao>";
                    xml += "<TipoCliente></TipoCliente>";
                    xml += "<Mensagem></Mensagem>";
                    xml += "</Cliente>";

                    xml += "<SKUs>";
                    bool jaInseriu = false;

                    for (int ii = 0; ii < dr.Length; ii++)
                    {
                        if (dr[ii]["CODIGOPR"].ToString() != "")
                        {
                            xml += "<SKU codigo=" + quote + dr[ii]["CODIGOPR"].ToString() + quote + ">";
                            xml += "<PesoBruto>" + dr[ii]["PESOBRUTO"].ToString() + "</PesoBruto>";
                            xml += "<PesoLiquido>" + dr[ii]["PESOLIQUIDO"].ToString() + "</PesoLiquido>";
                            xml += "<Volumes>" + dr[ii]["VOLUMES"].ToString() + "</Volumes>";
                            xml += "<Descricao>" + dr[ii]["DESCRICAO"].ToString().Replace("'", "").Replace("&", "e") + "</Descricao>";
                            xml += "<Qde>" + dr[ii]["QUANTIDADE"].ToString().Replace(",", ".") + "</Qde>";
                            xml += "<Uom>" + dr[ii]["UNIDADEDEMEDIDA"].ToString() + "</Uom>";
                            xml += "<Barcode>" + dr[ii]["BARCODE"].ToString() + "</Barcode>";
                            xml += "</SKU>";
                        }
                        else
                        {
                            if (jaInseriu == false)
                            {
                                xml += "<SKU codigo=" + quote + dr[0]["NUMERO"].ToString() + quote + ">";
                                xml += "<PesoBruto>0</PesoBruto>";
                                xml += "<PesoLiquido>0</PesoLiquido>";
                                xml += "<Volumes>1</Volumes>";
                                xml += "<Descricao>DANFE " + dr[0]["NUMERO"].ToString() + "</Descricao>";
                                xml += "<Qde>1</Qde>";
                                xml += "<Uom>FL</Uom>";
                                xml += "<Barcode>" + ch + "</Barcode>";
                                xml += "</SKU>";
                                jaInseriu = true;
                            }
                        }
                    }
                    xml += "</SKUs> ";

                    xml += " </Documentos> ";

                    string auxXML = xml;
                    xml = Base64Encode(xml);

                    if (ch.Contains("NFE"))
                    {
                        ch = ch.Replace("NFE", "000");
                        gerouCahve = true;
                    }

                    //string NomeArquivo = ch + DateTime.Now.ToString("yyyyMMddhhmmssffftt") + ".xml";
                    string NomeArquivo = dtds.Rows[i]["IDDOCUMENTO"].ToString() + ".xml";

                    WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
                    string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
                                   "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<SOAP-ENV:Body>" +
                                   "<tns:importDocsToPOD xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
                                   "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
                                   "</tns:" + "importDocsToPOD" + ">" +
                                   "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    byte[] data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "text/xml; charset=ISO-8859-1";
                    request.Headers.Add("SOAPAction", "urn:WebServicePOD#getDocumentsFromPOD");
                    request.Headers.Add("Authorization", "Basic " + auth);
                    request.ContentLength = data.Length;


                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }


                    Cursor.Current = Cursors.WaitCursor;
                    WebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream());

                    XmlDocument xmlAwnser = new XmlDocument();
                    xmlAwnser.LoadXml(sr.ReadToEnd());


                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                    nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

                    Label2.Text = DateTime.Now + "-" + "Enviando Nota ao Comprovei. IdDocumento: " + sss;
                    Application.DoEvents();

                    string retorno = xmlAwnser["status"].InnerText;

                    retorno = retorno.Replace("'", "");
                    retorno = retorno.Replace("/", "");
                    retorno = retorno.Replace("\\", "");
                    retorno = retorno.Replace("??", "CA");

                    string sql = "";
                    if (BaseTeste == false)
                    {
                        if (retorno.ToUpper().Contains("WEVE GOT SOME PROBLEM"))
                        {
                            sql = "UPDATE DOCUMENTO SET EnviadoComprovei=null  WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
                            sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), '" + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
                            //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";

                            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei ", "Problema no retorno em: " + DateTime.Now.ToString() + "- IdDocumento:" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + "<BR> " + retorno, "mail.grupologos.com.br", "logos0902", "Envio Comprovei - Problema no retorno do comprovei");

                        }
                        else
                        {

                            sql = "UPDATE DOCUMENTO SET EnviadoComprovei='ENVIADO COMPROVEI - " + DateTime.Now + "' " + (gerouCahve == true ? ", DocumentoDoCliente4='" + ch + "'" : "") + " WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
                            sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), 'Filial: " + dr[0]["REGIAO"].ToString() + "  " + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
                            //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";
                        }

                        //if (!retorno.Contains("existe."))
                        //{
                            Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                            GravarXML(auxXML, DateTime.Now);
                            GravarLog(ch, retorno);
                        //}
                        // Label2.Text = "Terminou Envio Comprovei";
                    }
                    Label2.Text = "EnviarComprovei Termino: " + DateTime.Now.ToString();
                }
                catch (Exception ex)
                {
                    reiniciarTimers();
                    Label2.Text = "EnviarComprovei Erro: " + DateTime.Now.ToString();
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei ", "Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
                }
            }
        }

        private void EnviarRota_Comprovei(string idDt, string IdFilial)
        {
            try
            {


                Label2.Text = "Enviar Rotas: " + DateTime.Now.ToString();

                string x = "SELECT TOP 100 DT.IDDT FROM DT  ";
                x += " INNER JOIN DTROMANEIO DTR ON DTR.IDDT = DT.IDDT ";
                x += " INNER JOIN ROMANEIO ROM ON ROM.IDROMANEIO = DTR.IDROMANEIO ";
                //                x += " WHERE DT.ANDAMENTO IN ('DOCUMENTACAO LIBERADA' ,  'EM ENTREGA', 'CARREGAMENTO EFETUADO') ";
                x += " where ROM.IDFILIAL  NOT IN(48, 15,30,54,49,27, 52) ";
                //x+ = " AND ROTAENVIADACOMPROVEI IS NULL AND ROM.EMISSAO>=GETDATE()-15 ";


                x += " and  DT.IdFilial in(select idfilial from FILIALENVIAROTASCOMPROVEI)";

                if (idDt == "")
                {
                    x += " AND (dt.EMISSAO >= GETDATE()-1 or rom.Liberacao >= GETDATE()-1) AND Ativo = 'SIM' AND ROTAENVIADACOMPROVEI IS NULL";
                    x += " AND ROM.Tipo = 'ENTREGA'";
                    x += " and  DT.ANDAMENTO IN ('EM ENTREGA') ";
                    x += " AND DT.ROTAENVIADACOMPROVEI IS NULL ";



                }
                else
                    x += " and dt.Iddt = " + idDt;



                DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                Application.DoEvents();
                const string quote = "\"";


                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

                for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
                {
                    try
                    {

                        DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];


                        DataView view = new DataView(dtGeral);
                        DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
                        string NomeArquivo = "";
                        int NumeroParada = 1;

                        string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

                        string xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
                        xml += "<Rotas>";
                        // xml += "<Rota numero=" + quote + NomeRota + quote + ">";
                        //  xml += "<Rota numero=" + quote + dtGeral.Rows[0]["NUMEROROTA"].ToString() + quote + " Regiao=" + quote + NomeRota + quote + ">";
                        xml += "<Rota numero=" + quote + dtGeral.Rows[0]["NUMERO"].ToString() + quote + ">";


                        for (int i = 0; i < dtds.Rows.Count; i++)
                        {

                            DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString() + "'" +
                                                            " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString() + "'" +
                                                            " and IDENDERECOBAIRRO=" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString() +
                                                            " and IDENDERECOCIDADE=" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString() +
                                                            " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString() + "'", "");


                            if (i == 0)
                            {

                                //xml += "     <Numero>" + ret[i]["NUMEROROTA"].ToString() + "_Teste</Numero>";
                                xml += "<Data>" + DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd") + "</Data>";
                                xml += "<Regiao>" + NomeRota + "</Regiao>";

                                //xml += "<Transportadora>";
                                //xml += "<Codigo>0</Codigo>";
                                //xml += "<Razao>SEM TRANSPORTADORA</Razao>";
                                //xml += "</Transportadora>";

                                xml += "<Transportadora>";
                                xml += "<Codigo></Codigo>";
                                xml += "<Razao></Razao>";
                                xml += "</Transportadora>";

                                //xml += "     <Regiao>" + ret[i]["REGIAO"].ToString() + "</Regiao>";
                                xml += "<Motorista>";
                                xml += "<Usuario>" + ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "") + "</Usuario>";
                                xml += "<PlacaVeiculo>" + ret[i]["PLACA"].ToString().Replace("-", "") + "</PlacaVeiculo>";
                                xml += "</Motorista>";

                                xml += "<Paradas>";

                                NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";

                            }


                            for (int ii = 0; ii < ret.Length; ii++)
                            {
                                xml += "<Parada numero=" + quote + NumeroParada + quote + ">";
                                xml += "<Documento>";
                                xml += "<ChaveNota>" + ret[ii]["CHAVE"].ToString() + "</ChaveNota>";
                                xml += "</Documento>";
                                xml += "</Parada>";
                                NumeroParada++;
                            }
                        }
                        xml += "</Paradas>";
                        xml += "</Rota>";
                        xml += "</Rotas>";

                        string auxXML = xml;
                        xml = Base64Encode(xml);

                        //  continue; // retinar na hora de enviar

                        WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
                        string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
                                       "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
                                       "<SOAP-ENV:Body>" +
                                       "<tns:sendDocsKeysToPOD xmlns:tns=\"urn:WebServicePOD\">" +
                                       "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
                                       "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
                                       "</tns:" + "sendDocsKeysToPOD" + ">" +
                                       "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                        byte[] data = Encoding.ASCII.GetBytes(postData);
                        request.Method = "POST";
                        request.ContentType = "text/xml; charset=ISO-8859-1";
                        request.Headers.Add("SOAPAction", "urn:WebServicePOD#sendDocsKeysToPOD");
                        request.Headers.Add("Authorization", "Basic " + auth);
                        request.ContentLength = data.Length;


                        using (var stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }


                        Cursor.Current = Cursors.WaitCursor;
                        WebResponse response = (HttpWebResponse)request.GetResponse();
                        StreamReader sr = new StreamReader(response.GetResponseStream());

                        XmlDocument xmlAwnser = new XmlDocument();
                        xmlAwnser.LoadXml(sr.ReadToEnd());


                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                        nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                        xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);
                        string retorno = xmlAwnser["status"].InnerText;

                        string sql = "Update Dt Set ROTAENVIADACOMPROVEI='ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno + "' where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();

                        sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                    catch (Exception ex)
                    {
                        Label2.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
                        Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rOTAS", "Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
                        continue;
                    }
                }
            }
            catch (Exception)
            {
                reiniciarTimers();
            }
        }

        private void EnviarComprovei_HOMEREFILL()
        {

            Label2.Text = "EnviarComprovei: " + DateTime.Now.ToString();
            //Sistran.Library.GetDataTables.ExecutarComandoSql("UPDATE DOCUMENTO SET NOMEDOARQUIVO1=NULL, DOCUMENTODOCLIENTE4=NULL WHERE  LTRIM(RTRIM(DOCUMENTODOCLIENTE4))='' AND  convert(datetime, DATADEEMISSAO, 103)  >=  convert(datetime,'01/12/2015',103) AND TIPODEDOCUMENTO='NOTA FISCAL'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
            string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);
            string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("homerefill" + ":" + "admin"));
            DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI_HOMEREFILL ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

            DataView view = new DataView(dtGeral);
            DataTable dtds = view.ToTable(true, "IDDOCUMENTO");
            const string quote = "\"";
            string xml = "";
            string ch = "";

            // Label2.Text = "Enviando Comprovei";

            for (int i = 0; i < dtds.Rows.Count; i++)
            {
                xml = "";

                Label2.Text = DateTime.Now + "- Enviado " + i + 1 + " de " + dtds.Rows.Count + " registros para o comprovei";
                Application.DoEvents();

                if (i == 1000)
                    return;

                try
                {
                    xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
                    xml += "<Documentos>";
                    DataRow[] dr = dtGeral.Select("IDDOCUMENTO=" + dtds.Rows[i]["IDDOCUMENTO"].ToString(), "");

                    ch = dr[0]["CHAVE"].ToString();

                    // Label2.Text = "Enviando Comprovei: " + ch;

                    bool gerouCahve = false;

                    if (ch.Contains("SERNUMERONOT"))
                    {
                        ch = ch.Replace("SER", dr[0]["SERIE"].ToString().PadLeft(3, '0'));
                        ch = ch.Replace("NUMERONOT", dr[0]["NUMERO"].ToString().PadLeft(9, '0'));

                        gerouCahve = true;
                    }



                    xml += "<Documento>";

                    if (dr[0]["TIPODEDOCUMENTO"].ToString().ToUpper() == "ORDEM DE SERVICO")
                    {
                        xml += "<Tipo>REQ</Tipo>";
                        xml += "<TipoParada>C</TipoParada>";
                        xml += "<Modelo>55</Modelo>";
                    }
                    else
                    {
                        xml += "<Tipo>NFE</Tipo>";
                        xml += "<TipoParada>E</TipoParada>";
                        xml += "<Modelo>55</Modelo>";
                    }

                    xml += "<Numero>" + dr[0]["NUMERO"].ToString() + "</Numero>";
                    xml += "<Valor>" + dr[0]["VALORDANOTA"].ToString().Replace(",", ".") + "</Valor>";

                    if (dr[0]["IDCLIENTE"].ToString() == "65286")
                    {
                        //se o cliente for fastshop procura a serie na chave da nota

                        if (ch.Length == 44)
                        {
                            xml += "<Serie>" + ch.Substring(22, 3) + "</Serie>";
                        }
                    }
                    else
                        xml += "<Serie>" + (dr[0]["SERIE"].ToString().Trim() == "NFE" ? "001" : dr[0]["SERIE"].ToString().Trim()) + "</Serie>";



                    xml += "<Emissao>" + DateTime.Parse(dr[0]["DATADEEMISSAO"].ToString()).ToString("yyyyMMdd") + "</Emissao>";
                    xml += "<Atualizacao>" + DateTime.Parse(dr[0]["ATUALIZACAO"].ToString()).ToString("yyyyMMdd") + "</Atualizacao>";
                    xml += "<Chave>" + ch + "</Chave>";
                    xml += "<cnpj>" + dr[0]["CNPJ"].ToString() + "</cnpj>";

                    xml += "<cnpjEmissor>" + dr[0]["CNPJEMISSOR"].ToString().Trim().Replace(" ", "") + "</cnpjEmissor>";
                    xml += "<cnpjTransportador>" + dr[0]["CNPJTRANSPORTADOR"].ToString().Trim().Replace(" ", "") + "</cnpjTransportador>";

                    xml += "</Documento>";
                    xml += "<Cliente>";
                    xml += "<Codigo>" + dr[0]["CODIGOCLIENTE"].ToString() + "</Codigo>";
                    xml += "<Contato>" + dr[0]["CONTATO"].ToString() + "</Contato>";

                    //if (dr[0]["TELEFONE"].ToString() == "")
                    //    xml += "<Telefone>11 9999-9999</Telefone>";
                    //else
                    xml += "<Telefone>" + dr[0]["TELEFONE"].ToString().Trim().Replace(" ", "") + "</Telefone>";


                    //if (dr[0]["EMAIL"].ToString() == "")
                    //    xml += "<Email>moises@sistecno.com.br</Email>";
                    //else
                    xml += "<Email>" + dr[0]["EMAIL"].ToString() + "</Email>";


                    xml += "<Razao>" + dr[0]["RAZAO"].ToString().Replace("&", "") + "</Razao>";
                    xml += "<Endereco>" + dr[0]["ENDERECO"].ToString() + ", " + dr[0]["NUMERO_END"].ToString() + "</Endereco>";
                    xml += "<Bairro>" + dr[0]["BAIRRO"].ToString() + "</Bairro>";
                    xml += "<Cidade>" + dr[0]["CIDADE"].ToString() + "</Cidade>";
                    xml += "<Estado>" + dr[0]["ESTADO"].ToString() + "</Estado>";
                    xml += "<Pais>BRASIL</Pais>";
                    xml += "<CEP>" + dr[0]["CEP"].ToString() + "</CEP>";


                    xml += "<Regiao>" + dr[0]["REGIAO"].ToString() + "</Regiao>";
                    xml += "<TipoCliente></TipoCliente>";
                    xml += "<Mensagem></Mensagem>";
                    xml += "</Cliente>";

                    xml += "<SKUs>";
                    bool jaInseriu = false;

                    for (int ii = 0; ii < dr.Length; ii++)
                    {
                        if (dr[ii]["CODIGOPR"].ToString() != "")
                        {
                            xml += "<SKU codigo=" + quote + dr[ii]["CODIGOPR"].ToString() + quote + ">";
                            xml += "<PesoBruto>" + dr[ii]["PESOBRUTO"].ToString() + "</PesoBruto>";
                            xml += "<PesoLiquido>" + dr[ii]["PESOLIQUIDO"].ToString() + "</PesoLiquido>";
                            xml += "<Volumes>" + dr[ii]["VOLUMES"].ToString() + "</Volumes>";
                            xml += "<Descricao>" + dr[ii]["DESCRICAO"].ToString().Replace("'", "").Replace("&", "e") + "</Descricao>";
                            xml += "<Qde>" + dr[ii]["QUANTIDADE"].ToString().Replace(",", ".") + "</Qde>";
                            xml += "<Uom>" + dr[ii]["UNIDADEDEMEDIDA"].ToString() + "</Uom>";
                            xml += "<Barcode>" + dr[ii]["BARCODE"].ToString() + "</Barcode>";
                            xml += "</SKU>";
                        }
                        else
                        {
                            if (jaInseriu == false)
                            {
                                xml += "<SKU codigo=" + quote + dr[0]["NUMERO"].ToString() + quote + ">";
                                xml += "<PesoBruto>0</PesoBruto>";
                                xml += "<PesoLiquido>0</PesoLiquido>";
                                xml += "<Volumes>1</Volumes>";
                                xml += "<Descricao>DANFE " + dr[0]["NUMERO"].ToString() + "</Descricao>";
                                xml += "<Qde>1</Qde>";
                                xml += "<Uom>FL</Uom>";
                                xml += "<Barcode>" + ch + "</Barcode>";
                                xml += "</SKU>";
                                jaInseriu = true;
                            }
                        }
                    }
                    xml += "</SKUs> ";

                    xml += " </Documentos> ";

                    string auxXML = xml;
                    xml = Base64Encode(xml);

                    if (ch.Contains("NFE"))
                    {
                        ch = ch.Replace("NFE", "000");
                        gerouCahve = true;
                    }

                    //string NomeArquivo = ch + DateTime.Now.ToString("yyyyMMddhhmmssffftt") + ".xml";
                    string NomeArquivo = dtds.Rows[i]["IDDOCUMENTO"].ToString() + ".xml";

                    WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
                    string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
                                   "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<SOAP-ENV:Body>" +
                                   "<tns:importDocsToPOD xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
                                   "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
                                   "</tns:" + "importDocsToPOD" + ">" +
                                   "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    byte[] data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "text/xml; charset=ISO-8859-1";
                    request.Headers.Add("SOAPAction", "urn:WebServicePOD#getDocumentsFromPOD");
                    request.Headers.Add("Authorization", "Basic " + auth);
                    request.ContentLength = data.Length;


                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }


                    Cursor.Current = Cursors.WaitCursor;
                    WebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream());

                    XmlDocument xmlAwnser = new XmlDocument();
                    xmlAwnser.LoadXml(sr.ReadToEnd());


                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                    nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

                    Label2.Text = DateTime.Now + "-" + "Enviando Nota ao Comprovei. IdDocumento: " + sss;
                    Application.DoEvents();

                    string retorno = xmlAwnser["status"].InnerText;

                    retorno = retorno.Replace("'", "");
                    retorno = retorno.Replace("/", "");
                    retorno = retorno.Replace("\\", "");
                    retorno = retorno.Replace("??", "CA");

                    string sql = "";

                    if (retorno.ToUpper().Contains("WEVE GOT SOME PROBLEM"))
                    {
                        sql = "UPDATE DOCUMENTO SET EnviadoComprovei=null  WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
                        sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), '" + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
                        //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";

                        Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei ", "Problema no retorno em: " + DateTime.Now.ToString() + "- IdDocumento:" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + "<BR> " + retorno, "mail.grupologos.com.br", "logos0902", "Envio Comprovei - Problema no retorno do comprovei");

                    }
                    else
                    {

                        sql = "UPDATE DOCUMENTO SET EnviadoComprovei='ENVIADO COMPROVEI - " + DateTime.Now + "' " + (gerouCahve == true ? ", DocumentoDoCliente4='" + ch + "'" : "") + " WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
                        sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), 'Filial: " + dr[0]["REGIAO"].ToString() + "  " + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
                        //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";
                    }
                    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    GravarXML(auxXML, DateTime.Now);
                    GravarLog(ch, retorno);
                    // Label2.Text = "Terminou Envio Comprovei";

                    Label2.Text = "EnviarComprovei Termino: " + DateTime.Now.ToString();


                }
                catch (Exception ex)
                {
                    reiniciarTimers();
                    Label2.Text = "EnviarComprovei Erro: " + DateTime.Now.ToString();
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei ", "Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            EnviarComprovei(false, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //EnviarRota_Comprovei("", "4, 13");
            GravarPedidosHomeRefill();
            //EnviarRota_Comprovei(txtIdDt.Text, "");

            // GravarPedidosHomeRefill();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            EnviarComprovei(false, true);

            //    try
            //    {
            //        //  return;

            //        //GravarLog("Iniciou", "ProcessarTwx");
            //        Label2.Text = "ProcessarTwx Inicio: " + DateTime.Now.ToString();


            //        string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
            //        string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

            //        //GravarLog("Ambiente: " + ambiente, "ProcessarTwx");
            //        //GravarLog("URL: " + tbUrl, "ProcessarTwx");


            //        string varName = "";
            //        string varType = "";
            //        string xmlPath = "";
            //        string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("homerefill" + ":" + "admin"));


            //                varName = "key";
            //                varType = "string";

            //        xmlPath = "Retorno/Documentos/Documento";
            //        xmlPath = "Retorno/Documentos/item";

            //        //tbUrl = "http://ws.comprovei.com.br/server.php?wsdl";
            //        tbUrl = "http://soap.comprovei.com.br/WebServicePOD/server.php?wsdl";

            //        WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
            //        //string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
            //        //               "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
            //        //               "<SOAP-ENV:Body>" +
            //        //               "<tns:getDocumentsStatus xmlns:tns=\"urn:WebServicePOD\">" +
            //        //               "<" + varName + " xsi:type=\"xsd:" + varType + "\">" + ConfigurationSettings.AppSettings["QtdDocumentosPorChamada"] + "</" + varName + ">" +
            //        //               "</tns:" + "getDocumentsStatus" + ">" +
            //        //               "</SOAP-ENV:Body></SOAP-ENV:Envelope>";



            //        string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
            //                       "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
            //                       "<SOAP-ENV:Body>" +
            //                       "<tns:getADocumentFromPOD xmlns:tns=\"urn:WebServicePOD\">" +
            //                       "<" + varName + " xsi:type=\"xsd:" + varType + "\">" + "35170219364681000103550010000246911001532252" + "</" + varName + ">" +
            //                       "</tns:" + "getADocumentFromPOD" + ">" +
            //                       "</SOAP-ENV:Body></SOAP-ENV:Envelope>";



            //        byte[] data = Encoding.ASCII.GetBytes(postData);
            //        request.Method = "POST";
            //        request.ContentType = "text/xml; charset=ISO-8859-1";
            //        request.Headers.Add("SOAPAction", "urn:WebServicePOD#getDocumentsFromPOD");
            //        request.Headers.Add("Authorization", "Basic " + auth);
            //        request.ContentLength = data.Length;


            //        using (var stream = request.GetRequestStream())
            //        {
            //            stream.Write(data, 0, data.Length);
            //        }


            //        Cursor.Current = Cursors.WaitCursor;
            //        WebResponse response = (HttpWebResponse)request.GetResponse();
            //        StreamReader sr = new StreamReader(response.GetResponseStream());

            //        XmlDocument xmlAwnser = new XmlDocument();
            //        xmlAwnser.LoadXml(sr.ReadToEnd());

            //        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
            //        nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            //        xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

            //        string documents = "";
            //        int countDocument = 1;

            //        // GravarXML(xmlAwnser.InnerXml, DateTime.Now);

            //        if (xmlAwnser.InnerText.Contains("No documents done yet!"))
            //        {
            //            request = null;
            //            response = null;
            //            xmlAwnser = null;

            //            Cursor.Current = Cursors.Default;
            //            Label2.Text = "ProcessarTwx FIM: " + DateTime.Now.ToString();
            //            Thread.Sleep(30000);
            //            return;
            //        }


            //        //int idXML = GravarXml(xmlAwnser);                

            //        int xx = 0;
            //        foreach (XmlNode xmlDocument in xmlAwnser.SelectNodes(xmlPath))
            //        {
            //            xx += 1;
            //            //GravarLog("", "");
            //            //GravarLog("---------------------------------------------------------------------------------------------------", "");
            //            GravarLog("##: " + countDocument, "");


            //            if (xmlDocument["Erro"] == null)
            //            {
            //                string sKey = (xmlDocument["Chave"] != null) ? xmlDocument["Chave"].InnerText : "";
            //                GravarLog("Chave da Nota: " + sKey, "ProcessarTwx");
            //                sss = sKey;

            //                //PEGA A NOTA
            //                string sql = "SELECT IDDOCUMENTO FROM DOCUMENTO with (nolock) WHERE TIPODEDOCUMENTO IN('NOTA FISCAL', 'ORDEM DE SERVICO', 'LOGISTICA') AND TIPODESERVICO in ('TRANSPORTE','ENTREGA') AND DOCUMENTODOCLIENTE4='" + sKey + "'";
            //                DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            //                if (dtNota.Rows.Count == 0)
            //                {
            //                    sql = "select IDDOCUMENTO from DocumentoEletronico where IdNota = '" + sKey + "'";
            //                    dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            //                }

            //                //se nao achar a nota vai para a proxima
            //                if (dtNota.Rows.Count == 0)
            //                {
            //                    GravarLog("Não Encontro o documento:" + sKey, "ProcessarTwx");
            //                    continue;
            //                }

            //                gravarTabelasAuxiliares(xmlDocument, int.Parse(dtNota.Rows[0]["IDDOCUMENTO"].ToString()));                       

            //                countDocument++;
            //            }
            //            else
            //            {
            //                documents = xmlDocument["Erro"].InnerText;
            //            }

            //        }



            //        if (String.IsNullOrEmpty(documents))
            //        {
            //            documents = xmlAwnser.FirstChild["Erro"].InnerText;
            //            GravarLog(documents, "Erro");

            //        }

            //        request = null;
            //        response = null;
            //        xmlAwnser = null;

            //        Cursor.Current = Cursors.Default;

            //        Label2.Text = DateTime.Now + "- Recebeu " + xx + " Baixa do Comprovei";
            //        Application.DoEvents();

            //    }
            //    catch (Exception ex)
            //    {


            //        GravarLog(sss, "ProcessarComprovei" + ex.Message);
            //        reiniciarTimers();
            //    }
            //}


        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //GravarPedidosHomeRefill();
            SolicitarProtocolo();

        }

        private void btnEnviarBlueSoft_Click(object sender, EventArgs e)
        {
            EnviarConferenciaBlueSoft();
        }

        private void EnviarConferenciaBlueSoft()
        {
            timer1.Enabled = false;
            // using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new System.Uri("https://erp.bluesoft.com.br/api/comercial/produtos?produtoKey=199734&api_key=eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ7XCJ1c2VyTmFtZVwiOlwiYmx1ZXNvZnRfbWljaGVsXCIsXCJzZWNyZXRcIjpcIlluaEhYZUVSaGVLbUpRa1B3QlN6cnVPQ01oSHNTZHF4Q01SRVhwTWFGblwifSJ9.C6nZlsRBh9dGWSmvI5c0gJEhLTBJODMK01Vg7Ejaadg-74ofKoChibVubBhOFlgKDU-0cM5MwjaAFrneEG3GVw");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage response = await client.GetAsync("api/produtos/3");
            //    if (response.IsSuccessStatusCode)
            //    {  //GET
            //        Produto produto = await response.Content.ReadAsAsync<Produto>();
            //        Console.WriteLine("{0}\tR${1}\t{2}", produto.Nome, produto.Preco, produto.Categoria);
            //        Console.WriteLine("Produto acessado e exibido.  Tecle algo para incluir um novo produto.");
            //        Console.ReadKey();
            //    }
            //    //POST
            //    var cha = new Produto() { Nome = "Chá Verde", Preco = 1.50M, Categoria = "Bebidas" };
            //    response = await client.PostAsJsonAsync("api/produtos", cha);
            //    Console.WriteLine("Produto cha verde incluído. Tecle algo para atualizar o preço do produto.");
            //    Console.ReadKey();
            //    if (response.IsSuccessStatusCode)
            //    {   //PUT
            //        Uri chaUrl = response.Headers.Location;
            //        cha.Preco = 2.55M;   // atualiza o preco do produto
            //        response = await client.PutAsJsonAsync(chaUrl, cha);
            //        Console.WriteLine("Produto preço do atualizado. Tecle algo para excluir o produto");
            //        Console.ReadKey();
            //        //DELETE
            //        response = await client.DeleteAsync(chaUrl);
            //        Console.WriteLine("Produto deletado");
            //        Console.ReadKey();
            //    }
            //}
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //GravarPedidosHomeRefill();
            for (int i = 0; i < 200; i++)
            {
                ConsultarProtocolo(false);
                    AcertarNotasNoStistranetDoComprovei();


            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProcessarImagensComprovei();
            Application.Exit();
        }

        private void ProcessarImagensComprovei()
        {
            try
            {
                string sql = "Select distinct  do.IDDocumentoOcorrencia, rci.* from RetornoComproveiImagem rci with (nolock) inner join DocumentoOcorrencia do with (nolock)  on do.IdOcorrenciaComprovei = rci.IdOcorrenciaComprovei";
                DataTable dtf = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                for (int i = 0; i < dtf.Rows.Count; i++)
                {
                    string CaminhoImagem = "";
                    CaminhoImagem = dtf.Rows[i]["Link"].ToString();


                    try
                    {
                        var request = WebRequest.Create(CaminhoImagem);
                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            Bitmap b = (Bitmap)Bitmap.FromStream(stream);
                            byte[] fotoArray = ConvertBitMapToByteArray(b);

                            if (fotoArray != null)
                            {

                                string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                string sql1 = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + dtf.Rows[i]["IDDOCUMENTOOCORRENCIA"].ToString() + ", @foto)  ";

                                SqlConnection vv = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                SqlCommand command = new SqlCommand();

                                command.CommandText = sql1;
                                command.CommandType = CommandType.Text;
                                command.Connection = vv;
                                command.Parameters.Add(new SqlParameter("@Foto", fotoArray));

                                vv.Open();
                                command.ExecuteNonQuery();

                                sql1 = "Delete from  RetornoComproveiImagem where IdOcorrenciaComprovei=" + dtf.Rows[i]["IdOcorrenciaComprovei"].ToString();
                                command.CommandText = sql1;
                                command.CommandType = CommandType.Text;
                                command.ExecuteNonQuery();

                                vv.Close();
                                vv.Dispose();
                            }
                        }
                    }
                    catch (Exception xx)
                    {

                    }
                }
            }
            catch (Exception)
            { }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            EnviarPedidoJojapar();
        }

        private void EnviarPedidoJojapar()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            try
            {

                string sql = "  Select top 1 *, c.Nome Cidade, est.Uf, b.Nome Bairro  ";
                sql += " from Documento d  with (nolock)  ";
				sql += " Inner Join DocumentoFilial DF on df.IdDocumento = d.IdDocumento ";
                sql += " inner join Cidade c on c.IdCidade = d.IDEnderecoCidade ";
                sql += " inner join Estado est on est.IDEstado = c.IDEstado  ";
                sql += "left join Bairro b on b.IdBairro = d.IdEnderecoBairro";
                sql += " Where IDCliente = 3703114 and TipoDeDocumento='Pedido' and (df.Situacao='LIBERADO PARA SEPARACAO'  OR d.payment like '%CRL-%' ) And EnviadoJosapar is null";
                DataTable dtf = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                if (dtf.Rows.Count == 0)
                    return;

                sql = "exec PRC_RETORNAR_PEDIDOS_JOSAPAR " + dtf.Rows[0]["IdDocumento"].ToString() + ", " + dtf.Rows[0]["IdDestinatario"].ToString();
                //dt 0 - Cadastro Destinatario
                //dt 1 - Itens do Documento
                //dt 3 =- ProdutoCliente

                DataSet ds = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    Josapar.OrderService.BOOrderIntegrationSoapClient ws_order = new Josapar.OrderService.BOOrderIntegrationSoapClient();
                    using (new OperationContextScope(ws_order.InnerChannel))
                    {
                        HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                        //requestMessage.Headers["Authorization"] = "Basic VEVTVEU6VEVTVEU=";  //MTYwNjIwOlBXRDE2MDYyMA==
                        requestMessage.Headers["Authorization"] = "Basic MTYwNjIwOlBXRDE2MDYyMA==";

                        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;

                        Josapar.OrderService.integrateOrderRequest req = new Josapar.OrderService.integrateOrderRequest();
                        req.order = new Robo.Email.Notas.Solutions.Windows.Testes.Josapar.OrderService.order();

                        //Preencher objectos e dados do pedido, conforme manual enviado pela infra -->

                        req.order.orderId = (dtf.Rows[0]["Numero"].ToString() == "" ? dtf.Rows[0]["NumeroOriginal"].ToString() : dtf.Rows[0]["Numero"].ToString());//
                        req.order.totalAmount = (decimal)float.Parse(dtf.Rows[0]["ValorDasMercadorias"].ToString().Replace(",", "."));//
                        req.order.totalDiscountAmount = (decimal)float.Parse(dtf.Rows[0]["ValorDeDesconto"].ToString().Replace(",", "."));//
                        req.order.purchaseDate = DateTime.Parse(dtf.Rows[0]["DataDeEmissao"].ToString());//
                        req.order.applicationVersion = "ACEC_V1";
                        req.order.saleChannel = "INT";

                        List<Josapar.OrderService.delivery> lDel = new List<Josapar.OrderService.delivery>();
                        Josapar.OrderService.delivery del = new Josapar.OrderService.delivery();

                        #region Custumer

                        Josapar.OrderService.customer cus = new Josapar.OrderService.customer();
                        cus.customerId = ds.Tables[0].Rows[0]["customerId"].ToString();
                        cus.documentNumber = ds.Tables[0].Rows[0]["CNPJCPF"].ToString().Replace("-", "").Replace(".", "").Replace("/", "");

                        cus.name = ds.Tables[0].Rows[0]["RazaoSocialNome"].ToString();
                        cus.email = ds.Tables[0].Rows[0]["Email"].ToString();
                        cus.phoneMobile = ds.Tables[0].Rows[0]["TelefoneCelular"].ToString();
                        cus.phoneOffice = ds.Tables[0].Rows[0]["TelefoneComercial"].ToString();
                        cus.createDt = DateTime.Parse(ds.Tables[0].Rows[0]["DataDeCadastro"].ToString());

                        req.order.customer = cus;
                        req.order.customerId = cus.customerId;

                        #endregion

                        #region Deliverys
                        del.deliveryId = "1";
                        del.orderId = req.order.orderId;
                        del.totalAmount = req.order.totalAmount;
                        del.totalDiscountAmount = req.order.totalDiscountAmount;



                        #region deliveryAddress

                        Josapar.OrderService.address add = new Josapar.OrderService.address();


                        //Endereco de Entrega
                        add.addressId = dtf.Rows[0]["addressId"].ToString();
                        add.recipientNm = ds.Tables[0].Rows[0]["RazaoSocialNome"].ToString();
                        add.address1 = dtf.Rows[0]["Endereco"].ToString();
                        add.addressNr = dtf.Rows[0]["EnderecoNumero"].ToString();
                        add.additionalInfo = dtf.Rows[0]["EnderecoComplemento"].ToString();
                        add.quarter = dtf.Rows[0]["Bairro"].ToString();
                        add.city = dtf.Rows[0]["Cidade"].ToString();
                        add.state = dtf.Rows[0]["UF"].ToString();
                        add.friendlyNm = "Endereco Principal";
                        add.countryId = "BR";
                        add.postalCd = dtf.Rows[0]["EnderecoCEP"].ToString();
                        del.deliveryAddress = add;


                        //Endereço de Faruramento
                        add = new Josapar.OrderService.address();

                        add.addressId = ds.Tables[0].Rows[0]["addressId"].ToString();
                        add.recipientNm = ds.Tables[0].Rows[0]["RazaoSocialNome"].ToString();
                        add.address1 = ds.Tables[0].Rows[0]["Endereco"].ToString();
                        add.addressNr = ds.Tables[0].Rows[0]["Numero"].ToString();
                        add.additionalInfo = ds.Tables[0].Rows[0]["Complemento"].ToString();
                        add.quarter = ds.Tables[0].Rows[0]["Bairro"].ToString();
                        add.city = ds.Tables[0].Rows[0]["Cidade"].ToString();
                        add.state = ds.Tables[0].Rows[0]["UF"].ToString();
                        add.friendlyNm = "Endereco Faturamento";
                        add.countryId = "BR";
                        add.postalCd = ds.Tables[0].Rows[0]["CEP"].ToString();

                        req.order.billingAddress = add;

                        #endregion
                        lDel.Add(del);
                        req.order.deliveries = lDel.ToArray();


                        #endregion

                        #region orderLineList

                        DataTable pc = ds.Tables[2];
                        DataTable di = ds.Tables[3];

                        List<Josapar.OrderService.orderLine> orderLineList = new List<Josapar.OrderService.orderLine>();
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            Josapar.OrderService.orderLine orderLine = new Josapar.OrderService.orderLine();
                            DataRow[] dr = pc.Select("IdProdutoCliente=" + ds.Tables[2].Rows[i]["IdProdutoCliente"].ToString(), "");
                            DataRow[] drFrete = di.Select("IdDocumentoItem=" + ds.Tables[3].Rows[i]["IdDocumentoItem"].ToString(), "");

                            if (dr.Length > 0)
                            {
                                orderLine.sku = dr[0]["Codigo"].ToString();
                                orderLine.skuType = ds.Tables[1].Rows[i]["skuType"].ToString();
                                orderLine.quantity = Convert.ToInt32(decimal.Parse(ds.Tables[1].Rows[i]["QUANTIDADE"].ToString()));
                                orderLine.catalogListPrice = Convert.ToDecimal(ds.Tables[1].Rows[i]["catalogListPrice"].ToString());
                                orderLine.listPrice = Convert.ToDecimal(ds.Tables[1].Rows[i]["listPrice"].ToString());
                                orderLine.salePrice = Convert.ToDecimal(ds.Tables[1].Rows[i]["listPrice"].ToString());
                                orderLine.unconditionalDiscountAmount = Convert.ToDecimal(0);
                                orderLine.roundingDiscountAmount = Convert.ToDecimal(ds.Tables[1].Rows[i]["roundingDiscountAmount"].ToString());


                                if (drFrete.Length > 0)
                                {
                                    Josapar.OrderService.freight Frete = new Josapar.OrderService.freight();

                                    Frete.chargedAmount = Convert.ToDecimal(float.Parse(drFrete[0]["ChargedAmount"].ToString().Replace(",", ".")));
                                    Frete.actualAmount = Convert.ToDecimal(float.Parse(drFrete[0]["ChargedAmount"].ToString().Replace(",", ".")));
                                    //Frete.freightRoundingAmount = Convert.ToDecimal(float.Parse(drFrete[0]["FreightRoundingAmount"].ToString().Replace(",", ".")));

                                    Frete.freightTime = drFrete[0]["freightTime"].ToString();
                                    Frete.pickupLeadTime = drFrete[0]["pickupLeadTime"].ToString();
                                    Frete.logisticContract = drFrete[0]["logisticContract"].ToString();

                                    orderLine.freight = Frete;

                                }

                                orderLine.service = false;
                                orderLine.skuName = dr[0]["Descricao"].ToString();
                                orderLine.productBrandName = ds.Tables[1].Rows[i]["productBrandName"].ToString();

                                orderLineList.Add(orderLine);

                            }
                        }

                        del.orderLineList = orderLineList.ToArray();


                        #endregion

                        #region Payment

                        List<Josapar.OrderService.payment> lpay = new List<Josapar.OrderService.payment>();

                        Josapar.OrderService.payment pay = new Josapar.OrderService.payment()
                        {
                            Item = new Josapar.OrderService.abstractPayment() { paymentType = dtf.Rows[0]["Payment"].ToString() }
                        };




                        lpay.Add(pay);

                        req.order.paymentList = lpay.ToArray();


                        #endregion

                        //Enviar Pedido Obtendo Retorno
                        Robo.Email.Notas.Solutions.Windows.Testes.Josapar.OrderService.integrateOrderResponse response = ws_order.integrateOrder(req);

                        if (response.message == "Pedido recebido com sucesso.")
                        {
                            sql = "Update Documento set /* Ativo='SIM',*/ EnviadoJosapar='" + DateTime.Now + "' where IdDocumento=" + dtf.Rows[0]["IdDocumento"].ToString() + "; select 1";
                            DataSet dss = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void Label2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=-23.5524617,-46.6993093&sensor=false";
                    string requestUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=-23.5524617,-46.6993093&sensor=false";

                    string url = requestUri;
                    WebRequest request = WebRequest.Create(url);
                    using (WebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            DataSet dsResult = new DataSet();
                            dsResult.ReadXml(reader);

                        }
                    }

                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            XmlDocument xmlSoapRequest = new XmlDocument();

            try
            {
                Tracking tr = new Tracking();
				InfraTracking.captureTrackingRequest request = new InfraTracking.captureTrackingRequest();

                tr.orderId = 1222;
                tr.controlPointId = "MAP"; // pedido Pago 
                                           //                tr.controlPointId = "MNA"; // pedido Reprovado 

                //                tr.controlPointId = "WMS"; // Table  de tipos de Tracking
                tr.controlPointNm = "Limite de Crédito aprovado";
                //                tr.controlPointNm = "Separação e embalagem";
                tr.occurrenceDt = DateTime.Now.AddMinutes(-40);


                //InfraTracking.InvoiceInfo inv = new InfraTracking.InvoiceInfo();
                //inv.issuerDocumentNr =123654;
                //inv.invoiceNumber = 123654;
                //inv.invoiceSerialNumber = "13985401888";
                //inv.invoiceEmissionDate = DateTime.Now;
                //inv.invoiceEletronicKey = "0000000000000000000000000000000000000000000";

                var ojData = new List<InfraTracking.ObjectData>();

                ojData.Add(new InfraTracking.ObjectData() { objectId = "1" });

                //inv.objectDataList =ojData.ToArray();

                //  tr.invoiceInfo = inv;

                List<SkuDeliveryTracking> dTrac = new List<SkuDeliveryTracking>();

                tr.skuDeliveryTrackingList = dTrac.ToArray();


                var lista = new List<InfraTracking.Tracking>();
                lista.Add(tr);
                request.trackingList = lista.ToArray();

				InfraTracking.TrackingServicesClient serv = new InfraTracking.TrackingServicesClient();
                serv.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
                serv.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
                var resp = serv.captureTracking(request);
            }
            catch (Exception ex)
            {
                // throw ex;
                //Sistran.Library.EnviarEmails.EnviarEmailPed("moises@sistecno.com.br", "sistema@grupologos.com.br", "Erro. TrackingTeste", ex.Message, "mail.grupologos.com.br", "logos0902", "Erro. TrackingTeste");

            }
        }

		


		private void button11_Click(object sender, EventArgs e)
		{
			EnviarEstoqueJosapar();

		}

		private void EnviarEstoqueJosapar()
		{
			//eviar quabdo houver baixa de estoque.
			//entrada e saida
			string sqlBaixaReg = "";
			string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
			string sql = "Select top 50 * from JosaparEstoque where EnviadoJosapar is null order by 1";

			DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);



			List<logs> l = new List<logs>();

			setStockRequest estR = new setStockRequest();
			var lista = new List<stock>();

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				stock ex = new stock()
				{
					skuId = dt.Rows[i]["Codigo"].ToString(),
					quantity = Convert.ToInt32(dt.Rows[i]["SALDO"].ToString()),
					stockTypeSourceName = "controlUnitQuantity"
				};
				lista.Add(ex);

				l.Add(new logs()
				{
					datahora = DateTime.Now.ToString(),
					acao = "sku: " + ex.skuId + ", Quantidade: " + ex.quantity + " ,  stockTypeSourceName: " + ex.stockTypeSourceName.ToString()
				});

				sqlBaixaReg += "Update JosaparEstoque set EnviadoJosapar=getDate() where IdJosaparEstoque=" + dt.Rows[i]["IdJosaparEstoque"].ToString() + "; ";
			}

			estR.storeId = "JOSAPAR";
			estR.stockList = lista.ToArray();
			StockServicesClient x = new StockServicesClient();

			x.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
			x.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
			var resposta = x.setStock(estR);

			string sqlLog = "";
			for (int i = 0; i < l.Count; i++)
			{
				sqlLog += "insert into logsInfracomence(DataHora, Acao, Status) values ('" + l[i].datahora + "', '" + l[i].acao + "', '" + (resposta == null ? "-" : resposta.result.ToString()) + "'); ";

			}

			if (sqlLog.Length > 5 )
			{
				Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlLog, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

				if (resposta.result.ToUpper() == "SUCESSO")
					Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlBaixaReg, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
			}

		}

		public string RetornarInvoices(DataRow[] row)
		{

			string str = "<inv:invoiceInfo>";

			//for (int i = 0; i < row.Length; i++)
			//{

				str += "<inv:issuerDocumentNr>"+ row[0]["issuerDocumentNr"].ToString() + "</inv:issuerDocumentNr>" +
					"<inv:invoiceNumber>"+ row[0]["invoiceNumber"].ToString() + "</inv:invoiceNumber>" +
					"<inv:invoiceSerialNumber>"+ row[0]["invoiceSerialNumber"].ToString() + "</inv:invoiceSerialNumber>" +
					"<inv:invoiceEmissionDate>" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.000") + "-03:00</inv:invoiceEmissionDate>" +
					"<inv:invoiceEletronicKey>" + row[0]["invoiceEletronicKey"].ToString() + "</inv:invoiceEletronicKey>" +
					"<inv:objectDataList>" +
					"<inv:objectData>" +
					"<inv:objectId>1</inv:objectId>" +
					"</inv:objectData>" +
					"</inv:objectDataList>";
				//}

			str += "</inv:invoiceInfo>";

			return str;
		}


		public string RetornarSKUS(DataRow[] row)
		{
			string ret = "";
			ret = "<trac1:skuDeliveryTrackingList>";
			for (int ii = 0; ii < row.Length; ii++)
			{
				if (row[ii]["skuId"].ToString() != "")
				{
					ret += "<trac1:skuDeliveryTracking>" +
							"<trac1:skuId>"+ row[ii]["skuId"].ToString() + "</trac1:skuId>" +
							"<trac1:preparedQt>"+ Convert.ToInt32(decimal.Parse(row[ii]["preparedQt"].ToString())) + "</trac1:preparedQt>" +
							"<trac1:unitPrice>" + decimal.Parse(row[ii]["unitPrice"].ToString()) + "</trac1:unitPrice>" +
							"</trac1:skuDeliveryTracking>";
			

					//dTrac.Add(new SkuDeliveryTracking()
					//{
					//	orderId = int.Parse(r[0]["orderId"].ToString()),
					//	skuId = (r[ii]["skuId"].ToString() == "" ? "" : r[ii]["skuId"].ToString()),
					//	preparedQt = (r[ii]["preparedQt"].ToString() == "" ? 0 : Convert.ToInt32(decimal.Parse(r[ii]["preparedQt"].ToString()))),
					//	unitPrice = (r[ii]["unitPrice"].ToString() == "" ? 0 : decimal.Parse(r[ii]["unitPrice"].ToString()))
					//});
				}
			}
			ret += "</trac1:skuDeliveryTrackingList>";
			return ret;
		}
		public XmlDocument ProcessaJosaparNfs(DataRow[] row)
		{
			WebRequest request = (HttpWebRequest)WebRequest.Create("https://is-josapar-dev.a8e.net.br/b2b/tracking");
			string postData = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:trac=\"http://www.accurate.com/acec/TrackingServices\" xmlns:trac1=\"http://www.accurate.com/acec/Tracking\" xmlns:inv=\"http://www.accurate.com/acec/InvoiceInfo\">" +
			"<soapenv:Body>" +
			"<trac:captureTrackingRequest>" +
			"<trac1:trackingList>" +
			"<trac1:tracking>" +

			"<trac1:orderId>" + row[0]["orderId"].ToString() + " </trac1:orderId>" +
			"<trac1:controlPointId>NFS</trac1:controlPointId>" +
			"<trac1:controlPointNm>NF Emitida</trac1:controlPointNm>" +
			"<trac1:occurrenceDt>" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.000") + "-03:00</trac1:occurrenceDt>" +
			"<trac1:invoiceURL>" + row[0]["invoiceURL"].ToString() + " </trac1:invoiceURL>" +

			RetornarInvoices(row) +
			RetornarSKUS(row)+
			
			"</trac1:tracking>" +
			"</trac1:trackingList>" +
			"</trac:captureTrackingRequest>" +
			"</soapenv:Body>" +
			"</soapenv:Envelope>"
;
			byte[] data = Encoding.ASCII.GetBytes(postData);
			request.Method = "POST";
			string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(ConfigurationSettings.AppSettings["UserJosapar"] + ":" + ConfigurationSettings.AppSettings["PasswodrJosapar"]));
			request.Headers.Add("Authorization", "Basic " + auth);
			request.ContentLength = data.Length;


			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}


			Cursor.Current = Cursors.WaitCursor;
			WebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader sr = new StreamReader(response.GetResponseStream());

			try
			{
				XmlDocument xmlAwnser = new XmlDocument();
				xmlAwnser.LoadXml(sr.ReadToEnd());
				
				return xmlAwnser;
			}
			catch (Exception)
			{
				return null;
			}
		}

		private void EnviarTrackinBD()
		{
			string sql = "select * from JosaparTracking jt with(nolock) left join JosaparSkuDeliveryTracking jdt on jdt.IdJosaparTracking = jt.IdJosaparTracking Where EnviadoInfracommerce is null ";
			
			DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

			DataView view = new DataView(dt);
			DataTable distinctValues = view.ToTable(true, "IdJosaparTracking");

			for (int i = 0; i < distinctValues.Rows.Count; i++)
			{
				DataRow[] r = dt.Select("IdJosaparTracking='" + distinctValues.Rows[i]["IdJosaparTracking"].ToString() + "'", "IdJosaparTracking");

				if(r.Length>0)
				{
				
					InfraTracking.Tracking tr = new InfraTracking.Tracking();

					if (r[0]["controlPointId"].ToString() == "NFS")
					{
						var x = ProcessaJosaparNfs(r);

						if(x!= null && x.InnerXml.Contains("<success>true</success>"))
						{
							sql = "update JosaparTracking set EnviadoInfracommerce=getdate() where IdJosaparTracking='" + distinctValues.Rows[i]["IdJosaparTracking"].ToString() + "'";
							Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
						}
					}
					else
					{

						tr.orderId = int.Parse(r[0]["orderId"].ToString());
						tr.controlPointId = r[0]["controlPointId"].ToString();
						tr.controlPointNm = r[0]["controlPointNm"].ToString();
						tr.occurrenceDt = DateTime.Now;

						InfraTracking.InvoiceInfo inv = new InfraTracking.InvoiceInfo();
						tr.invoiceInfo = inv;

						if (r[0]["invoiceURL"].ToString().Length > 0)
							tr.invoiceURL = r[0]["invoiceURL"].ToString();

						if (r[0]["issuerDocumentNr"].ToString().Length > 0)
						{
							tr.invoiceInfo.issuerDocumentNr = long.Parse(r[0]["issuerDocumentNr"].ToString());

							if (r[0]["invoiceNumber"].ToString().Length > 0)
								tr.invoiceInfo.invoiceNumber = int.Parse(r[0]["invoiceNumber"].ToString());

							if (r[0]["invoiceSerialNumber"].ToString().Length > 0)
								tr.invoiceInfo.invoiceSerialNumber = r[0]["invoiceSerialNumber"].ToString();

							if (r[0]["invoiceEmissionDate"].ToString().Length > 0)
								tr.invoiceInfo.invoiceEmissionDate = DateTime.Now; //DateTime.Parse(r[0]["invoiceEmissionDate"].ToString());

							if (r[0]["invoiceEletronicKey"].ToString().Length > 0)
								tr.invoiceInfo.invoiceEletronicKey = r[0]["invoiceEletronicKey"].ToString();

							if (r[0]["objectId"].ToString().Length > 0)
							{
								var ojData = new List<InfraTracking.ObjectData>();
								ojData.Add(new InfraTracking.ObjectData() { objectId = r[0]["objectId"].ToString() });
								tr.invoiceInfo.objectDataList = ojData.ToArray();
							}
							else
							{
								var ojData = new List<InfraTracking.ObjectData>();
								ojData.Add(new InfraTracking.ObjectData() { objectId = "1" });
								tr.invoiceInfo.objectDataList = ojData.ToArray();
							}
							//tr.invoiceInfo = inv;
						}

						List<SkuDeliveryTracking> dTrac = new List<SkuDeliveryTracking>();

						for (int ii = 0; ii < r.Length; ii++)
						{
							if (r[ii]["skuId"].ToString() != "")
							{
								dTrac.Add(new SkuDeliveryTracking()
								{
									orderId = int.Parse(r[0]["orderId"].ToString()),
									skuId = (r[ii]["skuId"].ToString() == "" ? "" : r[ii]["skuId"].ToString()),
									preparedQt = (r[ii]["preparedQt"].ToString() == "" ? 0 : Convert.ToInt32(decimal.Parse(r[ii]["preparedQt"].ToString()))),
									unitPrice = (r[ii]["unitPrice"].ToString() == "" ? 0 : decimal.Parse(r[ii]["unitPrice"].ToString()))
								});
							}
						}

						if (dTrac.Count > 0)
							tr.skuDeliveryTrackingList = dTrac.ToArray();


						var lista = new List<InfraTracking.Tracking>();
						lista.Add(tr);


						InfraTracking.captureTrackingRequest request = new InfraTracking.captureTrackingRequest();
						request.trackingList = lista.ToArray();


						InfraTracking.TrackingServicesClient serv = new InfraTracking.TrackingServicesClient();
						serv.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
						serv.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
						var resp = serv.captureTracking(request);

						//var soapRequest = SoapLoggerExtension.TraceExtension.XmlRequest.InnerXml;
						//var soapResponse = SoapLoggerExtension.TraceExtension.XmlResponse.InnerXml;

						if (resp.success)
						{
							sql = "update JosaparTracking set EnviadoInfracommerce=getdate() where IdJosaparTracking='" + distinctValues.Rows[i]["IdJosaparTracking"].ToString() + "'";
							Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
						}
					}
				}
			}			
		}

		private void button13_Click(object sender, EventArgs e)
		{
			EnviarTrackinBD();
		}
	}

	public struct logs
	{
		public string datahora { get; set; }
		public string acao { get; set; }
		public string resposta { get; set; }
	}
}

