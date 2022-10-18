using BoletoNet;
using DbUpdateCompusoft.Sistecno.Facility.CTe30.Service.Handle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace DbUpdateCompusoft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "Aguarando o proximo periodo de importação";
                Application.DoEvents();
                timer1.Enabled = true;

            }
            catch (Exception)
            {
                timer1.Enabled = true;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Enabled = false;

            PedidoDpToDocGirotrade();

            PedidoDpToDocGirotrade();
            PedidoDpToDocGirotrade();



            for (int i = 0; i < 20; i++)
            {
                EnviarDocumentos();
            }

            ConfirmaConferenciaGiroTrade();
            gerarPedidosCanceladosYandeh();
            PedidoDpToDocGirotrade();

            try
            {

                EnviarDocumentosCancelados();
                CalcularLinhaDigitavel();
                EnviarFatura();
              //  EnviarDocumentos();
                PrepararTituloAlteracao();

            }
            catch (Exception)
            {
                if (DateTime.Now.Minute % 5 == 0)
                {
                    EnviarCC();
                    EnviarFaturaCancelada();
                    EnviarDocumentosCancelados();
                    PedidoDpToDocGirotrade();
                    ConfirmaConferenciaGiroTrade();
                    EnviarCC();
                    EnviarFatura();
                    EnviarDocumentosCancelados();
                }
            }


            try
            {
                if (DateTime.Now.Minute % 5 == 0)
                {
                    EnviarInutilizados();
                    //for (int i = 0; i < 50; i++)
                    //{
                    //    EnviarDocumentos();
                    //}
                }
            }
            catch (Exception)
            {

            }


            try
            {
                PedidoDpToDocGirotrade();
                PedidoDpToDocMaisBrasil();
                ConfirmaConferenciaMaisBrasil();
                Comprovei();
            }
            catch (Exception)
            { }

            try
            {
                try
                {
                    Comprovei();
                }
                catch (Exception)
                { }

                try
                {
                    PedidoDpToDocGirotrade();
                }
                catch (Exception)
                { }

                try
                {
                    CriarDocumentoRelacionado();
                    ConfirmaConferenciaGiroTrade();
                }
                catch (Exception)
                {
                }


                try
                {
                    PedidoDpToDocGirotrade();
                    PedidoDpToDocGirotrade();
                    ConfirmaConferenciaGiroTrade();
                }
                catch (Exception)
                {
                }


                if (DateTime.Now.Minute % 30 == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        //EnviarDocumentos();
                        //EnviarBaixaFatura();
                    }
                }


                try
                {
                    Application.DoEvents();
                    try
                    {
                        if (DateTime.Now.Hour == 1 || DateTime.Now.Hour == 2 || DateTime.Now.Hour == 7 || DateTime.Now.Hour == 14 || DateTime.Now.Hour == 19)
                        {
                            //try
                            //{
                            //    for (int i = 0; i < 10; i++)
                            //    {
                            //        EnviarDocumentos();
                            //        //EnviarBaixaFatura();

                            //    }
                            //}
                            //catch (Exception)
                            //{
                            //}


                            try
                            {
                                EnviarCC();
                                EnviarRPCI();

                                if (DateTime.Now.Minute % 5 == 0)
                                {
                                    for (int i = 0; i < 100; i++)
                                    {
                                        EnviarFatura();
                                    }
                                }


                                //for (int i = 0; i < 10; i++)
                                //{
                                //    try
                                //    {
                                //        EnviarDocumentos();
                                //    }
                                //    catch (Exception)
                                //    {
                                //    }
                                //}

                                EnviarFaturaCancelada();
                            }
                            catch (Exception)
                            {
                            }
                        }


                        if (DateTime.Now.Hour == 23)
                        {
                            try
                            {
                                EnviarDocumentosCancelados();
                                EnviarDocumentosCancelados();
                            }
                            catch (Exception)
                            {
                            }
                        }

                    }
                    catch (Exception)
                    {


                    }
                    textBox1.Text = "Checando....";
                    Application.DoEvents();

                    try
                    {
                        Comprovei();
                        PedidoDpToDocGirotrade();
                        CriarDocumentoRelacionado();
                    }
                    catch (Exception)
                    {
                    }


                }
                catch (Exception)
                {
                }
                finally
                {
                    timer1.Enabled = true;
                    textBox1.Text = "Aguarando o proximo periodo de importação";
                    Application.DoEvents();
                }
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }
        }

        private void PrepararTituloAlteracao()
        {
            string strsql = "select Distinct IdTitulo  from  TituloAlteracaoLog where Processado is null ";
            DataSet ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            string sql = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sql += "Update Titulo set EnviadoCs=null where IdTitulo=" + ds.Tables[0].Rows[i]["IdTitulo"].ToString() +
                    " and EnviadoCS<> 'NFS' ; Update TituloAlteracaoLog set Processado=getdate() where IdTitulo=" + ds.Tables[0].Rows[i]["IdTitulo"].ToString() + ";";
            }

            if (sql.Length > 10)
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        }

        private void CalcularLinhaDigitavel()
        {
            try
            {
                string strsql = "EXEC GerarLinhaDigitavelBoleto";
                DataSet ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                for (int ix = 0; ix < ds.Tables[0].Rows.Count; ix++)
                {

                    try
                    {


                        Cedente c = new Cedente(ds.Tables[0].Rows[ix]["CNPJCPFCEDENTE"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim(), ds.Tables[0].Rows[ix]["CEDENTE"].ToString(), ds.Tables[0].Rows[ix]["AGENCIA"].ToString(), ds.Tables[0].Rows[ix]["CONTA"].ToString(), ds.Tables[0].Rows[ix]["CONTADIGITO"].ToString());
                        c.Codigo = int.Parse(ds.Tables[0].Rows[ix]["CARTEIRA"].ToString());


                        decimal valorTotal = decimal.Parse(ds.Tables[0].Rows[ix]["VALOR"].ToString());
                        if (DateTime.Now > DateTime.Parse(ds.Tables[0].Rows[ix]["DATADEVENCIMENTO"].ToString()))
                        {
                            TimeSpan ts = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(ds.Tables[0].Rows[ix]["DATADEVENCIMENTO"].ToString());
                            decimal juros = decimal.Parse(ds.Tables[0].Rows[ix]["JUROS"].ToString());
                            decimal jurosDiario = decimal.Parse(ds.Tables[0].Rows[ix]["JurosDiario"].ToString());
                            decimal multa = decimal.Parse(ds.Tables[0].Rows[ix]["Multa"].ToString());

                            valorTotal += multa + juros;
                            valorTotal += (jurosDiario * ts.Days);
                        }

                        Boleto b = new Boleto(DateTime.Parse(DateTime.Parse(ds.Tables[0].Rows[ix]["DATADEVENCIMENTO"].ToString()).ToShortDateString()), valorTotal, ds.Tables[0].Rows[ix]["CARTEIRA"].ToString(), ds.Tables[0].Rows[ix]["NOSSONUMERO"].ToString(), c);

                        b.NumeroDocumento = ds.Tables[0].Rows[ix]["NUMERODOCUMENTO"].ToString();
                        b.Sacado = new Sacado(ds.Tables[0].Rows[ix]["CNPJSACADO"].ToString(), ds.Tables[0].Rows[ix]["SACADO"].ToString());
                        b.Sacado.Endereco.End = ds.Tables[0].Rows[ix]["ENDERECO"].ToString() + ", " + ds.Tables[0].Rows[ix]["NUMERO"].ToString();
                        b.Sacado.Endereco.Bairro = "";
                        b.Sacado.Endereco.Cidade = ds.Tables[0].Rows[ix]["CIDADE"].ToString();
                        b.Sacado.Endereco.CEP = ds.Tables[0].Rows[ix]["CEP"].ToString();
                        b.Sacado.Endereco.UF = ds.Tables[0].Rows[ix]["UF"].ToString();



                        Instrucao_Bradesco i = new Instrucao_Bradesco();
                        i.Descricao = ds.Tables[0].Rows[ix]["DESCRICAO"].ToString();
                        b.Instrucoes.Add(i);

                        b.EspecieDocumento = new EspecieDocumento(237);
                        BoletoBancario bb = new BoletoBancario();
                        bb.CodigoBanco = 237;
                        bb.Boleto = b;
                        bb.MostrarCodigoCarteira = true;
                        bb.Boleto.Valida();
                        bb.MostrarComprovanteEntrega = false;


                        if (b.CodigoBarra.LinhaDigitavel != null && b.CodigoBarra.LinhaDigitavel != "")
                        {
                            string ss = "Update TituloDuplicata set LinhaDigitavel='" + b.CodigoBarra.LinhaDigitavel.Replace(" ", "").Replace(".", "") + "' where IdTitulo ='" + ds.Tables[0].Rows[ix]["IdTitulo"].ToString() + "'";
                            Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(ss, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        }

                    }
                    catch (Exception vv)
                    {
                        string ss = "Update TituloDuplicata set LinhaDigitavel='.' where IdTitulo ='" + ds.Tables[0].Rows[ix]["IdTitulo"].ToString() + "'";
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(ss, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                }
            }
            catch (Exception v)
            {
            }
        }

        private void ConfirmaConferenciaGiroTrade()
        {
            try
            {

                //força o faturamento
                string sql = "prc_Libera_Faturamento_yandeh_v2";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                
                sql = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string sql2 = "prc_Confere_yandeh_Itens " + dt.Rows[i]["IdDocumento"].ToString();
                    DataTable dtItens = Sistran.Library.GetDataTables.RetornarDataTableWin(sql2, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    textBox1.Text = "Liberando Pedido: " + dt.Rows[i]["IdDocumento"].ToString();
                    Application.DoEvents();

                    bool PedidoOK = true;

              


                    //verifica se existe Itens diferentes entre documentoItem e Movimentacao
                    for (int ii = 0; ii < dtItens.Rows.Count; ii++)
                    {

                        if (int.Parse( dtItens.Rows[ii]["qtdPedido"].ToString()) > int.Parse(  dtItens.Rows[ii]["QtdMov"].ToString()))
                        {

                            dtItens.Rows[ii]["qtdPedido"] = dtItens.Rows[ii]["QtdMov"].ToString();
                        }

                        if (dtItens.Rows[ii]["QtdMov"].ToString() != dtItens.Rows[ii]["qtdPedido"].ToString())
                        {                 
                            
                            PedidoOK = false;
                            sql = "Update documento Set EnviadoCS='Dirv. Itens', IntegradoCliente= getdate() where Iddocumento =" + dt.Rows[i]["IdDocumento"].ToString();
                            Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                            break;
                        }
                       

                    }

                    if (PedidoOK)
                    {

                        Thread.Sleep(100);

                        sql = "Declare @idStatus as int ";
                        sql += "; Update DocumentoFilial set Situacao='LIBERADO PARA FATURAMENTO' WHERE IDDocumento=" + dt.Rows[i]["IdDocumento"].ToString();
                        sql += "; update Documento set IntegradoCliente= getdate() where IdDocumento =" + dt.Rows[i]["IdDocumento"].ToString() + " ; Update MovimentacaoItem set GeradoYadeh=getdate() where GeradoYadeh is null and IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString();

                        sql += "; insert into YandehStatusPedido (NrPedido, Status, Consumido,DataHora,Sequencia,IdDocumento,Serie) Values ('"+ dt.Rows[i]["Numero"].ToString() + "', 'LIBERADO PARA FATURAMENTO', null,getdate(),0,"+ dt.Rows[i]["IdDocumento"].ToString() + ",'"+ dt.Rows[i]["Serie"].ToString() + "');  select @idStatus = SCOPE_IDENTITY()  ";

                        for (int ii = 0; ii < dtItens.Rows.Count; ii++)
                        {
                            sql += "; Insert into YandehStatusPedidoItem (IdStatusPedido,CodigoDoProduto,Quantidade) values ((Select @idStatus),'" + dtItens.Rows[ii]["Codigo"].ToString() + "', "+ dtItens.Rows[ii]["qtdPedido"].ToString() + ")";

                        }


                        try
                        {
                            Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        }
                        catch (Exception)
                        {
                            try
                            {
                                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

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
            }
        }


        public void gerarPedidosCanceladosYandeh()
        {
            string sql = "";
            try
            {
                //gera cancelados

                #region Cancelados

                sql = @"Select distinct  d.IDDocumento, d.Numero, d.Serie
                        from Documento d
                        Inner
                        join DOCUMENTOPEDIDO dp on dp.DocumentodoCliente4 = d.IDDocumento
                        inner Join DOCUMENTOPEDIDOITEM di on di.IDDocumento = dp.IDDocumento
                        Inner join ProdutoCliente pc on pc.IDProdutoCliente = di.IdProdutoCliente
                        where d.TipoDeDocumento = 'pedido'
                        and d.IDCliente = 114091
                        and d.DataDeCancelamento >= '2022-02-01'
                        and d.EnviadoCS is null
                        order by 2";


                DataTable dtc = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                for (int i = 0; i < dtc.Rows.Count; i++)
                {
                    sql = @"Select d.Numero,  di.IdProdutoCliente, pc.Codigo
                            from Documento d
                            Inner join DOCUMENTOPEDIDO dp on dp.DocumentodoCliente4 = d.IDDocumento
                            inner Join DOCUMENTOPEDIDOITEM di on di.IDDocumento = dp.IDDocumento
                            Inner join ProdutoCliente pc on pc.IDProdutoCliente = di.IdProdutoCliente
                            where d.TipoDeDocumento = 'pedido'
                            and d.IDCliente = 114091
                            and d.DataDeCancelamento >='2022-02-01'
                            and d.EnviadoCS is null
                            and d.IDDocumento =" + dtc.Rows[i]["IdDocumento"].ToString();

                    DataTable dtc1 = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    string cmd = "";
                    cmd = "Update DocumentoFilial set Situacao='Documento Cancelado' where IdDocumento=" + dtc.Rows[i]["IdDocumento"].ToString();
                    cmd += "; Insert  into YandehStatusPedido (NrPedido,Status,Consumido,DataHora,Sequencia,IdDocumento,Serie)  values ('" + dtc.Rows[i]["Numero"].ToString() + "','PEDIDO CANCELADO',getDate(),getdate(),0," + dtc.Rows[i]["IdDocumento"].ToString() + ",'" + dtc.Rows[i]["Serie"].ToString() + "'); ​select scope_identity() ";


                    try
                    {
                        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                        DbConnection cn = factory.CreateConnection();
                        DbCommand cd = factory.CreateCommand();
                        DbDataAdapter da = factory.CreateDataAdapter();
                        cn.ConnectionString = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                        DbTransaction oTrans;
                        cd.CommandText = cmd;
                        cd.Connection = cn;
                        cd.CommandTimeout = 999999999;
                        cn.Open();

                        oTrans = cn.BeginTransaction();
                        cd.Transaction = oTrans;
                        try
                        {
                            var id = cd.ExecuteScalar();

                            for (int ii = 0; ii < dtc1.Rows.Count; ii++)
                            {
                                string x = "insert into YandehStatusPedidoItem(IdStatusPedido,CodigoDoProduto,Quantidade) values (" + id + ",'" + dtc1.Rows[ii]["Codigo"].ToString() + "',0);";
                                cd.CommandText = x;
                                cd.ExecuteNonQuery();
                            }


                            if (dtc1.Rows.Count > 0)
                            {
                                string x = "Update Documento set EnviadoCs='Enviado:(Cancel): " + DateTime.Now.ToString("dd/MM/yyyy") + "' where IdDocumento= " + dtc.Rows[i]["IdDocumento"].ToString();
                                cd.CommandText = x;
                                cd.ExecuteNonQuery();
                            }

                        }
                        catch (Exception xxx)
                        {

                            oTrans.Rollback();
                        }


                        oTrans.Commit();

                    }
                    catch (Exception eeee)
                    {


                    }



                    //for (int ii = 0; ii < dtc1.Rows.Count; ii++)
                    //{

                    //}

                    //if(cmd.Length>10)
                    //{
                    //    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(cmd, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    //}

                }



                #endregion
            }
            catch (Exception)
            {


            }


        }

        private void ConfirmaConferenciaMaisBrasil()
        {
            try
            {
                string sql = "prc_Libera_Faturamento_MaisBrasil";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                sql = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = " update DocumentoFilial set Situacao='LIBERADO PARA FATURAMENTO' WHERE IDDocumento=" + dt.Rows[i]["IdDocumento"].ToString();
                    sql += "; Update MovimentacaoItem set GeradoYadeh=getdate() where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString();
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }
            }
            catch (Exception)
            {


            }
        }

        //private void LibreraPedidoYandhParaFaturamento()
        //{
        //    try
        //    {
        //        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin("exec PRC_Yandeh_Liberar_Faturamento", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //        string sqlAux = "";
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            sqlAux += " update DocumentoFilial set Situacao='LIBERADO PARA FATURAMENTO' WHERE IDDocumento=" + dt.Rows[i]["IDDocumento"].ToString() + ";";
        //            sqlAux += " update movimentacao set InseriuYandeh=getdate() where IdMovimentacao=" + dt.Rows[i]["IdMovimentacao"].ToString() + ";";
        //        }

        //        if (sqlAux.Length > 10)
        //        {
        //            Sistran.Library.GetDataTables.RetornarDataTableWin(sqlAux + " Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //        }

        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        private void CriarDocumentoRelacionado()
        {
            try
            {


                string sql = "select * from YandehPedidoChave where Processado is null";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];


                string IdPed = "";
                string IdNf = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "Select isnull(Iddocumento, 0) from Documento where IdCliente=114091 and TipoDeDocumento='Pedido' and Numero=" + dt.Rows[i]["Numero"].ToString();
                    IdPed = Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).ToString();

                    if (IdPed == "0")
                        continue;

                    sql = "Select IdDocumento from Documento where IdCliente=114091 and TipoDeDocumento='Nota Fiscal' and DocumentoDoCliente4=" + dt.Rows[i]["Chave"].ToString();
                    IdNf = Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).ToString();

                    if (IdNf == "0")
                        continue;


                    string id = Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoRelacionado", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    sql = "Insert into DocumentoRelacionado (IdDocumentoRelacionado, IdDocumentoPai, IdDocumentoFilho) values (" + id + ", " + IdPed + ", " + IdNf + "); ";
                    sql += "Update YandehPedidoChave set Processado = getdate() where id=" + dt.Rows[i]["id"].ToString();
                    var x = Sistran.Library.GetDataTables.RetornarDataSetWS(sql + "; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                }
            }
            catch (Exception)
            {
            }
        }


        private void PedidoDpToDocGirotrade()
        {
            try
            {
                //string sql = "SELECT * FROM DocumentoPedido  where iddocumento= 227457";
                string sql = "SELECT * FROM DocumentoPedido  where DATADOPROCESSAMENTO is null and IdCliente=114091 and Iddocumento >1000";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "Select IdDocumento from DocumentoPedido where Numero='" + dt.Rows[i]["Numero"].ToString() + "' and ATIVO='SIM' and IdCliente=" + dt.Rows[i]["IdCliente"].ToString();
                    DataTable dtN = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    if (dtN.Rows.Count > 1)
                    {
                        DataTable RETX = Sistran.Library.GetDataTables.RetornarDataTableWin("update documentoPedido set DATADOPROCESSAMENTO=getDate(), erro='Pedido Duplicado'  where Numero=" + dt.Rows[i]["Numero"].ToString() + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        continue;
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "Select IdDocumento from Documento where Numero='" + dt.Rows[i]["Numero"].ToString() + "' and ATIVO='SIM' AND Serie='" + dt.Rows[i]["SERIE"].ToString() + "' and IdFilial=" + dt.Rows[i]["IDFilial"].ToString() + " and EntradaSaida='SAIDA' and IdCliente=" + dt.Rows[i]["IdCliente"].ToString();
                    var existe = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    if (existe.Rows.Count > 0)
                    {
                        DataTable RETX = Sistran.Library.GetDataTables.RetornarDataTableWin("update documentoPedido set DATADOPROCESSAMENTO=getDate() where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        continue;
                    }

                    sql = "Select * from DocumentoPedidoItem where IdDocumento=" + dt.Rows[i]["Iddocumento"].ToString();
                    DataTable dtItens = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    int idDoc = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("Documento", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));

                    string sqlEf = "Insert into Documento(SeparacaoWMS  , IdProprietarioDocumento, CodigoDoRecExpImpresso, ClasseCFOP,idmodal, IDDocumento, IDFilial, IDFilialAtual,TipoDeDocumento,TipoDeServico, Serie,Numero,AnoMes,IDCliente, IDRemetente, IDDestinatario,Origem, EntradaSaida, DataDeEmissao,  DataDeEntrada, DataPlanejada, Endereco,EnderecoNumero, EnderecoComplemento,IDEnderecoBairro, IDEnderecoCidade, EnderecoCep, Ativo, IdTes,IdTesCfop)" +
                        " values ('SIM'," + dt.Rows[i]["IDCliente"].ToString() + ", '1', '5',1, " + idDoc + ", 15, 15,'PEDIDO','NORMAL', '" + dt.Rows[i]["Serie"].ToString() + "','" + dt.Rows[i]["Numero"].ToString() + "','" + dt.Rows[i]["AnoMes"].ToString() + "','" + dt.Rows[i]["IDCliente"].ToString() + "', '" + dt.Rows[i]["IDRemetente"].ToString() + "', '" + dt.Rows[i]["IDDestinatario"].ToString() + "','WEBSERVICE', 'SAIDA', '" + DateTime.Parse(dt.Rows[i]["DataDeEmissao"].ToString()).ToString("yyyy-MM-dd") + "',  getdate(), NULL, '" + dt.Rows[i]["Endereco"].ToString() + "','" + dt.Rows[i]["EnderecoNumero"].ToString() + "', '" + dt.Rows[i]["EnderecoComplemento"].ToString() + "','" + dt.Rows[i]["IDEnderecoBairro"].ToString() + "', '" + dt.Rows[i]["IDEnderecoCidade"].ToString() + "', '" + dt.Rows[i]["EnderecoCep"].ToString().Replace(".", "").Replace("-", "") + "', 'SIM', 45,4882)";

                    for (int ii = 0; ii < dtItens.Rows.Count; ii++)
                    {

                        string m = "Select * from ProdutoEmbalagem where IdProdutoEmbalagem=" + dtItens.Rows[ii]["IDProdutoEmbalagem"].ToString();

                        DataTable r = Sistran.Library.GetDataTables.RetornarDataTableWin(m, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        if (r.Rows.Count == 0)
                        {
                            m = "select top 1 IDProdutoEmbalagem from ProdutoEmbalagem  a Inner join ProdutoCliente pc on pc.IDProdutoCliente = a.IDProdutoCliente where pc.IDProdutoCliente = " + dtItens.Rows[ii]["IdProdutoCliente"].ToString() + " Order by a.UnidadeDoCliente";
                            DataTable r2 = Sistran.Library.GetDataTables.RetornarDataTableWin(m, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                            if (r2.Rows.Count > 0)
                            {
                                dtItens.Rows[ii]["IDProdutoEmbalagem"] = r2.Rows[0][0].ToString();
                            }
                            else
                            {
                                DataTable RETX = Sistran.Library.GetDataTables.RetornarDataTableWin("update documentoPedido set DATADOPROCESSAMENTO=getDate(), erro='Verificar Embalagem' where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                return;
                            }

                        }

                        m = "select isnull(PesoBruto,0) PesoBruto, isnull(PesoLiquido, 0) PesoLiquido from ProdutoEmbalagem pb with (nolock) inner join Produto p  with (nolock) on p.IDProduto = pb.IDProduto where IDProdutoEmbalagem =" + dtItens.Rows[ii]["IDProdutoEmbalagem"].ToString();
                        DataTable dtprodPeso = Sistran.Library.GetDataTables.RetornarDataTableWin(m, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        decimal pesoB = 0;
                        decimal pesoL = 0;

                        if (dtprodPeso.Rows.Count > 0)
                        {
                            pesoB = decimal.Parse(dtItens.Rows[ii]["Quantidade"].ToString()) * decimal.Parse(dtprodPeso.Rows[0]["PesoBruto"].ToString());
                            pesoL = decimal.Parse(dtItens.Rows[ii]["Quantidade"].ToString()) * decimal.Parse(dtprodPeso.Rows[0]["PesoLiquido"].ToString());


                        }

                        int idDocItem = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoItem", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
                        sqlEf += "; Insert into DocumentoItem(IDDocumentoItem, IDDocumento, IDProdutoEmbalagem, IDUsuario,Quantidade, QuantidadeUnidadeEstoque, Saldo,ValorUnitario, ValorTotalDoItem,IdProdutoCliente, IDCfop, IDTes, IDTesCfop, PesoLiquido,PesoBruto, UnidadeDoCLiente) " +
                            "Values (" + idDocItem + ", " + idDoc + ", '" + dtItens.Rows[ii]["IDProdutoEmbalagem"].ToString() + "', 2, " + dtItens.Rows[ii]["Quantidade"].ToString().Replace(",", ".") + ", " + dtItens.Rows[ii]["QuantidadeUnidadeEstoque"].ToString().Replace(",", ".") + ", " + dtItens.Rows[ii]["Saldo"].ToString().Replace(",", ".") + " ," + dtItens.Rows[ii]["ValorUnitario"].ToString().Replace(",", ".") + ", " + dtItens.Rows[ii]["ValorTotalDoItem"].ToString().Replace(",", ".") + "," + dtItens.Rows[ii]["IdProdutoCliente"].ToString() + ", 218, 45,4882, " + pesoL.ToString().Replace(",", ".") + " , " + pesoB.ToString().Replace(",", ".") + ", '"+ dtItens.Rows[ii]["UnidadeDoCLiente"].ToString().Replace(",", ".") + "' )";

                    }

                    int idDocFil = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoFilial", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));

                    sqlEf += "; Insert Into DocumentoFilial (IDDocumentoFilial, IDDocumento, IDFilial, Situacao, Data, IDRegiaoItem)  values (" + idDocFil + ", " + idDoc + ", 15, 'PEDIDO RECEBIDO', GETDATE(), 0)";

                    sqlEf += "; UPDATE  DocumentoFilial SET SITUACAO='LIBERADO PARA SEPARACAO' WHERE IDDOCUMENTOFILIAL =" + idDocFil;

                    //sqlEf += "; update documento set PesoLiquido= (Select SUM(isnull(PesoLiquido,0)) from documentoItem where Iddocumento=" + idDoc + "), PesoBruto= (Select SUM(isnull(PesoBruto,0)) from documentoItem where Iddocumento=" + idDoc + "), where Iddocumento=" + idDoc;

                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlEf + "; update documentoPedido set DATADOPROCESSAMENTO=getDate(), DocumentodoCliente4='" + idDoc + "' where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    try
                    {
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans("UPDATE documento set  EnderecoCep= REPLACE(EnderecoCep, '.', ''),DataDeEntrada=convert(varchar(10), getdate(), 120),ValorDaNota=(select sum(valorTotalDoItem) from DOCUMENTOITEM where IDDocumento=" + idDoc + ") where IdDocumento=" + idDoc, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans("UPDATE documento set PesoBruto=(select sum(PesoBruto) from DOCUMENTOITEM where IDDocumento=" + idDoc + ") where IdDocumento=" + idDoc, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans("UPDATE documento set PesoLiquido=(select sum(PesoLiquido) from DOCUMENTOITEM where IDDocumento=" + idDoc + ") where IdDocumento=" + idDoc, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                        string ss = "Select count(*) from DocumentoItem where IdProdutoEmbalagem not in(Select IdProdutoEmbalagem from ProdutoEmbalagem) and IdDocumento=" + idDoc;
                        DataTable dataTable = Sistran.Library.GetDataTables.RetornarDataTableWS(ss, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        if (dataTable.Rows[0][0].ToString() != "0")
                        {
                            ss = "update documento set Numero=IDDocumento, DataDeCancelamento=GETDATE(), Ativo='NAO' where IDDocumento in (select IDDocumento from Documento where IDDocumento in(" + idDoc + ")); ";
                            ss += " update documentoItem set EstoqueProcessado = 'SIM' where IDDocumento in (" + idDoc + "); ";
                            ss += "Update DOCUMENTOPEDIDO set DocumentodoCliente4 = null, DATADOPROCESSAMENTO = null where DocumentodoCliente4  in (" + idDoc + ") ";

                            Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(ss, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                            ss = "";

                        }

                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }


        private void PedidoDpToDocMaisBrasil()
        {

            try
            {
                //string sql = "SELECT * FROM DocumentoPedido  where iddocumento= 227457";
                string sql = "SELECT * FROM DocumentoPedido  where DATADOPROCESSAMENTO is null and IdCliente=5300980";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "Select IdDocumento from DocumentoPedido where Numero='" + dt.Rows[i]["Numero"].ToString() + "' and ATIVO='SIM' and IdCliente=" + dt.Rows[i]["IdCliente"].ToString();
                    DataTable dtN = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    if (dtN.Rows.Count > 1)
                    {
                        DataTable RETX = Sistran.Library.GetDataTables.RetornarDataTableWin("update documentoPedido set DATADOPROCESSAMENTO=getDate(), erro='Pedido Duplicado'  where Numero=" + dt.Rows[i]["Numero"].ToString() + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        continue;
                    }

                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    sql = "Select IdDocumento from Documento where Numero='" + dt.Rows[i]["Numero"].ToString() + "' and ATIVO='SIM' AND Serie='PED' and IdFilial=" + dt.Rows[i]["IDFilial"].ToString() + " and IdCliente=" + dt.Rows[i]["IdCliente"].ToString();
                    var existe = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    if (existe.Rows.Count > 0)
                    {

                        DataTable RETX = Sistran.Library.GetDataTables.RetornarDataTableWin("update documentoPedido set DATADOPROCESSAMENTO=getDate() where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        continue;
                    }

                    sql = "Select * from DocumentoPedidoItem where IdDocumento=" + dt.Rows[i]["Iddocumento"].ToString();
                    DataTable dtItens = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                    int idDoc = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("Documento", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));

                    string sqlEf = "Insert into Documento(CodigoDoRecExpImpresso, ClasseCFOP,idmodal, IDDocumento, IDFilial, IDFilialAtual,TipoDeDocumento,TipoDeServico, Serie,Numero,AnoMes,IDCliente, IDRemetente, IDDestinatario,Origem, EntradaSaida, DataDeEmissao,  DataDeEntrada, DataPlanejada, Endereco,EnderecoNumero, EnderecoComplemento,IDEnderecoBairro, IDEnderecoCidade, EnderecoCep, Ativo, IdTes,IdTesCfop)" +
                        " values ('1', '5',1, " + idDoc + ", 37, 37,'Pedido','NORMAL', 'PED','" + dt.Rows[i]["Numero"].ToString() + "','" + dt.Rows[i]["AnoMes"].ToString() + "','" + dt.Rows[i]["IDCliente"].ToString() + "', '" + dt.Rows[i]["IDRemetente"].ToString() + "', '" + dt.Rows[i]["IDDestinatario"].ToString() + "','WEBSERVICE', 'SAIDA', '" + DateTime.Parse(dt.Rows[i]["DataDeEmissao"].ToString()).ToString("yyyy-MM-dd") + "',  getdate(), NULL, '" + dt.Rows[i]["Endereco"].ToString() + "','" + dt.Rows[i]["EnderecoNumero"].ToString() + "', '" + dt.Rows[i]["EnderecoComplemento"].ToString() + "','" + dt.Rows[i]["IDEnderecoBairro"].ToString() + "', '" + dt.Rows[i]["IDEnderecoCidade"].ToString() + "', '" + dt.Rows[i]["EnderecoCep"].ToString() + "', 'SIM', 45,4882)";



                    for (int ii = 0; ii < dtItens.Rows.Count; ii++)
                    {
                        int idDocItem = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoItem", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
                        sqlEf += "; Insert into DocumentoItem(IDDocumentoItem, IDDocumento, IDProdutoEmbalagem, IDUsuario,Quantidade, QuantidadeUnidadeEstoque, Saldo,ValorUnitario, ValorTotalDoItem,IdProdutoCliente, IDCfop, IDTes, IDTesCfop, PesoLiquido,PesoBruto) " +
                            "Values (" + idDocItem + ", " + idDoc + ", '" + dtItens.Rows[ii]["IDProdutoEmbalagem"].ToString() + "', 2, " + dtItens.Rows[ii]["Quantidade"].ToString().Replace(",", ".") + ", " + dtItens.Rows[ii]["QuantidadeUnidadeEstoque"].ToString().Replace(",", ".") + ", " + dtItens.Rows[ii]["Saldo"].ToString().Replace(",", ".") + " ," + dtItens.Rows[ii]["ValorUnitario"].ToString().Replace(",", ".") + ", " + dtItens.Rows[ii]["ValorTotalDoItem"].ToString().Replace(",", ".") + "," + dtItens.Rows[ii]["IdProdutoCliente"].ToString() + ", 218, 45,4882, " + dtItens.Rows[ii]["PesoLiquido"].ToString().Replace(",", ".") + " , " + dtItens.Rows[ii]["PesoLiquido"].ToString().Replace(",", ".") + " )";

                    }

                    int idDocFil = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoFilial", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));

                    sqlEf += "; InserT Into DocumentoFilial (IDDocumentoFilial, IDDocumento, IDFilial, Situacao, Data, IDRegiaoItem)  values (" + idDocFil + ", " + idDoc + ", 37, 'PEDIDO RECEBIDO', GETDATE(), 0)";

                    sqlEf += "; UPDATE  DocumentoFilial SET SITUACAO='LIBERADO PARA SEPARACAO' WHERE IDDOCUMENTOFILIAL =" + idDocFil;

                    DataTable RET = Sistran.Library.GetDataTables.RetornarDataTableWin(sqlEf + "; update documentoPedido set DATADOPROCESSAMENTO=getDate(), DocumentodoCliente4='" + idDoc + "' where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    try
                    {
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans("UPDATE documento set ValorDaNota=(select sum(valorTotalDoItem) from DOCUMENTOITEM where IDDocumento=" + idDoc + ") where IdDocumento=" + idDoc, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans("UPDATE documento set PesoBruto=(select sum(PesoBruto) from DOCUMENTOITEM where IDDocumento=" + idDoc + ") where IdDocumento=" + idDoc, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans("UPDATE documento set PesoLiquido=(select sum(PesoLiquido) from DOCUMENTOITEM where IDDocumento=" + idDoc + ") where IdDocumento=" + idDoc, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                    catch (Exception)
                    {

                    }

                }

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }

        private void Comprovei()
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    AcertarNotasNoStistranetDoComprovei();
                    AcertarNotasNoStistranetDoComprovei();
                    //Thread.Sleep(10);
                }
            }
            catch (Exception)
            {
            }
        }

        #region Comprovei
        private void AcertarNotasNoStistranetDoComprovei()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string docAtual = "";
            try
            {
                textBox1.Text = "Excecutando o select";
                Application.DoEvents();

                string sql = "SELECT TOP 50 DATALENGTH (foto) Foto, DATALENGTH(Assinatura) Assinatura, IdRetornoComprovei,IdDocumento,Chave,DataDaOcorrencia,Ocorrencia,IdOcorrenciaComprovei,IdDocumentoOcorrencia,Processado,HorarioRecebimento,HorarioProcessamento,Protocolo,OcorrenciaSetada,OcorrenciaCodigoInvalido,EnviadoS3,UrlS3,Lat,Lon FROM RETORNOCOMPROVEI COMP WITH (NOLOCK) WHERE PROCESSADO IS NULL  and HorarioRecebimento>=getdate()-30 ORDER BY IdOcorrenciaComprovei asc ";
                
                //string sql = "SELECT TOP 200 DATALENGTH (foto) Foto, DATALENGTH(Assinatura) Assinatura, IdRetornoComprovei,IdDocumento,Chave,DataDaOcorrencia,Ocorrencia,IdOcorrenciaComprovei,IdDocumentoOcorrencia,Processado,HorarioRecebimento,HorarioProcessamento,Protocolo,OcorrenciaSetada,OcorrenciaCodigoInvalido,EnviadoS3,UrlS3,Lat,Lon FROM RETORNOCOMPROVEI COMP WITH (NOLOCK) WHERE PROCESSADO IS NULL  and HorarioRecebimento>=getdate()-10 ORDER BY comp.DataDaOcorrencia asc ";
                //string sql = "SELECT TOP 200 COMP.* FROM RETORNOCOMPROVEI COMP WITH (NOLOCK) WHERE PROCESSADO IS NULL  and HorarioRecebimento>=getdate()-10 ORDER BY comp.DataDaOcorrencia asc ";
                //sql = "SELECT TOP 200 COMP.* FROM RETORNOCOMPROVEI COMP WITH (NOLOCK) WHERE IdRetornoComprovei in (1285237) ";

                //base se arquivos               
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] oco = dt.Rows[i]["OCORRENCIA"].ToString().Split('-');
                    oco[0] = (oco[0].Trim().Length > 10 ? oco[0].Trim().Substring(0, 9) : oco[0].Trim());
                    oco[1] = oco[1].Trim();

                    //tratamento das ocorrencias que vem texto como codigo
                    try
                    {
                        int m = int.Parse(oco[0]);
                    }
                    catch (Exception)
                    {
                        //base de arquivos
                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate(), OcorrenciaCodigoInvalido='SIM' where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), cnx);
                        continue;
                    }

                    textBox1.Text = DateTime.Now + "-" + "AcertarNotasNoStistranetDoComprovei. IdDocumento: " + dt.Rows[i]["IDDOCUMENTO"].ToString() + "| - " + i + 1 + " De " + dt.Rows.Count;
                    Application.DoEvents();
                    docAtual = dt.Rows[i]["IDDOCUMENTO"].ToString();


                    string sqlaux = "SELECT * FROM OCORRENCIA WHERE IDOCORRENCIASERIE=3 AND CODIGO='" + oco[0] + "' ";
                    DataTable dtOco = Sistran.Library.GetDataTables.RetornarDataTableWin(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    int idOcorrencia = 0;
                    string finalzadora = "SIM";

                    // se nao existir a ocorrencia insere
                    if (dtOco.Rows.Count == 0)
                    {
                        // GravarLog("Gravando Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

                        idOcorrencia = RetornarIdTabelaNovo("OCORRENCIA");//
                        //idOcorrencia = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("OCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
                        sqlaux = "insert into ocorrencia (IDOcorrencia, IDEmpresa, IDOcorrenciaAcao, Codigo, Nome, Responsabilidade, NomeReduzido, PagaEntrega, Finalizador, Sistema,  Ativo, RestringirCarregamento, AbrirFecharOcorrencia, ApareceSiteCliente, IdOcorrenciaSerie)";
                        sqlaux += "VALUES (" + idOcorrencia + ", NULL, 5, '" + oco[0] + "', '" + (oco[1].Trim().ToUpper().Length >= 60 ? oco[1].Trim().ToUpper().Substring(0, 59) : oco[1].Trim().ToUpper()) + "', 'CLIENTE', '" + (oco[1].Trim().ToUpper().Length >= 30 ? oco[1].Trim().ToUpper().Substring(0, 29) : oco[1].Trim().ToUpper()) + "', 'NAO', 'NAO', NULL,  'NAO', 'NAO', 'AMBOS', NULL, 3)";
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
                                    {
                                        //base de arquivos
                                        strsql = "UPDATE DOCUMENTOOCORRENCIAARQUIVO SET ARQUIVO=(select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ") WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; Select 1";
                                        Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                                    }
                                    else
                                    {

                                        if (dt.Rows[i]["foto"].ToString() != "")
                                        {
                                            //base de arquivos
                                            string id = RetornarIdTabelaNovo("DOCUMENTOOCORRENCIAARQUIVO").ToString();//Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                            strsql = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                                            Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                                        }
                                    }



                                    strsql = "UPDATE DOCUMENTOOCORRENCIA SET IDOCORRENCIACOMPROVEI=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";
                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                                    //base arquivos
                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), cnx);

                                    continue;
                                }


                                if (dataDaOcorrencia.ToString("dd/MM/yyyy") == dataOcorrenciaJaExistente.ToString("dd/MM/yyyy"))
                                {
                                    if (existeFoto.Rows.Count > 0)
                                    {
                                        //base de arquivos
                                        strsql = "UPDATE DOCUMENTOOCORRENCIAARQUIVO SET ARQUIVO=(select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ") WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; select 1";
                                        Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                                    }
                                    else
                                    {
                                        if (dt.Rows[i]["foto"].ToString() != "")
                                        {
                                            //base de arquivos
                                            string id = RetornarIdTabelaNovo("OCORRENCIA").ToString();//Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                            strsql = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + ")) ; ";
                                            Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                                        }
                                    }

                                    strsql = "UPDATE DOCUMENTOOCORRENCIA SET DataOcorrencia='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' ,IDOCORRENCIACOMPROVEI=" + dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + " WHERE IDDOCUMENTOOCORRENCIA=" + aux.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() + " ; ";

                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                                    //base de arquivos
                                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate(), Foto=null where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), cnx);


                                    continue;
                                }
                            }
                        }
                        string IdDocOco = RetornarIdTabelaNovo("DOCUMENTOOCORRENCIA").ToString();//Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        //int IdDocOco = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));

                        sql = "Select top 1 r.IDFilial   from Documento d  Inner join RomaneioDocumento rd  WITH (NOLOCK) on rd.IDDocumento = d.IDDocumento   Inner join Romaneio r  WITH (NOLOCK) on r.IDRomaneio  = rd.IDRomaneio   where d.IdDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "   and r.Tipo='ENTREGA'   Order by 1 desc";
                        DataTable dtFil = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
                        strsql += " IDDocumentoOcorrencia, ";
                        strsql += " IdRomaneio, ";
                        strsql += " IDDocumento,";
                        strsql += " IDFilial,";
                        strsql += " IDOcorrencia,";
                        strsql += " DataOcorrencia,";
                        strsql += " Descricao,";
                        strsql += " Sistema,";
                        strsql += "IdOcorrenciaComprovei, Latitude,Longitude";
                        strsql += " ) VALUES (";
                        strsql += IdDocOco + " ,";
                        strsql += "ISNULL((SELECT TOP 1 ISNULL(RD.IDROMANEIO,null) FROM ROMANEIODOCUMENTO RD WITH (NOLOCK)  INNER JOIN ROMANEIO R ON R.IDROMANEIO = RD.IDROMANEIO WHERE RD.IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " AND  R.TIPO IN ('ENTREGA', 'COLETA') ORDER BY 1 DESC),null)" + " ,";
                        strsql += dt.Rows[i]["IDDOCUMENTO"].ToString() + " ,";
                        //strsql += Convert.ToInt32(dt.Rows[i]["IDFILIALATUAL"].ToString()) + " ,";

                        if (dtFil.Rows.Count > 0 && dtFil.Rows[0][0].ToString() != "")
                        {
                            strsql += dtFil.Rows[0][0].ToString() + ",";
                        }
                        else
                            strsql += "(Select IdFIlialAtual from Documento where IdDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "), ";
                        //strsql += "(Select top 1 r.IDFilial   from Documento d  Inner join RomaneioDocumento rd  WITH (NOLOCK) on rd.IDDocumento = d.IDDocumento   Inner join Romaneio r  WITH (NOLOCK) on r.IDRomaneio  = rd.IDRomaneio   where d.IdDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "   and r.Tipo='ENTREGA'   Order by 1 desc), ";


                        ////se a data de conclusao for null coloca a ocorrencia se nao apenas uma observação que se caracteriza pelo null no idocorrencia

                        //if (finalzadora == "SIM")
                        strsql += int.Parse(idOcorrencia.ToString()) + " ,";
                        //else
                        //    strsql += " null ,";


                        strsql += "'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' ,";
                        strsql += " '" + dt.Rows[i]["Ocorrencia"].ToString().Trim() + " - Comprovei',";
                        strsql += "'SIM',";
                        strsql += dt.Rows[i]["IdOcorrenciaComprovei"].ToString() + ", '" + dt.Rows[i]["Lat"].ToString() + "', '" + dt.Rows[i]["Lon"].ToString() + "' );   ";

                        string SetDocFilial = "";

                        if (finalzadora == "SIM")
                        {
                            strsql += "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + ", DATADECONCLUSAO= '" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "'  WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                            strsql += " UPDATE DOCUMENTOFILIAL SET  data='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "' ,SITUACAO='PROCESSO FINALIZADO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                            //strsql += " UPDATE DOCUMENTO SET DATADECONCLUSAO= convert(datetime,'" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "', 103)" + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "; "; 
                            SetDocFilial = "PROCESSO FINALIZADO";
                        }
                        else
                        {
                            string x = "SELECT COUNT(*) FROM DOCUMENTOFILIAL WHERE SITUACAO='PROCESSO FINALIZADO' AND IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString();
                            DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWin(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                            if (dtx.Rows[0][0].ToString() == "0")
                            {
                                if (dtOco.Rows[0]["RestringirCarregamento"].ToString() == "" || dtOco.Rows[0]["RestringirCarregamento"].ToString() == "NAO")
                                {
                                    strsql += " UPDATE DOCUMENTOFILIAL SET data='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "' , SITUACAO='AGUARDANDO EMBARQUE' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                                    SetDocFilial = "AGUARDANDO EMBARQUE";
                                }
                                else
                                {
                                    strsql += " UPDATE DOCUMENTOFILIAL SET DATA='" + DateTime.Parse(dt.Rows[i]["DataDaOcorrencia"].ToString()).ToString("yyyy-MM-dd") + "' , SITUACAO='AGUARDANDO SOLUCAO' WHERE IDDOCUMENTO = " + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ; ";
                                    SetDocFilial = "AGUARDANDO SOLUCAO";

                                }

                                if (oco[0] != "998")
                                    strsql += " UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " WHERE IDDocumento=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + " ;";
                            }
                        }

                        if (dt.Rows[i]["foto"].ToString() != "")
                        {
                            string id = RetornarIdTabelaNovo("DOCUMENTOOCORRENCIAARQUIVO").ToString();//Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                            Sistran.Library.GetDataTables.RetornarDataTableWin("insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + IdDocOco.ToString() + ", (select foto from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + "), null, 0) ; Select 1", cnx);
                        }

                        if (dt.Rows[i]["Assinatura"].ToString() != "")
                        {
                            string id = RetornarIdTabelaNovo("DOCUMENTOOCORRENCIAARQUIVO").ToString();//Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                            sql = "insert into DOCUMENTOOCORRENCIAARQUIVO values (" + id + ", " + IdDocOco.ToString() + ", (select Assinatura from RETORNOCOMPROVEI  WITH (NOLOCK) where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString() + "), null, 1) ; ";
                            Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                        }

                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        //base de arquivos
                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate(), OcorrenciaSetada='" + SetDocFilial + "' where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), cnx);
                        //Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate(), Foto=null, OcorrenciaSetada='" + SetDocFilial + "' where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), cnx);

                        //SE INSERIR A DATA DE CONCLUSAO CALCULA O PRAZO UTILIZADO
                        if (finalzadora == "SIM")
                            Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(" EXEC SP_PRAZO_UTILIZADO_ID " + dt.Rows[i]["IDDOCUMENTO"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    }
                    else
                    {
                        strsql = "";

                        if (strsql.Length > 10)
                            Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN("Update RetornoComprovei set Processado=getdate() where IdRetornoComprovei=" + dt.Rows[i]["IdRetornoComprovei"].ToString(), cnx);

                    }
                }
            }
            catch (Exception ex)
            {
                //Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "AcertarNotasNoStistranetDoComprovei ", "Documento: " + docAtual + ". Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Erro no Timer");

            }
        }

        public void AcertarDadosDTRomaneio(int iddocumento)
        {
            try
            {

                textBox1.Text = DateTime.Now + "-" + "AcertarDadosDTRomaneio. IdDocumento: " + iddocumento;
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
            catch (Exception)
            {
            }
        }
        #endregion

        private void EnviarCC()
        {
            string err = "";
            OracleConnection conn = new OracleConnection();
            OracleTransaction transaction = null; ;
            try
            {
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS("Exec PRC_Envio_CC_CS", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                string sqlora = "";
                // string sqllogos = "";

                textBox1.Text = "Iniciou...";
                Application.DoEvents();

                string oradb = "Data Source=(DESCRIPTION="
                    + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))"
                    + "(CONNECT_DATA=(SERVICE_NAME=csorcl)));"
                    + "User Id=csintegracao;Password=wxmj6evc8k;";

                conn.ConnectionString = oradb;
                conn.Open();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string CODIGO_CORRECAO = "", NR_CHAVE_ACESSO = "", VALOR_NOVO = "", SEQ_EVENTO = "", PROTOCOLO = "", TIPO_DOCUMENTO = "", COD_GRUPOEMPRESA = "", COD_EMPRESA = "", COD_FILIAL = "", NR_CARTACORRECAO = "", DATACARTA = "", DESC_UNIDADE = "";

                    err = dt.Rows[i]["IdDocumento"].ToString();

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(dt.Rows[i]["XMLCarta"].ToString());

                    string insert = "";
                    string values = "";

                    if (dt.Rows[i]["tipo"].ToString() == "CTE")
                    {
                        CODIGO_CORRECAO = "1";
                        TIPO_DOCUMENTO = "2";
                        PROTOCOLO = dt.Rows[i]["NumeroProtocolo"].ToString();
                        COD_GRUPOEMPRESA = "1";

                        COD_EMPRESA = dt.Rows[i]["codEmpresa"].ToString();
                        COD_FILIAL = dt.Rows[i]["fil"].ToString();

                        SEQ_EVENTO = xml.DocumentElement.GetElementsByTagName("nSeqEvento").Item(0).InnerText;
                        DATACARTA = xml.DocumentElement.GetElementsByTagName("dhRegEvento").Item(0).InnerText;
                        NR_CARTACORRECAO = (dt.Rows[i]["IdDocumentoEletronico"].ToString().Length > 8 ? dt.Rows[i]["IdDocumentoEletronico"].ToString().Substring(0, 9) : dt.Rows[i]["IdDocumentoEletronico"].ToString());
                        VALOR_NOVO = dt.Rows[i]["TextoCarta"].ToString(); ////xml.DocumentElement.GetElementsByTagName("xMotivo").Item(0).InnerText;
                        NR_CHAVE_ACESSO = xml.DocumentElement.GetElementsByTagName("chCTe").Item(0).InnerText;

                        insert = "Insert into cartacorrecao (";
                        values = " Values (";

                        insert += "CODIGO_CORRECAO,";
                        values += CODIGO_CORRECAO + ", ";

                        insert += "TIPO_DOCUMENTO, ";
                        values += "2, ";

                        insert += "NR_CHAVE_ACESSO, ";
                        values += "'" + NR_CHAVE_ACESSO + "', ";

                        insert += "PROTOCOLO, ";
                        values += "'" + PROTOCOLO + "', ";

                        insert += "COD_GRUPOEMPRESA, ";
                        values += COD_GRUPOEMPRESA + ", ";

                        insert += "COD_EMPRESA, ";
                        values += COD_EMPRESA + ", ";


                        insert += "COD_FILIAL,";
                        values += COD_FILIAL + ", ";

                        insert += "SEQ_EVENTO, ";
                        values += SEQ_EVENTO + ", ";

                        insert += "DATACARTA, ";
                        values += "TO_DATE('" + DateTime.Parse(DATACARTA).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'), ";

                        insert += "NR_CARTACORRECAO,";
                        values += NR_CARTACORRECAO + ", ";

                        insert += "VALOR_NOVO)";
                        values += "'" + VALOR_NOVO + "') ";


                    }
                    else
                    {
                        CODIGO_CORRECAO = "88";
                        NR_CHAVE_ACESSO = xml.DocumentElement.GetElementsByTagName("chNFe").Item(0).InnerText;
                        VALOR_NOVO = dt.Rows[i]["TextoCarta"].ToString(); //xml.DocumentElement.GetElementsByTagName("xCorrecao").Item(0).InnerText;
                        SEQ_EVENTO = xml.DocumentElement.GetElementsByTagName("nSeqEvento").Item(0).InnerText;
                        PROTOCOLO = dt.Rows[i]["NumeroProtocolo"].ToString();
                        TIPO_DOCUMENTO = "1";

                        COD_GRUPOEMPRESA = "1";
                        COD_EMPRESA = dt.Rows[i]["codEmpresa"].ToString();
                        COD_FILIAL = dt.Rows[i]["fil"].ToString();

                        NR_CARTACORRECAO = dt.Rows[i]["IdDocumentoEletronico"].ToString();
                        DATACARTA = xml.DocumentElement.GetElementsByTagName("dhEvento").Item(0).InnerText;
                        //string NR_ITEM_NF = "1";
                        DESC_UNIDADE = "1";

                        insert = "Insert into cartacorrecao (";
                        values = " Values (";

                        insert += "CODIGO_CORRECAO, ";
                        values += CODIGO_CORRECAO + ", ";

                        insert += "NR_CHAVE_ACESSO, ";
                        values += "'" + NR_CHAVE_ACESSO + "', ";

                        insert += "VALOR_NOVO, ";
                        values += "'" + VALOR_NOVO + "', ";

                        insert += "SEQ_EVENTO, ";
                        values += SEQ_EVENTO + ", ";

                        insert += "PROTOCOLO, ";
                        values += "'" + PROTOCOLO + "', ";

                        insert += "TIPO_DOCUMENTO, ";
                        values += TIPO_DOCUMENTO + ", ";

                        insert += "COD_GRUPOEMPRESA, ";
                        values += CODIGO_CORRECAO + ", ";

                        insert += "COD_EMPRESA , ";
                        values += COD_EMPRESA + ", ";

                        insert += "COD_FILIAL , ";
                        values += COD_FILIAL + ", ";

                        insert += "NR_CARTACORRECAO , ";
                        values += NR_CARTACORRECAO + ", ";

                        insert += "DATACARTA, ";
                        values += "TO_DATE('" + DateTime.Parse(DATACARTA).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'), ";

                        insert += "NR_ITEM_NF, ";
                        //values += CODIGO_CORRECAO + ", ";
                        values += "1, ";

                        insert += "DESC_UNIDADE) ";
                        values += "'" + DESC_UNIDADE + "') ";
                    }

                    sqlora = insert + values;

                    transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    OracleCommand oracleCommand = new OracleCommand(sqlora, conn);
                    oracleCommand.Transaction = transaction;

                    try
                    {
                        //oracleCommand.ExecuteNonQuery();

                        oracleCommand = new OracleCommand(sqlora, conn);
                        oracleCommand.Transaction = transaction;
                        oracleCommand.ExecuteNonQuery();

                        sqlora = "select max(ID_CORRECAO) from CARTACORRECAO t";
                        oracleCommand = new OracleCommand(sqlora, conn);
                        var c = oracleCommand.ExecuteScalar();
                        sqlora = "Insert Into cartacorrecao_xml(id_correcao, xml_cce ) values(" + c + ", :arquivo)";
                        OracleParameter par = new OracleParameter(":arquivo", OracleDbType.Blob);

                        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(xml.InnerXml);

                        par.Value = bytes;

                        oracleCommand = new OracleCommand(sqlora, conn);
                        oracleCommand.Parameters.Add(par);

                        oracleCommand.Transaction = transaction;
                        oracleCommand.ExecuteNonQuery();

                    }
                    catch (Exception xxx)
                    {
                        string er = xxx.Message;
                        transaction.Rollback();
                        DataTable dtcx = Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoEletronico set EnviadoCS='erro: cs recusou' where IdDocumentoEletronico=" + dt.Rows[i]["IdDocumentoEletronico"].ToString() + "; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        continue;
                    }

                    textBox1.Text = "Finalizou Importação do Documento : " + dt.Rows[i]["IdDocumento"].ToString();
                    Application.DoEvents();
                    transaction.Commit();

                    DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoEletronico set EnviadoCS='Enviado: " + DateTime.Now + "' where IdDocumentoEletronico=" + dt.Rows[i]["IdDocumentoEletronico"].ToString() + "; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }
                conn.Close();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        private void EnviarRPCI()
        {
            OracleConnection conn = new OracleConnection();
            OracleTransaction transaction = null;
            string sqlora = "";
            string sqllogos = "";

            textBox1.Text = "Iniciou...";
            Application.DoEvents();
            string err = "";

            try
            {

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS("Exec PRC_Envio_RPCI_CS", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                string oradb = "Data Source=(DESCRIPTION="
                + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))"
                + "(CONNECT_DATA=(SERVICE_NAME=csorcl)));"
                + "User Id=csintegracao;Password=wxmj6evc8k;";

                conn.ConnectionString = oradb;
                conn.Open();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    err = dt.Rows[i]["IdRPCI"].ToString();

                    string s = "Select count(*) from Mat_oci where Id_rpci='" + dt.Rows[i]["IDRPCI"].ToString() + "'";
                    OracleCommand oracleCommand = new OracleCommand(s, conn);
                    var x = oracleCommand.ExecuteScalar().ToString();

                    if (int.Parse(x) > 0)
                    {
                        sqllogos = "Update RPCI set EnviadoCS = 'Enviado: " + DateTime.Now + "' where IdRPCI=" + dt.Rows[i]["IDRPCI"].ToString();
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqllogos, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        continue;
                    }


                    sqlora = "Insert Into MAT_OCI (COD_GRUPOEmpresa, COD_EMPRESA,COD_FILIAL, DATA_OCI, COD_FUNCIONARIO, CPF_CNPJ, COD_PLANO, COD_MOTIVO,COD_TIPOFRETe, DATA_PAGTO_FIXO, ID_RPCI, NUMERO_RPCI  )" +
                        " Values (1, " + dt.Rows[i]["codEmpresa"].ToString() + ", " + dt.Rows[i]["fil"].ToString() + " , TO_DATE('" + DateTime.Parse(dt.Rows[i]["DataDeEmissao"].ToString()).ToString("dd/MM/yyyy") + "'),  273, '" + dt.Rows[i]["CNPJCPF"].ToString() + "',99,9,4, TO_DATE('" + DateTime.Parse(dt.Rows[i]["DataDeVencimento"].ToString()).ToString("dd/MM/yyyy") + "'), " + dt.Rows[i]["IDRPCI"].ToString() + ", " + dt.Rows[i]["NumeroRpci"].ToString() + " ) ";
                    //" 

                    transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    oracleCommand = new OracleCommand(sqlora, conn);
                    oracleCommand.Transaction = transaction;
                    oracleCommand.ExecuteNonQuery();

                    sqlora = "select max(ID_MATOCI) from MAT_OCI t";
                    oracleCommand = new OracleCommand(sqlora, conn);
                    var c = oracleCommand.ExecuteScalar();


                    sqlora = "Insert into MAT_OCI_ITEM (ID_MATOCI, ITEM, COD_MATERIAL, PRECO, QUANTIDADE, COD_OBJETOCUSTO, COD_ALMOXARIFADO  ) Values (" + c + ", 1, 2726, " + dt.Rows[i]["saldoAReceber"].ToString().Replace(",", ".") + ", 1, 14, 1)";
                    oracleCommand = new OracleCommand(sqlora, conn);
                    oracleCommand.Transaction = transaction;
                    oracleCommand.ExecuteNonQuery();

                    transaction.Commit();
                    sqllogos = "Update RPCI set EnviadoCS = 'Enviado: " + DateTime.Now + "' where IdRPCI=" + dt.Rows[i]["IDRPCI"].ToString();
                    Sistran.Library.GetDataTables.RetornarDataTableWS(sqllogos + " ; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }



                conn.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                try
                {

                    sqllogos = "Update RPCI set EnviadoCS= 'Erro: " + ex.Message + "' where IDRPCI=" + err + ";";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqllogos, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                }
                catch (Exception)
                {
                }


                // transaction.Rollback();

                string x = ex.Message;

                DataSet ds = new DataSet();
                if (File.Exists("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml"))
                {
                    ds.ReadXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdTitulo");
                    dt.Columns.Add("Erro");

                    ds.Tables.Add(dt);
                }


                DataRow row = ds.Tables[0].NewRow();
                row[0] = err;
                row[1] = ex.Message + sqlora;
                ds.Tables[0].Rows.Add(row);


                File.Delete("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
                ds.WriteXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");

                textBox1.Text = ex.Message;
                Application.DoEvents();

            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        private void EnviarDocumentos()
        {
            OracleConnection conn = new OracleConnection();
            OracleTransaction transaction;
            string sqlora = "";
            string sqllogos = "";

            textBox1.Text = "Iniciou...";
            Application.DoEvents();
            string err = "";
            try
            {
                //DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS("Exec PRC_Envio_CTe_NFe_CS", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS("Exec PRC_Envio_CTe_NFe_CS", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()); //pendencias de envio
                //Select* From Integracao where tabela = 'CTe-Enviar-CS' and DataDeIntegracao is null

                string oradb = "Data Source=(DESCRIPTION="
                + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))"
                + "(CONNECT_DATA=(SERVICE_NAME=csorcl)));"
                + "User Id=csintegracao;Password=wxmj6evc8k;";

                conn.ConnectionString = oradb;
                conn.Open();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    err = dt.Rows[i]["IdDocumento"].ToString();

                    string TIPO = "", CHAVE = "", CNPJ_EMITENTE = "", IE_EMITENTE = "", CPF_EMITENTE = "", NOME_EMITENTE = "", CNPJ_REMETENTE = "", IE_REMETENTE = "", CPF_REMETENTE = "", NOME_REMETENTE = "", CNPJ_DESTINATARIO = "", IE_DESTINATARIO = "", CPF_DESTINATARIO = "", NOME_DESTINATARIO = "", CNPJ_TOMADOR = "", IE_TOMADOR = "", CPF_TOMADOR = "", NOME_TOMADOR = "", TOMA = "";

                    XmlDocument x = new XmlDocument();
                    x.LoadXml(dt.Rows[i]["UltimoArquivoXml"].ToString());

                  //  x.Load()


                    TIPO = dt.Rows[i]["TIPO"].ToString();

                    if (dt.Rows[i]["TIPO"].ToString() == "NFE")
                    {
                        CHAVE = ((XmlElement)x.DocumentElement.GetElementsByTagName("infNFe").Item(0)).Attributes["Id"].Value.Replace("NFe", "");

                        if (CHAVE != dt.Rows[i]["IDNOTA"].ToString().Trim())
                            throw new Exception("Chave Divergente");


                        if (!CHAVE.Contains(dt.Rows[i]["numero"].ToString().Trim()))
                            throw new Exception("Chave com numero divergente");

                        CNPJ_EMITENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("emit").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;
                        IE_EMITENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("emit").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                        CPF_EMITENTE = "";
                        NOME_EMITENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("emit").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText;

                        CNPJ_REMETENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("emit").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;
                        IE_REMETENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("emit").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                        NOME_REMETENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("emit").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText;

                        if (((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("CPF").Count == 0)
                        {
                            CNPJ_DESTINATARIO = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;
                            try
                            {
                                IE_DESTINATARIO = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                            }
                            catch (Exception)
                            {
                                IE_DESTINATARIO = "ISENTO";
                            }
                        }
                        else
                            CPF_DESTINATARIO = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("CPF").Item(0).InnerText;

                        NOME_DESTINATARIO = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText;
                        TOMA = "4";
                    }
                    else if (dt.Rows[i]["TIPO"].ToString() == "CTE")
                    {
                        //DataTable dtx1 = Sistran.Library.GetDataTables.RetornarDataTableWS("Update Documento set EnviadoCS='-: " + DateTime.Now + "' where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + "; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        //try
                        //{
                        //    conn.Close();
                        //}
                        //catch (Exception)
                        //{

                           
                        //}
                        //continue;

                        TOMA = ((XmlElement)((XmlElement)x.DocumentElement.GetElementsByTagName("ide").Item(0))).GetElementsByTagName("toma").Item(0).FirstChild.Value;
                        CHAVE = ((XmlElement)x.DocumentElement.GetElementsByTagName("infCte").Item(0)).Attributes["Id"].Value.Replace("CTe", "");

                        if (CHAVE != dt.Rows[i]["IDNOTA"].ToString().Trim())
                            throw new Exception("Chave Divergente");

                        if (decimal.Parse(((XmlElement)x.DocumentElement.GetElementsByTagName("vPrest").Item(0)).GetElementsByTagName("vTPrest").Item(0).InnerText.Replace(".", ","))
                            != decimal.Parse(dt.Rows[i]["VlFreteSistema"].ToString()))
                        {
                            if (decimal.Parse(dt.Rows[i]["VlFreteSistema"].ToString()) != 0)
                            {
                                throw new Exception("Valor de Frete Divergente");
                            }
                        }


                        if (!CHAVE.Contains(dt.Rows[i]["numero"].ToString().Trim()))
                            throw new Exception("Chave com numero divergente");

                        CNPJ_EMITENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("emit").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;
                        IE_EMITENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("emit").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                        CPF_EMITENTE = "";
                        NOME_EMITENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("emit").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText.Replace("'", "");

                        if (((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("CNPJ").Item(0) != null)
                        {
                            CNPJ_REMETENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;


                            if (((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("IE").Item(0) == null)
                                IE_REMETENTE = "";
                            else
                            IE_REMETENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                            
                            
                            NOME_REMETENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText.Replace("'", "");
                        }
                        else
                        {
                            CNPJ_REMETENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("CPF").Item(0).InnerText;
                            IE_REMETENTE = "ISENTO";
                            NOME_REMETENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText.Replace("'", "");
                        }

                        if (((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("CPF").Count == 0)
                        {
                            CNPJ_DESTINATARIO = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;

                            if (((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("IE").Count > 0)
                                IE_DESTINATARIO = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                        }
                        else
                            CPF_DESTINATARIO = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("CPF").Item(0).InnerText;

                        NOME_DESTINATARIO = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText.Replace("'", "");


                        switch (TOMA)
                        {
                            case "0":
                                CNPJ_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;

                                if (((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("IE").Item(0) == null)
                                    IE_TOMADOR = "";
                                else
                                    IE_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                                
                                
                                NOME_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("rem").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText;
                                TOMA = "0";
                                break;

                            case "1":
                                CNPJ_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("exped").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;
                                IE_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("exped").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                                NOME_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("exped").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText;
                                TOMA = "1";
                                break;

                            case "2":
                                CNPJ_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("receb").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;
                                IE_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("receb").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                                NOME_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("receb").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText;
                                TOMA = "2";
                                break;


                            case "3":
                                if (((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("CPF").Count == 0)
                                {
                                    CNPJ_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("CNPJ").Item(0).InnerText;
                                    IE_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("IE").Item(0).InnerText;
                                }
                                else
                                    CPF_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("CPF").Item(0).InnerText;

                                NOME_TOMADOR = ((XmlElement)x.DocumentElement.GetElementsByTagName("dest").Item(0)).GetElementsByTagName("xNome").Item(0).InnerText;
                                TOMA = "3";
                                break;

                            case "4":
                                TOMA = "4";
                                CNPJ_TOMADOR = ((XmlElement)((XmlElement)x.DocumentElement.GetElementsByTagName("ide").Item(0))).GetElementsByTagName("CNPJ").Item(0).FirstChild.Value;

                                if (((XmlElement)((XmlElement)x.DocumentElement.GetElementsByTagName("ide").Item(0))).GetElementsByTagName("IE").Count > 0)
                                    IE_TOMADOR = ((XmlElement)((XmlElement)x.DocumentElement.GetElementsByTagName("ide").Item(0))).GetElementsByTagName("IE").Item(0).FirstChild.Value;
                                else
                                    IE_TOMADOR = "ISENTO";

                                NOME_TOMADOR = ((XmlElement)((XmlElement)x.DocumentElement.GetElementsByTagName("ide").Item(0))).GetElementsByTagName("xNome").Item(0).FirstChild.Value;


                                break;
                        }
                    }


                    string c = "select top 1 cid.CodigoDoIBGE " +
                    " from Documento d " +
                    " Inner join DocumentoEletronico de on de.IdDocumento = d.IDDocumento " +
                    " Inner join Cadastro c on c.IDCadastro = d.IDCliente " +
                    " Inner join Cidade cid on cid.IDCidade = c.IDCidade " +
                    " where DataDeEmissao >= '2019-09-25' " +
                    " and cid.CodigoDoIBGE > 0 " +
                    " and IdNota = '" + CHAVE + "'";
                    DataTable dtIbge = Sistran.Library.GetDataTables.RetornarDataTableWS(c, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());



                    string insert = "Insert into IMPORTACAO_XML (";
                    string values = " Values (";

                    insert += "ARQUIVO, ";
                    values += ":arquivo, ";

                    insert += "TIPO, ";
                    values += "'" + TIPO + "',";

                    insert += "CHAVE, ";
                    values += "'" + CHAVE + "',";

                    insert += "CNPJ_EMITENTE, ";
                    values += "'" + CNPJ_EMITENTE + "',";

                    insert += "IE_EMITENTE, ";
                    values += "'" + IE_EMITENTE + "',";

                    insert += "CPF_EMITENTE, ";
                    values += "'" + CPF_EMITENTE + "',";

                    insert += "NOME_EMITENTE, ";
                    values += "'" + NOME_EMITENTE + "',";

                    insert += "CNPJ_REMETENTE ,";
                    values += "'" + CNPJ_REMETENTE + "',";

                    insert += "IE_REMETENTE, ";
                    values += "'" + IE_REMETENTE + "',";

                    insert += "CPF_REMETENTE, ";
                    values += "'" + CPF_REMETENTE + "',";

                    insert += "NOME_REMETENTE, ";
                    values += "'" + NOME_REMETENTE + "',";

                    insert += "CNPJ_DESTINATARIO ,";
                    values += "'" + CNPJ_DESTINATARIO + "',";

                    insert += "IE_DESTINATARIO, ";
                    values += "'" + IE_DESTINATARIO + "',";

                    insert += "CPF_DESTINATARIO ,";
                    values += "'" + CPF_DESTINATARIO + "',";

                    insert += "NOME_DESTINATARIO ,";
                    values += "'" + NOME_DESTINATARIO + "',";

                    insert += "CNPJ_TOMADOR, ";
                    values += "'" + CNPJ_TOMADOR + "',";

                    insert += "IE_TOMADOR ,";
                    values += "'" + IE_TOMADOR + "',";

                    insert += "CPF_TOMADOR ,";
                    values += "'" + CPF_TOMADOR + "',";

                    insert += "NOME_TOMADOR ,";
                    values += "'" + NOME_TOMADOR + "',";

                    if (dtIbge.Rows.Count > 0)
                    {
                        insert += " CODIBGE_TOMADOR, ";
                        values += "'" + (dtIbge.Rows[0][0].ToString() == "" ? "0" : dtIbge.Rows[0][0].ToString()) + "',";

                    }

                    insert += "TOMA)";
                    values += "'" + TOMA + "')";

                    OracleParameter par = new OracleParameter(":arquivo", OracleDbType.Clob);
                    par.Value = x.InnerXml;
                    sqlora = insert + values;

                    transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    OracleCommand oracleCommand = new OracleCommand(sqlora, conn);
                    oracleCommand.Parameters.Add(par);

                    oracleCommand.Transaction = transaction;
                    oracleCommand.ExecuteNonQuery();

                    textBox1.Text = "Finalizou Importação do Documento : " + dt.Rows[i]["IdDocumento"].ToString();
                    Application.DoEvents();
                    transaction.Commit();

                    DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWS("Update Documento set EnviadoCS='Enviado: " + DateTime.Now + "' where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + "; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    sqllogos = "Update Documento set EnviadoCS= 'Erro: " + ex.Message + "' where idDocumento=" + err + ";";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqllogos, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }
                catch (Exception)
                {
                }

                string x = ex.Message;

                DataSet ds = new DataSet();
                if (File.Exists("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml"))
                {
                    ds.ReadXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdTitulo");
                    dt.Columns.Add("Erro");

                    ds.Tables.Add(dt);
                }


                DataRow row = ds.Tables[0].NewRow();
                row[0] = err;
                row[1] = ex.Message + sqlora;
                ds.Tables[0].Rows.Add(row);


                File.Delete("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
                ds.WriteXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");

                textBox1.Text = ex.Message;
                Application.DoEvents();

            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        private void EnviarDocumentosCancelados()
        {
            OracleConnection conn = new OracleConnection();
            OracleTransaction transaction;
            string sqlora = "";
            string sqllogos = "";

            textBox1.Text = "Iniciou...";
            Application.DoEvents();
            string err = "";
            try
            {

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS("Exec PRC_ENVIO_CTE_CS_CANCEL", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                string oradb = "Data Source=(DESCRIPTION="
                + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))"
                + "(CONNECT_DATA=(SERVICE_NAME=csorcl)));"
                + "User Id=csintegracao;Password=wxmj6evc8k;";

                conn.ConnectionString = oradb;
                conn.Open();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["TipodeDocumento"].ToString() == "CONHECIMENTO")
                    {
                        err = dt.Rows[i]["IdDocumento"].ToString();
                        //err = dt.Rows[i]["InscricaoRG"].ToString();


                        string CHAVE = "", CNPJ_EMITENTE = "";//.Insert//, IE_EMITENTE = "", CPF_EMITENTE = "", NOME_EMITENTE = "", CNPJ_REMETENTE = "", IE_REMETENTE = "", CPF_REMETENTE = "", NOME_REMETENTE = "", CNPJ_DESTINATARIO = "", IE_DESTINATARIO = "", CPF_DESTINATARIO = "", NOME_DESTINATARIO = "", CNPJ_TOMADOR = "", IE_TOMADOR = "", CPF_TOMADOR = "", NOME_TOMADOR = "", TOMA = "";

                        XmlDocument x = new XmlDocument();
                        try
                        {
                            x.LoadXml(dt.Rows[i]["UltimoArquivoXml"].ToString());

                        }
                        catch (Exception)
                        {
                            x.LoadXml(dt.Rows[i]["XMLCancelamento"].ToString());


                        }

                        CHAVE = ((XmlElement)x.DocumentElement.GetElementsByTagName("infEvento").Item(0)).GetElementsByTagName("chCTe").Item(0).FirstChild.Value;

                        try
                        {
                            CNPJ_EMITENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("infEvento").Item(0)).GetElementsByTagName("CNPJ").Item(0).FirstChild.Value;

                        }
                        catch (Exception)
                        {
                            CNPJ_EMITENTE = CHAVE.Substring(6, 14);

                        }


                        string insert = "Insert into IMPORTACAO_XML (CANCELADA,";
                        string values = " Values ('S',";

                        insert += "ARQUIVO, ";
                        values += ":arquivo, ";

                        insert += "TIPO, ";
                        values += "'CTE',";

                        insert += "CHAVE, ";
                        values += "'" + CHAVE + "',";

                        insert += "IE_EMITENTE, ";
                        values += "'" + (dt.Rows[i]["InscricaoRG"].ToString() == "" ? "ISENTO" : dt.Rows[i]["InscricaoRG"].ToString()) + "',";

                        insert += "CNPJ_EMITENTE) ";
                        values += "'" + CNPJ_EMITENTE + "')";

                        OracleParameter par = new OracleParameter(":arquivo", OracleDbType.Clob);
                        par.Value = x.InnerXml;
                        sqlora = insert + values;

                        transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        OracleCommand oracleCommand = new OracleCommand(sqlora, conn);
                        oracleCommand.Parameters.Add(par);

                        oracleCommand.Transaction = transaction;
                        oracleCommand.ExecuteNonQuery();

                        textBox1.Text = "Finalizou Importação do Documento : " + dt.Rows[i]["IdDocumento"].ToString();
                        Application.DoEvents();
                        transaction.Commit();

                        DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoEletronico set EnviadoCancelamentoCS='Enviado: " + DateTime.Now + "' where cstatus in('135', '155', '101') and IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + "; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    }
                    else //NF
                    {
                        err = dt.Rows[i]["IdDocumento"].ToString();
                        //err = dt.Rows[i]["InscricaoRG"].ToString();


                        string CHAVE = "", CNPJ_EMITENTE = "";//.Insert//, IE_EMITENTE = "", CPF_EMITENTE = "", NOME_EMITENTE = "", CNPJ_REMETENTE = "", IE_REMETENTE = "", CPF_REMETENTE = "", NOME_REMETENTE = "", CNPJ_DESTINATARIO = "", IE_DESTINATARIO = "", CPF_DESTINATARIO = "", NOME_DESTINATARIO = "", CNPJ_TOMADOR = "", IE_TOMADOR = "", CPF_TOMADOR = "", NOME_TOMADOR = "", TOMA = "";

                        XmlDocument x = new XmlDocument();
                        x.LoadXml(dt.Rows[i]["UltimoArquivoXml"].ToString());

                        CHAVE = ((XmlElement)x.DocumentElement.GetElementsByTagName("infEvento").Item(0)).GetElementsByTagName("chNFe").Item(0).FirstChild.Value;

                        CNPJ_EMITENTE = ((XmlElement)x.DocumentElement.GetElementsByTagName("infEvento").Item(0)).GetElementsByTagName("CNPJ").Item(0).FirstChild.Value;


                        string insert = "Insert into IMPORTACAO_XML (CANCELADA,";
                        string values = " Values ('S',";

                        insert += "ARQUIVO, ";
                        values += ":arquivo, ";

                        insert += "TIPO, ";
                        values += "'NFE',";

                        insert += "CHAVE, ";
                        values += "'" + CHAVE + "',";

                        insert += "IE_EMITENTE, ";
                        values += "'" + (dt.Rows[i]["InscricaoRG"].ToString() == "" ? "ISENTO" : dt.Rows[i]["InscricaoRG"].ToString()) + "',";

                        insert += "CNPJ_EMITENTE) ";
                        values += "'" + CNPJ_EMITENTE + "')";

                        OracleParameter par = new OracleParameter(":arquivo", OracleDbType.Clob);
                        par.Value = x.InnerXml;
                        sqlora = insert + values;

                        transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        OracleCommand oracleCommand = new OracleCommand(sqlora, conn);
                        oracleCommand.Parameters.Add(par);

                        oracleCommand.Transaction = transaction;
                        oracleCommand.ExecuteNonQuery();

                        textBox1.Text = "Finalizou Importação do Documento : " + dt.Rows[i]["IdDocumento"].ToString();
                        Application.DoEvents();
                        transaction.Commit();

                        DataTable dtx = Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoEletronico set EnviadoCancelamentoCS='Enviado: " + DateTime.Now + "' where cstatus in('135', '155') and IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() + "; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    }
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                try
                {
                    sqllogos = "Update DocumentoEletronico set EnviadoCancelamentoCS= 'Erro: -x' where idDocumento=" + err + ";";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqllogos, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }
                catch (Exception aex)
                {
                }

                string x = ex.Message;

                DataSet ds = new DataSet();
                if (File.Exists("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml"))
                {
                    ds.ReadXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdTitulo");
                    dt.Columns.Add("Erro");
                    ds.Tables.Add(dt);
                }

                DataRow row = ds.Tables[0].NewRow();
                row[0] = err;
                row[1] = ex.Message + sqlora;
                ds.Tables[0].Rows.Add(row);

                File.Delete("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
                ds.WriteXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");

                textBox1.Text = ex.Message;
                Application.DoEvents();

            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public void EnviarFaturaCancelada()
        {
            OracleConnection conn = new OracleConnection();
            OracleTransaction transaction = null;
            string sqlora = "";
            string sqllogos = "";

            textBox1.Text = "Iniciou...";
            Application.DoEvents();
            string err = "";
            try
            {
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS("exec IntregrarTituloCSCancel", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                string oradb = "Data Source=(DESCRIPTION="
                + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))"
                + "(CONNECT_DATA=(SERVICE_NAME=csorcl)));"
                + "User Id=csintegracao;Password=wxmj6evc8k;";

                conn.ConnectionString = oradb;
                conn.Open();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    err = dt.Rows[i]["IDTitulo"].ToString();


                    sqlora = "Insert into fatura (VALOR, NR_FATURA, DATA_VCTO, DATA_IMPORTACAO, COD_TIPOCOBRANCA, CNPJ_CPF_EMIT, cnpj_tomador, VALOR_DESCONTO, COD_OCORRENCIACONTASPAGAR, IDTitulo, TIPO_doc, DATA_EMISSAO, DATA_CANCELAMENTO) values (" + dt.Rows[i]["Valor"].ToString().Replace(",", ".") + ", " + dt.Rows[i]["Numero"].ToString() + ", TO_DATE('" + DateTime.Parse(dt.Rows[i]["DataDeVencimento"].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'),  TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'),  1, '" + dt.Rows[i]["CnpjCpf"].ToString().Replace("/", "").Replace(".", "").Replace("-", "").Trim() + "', '" + dt.Rows[i]["CNPJ_Tomador"].ToString().Replace("/", "").Replace(".", "").Replace("-", "").Trim() + "', " + dt.Rows[i]["Desconto"].ToString().Replace(",", ".") + "," + (dt.Rows[i]["CodOcor"].ToString() == "" ? "NULL" : dt.Rows[i]["CodOcor"].ToString()) + "," + dt.Rows[i]["IDTitulo"].ToString() + ", 'CTE', TO_DATE('" + DateTime.Parse(dt.Rows[i]["DataDeEmissaoFatura"].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'), TO_DATE('" + DateTime.Parse(dt.Rows[i]["DataDeCancelamento"].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss') )";
                    sqllogos = "Update Titulo set EnviandoCancelamento= 'Enviado: " + DateTime.Now + ".' where IdTitulo=" + dt.Rows[i]["IdTitulo"].ToString() + ";";

                    textBox1.Text = "Importando a Fatura : " + dt.Rows[i]["Numero"].ToString();
                    Application.DoEvents();

                    transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    OracleCommand oracleCommand = new OracleCommand(sqlora, conn);
                    oracleCommand.Transaction = transaction;
                    oracleCommand.ExecuteNonQuery();



                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqllogos, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    textBox1.Text = "Finalizou Importação do Cancelamento da Fatura : " + dt.Rows[i]["Numero"].ToString();
                    Application.DoEvents();
                    transaction.Commit();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    sqllogos = "Update Titulo set EnviadoCS= 'Erro: " + ex.Message + "' where IdTitulo=" + err + ";";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqllogos, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }
                catch (Exception)
                {
                }

                transaction.Rollback();

                string x = ex.Message;

                DataSet ds = new DataSet();
                if (File.Exists("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml"))
                    ds.ReadXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdTitulo");
                    dt.Columns.Add("Erro");
                    ds.Tables.Add(dt);
                }

                DataRow row = ds.Tables[0].NewRow();
                row[0] = err;
                row[1] = ex.Message + sqlora;
                ds.Tables[0].Rows.Add(row);


                File.Delete("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
                ds.WriteXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");

                textBox1.Text = ex.Message;
                Application.DoEvents();

            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public void EnviarFatura()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            //string cnx = "Data Source=192.168.10.4;Initial Catalog=VEX;User ID=sa;Password=WERasd27;  ";
            OracleConnection conn = new OracleConnection();
            OracleTransaction transaction = null;
            string sqlora = "";
            string sqllogos = "";
            bool abriuTransacao = false;

            textBox1.Text = "Iniciou...";
            Application.DoEvents();
            string err = "";
            try
            {
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS("Exec IntregrarTituloCS_V2", cnx);

                string oradb = "Data Source=(DESCRIPTION=" +
                 "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))" +
                 "(CONNECT_DATA=(SERVICE_NAME=csorcl)));" +
                 "User Id=csintegracao;Password=wxmj6evc8k;";

                ////if (checkBox1.Checked) // se teste
                ////{ 
                //string oradb = "Data Source=(DESCRIPTION="
                // + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.32.206)(PORT=1521)))"
                // + "(CONNECT_DATA=(SERVICE_NAME=csorcl)));"
                // + "User Id=sistranet;Password=MKepjDkQUYd6;";
                ////}

                conn.ConnectionString = oradb;
                conn.Open();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    err = dt.Rows[i]["IDTitulo"].ToString();

                    string aux = "Select  T.IDTitulo, T.Valor ValorFatura, d.Numero, d.DataDeEmissao, d.TipoDeDocumento,  case d.TipoDeDocumento when 'NOTA FISCAL' THEN sum(distinct isnull(D.ValorDoServico,0)) else sum(distinct isnull(DF.Frete,0)) end  ValorDoFrete,  d.Serie , td.IDTituloDuplicata, de.IdNota, cast(t.DataDeEmissao as date)  DataDeEmissaoFatura " +
                    " From Titulo T with (nolock) " +
                    " Inner Join TituloDuplicata TD  with (nolock) on TD.IdTitulo = T.IDTitulo " +
                    " Inner Join TituloDocumento TDI with (nolock) on TDI.IdTitulo = T.IdTitulo " +
                    " left join Documento d  with (nolock) on d.Iddocumento = tdi.Iddocumento " +
                    " left Join DocumentoFrete DF  with (nolock)on DF.IdDocumento = TDI.IdDocumento " +
                    " left join DocumentoEletronico de with (nolock) on de.idDocumento = d.IDDocumento   " +
                    " where T.IDTitulo =  " + dt.Rows[i]["IdTitulo"].ToString() + " and isnull(IdNota, '') <> ''  and d.Ativo='SIM' and cstatus = '100'" +
                    " group by T.IDTitulo, T.Valor , d.Numero, td.Desconto, d.DataDeEmissao, d.Serie,  d.Serie,td.IDTituloDuplicata, de.IdNota, d.TipoDeDocumento, cast(t.DataDeEmissao as date)  having  (case d.TipoDeDocumento when 'NOTA FISCAL' THEN sum(distinct isnull(D.ValorDoServico,0)) else sum(isnull(DF.Frete,0)) end) >0";

                    DataTable dtsocs = Sistran.Library.GetDataTables.RetornarDataTableWS(aux, cnx);

                    if (dtsocs.Rows.Count == 0)
                    {
                        Sistran.Library.GetDataTables.ExecutarComandoSql("Update Titulo set EnviadoCS='Erro' where IdTitulo=" + dt.Rows[i]["IdTitulo"].ToString(), cnx);
                        continue;
                    }

                    //1) Verifica os valor do xml x valor do frete
                    bool erro = false;
                    decimal vl_docs = 0;
                    for (int w = 0; w < dtsocs.Rows.Count; w++)
                    {
                        vl_docs = 0;
                        if (dtsocs.Rows[w]["TipoDeDocumento"].ToString().ToUpper() == "CONHECIMENTO")
                        {
                            if (dtsocs.Rows[w]["IdNota"].ToString() == "")
                            {
                                Sistran.Library.GetDataTables.ExecutarComandoSql("Update Titulo set EnviadoCS='Erro: Chave do CTE/Nf em branco (nfs)' where IdTitulo=" + dt.Rows[i]["IdTitulo"].ToString(), cnx);
                                erro = true;
                                continue;
                            }

                            try
                            {

                                var f = Sistran.Library.GetDataTables.RetornarDataTableWS("exec prc_acertaFatura '" + dtsocs.Rows[w]["IdNota"].ToString() + "'", cnx);
                                //VerificarCentavosXmlCte(dtsocs.Rows[w]["IdNota"].ToString());

                                if (f.Rows.Count > 0)
                                {

                                    if (f.Rows[0]["ValorCTe"].ToString() != f.Rows[0]["Frete"].ToString().Replace(",", "."))
                                    {
                                        Sistran.Library.GetDataTables.ExecutarComandoSql("Update DocumentoFrete set Frete='" + f.Rows[0]["ValorCTe"].ToString() + "' where Iddocumento=" + f.Rows[0]["IdDocumento"].ToString() + "; Update Titulo set EnviadoCS = 'Acertar Valor da Fatura' where IdTitulo = " + dt.Rows[i]["IdTitulo"].ToString(), cnx);
                                        erro = true;
                                    }
                                }
                                else
                                {
                                    Sistran.Library.GetDataTables.ExecutarComandoSql("Update Titulo set EnviadoCS='Erro: Nao encoutrou o Cte' where IdTitulo=" + dt.Rows[i]["IdTitulo"].ToString(), cnx);
                                    erro = true;
                                    continue;

                                }
                            }
                            catch (Exception)
                            {
                                if (VerificarCentavosXmlCte(dtsocs.Rows[w]["IdNota"].ToString()) == true)
                                {
                                    Sistran.Library.GetDataTables.ExecutarComandoSql("Update Titulo set EnviadoCS='Erro: verificar Centavos' where IdTitulo=" + dt.Rows[i]["IdTitulo"].ToString(), cnx);
                                    erro = true;
                                    continue;
                                }
                            }


                            //2) Verifica o Valor da somatoria dos ctes  x Valor Fatura
                            vl_docs = decimal.Parse(dtsocs.Compute("SUM(ValorDoFrete)", "IDTitulo=" + dt.Rows[i]["IdTitulo"].ToString()).ToString().Replace(".", ","));

                            if (w < 3)
                            {
                                if (vl_docs != (decimal.Parse(dt.Rows[i]["Valor"].ToString().Replace(".", ","))))
                                {
                                    if ((vl_docs - (decimal.Parse(dt.Rows[i]["Valor"].ToString().Replace(".", ",")))) != 0)
                                    {
                                        Sistran.Library.GetDataTables.ExecutarComandoSql("Update Titulo set EnviadoCS='Erro: Valor diverente' where IdTitulo=" + dt.Rows[i]["IdTitulo"].ToString(), cnx);
                                        erro = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            string nf = "";
                        }

                    }



                    if (erro)
                    {
                        //throw new Exception("Erro valor xml com cte");

                        sqllogos = "Update Titulo set EnviadoCS= 'Erro: Erro valor xml com cte ' where IdTitulo=" + err + ";";
                        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqllogos, cnx);

                        continue;
                    }
                    sqlora = "Insert into CSINTEGRACAO.fatura (VALOR, NR_FATURA, DATA_VCTO, DATA_IMPORTACAO, COD_TIPOCOBRANCA, CNPJ_CPF_EMIT, cnpj_tomador, VALOR_DESCONTO, COD_OCORRENCIACONTASPAGAR, IDTitulo, TIPO_doc, DATA_EMISSAO) values (" + dt.Rows[i]["Valor"].ToString().Replace(",", ".") + ", " + dt.Rows[i]["Numero"].ToString() + ", TO_DATE('" + DateTime.Parse(dt.Rows[i]["DataDeVencimento"].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'),  TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'),  1, '" + dt.Rows[i]["CnpjCpf"].ToString().Replace("/", "").Replace(".", "").Replace("-", "").Trim() + "', '" + dt.Rows[i]["CNPJ_Tomador"].ToString().Replace("/", "").Replace(".", "").Replace("-", "").Trim() + "', " + dt.Rows[i]["Desconto"].ToString().Replace(",", ".") + "," + (dt.Rows[i]["CodOcor"].ToString() == "" ? "NULL" : dt.Rows[i]["CodOcor"].ToString()) + "," + dt.Rows[i]["IDTitulo"].ToString() + ", '" + (dtsocs.Rows[0]["TipoDeDocumento"].ToString() == "CONHECIMENTO" ? "CTE" : "NFE") + "', TO_DATE('" + DateTime.Parse(dt.Rows[i]["DataDeEmissaoFatura"].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss') )";
                    sqllogos = "Update Titulo set EnviadoCS= 'Enviado: " + DateTime.Now + ".' where IdTitulo=" + dt.Rows[i]["IdTitulo"].ToString() + ";";

                    textBox1.Text = "Importando a Fatura : " + dt.Rows[i]["Numero"].ToString();
                    Application.DoEvents();

                    transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    OracleCommand oracleCommand = new OracleCommand(sqlora, conn);
                    oracleCommand.Transaction = transaction;
                    abriuTransacao = true;
                    oracleCommand.ExecuteNonQuery();

                    sqlora = "select max(ID_FATURA) from CSINTEGRACAO.Fatura t";
                    oracleCommand = new OracleCommand(sqlora, conn);
                    var c = oracleCommand.ExecuteScalar();


                    for (int w = 0; w < dtsocs.Rows.Count; w++)
                    {
                        if (dtsocs.Rows[0]["TipoDeDocumento"].ToString() == "CONHECIMENTO")
                        {
                            sqlora = "Insert Into CSINTEGRACAO.DETALHE_FATURA (NR_CTE, DATA_EMISSAO, VALOR, ID_FATURA, SERIE_NF, IDTITULODUPLICATA, CHAVE) Values(" + dtsocs.Rows[w]["Numero"].ToString() + ", TO_DATE('" + DateTime.Parse(dtsocs.Rows[w]["DataDeEmissao"].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'), " + dtsocs.Rows[w]["ValorDoFrete"].ToString().Replace(",", ".") + "," + c + ", '" + int.Parse(dtsocs.Rows[w]["IdNota"].ToString().Substring(22, 3)).ToString() + "', " + dtsocs.Rows[w]["IDTituloDuplicata"].ToString() + ", '" + dtsocs.Rows[w]["IdNota"].ToString() + "')";
                        }
                        else
                            sqlora = "Insert Into CSINTEGRACAO.DETALHE_FATURA (NR_NF, DATA_EMISSAO, VALOR, ID_FATURA, SERIE_NF, IDTITULODUPLICATA, CHAVE) Values(" + dtsocs.Rows[w]["Numero"].ToString() + ", TO_DATE('" + DateTime.Parse(dtsocs.Rows[w]["DataDeEmissao"].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'), " + dtsocs.Rows[w]["ValorFatura"].ToString().Replace(",", ".") + "," + c + ", '" + dtsocs.Rows[w]["Serie"].ToString().Trim() + "', " + dtsocs.Rows[w]["IDTituloDuplicata"].ToString() + ", '" + dtsocs.Rows[w]["IdNota"].ToString() + "')";

                        oracleCommand = new OracleCommand(sqlora, conn);
                        oracleCommand.Transaction = transaction;
                        oracleCommand.ExecuteNonQuery();
                    }

                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqllogos, cnx);

                    textBox1.Text = "Finalizou Importação da Fatura : " + dt.Rows[i]["Numero"].ToString();
                    Application.DoEvents();
                    transaction.Commit();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    sqllogos = "Update Titulo set EnviadoCS= 'Erro: " + ex.Message.Replace("'", "´") + "' where IdTitulo=" + err + ";";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqllogos, cnx);
                    if (abriuTransacao)
                        transaction.Rollback();

                }
                catch (Exception)
                {
                }


                string x = ex.Message;

                //DataSet ds = new DataSet();
                //if (File.Exists("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml"))
                //    ds.ReadXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");                
                //else
                //{
                //    DataTable dt = new DataTable();
                //    dt.Columns.Add("IdTitulo");
                //    dt.Columns.Add("Erro");
                //    ds.Tables.Add(dt);
                //}

                //DataRow row = ds.Tables[0].NewRow();
                //row[0] = err;
                //row[1] = ex.Message + sqlora;
                //ds.Tables[0].Rows.Add(row);


                //File.Delete("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
                //ds.WriteXml("erros" + DateTime.Now.ToString("yyyyMMdd") + ".xml");

                textBox1.Text = ex.Message;
                Application.DoEvents();

            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            while (true)
            {
                EnviarDocumentos();

            }

          //ConfirmaConferenciaGiroTrade();

            //MessageBox.Show("Test");
            //for (int i = 0; i < 1000; i++)
            //{
            //    AcertarNotasNoStistranetDoComprovei();

            //}
        }


        public bool VerificarCentavosXmlCte(string idNota)
        {
            bool ret = false;

            string sql = "select idnota, df.Frete, IDDocumentoFrete, ultimoarquivoxml " +
                "from DocumentoEletronico de " +
                "inner join DocumentoFrete df on df.IDDocumento = de.IdDocumento " +
                "where de.IdNota ='" + idNota + "'";
            DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            string upd = "";

            try
            {
                DataSet dataSet = new DataSet();

                using (StringReader reader = new StringReader(d.Rows[0]["ultimoarquivoxml"].ToString()))
                {
                    dataSet.ReadXml(reader);

                    if (d.Rows[0]["Frete"].ToString().Replace(",", ".") != dataSet.Tables["vPrest"].Rows[0]["vTPrest"].ToString())
                    {
                        ret = true;
                        upd = "update documentofrete set Frete='" + dataSet.Tables["vPrest"].Rows[0]["vTPrest"].ToString() + "' where IDDocumentoFrete='" + d.Rows[0]["IDDocumentoFrete"].ToString() + "'";
                        Sistran.Library.GetDataTables.RetornarDataTableWS(upd + ";Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                }

            }
            catch (Exception)
            {
                return true;
            }


            return ret;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;


            string sql = "select idnota, df.Frete, IDDocumentoFrete, ultimoarquivoxml " +
                "from DocumentoEletronico de " +
                "inner join DocumentoFrete df on df.IDDocumento = de.IdDocumento " +
                "where df.IdDocumento  in(select IdDocumento from TituloDocumento where IDTitulo in(3633983))";
            DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            string upd = "";
            float somaXml = 0;
            float somaDf = 0;

            for (int i = 0; i < d.Rows.Count; i++)
            {
                try
                {
                    DataSet dataSet = new DataSet();

                    using (StringReader reader = new StringReader(d.Rows[i]["ultimoarquivoxml"].ToString()))
                    {
                        dataSet.ReadXml(reader);

                        if (d.Rows[i]["Frete"].ToString().Replace(",", ".") != dataSet.Tables["vPrest"].Rows[0]["vTPrest"].ToString())
                        {
                            upd += "update documentofrete set Frete='" + dataSet.Tables["vPrest"].Rows[0]["vTPrest"].ToString() + "' where IDDocumentoFrete='" + d.Rows[i]["IDDocumentoFrete"].ToString() + "'; ";
                        }
                        else
                        {
                            somaXml += (float.Parse(dataSet.Tables["vPrest"].Rows[0]["vTPrest"].ToString()));
                            somaDf += (float.Parse(dataSet.Tables["vPrest"].Rows[0]["vTPrest"].ToString()));
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }




            MessageBox.Show("Test");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            EnviarDocumentos();
            //TesteWs.ws ws = new TesteWs.ws();
            //var x = ws.Estoque("321500", "321500");

            //System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(ws.GetType());

            //using (MemoryStream ms = new MemoryStream())
            //using (StreamReader sr = new StreamReader(ms))
            //{
            //    ser.Serialize(ms, ws);
            //    ms.Seek(0, 0);
            //    var r = sr.ReadToEnd();
            //}

        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            for (int i = 0; i < 10; i++)
            {
                EnviarImagemS3();

            }

        }

        private void EnviarImagemS3()
        {
            try
            {
                textBox1.Text = "Enviando S3";

                string sql = " Select top 100 idDocumentoOcorrenciaArquivo, CnpjCpf CNPJ, Arquivo ";
                sql += " from DocumentoOcorrenciaArquivo doa with(nolock) ";
                sql += " inner join DocumentoOcorrencia do with(nolock) on do.IDDocumentoOcorrencia = doa.IDDocumentoOcorrencia ";
                sql += " Inner join Documento d with(nolock) on d.IDDocumento = do.IDDocumento ";
                sql += " Inner join Cadastro c with(nolock) on c.IDCadastro = d.IdRemetente ";
                sql += " where DataOcorrencia >= '2020-07-01' ";
                sql += " and IdFacility is null";
                DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                S3 s3 = new S3();
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    var guid = Guid.NewGuid();

                    s3._bucketName = "facilityimagem";
                    s3._keyName = d.Rows[i]["CNPJ"].ToString().Substring(0, 10) + "/" + guid + ".jpg";

                    var m = s3.UploadFileSystemAsync((byte[])d.Rows[i]["Arquivo"]);

                    textBox1.Text = d.Rows[i]["CNPJ"].ToString().Substring(0, 10) + " / " + guid.ToString() + ".jpg";
                    Application.DoEvents();

                    sql = "Update DocumentoOcorrenciaArquivo set IdFacility='" + guid + "' where  IdDocumentoOcorrenciaArquivo= " + d.Rows[i]["IdDocumentoOcorrenciaArquivo"].ToString();
                    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //EnviarFaturaCancelada();


            for (int i = 0; i < 10000; i++)
            {
                EnviarFatura();
            }

            string sql = "select IDFilial, Tipo, Numero,Serie, Count(*)  quantidade from Titulo t Inner join TituloDuplicata td on td.IDTitulo = t.IDTitulo where DataDeVencimento >= '2020-01-01' and t.PagarReceber = 'RECEBER'  and t.Ativo = 'SIM' Group by IDFilial, Tipo, Numero,Serie Having Count(*) > 1";
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sql = "select * from Titulo where IDFilial=" + dt.Rows[i]["IDFilial"].ToString() + " and PagarReceber = 'RECEBER' and Ativo='SIM' and Numero='" + dt.Rows[i]["Numero"].ToString() + "' and Serie='" + dt.Rows[i]["Serie"].ToString() + "'";
                DataTable dt2 = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {
                    if (ii > 0)
                    {
                        sql = "Update Titulo set Serie = 'FAT" + ii + "' where IdTitulo=" + dt2.Rows[ii]["IdTitulo"].ToString();
                        Sistran.Library.GetDataTables.RetornarDataTableWS(sql + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            for (int i = 0; i < 1000; i++)
            {
                //EnviarDocumentosCancelados();
                //EnviarDocumentos();
                //EnviarFatura();                
                Comprovei();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //AcertarNotasNoStistranetDoComprovei();

            for (int i = 0; i < 100000; i++)
            {
                EnviarFatura();
            }
            Application.Exit();
            MessageBox.Show("ok");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            for (int i = 0; i < 100; i++)
            {
                EnviarBaixaFatura();
            }
        }

        public void EnviarBaixaFatura()
        {
            timer1.Enabled = false;
            string oradb = "Data Source=(DESCRIPTION=";
            //+ "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))"
            //+ "(CONNECT_DATA=(SERVICE_NAME=csorcl)));"
            //+ "User Id=csintegracao;Password=wxmj6evc8k;";

            //if (checkBox1.Checked) // se teste
            //{ 
            oradb = "Data Source=(DESCRIPTION="
            + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.32.206)(PORT=1521)))"
            + "(CONNECT_DATA=(SERVICE_NAME=csorcl)));"
            + "User Id=sistranet;Password=MKepjDkQUYd6;";
            //}




            try
            {
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS("Exec PRC_INTEGRAR_CS_BAIXA_TITULO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                OracleConnection conn = new OracleConnection();
                OracleTransaction transaction = null;
                string sqlora = "";
                string sqllogos = "";

                textBox1.Text = "Iniciou...";
                Application.DoEvents();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        sqlora = "Insert into csintegracao.baixa_fatura_automatica(DATA_BAIXA, VALOR_BAIXA, COD_BANCO, COD_AGENCIA, COD_CONTABANCARIA, IDTITULO, IDTITULODUPLICATA)";
                        sqlora += " VALUES (TO_DATE('" + DateTime.Parse(dt.Rows[i]["DataDeLiquidacao"].ToString()).ToString("dd/MM/yyyy") + "'), " + dt.Rows[i]["ValorPagoAcumulado"].ToString().Replace(",", ".") + ", '" + dt.Rows[i]["CodigoBanco"].ToString() + "','" + dt.Rows[i]["Agencia"].ToString() + "', '" + dt.Rows[i]["Conta"].ToString() + "', " + dt.Rows[i]["IdTitulo"].ToString() + ", " + dt.Rows[i]["IdTituloDuplicata"].ToString() + ")";

                        conn.ConnectionString = oradb;
                        conn.Open();
                        OracleCommand oracleCommand = new OracleCommand(sqlora, conn);

                        string sqlora_ = "select count(*) from CSINTEGRACAO.Fatura t where t.IdTitulo=" + dt.Rows[i]["IdTitulo"].ToString();
                        oracleCommand = new OracleCommand(sqlora_, conn);
                        var c = oracleCommand.ExecuteScalar();

                        if (c.ToString() == "0")
                        {
                            conn.Close();
                            sqllogos = "Update TituloDuplicata set EnviadoBaixaCS='erro: Fatura nao existe na CS' where IdTituloDuplicata=" + dt.Rows[i]["IdTituloDuplicata"].ToString();
                            Sistran.Library.GetDataTables.RetornarDataTableWS(sqllogos + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                            continue;
                        }

                        transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        oracleCommand = new OracleCommand(sqlora, conn);
                        oracleCommand.Transaction = transaction;
                        oracleCommand.ExecuteNonQuery();
                        textBox1.Text = "Finalizou Importação da Baixa Fatura : " + dt.Rows[i]["IDTitulo"].ToString();
                        Application.DoEvents();
                        transaction.Commit();
                        conn.Close();

                        sqllogos = "Update TituloDuplicata set EnviadoBaixaCS='Enviado " + DateTime.Now + "' where IdTituloDuplicata=" + dt.Rows[i]["IdTituloDuplicata"].ToString();
                        Sistran.Library.GetDataTables.RetornarDataTableWS(sqllogos + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    }
                    catch (Exception xx)
                    {
                        transaction.Rollback();
                        sqllogos = "Update TituloDuplicata set EnviadoBaixaCS='" + xx.Message + "' where IdTituloDuplicata=" + dt.Rows[i]["IdTituloDuplicata"].ToString();
                        Sistran.Library.GetDataTables.RetornarDataTableWS(sqllogos + "; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            for (int i = 0; i < 100; i++)
            {
                EnviarInutilizados();

            }
        }

        private void EnviarInutilizados()
        {
            try
            {
                string sql = "select * from DocumentoEletronico where CStatus='102' and EnviadoCs is null and UltimoArquivoXml is not null";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    XmlDocument x = new XmlDocument();
                    x.LoadXml(dt.Rows[i]["UltimoArquivoXml"].ToString());


                    string numero = x.DocumentElement.GetElementsByTagName("nCTIni").Item(0).InnerText;
                    string numeroFinal = x.DocumentElement.GetElementsByTagName("nCTIni").Item(0).InnerText;
                    string cod_modelo = "57";
                    string serie = x.DocumentElement.GetElementsByTagName("serie").Item(0).InnerText;
                    string datas = ((XmlElement)x.DocumentElement.GetElementsByTagName("infInut").Item(1)).GetElementsByTagName("dhRecbto").Item(0).InnerText;
                    string protocolo = ((XmlElement)x.DocumentElement.GetElementsByTagName("infInut").Item(1)).GetElementsByTagName("nProt").Item(0).InnerText;
                    string motivo = x.DocumentElement.GetElementsByTagName("xJust").Item(0).InnerText; //xJust((XmlElement)x.DocumentElement.GetElementsByTagName("infInut").Item(1)).GetElementsByTagName("xMotivo").Item(0).InnerText;
                    string cnpjEmit = x.DocumentElement.GetElementsByTagName("CNPJ").Item(0).InnerText;


                    string sqlora = "INSERT INTO CSINTEGRACAO.NFE_INUTILIZACAO_IMPORTACA(NR_INICIAL, COD_MODELODOCUMENTO , SERIE, DATA_SOLICITACAO, DATA_INUTILIZACAO, NR_PROTOCOLO, MOTIVO_INUTILIZACAO, CNPJ_CPF_EMITENTE,  ARQUIVO  ) VALUES ('" + numero + "', '" + cod_modelo + "', '" + serie + "', TO_DATE('" + DateTime.Parse(datas).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'), TO_DATE('" + DateTime.Parse(datas).ToString("yyyy/MM/dd HH:mm:ss") + "', 'yyyy/mm/dd hh24:mi:ss'), '" + protocolo + "', '" + motivo + "', '" + cnpjEmit + "', :arr)";

                    ///base de teste
                    string oradb = "Data Source=(DESCRIPTION=" +
                    "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVICE_NAME=csorcl)));" +
                    "User Id=csintegracao;Password=wxmj6evc8k;";
                    OracleConnection conn = new OracleConnection();
                    OracleTransaction transaction;

                    conn.ConnectionString = oradb;
                    conn.Open();



                    transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                    textBox1.Text = "Inutilizados";
                    Application.DoEvents();
                    try
                    {
                        OracleParameter par = new OracleParameter(":arr", OracleDbType.Clob);
                        par.Value = x.InnerXml;


                        OracleCommand oracleCommand = new OracleCommand(sqlora, conn);
                        oracleCommand.Parameters.Add(par);

                        oracleCommand.Transaction = transaction;
                        oracleCommand.ExecuteNonQuery();
                        transaction.Commit();

                        Sistran.Library.GetDataTables.RetornarDataTableWS("Update DocumentoEletronico set EnviadoCs='Enviado " + DateTime.Now + "' where IdDocumentoEletronico=" + dt.Rows[i]["IdDocumentoEletronico"].ToString() + "; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                    catch (Exception exx)
                    {
                        transaction.Rollback();
                    }

                }

            }
            catch (Exception)
            {

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

            timer1.Enabled = false;
            //AcertarNotasNoStistranetDoComprovei();
          while(true)
            {
                Comprovei();
                textBox1.Text = "Rodando " + DateTime.Now;
                Application.DoEvents();
            }

            MessageBox.Show("Test");
            return;

            //for (int i = 0; i < 20; i++)
            //{
            //    EnviarDocumentos();
            //}


            //            string cnx = "Data Source=192.168.10.20;Initial Catalog=FACILITY00002;Persist Security Info=True;User ID=sa;Password=WERasd27;MultipleActiveResultSets=true";

            //            DataTable dtPC =  Sistran.Library.GetDataTables.RetornarDataTableWS("Select * from ProdutoCliente where Ativo=1", cnx);

            //            DataTable dtpd = Sistran.Library.GetDataTables.RetornarDataTableWS("Select * from ProdutoDivisao where Ativo=1", cnx);


            //            for (int i = 0; i < dtPC.Rows.Count ; i++)
            //            {
            //                for (int ii = 0; ii < dtpd.Rows.Count; ii++)
            //                {
            //                    /*
            //                     * 
            //                     * Id
            //IdProdutoCliente
            //IdProdutoDivisao
            //Saldo
            //DataDeCadastro
            //Ativo

            //                    */

            //                    string sql = "insert into ProdutoDivisaoSaldo values ('"+Guid.NewGuid()+"', '"+ dtPC.Rows[i]["id"].ToString() +"', '"+ dtpd.Rows[ii]["id"].ToString() + "', 0, getdate()-45, 1) ";
            //                    Sistran.Library.GetDataTables.RetornarDataTableWS(sql + "; select 1" , cnx);

            //                }
            //            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try
            {


                string oradb = "Data Source=(DESCRIPTION=" +
                              "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.208)(PORT=1521)))" +
                              "(CONNECT_DATA=(SERVICE_NAME=csorcl)));" +
                              "User Id=csintegracao;Password=wxmj6evc8k;";
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = oradb;
                conn.Open();

                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "select Chave from Importacao_Xml where SUBSTR(chave,3,4) = '2011'";
                cmd.Connection = conn;
                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);



                conn.Close();

                //******************************


                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    string sql = "insert into chaves2011 values ('" + dataset.Tables[0].Rows[i][0].ToString() + "')";
                    Sistran.Library.GetDataTables.RetornarDataTableWS(sql + "; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                }
                MessageBox.Show("Test");
            }
            catch (Exception)
            {
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            for (int i = 0; i < 300; i++)
            {
                Comprovei();

                if (i == 200)
                    i = 0;

            }
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

        private void button14_Click(object sender, EventArgs e)
        {
            // GerarArquivoPandurata();
            timer1.Enabled = false;

            this.BackColor = System.Drawing.Color.Yellow;
            Application.DoEvents();

            while (true)
            {
                Comprovei();
            }


            for (int i = 0; i < 150; i++)
            {
                Comprovei();

               
            }


            for (int i = 0; i < 150; i++)
            {
                Comprovei();


            }



            for (int i = 0; i < 150; i++)
            {
                Comprovei();


            }
        }

        private void GerarArquivoPandurata()
        {
            try
            {
                string sql = "exec prc_canhotos_pandurata";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        sql = "Select arquivo from DocumentoOcorrenciaArquivo where idDocumentoOcorrenciaArquivo=" + dt.Rows[i]["IdDocumentoOcorrenciaArquivo"].ToString();
                        DataTable dti = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        if (dti.Rows.Count == 0)
                            throw new Exception("");

                        byte[] bytes = (byte[])dti.Rows[0]["Arquivo"];
                        File.WriteAllBytes(@"c:\tmp\" + dt.Rows[i]["numero"].ToString() + ".jpg", bytes);

                        sql = "Update DocumentoOcorrenciaArquivo set GeradoArquivo=1 where IdDocumentoOcorrenciaArquivo=" + dt.Rows[i]["IdDocumentoOcorrenciaArquivo"].ToString();
                        Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                    }
                    catch (Exception ex)
                    {
                        sql = "Update DocumentoOcorrenciaArquivo set GeradoArquivo=1 where IdDocumentoOcorrenciaArquivo=" + dt.Rows[i]["IdDocumentoOcorrenciaArquivo"].ToString();
                        Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        continue;
                    }
                }
            }
            catch (Exception)
            {
            }


        }
    }
}