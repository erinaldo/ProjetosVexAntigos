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
using Robo.Email.Notas.Solutions.Windows.Testes.Josapar.OrderService;
using Robo.Email.Notas.Solutions.Windows.Testes.InfraTracking;
//using ServicosWEB.wsInfraStock;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using RestSharp;

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
            if (this.BackColor == Color.Yellow)
            {
                reiniciarOperacao();
            }
        }

        private void reiniciarTimers()
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
            Application.DoEvents();
        }

        public void ConsultarProtocoloNaMaoChamar()
        {
            DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin("Exec prc_test", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            for (int i = 0; i < dtNota.Rows.Count; i++)
            {
                ConsultarProtocoloNaMao(false, dtNota.Rows[i]["Protocolo"].ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if(this.BackColor == Color.Yellow)
            {
                reiniciarOperacao();
            }

            try
            {

                SolicitarProtocolo();



            }
            catch (Exception)
            {
            }

            try
            {
                AtualizarPosicesDeClientes();
                for (int i = 0; i < 2; i++)
                {
                    try
                    {
                       // EnviarTrackinBD();

                        EnviarTrackinWmsBD();
                        EnviarTrackinWmsBD();
                        EnviarTrackinWmsBD();
                        EnviarTrackinWmsBD();
                        EnviarTrackinWmsBD();

                    }
                    catch (Exception)
                    {


                    }

                }
            }
            catch (Exception)
            {

            }

            try
            {
                DataTable dataTable = NotasComOcorrenciasNestle();
                processarNestle(dataTable);
            }
            catch (Exception)
            {                
            }

            try
            {
                Label2.Text = DateTime.Now + "- Ocorrencias frioVix";
                Application.DoEvents();
                EnviarOcorrenciaFrioVIX();
            }
            catch (Exception)
            {

            }

            try
            {

                try
                {
                    
                    Label2.Text = DateTime.Now + "- Ocorrencias ViaVarejo";
                    Application.DoEvents();                 

                    try
                    {
                        Label2.Text = DateTime.Now + "- Ocorrencias ViaVarejo -  Inicio";
                        Application.DoEvents();

                        DataTable dtOcoVia = NotasComOcorrenciasViaVarejoSislogic();
                        for (int i = 0; i < dtOcoVia.Rows.Count; i++)
                        {
                            processarViaVarejoImagemERecebedor(dtOcoVia.Rows[i]["IdDocumentoOcorrencia"].ToString());
                        }


                        Label2.Text = DateTime.Now + "- Ocorrencias ViaVarejo -  FIM";
                        Application.DoEvents();
                    }
                    catch (Exception)
                    {

                      //  throw;
                    }
                    
                }
                catch (Exception ex)
                {

                    Label2.Text = DateTime.Now + " - Erro - Ocorrencias ViaVarejo. " + ex.Message;
                    Application.DoEvents();
                }

              


                try
                {          
                    
                    SolicitarProtocolo();

                   
           
                }
                catch (Exception)
                {
                }
                
                try
                {
                    ConsultarProtocoloNaMaoChamar();
                    ConsultarProtocolo(false);
                }
                catch (Exception)
                {
                }

                
             

                DateTime horaAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                DateTime horaean6 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
                DateTime horaean8 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                DateTime horaean10 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
                DateTime horaean12 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
                DateTime horaean14 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0);
                DateTime horaean16 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0);
                DateTime horaean18 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);


                #region Açoes

                Label2.Text = "Consultando Protocolo Comprovei";
                Application.DoEvents();
                try
                {
                    //for (int i = 0; i < 200; i++)
                    //{
                        ConsultarProtocolo(false);
                        ConsultarProtocolo(false);
                    //}
                  
                }
                catch (Exception)
                {

                   
                }


                //comentado foi tranferido para um oitro robo
                //Label2.Text = DateTime.Now + "- Processando notas que o comprovei deu baixa";
                //Application.DoEvents();
                //for (int i = 0; i < 5; i++)
                //{
                // AcertarNotasNoStistranetDoComprovei();
                //}


                if (checkBox2.Checked)
                {
                    ProcessarComprovei();
                    Label2.Text = DateTime.Now + "- Recebeu Baixa do Comprovei";
                    Application.DoEvents();
                }


                Label2.Text = DateTime.Now + "- Enviou Notas Do Sistranet para o Comprovei";
                Application.DoEvents();

                if (DateTime.Now.Minute % 5 == 0)
                {
                    try
                    {
                        ConsultarProtocoloNovaTentativa();

                    }
                    catch (Exception)
                    {
                    }
                }
                try
                {
                    DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin("Exec prc_test", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    for (int i = 0; i < dtNota.Rows.Count; i++)
                    {
                        Label2.Text = DateTime.Now + "- Processando protocolos com erros";
                        Application.DoEvents();
                        ConsultarProtocoloNaMao(false, dtNota.Rows[i]["Protocolo"].ToString());

                    }

                }
                catch (Exception)
                {
                }
                ////////////////////////////////

                try
                {
                    Label2.Text = DateTime.Now + "- Processando Imagens Comprovei";
                    Application.DoEvents();

                    if(DateTime.Now.Hour ==0 || DateTime.Now.Hour == 1 )
                        ProcessarImagensComprovei();
                }
                catch (Exception)
                { }

                try
                {
                    if (chkHabilitaJosapar.Checked)
                    {

                        try
                        {
                            Label2.Text = "Enviando Pedido Josapar";
                            EnviarPedidoJojapar();
                        }
                        catch (Exception)
                        { }


                        try
                        {
                            Label2.Text = "Enviando Tracking";
                            EnviarTrackinBD();

                            for (int i = 0; i < 11; i++)
                            {
                                EnviarTrackinWmsBD();
                                EnviarTrackinWmsBD();
                                EnviarTrackinWmsBD();
                                EnviarTrackinWmsBD();
                                EnviarTrackinWmsBD();
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            string m = ex.Message;
                        }


                        try
                        {
                            Label2.Text = "Enviando Estoque";
                            //EnviarEstoqueJosapar();
                           // processarViaVarejo(NotasComOcorrenciasViaVarejo("33.041.260/0947-11"));
                            //processarViaVarejo(NotasComOcorrenciasViaVarejo("33.041.260/1615-08"));
                            //processarViaVarejo(NotasComOcorrenciasViaVarejo("33.041.260/0750-91"));
                        }
                        catch (Exception)
                        { }



                        try
                        {
                            Label2.Text = "Enviando Via Varejo";
                            //EnviarEstoqueJosapar();
                            //processarViaVarejo(NotasComOcorrenciasViaVarejo("33.041.260/0947-11"));
                            //processarViaVarejo(NotasComOcorrenciasViaVarejo("33.041.260/1615-08"));
                            //processarViaVarejo(NotasComOcorrenciasViaVarejo("33.041.260/0750-91"));                            
                            //processarViaVarejo(NotasComOcorrenciasViaVarejo("33.041.260/1195-60"));
                            //processarViaVarejo(NotasComOcorrenciasViaVarejo("33.041.260/1614-19"));
                            //processarViaVarejo(NotasComOcorrenciasViaVarejo("33.041.260/1617-61"));
                            


                            GerarTrackingEntrega();

                        }
                        catch (Exception)
                        { }


                        try
                        {
                            Label2.Text = DateTime.Now + "- Relacionando Pedidos";
                            CriarDocumentoRelacionado();
                            GerarTrackingEntrega();
                            GerarTrackingEMEntrega();
                        }
                        catch (Exception)
                        { }


                        try
                        {
                            Label2.Text = DateTime.Now + "- Ocorrencias Dafiti";

                            enviarOcorrenciaDafit();
                            enviarOcorrenciaDafit();
                            enviarOcorrenciaDafit();
                            enviarOcorrenciaDafit();
                            enviarOcorrenciaDafit();
                            enviarOcorrenciaDafit();
                        }
                        catch (Exception)
                        {

                            
                        }


                     
                    }
                }
                catch (Exception)
                { }
                #endregion

                Label2.Text = DateTime.Now + "- Último Processamento";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                reiniciarTimers();
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Timer ", "erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Erro no Timer");

                if (this.BackColor == Color.Yellow)
                {
                    reiniciarOperacao();
                }
            }
            finally
            {
                timer1.Enabled = true;
            }
        }

        private void AtualizarPosicesDeClientes()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            try
            {

              string sql = "Select  ual.IDUnidadeDeArmazenagemLote, UAL.IDUnidadeDeArmazenagem, LT.DataDeEntrada, cast(sum(LT.Quantidade) as int) Entrada " +
                " From ProdutoCliente PC with(Nolock) " +
                " Inner Join Estoque ES with(NoLock) on ES.IDProdutoCliente = PC.IdProdutoCliente " +
                " Inner Join Lote LT with(NoLock) on LT.IDEstoque = ES.IDEstoque and LT.IDProdutoCliente = PC.IDProdutoCliente " +
                " Inner Join UnidadeDeArmazenagemLote UAL with(NoLock) on UAL.IDLote = LT.IdLote " +
                " where UAL.Entrada IS NOT NULL AND UAL.ArmazenagemEntrada IS NULL " +
                " Group by ual.IDUnidadeDeArmazenagemLote, UAL.IDUnidadeDeArmazenagem, LT.DataDeEntrada " +
                " order by 2";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "Select * from Armazenagem where IdUnidadeDeArmazenagem=" + dt.Rows[i]["IDUnidadeDeArmazenagem"].ToString();
                    DataTable dti = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                    if (dti.Rows.Count > 0)
                        sql = "Update Armazenagem set Quantidade=isnull(Quantidade,0) + " + dt.Rows[i]["Entrada"].ToString() + " where IDUnidadeDeArmazenagem =" + dt.Rows[i]["IDUnidadeDeArmazenagem"].ToString() + " ; Update UnidadeDeArmazenagemLote set ArmazenagemEntrada=getdate() where IdUnidadeDeArmazenagemLote=" + dt.Rows[i]["IDUnidadeDeArmazenagemLote"].ToString();
                    else
                    {
                        sql = "Insert into Armazenagem(IdUnidadeDeArmazenagem,DataDeEntrada,Quantidade) values(" + dt.Rows[i]["IdUnidadeDeArmazenagem"].ToString() + ",'" + DateTime.Parse(dt.Rows[i]["DataDeEntrada"].ToString()).ToString("yyyy-MM-dd") + "', " + dt.Rows[i]["Entrada"].ToString() + ") ; Update UnidadeDeArmazenagemLote set ArmazenagemEntrada=getdate() where IdUnidadeDeArmazenagemLote=" + dt.Rows[i]["IDUnidadeDeArmazenagemLote"].ToString();
                    }
                    Sistran.Library.GetDataTables.RetornarDataTableWin(sql + "; select 1", cnx);

                }


                sql = "SELECT DISTINCT  UAL.IDUnidadeDeArmazenagem, IDUnidadeDeArmazenagemLote, ";
                sql += " (select top 1 EstoqueMov.DataHora from EstoqueMov where EstoqueMov.IDUnidadeDeArmazenagemLote = UAL.IDUnidadeDeArmazenagemLote and IDEstoqueOperacao = 2 order by EstoqueMov.DataHora desc) data";
                sql += "  From ProdutoCliente PC with(Nolock)";
                sql += " Inner Join Estoque ES with(NoLock) on ES.IDProdutoCliente = PC.IdProdutoCliente";
                sql += " Inner Join Lote LT with(NoLock) on LT.IDEstoque = ES.IDEstoque and LT.IDProdutoCliente = PC.IDProdutoCliente";
                sql += " Inner Join UnidadeDeArmazenagemLote UAL with(NoLock) on UAL.IDLote = LT.IdLote";
                sql += " WHERE";
                sql += " UAL.Entrada IS NOT NULL AND UAL.ArmazenagemSaida IS NULL";
                sql += " AND PC.IDCliente = 6922";
                sql += " AND UAL.Saldo = 0";

                dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "Select * from Armazenagem where IdUnidadeDeArmazenagem=" + dt.Rows[i]["IDUnidadeDeArmazenagem"].ToString();
                    DataTable dti = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                    if(dti.Rows.Count>0)
                    {
                        sql = "Update Armazenagem set DataDeSaida='"+ DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' where IDUnidadeDeArmazenagem =" + dt.Rows[i]["IDUnidadeDeArmazenagem"].ToString() + " ; Update UnidadeDeArmazenagemLote set ArmazenagemSaida=getdate() where IdUnidadeDeArmazenagemLote=" + dt.Rows[i]["IDUnidadeDeArmazenagemLote"].ToString();
                        Sistran.Library.GetDataTables.RetornarDataTableWin(sql + "; Select 1", cnx);
                    }

                }
            }
            catch (Exception  vv)
            {

            }
        }


        #region Tranferido para outro robo

        //private void AcertarNotasNoStistranetDoComprovei()
        //{
        //    return;
        //    string docAtual = "";
        //    try
        //    {
        //       // string sql = "SELECT TOP 1000 COMP.*, D.IDFILIALATUAL  FROM RETORNOCOMPROVEI COMP WITH (NOLOCK) INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = COMP.IDDOCUMENTO where IdRetornoComprovei=5967386";
        //        string sql = "SELECT TOP 1000 COMP.*, D.IDFILIALATUAL  FROM RETORNOCOMPROVEI COMP WITH (NOLOCK) INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = COMP.IDDOCUMENTO WHERE PROCESSADO IS NULL /*AND D.IDDOCUMENTO=13048980 */ and D.IdDocumento not in (Select IdDocumento from Bloquear)  ORDER BY comp.IdRetornoComprovei desc ";
        //        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {

        //            string[] oco = dt.Rows[i]["OCORRENCIA"].ToString().Split('-');
        //            oco[0] = (oco[0].Trim().Length > 10 ? oco[0].Trim().Substring(0, 9) : oco[0].Trim());
        //            oco[1] = oco[1].Trim();


        //            Label2.Text = DateTime.Now + "-" + "AcertarNotasNoStistranetDoComprovei. IdDocumento: " + dt.Rows[i]["IDDOCUMENTO"].ToString() + "| - " + i + 1 + " De " + dt.Rows.Count;
        //            Application.DoEvents();
        //            docAtual = dt.Rows[i]["IDDOCUMENTO"].ToString();


        //            string sqlaux = "SELECT * FROM OCORRENCIA WHERE IDOCORRENCIASERIE=3 AND CODIGO='" + oco[0] + "' ";
        //            DataTable dtOco = Sistran.Library.GetDataTables.RetornarDataTableWin(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //            int idOcorrencia = 0;
        //            string finalzadora = "SIM";

        //            // se nao existir a ocorrencia insere
        //            if (dtOco.Rows.Count == 0)
        //            {
        //                GravarLog("Gravando Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

        //                idOcorrencia = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("OCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
        //                sqlaux = "insert into ocorrencia (IDOcorrencia, IDEmpresa, IDOcorrenciaAcao, Codigo, Nome, Responsabilidade, NomeReduzido, PagaEntrega, Finalizador, Sistema,  Ativo, RestringirCarregamento, AbrirFecharOcorrencia, ApareceSiteCliente, IdOcorrenciaSerie)";
        //                sqlaux += "VALUES (" + idOcorrencia + ", NULL, 5, '" + oco[0] + "', '" + (oco[1].Trim().ToUpper().Length >= 60 ? oco[1].Trim().ToUpper().Substring(0, 59) : oco[1].Trim().ToUpper()) + "', 'CLIENTE', '" + (oco[1].Trim().ToUpper().Length >= 30 ? oco[1].Trim().ToUpper().Substring(0, 29) : oco[1].Trim().ToUpper()) + "', 'NAO', 'NAO', NULL,  'NAO', 'NAO', 'AMBOS', NULL, 3)";
        //                Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //            }
        //            else
        //            {
        //                idOcorrencia = int.Parse(dtOco.Rows[0]["IDOCORRENCIA"].ToString());
        //                finalzadora = dtOco.Rows[0]["FINALIZADOR"].ToString();
        //            }


        //            //Verifico se ja tem a ocorrencia do comprovei
        //            string strsql = "";
        //            sql = "SELECT * FROM DOCUMENTOOCORRENCIA with (nolock) WHERE IDOCORRENCIACOMPROVEI= " + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " and IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString();
        //            DataTable ret = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //            if (ret.Rows.Count == 0)
        //            {
        //                //insere a ocorrencia
        //                AcertarDadosDTRomaneio(int.Parse(dt.Rows[i]["IDDOCUMENTO"].ToString()));

        //                //verifico se ja tem alguma ocorrencia feita pelo usuario do sistema
        //                sql = "SELECT top 1 * FROM DOCUMENTOOCORRENCIA WITH (NOLOCK)  WHERE IDDOCUMENTO =" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " AND IDOCORRENCIA= (SELECT top 1 IDOCORRENCIA FROM OCORRENCIA WHERE CODIGO='" + oco[0] + "' and IDOCORRENCIASERIE=3) AND IDUSUARIO IS NOT NULL order by IdDocumentoOcorrencia Desc";
        //                DataTable aux = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                DataTable existeFoto = new DataTable();
        //                if (aux.Rows.Count > 0)
        //                {
        //                    existeFoto = Sistran.Library.GetDataTables.RetornarDataTableWin("select top 1 * from DocumentoOcorrenciaArquivo where IddocumentoOcorrencia= " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                    if (aux.Rows.Count > 0)
        //                    {

        //                        DateTime dataDaOcorrencia = DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString());
        //                        DateTime dataOcorrenciaJaExistente = DateTime.Parse(aux.Rows[0]["DataOcorrencia"].ToString());

        //                        if (dataDaOcorrencia.ToString("dd/MM/yyyy HH:mm") == dataOcorrenciaJaExistente.ToString("dd/MM/yyyy HH:mm"))
        //                        {

        //                            if (existeFoto.Rows.Count > 0)
        //                                strsql = "UPDATE DOCUMENTOOCORRENCIAARQUIVO SET ARQUIVO=(select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ") WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
        //                            else
        //                            {

        //                                if (dt.Rows[i]["foto"].ToString() != "")
        //                                {
        //                                    string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                                    strsql = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
        //                                }
        //                            }

        //                            strsql += "UPDATE DOCUMENTOOCORRENCIA SET IDOCORRENCIACOMPROVEI=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
        //                            Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                            Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                            continue;
        //                        }


        //                        if (dataDaOcorrencia.ToString("dd/MM/yyyy") == dataOcorrenciaJaExistente.ToString("dd/MM/yyyy"))
        //                        {

        //                            if (existeFoto.Rows.Count > 0)
        //                                strsql = "UPDATE DOCUMENTOOCORRENCIAARQUIVO SET ARQUIVO=(select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ") WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
        //                            else
        //                            {
        //                                if (dt.Rows[i]["foto"].ToString() != "")
        //                                {
        //                                    string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                                    strsql = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
        //                                }
        //                            }

        //                            strsql += "UPDATE DOCUMENTOOCORRENCIA SET DataOcorrencia='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' ,IDOCORRENCIACOMPROVEI=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";

        //                            Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                            Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


        //                            continue;
        //                        }
        //                    }
        //                }

        //                int IdDocOco = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));

        //                strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
        //                strsql += " IDDocumentoOcorrencia, ";
        //                strsql += " IdRomaneio, ";
        //                strsql += " IDDocumento,";
        //                strsql += " IDFilial,";
        //                strsql += " IDOcorrencia,";
        //                strsql += " DataOcorrencia,";
        //                strsql += " Descricao,";
        //                strsql += " Sistema,";
        //                strsql += "IdOcorrenciaComprovei";
        //                strsql += " ) VALUES (";
        //                strsql += IdDocOco + " ,";
        //                strsql += "ISNULL((SELECT TOP 1 ISNULL(RD.IDROMANEIO,null) FROM ROMANEIODOCUMENTO RD WITH (NOLOCK)  INNER JOIN ROMANEIO R ON R.IDROMANEIO = RD.IDROMANEIO WHERE RD.IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " AND  R.TIPO IN ('ENTREGA', 'COLETA') ORDER BY 1 DESC),null)" + " ,";
        //                strsql += dt.Rows[i]["IDDOCUMENTO"].ToString() + " ,";
        //                strsql += Convert.ToInt32(dt.Rows[i]["IDFILIALATUAL"].ToString()) + " ,";

        //                ////se a data de conclusao for null coloca a ocorrencia se nao apenas uma observação que se caracteriza pelo null no idocorrencia

        //                //if (finalzadora == "SIM")
        //                strsql += int.Parse(idOcorrencia.ToString()) + " ,";
        //                //else
        //                //    strsql += " null ,";


        //                strsql += "'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' ,";
        //                strsql += " '" + dt.Rows[i]["Ocorrencia"].ToString().Trim() + " - Comprovei',";
        //                strsql += "'SIM',";
        //                strsql += dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " );   ";

        //                string SetDocFilial = "";

        //              if (finalzadora == "SIM")
        //                {
        //                    strsql += "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + ", DATADECONCLUSAO= '" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "'  WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
        //                    strsql += " UPDATE DOCUMENTOFILIAL SET  data='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "' ,SITUACAO='PROCESSO FINALIZADO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
        //                    //strsql += " UPDATE DOCUMENTO SET DATADECONCLUSAO= convert(datetime,'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "', 103)" + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "; "; 
        //                    SetDocFilial = "PROCESSO FINALIZADO";
        //                }
        //                else
        //                {
        //                    string x = "SELECT COUNT(*) FROM DOCUMENTOFILIAL WHERE SITUACAO='PROCESSO FINALIZADO' AND IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString();
        //                    DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWin(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                    if (dtx.Rows[0][0].ToString() == "0")
        //                    {
        //                        if (dtOco.Rows[0]["RestringirCarregamento"].ToString() == "" || dtOco.Rows[0]["RestringirCarregamento"].ToString() == "NAO")
        //                        {
        //                            strsql += " UPDATE DOCUMENTOFILIAL SET data='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "' , SITUACAO='AGUARDANDO EMBARQUE' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
        //                            SetDocFilial = "AGUARDANDO EMBARQUE";
        //                        }
        //                        else
        //                        {
        //                            strsql += " UPDATE DOCUMENTOFILIAL SET DATA='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "' , SITUACAO='AGUARDANDO SOLUCAO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
        //                            SetDocFilial = "AGUARDANDO SOLUCAO";

        //                        }

        //                        if (oco[0] != "998")
        //                            strsql += " UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
        //                    }
        //                }

        //                if (dt.Rows[i]["foto"].ToString() != "")
        //                {
        //                    string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                    strsql += "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + IdDocOco.ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
        //                }

        //                Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate(), OcorrenciaSetada='"+ SetDocFilial + "' where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


        //                //SE INSERIR A DATA DE CONCLUSAO CALCULA O PRAZO UTILIZADO
        //                if (finalzadora == "SIM")
        //                    Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(" EXEC SP_PRAZO_UTILIZADO_ID " + dt.Rows[i]["IDDOCUMENTO"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //            }
        //            else
        //            {
        //                strsql = "";

        //                if (strsql.Length > 10)
        //                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "AcertarNotasNoStistranetDoComprovei ", "Documento: " + docAtual + ". Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Erro no Timer");
        //        reiniciarTimers();
        //    }
        //}

        #endregion
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
                        string sql = "SELECT IDDOCUMENTO FROM DOCUMENTO with (nolock) WHERE /*TIPODEDOCUMENTO IN('NOTA FISCAL', 'ORDEM DE SERVICO', 'LOGISTICA') AND TIPODESERVICO in ('TRANSPORTE','ENTREGA') AND*/ (DOCUMENTODOCLIENTE4='" + sKey + "'  or ChaveNFReferencia='" + sKey + "')";
                        DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        

                        if (dtNota.Rows.Count == 0)
                        {
                            sql = "select IDDOCUMENTO from DocumentoEletronico where IdNota = '" + sKey + "'";
                            dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        }

                        if (dtNota.Rows.Count == 0)
                        {
                            sql = "SELECT IDDOCUMENTO,DocumentodoCliente4 FROM DOCUMENTO with (nolock) WHERE  substring(DOCUMENTODOCLIENTE4, 1,34)='" + sKey.Replace("0000000000", "") + "'";
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
                    string x = RetornarIdTabelaNovo("RASTREAMENTO").ToString(); //Sistran.Library.GetDataTables.RetornarIdTabela("RASTREAMENTO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
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
                        //base arquivos
                        SqlConnection vv = new SqlConnection("Data Source=192.168.10.10;Initial Catalog=VEX20200631;User ID=sa;Password=WERasd27;");
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

                        //base de arquivos
                        SqlConnection vv = new SqlConnection("Data Source=192.168.10.10;Initial Catalog=VEX20200631;User ID=sa;Password=WERasd27;");
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

        //private void CdEanReposicaoRoge()
        //{
        //    Label2.Text = "CdEanReposicaoRoge Inicio: " + DateTime.Now.ToString();

        //    XmlDocument xdoc = new XmlDocument();
        //    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "EAN ", "Em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");

        //    try
        //    {
        //        try
        //        {
        //            xdoc.Load("http://wslogos01.roge.com.br:8086/csp/roge/WSRoge.WSLogos.cls?soap_method=RetornarDadosCDEAN");
        //        }
        //        catch (Exception)
        //        {
        //            xdoc.Load("http://wslogos02.roge.com.br:8087/csp/roge/WSRoge.WSLogos.cls?soap_method=RetornarDadosCDEAN");

        //        }

        //        XmlNodeList xnList = xdoc.GetElementsByTagName("SQL");

        //        if (xnList.Count == 0)
        //        {
        //            new cEmail().enviarEmail("Roge cdEan", "Nao Encontrou itens para atualizar em: " + DateTime.Now.ToString() + " ITENS PROCESSADOS: " + xnList.Count.ToString(), "moises@sistecno.com.br", "AlertaDoSite");

        //            throw new Exception("Nao Encontrou itens para atualizar" + DateTime.Now.ToString());
        //        }

        //        string sql = "";
        //        int erros = 0;
        //        //string cbErros = "";

        //        if (xnList.Count > 0)
        //        {
        //            Sistran.Library.GetDataTables.ExecutarComandoSql("update ReposicaoRogeEan set Status='D' ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //        }


        //        for (int i = 0; i < xnList.Count; i++)
        //        {
        //            try
        //            {
        //                string cb = xnList[i]["eancodigo"] == null ? "" : xnList[i]["eancodigo"].InnerText;
        //                string desc = xnList[i]["nome"] == null ? "" : xnList[i]["nome"].InnerText;

        //                sql += "  IF EXISTS (SELECT CodigoDeBarras FROM ReposicaoRogeEan WHERE CodigoDeBarras='" + cb + "') ";
        //                sql += " UPDATE ReposicaoRogeEan SET STATUS='U', DataInclusao=GETDATE(), Descricao='" + desc.Trim().ToUpper() + "' WHERE CodigoDeBarras='" + cb.Trim() + "'";
        //                sql += "  ELSE ";
        //                sql += " INSERT INTO ReposicaoRogeEan (CodigoDeBarras, Status, Descricao) VALUES ('" + cb.Trim() + "', 'I', '" + desc.Trim().ToUpper() + "') ";
        //                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                sql = "";
        //            }
        //            catch (Exception ex1)
        //            {
        //                erros++;
        //                //MessageBox.Show(ex1.Message);
        //            }
        //        }
        //        Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "Finalizou em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");
        //        Label2.Text = "CdEanReposicaoRoge FIM: " + DateTime.Now.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "erro em: " + DateTime.Now.ToString() + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "CDEAN");
        //    }
        //}

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

            for (int i = 0; i < 500; i++)
            {
                //ConsultarProtocolo(false);

                //Thread.Sleep(60000);

                //if (i == 50)
                //    i = 0;

                EnviarOcorrenciaFrioVIX();

            }

            Application.Exit();
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
                for (int i = 0; i < 100; i++)
                {
                    //SolicitarProtocolo();
                    //ConsultarProtocoloNovaTentativa();
                    //AcertarNotasNoStistranetDoComprovei();
                    //ProcessarComprovei();
                }

                //ConsultarProtocolo();




                //Application.Exit();
                MessageBox.Show("Test");
            }
            catch (Exception ex)
            {
                GravarLog("Erro: " + ex.Message, "btnTWX_Click");
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "btnTWX_Click ", "erro em: " + DateTime.Now.ToString() + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "CDEAN");

            }
        }

        public void SolicitarProtocoloSoap()
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




                tbUrl = "https://soap.comprovei.com.br/exportQueue/v2/index.php?wsdl";


                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);


                string postData = "<Envelope xmlns:soap=\"http://schemas.xmlsoap.org/wsdl/soap/\">";
                postData += "<Header>";
                postData += "<Credenciais xmlns = \"WebServiceComprovei\">";
                postData += "<Usuario>logos</Usuario>";
                postData += "<Senha>admin</Senha>";
                postData += "</Credenciais>";
                postData += "</Header>";
                postData += "<Body>";
                postData += "<urn:downloadDocumentsHistory soapenv:encodingStyle = \"http://schemas.xmlsoap.org/soap/encoding/\">";
                postData += "<qtdDocumentos xsi:type=\"xsd:integer\">2</qtdDocumentos>";
                postData += "</urn:downloadDocumentsHistory>";

                postData += "</Body>";
                postData += "</Envelope>";



                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=ISO-8859-1";
                //request.Headers.Add("SOAPAction", "urn:WebServicePOD#downloadDocumentsHistory");
                //request.Headers.Add("Authorization", "Basic " + auth);
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

                tbUrl = "https://soap.comprovei.com.br/exportQueue/index.php?wsdl";

                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);


                string postData = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:WebServiceComprovei\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                "<urn:downloadDocumentsHistory soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                //"<qtdDocumentos xsi:type=\"xsd:string\">" + ConfigurationSettings.AppSettings["QtdDocumentosPorChamada"] + "</qtdDocumentos>" +
                "<qtdDocumentos xsi:type=\"xsd:string\">999</qtdDocumentos>" +
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
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                //Sistran.Library.GetDataTables.RetornarDataTableWS("delete from ProtocoloComprovei where XmlProtocolo like '%Aguarde a conclusão do mesmo para abrir um novo chamado%'; select 1", cnx);


                string s = "Select top 1 * from ProtocoloComprovei with (nolock)  where ProcessadoSistran is null and DataConclusao is null and QTDProcessamento  is not null /*and DataSolicitacao<= DATEADD(MI, -3, GETDATE())*/  order by isnull(QTDProcessamento,0), 5 ";
                //string s = "Select top 1 * from ProtocoloComprovei where IdProtocoloComprovei = 38961";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(s, cnx);

                if (dt.Rows.Count == 0)
                {
                   // SolicitarProtocolo();
                    dt = Sistran.Library.GetDataTables.RetornarDataTableWS(s, cnx);

                    if (dt.Rows.Count == 0)
                        return;
                }


                Label2.Text = "Consultando Protocolo Inicio: " + DateTime.Now.ToString();
                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                //string varName = "";
                //string varType = "";
                //string xmlPath = "";

                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));
                //tbUrl = "http://34.207.154.27/exportQueue/index.php?wsdl";
                tbUrl = "https://soap.comprovei.com.br/exportQueue/index.php?wsdl";

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
            }

            catch (Exception ex)
            {
                GravarLog(sss, "Solicitar Protocolo" + ex.Message);
                reiniciarTimers();
            }
        }


        public void ConsultarProtocoloNaMao(bool solicitar, string prot)
        {
            string ultimoProtocolo = "";
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                ultimoProtocolo = prot;
                Label2.Text = "Consultando Protocolo Inicio: " + DateTime.Now.ToString();
                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                string varName = "";
                string varType = "";
                string xmlPath = "";
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));

                //tbUrl = "http://34.207.154.27/exportQueue/index.php?wsdl";

                tbUrl = "https://soap.comprovei.com.br/exportQueue/index.php?wsdl";


                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);

                string postData = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:WebServiceComprovei\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                "<urn:getExportProtocolStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                "<protocolo xsi:type=\"xsd:string\">" + ultimoProtocolo + "</protocolo>" +
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
                bool temOcorrencia = false;
                string sql = "";
                //string aux = "";
                XmlDocument doc1 = new XmlDocument();

                if (xmlAwnser.GetElementsByTagName("dataConclusao").Count > 0)
                {
                    if (xmlAwnser.GetElementsByTagName("dataConclusao").Item(0).InnerText != "")
                    {
                        sql = "Update ProtocoloComprovei set XmlProtocolo=@Arquivo, ";
                        sql += "DataConclusao=getdate() ";
                        sql += "where Protocolo='" + prot + "'";


                        if (!xmlAwnser.InnerText.Contains("Sem ocorrências para retornar") || !xmlAwnser.InnerText.Contains("Protocolo encerrado por inati"))
                        {
                            temOcorrencia = true;
                            if (xmlAwnser.GetElementsByTagName("url").ToString() != "" && xmlAwnser.GetElementsByTagName("url") != null && xmlAwnser.GetElementsByTagName("url").Count > 0 && xmlAwnser.GetElementsByTagName("url")[0].InnerText !="")
                            {
                                doc1.Load(xmlAwnser.GetElementsByTagName("url").Item(0).InnerText.Replace("https://s3.amazonaws.com/", "http://s3.amazonaws.com/"));
                                sql = "Update ProtocoloComprovei set XmlProtocolo=@Arquivo/*, XmlDocumento = @ArqDoc */";
                                sql += "where Protocolo='" + prot + "'";
                            }
                        }

                        SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        SqlCommand cmm = new SqlCommand();

                        cmm.CommandText = sql;
                        cmm.CommandType = CommandType.Text;
                        cmm.Connection = cnn;

                        SqlParameter par = new SqlParameter("@arquivo", xmlAwnser.InnerXml);
                        cmm.Parameters.Add(par);
                        //////
                        if (temOcorrencia)
                        {
                            par = new SqlParameter("@ArqDoc", doc1.InnerXml);
                            cmm.Parameters.Add(par);
                        }

                        try
                        {
                            cnn.Open();
                            cmm.ExecuteNonQuery();
                            cnn.Close();

                            if (temOcorrencia)
                            {
                                string ss = "Select IdOcorrenciaComprovei from RetornoComprovei with(nolock) where Protocolo='" + prot + "'";
                                DataTable dtRetComp = Sistran.Library.GetDataTables.RetornarDataTableWS(ss, cnx);
                                ProcessarXMLProtocolo(doc1, prot, dtRetComp);
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
            }

            catch (Exception ex)
            {
                Sistran.Library.GetDataTables.RetornarDataTableWS("Update ProtocoloComprovei set erro='S' where Protocolo='" + ultimoProtocolo + "'; select 1", cnx);
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

                string s = "Select top 1 * from ProtocoloComprovei where ProcessadoSistran is null and DataConclusao is null  and DataSolicitacao >='2021-08-11 08:00:00' order by 9,1 asc";
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
                //tbUrl = "http://34.207.154.27/exportQueue/index.php?wsdl";

                tbUrl = "https://soap.comprovei.com.br/exportQueue/index.php?wsdl";



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
                        //////
                        if (temOcorrencia)
                        {
                            par = new SqlParameter("@ArqDoc", doc1.InnerXml);
                            cmm.Parameters.Add(par);
                        }


                        try
                        {
                            cnn.Open();
                            cmm.ExecuteNonQuery();
                            cnn.Close();

                            if (temOcorrencia)
                            {
                                string ss = "Select IdOcorrenciaComprovei from RetornoComprovei with (nolock) where Protocolo='" + dt.Rows[0]["Protocolo"].ToString() + "'";
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
                //if (ex.Message.ToString() != "Impossível conectar-se ao servidor remoto")
                //{
                Sistran.Library.GetDataTables.RetornarDataTableWS("Update ProtocoloComprovei set erro='S' where Protocolo='" + ultimoProtocolo + "'; select 1", cnx);
                //GravarLog(sss, "Solicitar Protocolo" + ex.Message);
                reiniciarTimers();
                //}


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
                    byte[] assinaturaArray = null;
                    qtdx++;


                    Label2.Text = i.ToString() + " " + docs.Count + " " + Protocolo;
                    Application.DoEvents();

                    //PEGA A NOTA
                    string sql = "SELECT IDDOCUMENTO FROM DOCUMENTO with (nolock) WHERE NUMERO >1 AND DATADOMOVIMENTO >getdate()-120 and TIPODEDOCUMENTO IN('NOTA FISCAL', 'ORDEM DE SERVICO', 'LOGISTICA') AND (DOCUMENTODOCLIENTE4='" + Chave + "')";// or ChaveNFReferencia ='" + Chave + "')";
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

                        string Lat = "";
                        string Lon = "";
                        if (ocos[iOco]["Posicao"] != null)
                        {
                            Lat = ocos[iOco]["Posicao"]["Latitude"].InnerText;
                            Lon = ocos[iOco]["Posicao"]["Longitude"].InnerText;
                        }


                        Ocorrencia = ocos[iOco]["Motivo"].InnerText;
                        DataOco = ocos[iOco]["Data"].InnerText;

                        if (DataOco == "0000-00-00 00:00:00" || DataOco.Trim() == "")
                        {
                            DataOco = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        string anotacao = ocos[iOco]["Anotacao"].InnerText;



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
                               // Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "ProcessarXmlProtocolo ", xx.Message + xx.InnerException + " Protocolo: " + Protocolo + "<BR>" + carta, "mail.grupologos.com.br", "logos0902", "ProcessarXMLDOcumento");
                                sql += "Insert Into RetornoComproveiImagem (Link,IdOcorrenciaComprovei) values ('" + CaminhoImagem + "','" + IdOcorrenciaComprovei + "')";
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, "Data Source=192.168.10.10;Initial Catalog=VEX20200631;User ID=sa;Password=WERasd27;");                               
                            }
                        }
                        
                        string k = "";
                        if (fotoArray != null)
                            k = ", @foto ";


                        var assinatura = docs.Item(i).SelectNodes("Ocorrencias/Ocorrencia/Assinatura");
                        if (assinatura != null && assinatura.Count > 0 && assinatura.Item(0).InnerText != "")
                        {
                            string CaminhoAssinatura = "";
                            CaminhoAssinatura = assinatura[0].SelectSingleNode("Dado").InnerText;

                            try
                            {
                                var request = WebRequest.Create(CaminhoAssinatura);

                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                { 
                                    BinaryReader bin = new BinaryReader(response.GetResponseStream());                                  
                                    assinaturaArray = bin.ReadBytes(1000000);
                                }
                            }                            
                            catch (Exception xx)
                            {
                            }
                        }


                        string kAss = "";

                        string Nrec = "";
                        string Docrec = "";
                        if (assinaturaArray != null)
                        {
                            kAss = ", @Assinatura ";


                            if (docs.Item(i).SelectNodes("Ocorrencias/Ocorrencia/Assinatura/Nome").Item(0) != null)
                            {

                                var dd = docs.Item(i).SelectNodes("Ocorrencias/Ocorrencia/Assinatura/Nome").Item(0).InnerText.Split(',');

                                if (dd.Length == 1)
                                    Nrec = dd[0].Trim();

                                if (dd.Length == 2)
                                {
                                    Nrec = dd[0].Trim();
                                    Docrec = dd[1].Trim();
                                }
                            }
                        }

                        sql = "Insert into RetornoComprovei (Anotacao, NomeRecebedor,DocRecebedor, IdDocumento, Protocolo, Chave, DataDaOcorrencia, Ocorrencia, IdOcorrenciaComprovei" + k.Replace("@", "") + ", lat, lon "+ kAss.Replace("@", "") + ")";
                        sql += "values ('"+anotacao+"', '"+ Nrec.Replace("'", "´") + "','"+ Docrec.Replace("'", "´") + "'," + IdDocumento + ", '" + Protocolo + "' ,'" + Chave + "', '" + DateTime.Parse(DataOco).ToString("yyyy-MM-dd HH:mm:ss") + "', left('" + Ocorrencia + "',50), '" + IdOcorrenciaComprovei + "'" + k + ", '"+Lat+"', '"+Lon+"' "+kAss+")";


                        //base arquivo
                        SqlConnection cnn = new SqlConnection("Data Source=192.168.10.10;Initial Catalog=VEX20200631;User ID=sa;Password=WERasd27;");
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

                        if (kAss != "")
                        {
                            par = new SqlParameter("@Assinatura", assinaturaArray);
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
                //Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "ProcessarXmlProtocolo ", ex.Message + ex.InnerException + " Protocolo: " + Protocolo, "mail.grupologos.com.br", "logos0902", "ProcessarXMLDOcumento");
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

        //private void EnviarComprovei(bool dt, bool BaseTeste)
        //{

        //    Label2.Text = "EnviarComprovei: " + DateTime.Now.ToString();
        //    //Sistran.Library.GetDataTables.ExecutarComandoSql("UPDATE DOCUMENTO SET NOMEDOARQUIVO1=NULL, DOCUMENTODOCLIENTE4=NULL WHERE  LTRIM(RTRIM(DOCUMENTODOCLIENTE4))='' AND  convert(datetime, DATADEEMISSAO, 103)  >=  convert(datetime,'01/12/2015',103) AND TIPODEDOCUMENTO='NOTA FISCAL'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //    string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
        //    string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

        //    string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin"));

        //    if (BaseTeste)
        //        auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));

        //    DataTable dtGeral = null;

        //    if (BaseTeste)
        //    {
        //        dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI_TESTE " + txtIdDt.Text, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
        //    }
        //    else
        //    {
        //        if (dt)
        //            dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI_DT ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
        //        else
        //            dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
        //    }


        //    DataView view = new DataView(dtGeral);
        //    DataTable dtds = view.ToTable(true, "IDDOCUMENTO");
        //    const string quote = "\"";
        //    string xml = "";
        //    string ch = "";

        //    // Label2.Text = "Enviando Comprovei";

        //    for (int i = 0; i < dtds.Rows.Count; i++)
        //    {
        //        xml = "";

        //        Label2.Text = DateTime.Now + "- Enviado " + (i + 1).ToString() + " de " + dtds.Rows.Count + " registros para o comprovei";
        //        Application.DoEvents();

        //        if (i == 1000)
        //            return;

        //        try
        //        {
        //            xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
        //            xml += "<Documentos>";
        //            DataRow[] dr = dtGeral.Select("IDDOCUMENTO=" + dtds.Rows[i]["IDDOCUMENTO"].ToString(), "");

        //            ch = dr[0]["CHAVE"].ToString();

        //            // Label2.Text = "Enviando Comprovei: " + ch;

        //            bool gerouCahve = false;

        //            if (ch.Contains("SERNUMERONOT"))
        //            {
        //                ch = ch.Replace("SER", dr[0]["SERIE"].ToString().PadLeft(3, '0'));
        //                ch = ch.Replace("NUMERONOT", dr[0]["NUMERO"].ToString().PadLeft(9, '0'));

        //                gerouCahve = true;
        //            }



        //            xml += "<Documento>";

        //            if (dr[0]["TIPODEDOCUMENTO"].ToString().ToUpper() == "ORDEM DE SERVICO")
        //            {
        //                xml += "<Tipo>REQ</Tipo>";
        //                xml += "<TipoParada>C</TipoParada>";
        //                xml += "<Modelo>55</Modelo>";
        //            }
        //            else
        //            {
        //                xml += "<Tipo>NFE</Tipo>";
        //                xml += "<TipoParada>E</TipoParada>";
        //                xml += "<Modelo>55</Modelo>";
        //            }

        //            xml += "<Numero>" + dr[0]["NUMERO"].ToString() + "</Numero>";
        //            xml += "<Valor>" + dr[0]["VALORDANOTA"].ToString().Replace(",", ".") + "</Valor>";

        //            if (dr[0]["IDCLIENTE"].ToString() == "65286")
        //            {
        //                //se o cliente for fastshop procura a serie na chave da nota

        //                if (ch.Length == 44)
        //                {
        //                    xml += "<Serie>" + ch.Substring(22, 3) + "</Serie>";
        //                }
        //            }
        //            else
        //                xml += "<Serie>" + (dr[0]["SERIE"].ToString().Trim() == "NFE" ? "001" : dr[0]["SERIE"].ToString().Trim()) + "</Serie>";



        //            xml += "<Emissao>" + DateTime.Parse(dr[0]["DATADEEMISSAO"].ToString()).ToString("yyyyMMdd") + "</Emissao>";
        //            xml += "<Atualizacao>" + DateTime.Parse(dr[0]["ATUALIZACAO"].ToString()).ToString("yyyyMMdd") + "</Atualizacao>";
        //            xml += "<Chave>" + ch + "</Chave>";
        //            xml += "<cnpj>" + dr[0]["CNPJ"].ToString() + "</cnpj>";

        //            xml += "<cnpjEmissor>" + dr[0]["CNPJEMISSOR"].ToString() + "</cnpjEmissor>";
        //            xml += "<cnpjTransportador>" + dr[0]["CNPJTRANSPORTADOR"].ToString() + "</cnpjTransportador>";

        //            xml += "</Documento>";
        //            xml += "<Cliente>";
        //            xml += "<Codigo>" + dr[0]["CODIGOCLIENTE"].ToString() + "</Codigo>";
        //            xml += "<Contato>" + dr[0]["CONTATO"].ToString() + "</Contato>";

        //            //if (dr[0]["TELEFONE"].ToString() == "")
        //            //    xml += "<Telefone>11 9999-9999</Telefone>";
        //            //else
        //            xml += "<Telefone>" + dr[0]["TELEFONE"].ToString() + "</Telefone>";


        //            //if (dr[0]["EMAIL"].ToString() == "")
        //            //    xml += "<Email>moises@sistecno.com.br</Email>";
        //            //else
        //            xml += "<Email>" + dr[0]["EMAIL"].ToString() + "</Email>";


        //            xml += "<Razao>" + dr[0]["RAZAO"].ToString().Replace("&", "") + "</Razao>";
        //            xml += "<Endereco>" + dr[0]["ENDERECO"].ToString() + ", " + dr[0]["NUMERO_END"].ToString() + "</Endereco>";
        //            xml += "<Bairro>" + dr[0]["BAIRRO"].ToString() + "</Bairro>";
        //            xml += "<Cidade>" + dr[0]["CIDADE"].ToString() + "</Cidade>";
        //            xml += "<Estado>" + dr[0]["ESTADO"].ToString() + "</Estado>";
        //            xml += "<Pais>BRASIL</Pais>";
        //            xml += "<CEP>" + dr[0]["CEP"].ToString() + "</CEP>";


        //            xml += "<Regiao>" + dr[0]["REGIAO"].ToString() + "</Regiao>";
        //            xml += "<TipoCliente></TipoCliente>";
        //            xml += "<Mensagem></Mensagem>";
        //            xml += "</Cliente>";

        //            xml += "<SKUs>";
        //            bool jaInseriu = false;

        //            for (int ii = 0; ii < dr.Length; ii++)
        //            {
        //                if (dr[ii]["CODIGOPR"].ToString() != "")
        //                {
        //                    xml += "<SKU codigo=" + quote + dr[ii]["CODIGOPR"].ToString() + quote + ">";
        //                    xml += "<PesoBruto>" + dr[ii]["PESOBRUTO"].ToString() + "</PesoBruto>";
        //                    xml += "<PesoLiquido>" + dr[ii]["PESOLIQUIDO"].ToString() + "</PesoLiquido>";
        //                    xml += "<Volumes>" + dr[ii]["VOLUMES"].ToString() + "</Volumes>";
        //                    xml += "<Descricao>" + dr[ii]["DESCRICAO"].ToString().Replace("'", "").Replace("&", "e") + "</Descricao>";
        //                    xml += "<Qde>" + dr[ii]["QUANTIDADE"].ToString().Replace(",", ".") + "</Qde>";
        //                    xml += "<Uom>" + dr[ii]["UNIDADEDEMEDIDA"].ToString() + "</Uom>";
        //                    xml += "<Barcode>" + dr[ii]["BARCODE"].ToString() + "</Barcode>";
        //                    xml += "</SKU>";
        //                }
        //                else
        //                {
        //                    if (jaInseriu == false)
        //                    {
        //                        xml += "<SKU codigo=" + quote + dr[0]["NUMERO"].ToString() + quote + ">";
        //                        xml += "<PesoBruto>0</PesoBruto>";
        //                        xml += "<PesoLiquido>0</PesoLiquido>";
        //                        xml += "<Volumes>1</Volumes>";
        //                        xml += "<Descricao>DANFE " + dr[0]["NUMERO"].ToString() + "</Descricao>";
        //                        xml += "<Qde>1</Qde>";
        //                        xml += "<Uom>FL</Uom>";
        //                        xml += "<Barcode>" + ch + "</Barcode>";
        //                        xml += "</SKU>";
        //                        jaInseriu = true;
        //                    }
        //                }
        //            }
        //            xml += "</SKUs> ";

        //            xml += " </Documentos> ";

        //            string auxXML = xml;
        //            xml = Base64Encode(xml);

        //            if (ch.Contains("NFE"))
        //            {
        //                ch = ch.Replace("NFE", "000");
        //                gerouCahve = true;
        //            }

        //            //string NomeArquivo = ch + DateTime.Now.ToString("yyyyMMddhhmmssffftt") + ".xml";
        //            string NomeArquivo = dtds.Rows[i]["IDDOCUMENTO"].ToString() + ".xml";

        //            WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
        //            string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
        //                           "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
        //                           "<SOAP-ENV:Body>" +
        //                           "<tns:importDocsToPOD xmlns:tns=\"urn:WebServicePOD\">" +
        //                           "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
        //                           "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
        //                           "</tns:" + "importDocsToPOD" + ">" +
        //                           "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
        //            byte[] data = Encoding.ASCII.GetBytes(postData);
        //            request.Method = "POST";
        //            request.ContentType = "text/xml; charset=ISO-8859-1";
        //            request.Headers.Add("SOAPAction", "urn:WebServicePOD#getDocumentsFromPOD");
        //            request.Headers.Add("Authorization", "Basic " + auth);
        //            request.ContentLength = data.Length;


        //            using (var stream = request.GetRequestStream())
        //            {
        //                stream.Write(data, 0, data.Length);
        //            }


        //            Cursor.Current = Cursors.WaitCursor;
        //            WebResponse response = (HttpWebResponse)request.GetResponse();
        //            StreamReader sr = new StreamReader(response.GetResponseStream());

        //            XmlDocument xmlAwnser = new XmlDocument();
        //            xmlAwnser.LoadXml(sr.ReadToEnd());


        //            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
        //            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        //            xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

        //            Label2.Text = DateTime.Now + "-" + "Enviando Nota ao Comprovei. IdDocumento: " + sss;
        //            Application.DoEvents();

        //            string retorno = xmlAwnser["status"].InnerText;

        //            retorno = retorno.Replace("'", "");
        //            retorno = retorno.Replace("/", "");
        //            retorno = retorno.Replace("\\", "");
        //            retorno = retorno.Replace("??", "CA");

        //            string sql = "";
        //            if (BaseTeste == false)
        //            {
        //                if (retorno.ToUpper().Contains("WEVE GOT SOME PROBLEM"))
        //                {
        //                    sql = "UPDATE DOCUMENTO SET EnviadoComprovei=null  WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
        //                    sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), '" + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
        //                    //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";

        //                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei ", "Problema no retorno em: " + DateTime.Now.ToString() + "- IdDocumento:" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + "<BR> " + retorno, "mail.grupologos.com.br", "logos0902", "Envio Comprovei - Problema no retorno do comprovei");

        //                }
        //                else
        //                {

        //                    sql = "UPDATE DOCUMENTO SET EnviadoComprovei='ENVIADO COMPROVEI - " + DateTime.Now + "' " + (gerouCahve == true ? ", DocumentoDoCliente4='" + ch + "'" : "") + " WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
        //                    sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), 'Filial: " + dr[0]["REGIAO"].ToString() + "  " + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
        //                    //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";
        //                }

        //                //if (!retorno.Contains("existe."))
        //                //{
        //                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                GravarXML(auxXML, DateTime.Now);
        //                GravarLog(ch, retorno);
        //                //}
        //                // Label2.Text = "Terminou Envio Comprovei";
        //            }
        //            Label2.Text = "EnviarComprovei Termino: " + DateTime.Now.ToString();
        //        }
        //        catch (Exception ex)
        //        {
        //            reiniciarTimers();
        //            Label2.Text = "EnviarComprovei Erro: " + DateTime.Now.ToString();
        //            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei ", "Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
        //        }
        //    }
        //}

        //private void EnviarRota_Comprovei(string idDt, string IdFilial)
        //{
        //    try
        //    {


        //        Label2.Text = "Enviar Rotas: " + DateTime.Now.ToString();

        //        string x = "SELECT TOP 100 DT.IDDT FROM DT  ";
        //        x += " INNER JOIN DTROMANEIO DTR ON DTR.IDDT = DT.IDDT ";
        //        x += " INNER JOIN ROMANEIO ROM ON ROM.IDROMANEIO = DTR.IDROMANEIO ";
        //        //                x += " WHERE DT.ANDAMENTO IN ('DOCUMENTACAO LIBERADA' ,  'EM ENTREGA', 'CARREGAMENTO EFETUADO') ";
        //        x += " where ROM.IDFILIAL  NOT IN(48, 15,30,54,49,27, 52) ";
        //        //x+ = " AND ROTAENVIADACOMPROVEI IS NULL AND ROM.EMISSAO>=GETDATE()-15 ";


        //        x += " and  DT.IdFilial in(select idfilial from FILIALENVIAROTASCOMPROVEI)";

        //        if (idDt == "")
        //        {
        //            x += " AND (dt.EMISSAO >= GETDATE()-1 or rom.Liberacao >= GETDATE()-1) AND Ativo = 'SIM' AND ROTAENVIADACOMPROVEI IS NULL";
        //            x += " AND ROM.Tipo = 'ENTREGA'";
        //            x += " and  DT.ANDAMENTO IN ('EM ENTREGA') ";
        //            x += " AND DT.ROTAENVIADACOMPROVEI IS NULL ";



        //        }
        //        else
        //            x += " and dt.Iddt = " + idDt;



        //        DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

        //        Application.DoEvents();
        //        const string quote = "\"";


        //        string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
        //        string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

        //        //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
        //        string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

        //        for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
        //        {
        //            try
        //            {

        //                DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];


        //                DataView view = new DataView(dtGeral);
        //                DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
        //                string NomeArquivo = "";
        //                int NumeroParada = 1;

        //                string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

        //                string xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
        //                xml += "<Rotas>";
        //                // xml += "<Rota numero=" + quote + NomeRota + quote + ">";
        //                //  xml += "<Rota numero=" + quote + dtGeral.Rows[0]["NUMEROROTA"].ToString() + quote + " Regiao=" + quote + NomeRota + quote + ">";
        //                xml += "<Rota numero=" + quote + dtGeral.Rows[0]["NUMERO"].ToString() + quote + ">";


        //                for (int i = 0; i < dtds.Rows.Count; i++)
        //                {

        //                    DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString() + "'" +
        //                                                    " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString() + "'" +
        //                                                    " and IDENDERECOBAIRRO=" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString() +
        //                                                    " and IDENDERECOCIDADE=" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString() +
        //                                                    " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString() + "'", "");


        //                    if (i == 0)
        //                    {

        //                        //xml += "     <Numero>" + ret[i]["NUMEROROTA"].ToString() + "_Teste</Numero>";
        //                        xml += "<Data>" + DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd") + "</Data>";
        //                        xml += "<Regiao>" + NomeRota + "</Regiao>";

        //                        //xml += "<Transportadora>";
        //                        //xml += "<Codigo>0</Codigo>";
        //                        //xml += "<Razao>SEM TRANSPORTADORA</Razao>";
        //                        //xml += "</Transportadora>";

        //                        xml += "<Transportadora>";
        //                        xml += "<Codigo></Codigo>";
        //                        xml += "<Razao></Razao>";
        //                        xml += "</Transportadora>";

        //                        //xml += "     <Regiao>" + ret[i]["REGIAO"].ToString() + "</Regiao>";
        //                        xml += "<Motorista>";
        //                        xml += "<Usuario>" + ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "") + "</Usuario>";
        //                        xml += "<PlacaVeiculo>" + ret[i]["PLACA"].ToString().Replace("-", "") + "</PlacaVeiculo>";
        //                        xml += "</Motorista>";

        //                        xml += "<Paradas>";

        //                        NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";

        //                    }


        //                    for (int ii = 0; ii < ret.Length; ii++)
        //                    {
        //                        xml += "<Parada numero=" + quote + NumeroParada + quote + ">";
        //                        xml += "<Documento>";
        //                        xml += "<ChaveNota>" + ret[ii]["CHAVE"].ToString() + "</ChaveNota>";
        //                        xml += "</Documento>";
        //                        xml += "</Parada>";
        //                        NumeroParada++;
        //                    }
        //                }
        //                xml += "</Paradas>";
        //                xml += "</Rota>";
        //                xml += "</Rotas>";

        //                string auxXML = xml;
        //                xml = Base64Encode(xml);

        //                //  continue; // retinar na hora de enviar

        //                WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
        //                string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
        //                               "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
        //                               "<SOAP-ENV:Body>" +
        //                               "<tns:sendDocsKeysToPOD xmlns:tns=\"urn:WebServicePOD\">" +
        //                               "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
        //                               "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
        //                               "</tns:" + "sendDocsKeysToPOD" + ">" +
        //                               "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
        //                byte[] data = Encoding.ASCII.GetBytes(postData);
        //                request.Method = "POST";
        //                request.ContentType = "text/xml; charset=ISO-8859-1";
        //                request.Headers.Add("SOAPAction", "urn:WebServicePOD#sendDocsKeysToPOD");
        //                request.Headers.Add("Authorization", "Basic " + auth);
        //                request.ContentLength = data.Length;


        //                using (var stream = request.GetRequestStream())
        //                {
        //                    stream.Write(data, 0, data.Length);
        //                }


        //                Cursor.Current = Cursors.WaitCursor;
        //                WebResponse response = (HttpWebResponse)request.GetResponse();
        //                StreamReader sr = new StreamReader(response.GetResponseStream());

        //                XmlDocument xmlAwnser = new XmlDocument();
        //                xmlAwnser.LoadXml(sr.ReadToEnd());


        //                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
        //                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        //                xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);
        //                string retorno = xmlAwnser["status"].InnerText;

        //                string sql = "Update Dt Set ROTAENVIADACOMPROVEI='ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno + "' where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();

        //                sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
        //                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //            }
        //            catch (Exception ex)
        //            {
        //                Label2.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
        //                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rOTAS", "Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
        //                continue;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        reiniciarTimers();
        //    }
        //}

        //private void EnviarComprovei_HOMEREFILL(string baseA)
        //{

        //    Label2.Text = "EnviarComprovei: " + DateTime.Now.ToString();
        //    //Sistran.Library.GetDataTables.ExecutarComandoSql("UPDATE DOCUMENTO SET NOMEDOARQUIVO1=NULL, DOCUMENTODOCLIENTE4=NULL WHERE  LTRIM(RTRIM(DOCUMENTODOCLIENTE4))='' AND  convert(datetime, DATADEEMISSAO, 103)  >=  convert(datetime,'01/12/2015',103) AND TIPODEDOCUMENTO='NOTA FISCAL'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //    string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
        //    string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);
        //    string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin"));
        //    DataTable dtGeral = new DataTable();

        //    //if (baseA.Length == 0)
        //    //	dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI_HOMEREFILL ", "Data Source=192.168.10.20;Initial Catalog=homerefill;User ID=sa;Password=@oncetsis05083#;").Tables[0];
        //    //else
        //    dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI_HOMEREFILL ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

        //    DataView view = new DataView(dtGeral);
        //    DataTable dtds = view.ToTable(true, "IDDOCUMENTO", "NUMERO", "SERIE", "ANOMES");
        //    const string quote = "\"";
        //    string xml = "";
        //    string ch = "";

        //    // Label2.Text = "Enviando Comprovei";

        //    for (int i = 0; i < dtds.Rows.Count; i++)
        //    {
        //        xml = "";

        //        Label2.Text = DateTime.Now + "- Enviado " + i + 1 + " de " + dtds.Rows.Count + " registros para o comprovei";
        //        Application.DoEvents();

        //        if (i == 1000)
        //            return;

        //        try
        //        {
        //            xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
        //            xml += "<Documentos>";
        //            DataRow[] dr = dtGeral.Select("IDDOCUMENTO=" + dtds.Rows[i]["IDDOCUMENTO"].ToString(), "");

        //            ch = dr[0]["CHAVE"].ToString();

        //            // Label2.Text = "Enviando Comprovei: " + ch;

        //            bool gerouCahve = false;

        //            if (ch.Contains("SERNUMERONOT"))
        //            {
        //                ch = ch.Replace("SER", dr[0]["SERIE"].ToString().PadLeft(3, '0'));
        //                ch = ch.Replace("NUMERONOT", dr[0]["NUMERO"].ToString().PadLeft(9, '0'));

        //                gerouCahve = true;
        //            }



        //            xml += "<Documento>";

        //            if (dr[0]["TIPODEDOCUMENTO"].ToString().ToUpper() == "ORDEM DE SERVICO")
        //            {
        //                xml += "<Tipo>REQ</Tipo>";
        //                xml += "<TipoParada>C</TipoParada>";
        //                xml += "<Modelo>55</Modelo>";
        //            }
        //            else
        //            {
        //                xml += "<Tipo>NFE</Tipo>";
        //                xml += "<TipoParada>E</TipoParada>";
        //                xml += "<Modelo>55</Modelo>";
        //            }

        //            xml += "<Numero>" + dr[0]["NUMERO"].ToString() + "</Numero>";
        //            xml += "<Valor>" + dr[0]["VALORDANOTA"].ToString().Replace(",", ".") + "</Valor>";

        //            if (dr[0]["IDCLIENTE"].ToString() == "65286")
        //            {
        //                //se o cliente for fastshop procura a serie na chave da nota

        //                if (ch.Length == 44)
        //                {
        //                    xml += "<Serie>" + ch.Substring(22, 3) + "</Serie>";
        //                }
        //            }
        //            else
        //                xml += "<Serie>" + (dr[0]["SERIE"].ToString().Trim() == "NFE" ? "001" : dr[0]["SERIE"].ToString().Trim()) + "</Serie>";



        //            xml += "<Emissao>" + DateTime.Parse(dr[0]["DATADEEMISSAO"].ToString()).ToString("yyyyMMdd") + "</Emissao>";
        //            xml += "<Atualizacao>" + DateTime.Parse(dr[0]["ATUALIZACAO"].ToString()).ToString("yyyyMMdd") + "</Atualizacao>";
        //            xml += "<Chave>" + ch + "</Chave>";
        //            xml += "<cnpj>" + dr[0]["CNPJ"].ToString() + "</cnpj>";

        //            xml += "<cnpjEmissor>" + dr[0]["CNPJEMISSOR"].ToString().Trim().Replace(" ", "") + "</cnpjEmissor>";
        //            xml += "<cnpjTransportador>" + dr[0]["CNPJTRANSPORTADOR"].ToString().Trim().Replace(" ", "") + "</cnpjTransportador>";

        //            xml += "</Documento>";
        //            xml += "<Cliente>";
        //            xml += "<Codigo>" + dr[0]["CODIGOCLIENTE"].ToString() + "</Codigo>";
        //            xml += "<Contato>" + dr[0]["CONTATO"].ToString() + "</Contato>";

        //            //if (dr[0]["TELEFONE"].ToString() == "")
        //            //    xml += "<Telefone>11 9999-9999</Telefone>";
        //            //else
        //            xml += "<Telefone>" + dr[0]["TELEFONE"].ToString().Trim().Replace(" ", "") + "</Telefone>";


        //            //if (dr[0]["EMAIL"].ToString() == "")
        //            //    xml += "<Email>moises@sistecno.com.br</Email>";
        //            //else
        //            xml += "<Email>" + dr[0]["EMAIL"].ToString() + "</Email>";


        //            xml += "<Razao>" + dr[0]["RAZAO"].ToString().Replace("&", "") + "</Razao>";
        //            xml += "<Endereco>" + dr[0]["ENDERECO"].ToString() + ", " + dr[0]["NUMERO_END"].ToString() + "</Endereco>";
        //            xml += "<Bairro>" + dr[0]["BAIRRO"].ToString() + "</Bairro>";
        //            xml += "<Cidade>" + dr[0]["CIDADE"].ToString() + "</Cidade>";
        //            xml += "<Estado>" + dr[0]["ESTADO"].ToString() + "</Estado>";
        //            xml += "<Pais>BRASIL</Pais>";
        //            xml += "<CEP>" + dr[0]["CEP"].ToString() + "</CEP>";


        //            xml += "<Regiao>" + dr[0]["REGIAO"].ToString() + "</Regiao>";
        //            xml += "<TipoCliente></TipoCliente>";
        //            xml += "<Mensagem></Mensagem>";
        //            xml += "</Cliente>";

        //            xml += "<SKUs>";
        //            bool jaInseriu = false;

        //            for (int ii = 0; ii < dr.Length; ii++)
        //            {
        //                if (dr[ii]["CODIGOPR"].ToString() != "")
        //                {
        //                    xml += "<SKU codigo=" + quote + dr[ii]["CODIGOPR"].ToString() + quote + ">";
        //                    xml += "<PesoBruto>" + dr[ii]["PESOBRUTO"].ToString() + "</PesoBruto>";
        //                    xml += "<PesoLiquido>" + dr[ii]["PESOLIQUIDO"].ToString() + "</PesoLiquido>";
        //                    xml += "<Volumes>" + dr[ii]["VOLUMES"].ToString() + "</Volumes>";
        //                    xml += "<Descricao>" + dr[ii]["DESCRICAO"].ToString().Replace("'", "").Replace("&", "e") + "</Descricao>";
        //                    xml += "<Qde>" + dr[ii]["QUANTIDADE"].ToString().Replace(",", ".") + "</Qde>";
        //                    xml += "<Uom>" + dr[ii]["UNIDADEDEMEDIDA"].ToString() + "</Uom>";
        //                    xml += "<Barcode>" + dr[ii]["BARCODE"].ToString() + "</Barcode>";
        //                    xml += "</SKU>";
        //                }
        //                else
        //                {
        //                    if (jaInseriu == false)
        //                    {
        //                        xml += "<SKU codigo=" + quote + dr[0]["NUMERO"].ToString() + quote + ">";
        //                        xml += "<PesoBruto>0</PesoBruto>";
        //                        xml += "<PesoLiquido>0</PesoLiquido>";
        //                        xml += "<Volumes>1</Volumes>";
        //                        xml += "<Descricao>DANFE " + dr[0]["NUMERO"].ToString() + "</Descricao>";
        //                        xml += "<Qde>1</Qde>";
        //                        xml += "<Uom>FL</Uom>";
        //                        xml += "<Barcode>" + ch + "</Barcode>";
        //                        xml += "</SKU>";
        //                        jaInseriu = true;
        //                    }
        //                }
        //            }
        //            xml += "</SKUs> ";

        //            xml += " </Documentos> ";

        //            string auxXML = xml;
        //            xml = Base64Encode(xml);

        //            if (ch.Contains("NFE"))
        //            {
        //                ch = ch.Replace("NFE", "000");
        //                gerouCahve = true;
        //            }

        //            //string NomeArquivo = ch + DateTime.Now.ToString("yyyyMMddhhmmssffftt") + ".xml";
        //            string NomeArquivo = dtds.Rows[i]["IDDOCUMENTO"].ToString() + "32.xml";

        //            WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
        //            string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
        //                           "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
        //                           "<SOAP-ENV:Body>" +
        //                           "<tns:importDocsToPOD xmlns:tns=\"urn:WebServicePOD\">" +
        //                           "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
        //                           "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
        //                           "</tns:" + "importDocsToPOD" + ">" +
        //                           "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
        //            byte[] data = Encoding.ASCII.GetBytes(postData);
        //            request.Method = "POST";
        //            request.ContentType = "text/xml; charset=ISO-8859-1";
        //            request.Headers.Add("SOAPAction", "urn:WebServicePOD#getDocumentsFromPOD");
        //            request.Headers.Add("Authorization", "Basic " + auth);
        //            request.ContentLength = data.Length;


        //            using (var stream = request.GetRequestStream())
        //            {
        //                stream.Write(data, 0, data.Length);
        //            }


        //            Cursor.Current = Cursors.WaitCursor;
        //            WebResponse response = (HttpWebResponse)request.GetResponse();
        //            StreamReader sr = new StreamReader(response.GetResponseStream());

        //            XmlDocument xmlAwnser = new XmlDocument();
        //            xmlAwnser.LoadXml(sr.ReadToEnd());


        //            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
        //            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        //            xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

        //            Label2.Text = DateTime.Now + "-" + "Enviando Nota ao Comprovei. IdDocumento: " + sss;
        //            Application.DoEvents();

        //            string retorno = xmlAwnser["status"].InnerText;

        //            retorno = retorno.Replace("'", "");
        //            retorno = retorno.Replace("/", "");
        //            retorno = retorno.Replace("\\", "");
        //            retorno = retorno.Replace("??", "CA");

        //            string sql = "";

        //            if (retorno.ToUpper().Contains("WEVE GOT SOME PROBLEM"))
        //            {
        //                sql = "UPDATE DOCUMENTO SET EnviadoComprovei=null  WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
        //                sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", "Data Source=192.168.10.20;Initial Catalog=homerefill;User ID=sa;Password=@oncetsis05083#;") + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), '" + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
        //                //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";

        //                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei ", "Problema no retorno em: " + DateTime.Now.ToString() + "- IdDocumento:" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + "<BR> " + retorno, "mail.grupologos.com.br", "logos0902", "Envio Comprovei - Problema no retorno do comprovei");

        //            }
        //            else
        //            {

        //                sql = "UPDATE DOCUMENTO SET EnviadoComprovei='ENVIADO COMPROVEI - " + DateTime.Now + "' " + (gerouCahve == true ? ", DocumentoDoCliente4='" + ch + "'" : "") + " WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
        //                sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", "Data Source=192.168.10.20;Initial Catalog=homerefill;User ID=sa;Password=@oncetsis05083#;") + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), 'Filial: " + dr[0]["REGIAO"].ToString() + "  " + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
        //                //sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";
        //            }
        //            Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //            GravarXML(auxXML, DateTime.Now);

        //            try
        //            {
        //                //TransferirNotasDaHomeRefill(dtds.Rows[i]["IDDOCUMENTO"].ToString(), dtds.Rows[i]["NUMERO"].ToString(), dtds.Rows[i]["SERIE"].ToString(), dtds.Rows[i]["anomes"].ToString());
        //            }
        //            catch (Exception)
        //            { }


        //            GravarLog(ch, retorno);
        //            // Label2.Text = "Terminou Envio Comprovei";

        //            Label2.Text = "EnviarComprovei Termino: " + DateTime.Now.ToString();


        //        }
        //        catch (Exception ex)
        //        {
        //            reiniciarTimers();
        //            Label2.Text = "EnviarComprovei Erro: " + DateTime.Now.ToString();
        //            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei ", "Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
        //        }
        //    }
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            ////EnviarComprovei(false, false);
            ////AcertarNotasNoStistranetDoComprovei();
            //MessageBox.Show("finalizou");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //GravarPedidosHomeRefill();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //EnviarComprovei(false, true);

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
            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            //    Sistran.Library.GetDataTables.RetornarDataTableWS("delete from ProtocoloComprovei where (XmlProtocolo like '%Aguarde a conclusão do mesmo para abrir um novo chamado%'  or XmlProtocolo not like '%sem ocorr%') and DataSolicitacao>=getdate()-1 ; select 1", cnx);

                reiniciarOperacao();
             

            }
            catch (Exception)
            {

                if (this.BackColor == Color.Yellow)
                {
                    reiniciarOperacao();
                }
            }
        }

        private void reiniciarOperacao() 
        {
            try
            {
                SolicitarProtocolo();
                SolicitarProtocolo();
                //SolicitarProtocolo();
                timer1.Enabled = false;
                this.BackColor = Color.Yellow;

                
                for (int ix = 0; ix < 10000; ix++)
                {
                    if (ix % 500 == 0)
                        SolicitarProtocolo();

                    //string sql = "select protocolo from ProtocoloComprovei where DataSolicitacao >= '2022-02-13'  and DataConclusao is null and XmlProtocolo not like '%sem ocorr%' order by 1 asc";
                    string sql = "select top 10 protocolo, DataSolicitacao from ProtocoloComprovei where ProcessadoSistran is null and XmlProtocolo not like '%sem ocorr%' and  XmlProtocolo not like '%Aguarde a conclusão do mesmo para abrir um novo chamado%'   and XmlProtocolo not like '%Existe um protocolo aberto para este método%' and DataSolicitacao>=getdate()-1 and protocolo<>'' and protocolo is not null order by 2 desc";

                    if (ix % 5 == 0 && ix>0)
                        sql = "select   top 100 protocolo, DataSolicitacao from ProtocoloComprovei where ProcessadoSistran is null and XmlProtocolo not like '%sem ocorr%'  and  XmlProtocolo not like '%Aguarde a conclusão do mesmo para abrir um novo chamado%'   and XmlProtocolo not like '%Existe um protocolo aberto para este método%' and DataSolicitacao>=getdate()-1 order by 2 desc";

                    DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                 
                    for (int i = 0; i < dtNota.Rows.Count; i++)
                    {
                        ConsultarProtocoloNaMao(false, dtNota.Rows[i][0].ToString());                      

                        Label2.Text = DateTime.Now + "- Protocolo: "+ dtNota.Rows[i][0].ToString() + " - " + dtNota.Rows[i]["DataSolicitacao"].ToString();
                        Application.DoEvents();                 

                    }
                    if (ix == 9000)
                        ix = 0;
                }            

            }
            catch (Exception)
            {

                reiniciarOperacao();
            }
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

            for (int ii = 0; ii < 10; ii++)
            {
                //if (ii == 49)
                //    ii = 0;

                Thread.Sleep(5000);
                SolicitarProtocolo();

                //try
                //{
                //    DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin("Exec prc_ProcessarProtocolo15", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                //    for (int i = 0; i < dtNota.Rows.Count; i++)
                //    {
                //        try
                //        {
                //            ConsultarProtocoloNaMao(false, dtNota.Rows[i]["Protocolo"].ToString());
                //        }
                //        catch (Exception)
                //        {

                //        }
                //    }
                //}
                //catch (Exception)
                //{
                //    continue;   
                //}

               // Thread.Sleep(10000);



            }
            MessageBox.Show("Test");

           

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProcessarImagensComprovei();
            Application.Exit();
        }

        private void ProcessarImagensComprovei()
        {
            return;
            try
            {
                string sql = "Select distinct top 5 do.IDDocumentoOcorrencia, rci.* from RetornoComproveiImagem rci with (nolock) inner join DocumentoOcorrencia do with (nolock)  on do.IdOcorrenciaComprovei = rci.IdOcorrenciaComprovei where do.DataOcorrencia >= getdate()-1 order by 1 desc";
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

                                string id = RetornarIdTabelaNovo("DOCUMENTOOCORRENCIAARQUIVO").ToString();// Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
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

            for (int i = 0; i < 100; i++)
            {
                EnviarPedidoJojapar();
            }
            
        }

        private void EnviarPedidoJojapar()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string numeroPed = "";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {

                string sql = "  Select top 1 *, c.Nome Cidade, est.Uf, b.Nome Bairro  ";
                sql += " from Documento d  with (nolock)  ";
                sql += " Inner Join DocumentoFilial DF on df.IdDocumento = d.IdDocumento ";
                sql += " inner join Cidade c on c.IdCidade = d.IDEnderecoCidade ";
                sql += " inner join Estado est on est.IDEstado = c.IDEstado  ";
                sql += "left join Bairro b on b.IdBairro = d.IdEnderecoBairro";
                sql += " Where IDCliente = 3975526 and TipoDeDocumento='Pedido' and (df.Situacao='PAGAMENTO APROVADO'  OR d.payment like '%CRL-%' or d.payment like '%COUPON%' or df.Situacao='EM ROMANEIO') And EnviadoJosapar is null  and d.Ativo='SIM' order by 1 desc";
                //sql += " Where d.IdDocumento in (27320020)";

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
                        string idDocumento = dtf.Rows[0]["IdDocumento"].ToString();

                        req.order.orderId = (dtf.Rows[0]["Numero"].ToString() == "" ? dtf.Rows[0]["NumeroOriginal"].ToString() : dtf.Rows[0]["Numero"].ToString());//
                        numeroPed = req.order.orderId;
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
                        cus.state_subscription = ds.Tables[0].Rows[0]["InscricaoRG"].ToString().ToUpper().Replace("ISENTO", "");

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

                        if (dtf.Rows[0]["Cidade"].ToString().ToUpper().Trim() == "CIDADE NAO CADASTRADA")
                        {
                            try
                            {
                                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Josapar Erro", "Pedido " + dtf.Rows[0]["IdDocumento"].ToString() + " sem a cidade ou bairro", "mail.grupologos.com.br", "logos0902", "Erro no Timer");
                            }
                            catch (Exception)
                            { }
                            return;
                        }



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
                            DataRow[] dr = pc.Select("IdProdutoCliente=" + ds.Tables[1].Rows[i]["IdProdutoCliente"].ToString(), "");
                            DataRow[] drFrete = di.Select("IdDocumentoItem=" + ds.Tables[3].Rows[i]["IdDocumentoItem"].ToString(), "");

                            if (dr.Length > 0)
                            {
                                orderLine.sku = dr[0]["Codigo"].ToString();
                                orderLine.skuType = ds.Tables[1].Rows[i]["skuType"].ToString();
                                orderLine.quantity = Convert.ToInt32(decimal.Parse(ds.Tables[1].Rows[i]["QUANTIDADE"].ToString()));
                                orderLine.catalogListPrice = Convert.ToDecimal(ds.Tables[1].Rows[i]["catalogListPrice"].ToString());
                                orderLine.listPrice = Convert.ToDecimal(ds.Tables[1].Rows[i]["listPrice"].ToString());
                                orderLine.salePrice = Convert.ToDecimal(ds.Tables[1].Rows[i]["ValorUnitario"].ToString());
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

                        var pagamento = dtf.Rows[0]["Payment"].ToString().Split('?');

                        for (int i = 0; i < pagamento.Length; i++)
                        {
                            var subPag = pagamento[i].Split('|');
                            Josapar.OrderService.payment pay = new Josapar.OrderService.payment()
                            {
                                Item = new Josapar.OrderService.abstractPayment()
                                {
                                    paymentType = subPag[0].ToString(),
                                    value = decimal.Parse(subPag[1].ToString())
                                }
                            };

                            lpay.Add(pay);
                        }
                        req.order.paymentList = lpay.ToArray();

                        #endregion


                        //return;
                        //Enviar Pedido Obtendo Retorno
                        Robo.Email.Notas.Solutions.Windows.Testes.Josapar.OrderService.integrateOrderResponse response = ws_order.integrateOrder(req);

                        if (response.message == "Pedido recebido com sucesso.")
                        {
                            sql = "Update Documento set  EnviadoJosapar='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where IdDocumento=" + dtf.Rows[0]["IdDocumento"].ToString() + "; select 1";
                            DataSet dss = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx);

                            string sqlLog = "insert into logsInfracomence(DataHora, Acao, Status) values (getdate(),'Pedido " + numeroPed + "', 'Enviado para josapar'); ";
                            Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlLog, cnx);
                        }
                        else
                        {
                            try
                            {
                                string sqlLog = "insert into logsInfracomence(DataHora, Acao, Status) values (getdate(),'Pedido " + numeroPed + "', 'Erro ao enviar para josapar: Erro: " + response.message + "'); ";
                                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlLog, cnx);

                                if (response.message == "ERRO - PEDIDO JA EXISTE NA BASE." || response.message== "Pedido já existe no ERP")
                                {
                                    sqlLog = "Update documento set EnviadoJosapar=getdate() where IdDocumento=  " + dtf.Rows[0]["IdDocumento"].ToString();
                                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlLog, cnx);
                                }
                            }
                            catch (Exception)
                            {
                            }

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

        //public DataTable NotasComOcorrenciasViaVarejo(string cnpj)
        //{
        //    string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
        //    string sql = "exec Prc_Integrar_ViaVarejo '" + cnpj +  "'" ;
        //    DataSet dss = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx);

        //    return dss.Tables[0];

        //}

        public DataTable NotasComOcorrenciasViaVarejoSislogic()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string sql = "exec Prc_Integrar_ViaVarejo_sis_logic ";
            DataSet dss = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx);

            return dss.Tables[0];

        }

        public DataTable NotasComOcorrenciasNestle()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string sql = "exec Prc_Integrar_Ocorrencia_Nestle ";
            DataSet dss = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx);

            return dss.Tables[0];

        }


        public void EnviarOcorrenciaFrioVIX()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string sql = "exec Prc_OcorrenciasDicponiveisFrioVIX ";
            DataSet dss = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx);

            /*
             •	Link Homologação: srvmanos2.no-ip.info:200/scripts/mh.dll/wc
            •	Link Produção: webcorpem.no-ip.info:63574/scripts/mh.dll/wc

             */

            string idDocOco = "";

            for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
            {

                idDocOco = dss.Tables[0].Rows[i]["IdDocumentoOcorrencia"].ToString();

                sql = "exec Prc_OcorrenciasDicponiveisFrioVIXDetalehe " + dss.Tables[0].Rows[i]["IdDocumentoOcorrencia"].ToString();
                DataSet dtoco = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx);

                if (dtoco.Tables[0].Rows.Count == 0)
                {
                    Label2.Text =DateTime.Now +  "Sem Ocorrencia";
                    Application.DoEvents();
                    continue;
                }

                

                var client = new RestClient("http://webcorpem.no-ip.info:63574/scripts/mh.dll/wc");

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);

                FrioVIX frioVIX = new FrioVIX();
                CORPEMTMSOCO cORPEMTMSOCO = new CORPEMTMSOCO();

                cORPEMTMSOCO.cnpj_trp_ws = "74395542000147";
                cORPEMTMSOCO.num_cli_ws = int.Parse(dss.Tables[0].Rows[i]["CodidoEnvioCliente"].ToString());

                frioVIX.CORPEM_TMS_OCO = cORPEMTMSOCO;
                //frioVIX.CORPEM_TMS_OCO.cnpj_trp_ws = "74395542000147";
                //frioVIX.CORPEM_TMS_OCO.num_cli_ws = int.Parse(dss.Tables[0].Rows[i]["CodidoEnvioCliente"].ToString());

                if (dtoco.Tables[0].Rows[0]["NrNF"].ToString() == "")
                    continue;

                var lista = new List<TrpOco>();
                Oco oco = new Oco()
                {
                    sr_cte = dtoco.Tables[0].Rows[0]["conhecimentoSerie"].ToString(),
                    obs_oco = dtoco.Tables[0].Rows[0]["Descricao"].ToString(),
                    cnpj_trp_cte = "74395542000147",
                    cod_oco = dtoco.Tables[0].Rows[0]["original_code"].ToString().Trim(),
                    dt_oco = DateTime.Parse(dtoco.Tables[0].Rows[0]["event_date"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"),
                    num_cte = int.Parse(dtoco.Tables[0].Rows[0]["conhecimento"].ToString()),
                    num_nfe = int.Parse(dtoco.Tables[0].Rows[0]["NrNF"].ToString()),
                    sr_nfe = dtoco.Tables[0].Rows[0]["SerieNF"].ToString(),
                    cnpj_emi_nfe = dtoco.Tables[0].Rows[0]["CNPJEmitenteNF"].ToString().Replace(".", "").Replace("/", "").Replace("/", "").Replace("-", "")
                    //cnpj_emi_nfe = "09316105000714"
                    

                };

                List<Oco> ocoss = new List<Oco>();

                ocoss.Add(oco);

                lista.Add(new TrpOco()
                {
                    cnpj_trp_oco = "74395542000147",
                    razao_trp_oco = "Vex Logistica",
                    ocos = ocoss
                });


               

                frioVIX.CORPEM_TMS_OCO.trp_oco = lista;

                var body = JsonConvert.SerializeObject(frioVIX);


                Label2.Text = DateTime.Now + "FrioVIx-" + body;
                Application.DoEvents();


                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);


                var json = response.Content;
                var cc = JsonConvert.DeserializeObject<FrioVIXRetornoRoot>(json);

                try
                {
                    Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set Protocolo='" + cc.trp_oco[0].ocos[0].num_protocolo + "', EnviadoParaCliente=getdate(),DetalheEnvioParaCliente=null , DetalheEnvio='OK: " + cc.trp_oco[0].ocos[0].cod_erro + " : " + cc.trp_oco[0].ocos[0].msg_erro + "' where IdDocumentoOcorrencia=" + idDocOco + "; Select 1", cnx);

                }
                catch (Exception)
                {
                    Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set Protocolo='erro', EnviadoParaCliente=getdate(),DetalheEnvioParaCliente=null  where IdDocumentoOcorrencia=" + idDocOco + "; Select 1", cnx);

                }

                //Label2.Text = DateTime.Now + "- Enviou Ocorrencias ViaVarejo: " + body;
            }

        }


        private void button9_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            // processarViaVarejoImagemERecebedor("154745807");
            DataTable dataTable = NotasComOcorrenciasNestle();
            processarNestle(dataTable);

            //for (int ii = 0; ii < 100; ii++)
            //{
            //    try
            //    {
            //        DataTable dtOcoVia = NotasComOcorrenciasViaVarejoSislogic();
            //        for (int i = 0; i < dtOcoVia.Rows.Count; i++)
            //        {
            //            processarViaVarejoImagemERecebedor(dtOcoVia.Rows[i]["IdDocumentoOcorrencia"].ToString());

            //            if(ii%10 == 0)
            //            {

            //            }

            //        }
            //    }
            //    catch (Exception)
            //    {
            //        if (ii == 90)
            //            ii = 0;

            //    }


            //    if (ii == 90)
            //        ii = 0;
            //}

        }


        public void processarViaVarejoImagemERecebedor(string idDocOco)
        {

            //for (int i = 0; i < nfs.Rows.Count; i++)
            //{
            try
            {

                ViaVarejoModel m = new ViaVarejoModel();

                Label2.Text = "processarViaVarejoImagemERecebedor";
                Application.DoEvents();

                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                string sql = "exec Prc_Integrar_Ocorrencia_viaVarejo_Imagem_Entregador " + idDocOco;
                DataTable dss = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                for (int ii = 0; ii < dss.Rows.Count; ii++)
                {
                    Label2.Text = "";
                    Application.DoEvents();

                    m.LogisticsProvider = "JM/Vex";
                    m.Shipper = "Via Varejo";
                    m.LogisticsProviderFederalTaxId = "74395542000147";
                    m.ShipperFederalTaxId = dss.Rows[ii]["CnpjCliente"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim();
                    m.InvoiceKey = dss.Rows[ii]["chave"].ToString();
                    m.InvoiceNumber = dss.Rows[ii]["Numero"].ToString();
                    m.InvoiceSeries = dss.Rows[ii]["Serie"].ToString();
                    m.LogisticsProviderId = 227;

                    string DadosRecNome = "";
                    string DadosRecDoc = "";

                    if (dss.Rows[ii]["NomeRecebedor"].ToString() != "")
                    {
                        var c = dss.Rows[ii]["NomeRecebedor"].ToString().Split(' ');
                        if (c.Length > 1)
                        {
                            DadosRecNome = c[0];
                            DadosRecDoc = c[c.Length - 1];
                        }
                    }

                    AdditionalInformation sdinfo = new AdditionalInformation()
                    {
                        ReceiverDocument = dss.Rows[ii]["DocRecebedor"].ToString(),
                        ReceiverName = DadosRecNome,
                        Kinship = DadosRecDoc

                    };

                    if (dss.Rows[ii]["content_in_base64"].ToString() != "")
                    {
                        List<Attachment> attachments = new List<Attachment>();

                        attachments.Add(new Attachment()
                        {
                            ContentInBase64 = Convert.ToBase64String((byte[])dss.Rows[ii]["content_in_base64"]),
                            Type = "POD",
                            FileName = dss.Rows[ii]["chave"].ToString() + ".jpg",
                             MimeType = "image/jepg",
                            AdditionalInformation = sdinfo

                        });


                        Event ee = new Event();
                        ee.EventDate = DateTime.Parse(dss.Rows[ii]["DataOcorrencia"].ToString());
                        ee.OriginalCode = dss.Rows[ii]["codigo"].ToString().Trim();
                        ee.OriginalMessage = dss.Rows[ii]["texto"].ToString();
                        ee.Attachments = attachments;

                        m.Events.Add(ee);
                    }
                    else
                    {
                        Event ee = new Event();
                        ee.EventDate = DateTime.Parse(dss.Rows[ii]["DataOcorrencia"].ToString());
                        ee.OriginalCode = dss.Rows[ii]["codigo"].ToString().Trim();
                        ee.OriginalMessage = dss.Rows[ii]["texto"].ToString();

                        m.Events.Add(ee);

                    }
                }

                var client = new RestClient("https://apivia.sislogica.com.br/api/v1/tracking/add/events");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("platform", "Vex");
                request.AddHeader("logistic-provider-api-key", "100cb3b7-842c-4dfa-ab2f-329c53397871");
                request.AddHeader("Content-Type", "application/json");

                    var body = JsonConvert.SerializeObject(m);

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);


                    var json = response.Content;
                    var cc = JsonConvert.DeserializeObject<RootVia>(json);

                    
                    Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set Protocolo='" + cc.hash + "', EnviadoParaCliente=getdate(),DetalheEnvioParaCliente=null , DetalheEnvio='OK: " + cc.status + " : " + cc.messages[0].text + "' where IdDocumentoOcorrencia=" + idDocOco + "; Select 1", cnx);

                    Label2.Text = DateTime.Now + "- Enviou Ocorrencias ViaVarejo: " + body;
                    Application.DoEvents();
               

            }

            catch (Exception)
            {                
            }
            // }
        }

        /*antigo
        private void processarViaVarejo(DataTable nfs)
        {
            for (int i = 0; i < nfs.Rows.Count; i++)
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                string sql = "exec Prc_Integrar_Ocorrencia_viaVarejo " + nfs.Rows[i]["IDDocumentoOcorrencia"].ToString();
                DataTable dss = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                for (int ii = 0; ii < dss.Rows.Count; ii++)
                {
                    string ocorr = "[" +
                                    "{ " +
                                    "\"data\": \"" + DateTime.Parse(dss.Rows[ii]["DataOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\", " +
                                    "\"id\": " + int.Parse(dss.Rows[ii]["codigo"].ToString().Trim()) + ", " +
                                    "\"cnpjFilial\": \"" + dss.Rows[ii]["CnpjCliente"].ToString().Trim().Replace(".", "").Replace("-", "").Replace("/", "") + "\"," + // fixo JM                                    
                                    "\"notaFiscal\": \"" + nfs.Rows[i]["Numero"].ToString() + "\", " +
                                    "\"serie\": \"" + nfs.Rows[i]["Serie"].ToString() + "\"," +
                                    "\"obervacao\": \"" + dss.Rows[ii]["texto"].ToString() + "\"" +
                                    "}" +
                                    "]";


                    using (var client = new HttpClient())
                    {
                        var serializedProduto = JsonConvert.SerializeObject(ocorr);
                        var content = new StringContent(ocorr, Encoding.UTF8, "application/json");                        
                        client.DefaultRequestHeaders.Add("token", "100CB3B7-842C-4DFA-AB2F-329C53397871" );
                        client.DefaultRequestHeaders.Add("tpOperacao", "0");
                        client.DefaultRequestHeaders.Add("cnpjTransportador", "74395542000147");                        
                        var result = client.PostAsync("http://vv.sislogica.com.br/api/ocorren", content ); // produção

                        var c = result.Result;

                        if (c.StatusCode == HttpStatusCode.OK)
                        {
                            var json = c.Content.ReadAsStringAsync();
                            var cc = JsonConvert.DeserializeObject<ocorrenViaVarejoRet>(json.Result);

                            var mm = cc;
                            Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set Protocolo='"+mm.protocolo+"', EnviadoParaCliente=getdate(),DetalheEnvioParaCliente='"+ ocorr + "' , DetalheEnvio='OK: " + mm.status + " - err: " + mm.erros + "' where IdDocumentoOcorrencia=" + nfs.Rows[i]["IdDocumentoOcorrencia"].ToString() + "; Select 1", cnx);

                            Label2.Text = DateTime.Now + "- Enviou Ocorrencias ViaVarejo: " + ocorr;
                            Application.DoEvents();
                        }
                        else
                        {
                            Label2.Text = DateTime.Now + "- Erro Ocorrencias ViaVarejo: " + ocorr;
                            Application.DoEvents();
                        }
                    }
                }
            }
        }
        */


        private void processarNestle(DataTable nfs)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            for (int i = 0; i < nfs.Rows.Count; i++)           
            {

                //DataTable dtFrete = Sistran.Library.GetDataTables.RetornarDataTableWin("Select IdFrete, CnpjRemetente from ColetaFreteRapido where Chave='" + nfs.Rows[i]["Chave"].ToString() +"'", cnx);
                
                //if (dtFrete.Rows.Count == 0)
                //   continue;

                string sql = "exec [Prc_Integrar_Ocorrencia_Nestle_Detalhe] " + nfs.Rows[i]["IDDocumentoOcorrencia"].ToString();
                DataTable dss = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                for (int ii = 0; ii < dss.Rows.Count; ii++)
                {

               
                    cNestle nestle = new cNestle()
                    {
                        cnpj_remetente = nfs.Rows[i]["CadRem"].ToString().Replace(".", "").Replace("/", "").Replace("-",""),
                        ocorrencia = int.Parse(dss.Rows[ii]["codigo"].ToString().Trim()),                       
                        data_hora = DateTime.Parse(dss.Rows[ii]["DataOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                        mensagem = dss.Rows[ii]["texto"].ToString(),
                        numero = nfs.Rows[i]["numero"].ToString(),
                        serie = nfs.Rows[i]["serie"].ToString()
                    };
                    
                    using (var client = new HttpClient())
                    {
                        var serializedProduto = JsonConvert.SerializeObject(nestle);
                        var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");
                        
                      //   var result = client.PostAsync("https://freterapido.com/api/external/transportadora/v1/quotes/"+ dtFrete.Rows[0][0].ToString() + "/occurrences?token=a9c1e96cb8969687557ce8f319a193d0", content); // produção
                        

                        var result = client.PostAsync("https://freterapido.com/api/external/transportadora/v1/quotes/occurrences?token=a9c1e96cb8969687557ce8f319a193d0", content); // produção

                        var c = result.Result;


                        if (c.StatusCode == HttpStatusCode.OK)
                        {
                            var json = c.Content.ReadAsStringAsync();
                            var cc = JsonConvert.DeserializeObject<ocorrenViaVarejoRet>(json.Result);

                            var mm = cc;
                            Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set Protocolo='Enviado', EnviadoParaCliente=getdate(),DetalheEnvioParaCliente='" + c.StatusCode + "' , DetalheEnvio='OK' where IdDocumentoOcorrencia=" + nfs.Rows[i]["IdDocumentoOcorrencia"].ToString() + "; Select 1", cnx);
                            Label2.Text = DateTime.Now + "- Enviou Ocorrencias Nestle: " + c.StatusCode;
                            Application.DoEvents();
                        }
                        else
                        {
                            Label2.Text = DateTime.Now + "- Erro Ocorrencias Nestle: " + content;

                            Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set Protocolo='erro', EnviadoParaCliente=getdate(),DetalheEnvioParaCliente='" + c.StatusCode + "' , DetalheEnvio='erro' where IdDocumentoOcorrencia=" + nfs.Rows[i]["IdDocumentoOcorrencia"].ToString() + "; Select 1", cnx);

                            Application.DoEvents();
                        }
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //EnviarPedidoJojapar();
            //LiberarFaturamentoJosapar();
            // EnviarTrackinBD();

            GerarTrackingJosapar();

        }

        private void LiberarFaturamentoJosapar()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            string sql = "select D.Numero, d.IDDocumento from Documento d with (nolock) ";
            sql += " Inner join DocumentoFilial df  with(nolock) on df.IDDocumento = d.IDDocumento ";
            sql += " where d.TipoDeDocumento = 'Pedido' ";
            sql += " and IDCliente = 3975526  /*and d.IdDocumento=20791474*/";
            sql += " and Ativo = 'SIM' ";
            sql += " and Origem = 'WebService' ";
            sql += " and df.situacao = 'LIBERADO PARA FATURAMENTO' ";
            sql += " and d.Numero not In (select OrderID from josapartracking where controlPointId='WMS' And orderId = d.Numero and controlPointNm='Separação Concluida')";
            sql += " and EnviadoFaturamentoJosapar is null";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (dt.Rows[i]["Numero"].ToString().Trim() == "0")
                    continue;

                sql = "insert into JosaparTracking(orderId,controlPointId,controlPointNm,occurrenceDt,Direcao) values ('" + dt.Rows[i]["Numero"].ToString() + "','WMS','Separação Concluida',getdate(),'Josapar'); ";
                sql += "insert into JosaparTracking(orderId,controlPointId,controlPointNm,occurrenceDt) values ('" + dt.Rows[i]["Numero"].ToString() + "','WMS','Separação Concluida',getdate()); ";
                sql += " Update documento set EnviadoFaturamentoJosapar=getdate() where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + " ;Update DocumentoFilial set Situacao = 'DOCUMENTO FATURADO' WHERE IDDOCUMENTO=" + dt.Rows[i]["IdDocumento"].ToString() + ";  Select 1";
                DataTable dts = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //var store = new System.Security.Cryptography.X509Certificates.X509Store("My");
            //store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadOnly);
            //foreach (var item in store.Certificates)
            //{
            //	var x = item;

            //	textBox1.Text += item.ToString() + "\n\r";
            //}


            //EnviarEstoqueJosapar();
        }

        //private void EnviarEstoqueJosapar()
        //{
        //    //eviar quabdo houver baixa de estoque.
        //    //entrada e saida
        //    string sqlBaixaReg = "";
        //    string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
        //    string sql = "Select top 50 * from JosaparEstoque where EnviadoJosapar is null order by 1";
        //    //string sql = "Select top 50 * from JosaparEstoque where IdJosaparEstoque>=8491";

        //    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);



        //    List<logs> l = new List<logs>();


        //    setStockRequest estR = new setStockRequest();
        //    var lista = new List<stock>();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        stock ex = new stock()
        //        {
        //            skuId = dt.Rows[i]["Codigo"].ToString(),
        //            quantity = Convert.ToInt32(dt.Rows[i]["SALDO"].ToString()),
        //            stockTypeSourceName = "controlUnitQuantity"
        //        };
        //        lista.Add(ex);

        //        l.Add(new logs()
        //        {
        //            datahora = DateTime.Now.ToString(),
        //            acao = "sku: " + ex.skuId + ", Quantidade: " + ex.quantity + " ,  stockTypeSourceName: " + ex.stockTypeSourceName.ToString()
        //        });

        //        sqlBaixaReg += "Update JosaparEstoque set EnviadoJosapar=getDate() where IdJosaparEstoque=" + dt.Rows[i]["IdJosaparEstoque"].ToString() + "; ";
        //    }

        //    estR.storeId = "JOSAPAR";
        //    estR.stockList = lista.ToArray();
        //    StockServicesClient x = new StockServicesClient();


        //    try
        //    {
        //        //x.ClientCredentials.ServiceCertificate.SetDefaultCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "CN=*.a8e.net.br, OU=COMODO SSL Wildcard, OU=Domain Control Validated");
        //        //x.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My,  X509FindType.FindBySubjectName, "CN=*.a8e.net.br, OU=COMODO SSL Wildcard, OU=Domain Control Validated");

        //        x.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
        //        x.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //        var resposta = x.setStock(estR);

        //        string sqlLog = "";
        //        for (int i = 0; i < l.Count; i++)
        //        {
        //            sqlLog += "insert into logsInfracomence(DataHora, Acao, Status) values ('" + l[i].datahora + "', '" + l[i].acao + "', '" + (resposta == null ? "-" : resposta.result.ToString()) + "'); ";
        //        }

        //        if (sqlLog.Length > 5)
        //        {
        //            Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlLog, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //            if (resposta.result.ToUpper() == "SUCESSO")
        //                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlBaixaReg, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //        }

        //        //MessageBox.Show("Envio OK.");
        //    }
        //    catch (Exception ex)
        //    {
        //        string erro = ex.Message;
        //        //MessageBox.Show(ex.Message);
        //    }
        //}

        //private void EnviarTesteSemBanco()
        //{
        //	setStockRequest estR = new setStockRequest();
        //	var lista = new List<stock>();

        //	stock ex = new stock()
        //	{
        //		skuId = "123456",
        //		quantity = 10,
        //		stockTypeSourceName = "controlUnitQuantity"
        //	};
        //	lista.Add(ex);

        //	estR.storeId = "JOSAPAR";
        //	estR.stockList = lista.ToArray();
        //	StockServicesClient x = new StockServicesClient();

        //	try
        //	{
        //		x.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
        //		x.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
        //		var resposta = x.setStock(estR);



        //		//	MessageBox.Show("Envio OK.");
        //	}
        //	catch (Exception ex1)
        //	{
        //		//MessageBox.Show(ex1.Message);
        //	}
        //}

        public string RetornarInvoices(DataRow[] row)
        {

            string str = "<inv:invoiceInfo>";

            //for (int i = 0; i < row.Length; i++)
            //{

            str += "<inv:issuerDocumentNr>" + row[0]["issuerDocumentNr"].ToString() + "</inv:issuerDocumentNr>" +
                "<inv:invoiceNumber>" + row[0]["invoiceNumber"].ToString() + "</inv:invoiceNumber>" +
                "<inv:invoiceSerialNumber>" + row[0]["invoiceSerialNumber"].ToString() + "</inv:invoiceSerialNumber>" +
                "<inv:invoiceEmissionDate>" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.000") + "-03:00</inv:invoiceEmissionDate>" +
                "<inv:invoiceEletronicKey>" + row[0]["invoiceEletronicKey"].ToString() + "</inv:invoiceEletronicKey>";
            //"<inv:objectDataList>" +
            //"<inv:objectData>" +
            //"<inv:objectId>1</inv:objectId>" +
            //"</inv:objectData>" +
            //"</inv:objectDataList>";
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

                    //arrumar o ponto aqui.
                    ret += "<trac1:skuDeliveryTracking>" +
                            "<trac1:skuId>" + row[ii]["skuId"].ToString() + "</trac1:skuId>" +
                            "<trac1:preparedQt>" + Convert.ToInt32(decimal.Parse(row[ii]["preparedQt"].ToString())) + "</trac1:preparedQt>" +
                            "<trac1:unitPrice>" + decimal.Parse(row[ii]["unitPrice"].ToString()).ToString().Replace(",", ".") + "</trac1:unitPrice>" +
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //WebRequest request = (HttpWebRequest)WebRequest.Create("https://ws-josapar.a8e.net.br/b2b/tracking");
            WebRequest request = (HttpWebRequest)WebRequest.Create("https://hub-diretodajosapar.ifcshop.com.br/b2b/tracking");
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //"<trac1:occurrenceDt>" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.000") + "-03:00</trac1:occurrenceDt>" +


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
            RetornarSKUS(row) +

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


         

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                WebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());

                XmlDocument xmlAwnser = new XmlDocument();
                xmlAwnser.LoadXml(sr.ReadToEnd());

                return xmlAwnser;
            }
            catch (Exception es)
            {
                return null;
            }
        }

        private void EnviarTrackinBD()
        {
            string sql = "select * from JosaparTracking jt with(nolock) left join JosaparSkuDeliveryTracking jdt on jdt.IdJosaparTracking = jt.IdJosaparTracking Where EnviadoInfracommerce is null and Direcao is null and jt.IDJosaparTracking>=12066  and controlPointId in('ETR','ENT') and ltrim(rtrim(jt.OrderId))<>'0' order by 1 ";
           // string sql = "select * from JosaparTracking jt with(nolock) left join JosaparSkuDeliveryTracking jdt on jdt.IdJosaparTracking = jt.IdJosaparTracking Where jt.IdJosaparTracking in(12017,12018,12019) ";
            

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "IdJosaparTracking");

            for (int i = 0; i < distinctValues.Rows.Count; i++)
            {
                DataRow[] r = dt.Select("IdJosaparTracking='" + distinctValues.Rows[i]["IdJosaparTracking"].ToString() + "'", "IdJosaparTracking");

                if (r.Length > 0)
                {

                    InfraTracking.Tracking tr = new InfraTracking.Tracking();

                    if (r[0]["controlPointId"].ToString() == "NFS")
                    {
                        //continue;

                        var x = ProcessaJosaparNfs(r);

                        if (x != null && x.InnerXml.Contains("<success>true</success>"))
                        { 
                            sql = "update JosaparTracking set EnviadoInfracommerce=getdate() where IdJosaparTracking='" + distinctValues.Rows[i]["IdJosaparTracking"].ToString() + "'";
                        
                            Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        }
                        else
                        {
                            sql = "update JosaparTracking set EnviadoInfracommerce=getdate(), Direcao='erro' where IdJosaparTracking='" + distinctValues.Rows[i]["IdJosaparTracking"].ToString() + "'";
                            Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        }
                    }
                    else
                    {
                        if (r[0]["controlPointId"].ToString() == "FAT")
                            Thread.Sleep(60000);



                        tr.orderId = int.Parse(r[0]["orderId"].ToString());
                        tr.controlPointId = r[0]["controlPointId"].ToString();
                        tr.controlPointNm = r[0]["controlPointNm"].ToString();
                        tr.occurrenceDt = DateTime.Parse( r[0]["occurrenceDt"].ToString());

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

                        //Envia uma tracking para pedido Cancelado
                        if (r[0]["controlPointId"].ToString() == "MNA")
                        {
                            tr = new InfraTracking.Tracking();
                            tr.contractId = "CAN";
                            tr.controlPointNm = "Pedido Cancelado";
                            tr.orderId = int.Parse(r[0]["orderId"].ToString());
                            tr.occurrenceDt = DateTime.Parse(r[0]["occurrenceDt"].ToString()); 



                            lista.Add(tr);
                        }

                        InfraTracking.captureTrackingRequest request = new InfraTracking.captureTrackingRequest();
                        request.trackingList = lista.ToArray();

                        try
                        {
                            InfraTracking.TrackingServicesClient serv = new InfraTracking.TrackingServicesClient();
                            serv.ClientCredentials.UserName.UserName = ConfigurationSettings.AppSettings["UserJosapar"];//  "josapar-b2b";
                            serv.ClientCredentials.UserName.Password = ConfigurationSettings.AppSettings["PasswodrJosapar"];
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                            try
                            {
                                var resp = serv.captureTracking(request);

                                if (resp.success)
                                {
                                    sql = "update JosaparTracking set EnviadoInfracommerce=getdate() where IdJosaparTracking='" + distinctValues.Rows[i]["IdJosaparTracking"].ToString() + "'";
                                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                }
                            }catch(Exception d)
                            {
                                sql = "update JosaparTracking set EnviadoInfracommerce=getdate(), Direcao='Erro' where IdJosaparTracking='" + distinctValues.Rows[i]["IdJosaparTracking"].ToString() + "'";
                                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                            }

                        }
                        catch (Exception ex)
                        {
                            string x = ex.Message;
                        }
                    }
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            EnviarTrackinBD();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
             EnviarTrackinWmsBD();
            EnviarTrackinWmsBD();

        }

        private void EnviarTrackinWmsBD()
        {
            Josapar.Tracking.TrackingServicesMediator_epSoapClient wsTr = new Josapar.Tracking.TrackingServicesMediator_epSoapClient();


            for (int i = 0; i < 10; i++)
            {


                string sql = "select top 1 * from JosaparTracking jt with(nolock)  Where EnviadoInfracommerce is null and Direcao = 'Josapar' and IdJosaparTracking>=12066  and OrderId<>'0' order by 1";


                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (dt.Rows.Count > 0)
                {

                    using (new OperationContextScope(wsTr.InnerChannel))
                    {
                        HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                        requestMessage.Headers["Authorization"] = "Basic MTYwNjIwOlBXRDE2MDYyMA==";
                        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                        Josapar.Tracking.captureTrackingRequest req = new Josapar.Tracking.captureTrackingRequest();
                        Josapar.Tracking.Tracking tr = new Josapar.Tracking.Tracking();
                        tr.orderId = int.Parse(dt.Rows[0]["orderId"].ToString());
                        tr.controlPointId = dt.Rows[0]["controlPointId"].ToString();
                        tr.controlPointNm = dt.Rows[0]["controlPointNm"].ToString();
                        tr.occurrenceDt = DateTime.Parse(dt.Rows[0]["occurrenceDt"].ToString()); ;

                        var lista = new List<Josapar.Tracking.Tracking>();
                        lista.Add(tr);

                        req.trackingList = lista.ToArray();
                        Josapar.Tracking.captureTrackingResponse resposta = wsTr.captureTracking(req);


                        Label2.Text = req.ToString() + " - Josapar " + dt.Rows[0]["orderId"].ToString();
                        Application.DoEvents();



                        if (resposta.success)
                        {
                            sql = "update JosaparTracking set EnviadoInfracommerce=getdate() where IdJosaparTracking='" + dt.Rows[0]["IdJosaparTracking"].ToString() + "'";
                            Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        }
                    }
                }
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            EnviarOcorrenciaFrioVIX();

            DataTable dataTable = NotasComOcorrenciasNestle();
            processarNestle(dataTable);

        }

        private void enviarOcorrenciaDafit()
        {
            try
            {
               // return;
                string sql = "exec Prc_Integrar_Dafiti";
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                //comentar
                // cnx = "Data Source=192.168.0.13;Initial Catalog=STNNOVO;Persist Security Info=True;User ID=sa;Password=@oncetsis05083#";
                DataTable dtcabec = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                for (int i = 0; i < dtcabec.Rows.Count; i++)
                {
                   string IdDoc = dtcabec.Rows[i]["IdDocumento"].ToString();

                    sql = "exec Prc_Integrar_Ocorrencia_Intelipost_dafit " + dtcabec.Rows[i]["IdDocumentoOcorrencia"].ToString();

                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                    if (dt.Rows.Count == 0)
                    {
                        sql = "Update DocumentoOcorrencia set protocolo='ocorrencia sem depara' where IdDocumentoOcorrencia=" + dtcabec.Rows[i]["IdDocumentoOcorrencia"].ToString();
                        Sistran.Library.GetDataTables.RetornarDataTableWS(sql + ";select 1", cnx);

                        continue;
                    }

                    bool enviar = true;
                    if(dt.Rows[0]["DocumentoDoCliente1"].ToString().Trim() == "REVERSA")
                    {
                        if(dt.Rows[0]["original_code"].ToString().Trim() != "53"  
                            && dt.Rows[0]["original_code"].ToString().Trim() != "51" 
                            && dt.Rows[0]["original_code"].ToString().Trim() != "78")
                        {
                            enviar = false;
                        }
                    }


                    if (enviar)
                    {
                        WebRequest request = (HttpWebRequest)WebRequest.Create("https://api-transportadoras.dafiti.com.br/RESTAdapter/ocorren");
                        string postData = "{\"DATA\":" +
                            "[" +
                            "{\"NOME_ARQ\":\"WS_JM_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + "\"," +
                            "\"OCOR\":[" +
                            "{\"STCD1\": \"74395542000147\"," +
                            "\"VBELN\": \"" + dt.Rows[0]["NumeroOriginal"].ToString().Trim() + "\"," + //NumeroOriginal
                            "\"SIGDAFATU\": \"" + dt.Rows[0]["original_code"].ToString().Trim() + "\"," + //Codigo da ocorrencia 
                            "\"DATA_OCORREN\": \"" + DateTime.Parse(dt.Rows[0]["event_date"].ToString()).ToString("ddMMyyyy") + "\"," +
                            "\"HORA_OCORREN\": \"" + DateTime.Parse(dt.Rows[0]["event_date"].ToString()).ToString("HHmmss") + "\" " +
                            "}" +
                            "]" +
                            "}" +
                            "]" +
                            "}";

                        byte[] data = Encoding.ASCII.GetBytes(postData);
                        request.Method = "POST";
                        request.Headers.Add("Authorization", "Basic dmV4OiR6N3EkRGYx");
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
                            var ret = sr.ReadToEnd();
                            var x = JsonConvert.DeserializeObject<Root>(ret);
                            Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set Protocolo='" + x.DATA_RESP[0].RESUL.PROT + "' , EnviadoParaCliente=getDate(), DetalheEnvioParaCliente='" + postData + "', DetalheEnvio='" + ret.ToString() + "' where IdDocumentoOcorrencia=" + dtcabec.Rows[i]["IdDocumentoOcorrencia"].ToString() + "; Select 1", cnx); ;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else //Nao envia ocorrencia de reversa/coleta 
                    {
                        try
                        {
                            Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set EnviadoParaCliente=getDate(), DetalheEnvioParaCliente='Ocorrencia Intermediaria de coleta' where IdDocumentoOcorrencia=" + dtcabec.Rows[i]["IdDocumentoOcorrencia"].ToString() + "; Select 1", cnx); ;

                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }

                }
            }
            catch (Exception)
            {

            }
        }


        //private void enviarOcorrenciaInfraCommerce()
        //{
        //    try
        //    {
        //        // return;
        //        string sql = "exec [Prc_Integrar_infracommerce]";
        //        string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

        //        //comentar
        //        // cnx = "Data Source=192.168.0.13;Initial Catalog=STNNOVO;Persist Security Info=True;User ID=sa;Password=@oncetsis05083#";
        //        DataTable dtcabec = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

        //        for (int i = 0; i < dtcabec.Rows.Count; i++)
        //        {
        //            string IdDoc = dtcabec.Rows[i]["IdDocumento"].ToString();

        //            sql = "exec Prc_Integrar_Ocorrencia_Intelipost_infracommerce " + dtcabec.Rows[i]["IdDocumentoOcorrencia"].ToString();

        //            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

        //            if (dt.Rows.Count == 0)
        //            {
        //                sql = "Update DocumentoOcorrencia set protocolo='ocorrencia sem depara' where IdDocumentoOcorrencia=" + dtcabec.Rows[i]["IdDocumentoOcorrencia"].ToString();
        //                Sistran.Library.GetDataTables.RetornarDataTableWS(sql + ";select 1", cnx);

        //                continue;
        //            }

        //            bool enviar = true;
        //            //if (dt.Rows[0]["DocumentoDoCliente1"].ToString().Trim() == "REVERSA")
        //            //{
        //            //    if (dt.Rows[0]["original_code"].ToString().Trim() != "53"
        //            //        && dt.Rows[0]["original_code"].ToString().Trim() != "51"
        //            //        && dt.Rows[0]["original_code"].ToString().Trim() != "78")
        //            //    {
        //            //        enviar = false;
        //            //    }
        //            //}


        //            if (enviar)
        //            {
        //                WebRequest request = (HttpWebRequest)WebRequest.Create("https://api-transportadoras.dafiti.com.br/RESTAdapter/ocorren");
        //                string postData = "{\"DATA\":" +
        //                    "[" +
        //                    "{\"NOME_ARQ\":\"WS_JM_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + "\"," +
        //                    "\"OCOR\":[" +
        //                    "{\"STCD1\": \"74395542000147\"," +
        //                    "\"VBELN\": \"" + dt.Rows[0]["NumeroOriginal"].ToString().Trim() + "\"," + //NumeroOriginal
        //                    "\"SIGDAFATU\": \"" + dt.Rows[0]["original_code"].ToString().Trim() + "\"," + //Codigo da ocorrencia 
        //                    "\"DATA_OCORREN\": \"" + DateTime.Parse(dt.Rows[0]["event_date"].ToString()).ToString("ddMMyyyy") + "\"," +
        //                    "\"HORA_OCORREN\": \"" + DateTime.Parse(dt.Rows[0]["event_date"].ToString()).ToString("HHmmss") + "\" " +
        //                    "}" +
        //                    "]" +
        //                    "}" +
        //                    "]" +
        //                    "}";

        //                byte[] data = Encoding.ASCII.GetBytes(postData);
        //                request.Method = "POST"
        //                request.Headers.Add("Authorization", "Basic dmV4OiR6N3EkRGYx");
        //                request.ContentLength = data.Length;

        //                using (var stream = request.GetRequestStream())
        //                {
        //                    stream.Write(data, 0, data.Length);
        //                }

        //                Cursor.Current = Cursors.WaitCursor;
        //                WebResponse response = (HttpWebResponse)request.GetResponse();
        //                StreamReader sr = new StreamReader(response.GetResponseStream());

        //                try
        //                {
        //                    var ret = sr.ReadToEnd();
        //                    var x = JsonConvert.DeserializeObject<Root>(ret);
        //                    Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set Protocolo='" + x.DATA_RESP[0].RESUL.PROT + "' , EnviadoParaCliente=getDate(), DetalheEnvioParaCliente='" + postData + "', DetalheEnvio='" + ret.ToString() + "' where IdDocumentoOcorrencia=" + dtcabec.Rows[i]["IdDocumentoOcorrencia"].ToString() + "; Select 1", cnx); ;
        //                }
        //                catch (Exception)
        //                {
        //                }
        //            }
        //            else //Nao envia ocorrencia de reversa/coleta 
        //            {
        //                try
        //                {
        //                    Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoOcorrencia set EnviadoParaCliente=getDate(), DetalheEnvioParaCliente='Ocorrencia Intermediaria de coleta' where IdDocumentoOcorrencia=" + dtcabec.Rows[i]["IdDocumentoOcorrencia"].ToString() + "; Select 1", cnx); ;

        //                }
        //                catch (Exception)
        //                {

        //                    throw;
        //                }

        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        private void button15_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //EnviarTesteSemBanco();
            //timer1.Enabled = false;







            string sql = "select Protocolo from ProtocoloComprovei with (nolock) where  dataconclusao is null and DataSolicitacao >='2019-10-21' order by DataSolicitacao";
       DataTable dtDest = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];


            for (int i = 0; i < dtDest.Rows.Count; i++)
            {
                ConsultarProtocoloNaMao(false, dtDest.Rows[i][0].ToString());
            }




            MessageBox.Show("Test");

        }

        private void button16_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //TransferirNotasDaHomeRefill("18268905", "85694", "1", "2018/03");

        }

        //private void TransferirNotasDaHomeRefill(string idDocumento, string numero, string serie, string anomes)
        //{
        //	//return;
        //	string sql = "";
        //	try
        //	{
        //		sql = " select * from Documento d with (nolock)  " +
        //		"Inner join DocumentoEletronico de  with(nolock) on de.IdDocumento = d.IDDocumento  " +
        //		"Inner join Cadastro  dest with(nolock) on dest.IdCadastro = d.IdDestinatario " +
        //		"Where IDCliente = 150000  "+				
        //		"and DataDeEmissao >= '2018-03-26' " +
        //		" and cstatus = 100  " +
        //		"and EnviadoHomeRefill is null " +
        //		"and d.TipoDeDocumento ='nota fiscal' " +
        //		"and d.EntradaSaida='SAIDA' ";


        //		DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, "Data Source=192.168.10.20;Initial Catalog=homerefill;User ID=sa;Password=@oncetsis05083#;").Tables[0];

        //		for (int i = 0; i < dtGeral.Rows.Count; i++)
        //		{
        //			string IdDest = "";
        //			//verifica se a nota ja esta na base da logos
        //			if(int.Parse(dtGeral.Rows[i]["IdDocumento"].ToString())> 50000000)
        //				sql = "Select * from Documento Where IdDocumento="+ dtGeral.Rows[i]["IdDocumento"].ToString() + "  and TipodeDocumento='NOTA FISCAL' AND IDFILIAL=43";
        //			else
        //				sql = "Select * from Documento Where IdCliente=150000 and Numero='" + dtGeral.Rows[i]["Numero"].ToString() + "' and Serie='" + dtGeral.Rows[i]["Serie"].ToString() + "'  AND ANOMES ='" + dtGeral.Rows[i]["ANOMES"].ToString() + "' and TipodeDocumento='NOTA FISCAL' AND IDFILIAL=43";
        //			DataTable dtexiste = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

        //			if (dtexiste.Rows.Count == 0)
        //			{
        //				//Dados do Destinatario
        //				sql = "Select * from Cadastro with (nolock) where CnpjCPF ='" + dtGeral.Rows[i]["cnpjcpf"].ToString().Trim() + "'";
        //				DataTable dtDestOri = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

        //				if (dtDestOri.Rows.Count > 0)
        //					IdDest = dtDestOri.Rows[0]["IdCadastro"].ToString();
        //				else
        //				{							

        //					IdDest =Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //					sql = "insert into Cadastro(IDCadastro, CnpjCpf, InscricaoRG, RazaoSocialNome, FantasiaApelido, Endereco, Numero, Complemento, IDCidade, IDBairro, Cep) " +
        //						"Values("+IdDest+", '"+ dtGeral.Rows[i]["CNPJCPF"].ToString() + "', 'ISENTO', '"+ dtGeral.Rows[i]["RAZAOSOCIALNOME"].ToString() + "', '" + dtGeral.Rows[i]["FantasiaApelido"].ToString() + "','"+ dtGeral.Rows[i]["Endereco"].ToString() + "', '"+ dtGeral.Rows[i]["NUMERO"].ToString() + "', '"+ dtGeral.Rows[i]["COMPLEMENTO"].ToString() + "', "+ dtGeral.Rows[i]["IDCidade"].ToString() + ", 0 , '"+ dtGeral.Rows[i]["cep"].ToString() + "'); select 1";

        //					try
        //					{
        //						DataTable dtDest = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
        //					}
        //					catch (Exception cc)
        //					{ }

        //				}


        //				sql = "Insert Into Documento (";
        //				sql += "IDDocumento, ";
        //				sql += "IDFilial, ";
        //				sql += "IDFilialAtual, ";
        //				sql += "IDProprietarioDocumento, ";
        //				sql += "TipoDeDocumento, ";
        //				sql += "TipoDeServico, ";
        //				sql += "Serie, ";
        //				sql += "Numero, ";
        //				sql += "AnoMes, ";
        //				sql += "NumeroOriginal, ";
        //				sql += "IDModal, ";
        //				sql += "IDCliente, ";
        //				sql += "IDRemetente, ";
        //				sql += "IDDestinatario, ";
        //				sql += "IDConsignatario, ";
        //				sql += "IDUsuario, ";
        //				sql += "IDTes, ";
        //				sql += "IDTesCFOP, ";
        //				sql += "ClasseCFOP, ";
        //				sql += "Origem, ";
        //				sql += "EntradaSaida, ";
        //				sql += "DataDoMovimento, ";
        //				sql += "DataDeEmissao, ";
        //				sql += "DataDeEntrada, ";
        //				sql += "DataPlanejada, ";
        //				sql += "PesoLiquido, ";
        //				sql += "PesoBruto, ";
        //				sql += "PesoCubado, ";
        //				sql += "Volumes, ";
        //				sql += "CifFob, ";
        //				sql += "Impresso, ";
        //				sql += "ValorDaNota, ";
        //				sql += "ValorDasMercadorias, ";
        //				sql += "ValorDoICMS, ";
        //				sql += "BaseDoICMS, ";
        //				sql += "Endereco, ";
        //				sql += "EnderecoNumero, ";
        //				sql += "EnderecoComplemento, ";
        //				sql += "IDEnderecoBairro, ";
        //				sql += "IDEnderecoCidade, ";
        //				sql += "EnderecoCep, ";
        //				sql += "Ativo, ";
        //				sql += "DocumentoDoCliente, ";
        //				sql += "DocumentoDoCliente4, ";
        //				sql += "DocumentoDoClienteSerie, ";
        //				sql += "PagarReceber, ";
        //				sql += "CodigoDeBarrasRecExp, ";
        //				sql += "CodigoDoRecExpImpresso, ";
        //				sql += "Enviado, ";
        //				sql += "IdFilialDestino, ";
        //				sql += "ValorPis, ";
        //				sql += "ValorCofins, ";
        //				sql += "TipoDeFrete, ";
        //				sql += "SituacaoDocumento, ";
        //				sql += "IdContaContabilFilial, ";
        //				sql += "SituacaoTributariaCOFINS, ";
        //				sql += "BaseCalculoPIS, ";
        //				sql += "BaseCalculoCOFINS, ";
        //				sql += "DataDeAgendamento, ";
        //				sql += "FreteConciliado, ";
        //				sql += "NumeroDocumentoEletronico, ";
        //				sql += "TPServico, ";
        //				sql += "vICMSUFRemet, ";
        //				sql += "PeriodoDeEntregaInicio, ";
        //				sql += "PeriodoDeEntregaFim, ";
        //				sql += "DataRecebimentoDoCanhoto, ";
        //				sql += "DataHoraEntradaNota	)";


        //				string iddocNF = dtGeral.Rows[i]["IdDocumento"].ToString();// Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //				sql += " Values (" + iddocNF + ", ";


        //				sql += "'" + dtGeral.Rows[i]["IDFilial"].ToString() + "', ";
        //				sql += "59, ";
        //				sql += "'" + dtGeral.Rows[i]["IDProprietarioDocumento"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["TipoDeDocumento"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["TipoDeServico"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["Serie"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["Numero"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["AnoMes"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["NumeroOriginal"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IDModal"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IDCliente"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IDRemetente"].ToString() + "', ";
        //				sql += "'" + IdDest + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IDConsignatario"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IDUsuario"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IDTes"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IDTesCFOP"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["ClasseCFOP"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["Origem"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["EntradaSaida"].ToString() + "', ";
        //				sql += (dtGeral.Rows[i]["DataDoMovimento"].ToString() == "" ? "NULL" : "'" + DateTime.Parse(dtGeral.Rows[i]["DataDoMovimento"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";

        //				sql += (dtGeral.Rows[i]["DataDeEmissao"].ToString() == "" ? "NULL" : "'" + DateTime.Parse(dtGeral.Rows[i]["DataDeEmissao"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //				sql += (dtGeral.Rows[i]["DataDeEntrada"].ToString() == "" ? "NULL" : "'" + DateTime.Parse(dtGeral.Rows[i]["DataDeEntrada"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //				sql += (dtGeral.Rows[i]["DataPlanejada"].ToString() == "" ? "NULL" : "'" + DateTime.Parse(dtGeral.Rows[i]["DataPlanejada"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";


        //				sql += "'" + dtGeral.Rows[i]["PesoLiquido"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["PesoBruto"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["PesoCubado"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["Volumes"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["CifFob"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["Impresso"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["ValorDaNota"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["ValorDasMercadorias"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["ValorDoICMS"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["BaseDoICMS"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["Endereco"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["EnderecoNumero"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["EnderecoComplemento"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IDEnderecoBairro"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IDEnderecoCidade"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["EnderecoCep"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["Ativo"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["DocumentoDoCliente"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IdNota"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["DocumentoDoClienteSerie"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["PagarReceber"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["CodigoDeBarrasRecExp"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["CodigoDoRecExpImpresso"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["Enviado"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IdFilialDestino"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["ValorPis"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["ValorCofins"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["TipoDeFrete"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["SituacaoDocumento"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["IdContaContabilFilial"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["SituacaoTributariaCOFINS"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["BaseCalculoPIS"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["BaseCalculoCOFINS"].ToString().Replace(",", ".") + "', ";
        //				sql += "'" + dtGeral.Rows[i]["DataDeAgendamento"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["FreteConciliado"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["NumeroDocumentoEletronico"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["TPServico"].ToString() + "', ";
        //				sql += "'" + dtGeral.Rows[i]["vICMSUFRemet"].ToString().Replace(",", ".") + "', ";

        //				sql += (dtGeral.Rows[i]["PeriodoDeEntregaInicio"].ToString() == "" ? "NULL" : "'" + DateTime.Parse(dtGeral.Rows[i]["PeriodoDeEntregaInicio"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //				sql += (dtGeral.Rows[i]["PeriodoDeEntregaFim"].ToString() == "" ? "NULL" : "'" + DateTime.Parse(dtGeral.Rows[i]["PeriodoDeEntregaFim"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //				sql += (dtGeral.Rows[i]["DataRecebimentoDoCanhoto"].ToString() == "" ? "NULL" : "'" + DateTime.Parse(dtGeral.Rows[i]["DataRecebimentoDoCanhoto"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //				sql += (dtGeral.Rows[i]["DataHoraEntradaNota"].ToString() == "" ? "NULL" : "'" + DateTime.Parse(dtGeral.Rows[i]["DataHoraEntradaNota"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'");

        //				sql += "); "; //nota fiscal


        //				//documento Filial Nota Fiscal
        //				sql += "Insert into DocumentoFilial(IdDocumentoFilial, IdDocumento, IdFilial, Situacao, IDRegiaoItem) values (" + Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOFILIAL", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", " + iddocNF + ", 59, 'AGUARDANDO EMBARQUE', 51 );";

        //				try
        //				{
        //					try
        //					{
        //						Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //					}
        //					catch (Exception xx) {
        //					}
        //						sql = "Update Documento set EnviadoHomeRefill=getdate() where IdDocumento = " + dtGeral.Rows[i]["IdDocumento"].ToString() + "; select 1";
        //					DataTable s = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, "Data Source=192.168.10.20;Initial Catalog=homerefill;User ID=sa;Password=@oncetsis05083#;").Tables[0];
        //				}
        //				catch (Exception EXX)
        //				{ }

        //			}
        //			else
        //			{

        //				sql = "Select * from DocumentoFilial where IdDocumento=" + dtexiste.Rows[0]["IdDocumento"].ToString();
        //				DataTable s = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

        //				if (s.Rows.Count == 0)
        //				{
        //					sql += "Insert into DocumentoFilial(IdDocumentoFilial, IdDocumento, IdFilial, Situacao, IDRegiaoItem) values (" + Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOFILIAL", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", " + dtexiste.Rows[0]["IdDocumento"].ToString() + ", 59, 'AGUARDANDO EMBARQUE', 51 ); Select 1";
        //					s = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
        //				}


        //				sql = "Update Documento set EnviadoHomeRefill=getdate() where IdDocumento = " + dtGeral.Rows[i]["IdDocumento"].ToString() + "; select 1";
        //				s = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, "Data Source=192.168.10.20;Initial Catalog=homerefill;User ID=sa;Password=@oncetsis05083#;").Tables[0];
        //			}
        //		}
        //	}
        //	catch (Exception d)
        //	{
        //		try
        //		{
        //			Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Notas Home Refil ", d.Message + " | " + sql, "mail.grupologos.com.br", "logos0902", "Erro no Timer");
        //		}
        //		catch (Exception)
        //		{ }
        //	}
        //}

        private void button17_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
           
            ConsultarProtocoloNaMao(false, "2022090663175ee436f4");
            
            //DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin("SELECT * FROM ProtocoloComprovei WHERE ProcessadoSistran IS NULL  AND DataSolicitacao>='21/JUN/2019' AND XmlProtocolo NOT LIKE '%SEM OCO%'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            //for (int i = 0; i < dtNota.Rows.Count; i++)
            //{

            //    XmlDocument xxxx = new XmlDocument();

            //    xxxx.LoadXml(dtNota.Rows[i]["XMLPROTOCOLO"].ToString());

            //    var pp = xxxx.GetElementsByTagName("protocolo").Item(0).InnerText;


            //    ConsultarProtocoloNaMao(false, pp);
            //}


            ////EnviarComprovei_HOMEREFILL("antiga");
            ////TransferirPedidosHomeRefill();
            ////AcertarNotasNoStistranetDoComprovei();
            ///



            //MessageBox.Show("Finalizou.");
        }


        //public void TransferirPedidosHomeRefill()
        //{
        //	string sql = "";
        //	try
        //	{
        //		string sped = "Select nf.IDDocumento idNF, ped.* , c.CnpjCpf from Documento nf " +
        //		"Inner join DocumentoRelacionado dr on dr.IdDocumentoFilho = nf.IDDocumento " +
        //		"Inner join Documento ped on ped.IDDocumento = dr.IdDocumentoPai and ped.TipoDeDocumento = 'pedido' and ped.IDDocumento >= 50000000 " +
        //"inner join Cadastro c on c.IDCadastro = ped.IDDestinatario "+			
        //		"where nf.tipodedocumento = 'Nota Fiscal' " +
        //		"and nf.Iddocumento >= 50000000 " +
        //		"and ped.EnviadoHomeRefill is null " +
        //		"and nf.EnviadoHomeRefill is not null and nf.EntradaSaida='SAIDA' and  nf.DataDeEmissao >='2018-03-31'";

        //		DataTable r = Sistran.Library.GetDataTables.RetornarDataSetWS(sped, "Data Source=192.168.10.20;Initial Catalog=homerefill;User ID=sa;Password=@oncetsis05083#;").Tables[0];


        //		string iddocPed = "";
        //		for (int i = 0; i < r.Rows.Count; i++)
        //		{


        //			#region Maior que 50000000
        //			sql = "Insert Into Documento (";
        //			sql += "IDDocumento, ";
        //			sql += "IDFilial, ";
        //			sql += "IDFilialAtual, ";
        //			//sql += "IDProprietarioDocumento, ";
        //			sql += "TipoDeDocumento, ";
        //			sql += "TipoDeServico, ";
        //			sql += "Serie, ";
        //			sql += "Numero, ";
        //			sql += "AnoMes, ";
        //			sql += "NumeroOriginal, ";
        //			//sql += "IDModal, ";
        //			sql += "IDCliente, ";
        //			sql += "IDRemetente, ";
        //			sql += "IDDestinatario, ";
        //			//sql += "IDConsignatario, ";
        //			sql += "IDUsuario, ";
        //			sql += "IDTes, ";
        //			sql += "IDTesCFOP, ";
        //			sql += "ClasseCFOP, ";
        //			sql += "Origem, ";
        //			sql += "EntradaSaida, ";
        //			sql += "DataDoMovimento, ";
        //			sql += "DataDeEmissao, ";
        //			sql += "DataDeEntrada, ";
        //			sql += "DataPlanejada, ";
        //			sql += "PesoLiquido, ";
        //			sql += "PesoBruto, ";
        //			sql += "PesoCubado, ";
        //			sql += "Volumes, ";
        //			sql += "CifFob, ";
        //			sql += "Impresso, ";
        //			sql += "ValorDaNota, ";
        //			sql += "ValorDasMercadorias, ";
        //			sql += "ValorDoICMS, ";
        //			sql += "BaseDoICMS, ";
        //			sql += "Endereco, ";
        //			sql += "EnderecoNumero, ";
        //			sql += "EnderecoComplemento, ";
        //			sql += "IDEnderecoBairro, ";
        //			sql += "IDEnderecoCidade, ";
        //			sql += "EnderecoCep, ";
        //			sql += "Ativo, ";
        //			sql += "DocumentoDoCliente, ";
        //			sql += "DocumentoDoCliente4, ";
        //			sql += "DocumentoDoClienteSerie, ";
        //			sql += "PagarReceber, ";
        //			sql += "CodigoDeBarrasRecExp, ";
        //			sql += "CodigoDoRecExpImpresso, ";
        //			sql += "Enviado, ";
        //			sql += "IdFilialDestino, ";
        //			sql += "ValorPis, ";
        //			sql += "ValorCofins, ";
        //			sql += "TipoDeFrete, ";
        //			sql += "SituacaoDocumento, ";
        //			//sql += "IdContaContabilFilial, ";
        //			sql += "SituacaoTributariaCOFINS, ";
        //			sql += "BaseCalculoPIS, ";
        //			sql += "BaseCalculoCOFINS, ";
        //			sql += "DataDeAgendamento, ";
        //		//	sql += "FreteConciliado, ";
        //			sql += "NumeroDocumentoEletronico, ";
        //			//sql += "TPServico, ";
        //			//sql += "vICMSUFRemet, ";
        //			sql += "PeriodoDeEntregaInicio, ";
        //			sql += "PeriodoDeEntregaFim, ";
        //			//sql += "DataRecebimentoDoCanhoto, ";
        //			sql += "DataHoraEntradaNota	)";

        //			iddocPed = r.Rows[i]["IDDocumento"].ToString();


        //			sql += " Values (" + iddocPed + ", ";


        //			sql += "'" + r.Rows[i]["IDFilial"].ToString() + "', ";
        //			sql += "43, ";
        //		//	sql += "'" + r.Rows[i]["IDProprietarioDocumento"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["TipoDeDocumento"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["TipoDeServico"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["Serie"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["Numero"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["AnoMes"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["NumeroOriginal"].ToString() + "', ";
        //			//sql += "'" + r.Rows[i]["IDModal"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["IDCliente"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["IDRemetente"].ToString() + "', ";

        //			string sql1 = "Select * from Cadastro with (nolock) where CnpjCPF ='" + r.Rows[i]["cnpjcpf"].ToString() + "'";
        //			DataTable dtDestOri = Sistran.Library.GetDataTables.RetornarDataSetWS(sql1, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];


        //			sql += "'" + dtDestOri.Rows[0]["IdCadastro"].ToString() + "', ";


        //			//sql += "'" + r.Rows[i]["IDConsignatario"].ToString() + "', ";
        //			sql += "2, ";
        //			sql += "'" + r.Rows[i]["IDTes"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["IDTesCFOP"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["ClasseCFOP"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["Origem"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["EntradaSaida"].ToString() + "', ";
        //			sql += ( r.Rows[i]["DataDoMovimento"].ToString() == "" ? "NULL" : "'" + DateTime.Parse( r.Rows[i]["DataDoMovimento"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";

        //			sql += ( r.Rows[i]["DataDeEmissao"].ToString() == "" ? "NULL" : "'" + DateTime.Parse( r.Rows[i]["DataDeEmissao"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //			sql += ( r.Rows[i]["DataDeEntrada"].ToString() == "" ? "NULL" : "'" + DateTime.Parse( r.Rows[i]["DataDeEntrada"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //			sql += ( r.Rows[i]["DataPlanejada"].ToString() == "" ? "NULL" : "'" + DateTime.Parse( r.Rows[i]["DataPlanejada"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";


        //			sql += "0, ";
        //			sql += "0, ";
        //			sql += "0, ";
        //			sql += "'" + r.Rows[i]["Volumes"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["CifFob"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["Impresso"].ToString() + "', ";
        //			sql +=  (r.Rows[i]["ValorDaNota"].ToString().Replace(",", ".")==""? "0" : r.Rows[i]["ValorDaNota"].ToString().Replace(",", ".")) + ", ";
        //			sql +=  (r.Rows[i]["ValorDaNota"].ToString().Replace(",", ".")==""? "0" : r.Rows[i]["ValorDaNota"].ToString().Replace(",", ".")) + ", ";

        //			sql += "0, ";
        //			sql += "0, ";
        //			sql += "'" + r.Rows[i]["Endereco"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["EnderecoNumero"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["EnderecoComplemento"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["IDEnderecoBairro"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["IDEnderecoCidade"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["EnderecoCep"].ToString() + "', ";
        //			sql += "'NAO', ";
        //			sql += "'" + r.Rows[i]["DocumentoDoCliente"].ToString() + "', ";

        //			sql += "null, ";

        //			sql += "'" + r.Rows[i]["DocumentoDoClienteSerie"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["PagarReceber"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["CodigoDeBarrasRecExp"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["CodigoDoRecExpImpresso"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["Enviado"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["IdFilialDestino"].ToString() + "', ";

        //			sql += "null, ";
        //			sql += "null, ";
        //			sql += "'" + r.Rows[i]["TipoDeFrete"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["SituacaoDocumento"].ToString() + "', ";
        //			//sql += "'" + r.Rows[i]["IdContaContabilFilial"].ToString() + "', ";
        //			sql += "null, ";
        //			sql += "0, ";
        //			sql += "0, ";
        //			sql += "'" + r.Rows[i]["DataDeAgendamento"].ToString() + "', ";
        //			//sql += "'" + r.Rows[i]["FreteConciliado"].ToString() + "', ";
        //			sql += "'" + r.Rows[i]["NumeroDocumentoEletronico"].ToString() + "', ";
        //			//sql += "'" + r.Rows[i]["TPServico"].ToString() + "', ";
        //			//sql += "'" + r.Rows[i]["vICMSUFRemet"].ToString().Replace(",", ".") + "', ";

        //			sql += ( r.Rows[i]["PeriodoDeEntregaInicio"].ToString() == "" ? "NULL" : "'" + DateTime.Parse( r.Rows[i]["PeriodoDeEntregaInicio"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //			sql += ( r.Rows[i]["PeriodoDeEntregaFim"].ToString() == "" ? "NULL" : "'" + DateTime.Parse( r.Rows[i]["PeriodoDeEntregaFim"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //			//sql += ( r.Rows[i]["DataRecebimentoDoCanhoto"].ToString() == "" ? "NULL" : "'" + DateTime.Parse( r.Rows[i]["DataRecebimentoDoCanhoto"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'") + ", ";
        //			sql += ( r.Rows[i]["DataHoraEntradaNota"].ToString() == "" ? "NULL" : "'" + DateTime.Parse( r.Rows[i]["DataHoraEntradaNota"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'");

        //			sql += "); ";

        //			#region DocumentoRelacionado

        //				string idDocRel = Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoRelacionado", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //				sql += "Insert into DocumentoRelacionado(IdDocumentoRelacionado,IdDocumentoPai,IdDocumentoFilho) values (" + idDocRel + ", " + iddocPed + ", " + r.Rows[i]["idNF"].ToString() + ") ;";

        //			#endregion
        //			#endregion

        //			try
        //			{
        //				Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //				var s = Sistran.Library.GetDataTables.RetornarDataSetWS("update documento set EnviadoHomeRefill=GETDATE() where IDDocumento=" + iddocPed + " ; select 1", "Data Source=192.168.10.20;Initial Catalog=homerefill;User ID=sa;Password=@oncetsis05083#;").Tables[0];
        //			}
        //			catch (Exception xx)
        //			{

        //			}
        //		}
        //	}

        //	catch (Exception exx)
        //	{

        //	}
        //}

        private void button18_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            var client = new RestClient("http://webcorpem.no-ip.info:63574/scripts/mh.dll/wc");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
" + "\n" +
            @"  ""CORPEM_TMS_OCO"": {
" + "\n" +
            @"    ""num_cli_ws"": 14,
" + "\n" +
            @"    ""cnpj_trp_ws"": ""74395542000147"",
" + "\n" +
            @"
" + "\n" +
            @"    ""trp_oco"": [
" + "\n" +
            @"      {
" + "\n" +
            @"        ""cnpj_trp_oco"": ""74395542000147"",
" + "\n" +
            @"        ""razao_trp_oco"": ""ALFANUMERIO ATE 100 POSICOES"",
" + "\n" +
            @"        ""ocos"": [
" + "\n" +
            @"          {
" + "\n" +
            @"            ""cnpj_trp_cte"": ""74395542000147"",
" + "\n" +
            @"            ""num_cte"": 123456789,
" + "\n" +
            @"            ""sr_cte"": ""123"",
" + "\n" +
            @"            ""cnpj_emi_nfe"": ""74395542000147"",
" + "\n" +
            @"            ""num_nfe"": 123456789,
" + "\n" +
            @"            ""sr_nfe"": ""123"",
" + "\n" +
            @"            ""cod_oco"": ""1234567890"",
" + "\n" +
            @"            ""dt_oco"": ""01/01/0001 01:01"",
" + "\n" +
            @"            ""obs_oco"": ""ate 1000 caracteres""
" + "\n" +
            @"          }
" + "\n" +
            @"        ]
" + "\n" +
            @"      }
" + "\n" +
            @"    ]
" + "\n" +
            @"  }
" + "\n" +
            @"}
" + "\n" +
            @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);



        }


public int RetornarIdTabelaNovo(string tabela)
        {
            //timer1.Enabled = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()))
                using (SqlCommand cmd = new SqlCommand("GERAR_ID_TABELA", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // set up the parameters
                    cmd.Parameters.Add("@NOMEDATABELA", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@RETORNAR_ID_TABELA", SqlDbType.Int).Direction = ParameterDirection.Output;

                    // set parameter values
                    cmd.Parameters["@NOMEDATABELA"].Value = tabela;

                    // open connection and execute stored procedure
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // read output value from @NewId
                    int contractID = Convert.ToInt32(cmd.Parameters["@RETORNAR_ID_TABELA"].Value);
                    conn.Close();

                    return contractID;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        private void CriarDocumentoRelacionado()
        {

            string sql = "select distinct  orderId, invoiceEletronicKey, nf.IDDocumento IdNF, ped.IDDocumento IdPed, ped.numero ";
            sql += " from JosaparTracking JT ";
            sql += " Inner join Documento Ped on ped.Numero = orderId and ped.IDCliente = 3975526 and ped.TipoDeDocumento = 'pedido' ";
            sql += " Left join Documento Nf on nf.DocumentoDoCliente4 = invoiceEletronicKey ";
            sql += " Left Join DocumentoRelacionado DR on DR.IdDocumentoPai = Ped.IDDocumento and DR.IdDocumentoFilho = Nf.IDDocumento ";
            sql += " where invoiceEletronicKey is not null  and IdDocumentoRelacionado is null and nf.IDDocumento is not null";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

            sql = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = RetornarIdTabelaNovo("DocumentoRelacionado").ToString();// Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoRelacionado", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                sql += "Insert into DocumentoRelacionado (IdDocumentoRelacionado, IdDocumentoPai, IdDocumentoFilho) values (" + id + ", " + dt.Rows[i]["IdPed"].ToString() + ", " + dt.Rows[i]["IdNF"].ToString() + "); ";

                if (DateTime.Now >= DateTime.Parse("2018-12-11"))
                {
                    sql += " Update DocumentoFilial Set Situacao='LIBERADO PARA SEPARACAO', Data=GetDate() WHERE IdDocumento= " + dt.Rows[i]["IdNF"].ToString() + "; ";
                    sql += " Update DocumentoItem set EstoqueProcessado='NAO' Where IdDocumento=" + dt.Rows[i]["IdNF"].ToString() + " ;";
                }
            }


            if (sql.Length > 0)
            {
                var x = Sistran.Library.GetDataTables.RetornarDataSetWS(sql + "; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            }
        }

        public void GerarTrackingJosapar()
        {
            string sql = "  select do.IDDocumento, Do.Descricao, do.DataOcorrencia, DocumentodoCliente4, do.IdDocumentoOcorrencia, "+
 " (Select top 1 orderID From JosaparTracking where invoiceEletronicKey = d.DocumentodoCliente4) NrPedido "+
 " from Documento d with(nolock)  inner join DocumentoOcorrencia do with(nolock) on do.IDDocumento = d.IDDocumento " +
 " where d.IDCliente = 3975526 " +
 " and(Descricao = 'MERCADORIA EMBARCADA' OR DO.IDOcorrencia = 4868) " +
 " and TipoDeDocumento = 'NOTA FISCAL' " +
 " AND DO.EnviadoParaCliente IS NULL and DataOcorrencia >= getdate() - 2 " +
 " and(Select top 1 orderID From JosaparTracking where invoiceEletronicKey = d.DocumentodoCliente4) is not null " +
 " ORDER BY D.IDDocumento, DataOcorrencia"; 
            DataTable dts = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            for (int i = 0; i < dts.Rows.Count; i++)
            {
                string control = "";
                string controlNome = "";

                if (dts.Rows[i]["Descricao"].ToString() == "MERCADORIA EMBARCADA")
                {
                    control = "ETR";
                    controlNome = "Entrega em andamento";
                }
                if (dts.Rows[i]["Descricao"].ToString().Contains("Entrega realizada normalmente"))
                {
                    control = "ENT";
                    controlNome = "Entrega Concluida";
                }

                if (control == "")
                    continue;


               // DataTable dNumPed = Sistran.Library.GetDataTables.RetornarDataTableWS("Select top 1 OrderId from JosaparTracking where "

                sql = "insert into JosaparTracking(orderId,controlPointId,controlPointNm,occurrenceDt,Direcao, invoiceEletronicKey) values ('" + dts.Rows[i]["NrPedido"].ToString() + "','"+control+"','"+controlNome +"',getdate(),'Josapar', '"+ dts.Rows[i]["DocumentoDoCliente4"].ToString() + "'); ";
                sql += "Update DocumentoOcorrencia set EnviadoParaCliente = getdate() where IddocumentoOcorrencia= " + dts.Rows[i]["IdDocumentoOcorrencia"].ToString();
                sql += "; Update documento set TrackingEmEntrega=getdate() where IdDocumento=" + dts.Rows[i]["IdDocumento"].ToString() + " ;Select 1";
                dts = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            }
        }


        public void GerarTrackingEMEntrega()
        {
            string sql = " select distinct orderId, D.IdDocumento from Documento d with (nolock)";
            sql += " Inner join DocumentoFilial DF ON DF.IDDocumento = D.IDDocumento ";
            sql += " Inner join RomaneioDocumento rd with(nolock)on rd.IdDocumento = d.IdDocumento";
            sql += " Inner join Romaneio r with (nolock) on r.IdRomaneio = rd.IdRomaneio";
            sql += " Inner join JosaparTracking jt  with (nolock) on jt.invoiceEletronicKey = d.DocumentodoCliente4";
            sql += " Inner join dtRomaneio dtr   with (nolock) on dtr.IdRomaneio = r.IdRomaneio";
            sql += " Inner join Dt with (nolock) on dt.IdDt = dtr.IdDt";
            sql += " where d.IdCliente = 3975526";
            sql += " and d.TipoDeDocumento= 'Nota fiscal'";
            sql += " and r.Tipo= 'Entrega'";
            sql += " and DataDeConclusao is null and DF.Situacao = 'EM ENTREGA'and d.TrackingEmEntrega is null";
            DataTable dts = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            for (int i = 0; i < dts.Rows.Count; i++)
            {
                if (dts.Rows[i]["orderId"].ToString().Trim() == "0")
                    continue;

                DataTable data = Sistran.Library.GetDataTables.RetornarDataTableWS("Select orderId from JosaparTracking where orderId='"+ dts.Rows[i]["orderId"].ToString() + "' and controlPointId='ETR'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (data.Rows.Count > 0)
                    continue;

                sql = "insert into JosaparTracking(orderId,controlPointId,controlPointNm,occurrenceDt) values ('" + dts.Rows[i]["orderId"].ToString() + "','ETR','Entrega em andamento',getdate()); ";
                sql += "insert into JosaparTracking(orderId,controlPointId,controlPointNm,occurrenceDt,Direcao) values ('" + dts.Rows[i]["orderId"].ToString() + "','ETR','Entrega em andamento',getdate(),'Josapar'); ";
                sql += " Update documento set TrackingEmEntrega=getdate() where IdDocumento=" + dts.Rows[i]["IdDocumento"].ToString() + " ;Select 1";
                dts = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            }
        }

        public void GerarTrackingEntrega()
        {
            string sql = "	select distinct orderId, d.IDDocumento from Documento d  with (nolock) ";
            sql += " Inner join JosaparTracking jt  with(nolock) on jt.invoiceEletronicKey = d.DocumentodoCliente4 ";
            sql += " Inner join DocumentoOcorrencia do on do.IDDocumento = d.IDDocumento ";
            sql += "  where d.IdCliente = 3975526 and d.TipoDeDocumento = 'Nota fiscal' and DataDeConclusao is not null and d.TrackingEntrega is null and do.IDOcorrencia  in(1,117,4868,4908,5026, 5099) ";
            DataTable dts = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            for (int i = 0; i < dts.Rows.Count; i++)
            {
                if (dts.Rows[i]["orderId"].ToString().Trim() == "0")
                    continue;


                DataTable data = Sistran.Library.GetDataTables.RetornarDataTableWS("Select orderId from JosaparTracking where orderId='" + dts.Rows[i]["orderId"].ToString() + "' and controlPointId='ENT'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (data.Rows.Count > 0)
                    continue;

                sql = "insert into JosaparTracking(orderId,controlPointId,controlPointNm,occurrenceDt) values ('" + dts.Rows[i]["orderId"].ToString() + "','ENT','Entrega Concluida',getdate()); ";
                sql += "insert into JosaparTracking(orderId,controlPointId,controlPointNm,occurrenceDt,Direcao) values ('" + dts.Rows[i]["orderId"].ToString() + "','ENT','Entrega Concluida',getdate(),'Josapar'); ";
                sql += " Update documento set TrackingEntrega=getdate() where IdDocumento=" + dts.Rows[i]["IdDocumento"].ToString() + " ;  Select 1";
                dts = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            }
        }
    }

    public struct logs
    {
        public string datahora { get; set; }
        public string acao { get; set; }
        public string resposta { get; set; }
    }
}

public class ocorrenViaVarejoRet
{
    public int protocolo { get; set; }
    public int qtd_registros { get; set; }
    public string status { get; set; }
    public object erros { get; set; }
}
public class ocorrenViaVarejo
{
    public string data { get; set; }
    public int id { get; set; }
    public string cnpjFilial { get; set; }
    public string notaFiscal { get; set; }
    public string serie { get; set; }
    public string obervacao { get; set; }
}


public class MessageVia
{
    public string type { get; set; }
    public string text { get; set; }
    public string key { get; set; }
}

public class RootVia
{
    public string status { get; set; }
    public List<MessageVia> messages { get; set; }
    public string time { get; set; }
    public int client_id { get; set; }
    public int logistics_provider { get; set; }
    public object timezone { get; set; }
    public object locale { get; set; }
    public string hash { get; set; }
}




public class RESUL
{
    public long VBELN { get; set; }
    public int STATUS { get; set; }
    public string MSG_RET { get; set; }
    public string PROT { get; set; }

}

public class DATARESP
{
    public string NOME_ARQ { get; set; }
    public RESUL RESUL { get; set; }

}

public class Root
{
    public List<DATARESP> DATA_RESP { get; set; }

}

public class cNestle
{
    public int ocorrencia { get; set; }
    public string data_hora { get; set; }
    public string cnpj_remetente { get; set; }
    public string mensagem { get; set; }
    public string numero { get; set; }
    public string serie { get; set; }
    //public string data_reentrega { get; set; }
    //public string prazo_devolucao { get; set; }
   //public List<string> comprovantes { get; set; }
    //public List<string> codigos_redespacho { get; set; }
}

public class AdditionalInformation
{
    [JsonProperty("receiver_name")]
    public string ReceiverName { get; set; }

    [JsonProperty("receiver_document")]
    public string ReceiverDocument { get; set; }

    [JsonProperty("kinship")]
    public string Kinship { get; set; }
}

public class Oco
{
    public string cnpj_trp_cte { get; set; }
    public int num_cte { get; set; }
    public string sr_cte { get; set; }
    public string cnpj_emi_nfe { get; set; }
    public int num_nfe { get; set; }
    public string sr_nfe { get; set; }
    public string cod_oco { get; set; }
    public string dt_oco { get; set; }
    public string obs_oco { get; set; }
}

public class TrpOco
{
    public string cnpj_trp_oco { get; set; }
    public string razao_trp_oco { get; set; }
    public List<Oco> ocos { get; set; }
}

public class CORPEMTMSOCO
{
    public int num_cli_ws { get; set; }
    public string cnpj_trp_ws { get; set; }
    public List<TrpOco> trp_oco { get; set; }
}

public class FrioVIX
{
    public CORPEMTMSOCO CORPEM_TMS_OCO { get; set; }
}



public class FrioVIXRetornoOco
{
    public int num_protocolo { get; set; }
    public int cod_erro { get; set; }
    public string msg_erro { get; set; }
}

public class FrioVIXRetornoTrpOco
{
    public string cnpj_trp_oco { get; set; }
    public int cod_erro { get; set; }
    public string msg_erro { get; set; }
    public List<FrioVIXRetornoOco> ocos { get; set; }
}

public class FrioVIXRetornoRoot
{
    public List<FrioVIXRetornoTrpOco> trp_oco { get; set; }
}
