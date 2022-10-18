using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SistecnoColetor.Classes;

namespace SistecnoColetor
{
    public partial class CLW00015 : Form
    {
        public CLW00015()
        {
            InitializeComponent();
        }

        private void CLW00015_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = this.Name;
                statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
                lblTitulo.Text = VarGlobal.NomePrograma;
                CarregarConferenciasPendentes("");
                linhaClicada = -1;
            }
            catch (Exception ecx)
            {
                MessageBox.Show(ecx.Message);
            }
        }

        DataTable dtPendentes;
        private void CarregarConferenciasPendentes(string IdRom)
        {

            Cursor.Current = Cursors.WaitCursor;
            try
            {



                string sql = "Select Distinct ";
                sql += " R.IdRomaneio [IdRomaneio], R.Emissao [Emissao], DPL.Codigo [Doca], R.Observacao1 [Observacao]";
                sql += " From Movimentacao MV ";
                sql += " Inner Join MovimentacaoItem MVI on (MVI.IdMovimentacao = MV.IdMovimentacao)";
                sql += " Inner Join Romaneio R on (R.IdRomaneio = MVI.IdRomaneio)";
                sql += " Inner Join DepositoPlantaLocalizacao DPL on (DPL.IdDepositoPlantaLocalizacao = R.IdDepositoPlantaLocalizacao)";
                sql += " where ";
                sql += " MV.IdFilial = " + VarGlobal.Usuario.UltimaFilial + " and MV.EstoqueProcessado = 'NAO'";
                sql += " order by R.IdRomaneio";
                dtPendentes = new DataTable();
                dtPendentes = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                dataGrid1.DataSource = dtPendentes;

                if (dtPendentes.Rows.Count == 0)
                    linhaClicada = -1;


            }
            catch (Exception)
            {
                MessageBox.Show("Problema CarregarConferenciasPendentes");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        int linhaClicada = -1;
        private void dataGrid1_Click(object sender, EventArgs e)
        {
            linhaClicada = dataGrid1.CurrentCell.RowNumber;
            lblObservacao.Text = dataGrid1[linhaClicada, 3].ToString();
            idRomaneio = dataGrid1[linhaClicada, 0].ToString();

        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            linhaClicada = dataGrid1.CurrentCell.RowNumber;
            lblObservacao.Text = dataGrid1[linhaClicada, 3].ToString();
            idRomaneio = dataGrid1[linhaClicada, 0].ToString();
        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            if (linhaClicada >= 0)
            {
                tabControl1.SelectedIndex = 1;
                txtUa.Focus();
            }
        }


        private void txtUa_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtUa.Text != "")
            {
                e.Handled = true;
                PesquisarUA();
            }
        }

        private void PesquisarUA()
        {
            Cursor.Current = Cursors.WaitCursor;


            try
            {


                string sql = "  SELECT IDCONFERENCIAPALLET FROM CONFERENCIA C ";
                sql += " INNER JOIN  CONFERENCIAPALLET CP ON CP.IDCONFERENCIA = C.IDCONFERENCIA ";
                sql += " WHERE IDROMANEIO =" + idRomaneio;
                //sql += " AND TIPO = 'SEPARACAO'";
                sql += " AND IDPALLET = " + Helpers.RetirarDigitoUA(txtUa.Text);

                string ret = Classes.BdExterno.ExecutarComRetorno(sql, VarGlobal.Conexao);


                if (ret == "0")
                {


                    System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(VarGlobal.Conexao);
                    Conn.Open();
                    System.Data.SqlClient.SqlTransaction trans = Conn.BeginTransaction();


                    System.Data.SqlClient.SqlCommand Comm = new System.Data.SqlClient.SqlCommand();
                    Comm.CommandType = CommandType.Text;
                    Comm.Connection = Conn;
                    Comm.Transaction = trans;

                    DataTable dtTemp = new DataTable();

                    #region Conferencia

                    try
                    {



                        string idConferencia = "";
                        sql = "SELECT IDCONFERENCIA  FROM conferencia WHERE IDROMANEIO= " + idRomaneio;
                        dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                        if (dtTemp.Rows.Count > 0)
                            idConferencia = dtTemp.Rows[0][0].ToString();
                        if (idConferencia == "" || idConferencia == "0")
                        {

                            idConferencia = BdExterno.RetornarIDTabela("CONFERENCIA").ToString();
                            sql = "INSERT INTO CONFERENCIA (IdConferencia, IdRomaneio, IdUsuario, Inicio, Final, Situacao)";
                            sql += " VALUES (" + idConferencia + ", " + idRomaneio + ", " + VarGlobal.Usuario.IDUsuario + ", GetDate(), NULL, 'FORMACAO DE PALLET')";
                            Comm.CommandText = sql;
                            Comm.ExecuteNonQuery();
                        }



                        string idConferenciaPallet = "";
                        sql = "SELECT IDCONFERENCIAPALLET  FROM CONFERENCIAPALLET WHERE  TIPO='SEPARACAO'  AND  IDCONFERENCIA= " + idConferencia + " and idpallet=" + Helpers.RetirarDigitoUA(txtUa.Text);
                        dtTemp = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                        if (dtTemp.Rows.Count > 0)
                            idConferenciaPallet = dtTemp.Rows[0][0].ToString();
                        if (idConferenciaPallet == "" || idConferenciaPallet == "0")
                        {

                            idConferenciaPallet = BdExterno.RetornarIDTabela("CONFERENCIAPALLET").ToString();
                            sql = "INSERT INTO CONFERENCIAPALLET (IdConferenciaPallet, IdConferencia, IdPallet, IdDepositoPlantaLocalizacao )";
                            sql += " VALUES (" + idConferenciaPallet + ", " + idConferencia + ", " + Helpers.RetirarDigitoUA(txtUa.Text) + ", (select top 1 IdDepositoPlantaLocalizacao from Romaneio Where IdRomaneio=" + idRomaneio + "))";  //VERIFICAR DEPOIS O ENDERECO
                            Comm.CommandText = sql;
                            Comm.ExecuteNonQuery();
                        }

                        lblIdConferenciaPallet.Text = idConferenciaPallet;


                        //string idConferenciaPalletProduto = "";

                        //if (idConferenciaPalletProduto == "" || idConferenciaPalletProduto == "0")
                        //{

                        //    idConferenciaPalletProduto = BdExterno.RetornarIDTabela("CONFERENCIAPALLETPRODUTO").ToString();
                        //    sql = "INSERT INTO CONFERENCIAPALLETPRODUTO (IdConferenciaPalletProduto, IdConferenciaPallet, IdProdutoEmbalagem, Quantidade, tipo)";
                        //    sql += " VALUES (" + idConferenciaPalletProduto + ", " + idConferenciaPallet + ", " + lblIdProdutoEmbalagem.Text + ", " + int.Parse(float.Parse(txtQuantidade.Text).ToString()) + ", 'CONFERENCIA')";
                        //    Comm.CommandText = sql;
                        //    Comm.ExecuteNonQuery();
                        //}



                    #endregion

                        trans.Commit();
                        txtCodigoDeBarras.Enabled = true;
                        txtCodigoDeBarras.Focus();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Problema ao Identificar a UA");
                        trans.Rollback();
                    }
                    finally
                    {
                        if (Conn.State == ConnectionState.Open)
                        {
                            Conn.Close(); Conn.Dispose();
                        }
                    }

                }
                else
                {
                    lblIdConferenciaPallet.Text = ret;
                    txtCodigoDeBarras.Enabled = true;
                    txtCodigoDeBarras.Focus();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Problema com a UA");

            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }


        string idRomaneio = "";
        private void txtCodigoDeBarras_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (e.KeyChar == (char)Keys.Return && txtUa.Text != "" && txtCodigoDeBarras.Text != "")
                {
                    e.Handled = true;

                    ///verifica se existe o produto
                    string sql = "";
                    sql = " SELECT count(*)    FROM PRODUTO P     INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTO = P.IDPRODUTO     WHERE P.CODIGODEBARRAS = '" + txtCodigoDeBarras.Text + "'  ";
                    string x = Classes.BdExterno.ExecutarComRetorno(sql, VarGlobal.Conexao);

                    if (x == "0")
                    {
                        MessageBox.Show("PRODUTO NÂO ENCONTRADO", "Coletor Sistecno");
                        txtCodigoDeBarras.SelectAll();
                        return;
                    }




                    //sql = " SELECT ";
                    //sql += " PE.IDPRODUTOEMBALAGEM ";
                    //sql += " FROM PRODUTO P ";
                    //sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTO = P.IDPRODUTO ";
                    //sql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDPRODUTOEMBALAGEM = PE.IDPRODUTOEMBALAGEM ";
                    //sql += " INNER JOIN ROMANEIODOCUMENTOITEM RDI ON RDI.IDDOCUMENTOITEM =  DI.IDDOCUMENTOITEM ";
                    //sql += " INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDROMANEIODOCUMENTO = RDI.IDROMANEIODOCUMENTO ";
                    //sql += " WHERE P.CODIGODEBARRAS = '" + txtCodigoDeBarras.Text + "' ";
                    //sql += " AND RD.IDROMANEIO =  " + idRomaneio;

                    sql = " SELECT  PE.IDPRODUTOEMBALAGEM, P.CODIGODEBARRAS   FROM PRODUTO P  ";
                    sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTO = P.IDPRODUTO     ";
                    sql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE   ";
                    sql += " INNER JOIN ROMANEIODOCUMENTOITEM RDI ON RDI.IDDOCUMENTOITEM =  DI.IDDOCUMENTOITEM   ";
                    sql += " INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDROMANEIODOCUMENTO = RDI.IDROMANEIODOCUMENTO   ";
                    sql += " WHERE  ";
                    sql += " PE.IDProdutoCliente IN ";
                    sql += " ( ";
                    sql += " SELECT PE.IDProdutoCliente ";
                    sql += " FROM PRODUTO P ";
                    sql += " INNER JOIN ProdutoEmbalagem PE ON PE.IDProduto = P.IDProduto ";
                    sql += " WHERE P.CODIGODEBARRAS = '" + txtCodigoDeBarras.Text + "' ";
                    sql += " ) ";
                    sql += " AND RD.IDROMANEIO =   " + idRomaneio;
                    
                    DataTable dt = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                    
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("PRODUTO NÃO PERTENCE AO ROMANEIO: ." + idRomaneio, "Coletor Sistecno");
                        txtCodigoDeBarras.SelectAll();
                        return;
                    }

                    DataRow[] o = dt.Select("CODIGODEBARRAS='"+txtCodigoDeBarras.Text+"'", "");

                    if(o.Length==0)
                        lblIdProdutoEmbalagem.Text = dt.Rows[0]["IDPRODUTOEMBALAGEM"].ToString();  
                    else
                        lblIdProdutoEmbalagem.Text = o[0]["IDPRODUTOEMBALAGEM"].ToString();  
                  
                    txtQuantidade.Enabled = true;
                    txtQuantidade.Focus();
                }
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    if (int.Parse(float.Parse(txtQuantidade.Text).ToString()) > 0)
                    {
                        btnConfirmar.Enabled = true;
                        btnConfirmar.Focus();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problema com a Quantidade");

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            if (txtUa.Text == "" || txtCodigoDeBarras.Text == "" || txtQuantidade.Text == "")
            {
                btnConfirmar.Enabled = false;
                return;
            }

            try
            {

                string idConferenciaPalletProduto = Classes.BdExterno.RetornarIDTabela("CONFERENCIAPALLETPRODUTO").ToString();
                string sql = "INSERT INTO CONFERENCIAPALLETPRODUTO (IdConferenciaPalletProduto, IdConferenciaPallet, IdProdutoEmbalagem, Quantidade, TIPO)";
                sql += " VALUES (" + idConferenciaPalletProduto + ", " + lblIdConferenciaPallet.Text + ", " + lblIdProdutoEmbalagem.Text + ", " + int.Parse(float.Parse(txtQuantidade.Text).ToString()) + ", 'CONFERENCIA')";

                Classes.BdExterno.Executar(sql, VarGlobal.Conexao);

                txtUa.Text = "";
                txtCodigoDeBarras.Text = "";
                txtCodigoDeBarras.Enabled = false;
                btnConfirmar.Enabled = false;
                txtQuantidade.Text = "";
                txtQuantidade.Enabled = false;
                //lblIdProdutoEmbalagem.Text = "";
                //lblIdConferenciaPallet.Text = "";
                txtUa.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (idRomaneio == "" || idRomaneio == "0")
                {
                    MessageBox.Show("Selecione o Romaneio");
                    tabControl1.SelectedIndex = 0;
                    return;
                }
            }

            if (tabControl1.SelectedIndex == 2)
            {
                if (idRomaneio == "" || idRomaneio == "0")
                {
                    MessageBox.Show("Selecione o Romaneio");
                    tabControl1.SelectedIndex = 0;
                    return;
                }
                else
                {
                    CarregarLancamentos();
                }
            }

            if (tabControl1.SelectedIndex == 3)
            {
                if (idRomaneio == "" || idRomaneio == "0")
                {
                    MessageBox.Show("Selecione o Romaneio");
                    tabControl1.SelectedIndex = 0;
                    return;
                }
                else
                {
                    CarregarDivergencias();
                }
            }

            if (tabControl1.SelectedIndex == 0)
            {
                txtUa.Text = "";
                txtCodigoDeBarras.Text = "";
                txtCodigoDeBarras.Enabled = false;
                btnConfirmar.Enabled = false;
                txtQuantidade.Text = "";
                txtQuantidade.Enabled = false;
                lblIdProdutoEmbalagem.Text = "";
                lblIdConferenciaPallet.Text = "";
                linhaClicada = -1;
            }
        }

        private void CarregarDivergencias()
        {
            if (idRomaneio == "" || idRomaneio == "0")
            {
                MessageBox.Show("Selecione o Romaneio");
                tabControl1.SelectedIndex = 0;
            }

            Cursor.Current = Cursors.WaitCursor;

            try
            {

                string sql = " ";
                sql += " SELECT  'NAO' SEPARACAO,";
                sql += " PE.IDPRODUTOCLIENTE, R.IDROMANEIO, PC.CODIGO, PC.DESCRICAO , ";
                sql += " ISNULL(SUM(ISNULL(RDI.QUANTIDADE, 0)),0) QUANTIDADEBASE, ";
                sql += " ( ";
                sql += " SELECT ISNULL(SUM(ISNULL(CPP.QUANTIDADE, 0)),0) FROM CONFERENCIAPALLETPRODUTO CPP ";
                sql += " INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIAPALLET = CPP.IDCONFERENCIAPALLET ";
                sql += " INNER JOIN CONFERENCIA C ON C.IDCONFERENCIA = CP.IDCONFERENCIA ";
                sql += " WHERE C.IDROMANEIO =" + idRomaneio;
                //sql += " AND CPP.TIPO='CONFERENCIA' AND CPP.IDPRODUTOEMBALAGEM = PE.IDPRODUTOEMBALAGEM ) QTD_CONFERIDA, 0 QTD_SEPARADA ";
                sql += " AND CPP.TIPO='CONFERENCIA' AND CPP.IDPRODUTOEMBALAGEM in(SELECT PEI.IDPRODUTOEMBALAGEM FROM PRODUTO PII	INNER JOIN PRODUTOEMBALAGEM PEI ON PEI.IDPRODUTO = PII.IDPRODUTO WHERE PEI.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ) QTD_CONFERIDA, 0 QTD_SEPARADA ";
                sql += " FROM ROMANEIODOCUMENTOITEM RDI ";
                sql += " INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDROMANEIODOCUMENTO = RDI.IDROMANEIODOCUMENTO ";
                sql += " INNER JOIN ROMANEIO R ON R.IDROMANEIO = RD.IDROMANEIO ";
                sql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTOITEM = RDI.IDDOCUMENTOITEM ";
                sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ";
                sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
                sql += " WHERE R.IDROMANEIO =" + idRomaneio;
                sql += " GROUP BY  PE.IDPRODUTOCLIENTE, R.IDROMANEIO, PC.CODIGO, PC.DESCRICAO, PE.IDPRODUTOEMBALAGEM ";


                //DataTable dtDiver = Classes.BdExterno.RetornarDT("EXEC PRC_CONFERENCIA_DETALHE " + idRomaneio, VarGlobal.Conexao);
                DataTable dtDiver = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                DataTable dtAux = new DataTable();
                dtAux.Columns.Add("UA");
                dtAux.Columns.Add("PRODUTO");
                dtAux.Columns.Add("CODIGO");
                dtAux.Columns.Add("QTDSEPARADA", System.Type.GetType("System.Int32"));
                dtAux.Columns.Add("QTDCONFEREIDA", System.Type.GetType("System.Int32"));
                dtAux.Columns.Add("QTDBASE", System.Type.GetType("System.Int32"));


                for (int i = 0; i < dtDiver.Rows.Count; i++)
                {
                    string Separacao = dtDiver.Rows[i]["SEPARACAO"].ToString();


                    //considerar separação
                    if (Separacao == "SIM")
                    {
                        if (int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString()) != int.Parse(float.Parse(dtDiver.Rows[i]["QTD_SEPARADA"].ToString()).ToString())
                         || int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString()) != int.Parse(float.Parse(dtDiver.Rows[i]["QTD_CONFERIDA"].ToString()).ToString()))
                        {
                            DataRow r = dtAux.NewRow();
                            r[0] = "";
                            r[1] = dtDiver.Rows[i]["DESCRICAO"].ToString();
                            r[2] = dtDiver.Rows[i]["CODIGO"].ToString();
                            r[3] = int.Parse(float.Parse(dtDiver.Rows[i]["QTD_SEPARADA"].ToString()).ToString());
                            r[4] = int.Parse(float.Parse(dtDiver.Rows[i]["QTD_CONFERIDA"].ToString()).ToString());
                            r[5] = int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString());
                            dtAux.Rows.Add(r);
                        }
                    }
                    else
                    {
                        if (int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString()) != int.Parse(float.Parse(dtDiver.Rows[i]["QTD_CONFERIDA"].ToString()).ToString()))
                        {
                            DataRow r = dtAux.NewRow();
                            r[0] = "";
                            r[1] = dtDiver.Rows[i]["DESCRICAO"].ToString();
                            r[2] = dtDiver.Rows[i]["CODIGO"].ToString();
                            r[3] = int.Parse(float.Parse(dtDiver.Rows[i]["QTD_SEPARADA"].ToString()).ToString());
                            r[4] = int.Parse(float.Parse(dtDiver.Rows[i]["QTD_CONFERIDA"].ToString()).ToString());
                            r[5] = int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString());
                            dtAux.Rows.Add(r);
                        }
                    }
                }
                grdDivergencias.DataSource = dtAux;

                if (dtAux.Rows.Count > 0)
                    button1.Enabled = false;
                else
                    button1.Enabled = true;


            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao Carregar as Divergências.");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void CarregarLancamentos()
        {
            if (idRomaneio == "" || idRomaneio == "0")
            {
                MessageBox.Show("Selecione o Romaneio");
                tabControl1.SelectedIndex = 0;
            }

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string sql = " SELECT CPP.IDCONFERENCIAPALLETPRODUTO, CP.IDPALLET [UA],P.CODIGODEBARRAS [CODIGO DE BARRAS], CPP.QUANTIDADE  [QUANTIDADE]";
                sql += " FROM CONFERENCIA C ";
                sql += " INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIA = C.IDCONFERENCIA ";
                sql += " INNER JOIN CONFERENCIAPALLETPRODUTO CPP ON CPP.IDCONFERENCIAPALLET = CP.IDCONFERENCIAPALLET ";
                sql += " INNER JOIN PRODUTOEMBALAGEM PE  ON PE.IDPRODUTOEMBALAGEM = CPP.IDPRODUTOEMBALAGEM ";
                sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                sql += " WHERE C.IDROMANEIO = " + idRomaneio;
                sql += " AND CPP.TIPO = 'CONFERENCIA' ";
                grdLancamentos.DataSource = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao Carregar os Lancamentos", "Coletor Sistecno");
            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            ExcluirLancamento();
        }

        private void ExcluirLancamento()
        {
            int LinhaSelLanc = grdLancamentos.CurrentCell.RowNumber;
            string id = grdLancamentos[LinhaSelLanc, 3].ToString();
            try
            {
                string sql = "DELETE FROM CONFERENCIAPALLETPRODUTO WHERE IDCONFERENCIAPALLETPRODUTO=" + id;
                Classes.BdExterno.Executar(sql, VarGlobal.Conexao);
                CarregarLancamentos();
            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao excluir a ConferenciaPallet: " + id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (idRomaneio == "" || idRomaneio == "0")
            {
                MessageBox.Show("Selecione o Romaneio");
                tabControl1.SelectedIndex = 0;
            }

            Cursor.Current = Cursors.WaitCursor;

            string Sql = "";
            try
            {
                string sql = " ";
                /*sql += " SELECT  'NAO' SEPARACAO,";

                sql += " PE.IDPRODUTOCLIENTE, R.IDROMANEIO, PC.CODIGO, PC.DESCRICAO , ";
                sql += " ISNULL(SUM(ISNULL(RDI.QUANTIDADE, 0)),0) QUANTIDADEBASE, ";
                sql += " ( ";
                sql += " SELECT ISNULL(SUM(ISNULL(CPP.QUANTIDADE, 0)),0) FROM CONFERENCIAPALLETPRODUTO CPP ";
                sql += " INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIAPALLET = CPP.IDCONFERENCIAPALLET ";
                sql += " INNER JOIN CONFERENCIA C ON C.IDCONFERENCIA = CP.IDCONFERENCIA ";
                sql += " WHERE C.IDROMANEIO =" + idRomaneio;
                sql += " ) QTD_CONFERIDA, 0 QTD_SEPARADA ";
                sql += " FROM ROMANEIODOCUMENTOITEM RDI ";
                sql += " INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDROMANEIODOCUMENTO = RDI.IDROMANEIODOCUMENTO ";
                sql += " INNER JOIN ROMANEIO R ON R.IDROMANEIO = RD.IDROMANEIO ";
                sql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTOITEM = RDI.IDDOCUMENTOITEM ";
                sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ";
                sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
                sql += " WHERE R.IDROMANEIO =" + idRomaneio;
                sql += " GROUP BY  PE.IDPRODUTOCLIENTE, R.IDROMANEIO, PC.CODIGO, PC.DESCRICAO ";
                */


                //sql += " SELECT  'NAO' SEPARACAO,";
                //sql += " PE.IDPRODUTOCLIENTE, R.IDROMANEIO, PC.CODIGO, PC.DESCRICAO , ";
                //sql += " ISNULL(SUM(ISNULL(RDI.QUANTIDADE, 0)),0) QUANTIDADEBASE, ";
                //sql += " ( ";
                //sql += " SELECT ISNULL(SUM(ISNULL(CPP.QUANTIDADE, 0)),0) FROM CONFERENCIAPALLETPRODUTO CPP ";
                //sql += " INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIAPALLET = CPP.IDCONFERENCIAPALLET ";
                //sql += " INNER JOIN CONFERENCIA C ON C.IDCONFERENCIA = CP.IDCONFERENCIA ";
                //sql += " WHERE C.IDROMANEIO =" + idRomaneio;
                //sql += " AND CPP.TIPO='CONFERENCIA' AND CPP.IDPRODUTOEMBALAGEM = PE.IDPRODUTOEMBALAGEM ) QTD_CONFERIDA, 0 QTD_SEPARADA ";
                //sql += " FROM ROMANEIODOCUMENTOITEM RDI ";
                //sql += " INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDROMANEIODOCUMENTO = RDI.IDROMANEIODOCUMENTO ";
                //sql += " INNER JOIN ROMANEIO R ON R.IDROMANEIO = RD.IDROMANEIO ";
                //sql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTOITEM = RDI.IDDOCUMENTOITEM ";
                //sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ";
                //sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                //sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
                //sql += " WHERE R.IDROMANEIO =" + idRomaneio;
                //sql += " GROUP BY  PE.IDPRODUTOCLIENTE, R.IDROMANEIO, PC.CODIGO, PC.DESCRICAO, PE.IDPRODUTOEMBALAGEM ";



                //string sql = " ";
                sql += " SELECT  'NAO' SEPARACAO,";
                sql += " PE.IDPRODUTOCLIENTE, R.IDROMANEIO, PC.CODIGO, PC.DESCRICAO , ";
                sql += " ISNULL(SUM(ISNULL(RDI.QUANTIDADE, 0)),0) QUANTIDADEBASE, ";
                sql += " ( ";
                sql += " SELECT ISNULL(SUM(ISNULL(CPP.QUANTIDADE, 0)),0) FROM CONFERENCIAPALLETPRODUTO CPP ";
                sql += " INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIAPALLET = CPP.IDCONFERENCIAPALLET ";
                sql += " INNER JOIN CONFERENCIA C ON C.IDCONFERENCIA = CP.IDCONFERENCIA ";
                sql += " WHERE C.IDROMANEIO =" + idRomaneio;
                //sql += " AND CPP.TIPO='CONFERENCIA' AND CPP.IDPRODUTOEMBALAGEM = PE.IDPRODUTOEMBALAGEM ) QTD_CONFERIDA, 0 QTD_SEPARADA ";
                sql += " AND CPP.TIPO='CONFERENCIA' AND CPP.IDPRODUTOEMBALAGEM in(SELECT PEI.IDPRODUTOEMBALAGEM FROM PRODUTO PII	INNER JOIN PRODUTOEMBALAGEM PEI ON PEI.IDPRODUTO = PII.IDPRODUTO WHERE PEI.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ) QTD_CONFERIDA, 0 QTD_SEPARADA ";
                sql += " FROM ROMANEIODOCUMENTOITEM RDI ";
                sql += " INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDROMANEIODOCUMENTO = RDI.IDROMANEIODOCUMENTO ";
                sql += " INNER JOIN ROMANEIO R ON R.IDROMANEIO = RD.IDROMANEIO ";
                sql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTOITEM = RDI.IDDOCUMENTOITEM ";
                sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ";
                sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
                sql += " WHERE R.IDROMANEIO =" + idRomaneio;
                sql += " GROUP BY  PE.IDPRODUTOCLIENTE, R.IDROMANEIO, PC.CODIGO, PC.DESCRICAO, PE.IDPRODUTOEMBALAGEM ";


                //DataTable dtDiver = Classes.BdExterno.RetornarDT("EXEC PRC_CONFERENCIA_DETALHE " + idRomaneio, VarGlobal.Conexao);
                DataTable dtDiver = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                //DataTable dtDiver = Classes.BdExterno.RetornarDT("EXEC PRC_CONFERENCIA_DETALHE " + idRomaneio, VarGlobal.Conexao);
                DataTable dtAux = new DataTable();
                dtAux.Columns.Add("UA");
                dtAux.Columns.Add("PRODUTO");
                dtAux.Columns.Add("CODIGO");
                dtAux.Columns.Add("QTDSEPARADA", System.Type.GetType("System.Int32"));
                dtAux.Columns.Add("QTDCONFEREIDA", System.Type.GetType("System.Int32"));
                dtAux.Columns.Add("QTDBASE", System.Type.GetType("System.Int32"));

                for (int i = 0; i < dtDiver.Rows.Count; i++)
                {
                    string Separacao = dtDiver.Rows[i]["SEPARACAO"].ToString();

                    //considerar separação
                    if (Separacao == "SIM")
                    {
                        if (int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString()) != int.Parse(float.Parse(dtDiver.Rows[i]["QTD_SEPARADA"].ToString()).ToString())
                         || int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString()) != int.Parse(float.Parse(dtDiver.Rows[i]["QTD_CONFERIDA"].ToString()).ToString()))
                        {
                            DataRow r = dtAux.NewRow();
                            r[0] = "";
                            r[1] = dtDiver.Rows[i]["DESCRICAO"].ToString();
                            r[2] = dtDiver.Rows[i]["CODIGO"].ToString();
                            r[3] = int.Parse(float.Parse(dtDiver.Rows[i]["QTD_SEPARADA"].ToString()).ToString());
                            r[4] = int.Parse(float.Parse(dtDiver.Rows[i]["QTD_CONFERIDA"].ToString()).ToString());
                            r[5] = int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString());
                            dtAux.Rows.Add(r);
                        }
                    }
                    else
                    {
                        if (int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString()) != int.Parse(float.Parse(dtDiver.Rows[i]["QTD_CONFERIDA"].ToString()).ToString()))
                        {
                            DataRow r = dtAux.NewRow();
                            r[0] = "";
                            r[1] = dtDiver.Rows[i]["DESCRICAO"].ToString();
                            r[2] = dtDiver.Rows[i]["CODIGO"].ToString();
                            r[3] = int.Parse(float.Parse(dtDiver.Rows[i]["QTD_SEPARADA"].ToString()).ToString());
                            r[4] = int.Parse(float.Parse(dtDiver.Rows[i]["QTD_CONFERIDA"].ToString()).ToString());
                            r[5] = int.Parse(float.Parse(dtDiver.Rows[i]["QUANTIDADEBASE"].ToString()).ToString());
                            dtAux.Rows.Add(r);
                        }
                    }
                }
                grdDivergencias.DataSource = dtAux;

                if (dtAux.Rows.Count == 0)
                {
                    Sql = "UPDATE CONFERENCIA SET FINAL=GETDATE(), SITUACAO='CONFERIDA' WHERE IDROMANEIO=" + idRomaneio;
                    Classes.BdExterno.Executar(Sql, VarGlobal.Conexao);

                    Sql = "UPDATE MOVIMENTACAO SET ESTOQUEPROCESSADO='SIM' WHERE IDMOVIMENTACAO = (SELECT TOP 1 IDMOVIMENTACAO FROM MOVIMENTACAOITEM WHERE IDROMANEIO=" + idRomaneio + ")";
                    Classes.BdExterno.Executar(Sql, VarGlobal.Conexao);

                    MessageBox.Show("Conferencia Finalizada com Sucesso!", "Conferencia OK");

                    tabControl1.SelectedIndex = 0;

                    txtUa.Text = "";
                    txtCodigoDeBarras.Text = "";
                    txtCodigoDeBarras.Enabled = false;
                    btnConfirmar.Enabled = false;
                    txtQuantidade.Text = "";
                    txtQuantidade.Enabled = false;
                    lblIdProdutoEmbalagem.Text = "";
                    lblIdConferenciaPallet.Text = "";
                    linhaClicada = -1;


                    CarregarConferenciasPendentes("");
                }
                else
                {
                    MessageBox.Show("ExisteM Pendencias Para o Romaneio: " + idRomaneio);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao Finalizar a Conferencia. Sql: " + Sql);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}