using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistecnoColetor
{
    public partial class frmConferencia_Movimentacao : Form
    {
        public frmConferencia_Movimentacao()
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
                //idRomaneio = dataGrid1[linhaClicada, 0].ToString();
                txtUa.Text = Helpers.RetirarDigitoUA(txtUa.Text);

                string sql = "  SELECT IDPALLET FROM CONFERENCIA C ";
                sql += " INNER JOIN  CONFERENCIAPALLET CP ON CP.IDCONFERENCIA = C.IDCONFERENCIA ";
                sql += " WHERE IDROMANEIO =" + idRomaneio;
                sql += " AND TIPO = 'SEPARACAO'";
                sql += " AND IDPALLET = " + txtUa.Text;

                DataTable dtx = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dtx.Rows.Count == 0)
                {
                    MessageBox.Show("UA Incorreta ou pertence a outra conferencia.", "Coletor Sistecno");
                    txtUa.SelectAll();
                    return;
                }
                else
                {
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
                                      
                    string sql = "";
                    sql += " SELECT * FROM CONFERENCIAPALLETPRODUTO CPP ";
                    sql += " INNER JOIN  CONFERENCIAPALLET CP ON CP.IDCONFERENCIAPALLET = CPP.IDCONFERENCIAPALLET ";
                    sql += " INNER JOIN CONFERENCIA C ON C.IDCONFERENCIA = CP.IDCONFERENCIA ";
                    sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = CPP.IDPRODUTOEMBALAGEM ";
                    sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                    sql += " WHERE IDROMANEIO = " + idRomaneio;
                    sql += " AND cpp.TIPO = 'SEPARACAO' ";
                    sql += " AND P.CODIGODEBARRAS = '" + txtCodigoDeBarras.Text + "' ";
                    sql += " AND IDPALLET=" + txtUa.Text;

                    DataTable dt = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Produto Não pertence a Ua.", "Coletor Sistecno");
                        txtCodigoDeBarras.SelectAll();
                        return;
                    }
                    lblIdProdutoEmbalagem.Text = dt.Rows[0]["IDPRODUTOEMBALAGEM"].ToString();
                    lblIdConferenciaPallet.Text = dt.Rows[0]["IdConferenciaPallet"].ToString();
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
                lblIdProdutoEmbalagem.Text = "";
                lblIdConferenciaPallet.Text = "";
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
            if (tabControl1.SelectedIndex == 2)
                CarregarLancamentos();


            if (tabControl1.SelectedIndex == 3)
                CarregarDivergencias();
        }

        private void CarregarDivergencias()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {


                DataTable dtDiver = Classes.BdExterno.RetornarDT("EXEC PRC_CONFERENCIA_DETALHE " + idRomaneio, VarGlobal.Conexao);

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
            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao excluir a ConferenciaPallet: " + id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string Sql = "";
            try
            {
                DataTable dtDiver = Classes.BdExterno.RetornarDT("EXEC PRC_CONFERENCIA_DETALHE " + idRomaneio, VarGlobal.Conexao);
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