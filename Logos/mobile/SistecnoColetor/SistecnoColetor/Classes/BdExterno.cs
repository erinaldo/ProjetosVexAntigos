using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SistecnoColetor.Classes
{
    public class ParametrosProcedures
    {
        public string tipoDeDados { get; set; }
        public string nomePar { get; set; }
        public string valorPar { get; set; }
        public string direcao { get; set; }

    }

    public static class BdExterno
    {
        public static DataTable RetornarDataTableProcedure(string NomeProcedure, List<ParametrosProcedures> parametros, string Conx)
        {
            DataSet ds = new DataSet();

            System.Data.SqlClient.SqlConnection cn = new SqlConnection(VarGlobal.Conexao);
            System.Data.SqlClient.SqlCommand cd = new SqlCommand();

            cn.ConnectionString = VarGlobal.Conexao;
            cd.CommandText = NomeProcedure;
            cd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[parametros.Count];

            for (int i = 0; i < parametros.Count; i++)
            {
                System.Data.SqlClient.SqlParameter par = new System.Data.SqlClient.SqlParameter();
                par.ParameterName = "@" + parametros[i].nomePar;

                par.Value = parametros[i].valorPar;

                if (parametros[i].direcao != null && parametros[i].direcao != "")
                {
                    par.Direction = ParameterDirection.Output;
                }

                switch (parametros[i].tipoDeDados)
                {
                    case "string":
                        par.DbType = DbType.String;
                        break;
                    case "int":
                        par.DbType = DbType.Int32;
                        break;
                    case "datetime":
                        par.DbType = DbType.DateTime;
                        break;

                    default:
                        par.DbType = DbType.String;
                        break;
                }
                param[i] = par;
                cd.Parameters.Add(par);
            }


            cd.Connection = cn;
            cn.Open();
            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cd);
                da.Fill(ds);
                return ds.Tables[0];
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }          

        }

        public static int RetornarIDTabela(string sTabela)
        {
            int ret = 0;


            System.Data.SqlClient.SqlConnection cn = new SqlConnection(VarGlobal.Conexao);
            System.Data.SqlClient.SqlCommand cd = new SqlCommand();
            
            cn.ConnectionString = VarGlobal.Conexao;
            cd.CommandText = "GERAR_ID_TABELA";
            cd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter();
            p1.ParameterName = "@NOMEDATABELA";
            p1.DbType = DbType.String;
            p1.Value = sTabela;

            cd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter(); ;
            p2.ParameterName = "@RETORNAR_ID_TABELA";
            p2.DbType = DbType.Int32;
            p2.Value = "0";
            p2.Direction = ParameterDirection.Output;

            cd.Parameters.Add(p2);


            cd.Connection = cn;
            cn.Open();
            try
            {
                cd.ExecuteNonQuery();
                ret = int.Parse(cd.Parameters["@RETORNAR_ID_TABELA"].Value.ToString());
            }
            catch(Exception ex)
            {               
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ret;
        }

        /// <summary>
        /// Retona um dataTable correnspondente a um sql
        /// </summary>
        /// <param name="isql"></param>
        /// <param name="cnx"></param>
        /// <returns></returns>
        public static DataTable RetornarDT(string isql, string cnx)
        {
            //cnx = cnx.Replace("Initial Catalog", "Database").Replace(",1433", "") ;

            System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection();
            System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand();

            myConn.ConnectionString = cnx;
            myCommand.Connection = myConn;
            myCommand.CommandText =isql;
            myCommand.CommandType = CommandType.Text;



            DataTable dt = new DataTable();
            
            try
            {
                myConn.Open();
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(myCommand);
                da.Fill(dt);
                return dt;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                    myCommand.Dispose();
                    myConn.Dispose();
                }
            }
        }
        

        public static void Executar(string isql, string cnx)
        {
            System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection(cnx);
            System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(isql, myConn);
            myCommand.CommandType = CommandType.Text;            
            try
            {

                myConn.Open();
                myCommand.ExecuteNonQuery();

            
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();

            }
        }


        public static void ExecutarTrans(string isql, string cnx)
        {
            System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection(cnx);
            System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(isql, myConn);
            myConn.Open();

            System.Data.SqlClient.SqlTransaction tran = myConn.BeginTransaction();
            try
            {
                myCommand.Transaction = tran;
                myCommand.CommandType = CommandType.Text;
                myCommand.ExecuteNonQuery();
                tran.Commit();
            }
            catch (System.Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();

            }
        }


        public static void FinalizarConferencia(string isql, string cnx)
        {
            System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection(cnx);
            System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(isql, myConn);
            myConn.Open();

            System.Data.SqlClient.SqlTransaction tran = myConn.BeginTransaction();

            myCommand.CommandType = CommandType.Text;
            try
            {

                myCommand.CommandText = isql;
                myCommand.Transaction = tran;
                myCommand.ExecuteNonQuery();
               
                tran.Commit(); // MUDAR PARA COMMIT

            }
            catch (System.Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();

            }
        }

        public static void ExecutarSaidaAposConferencia(string isql, string idRomaneio,  string cnx)
        {
            System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection(cnx);
            System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(isql, myConn);
            myConn.Open();

            string UltimaUa = "";
            string UltimaMovimentacao = "";
            string UltimaOrigem = "";

            System.Data.SqlClient.SqlTransaction tran = myConn.BeginTransaction();

            myCommand.CommandType = CommandType.Text;
            try
            {               

                isql = " Select isnull(MI.IDMovimentacao,0) IDMovimentacao , isnull(mi.IDUnidadeDeArmazenagem, 0) IDUnidadeDeArmazenagem, mi.IDDepositoPlantaLocalizacaoOrigem, mi.IDDepositoPlantaLocalizacaoDestino, mi.Quantidade , mi.IdProdutoCliente";
                isql += " From Movimentacao M with(nolock) ";
                isql += " Inner Join MovimentacaoItem MI on MI.IDMovimentacao = M.IDMovimentacao ";
                isql += " where MI.IDRomaneio = " + idRomaneio;

                DataTable dt = Classes.BdExterno.RetornarDT(isql, VarGlobal.Conexao);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        UltimaUa = dt.Rows[i]["IDUnidadeDeArmazenagem"].ToString();
                        UltimaMovimentacao = dt.Rows[i]["IDMovimentacao"].ToString();
                        UltimaOrigem = dt.Rows[i]["IDDepositoPlantaLocalizacaoOrigem"].ToString();

                        if (dt.Rows[i]["IDUnidadeDeArmazenagem"].ToString() == "0")
                        {

                            if (dt.Rows[i]["IDDepositoPlantaLocalizacaoOrigem"].ToString() == "")
                                break;

                            isql = "  select ua.IDUnidadeDeArmazenagem, ual.Saldo  ";
                            isql += " from UnidadeDeArmazenagem ua with (nolock) ";
                            isql += " inner join UnidadeDeArmazenagemLote ual  with (nolock) on ual.IDUnidadeDeArmazenagem = ua.IDUnidadeDeArmazenagem ";
                            isql += " Inner Join Lote LT  with (nolock) on LT.IdLote = UAL.IdLote ";
                            isql += " where ua.IDDepositoPlantaLocalizacao = " + dt.Rows[i]["IDDepositoPlantaLocalizacaoOrigem"].ToString();
                            isql += " and ual.Saldo>0 ";
                            isql += " and LT.idProdutoCliente = " + dt.Rows[i]["IDProdutoCliente"].ToString();
                            isql += " order by ual.Saldo ";

                            DataTable dtuas = new DataTable();
                            dtuas = Classes.BdExterno.RetornarDT(isql, VarGlobal.Conexao);

                            int qtdTotal = int.Parse( float.Parse(dt.Rows[i]["Quantidade"].ToString()).ToString());
                            int qtdBaixada = 0;
                            int retirar = 0;
                            for (int iuas = 0; iuas < dtuas.Rows.Count; iuas++)
                            {

                                if ((qtdTotal-qtdBaixada) >= int.Parse(float.Parse(dtuas.Rows[iuas]["Saldo"].ToString()).ToString()))
                                {
                                    retirar = int.Parse(float.Parse(dtuas.Rows[iuas]["Saldo"].ToString()).ToString());
                                    qtdBaixada += retirar;
                                }
                                else
                                {
                                    retirar = (qtdTotal - qtdBaixada);
                                    qtdBaixada += retirar;
                                }
                                                                                       
                                

                                #region Estoque

                                isql = "SELECT ISNULL(IDESTOQUE, 0 ), SALDO FROM ESTOQUE with(nolock) WHERE IDPRODUTOCLIENTE =" + dt.Rows[i]["IdProdutoCliente"].ToString() + " AND IDFILIAL=" + VarGlobal.Usuario.UltimaFilial;
                                DataTable dtTemp = BdExterno.RetornarDT(isql, VarGlobal.Conexao);
                                string idEstoque = "";

                                if (dtTemp.Rows.Count > 0)
                                    idEstoque = dtTemp.Rows[0][0].ToString();

                                if (idEstoque == "" || idEstoque == "0")
                                {
                                    throw new Exception("Estoque não encontrado");
                                }
                                else
                                {
                                    if (float.Parse(dtTemp.Rows[0][1].ToString()) < float.Parse(dt.Rows[i]["Quantidade"].ToString()))
                                        throw new Exception("Saldo menor que a quantidade solicitada.");


                                    isql = "UPDATE ESTOQUE SET SALDO=SALDO-" + retirar + " WHERE IDESTOQUE=" + idEstoque;
                                    myCommand.CommandText = isql;
                                    myCommand.Transaction = tran;
                                    myCommand.ExecuteNonQuery();
                                }
                                #endregion

                                #region Lote
                                isql = "";
                                isql += " SELECT L.IDLOTE FROM LOTE L  with (nolock) ";
                                isql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL with (nolock)  ON UAL.IDLOTE = L.IDLOTE";
                                isql += " INNER JOIN UNIDADEDEARMAZENAGEM UA  with (nolock) ON UA.IDUNIDADEDEARMAZENAGEM = UAL.IDUNIDADEDEARMAZENAGEM";
                                isql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL  with (nolock) ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO";
                                isql += " WHERE IDESTOQUE = " + idEstoque;
                                isql += " AND UAL.IDUNIDADEDEARMAZENAGEM =" + dtuas.Rows[iuas]["IDUnidadeDeArmazenagem"].ToString();
                                isql += " AND UA.IDDEPOSITOPLANTALOCALIZACAO =" + dt.Rows[i]["IdDepositoplantaLocalizacaoOrigem"].ToString();


                                dtTemp = BdExterno.RetornarDT(isql, VarGlobal.Conexao);

                                string idLote = "";
                                if (dtTemp.Rows.Count > 0)
                                    idLote = dtTemp.Rows[0][0].ToString();


                                if (idLote == "" || idLote == "0")
                                {
                                    throw new Exception("Lote Não Encontrado");
                                }


                                #region UnidadeDeArmazenagemLote


                                isql = "SELECT IDUNIDADEDEARMAZENAGEMLOTE FROM UNIDADEDEARMAZENAGEMLOTE  with (nolock)  WHERE IDLOTE =" + idLote;
                                string idUALote = "";

                                dtTemp = BdExterno.RetornarDT(isql, VarGlobal.Conexao);


                                if (dtTemp.Rows.Count > 0)
                                    idUALote = dtTemp.Rows[0][0].ToString();


                                if (idUALote == "" || idUALote == "0")
                                {
                                    throw new Exception("Lote Não Encontrado");

                                }
                                else
                                {
                                    isql = "select sum(Saldo) from UnidadeDeArmazenagemLote where  IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                                    DataTable dtsaldo = BdExterno.RetornarDT(isql, VarGlobal.Conexao);

                                    if (int.Parse(float.Parse(dtsaldo.Rows[0][0].ToString()).ToString()) > 0)
                                        isql = "UPDATE UNIDADEDEARMAZENAGEMLOTE SET SALDO=SALDO-" + retirar + " WHERE IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                                    else
                                        isql = "UPDATE UNIDADEDEARMAZENAGEMLOTE SET SALDO=0 WHERE IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;


                                    myCommand.CommandText = isql;
                                    myCommand.ExecuteNonQuery();
                                }

                                #endregion
                                #endregion

                                #region EstoqueMov
                                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                                isql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoOrigem, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque, IDDepositoPlantaLocalizacaoDestino) ";
                                isql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + dt.Rows[i]["IdProdutoCliente"].ToString() + ",  " + dt.Rows[i]["IdDepositoplantaLocalizacaoOrigem"].ToString() + ", 2, " + VarGlobal.Usuario.IDUsuario + ",'SAÍDA',GetDate()," + retirar + "," + retirar + ",  (select saldo from estoque where idestoque=" + idEstoque + ")  ,NULL, " + (dt.Rows[i]["IdDepositoplantaLocalizacaoDestino"].ToString() == "" ? "NULL" : dt.Rows[i]["IdDepositoplantaLocalizacaoDestino"].ToString()) + ")";
                                myCommand.CommandText = isql;
                                myCommand.ExecuteNonQuery();
                                #endregion




                                if (qtdBaixada == qtdTotal)
                                    break; 

                            }


                        }
                        else // se tiver a ua na movimentação retirar de propria ua
                        {
                            #region Estoque

                            isql = "SELECT ISNULL(IDESTOQUE, 0 ), SALDO FROM ESTOQUE with(nolock) WHERE IDPRODUTOCLIENTE =" + dt.Rows[i]["IdProdutoCliente"].ToString() + " AND IDFILIAL=" + VarGlobal.Usuario.UltimaFilial;
                            DataTable dtTemp = BdExterno.RetornarDT(isql, VarGlobal.Conexao);
                            string idEstoque = "";

                            if (dtTemp.Rows.Count > 0)
                                idEstoque = dtTemp.Rows[0][0].ToString();

                            if (idEstoque == "" || idEstoque == "0")
                            {
                                throw new Exception("Estoque não encontrado");
                            }
                            else
                            {
                                if (float.Parse(dtTemp.Rows[0][1].ToString()) < float.Parse(dt.Rows[i]["Quantidade"].ToString()))
                                    throw new Exception("Saldo menor que a quantidade solicitada.");


                                isql = "UPDATE ESTOQUE SET SALDO=SALDO-" + int.Parse(float.Parse(dt.Rows[i]["Quantidade"].ToString()).ToString()) + " WHERE IDESTOQUE=" + idEstoque;
                                myCommand.CommandText = isql;
                                myCommand.Transaction = tran;
                                myCommand.ExecuteNonQuery();
                            }
                            #endregion

                            #region Lote
                            isql = "";
                            isql += " SELECT L.IDLOTE FROM LOTE L with(nolock)";
                            isql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDLOTE = L.IDLOTE";
                            isql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM = UAL.IDUNIDADEDEARMAZENAGEM";
                            isql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO";
                            isql += " WHERE IDESTOQUE = " + idEstoque;
                            isql += " AND UAL.IDUNIDADEDEARMAZENAGEM =" + dt.Rows[i]["IDUnidadeDeArmazenagem"].ToString();
                           // isql += " AND UA.IDDEPOSITOPLANTALOCALIZACAO =" + dt.Rows[i]["IdDepositoplantaLocalizacaoOrigem"].ToString();


                            dtTemp = BdExterno.RetornarDT(isql, VarGlobal.Conexao);

                            string idLote = "";
                            if (dtTemp.Rows.Count > 0)
                                idLote = dtTemp.Rows[0][0].ToString();


                            if (idLote == "" || idLote == "0")
                            {
                                throw new Exception("Lote Não Encontrado");
                            }
                            #endregion

                            #region UnidadeDeArmazenagemLote


                            isql = "SELECT IDUNIDADEDEARMAZENAGEMLOTE FROM UNIDADEDEARMAZENAGEMLOTE with(nolock) WHERE IDLOTE =" + idLote;
                            string idUALote = "";

                            dtTemp = BdExterno.RetornarDT(isql, VarGlobal.Conexao);


                            if (dtTemp.Rows.Count > 0)
                                idUALote = dtTemp.Rows[0][0].ToString();


                            if (idUALote == "" || idUALote == "0")
                                       throw new Exception("Lote Não Encontrado");


                            
                            else
                            {
                                //MessageBox.Show(dt.Rows[i]["IDUnidadeDeArmazenagem"].ToString() + "-Qan:" + dt.Rows[i]["Quantidade"].ToString() + " Lote:" + idLote + "Estoque:" + idEstoque + " Endere" + dt.Rows[i]["IdDepositoplantaLocalizacaoOrigem"].ToString());

                                isql = "select sum(Saldo) from UnidadeDeArmazenagemLote where  IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                                DataTable dtsaldo = BdExterno.RetornarDT(isql, VarGlobal.Conexao);

                                if (int.Parse(float.Parse(dtsaldo.Rows[0][0].ToString()).ToString()) > 0)
                                {
                                    isql = "UPDATE UNIDADEDEARMAZENAGEMLOTE SET SALDO=SALDO-" + int.Parse(float.Parse(dt.Rows[i]["Quantidade"].ToString()).ToString()) + " WHERE IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                                    myCommand.CommandText = isql;
                                    myCommand.ExecuteNonQuery();
                                }
                                else
                                {
                                    isql = "UPDATE UNIDADEDEARMAZENAGEMLOTE SET SALDO=0 WHERE IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                                    myCommand.CommandText = isql;
                                    myCommand.ExecuteNonQuery();
                                }

                            #endregion

                                #region EstoqueMov
                                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                                isql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoOrigem, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque, IDDepositoPlantaLocalizacaoDestino) ";
                                isql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + dt.Rows[i]["IdProdutoCliente"].ToString() + ",  " + dt.Rows[i]["IdDepositoplantaLocalizacaoOrigem"].ToString() + ", 2, " + VarGlobal.Usuario.IDUsuario + ",'SAÍDA',GetDate()," + int.Parse(float.Parse(dt.Rows[i]["Quantidade"].ToString()).ToString()) + "," + int.Parse(float.Parse(dt.Rows[i]["Quantidade"].ToString()).ToString()) + ",  (select saldo from estoque where idestoque=" + idEstoque + ")  ,NULL, " + (dt.Rows[i]["IdDepositoplantaLocalizacaoDestino"].ToString() == "" ? "NULL" : dt.Rows[i]["IdDepositoplantaLocalizacaoDestino"].ToString()) + ")";
                                myCommand.CommandText = isql;
                                myCommand.ExecuteNonQuery();
                                #endregion

                            }
                        }

                    }
                }
                else
                {
                    throw new Exception("Movimentação não encontada para o romaneio: " + idRomaneio);
                }


                isql = " UPDATE MOVIMENTACAO SET ESTOQUEPROCESSADO='SIM' WHERE IDMOVIMENTACAO = (SELECT TOP 1 IDMOVIMENTACAO FROM MOVIMENTACAOITEM WHERE IDROMANEIO=" + idRomaneio + ")";
                myCommand.CommandText = isql;
                myCommand.ExecuteNonQuery();

                tran.Commit(); // MUDAR PARA COMMIT
                

            }
            catch (System.Exception ex)
            {
                tran.Rollback();
                throw new Exception("Problema para baixar o Romaneio:" + idRomaneio + " - UA: " + UltimaUa + " - UltimaMovimentação:" + UltimaMovimentacao + " - Endereco de Origem: " +UltimaOrigem );
                
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();

            }
        }


        public static string ExecutarComRetorno(string isql, string cnx)
        {
            System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection(cnx);
            System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(isql, myConn);
            myCommand.CommandType = CommandType.Text;
            string ret = "";
            try
            {

                myConn.Open();
                ret = myCommand.ExecuteScalar().ToString();       

                return ret;

            }
            catch (System.Exception)
            {
                //throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
                return "0";
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();

            }
        }
    }
    public static class Combo
    {
        public static void CarregarCombo(DataTable fonteDeDados, ref ComboBox dp, Boolean InserirSelecione, string CampoValue, string CampoText)
        {
            dp.Items.Clear();
            

            for (int i = 0; i < fonteDeDados.Rows.Count; i++)
            {
                dp.Items.Add(fonteDeDados.Rows[i][CampoValue] + "-" + fonteDeDados.Rows[i][CampoText]);
            }
        }
    }
}    
