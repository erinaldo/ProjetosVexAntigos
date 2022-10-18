using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SistecnoColetor.Classes.DAL
{
    public class Estoque
    {
        public void EntrarComUA(string IdUa, string IdProdutoCliente, DateTime? Validade, string Quantidade, string Referencia, string IdDepositoplantaLocalizacao, string idProdutoEmbalagem, string ComandoDeZerarUas)
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

                //zera as uas

                sql = ComandoDeZerarUas;
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();

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
                    sql += " VALUES (" + idLote + " , " + idEstoque + ", " + IdProdutoCliente + " ," + idProdutoEmbalagem + ", " + VarGlobal.Usuario.IDUsuario + ", " + (Validade == null ? "NULL" : "'" + dataValidade + "'") + " ,GetDate()," + Quantidade + ",0,'" + Referencia + "','SIM','Entrada de UA')";
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

                    sql = "delete from UnidadeDeArmazenagemLote where idunidadeDeArmazenagem =" + IdUa + " and saldo=0";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();


                }
                #endregion
                #endregion

                #region EstoqueMov

                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                sql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoDestino, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque, IDDepositoPlantaLocalizacaoOrigem) ";
                sql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + IdProdutoCliente + ",  " + IdDepositoplantaLocalizacao + ", 1, " + VarGlobal.Usuario.IDUsuario + ",'Entrada de Pallet',GetDate()," + int.Parse(float.Parse(Quantidade).ToString()) + "," + int.Parse(float.Parse(Quantidade).ToString()) + ",  (select saldo from estoque where idestoque=" + idEstoque + ")" + ",NULL, 1)";
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


        public void EntrarComUA(string IdUa, string IdDepositoplantaLocalizacao)
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


                sql = "select l.IdProdutoCliente, l.IdLote, ual.Saldo, ual.idUnidadedeArmazenagemLote ";
                sql += " from unidadedearmazenagem ua ";
                sql += " inner join unidadeDeArmazenagemLote ual on ual.IDUnidadeDeArmazenagem = ua.IDUnidadeDeArmazenagem ";
                sql += " inner join Lote l on l.idlote = ual.idlote ";
                sql += " where ua.idunidadedearmazenagem =" + IdUa;


                DataTable dtUa = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtUa.Rows.Count == 0)
                {
                    trans.Rollback();
                    throw new Exception("Ua Não Encontrada");
                }


                for (int i = 0; i < dtUa.Rows.Count; i++)
                {
                    string idEstoque = "";
                    sql = "Select IdEstoque From Estoque where IdProdutoCliente=" + dtUa.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                    DataTable dtest = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    //Nao existe estoque
                    if (dtest.Rows.Count == 0)
                    {
                        idEstoque = BdExterno.RetornarIDTabela("Estoque").ToString();
                        sql = "INSERT INTO ESTOQUE (IDEstoque, IDProdutoCliente, IDFilial, Saldo) ";
                        sql += " VALUES (" + idEstoque + ", " + dtUa.Rows[i]["IDPRODUTOCLIENTE"].ToString() + ", " + VarGlobal.Usuario.UltimaFilial + ", " + float.Parse(dtUa.Rows[i]["SALDO"].ToString()).ToString().Replace(",", ".") + ")";
                        Comm.CommandText = sql;
                        Comm.ExecuteNonQuery();

                    }
                    else
                    {
                        idEstoque = dtest.Rows[0]["IdEstoque"].ToString();
                        sql = "UPDATE ESTOQUE SET SALDO=SALDO+ " + float.Parse(dtUa.Rows[i]["SALDO"].ToString()).ToString().Replace(",", ".") + " WHERE IDESTOQUE=" + idEstoque;
                        Comm.CommandText = sql;
                        Comm.ExecuteNonQuery();
                    }

                    //atualiza o IdEstoque no Lote

                    sql = "Update Lote set IdEstoque=" + idEstoque + " where IdLote = " + dtUa.Rows[i]["IDLOTE"].ToString();
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();

                    string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                    sql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoDestino, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque, IDDepositoPlantaLocalizacaoOrigem) ";
                    sql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + dtUa.Rows[i]["idUnidadedeArmazenagemLote"].ToString() + " , " + dtUa.Rows[i]["IDPRODUTOCLIENTE"].ToString() + ",  " + IdDepositoplantaLocalizacao + ", 1, " + VarGlobal.Usuario.IDUsuario + ",'Entrada de Pallet',GetDate()," + float.Parse(dtUa.Rows[i]["SALDO"].ToString()).ToString().Replace(",", ".") + "," + float.Parse(dtUa.Rows[i]["SALDO"].ToString()).ToString().Replace(",", ".") + ",  (select saldo from estoque where idestoque=" + idEstoque + ")" + ",NULL, 1)";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();


                    #region MovimentaçES
                    // System.Windows.Forms.MessageBox.Show("07- Movimentacao Item");

                    sql = "UPDATE MOVIMENTACAOITEM SET QuantidadeBaixada = " + float.Parse(dtUa.Rows[i]["SALDO"].ToString()).ToString().Replace(",", ".") + " , DataDeExecucao=GETDATE() WHERE IDUnidadeDeArmazenagem=" + IdUa;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();

                    //System.Windows.Forms.MessageBox.Show("08- Movimentacao ");

                    sql = "UPDATE MOVIMENTACAO SET ESTOQUEPROCESSADO='SIM' WHERE TIPO='ENTRADA' AND IDMOVIMENTACAO = (SELECT TOP 1 IDMOVIMENTACAO FROM MOVIMENTACAOITEM WHERE IDUnidadeDeArmazenagem=" + IdUa + ")";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();


                    sql = "UPDATE UNIDADEDEARMAZENAGEM SET SITUACAO='FINALIZADO', STATUS='EM ESTOQUE', IDDEPOSITOPLANTALOCALIZACAO="+ IdDepositoplantaLocalizacao +" WHERE IDUNIDADEDEARMAZENAGEM=" + IdUa ;
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();


                    #endregion


                }
                trans.Commit();

            }
            catch (System.Exception ex)
            {                

                trans.Rollback();
                throw new Exception("Problema em Entrar com a UA");
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();

            }
        }



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

                //System.Windows.Forms.MessageBox.Show("01- seleciona o estoque");

                DataTable dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                string idEstoque = "";
                if (dtTemp.Rows.Count > 0)
                    idEstoque = dtTemp.Rows[0][0].ToString();

                if (idEstoque == "" || idEstoque == "0")
                {
                    //System.Windows.Forms.MessageBox.Show("02- insere o estoque");

                    idEstoque = BdExterno.RetornarIDTabela("Estoque").ToString();
                    sql = "INSERT INTO ESTOQUE (IDEstoque, IDProdutoCliente, IDFilial, Saldo) ";
                    sql += " VALUES (" + idEstoque + ", " + IdProdutoCliente + ", " + VarGlobal.Usuario.UltimaFilial + ", " + int.Parse(float.Parse(Quantidade).ToString()) + ")";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }
                else
                {
//                    System.Windows.Forms.MessageBox.Show("02- Altera o estoque");

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

                  //  System.Windows.Forms.MessageBox.Show("03- insere o Lote");
                 
                    idLote = BdExterno.RetornarIDTabela("Lote").ToString();
                    sql = "INSERT INTO LOTE (IDLote, IDEstoque, IDProdutoCliente,IDProdutoEmbalagem,IDUsuario,Validade,DataDeEntrada,Quantidade,ValorUnitario,Referencia,Ativo,Observacao) ";
                    sql += " VALUES (" + idLote + " , " + idEstoque + ", " + IdProdutoCliente + " ,"+ idProdutoEmbalagem+", " + VarGlobal.Usuario.IDUsuario + ", " + (Validade == null ? "NULL" : "'" + dataValidade + "'") + " ,GetDate()," + int.Parse( float.Parse(Quantidade).ToString()) + ",0,'" + Referencia + "','SIM','Entrada de UA')";
                   // System.Windows.Forms.MessageBox.Show(sql);
                    
                    
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                }

                #region UnidadeDeArmazenagemLote                
                string idUALote = "";             

                if (idUALote == "" || idUALote == "0")
                {
                    //System.Windows.Forms.MessageBox.Show("04- insere o Unidade de ArmazeLote");

                    idUALote = BdExterno.RetornarIDTabela("UNIDADEDEARMAZENAGEMLOTE").ToString();
                    sql = "INSERT INTO UNIDADEDEARMAZENAGEMLOTE (IDUnidadeDeArmazenagemLote, IDUnidadeDeArmazenagem, IDLote, Saldo, Divisao) ";
                    sql += " VALUES (" + idUALote + ",  " + IdUa + ", " + idLote + ", " + int.Parse(float.Parse(Quantidade).ToString()) + ", 'ARMAZENAGEM')";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();                  

                }                
                #endregion
                #endregion

                #region EstoqueMov

                //System.Windows.Forms.MessageBox.Show("05- insere o estoqueMov");

                string idEstoqueMov = BdExterno.RetornarIDTabela("ESTOQUEMOV").ToString();
                sql = "INSERT INTO ESTOQUEMOV(IDEstoqueMov, IDEstoque, IDUnidadeDeArmazenagemLote, IDProdutoCliente,  IDDepositoPlantaLocalizacaoDestino, IDEstoqueOperacao, IDUsuario,Historico,DataHora,QuantidadeSolicitada,Quantidade,Saldo,ValorEmEstoque, IDDepositoPlantaLocalizacaoOrigem) ";
                sql += " VALUES (" + idEstoqueMov + ", " + idEstoque + "," + idUALote + " , " + IdProdutoCliente + ",  " + IdDepositoplantaLocalizacao + ", 1, " + VarGlobal.Usuario.IDUsuario + ",'Entrada de Pallet',GetDate()," + int.Parse(float.Parse(Quantidade).ToString()) + "," + int.Parse(float.Parse(Quantidade).ToString()) + ",  (select saldo from estoque where idestoque="+idEstoque+")"+ ",NULL, 1)";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                
                #endregion

                #region Unidade de Armazenagem

               // System.Windows.Forms.MessageBox.Show("06- Update Unidade De Armaz");

                sql = "UPDATE UNIDADEDEARMAZENAGEM SET  IdProdutoCliente=" + IdProdutoCliente + ", IdProdutoEmbalagem=" + idProdutoEmbalagem + ", QUANTIDADE=" + int.Parse(float.Parse(Quantidade).ToString()) + ",STATUS='EM ESTOQUE', SITUACAO='FINALIZADO', IDDEPOSITOPLANTALOCALIZACAO = " + IdDepositoplantaLocalizacao + " WHERE IDUNIDADEDEARMAZENAGEM =  " + IdUa;
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion


                #region MovimentaçES
               // System.Windows.Forms.MessageBox.Show("07- Movimentacao Item");

                sql = "UPDATE MOVIMENTACAOITEM SET QuantidadeBaixada = " + int.Parse(float.Parse(Quantidade).ToString()) + " , DataDeExecucao=GETDATE() WHERE IDUnidadeDeArmazenagem=" + IdUa;
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();

                //System.Windows.Forms.MessageBox.Show("08- Movimentacao ");
                
                sql = "UPDATE MOVIMENTACAO SET ESTOQUEPROCESSADO='SIM' WHERE TIPO='ENTRADA' AND IDMOVIMENTACAO = (SELECT TOP 1 IDMOVIMENTACAO FROM MOVIMENTACAOITEM WHERE IDUnidadeDeArmazenagem=" + IdUa + ")";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                #endregion

                ///System.Windows.Forms.MessageBox.Show("09 - Comitou");

                trans.Commit();

            }
            catch (System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("10- Rollback");

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

        public void SairPorRomaneio (string EndDestinoRomaneio, string IdRomaneio, string IdConferencia , string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem, string idUaDestino, string IdProdutoEmbalagem, string IdUnidadeDeArmazenagemOrigem)
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



                /**/
                #region Conferencia

                string idConferencia = "";
                sql = "SELECT IDCONFERENCIA  FROM conferencia WHERE IDROMANEIO=" + IdRomaneio;
                DataTable dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

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
                //sql = "SELECT IDCONFERENCIAPALLET  FROM CONFERENCIAPALLET WHERE  TIPO='SEPARACAO'  AND  IDCONFERENCIA= " + idConferencia + " and idpallet=" + idUaDestino + "AND IdDepositoPlantaLocalizacaoOrigem=" + IdDepositoplantaLocalizacaoOrigem + " and IdUnidadeDeArmazenagemOrigem=" + IdUnidadeDeArmazenagemOrigem;
                //dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                //if (dtTemp.Rows.Count > 0)
                //    idConferenciaPallet = dtTemp.Rows[0][0].ToString();
                //if (idConferenciaPallet == "" || idConferenciaPallet == "0")
                //{

                idConferenciaPallet = BdExterno.RetornarIDTabela("CONFERENCIAPALLET").ToString();
                sql = "INSERT INTO CONFERENCIAPALLET (IdConferenciaPallet, IdConferencia, IdPallet, IdDepositoPlantaLocalizacao, TIPO, IdDepositoPlantaLocalizacaoOrigem, IdUnidadeDeArmazenagemOrigem)";
                sql += " VALUES (" + idConferenciaPallet + ", " + idConferencia + ", " + idUaDestino + ", " + EndDestinoRomaneio + ", 'SEPARACAO', " + IdDepositoplantaLocalizacaoOrigem + ", " + IdUnidadeDeArmazenagemOrigem + ")";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();
                //}

                string idConferenciaPalletProduto = "";
                idConferenciaPalletProduto = BdExterno.RetornarIDTabela("CONFERENCIAPALLETPRODUTO").ToString();
                sql = "INSERT INTO CONFERENCIAPALLETPRODUTO (IdConferenciaPalletProduto, IdConferenciaPallet, IdProdutoEmbalagem, Quantidade, TIPO, IdDepositoPlantaLocalizacaoOrigem, IdUnidadeDeAemazenagemOrigem)";
                sql += " VALUES (" + idConferenciaPalletProduto + ", " + idConferenciaPallet + ", " + IdProdutoEmbalagem + ", " + int.Parse(float.Parse(Quantidade).ToString()) + ", NULL, NULL, NULL)";
                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();

                #endregion

                trans.Commit();
            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                System.Windows.Forms.MessageBox.Show("Problema em Processar a Saida");
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




        public int GerarRomaneio(List<string> IdDocumento, List<string> Tipo, string Divisao, string Andamento)
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

                #region Romaneio

                string idRomaneio = BdExterno.RetornarIDTabela("ROMANEIO").ToString();
                sql = "insert into Romaneio (IDRomaneio, IDFilial, IDUsuario, Emissao,Tipo, Divisao,Andamento)";
                sql += " values ("+idRomaneio+", "+VarGlobal.Usuario.UltimaFilial+", "+VarGlobal.Usuario.IDUsuario+", GetDate(),'"+Tipo[0]+"', '"+Divisao+"','"+Andamento+"') ";

                Comm.CommandText = sql;
                Comm.ExecuteNonQuery();


                #region RomaneioDocumento

                for (int i = 0; i < IdDocumento.Count; i++)
                {
                    string idRomaneioDocumento = BdExterno.RetornarIDTabela("RomaneioDocumento").ToString();
                    sql = "Insert into RomaneioDocumento(IDRomaneioDocumento, IDRomaneio, IDDocumento) ";
                    sql += "values (" + idRomaneioDocumento + ", " + idRomaneio + ", " + IdDocumento[i].ToString() + ")";
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();

                    DataTable di = Classes.BdExterno.RetornarDT("Select * from DocumentoItem where IdDocumento=" + IdDocumento[i].ToString(), VarGlobal.Conexao);

                    #region RomaneioDocumentoItem

                    for (int dit = 0; dit < di.Rows.Count; dit++)
                    {
                        sql = "Insert into RomaneioDocumentoItem(IDRomaneioDocumentoItem, IDRomaneioDocumento, IDDocumentoItem,Quantidade, Status, QuantidadeUnidadeEstoque) ";
                        sql += " Values(" + BdExterno.RetornarIDTabela("RomaneioDocumentoItem").ToString() + ", " + idRomaneioDocumento + ", " + di.Rows[dit]["IdDocumentoItem"].ToString() + "," + float.Parse(di.Rows[dit]["Quantidade"].ToString()).ToString().Replace(",", ".") + ", '1', " + float.Parse(di.Rows[dit]["Quantidade"].ToString()).ToString().Replace(",", ".") + ")";
                        Comm.CommandText = sql;
                        Comm.ExecuteNonQuery();
                    }

                    #endregion


                    sql = "Update DocumentoFilial set Situacao='EM ROMANEIO' WHERE IDDOCUMENTO=" + IdDocumento[i].ToString();
                    Comm.CommandText = sql;
                    Comm.ExecuteNonQuery();
                        
                }


                #endregion
                #endregion

                trans.Commit();
                return int.Parse(idRomaneio);


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