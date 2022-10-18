using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SistecnoColetor.Classes.DAL
{
    public class Estoque
    {
        public void EntrarComUA(string IdUa, string IdProdutoCliente, DateTime? Validade, string Quantidade, string Referencia, string IdDepositoplantaLocalizacao, string idProdutoEmbalagem)
        {
            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
            Conn.Open();
            System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();

            try
            {
                System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();
                Comm.CommandType = CommandType.Text;
                Comm.Connection = Conn;
                Comm.Transaction = trans;
                string sql = "";

                #region Estoque

                sql = "SELECT ISNULL(IDESTOQUE, 0 ) FROM ESTOQUE WHERE IDPRODUTOCLIENTE =" + IdProdutoCliente + " AND IDFILIAL=" + VarGlobal.Usuario.UltimaFilial;

                DataTable dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                string idEstoque = "";
                if (dtTemp.Rows.Count > 0)
                    idEstoque = dtTemp.Rows[0][0].ToString();

                if (idEstoque == "" || idEstoque == "0")
                {
                    idEstoque = BdExterno.RetornarIDTabela("Estoque").ToString();
                    sql = "INSERT INTO ESTOQUE (IDEstoque, IDProdutoCliente, IDFilial, Saldo) ";
                    sql += " VALUES (" + idEstoque + ", " + IdProdutoCliente + ", " + VarGlobal.Usuario.UltimaFilial + ", " + int.Parse(float.Parse(Quantidade).ToString()) + ")";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }
                else
                {
                    sql = "UPDATE ESTOQUE SET SALDO=SALDO+ " + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDESTOQUE=" + idEstoque;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }

                #endregion

                #region Lote

                string idLote = "";
                if (idLote == "" || idLote == "0")
                {
                    string dataValidade = "";
                    if (Validade != null)
                        dataValidade = ((DateTime)Validade).Year.ToString() + "-" + ((DateTime)Validade).Month.ToString() + "-" + ((DateTime)Validade).Day.ToString();
                                       
                 
                    idLote = BdExterno.RetornarIDTabela("Lote").ToString();
                    sql = "INSERT INTO LOTE (IDLote, IDEstoque, IDProdutoCliente,IDProdutoEmbalagem,IDUsuario,Validade,DataDeEntrada,Quantidade,ValorUnitario,Referencia,Ativo,Observacao) ";
                    sql += " VALUES (" + idLote + " , " + idEstoque + ", " + IdProdutoCliente + " ,"+ idProdutoEmbalagem+", " + VarGlobal.Usuario.IDUsuario + ", " + (Validade == null ? "NULL" : "'" + dataValidade + "'") + " ,GetDate()," + Quantidade + ",0,'" + Referencia + "','SIM','Entrada de UA')";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }

                #region UnidadeDeArmazenagemLote                
                string idUALote = "";             

                if (idUALote == "" || idUALote == "0")
                {
                    idUALote = BdExterno.RetornarIDTabela("UNIDADEDEARMAZENAGEMLOTE").ToString();
                    sql = "INSERT INTO UNIDADEDEARMAZENAGEMLOTE (IDUnidadeDeArmazenagemLote, IDUnidadeDeArmazenagem, IDLote, Saldo, Divisao) ";
                    sql += " VALUES (" + idUALote + ",  " + IdUa + ", " + idLote + ", " + Quantidade + ", 'ARMAZENAGEM')";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();                  

                }                
                #endregion
                #endregion

                #region EstoqueMov

                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                sql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoDestino, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque, IDDepositoPlantaLocalizacaoOrigem) ";
                sql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + IdProdutoCliente + ",  " + IdDepositoplantaLocalizacao + ", 1, " + VarGlobal.Usuario.IDUsuario + ",'Entrada de Pallet',GetDate()," + int.Parse(float.Parse(Quantidade).ToString()) + "," + int.Parse(float.Parse(Quantidade).ToString()) + ",  (select saldo from estoque where idestoque="+idEstoque+")"+ ",NULL, 1)";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                
                #endregion

                #region Unidade de Armazenagem

                sql = "UPDATE UNIDADEDEARMAZENAGEM SET  IdProdutoCliente=" + IdProdutoCliente + ", IdProdutoEmbalagem=" + idProdutoEmbalagem + ", QUANTIDADE=" + int.Parse(float.Parse(Quantidade).ToString()) + ",STATUS='EM ESTOQUE', SITUACAO='FINALIZADO', IDDEPOSITOPLANTALOCALIZACAO = " + IdDepositoplantaLocalizacao + " WHERE IDUNIDADEDEARMAZENAGEM =  " + IdUa;
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion


                #region MovimentaçES

                sql = "UPDATE MOVIMENTACAOITEM SET QuantidadeBaixada = " + int.Parse(float.Parse(Quantidade).ToString()) + " , DataDeExecucao=GETDATE() WHERE IDUnidadeDeArmazenagem=" + IdUa;
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();

                sql = "UPDATE MOVIMENTACAO SET ESTOQUEPROCESSADO='SIM' WHERE TIPO='ENTRADA' AND IDMOVIMENTACAO = (SELECT TOP 1 IDMOVIMENTACAO FROM MOVIMENTACAOITEM WHERE IDUnidadeDeArmazenagem=" + IdUa + ")";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion
                trans.Commit();

            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + ex.InnerException);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();

            }
        }

        public void Sair(string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem)
        {
            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
            Conn.Open();
            System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();

            try
            {
                System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();
                Comm.CommandType = CommandType.Text;
                Comm.Connection = Conn;
                Comm.Transaction = trans;

                string sql = "";

                #region Estoque

                sql = "SELECT ISNULL(IDESTOQUE, 0 ), SALDO FROM ESTOQUE WHERE IDPRODUTOCLIENTE =" + IdProdutoCliente + " AND IDFILIAL=" + VarGlobal.Usuario.UltimaFilial;
                DataTable dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                string idEstoque = "";

                if (dtTemp.Rows.Count > 0)
                    idEstoque = dtTemp.Rows[0][0].ToString();
                
                if (idEstoque == "" || idEstoque == "0")
                {
                    throw new Exception("Estoque não encontrado");
                }
                else
                {
                    if(float.Parse(dtTemp.Rows[0][1].ToString()) < float.Parse(Quantidade))
                        throw new Exception("Saldo menor que a quantidade solicitada.");

                    
                    sql = "UPDATE ESTOQUE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDESTOQUE=" + idEstoque;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();



                }
                #endregion

                #region Lote
                sql = "SELECT IDLOTE FROM LOTE WHERE IDESTOQUE =" + idEstoque;
                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                string idLote = "";
                if (dtTemp.Rows.Count > 0)
                    idLote = dtTemp.Rows[0][0].ToString();


                if (idLote == "" || idLote == "0")
                {
                    throw new Exception("Lote Não Encontrado");
                }


                #region UnidadeDeArmazenagemLote


                sql = "SELECT IDUNIDADEDEARMAZENAGEMLOTE FROM UNIDADEDEARMAZENAGEMLOTE WHERE IDLOTE =" + idLote;
                string idUALote = "";

                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);


                if (dtTemp.Rows.Count > 0)
                    idUALote = dtTemp.Rows[0][0].ToString();


                if (idUALote == "" || idUALote == "0")
                {
                    throw new Exception("Lote Não Encontrado");

                }
                else
                {
                    sql = "UPDATE UNIDADEDEARMAZENAGEMLOTE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }

                #endregion
                #endregion

                #region EstoqueMov
                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                sql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoOrigem, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque) ";
                sql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + IdProdutoCliente + ",  " + IdDepositoplantaLocalizacaoOrigem + ", 2, " + VarGlobal.Usuario.IDUsuario + ",'SAÍDA',GetDate()," + int.Parse(float.Parse(Quantidade).ToString()) + "," + int.Parse(float.Parse(Quantidade).ToString()) + ",  (select saldo from estoque where idestoque="+idEstoque+")  ,NULL)";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion
                
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();

            }
        }

        public void SairPorPicking (string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem, string idUaDestino, string IdProdutoEmbalagem)
        {
            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
            Conn.Open();
            System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();

            try
            {
                System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();
                Comm.CommandType = CommandType.Text;
                Comm.Connection = Conn;
                Comm.Transaction = trans;

                string sql = "";

                #region Estoque

                sql = "SELECT ISNULL(IDESTOQUE, 0 ), SALDO FROM ESTOQUE WHERE IDPRODUTOCLIENTE =" + IdProdutoCliente + " AND IDFILIAL=" + VarGlobal.Usuario.UltimaFilial;
                DataTable dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                string idEstoque = "";

                if (dtTemp.Rows.Count > 0)
                    idEstoque = dtTemp.Rows[0][0].ToString();

                if (idEstoque == "" || idEstoque == "0")
                {
                    throw new Exception("Estoque não encontrado");
                }
                else
                {
                    if (float.Parse(dtTemp.Rows[0][1].ToString()) < float.Parse(Quantidade))
                        throw new Exception("Saldo menor que a quantidade solicitada.");

                    sql = "UPDATE ESTOQUE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDESTOQUE=" + idEstoque;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }
                #endregion

                #region Lote
                
                sql = "";
                sql += " SELECT L.IDLOTE FROM LOTE L";
                sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDLOTE = L.IDLOTE";
                sql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM = UAL.IDUNIDADEDEARMAZENAGEM";
                sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO";
                sql += " WHERE IDESTOQUE = " + idEstoque;
                sql += " AND UAL.IDUNIDADEDEARMAZENAGEM =" + IdUa;
                sql += " AND UA.IDDEPOSITOPLANTALOCALIZACAO =" + IdDepositoplantaLocalizacaoOrigem;
                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                string idLote = "";
                if (dtTemp.Rows.Count > 0)
                    idLote = dtTemp.Rows[0][0].ToString();


                if (idLote == "" || idLote == "0")
                                    throw new Exception("Lote Não Encontrado");
                
                #region UnidadeDeArmazenagemLote
                
                sql = "SELECT IDUNIDADEDEARMAZENAGEMLOTE FROM UNIDADEDEARMAZENAGEMLOTE WHERE IDLOTE =" + idLote;
                string idUALote = "";

                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);


                if (dtTemp.Rows.Count > 0)
                    idUALote = dtTemp.Rows[0][0].ToString();
                
                if (idUALote == "" || idUALote == "0")
                {
                    throw new Exception("Lote Não Encontrado");

                }
                else
                {
                    sql = "UPDATE UNIDADEDEARMAZENAGEMLOTE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }

                #endregion
                #endregion

                #region EstoqueMov
                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                sql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoOrigem, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque) ";
                sql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + IdProdutoCliente + ",  " + IdDepositoplantaLocalizacaoOrigem + ", 2, " + VarGlobal.Usuario.IDUsuario + ",'SAÍDA',GetDate()," + int.Parse(float.Parse(Quantidade).ToString()) + "," + int.Parse(float.Parse(Quantidade).ToString()) + ",  (select saldo from estoque where idestoque=" + idEstoque + ")  ,NULL)";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion

/**/
                #region Conferencia

                string idConferencia = "";
                sql = "SELECT IDCONFERENCIA  FROM conferencia WHERE IDROMANEIO=1 ";                
                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtTemp.Rows.Count > 0)
                    idConferencia = dtTemp.Rows[0][0].ToString();
                if (idConferencia == "" || idConferencia == "0")
                {

                    idConferencia = BdExterno.RetornarIDTabela("CONFERENCIA").ToString();
                    sql = "INSERT INTO CONFERENCIA (IdConferencia, IdRomaneio, IdUsuario, Inicio, Final, Situacao)";
                    sql += " VALUES (" + idConferencia + ", 1, " + VarGlobal.Usuario.IDUsuario + ", GetDate(), NULL, 'FORMACAO DE PALLET')"; // VERIFICAR A QUESTAO DO ROMANEIO
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }



                string idConferenciaPallet = "";
                sql = "SELECT IDCONFERENCIAPALLET  FROM CONFERENCIAPALLET WHERE  TIPO='SEPARACAO'  AND  IDCONFERENCIA= " + idConferencia + " and idpallet="+ idUaDestino;
                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtTemp.Rows.Count > 0)
                    idConferenciaPallet = dtTemp.Rows[0][0].ToString();
                if (idConferenciaPallet == "" || idConferenciaPallet == "0")
                {

                    idConferenciaPallet = BdExterno.RetornarIDTabela("CONFERENCIAPALLET").ToString();
                    sql = "INSERT INTO CONFERENCIAPALLET (IdConferenciaPallet, IdConferencia, IdPallet, IdDepositoPlantaLocalizacao, TIPO)";
                    sql += " VALUES ("+idConferenciaPallet+", "+idConferencia+", "+idUaDestino+", 5, 'SEPARACAO')";  //VERIFICAR DEPOIS O ENDERECO
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }


                string idConferenciaPalletProduto = "";         

               // if (dtTemp.Rows.Count > 0)
               //     idConferenciaPalletProduto = dtTemp.Rows[0][0].ToString();
                if (idConferenciaPalletProduto == "" || idConferenciaPalletProduto == "0")
                {

                    idConferenciaPalletProduto = BdExterno.RetornarIDTabela("CONFERENCIAPALLETPRODUTO").ToString();
                    sql = "INSERT INTO CONFERENCIAPALLETPRODUTO (IdConferenciaPalletProduto, IdConferenciaPallet, IdProdutoEmbalagem, Quantidade)";
                    sql += " VALUES (" + idConferenciaPalletProduto + ", " + idConferenciaPallet + ", " + IdProdutoEmbalagem + ", " + int.Parse(float.Parse(Quantidade).ToString()) + ")";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }



                #endregion
 /* */
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();

            }
        }
        
        public void SairPorPicking(string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem, string idUaDestino, string IdProdutoEmbalagem, string IdMovimentacaoItem, string IdRomaneio)
        {
            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
            Conn.Open();
            System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();

            try
            {
                System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();
                Comm.CommandType = CommandType.Text;
                Comm.Connection = Conn;
                Comm.Transaction = trans;

                string sql = "";

                #region Estoque

                sql = "SELECT ISNULL(IDESTOQUE, 0 ), SALDO FROM ESTOQUE WHERE IDPRODUTOCLIENTE =" + IdProdutoCliente + " AND IDFILIAL=" + VarGlobal.Usuario.UltimaFilial;
                DataTable dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                string idEstoque = "";

                if (dtTemp.Rows.Count > 0)
                    idEstoque = dtTemp.Rows[0][0].ToString();

                if (idEstoque == "" || idEstoque == "0")
                {
                    throw new Exception("Estoque não encontrado");
                }
                else
                {
                    if (float.Parse(dtTemp.Rows[0][1].ToString()) < float.Parse(Quantidade))
                        throw new Exception("Saldo menor que a quantidade solicitada.");

                    sql = "UPDATE ESTOQUE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDESTOQUE=" + idEstoque;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }
                #endregion

                #region Lote

                sql = "";
                sql += " SELECT L.IDLOTE FROM LOTE L";
                sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDLOTE = L.IDLOTE";
                sql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM = UAL.IDUNIDADEDEARMAZENAGEM";
                sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO";
                sql += " WHERE IDESTOQUE = " + idEstoque;
                sql += " AND UAL.IDUNIDADEDEARMAZENAGEM =" + IdUa;
                sql += " AND UA.IDDEPOSITOPLANTALOCALIZACAO =" + IdDepositoplantaLocalizacaoOrigem;
                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                string idLote = "";
                if (dtTemp.Rows.Count > 0)
                    idLote = dtTemp.Rows[0][0].ToString();


                if (idLote == "" || idLote == "0")
                    throw new Exception("Lote Não Encontrado");

                #region UnidadeDeArmazenagemLote

                sql = "SELECT IDUNIDADEDEARMAZENAGEMLOTE FROM UNIDADEDEARMAZENAGEMLOTE WHERE IDLOTE =" + idLote;
                string idUALote = "";

                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);


                if (dtTemp.Rows.Count > 0)
                    idUALote = dtTemp.Rows[0][0].ToString();

                if (idUALote == "" || idUALote == "0")
                {
                    throw new Exception("Lote Não Encontrado");

                }
                else
                {
                    sql = "UPDATE UNIDADEDEARMAZENAGEMLOTE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }

                #endregion
                #endregion

                #region EstoqueMov
                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                sql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoOrigem, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque) ";
                sql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + IdProdutoCliente + ",  " + IdDepositoplantaLocalizacaoOrigem + ", 2, " + VarGlobal.Usuario.IDUsuario + ",'SAÍDA',GetDate()," + int.Parse(float.Parse(Quantidade).ToString()) + "," + int.Parse(float.Parse(Quantidade).ToString()) + ",  (select saldo from estoque where idestoque=" + idEstoque + ")  ,NULL)";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion

                /**/
                #region Conferencia

                string idConferencia = "";
                sql = "SELECT IDCONFERENCIA  FROM CONFERENCIA WHERE IDROMANEIO="+ IdRomaneio;
                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtTemp.Rows.Count > 0)
                    idConferencia = dtTemp.Rows[0][0].ToString();
                if (idConferencia == "" || idConferencia == "0")
                {

                    idConferencia = BdExterno.RetornarIDTabela("CONFERENCIA").ToString();
                    sql = "INSERT INTO CONFERENCIA (IdConferencia, IdRomaneio, IdUsuario, Inicio, Final, Situacao)";
                    sql += " VALUES (" + idConferencia + ", "+IdRomaneio+", " + VarGlobal.Usuario.IDUsuario + ", GetDate(), NULL, 'FORMACAO DE PALLET')"; // VERIFICAR A QUESTAO DO ROMANEIO
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }



                string idConferenciaPallet = "";
                sql = "SELECT IDCONFERENCIAPALLET  FROM CONFERENCIAPALLET WHERE  TIPO='SEPARACAO'  AND  IDCONFERENCIA= " + idConferencia + " and idpallet=" + idUaDestino;
                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtTemp.Rows.Count > 0)
                    idConferenciaPallet = dtTemp.Rows[0][0].ToString();
                if (idConferenciaPallet == "" || idConferenciaPallet == "0")
                {

                    idConferenciaPallet = BdExterno.RetornarIDTabela("CONFERENCIAPALLET").ToString();
                    sql = "INSERT INTO CONFERENCIAPALLET (IdConferenciaPallet, IdConferencia, IdPallet, IdDepositoPlantaLocalizacao, TIPO)";
                    sql += " VALUES (" + idConferenciaPallet + ", " + idConferencia + ", " + idUaDestino + ", 5, 'SEPARACAO')";  //VERIFICAR DEPOIS O ENDERECO
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }


                string idConferenciaPalletProduto = "";                
                if (idConferenciaPalletProduto == "" || idConferenciaPalletProduto == "0")
                {

                    idConferenciaPalletProduto = BdExterno.RetornarIDTabela("CONFERENCIAPALLETPRODUTO").ToString();
                    sql = "INSERT INTO CONFERENCIAPALLETPRODUTO (IdConferenciaPalletProduto, IdConferenciaPallet, IdProdutoEmbalagem, Quantidade, TIPO)";
                    sql += " VALUES (" + idConferenciaPalletProduto + ", " + idConferenciaPallet + ", " + IdProdutoEmbalagem + ", " + int.Parse(float.Parse(Quantidade).ToString()) + ", 'SEPARACAO')";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }



                #endregion

                #region MovimentacaoItem

                sql = "UPDATE MOVIMENTACAOITEM SET DataDeExecucao=GETDATE(), QuantidadeBaixada=" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDMOVIMENTACAOITEM=" + IdMovimentacaoItem;
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();


                #endregion
                /* */
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();

            }
        }
        
        public void SairUAInteira(string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem, string IdDepositoplantaLocalizacaoDestino, string IdProdutoEmbalagem)
        {
            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
            Conn.Open();
            System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();

            try
            {
                System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();
                Comm.CommandType = CommandType.Text;
                Comm.Connection = Conn;
                Comm.Transaction = trans;

                string sql = "";

                #region Estoque

                sql = "SELECT ISNULL(IDESTOQUE, 0 ), SALDO FROM ESTOQUE WHERE IDPRODUTOCLIENTE =" + IdProdutoCliente + " AND IDFILIAL=" + VarGlobal.Usuario.UltimaFilial;
                DataTable dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                string idEstoque = "";

                if (dtTemp.Rows.Count > 0)
                    idEstoque = dtTemp.Rows[0][0].ToString();

                if (idEstoque == "" || idEstoque == "0")
                {
                    throw new Exception("Estoque não encontrado");
                }
                else
                {
                    if (float.Parse(dtTemp.Rows[0][1].ToString()) < float.Parse(Quantidade))
                        throw new Exception("Saldo menor que a quantidade solicitada.");


                    sql = "UPDATE ESTOQUE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDESTOQUE=" + idEstoque;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();



                }
                #endregion

                #region Lote
                sql = "";
                sql+= " SELECT L.IDLOTE FROM LOTE L";
                    sql+= " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDLOTE = L.IDLOTE";
                    sql+= " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM = UAL.IDUNIDADEDEARMAZENAGEM";
                    sql+= " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO";
                    sql+= " WHERE IDESTOQUE = "+idEstoque;
                    sql+= " AND UAL.IDUNIDADEDEARMAZENAGEM ="+IdUa;
                    sql += " AND UA.IDDEPOSITOPLANTALOCALIZACAO =" + IdDepositoplantaLocalizacaoOrigem;


                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                string idLote = "";
                if (dtTemp.Rows.Count > 0)
                    idLote = dtTemp.Rows[0][0].ToString();


                if (idLote == "" || idLote == "0")
                {
                    throw new Exception("Lote Não Encontrado");
                }


                #region UnidadeDeArmazenagemLote


                sql = "SELECT IDUNIDADEDEARMAZENAGEMLOTE FROM UNIDADEDEARMAZENAGEMLOTE WHERE IDLOTE =" + idLote;
                string idUALote = "";

                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);


                if (dtTemp.Rows.Count > 0)
                    idUALote = dtTemp.Rows[0][0].ToString();


                if (idUALote == "" || idUALote == "0")
                {
                    throw new Exception("Lote Não Encontrado");

                }
                else
                {
                    sql = "UPDATE UNIDADEDEARMAZENAGEMLOTE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }

                #endregion
                #endregion

                #region EstoqueMov
                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                sql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoOrigem, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque, IDDepositoPlantaLocalizacaoDestino) ";
                sql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + IdProdutoCliente + ",  " + IdDepositoplantaLocalizacaoOrigem + ", 2, " + VarGlobal.Usuario.IDUsuario + ",'SAÍDA',GetDate()," + int.Parse(float.Parse(Quantidade).ToString()) + "," + int.Parse(float.Parse(Quantidade).ToString()) + ",  (select saldo from estoque where idestoque=" + idEstoque + ")  ,NULL, " + (IdDepositoplantaLocalizacaoDestino == "" ? "NULL" : IdDepositoplantaLocalizacaoDestino) + ")";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion

/**/
                #region Conferencia

                string idConferencia = "";
                sql = "SELECT IDCONFERENCIA  FROM conferencia WHERE IDROMANEIO=1 ";
                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtTemp.Rows.Count > 0)
                    idConferencia = dtTemp.Rows[0][0].ToString();
                if (idConferencia == "" || idConferencia == "0")
                {

                    idConferencia = BdExterno.RetornarIDTabela("CONFERENCIA").ToString();
                    sql = "INSERT INTO CONFERENCIA (IdConferencia, IdRomaneio, IdUsuario, Inicio, Final, Situacao)";
                    sql += " VALUES (" + idConferencia + ", 1, " + VarGlobal.Usuario.IDUsuario + ", GetDate(), NULL, 'FORMACAO DE PALLET')"; // VERIFICAR A QUESTAO DO ROMANEIO
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }



                string idConferenciaPallet = "";
               // sql = "SELECT IDCONFERENCIAPALLET  FROM CONFERENCIAPALLET WHERE IDCONFERENCIA= " + idConferencia;
                sql = "SELECT IDCONFERENCIAPALLET  FROM CONFERENCIAPALLET WHERE  TIPO='SEPARACAO'  AND  IDCONFERENCIA= " + idConferencia + " and idpallet=" + IdUa;

                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtTemp.Rows.Count > 0)
                    idConferenciaPallet = dtTemp.Rows[0][0].ToString();
                if (idConferenciaPallet == "" || idConferenciaPallet == "0")
                {

                    idConferenciaPallet = BdExterno.RetornarIDTabela("CONFERENCIAPALLET").ToString();
                    sql = "INSERT INTO CONFERENCIAPALLET (IdConferenciaPallet, IdConferencia, IdPallet, IdDepositoPlantaLocalizacao, TIPO)";
                    sql += " VALUES (" + idConferenciaPallet + ", " + idConferencia + ", " + IdUa + ", " + IdDepositoplantaLocalizacaoDestino + ", 'SEPARACAO')"; 
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }


                string idConferenciaPalletProduto = "";
                
                if (idConferenciaPalletProduto == "" || idConferenciaPalletProduto == "0")
                {

                    idConferenciaPalletProduto = BdExterno.RetornarIDTabela("CONFERENCIAPALLETPRODUTO").ToString();
                    sql = "INSERT INTO CONFERENCIAPALLETPRODUTO (IdConferenciaPalletProduto, IdConferenciaPallet, IdProdutoEmbalagem, Quantidade, TIPO)";
                    sql += " VALUES (" + idConferenciaPalletProduto + ", " + idConferenciaPallet + ", " + IdProdutoEmbalagem + ", " + int.Parse(float.Parse(Quantidade).ToString()) + ", 'SEPARACAO')";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }



                #endregion
 /* */
                trans.Commit();
                //trans.Rollback();

            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();

            }
        }
        
        public void SairUAInteira(string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem, string IdDepositoplantaLocalizacaoDestino, string IdProdutoEmbalagem, string IdMovimentacaoItem, string IdRomaneio)
        {
            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
            Conn.Open();
            System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();

            try
            {
                System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();
                Comm.CommandType = CommandType.Text;
                Comm.Connection = Conn;
                Comm.Transaction = trans;

                string sql = "";

                #region Estoque

                sql = "SELECT ISNULL(IDESTOQUE, 0 ), SALDO FROM ESTOQUE WHERE IDPRODUTOCLIENTE =" + IdProdutoCliente + " AND IDFILIAL=" + VarGlobal.Usuario.UltimaFilial;
                DataTable dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                string idEstoque = "";

                if (dtTemp.Rows.Count > 0)
                    idEstoque = dtTemp.Rows[0][0].ToString();

                if (idEstoque == "" || idEstoque == "0")
                {
                    throw new Exception("Estoque não encontrado");
                }
                else
                {
                    if (float.Parse(dtTemp.Rows[0][1].ToString()) < float.Parse(Quantidade))
                        throw new Exception("Saldo menor que a quantidade solicitada.");


                    sql = "UPDATE ESTOQUE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDESTOQUE=" + idEstoque;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();



                }
                #endregion

                #region Lote
                sql = "";
                sql += " SELECT L.IDLOTE FROM LOTE L";
                sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDLOTE = L.IDLOTE";
                sql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM = UAL.IDUNIDADEDEARMAZENAGEM";
                sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO";
                sql += " WHERE IDESTOQUE = " + idEstoque;
                sql += " AND UAL.IDUNIDADEDEARMAZENAGEM =" + IdUa;
                sql += " AND UA.IDDEPOSITOPLANTALOCALIZACAO =" + IdDepositoplantaLocalizacaoOrigem;


                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                string idLote = "";
                if (dtTemp.Rows.Count > 0)
                    idLote = dtTemp.Rows[0][0].ToString();


                if (idLote == "" || idLote == "0")
                {
                    throw new Exception("Lote Não Encontrado");
                }


                #region UnidadeDeArmazenagemLote


                sql = "SELECT IDUNIDADEDEARMAZENAGEMLOTE FROM UNIDADEDEARMAZENAGEMLOTE WHERE IDLOTE =" + idLote;
                string idUALote = "";

                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);


                if (dtTemp.Rows.Count > 0)
                    idUALote = dtTemp.Rows[0][0].ToString();


                if (idUALote == "" || idUALote == "0")
                {
                    throw new Exception("Lote Não Encontrado");

                }
                else
                {
                    sql = "UPDATE UNIDADEDEARMAZENAGEMLOTE SET SALDO=SALDO-" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDUNIDADEDEARMAZENAGEMLOTE= " + idUALote;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }

                #endregion
                #endregion

                #region EstoqueMov
                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                sql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoOrigem, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque, IDDepositoPlantaLocalizacaoDestino) ";
                sql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + IdProdutoCliente + ",  " + IdDepositoplantaLocalizacaoOrigem + ", 2, " + VarGlobal.Usuario.IDUsuario + ",'SAÍDA',GetDate()," + int.Parse(float.Parse(Quantidade).ToString()) + "," + int.Parse(float.Parse(Quantidade).ToString()) + ",  (select saldo from estoque where idestoque=" + idEstoque + ")  ,NULL, " + (IdDepositoplantaLocalizacaoDestino == "" ? "NULL" : IdDepositoplantaLocalizacaoDestino) + ")";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion

                /**/
                #region Conferencia

                string idConferencia = "";
                sql = "SELECT IDCONFERENCIA  FROM conferencia WHERE IDROMANEIO= " + IdRomaneio;
                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtTemp.Rows.Count > 0)
                    idConferencia = dtTemp.Rows[0][0].ToString();
                if (idConferencia == "" || idConferencia == "0")
                {

                    idConferencia = BdExterno.RetornarIDTabela("CONFERENCIA").ToString();
                    sql = "INSERT INTO CONFERENCIA (IdConferencia, IdRomaneio, IdUsuario, Inicio, Final, Situacao)";
                    sql += " VALUES (" + idConferencia + ", "+IdRomaneio+", " + VarGlobal.Usuario.IDUsuario + ", GetDate(), NULL, 'FORMACAO DE PALLET')"; // VERIFICAR A QUESTAO DO ROMANEIO
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }



                string idConferenciaPallet = "";
                // sql = "SELECT IDCONFERENCIAPALLET  FROM CONFERENCIAPALLET WHERE IDCONFERENCIA= " + idConferencia;
                sql = "SELECT IDCONFERENCIAPALLET  FROM CONFERENCIAPALLET WHERE  TIPO='SEPARACAO'  AND  IDCONFERENCIA= " + idConferencia + " and idpallet=" + IdUa;

                dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtTemp.Rows.Count > 0)
                    idConferenciaPallet = dtTemp.Rows[0][0].ToString();
                if (idConferenciaPallet == "" || idConferenciaPallet == "0")
                {

                    idConferenciaPallet = BdExterno.RetornarIDTabela("CONFERENCIAPALLET").ToString();
                    sql = "INSERT INTO CONFERENCIAPALLET (IdConferenciaPallet, IdConferencia, IdPallet, IdDepositoPlantaLocalizacao, TIPO)";
                    sql += " VALUES (" + idConferenciaPallet + ", " + idConferencia + ", " + IdUa + ", " + IdDepositoplantaLocalizacaoDestino + ", 'SEPARACAO')";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }


                string idConferenciaPalletProduto = "";
                //sql = "SELECT IdConferenciaPalletProduto  FROM ConferenciaPalletProduto WHERE IdConferenciaPallet= " + idConferenciaPallet;
                //dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                //if (dtTemp.Rows.Count > 0)
                //    idConferenciaPalletProduto = dtTemp.Rows[0][0].ToString();
                if (idConferenciaPalletProduto == "" || idConferenciaPalletProduto == "0")
                {

                    idConferenciaPalletProduto = BdExterno.RetornarIDTabela("CONFERENCIAPALLETPRODUTO").ToString();
                    sql = "INSERT INTO CONFERENCIAPALLETPRODUTO (IdConferenciaPalletProduto, IdConferenciaPallet, IdProdutoEmbalagem, Quantidade, TIPO)";
                    sql += " VALUES (" + idConferenciaPalletProduto + ", " + idConferenciaPallet + ", " + IdProdutoEmbalagem + ", " + int.Parse(float.Parse(Quantidade).ToString()) + ", 'SEPARACAO')";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }



                #endregion


                #region MovimentacaoItem

                sql = "UPDATE MOVIMENTACAOITEM SET DataDeExecucao=GETDATE(), QuantidadeBaixada=" + int.Parse(float.Parse(Quantidade).ToString()) + " WHERE IDMOVIMENTACAOITEM=" + IdMovimentacaoItem;                
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();


                #endregion
                /* */
                trans.Commit();
                //trans.Rollback();

            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + ex.StackTrace + ex.InnerException);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();

            }
        }

    }
}