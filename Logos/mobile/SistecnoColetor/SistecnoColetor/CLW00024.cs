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
    public partial class CLW00024 : Form
    {
        public CLW00024()
        {
            InitializeComponent();
        }

        private void WEB00024_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = this.Name;
                statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
                lblTitulo.Text = VarGlobal.NomePrograma;
                CarregarRomaneios();
            }
            catch (Exception)
            {
            }
        }

        int linhaClicada = -1;
        string idRomaneio = "";
        private void CarregarRomaneios()
        {
            string sql = "";
            sql = "SELECT IDROMANEIO, OBSERVACAO1 FROM ROMANEIO R WHERE R.TIPO ='ENTRADA' AND R.DIVISAO ='ARMAZENAGEM' AND ANDAMENTO = 'AGUARDANDO RECEBIMENTO' and idromaneio not in (select IdRomaneio from conferencia where SITUACAO='CONFERIDA' ) ORDER BY 1 DESC";
            DataTable dt = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);
            dataGrid1.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                dataGrid1.Select(0);
                setarLinha();
            }
        }

        public void setarLinha()
        {
            linhaClicada = dataGrid1.CurrentCell.RowNumber;
            idRomaneio = dataGrid1[linhaClicada, 0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnNovoRomaneio.Visible = false;
            dataGrid1.Visible = false;
            pnlNovoRomaneio.Visible = true;
            txtNovoRomanioChave.Text = "";


            dtNovoRom = new DataTable();
            dtNovoRom.Columns.Add("IDDOCUMENTO");
            dtNovoRom.Columns.Add("CHAVE");
            dtNovoRom.Columns.Add("Tipo");

            txtNovoRomanioChave.Focus();

        }

        DataTable dtNovoRom;

        private void btnConfirmarRomaneio_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                List<string> notas = new List<string>();
                List<string> tipo = new List<string>();

                for (int i = 0; i < dtNovoRom.Rows.Count; i++)
                {
                    notas.Add(dtNovoRom.Rows[i][0].ToString());
                    tipo.Add(dtNovoRom.Rows[i][2].ToString());

                }

                int IdR = new Classes.BLL.Estoque().GerarRomaneio(notas, tipo, "ARMAZENAGEM", "AGUARDANDO RECEBIMENTO");

                MessageBox.Show("Romaneio " + IdR.ToString() + ". Gerado Com Sucesso.", "Atenção");
                CarregarRomaneios();


                btnNovoRomaneio.Visible = true;
                dataGrid1.Visible = true;
                pnlNovoRomaneio.Visible = false;
                dtNovoRom = null;
                dataGrid2.DataSource = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema ao Criar o Romaneio. " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void txtNovoRomanioChave_KeyPress(object sender, KeyPressEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    if (txtNovoRomanioChave.Text != "")
                    {
                        PesquisarNota();
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void PesquisarNota()
        {
            try
            {
                string sql = "SELECT D.IDDOCUMENTO,  D.DOCUMENTODOCLIENTE4 ";
                sql += " FROM DOCUMENTO D  ";
                sql += " INNER JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = D.IDDOCUMENTO ";
                sql += " WHERE TIPODEDOCUMENTO = 'NOTA FISCAL' ";
                sql += " AND DF.SITUACAO ='LIBERADO PARA RECEBIMENTO'  ";
                sql += " AND D.DOCUMENTODOCLIENTE4='" + txtNovoRomanioChave.Text.Trim() + "'";

                DataTable dt = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Nota Fiscal não encontrada.");

                }
                else
                {
                    DataRow x = dtNovoRom.NewRow();
                    x[0] = dt.Rows[0][0].ToString();
                    x[1] = dt.Rows[0][1].ToString();
                    x[2] = (chkDevolucao.Checked ? "DEVOLUCAO" : "ENTRADA");
                    dtNovoRom.Rows.Add(x);

                    dataGrid2.DataSource = dtNovoRom;
                }

                txtNovoRomanioChave.Text = "";
                txtNovoRomanioChave.Focus();

            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {
            setarLinha();
            //  MessageBox.Show(idRomaneio);
            tabControl1.SelectedIndex = 1;
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            setarLinha();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 && idRomaneio == "0")
            {
                MessageBox.Show("Selecione Um Romaneio.");
                tabControl1.SelectedIndex = 0;

            }
            else if (tabControl1.SelectedIndex == 2 && idRomaneio == "0")
            {
                MessageBox.Show("Selecione Um Romaneio.");
                tabControl1.SelectedIndex = 0;

            }
            else if (tabControl1.SelectedIndex == 2 && idRomaneio != "0")
            {
                CarregarLancamento();
            }
            else if (tabControl1.SelectedIndex == 3 && idRomaneio != "0")
            {
                CarregarDivergencias();
            }
            else
            {
                txtExecUa.Text = "";
                txtExecUa.Focus();
            }
        }

        private void CarregarDivergencias()
        {
            string sql = "";
            sql += " SELECT P.CODIGODEBARRAS, PC.CODIGO, PC.DESCRICAO, PE.IDPRODUTOCLIENTE, SUM(RDI.QUANTIDADE) QUANTIDADEBASE, ";
            sql += " isnull(( ";
            sql += " 	SELECT SUM(ISNULL(CPP.QUANTIDADE,0)) ";
            sql += " FROM CONFERENCIA C  ";
            sql += " INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIA = C.IDCONFERENCIA ";
            sql += " INNER JOIN CONFERENCIAPALLETPRODUTO CPP ON CPP.IDCONFERENCIAPALLET = CP.IDCONFERENCIAPALLET  ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PEI ON PEI.IDPRODUTOEMBALAGEM = CPP.IDPRODUTOEMBALAGEM ";
            sql += " WHERE C.IDROMANEIO=RD.IDROMANEIO ";
            sql += " AND CPP.TIPO IN( 'ENTRADA', 'SAIDA') ";
            sql += " AND PEI.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
            sql += " ), 0) QUANTIDADELIDO ";
            sql += " FROM ROMANEIODOCUMENTOITEM RDI ";
            sql += " INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDROMANEIODOCUMENTO = RDI.IDROMANEIODOCUMENTO ";
            sql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTOITEM = RDI.IDDOCUMENTOITEM ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM  = DI.IDPRODUTOEMBALAGEM ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = DI.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
            sql += " WHERE RD.IDROMANEIO =  "+idRomaneio;
            sql += " GROUP BY  RD.IDROMANEIO , P.CODIGODEBARRAS, PC.CODIGO, PC.DESCRICAO, PE.IDPRODUTOCLIENTE, PE.IDPRODUTOEMBALAGEM ";
            sql += " HAVING  ";
            sql += " isnull(( ";
            sql += " 	SELECT SUM(ISNULL(CPP.QUANTIDADE,0)) ";
            sql += " FROM CONFERENCIA C  ";
            sql += " INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIA = C.IDCONFERENCIA ";
            sql += " INNER JOIN CONFERENCIAPALLETPRODUTO CPP ON CPP.IDCONFERENCIAPALLET = CP.IDCONFERENCIAPALLET  ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PEI ON PEI.IDPRODUTOEMBALAGEM = CPP.IDPRODUTOEMBALAGEM ";
            sql += " WHERE C.IDROMANEIO=RD.IDROMANEIO ";
            sql += " AND CPP.TIPO IN( 'ENTRADA', 'SAIDA') ";
            sql += " AND PEI.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
            sql += " ), 0) <> SUM(RDI.QUANTIDADE) ";

            grdDivergencias.DataSource = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

        }

        private void CarregarLancamento()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;


                string sql = " SELECT CP.IDPALLET UA, CPP.IDCONFERENCIAPALLETPRODUTO, P.CODIGODEBARRAS, CPP.QUANTIDADE, CP.VALIDADE, CP.REFERENCIA, CP.IDCONFERENCIAPALLET ";
                sql += " FROM CONFERENCIA C  ";
                sql += " INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIA = C.IDCONFERENCIA ";
                sql += " INNER JOIN CONFERENCIAPALLETPRODUTO CPP ON CPP.IDCONFERENCIAPALLET = CP.IDCONFERENCIAPALLET  ";
                sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = CPP.IDPRODUTOEMBALAGEM ";
                sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                sql += " WHERE C.IDROMANEIO=" + idRomaneio;
                sql += " AND CPP.TIPO IN( 'ENTRADA', 'SAIDA') ";


                grdLancamentos.DataSource = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);
            }
            catch (Exception)
            {

                MessageBox.Show("Selecione um Romaneio");
            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }

        }

        private void txtExecUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (e.KeyChar == (char)Keys.Return && txtExecUa.Text.Length > 0)
                {
                    //verifica se a Ua esta Vazia
                    string sql = " SELECT * FROM UNIDADEDEARMAZENAGEM UA ";
                    sql += " LEFT JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDUNIDADEDEARMAZENAGEM=UA.IDUNIDADEDEARMAZENAGEM ";
                    sql += " WHERE UA.IDDEPOSITOPLANTALOCALIZACAO=0 ";
                    sql += " AND UAL.SALDO IS NULL ";
                    sql += " AND UA.STATUS='AGUARDANDO TRANSFERENCIA' ";
                    sql += " AND UA.IDUNIDADEDEARMAZENAGEM NOT IN (SELECT IDPALLET FROM CONFERENCIAPALLET) ";
                    sql += " AND UA.IDUNIDADEDEARMAZENAGEM = " + Helpers.RetirarDigitoUA(txtExecUa.Text);

                    DataTable dtx = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    if (dtx.Rows.Count == 0)
                    {
                        MessageBox.Show("Ua Inválida ou não esta vazia");
                        txtExecUa.Text = "";
                        txtExecUa.Focus();
                    }
                    else
                    {
                        txtExecCodigoDeBarras.Enabled = true;
                        txtExecCodigoDeBarras.Focus();
                    }

                }
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

        private void txtExecCodigoDeBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return || txtExecCodigoDeBarras.Text.Length == 0)
                return;

            Cursor.Current = Cursors.WaitCursor;
            lblIdProdutoCliente.Text = "";
            lblIdProdutoEmbalagem.Text = "";
            lblSolicitaLote.Text = "";
            lblSolicitaValidade.Text = "";

            try
            {
                string sql = "";
                sql = "SELECT RD.IDROMANEIO, P.CODIGODEBARRAS, PE.IDPRODUTOCLIENTE, PE.IDPRODUTOEMBALAGEM, PC.SOLICITARDATADEVALIDADE, PC.SOLICITARLOTE  ";
                sql += " FROM ROMANEIODOCUMENTOITEM RDI ";
                sql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTOITEM = RDI.IDDOCUMENTOITEM ";
                sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ";
                sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                sql += " INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDROMANEIODOCUMENTO = RDI.IDROMANEIODOCUMENTO ";
                sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
                sql += " WHERE RD.IDROMANEIO =  " + idRomaneio;
                sql += " AND   P.CODIGODEBARRAS='" + txtExecCodigoDeBarras.Text.Trim() + "' ";
                sql += " ORDER BY PE.IDPRODUTOCLIENTE ";

                DataTable DT = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (DT.Rows.Count > 0)
                {
                    lblIdProdutoCliente.Text = DT.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                    lblIdProdutoEmbalagem.Text = DT.Rows[0]["IDPRODUTOEMBALAGEM"].ToString();
                    lblSolicitaLote.Text = DT.Rows[0]["SOLICITARLOTE"].ToString();
                    lblSolicitaValidade.Text = DT.Rows[0]["SOLICITARDATADEVALIDADE"].ToString();

                    txtQuantidade.Enabled = true;
                    txtQuantidade.Focus();

                }
                else
                {
                    MessageBox.Show("Produto " + txtExecCodigoDeBarras.Text + " Não pertence ao Romaneio: " + idRomaneio + ".");
                    txtExecCodigoDeBarras.SelectAll();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema ao Procurar o Produto");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return || txtQuantidade.Text.Length == 0)
                return;

            Cursor.Current = Cursors.WaitCursor;


            try
            {
                if (Helpers.ValidaNumeto(txtQuantidade.Text))
                {
                    txtLote.Enabled = true;
                    txtValidade.Enabled = true;
                    txtLote.Focus();

                    if (lblSolicitaValidade.Text != "SIM" && lblSolicitaLote.Text != "SIM")
                        btnConfirmarExecucao.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Quantidade Inválida");
                    txtQuantidade.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema ao Procurar o Produto");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != (char)Keys.Return)
                return;
            Cursor.Current = Cursors.WaitCursor;


            try
            {
                txtValidade.Focus();
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

        private void txtValidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;

            Cursor.Current = Cursors.WaitCursor;


            try
            {
                if (Helpers.IsData(txtValidade.Text))
                {
                    btnConfirmarExecucao.Enabled = true;
                    btnConfirmarExecucao.Focus();
                }
                else
                {
                    MessageBox.Show("Data Inválida");
                    txtValidade.SelectAll();
                }
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

        private void btnConfirmarExecucao_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (txtExecUa.Text == "" || txtExecCodigoDeBarras.Text == "" || txtQuantidade.Text == "")
                {
                    btnConfirmarExecucao.Enabled = false;
                    return;
                }


                if (lblSolicitaLote.Text == "SIM" && txtLote.Text == "")
                {
                    MessageBox.Show("Informe o Lote.");
                    txtLote.Focus();
                }


                if (lblSolicitaValidade.Text == "SIM" && txtValidade.Text == "")
                {
                    MessageBox.Show("Informe o Validade.");
                    txtValidade.Focus();
                }

                string idcc = "";
                string sql = "";
                string s = "select * from conferencia where idromaneio=" + idRomaneio;
                DataTable xs = Classes.BdExterno.RetornarDT(s, VarGlobal.Conexao);

                if (xs.Rows.Count > 0)
                    idcc = xs.Rows[0]["IdConferencia"].ToString();
                else
                {
                    idcc = Classes.BdExterno.RetornarIDTabela("CONFERENCIA").ToString();
                    sql = "INSERT INTO CONFERENCIA (IdConferencia, IdRomaneio, IdUsuario, Inicio, Final, Situacao)";
                    sql += " VALUES (" + idcc + ", " + idRomaneio + ", " + VarGlobal.Usuario.IDUsuario + ", GetDate(), NULL, 'FORMACAO DE PALLET'); ";

                }

                s = "Select * from romaneio where idromaneio=" + idRomaneio;
                DataTable xrom = Classes.BdExterno.RetornarDT(s, VarGlobal.Conexao);

            if(txtValidade.Text!="")
                txtValidade.Text = (new DateTime(int.Parse(txtValidade.Text.Substring(6, 4)), int.Parse(txtValidade.Text.Substring(3, 2)), int.Parse(txtValidade.Text.Substring(0, 2)))).ToString("yyyy-MM-dd"); 

                string idConferenciaPallet = Classes.BdExterno.RetornarIDTabela("CONFERENCIAPALLET").ToString();
                //string sql = "";
                sql += "INSERT INTO CONFERENCIAPALLET (IdConferenciaPallet, IdConferencia, IdPallet, IdDepositoPlantaLocalizacao, Tipo, Embarcado, UACompleta, ExecutouServico, IdDepositoPlantaLocalizacaoOrigem, IdUnidadeDeArmazenagemOrigem , Referencia, Validade) ";
                sql += " VALUES(" + idConferenciaPallet + ", " + idcc + ", " + Helpers.RetirarDigitoUA(txtExecUa.Text) + ", 0, 'CONFERENCIA', NULL, NULL, NULL, NULL, NULL, '" + txtLote.Text.Trim() + "', " + (txtValidade.Text.Trim() == "" ? "NULL" : "'" +DateTime.Parse(txtValidade.Text).ToString("yyyy-MM-dd") + "'") + ")";

                string idConferenciaPalletProduto = Classes.BdExterno.RetornarIDTabela("CONFERENCIAPALLETPRODUTO").ToString();
                sql += "; INSERT INTO CONFERENCIAPALLETPRODUTO (IdConferenciaPalletProduto, IdConferenciaPallet, IdProdutoEmbalagem, Quantidade, TIPO, Conferido)";
                sql += " VALUES (" + idConferenciaPalletProduto + ", " + idConferenciaPallet + ", " + lblIdProdutoEmbalagem.Text + ", " + int.Parse(float.Parse(txtQuantidade.Text).ToString()) + ", 'ENTRADA', 'SIM')";

                Classes.BdExterno.ExecutarTrans(sql, VarGlobal.Conexao);

                lblSolicitaValidade.Text = "";
                lblSolicitaLote.Text = "";
                lblIdProdutoCliente.Text = "";
                lblIdProdutoEmbalagem.Text = "";
                btnConfirmarExecucao.Enabled = false;
                txtLote.Text = "";
                txtLote.Enabled = false;
                txtValidade.Enabled = false;
                txtValidade.Text = "";
                txtQuantidade.Text = "";
                txtQuantidade.Enabled = false;
                txtExecCodigoDeBarras.Text = "";
                txtExecCodigoDeBarras.Enabled = false;
                txtExecUa.Text = "";
                txtExecUa.Focus();


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

        private void btnConcluir_Click(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                int l = grdLancamentos.CurrentRowIndex;
                string sql = "delete from conferenciaPalletProduto where idConferenciaPallet=" + grdLancamentos[l, 6].ToString() + " ;";
                sql += " delete from conferenciaPallet where idConferenciaPallet=" + grdLancamentos[l, 6].ToString() ;
                Classes.BdExterno.ExecutarTrans(sql, VarGlobal.Conexao);
                CarregarLancamento();
            }

            catch (Exception ex )
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pnlNovoRomaneio.Visible = false;
            btnNovoRomaneio.Visible = true;
            dataGrid1.Visible = true;

        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string sql = "EXEC PRC_LOTE_ENTRADA " + idRomaneio + ", " + VarGlobal.Usuario.IDUsuario + "," + VarGlobal.Usuario.UltimaFilial;

                Classes.BdExterno.ExecutarTrans(sql, VarGlobal.Conexao);

                pnlNovoRomaneio.Visible = false;
                btnNovoRomaneio.Visible = true;
                dataGrid1.Visible = true;
                MessageBox.Show("Operação Efetuada com sucesso.");
                tabControl1.SelectedIndex = 0;
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
    }
}